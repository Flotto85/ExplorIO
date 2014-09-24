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
                if (ioDesc != null)
                    ioDesc.PropertyChanged -= new PropertyChangedEventHandler(ioDesc_PropertyChanged);

                ioDesc = value;

                if (ioDesc != null)
                    ioDesc.PropertyChanged += new PropertyChangedEventHandler(ioDesc_PropertyChanged);

                this.virtHeight = (int)((ioDesc.Size + 1) * (byteHeight));
                this.vScrollBar.Maximum = ((this.virtHeight - this.Height + byteHeight + 35) / scrollDivisor);
                this.RePaintControl(true);
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
            this.MouseWheel += new MouseEventHandler(IoDescEditorControl2_MouseWheel);

            this.KeyDown += new KeyEventHandler(IoDescEditorControl2_KeyDown);
            this.KeyUp += new KeyEventHandler(IoDescEditorControl2_KeyUp);

            this.LostFocus += new EventHandler(IoDescEditorControl2_LostFocus);
            this.vScrollBar.ValueChanged += new EventHandler(vScrollBar_ValueChanged);
        }
        #endregion

        #region Event Handler
        private void ioDesc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Items")
                this.RePaintControl(true);
        }

        private void IoDescEditorControl2_MouseWheel(object sender, MouseEventArgs e)
        {
            int scrollBarValue = this.vScrollBar.Value;
            int newScrollValue = -e.Delta / scrollDivisor + scrollBarValue;
            if (newScrollValue < this.vScrollBar.Minimum)
                newScrollValue = this.vScrollBar.Minimum;
            else if (newScrollValue > (this.vScrollBar.Maximum + 1 - this.vScrollBar.LargeChange))
                newScrollValue = (this.vScrollBar.Maximum + 1 - this.vScrollBar.LargeChange);
            this.vScrollBar.Value = newScrollValue;
        }

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
                case 37:
                case 38:
                case 39:
                case 40:
                    e.Handled = true;
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
            this.Focus();

            ItemRectContainer clickedRect = FindItemRectAtControlPos(new Point(e.X, e.Y));
            if (clickedRect == null)
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

            this.selectedItemRects.Sort((cont1, cont2) => cont1.Index.CompareTo(cont2.Index));
            CreateIoDescBitmap();
            this.Invalidate();
        }

        private void IoDescEditorControl2_MouseClick(object sender, MouseEventArgs e)
        {

        }
        #endregion

        #region Tools
        private ItemRectContainer FindItemRectAtControlPos(Point pos)
        {
            if (this.itemRects.Count == 0)
                return null;
            int worldMousePos = (pos.Y + vScrollBar.Value * scrollDivisor);
            ItemRectContainer rect = null;
            for (int i = worldMousePos; i >= 0; i--)
            {
                if (itemRects.ContainsKey(i))
                {
                    if (itemRects[i].ItemRect.Bottom > worldMousePos &&
                        pos.X >= itemRects[i].ItemRect.Left &&
                        pos.X <= itemRects[i].ItemRect.Right)
                        rect = itemRects[i];
                    break;
                }
            }
            return rect;
        }

        private void RePaintControl(bool reCreateItemRects)
        {
            if (reCreateItemRects)
            {
                this.itemRects = this.CreateItemRectangles(this.ioDesc);
            }
            this.CreateIoDescBitmap();
            this.Invalidate();
            this.Update();
        }

        private void AddSelectedToIoDesc()
        {
            if (this.selectedItemRects.Count == 0)
                return;
            List<ItemRectContainer> rectsToAdd = new List<ItemRectContainer>();
            int i = 0;
            foreach (ItemRectContainer itemRect in this.selectedItemRects)
            {
                if (itemRect.Item != null)
                    return;
                if (i > 0)
                {
                    if (itemRect.Index != selectedItemRects[i - 1].Index + 1)
                    {
                        break;
                    }
                }
                rectsToAdd.Add(itemRect);
                i++;
            }
            if (rectsToAdd.Count == 0)
                return;

            IoDescriptionItem item = new IoGroup(rectsToAdd.Count, rectsToAdd[0].Address);
            this.ioDesc.AddItem(item);
        }

        private void RemoveSelectedFromIoDec()
        {
            if (this.selectedItemRects.Count == 0)
                return;
            List<ItemRectContainer> itemsToRemove = new List<ItemRectContainer>(this.selectedItemRects);
            foreach (ItemRectContainer item in itemsToRemove)
            {
                if (item.Item != null)
                {
                    this.ioDesc.RemoveItem(item.Item);
                }
            }
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
                        (address * byteHeight) + (index) /*+ horizontalSpacerSize*/,
                        byteWidth,
                        (byteHeight * size));

                    rectContainer = new ItemRectContainer();
                    rectContainer.Item = item;
                    rectContainer.ItemRect = rect;
                    rectContainer.Index = index;
                    rectContainer.Address = address;

                    rectList.Add(rect.Top, rectContainer);
                    address += (size - 1);
                }
                ItemRectContainer sel = null;

                if (selectedItemRects.Count == 1)
                {
                    if (selectedItemRects[0].Item != null)
                        sel = rectList.Values.FirstOrDefault(x => x.Item == selectedItemRects[0].Item);
                    else
                        sel = rectList.Values.FirstOrDefault(x => x.Address == selectedItemRects[0].Address);
                }

                this.selectedItemRects.Clear();

                if (sel != null)
                    this.selectedItemRects.Add(sel);

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
                Brush foreColorBrush = new SolidBrush(this.ForeColor);
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
                    g.DrawLine(new Pen(foreColorBrush), new Point(rect.Left, rect.Bottom - 1), new Point(rect.Right - 1, rect.Bottom - 1));
                    g.DrawString(itemRects.Values[i].Address.ToString(), this.Font, foreColorBrush, new Point(rect.X, rect.Y));
                }
                g.DrawLine(new Pen(foreColorBrush), new Point(itemRects.Values[0].ItemRect.Left, itemRects.Values[0].ItemRect.Top),
                    new Point(itemRects.Values[itemRects.Count - 1].ItemRect.Left, itemRects.Values[itemRects.Count - 1].ItemRect.Bottom));
                g.DrawLine(new Pen(foreColorBrush), new Point(itemRects.Values[0].ItemRect.Right, itemRects.Values[0].ItemRect.Top),
                    new Point(itemRects.Values[itemRects.Count - 1].ItemRect.Right, itemRects.Values[itemRects.Count - 1].ItemRect.Bottom));

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
                if (this.virtHeight > this.Height)
                {
                    this.vScrollBar.Visible = true;
                }
                else
                    this.vScrollBar.Visible = false;
                //CreateIoDescBitmap();
            }

            int yOffset = this.vScrollBar.Value * scrollDivisor;
            if (yOffset > virtHeight - this.Height)
                yOffset = virtHeight - this.Height;
            if (yOffset < 0)
                yOffset = 0;
            int width = (bmp.Width >= this.Width) ? this.Width : bmp.Width;
            int height = (bmp.Height >= this.Height) ? this.Height : bmp.Height;
            Bitmap tmpBitMap = bmp.Clone(new Rectangle(0, yOffset, width, height), System.Drawing.Imaging.PixelFormat.DontCare);
            
            e.Graphics.DrawImage(tmpBitMap, new Point(0, 0));
            tmpBitMap.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {

                return true; //for the active control to see the keypress, return false
            }
            else if (keyData == Keys.Right)
            {
                return true; //for the active control to see the keypress, return false
            }
            else if (keyData == Keys.Up)
            {
                if (this.selectedItemRects.Count == 1 && ioDesc != null)
                {
                    try
                    {
                        ioDesc.MoveItem(selectedItemRects[0].Item, selectedItemRects[0].Item.Address - 1);
                    }
                    catch { }
                   
                }
                return true; //for the active control to see the keypress, return false
            }
            else if (keyData == Keys.Down)
            {
                if (this.selectedItemRects.Count == 1 && ioDesc != null)
                {
                    try
                    {
                        ioDesc.MoveItem(selectedItemRects[0].Item, selectedItemRects[0].Item.Address + selectedItemRects[0].Item.Size);
                    }
                    catch { }
                }
                return true; //for the active control to see the keypress, return false
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
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
