using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExplorIO.UI {
    public partial class IoByteControl2 : UserControl {
        List<Rectangle> bitBoxes;
        int height = 90;
        int width = 90;
        int pixelsBetween=5;
        Bitmap bmp;
        Graphics g;

        public string Text { get; set; }

        public IoByteControl2() {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.bitBoxes = new List<Rectangle>();
            for (int i = 0; i < 8; i++) {
                this.bitBoxes.Add(new Rectangle(i * (width + pixelsBetween), pixelsBetween, width, height));
            }
            this.Width = 8 * (width + pixelsBetween);
            this.Height = height;

            bmp = new Bitmap(this.Width, this.Height);
            g = Graphics.FromImage(bmp);
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            SolidBrush brush = new SolidBrush(SystemColors.ActiveCaption);

            for (int i = 0; i < 8; i++) {              

                g.FillRectangle(brush, bitBoxes[i]);
                g.DrawString(i.ToString(), this.Font, Brushes.Black, new Point(this.bitBoxes[i].X, this.bitBoxes[i].Y));
                
            }
            e.Graphics.DrawImage(bmp, new Point(0, 0));   
            g.Clear(Color.White);
        }
    }
}
