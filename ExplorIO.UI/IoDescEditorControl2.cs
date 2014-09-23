using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExplorIO.Data;
using System.Drawing.Imaging;
using System.Threading;

namespace ExplorIO.UI {
    public partial class IoDescEditorControl2 : UserControl
    {
        #region Constant Fields
        const int byteHeight = 70;
        const int byteWidth = 770;
        const int horizontalSpacerSize = 5;
        const int vericalSpacerSize = 5;
        const int scrollDivisor = 10;
        #endregion

        #region Fields and Properties
        Color unusedByteColor = SystemColors.ActiveCaption;
        Color usedByteColor = Color.LightSalmon;

        private SortedList<int, ItemRectContainer> itemRects;
        private List<ItemRectContainer> selectedItemRects;
        private IoDescription ioDesc;
        private Bitmap bmp;
        private Graphics g;
        private int virtHeight;        
        int lastHeight = 0;
        bool mouseDown;
        bool ctrlPressed;
        object syncLock = new object();

        public IoDescription IoDesc {
            get { return ioDesc; }
            set {
                if (ioDesc == value)
                    return;
                if (value == null)
                    return;
                ioDesc = value;

                this.itemRects = CreateItemRectangles(ioDesc);
                this.virtHeight = (int)((ioDesc.Size) * (byteHeight + horizontalSpacerSize));
                CreateIoDescBitmap();
                this.Invalidate();
            }
        }
        #endregion

        #region Initialization
        public IoDescEditorControl2() {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);

            this.itemRects = new SortedList<int, ItemRectContainer>();
            this.selectedItemRects = new List<ItemRectContainer>();
            this.DoubleBuffered = true;
            this.virtHeight = 1;

            this.mouseDown = false;
            this.ctrlPressed = false;

            this.MouseDown += new MouseEventHandler(IoDescEditor2_MouseDown);
            this.MouseUp += new MouseEventHandler(IoDescEditorControl2_MouseUp);
            this.MouseMove += new MouseEventHandler(IoDescEditorControl2_MouseMove);
            this.MouseLeave += new EventHandler(IoDescEditorControl2_MouseLeave);
            this.MouseClick += new MouseEventHandler(IoDescEditorControl2_MouseClick);

            this.KeyDown += new KeyEventHandler(IoDescEditorControl2_KeyDown);
            this.KeyUp += new KeyEventHandler(IoDescEditorControl2_KeyUp);
            this.LostFocus += new EventHandler(IoDescEditorControl2_LostFocus);

            this.vScrollBar.ValueChanged += new EventHandler(vScrollBar_ValueChanged);
        }
        #endregion

        #region Event Handler
        private void IoDescEditorControl2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (17): // Ctrl
                    this.ctrlPressed = true;
                    break;
                case 107: // Plus
                    this.AddSelectedToIoDesc();
                    break;
                case 109: // Minus
                    this.RemoveSelectedFromIoDec();
                    break;
            }
        }

        private void IoDescEditorControl2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 17)
                this.ctrlPressed = false;
        }

        private void IoDescEditorControl2_LostFocus(object sender, EventArgs e)
        {
            this.ctrlPressed = false;
            this.mouseDown = false;
        }

        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void IoDescEditorControl2_MouseLeave(object sender, EventArgs e)
        {
            mouseDown = false;
        }

        private void IoDescEditorControl2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                //CreateIoDescBitmap(e.X, e.Y);
                //this.Invalidate();
            }
        }

        private void IoDescEditorControl2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            CreateIoDescBitmap();
            this.Invalidate();
        }

        private void IoDescEditor2_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.itemRects.Count == 0)
                return;
            int worldMousePos = (e.Y + vScrollBar.Value * scrollDivisor);
            ItemRectContainer clickedRect = null;
            for (int i = worldMousePos; i >= 0; i--)
            {
                if (itemRects.ContainsKey(i))
                {
                    if (itemRects[i].ItemRect.Bottom > worldMousePos &&
                        e.X >= itemRects[i].ItemRect.Left &&
                        e.X <= itemRects[i].ItemRect.Right)
                        clickedRect = itemRects[i];
                    break;
                }
            }
            if (clickedRect == null && this.selectedItemRects.Count == 1)
                return;
            if (!this.ctrlPressed)
            {
                this.selectedItemRects.Clear();
                this.selectedItemRects.Add(clickedRect);
            }
            else if (this.selectedItemRects.Contains(clickedRect))
                this.selectedItemRects.Remove(clickedRect);
            else
                this.selectedItemRects.Add(clickedRect);
            CreateIoDescBitmap();
            this.Invalidate();
        }

        private void IoDescEditorControl2_MouseClick(object sender, MouseEventArgs e)
        {

        }
        #endregion

        #region Tools
        private void RePaintControl(bool reCreateItemRects)
        {
            //Thread t = new Thread(new ThreadStart(delegate()
            //{
                if (reCreateItemRects)
                {
                    this.itemRects = this.CreateItemRectangles(this.ioDesc);
                    
                }
                this.CreateIoDescBitmap();
                this.Invalidate();
                this.Update();
            //}));
        }

        private void AddSelectedToIoDesc()
        {
            if (this.selectedItemRects.Count == 0)
                return;
            foreach (ItemRectContainer itemRect in this.selectedItemRects)
            {
                if (itemRect.Item != null)
                    return;
            }

            IoDescriptionItem item = new IoGroup(selectedItemRects.Count, selectedItemRects[0].Address);
            this.ioDesc.AddItem(item);
            this.selectedItemRects.Clear();
            this.itemRects = CreateItemRectangles(ioDesc);
            this.CreateIoDescBitmap();
            this.Invalidate();
        }

        private void RemoveSelectedFromIoDec()
        {
            if (this.selectedItemRects.Count == 0)
                return;
            foreach (ItemRectContainer item in selectedItemRects)
            {
                this.ioDesc.RemoveItem(item.Item);
            }
            RePaintControl(true);
        }


        private SortedList<int, ItemRectContainer> CreateItemRectangles(IoDescription ioDesc)
        {
            lock (syncLock)
            {
                SortedList<int, ItemRectContainer> rectList = new SortedList<int, ItemRectContainer>();
                ItemRectContainer rectContainer;
                int size;

                if (ioDesc == null || ioDesc.Size <= 0)
                    return rectList;

                for (int address = 0, index = 0; address < ioDesc.Size; address++, index++)
                {
                    IoDescriptionItem item = ioDesc.GetItemAtAddress(address);
                    if (item == null)
                        size = 1;
                    else
                        size = item.Size;

                    Rectangle rect = new Rectangle(
                        vericalSpacerSize,
                        (address * byteHeight) + (index * horizontalSpacerSize) + horizontalSpacerSize,
                        byteWidth,
                        byteHeight * size);

                    rectContainer = new ItemRectContainer();
                    rectContainer.Item = item;
                    rectContainer.ItemRect = rect;
                    rectContainer.Index = index;
                    rectContainer.Address = address;

                    rectList.Add(rect.Top, rectContainer);
                    address += (size - 1);
                }
                return rectList;
            }
        }

        private void CreateIoDescBitmap()
        {
            lock (syncLock)
            {
                if (itemRects == null || itemRects.Count == 0)
                    return;

                if (bmp != null)
                    bmp.Dispose();
                bmp = new Bitmap(this.Width, this.virtHeight);

                g = Graphics.FromImage(bmp);
                SolidBrush brush;
                Color color;
                for (int i = 0; i < this.itemRects.Count; i++)
                {
                    if (itemRects.Values[i].Item == null)
                        color = unusedByteColor;
                    else
                        color = usedByteColor;
                    if (this.selectedItemRects.Contains(itemRects.Values[i]))
                        color = ControlPaint.Dark(color);
                    brush = new SolidBrush(color);

                    Rectangle rect = itemRects.Values[i].ItemRect;

                    g.FillRectangle(brush, rect);
                    g.DrawString(itemRects.Values[i].Address.ToString(), this.Font, new SolidBrush(this.ForeColor), new Point(rect.X, rect.Y));
                }
                g.Flush();
                g.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            if (bmp == null || ioDesc == null)
                return;

            if (this.Height != this.lastHeight) {
                this.lastHeight = this.Height;
                if (this.virtHeight > this.Height) {
                    this.vScrollBar.Visible = true;
                    this.vScrollBar.Maximum = ((this.virtHeight - this.Height + byteHeight + 35) / scrollDivisor);
                }
                //CreateIoDescBitmap();
            }

            int yOffset = this.vScrollBar.Value * scrollDivisor;
            if (yOffset > virtHeight - this.Height)
                yOffset = virtHeight - this.Height;
            int width = (bmp.Width >= this.Width) ? this.Width : bmp.Width;
            int height = (bmp.Height >= this.Height) ? this.Height : bmp.Height;
            Bitmap tmpBitMap = bmp.Clone(new Rectangle(0, yOffset, width, height), System.Drawing.Imaging.PixelFormat.DontCare);
            
            e.Graphics.DrawImage(tmpBitMap, new Point(0, 0));
            tmpBitMap.Dispose();
        }
        #endregion
    }

    internal class ItemRectContainer
    {
        public Rectangle ItemRect { get; set; }
        public IoDescriptionItem Item { get; set; }
        public int Address { get; set; }
        public int Index { get; set; }
    }
}
