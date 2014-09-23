using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExplorIO.UI
{
    public partial class IoDataControl : UserControl {
        #region Fields and Properties
        Bitmap bmp;
        Graphics g;
        bool selected;
        #endregion

        public string Text {
            get;
            set;
        }

        public IoDataControl()
        {
            InitializeComponent();
            this.BackColor = SystemColors.ActiveCaption;
            this.MouseDown += new MouseEventHandler(IoDataControl_MouseDown);
            this.MouseUp += new MouseEventHandler(IoDataControl_MouseUp);
            this.DragDrop += new DragEventHandler(IoDataControl_DragDrop);
            this.MouseClick += new MouseEventHandler(IoDataControl_MouseClick);     
        }

        void IoDataControl_MouseClick(object sender, MouseEventArgs e)
        {
            //SetSelected();
        }

        void IoDataControl_DragDrop(object sender, DragEventArgs e)
        {
            //this.BackColor = SystemColors.ActiveCaption;
        }

        void IoDataControl_MouseUp(object sender, MouseEventArgs e)
        {
            this.SetUnselected();
        }

        void IoDataControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.SetSelected();
        }

        public void SetUnselected()
        {
            selected = false;
            this.Invalidate();
        }

        public void SetSelected()
        {
            selected = true;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            if (bmp == null){
                bmp = new Bitmap(this.Width, this.Height);
            }
            if (g == null) {
                g = Graphics.FromImage(bmp);
            }
            SolidBrush brush = new SolidBrush( selected?Color.Yellow:SystemColors.ActiveCaption);
            g.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
            g.DrawString(Text, this.Font, Brushes.Black, new PointF(10, 10));
            e.Graphics.DrawImage(bmp, new Point(0, 0));
        }
    }
}
