using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExplorIO.Data;

namespace ExplorIO.UI
{
    public partial class IoByteControl : UserControl {
        #region Fields and Properties

        private List<IoDataControl> bitControls;
        private bool mouseIsDown;
        private IoByte ioByte;

        public IoByte IoByte {
            get { return ioByte; }
            set {
                if (ioByte != null) {

                }
                ioByte = value;
                if (ioByte != null) {
                    foreach (IoBit bit in ioByte.Bits) {

                    }
                }
            }
        }

        #endregion

        public IoByteControl()
        {
            InitializeComponent();
            this.bitControls = new List<IoDataControl>();

            for (int i = 0; i < 8;i++) {
                IoDataControl bitCon = new IoDataControl();
                bitCon.AllowDrop = true;
                bitCon.MouseDown += new MouseEventHandler(OnBitCon_MouseDown);
                bitCon.DragEnter += new DragEventHandler(OnBitCon_DragEnter);
                bitCon.MouseUp += new MouseEventHandler(bitCon_MouseUp);
                bitCon.MouseMove += new MouseEventHandler(bitCon_MouseMove);
                bitCon.Text = "Bit " + i;
                bitCon.Margin = new Padding(1);
                this.bitControls.Add(bitCon);
            }

            this.MouseDown += new MouseEventHandler(OnBitCon_MouseDown);
            this.MouseUp += new MouseEventHandler(bitCon_MouseUp);
            this.flowLayoutPanelBits.Controls.AddRange(this.bitControls.ToArray());
            this.mouseIsDown = false;
        }

        void bitCon_MouseMove(object sender, MouseEventArgs e) {
            if (this.mouseIsDown) {
                IoDataControl bitCon = (IoDataControl)sender as IoDataControl;

                bitCon.DoDragDrop(bitCon, DragDropEffects.All);
                mouseIsDown = false;
            }
        }

        void bitCon_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseIsDown = false;
            this.BackColor = SystemColors.ControlDark;
        }

        void OnBitCon_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(IoDataControl)) != null)
            {
                e.Effect = DragDropEffects.Move;
                IoDataControl bitCon = (IoDataControl)sender as IoDataControl;
                if (bitCon == null)
                    return;
                FlowLayoutPanel p = (FlowLayoutPanel)bitCon.Parent;
                if (p != this.flowLayoutPanelBits)
                    return;

                int myIndex = p.Controls.GetChildIndex(bitCon);

                IoDataControl q = (IoDataControl)e.Data.GetData(typeof(IoDataControl));
                FlowLayoutPanel p2 = (FlowLayoutPanel)q.Parent;

                if (p2 != this.flowLayoutPanelBits)
                    return;

                p.Controls.SetChildIndex(q, myIndex);
            } 
        }

        void OnBitCon_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackColor = SystemColors.ActiveCaption;
            this.mouseIsDown = true;
        }
    }
}
