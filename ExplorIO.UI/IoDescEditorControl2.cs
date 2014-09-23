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

namespace ExplorIO.UI {
    public partial class IoDescEditorControl2 : UserControl {
        private int byteHeight = 70;
        private List<ItemRectContainer> itemRects;
        private IoDescription ioDesc;
        private Bitmap bmp;
        private Graphics g;
        private int virtHeight;
        private int scrollDivisor = 10;
        int lastHeight = 0;
        int selectedItemIndex;
        bool mouseDown;
        Point mouseDownPos;

        public IoDescription IoDesc {
            get { return ioDesc; }
            set {
                if (ioDesc == value)
                    return;
                if (value == null)
                    return;
                ioDesc = value;

                this.itemRects.Clear();
                for (int i = 0; i < ioDesc.Size; i++) {
                    Rectangle rect = new Rectangle(0, i * (byteHeight + 5), this.Width, byteHeight);
                    ItemRectContainer rectContainer = new ItemRectContainer();
                    rectContainer.ItemRect = rect;
                    rectContainer.Color = SystemColors.ActiveCaption;
                    this.itemRects.Add(rectContainer);
                }
                this.virtHeight = (int)((ioDesc.Size) * (byteHeight + 5));
                CreateIoDescBitmap();
                this.Invalidate();
            }
        }

        void vScrollBar_ValueChanged(object sender, EventArgs e) {
            this.Invalidate();
        }

        public IoDescEditorControl2() {
            InitializeComponent();

            this.itemRects = new List<ItemRectContainer>();
            this.DoubleBuffered = true;
            this.virtHeight = 1;

            this.selectedItemIndex = -1;
            bool mouseDown = false;

            this.MouseDown += new MouseEventHandler(IoDescEditor2_MouseDown);
            this.MouseUp += new MouseEventHandler(IoDescEditorControl2_MouseUp);
            this.MouseMove += new MouseEventHandler(IoDescEditorControl2_MouseMove);
            this.MouseLeave += new EventHandler(IoDescEditorControl2_MouseLeave);
            this.KeyPress += new KeyPressEventHandler(IoDescEditorControl2_KeyPress);
            this.vScrollBar.ValueChanged += new EventHandler(vScrollBar_ValueChanged);
        }

        void IoDescEditorControl2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.selectedItemIndex >= 0)
            {
                if (e.KeyChar == 's')
                {
                    this.selectedItemIndex++;
                }
                if (e.KeyChar == 'w')
                {
                    this.selectedItemIndex--;
                }
                if (this.selectedItemIndex < 0)
                    this.selectedItemIndex = 0;
                if (this.selectedItemIndex > this.itemRects.Count - 1)
                    this.selectedItemIndex = this.itemRects.Count - 1;
                this.CreateIoDescBitmap();
                this.Invalidate();
            }
        }

        void IoDescEditorControl2_MouseLeave(object sender, EventArgs e)
        {
            mouseDown = false;
        }

        void IoDescEditorControl2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                //CreateIoDescBitmap(e.X, e.Y);
                //this.Invalidate();
            }
        }

        void IoDescEditorControl2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            CreateIoDescBitmap();
            this.Invalidate();
        }

        void IoDescEditor2_MouseDown(object sender, MouseEventArgs e) {
            if (this.itemRects == null || this.itemRects.Count == 0) {
                return;
            }
            int index = ((e.Y + vScrollBar.Value * scrollDivisor) / (byteHeight + 5));
            if (index > itemRects.Count-1){
                selectedItemIndex = -1;
                return;            
            }
            mouseDown = true;
            this.selectedItemIndex = index;
            this.mouseDownPos = new Point(e.X, e.Y);
            CreateIoDescBitmap();
            this.Invalidate();
        }

        private void CreateIoDescBitmap(int mouseX, int mouseY)
        {
            if (itemRects == null || itemRects.Count == 0)
                return;

            if (bmp != null)
                bmp.Dispose();
            bmp = new Bitmap(this.Width, this.virtHeight);

            g = Graphics.FromImage(bmp);
            SolidBrush brush = new SolidBrush(SystemColors.ActiveCaption);
            for (int i = 0; i < this.itemRects.Count; i++) {
                if (i==selectedItemIndex)
                    brush = new SolidBrush(SystemColors.ControlDark);
                else
                    brush = new SolidBrush(itemRects[i].Color);
                Rectangle rect = itemRects[i].ItemRect;
                if (i == this.selectedItemIndex && mouseDown)
                {
                    //rect.X += mouseX - this.mouseDownPos.X;
                    //rect.Y -= this.mouseDownPos.Y - mouseY;
                }
                g.FillRectangle(brush, rect);
                g.DrawString(i.ToString(), this.Font, Brushes.Black, new Point(rect.X, rect.Y));
            }
            g.Flush();
            g.Dispose();
        }

        private void CreateIoDescBitmap() {
            CreateIoDescBitmap(0, 0);
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
                CreateIoDescBitmap();
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
    }

    public class ItemRectContainer
    {
        public Rectangle ItemRect { get; set; }
        public Color Color { get; set; }
    }

}
