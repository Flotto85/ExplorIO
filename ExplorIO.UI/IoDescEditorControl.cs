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
    public partial class IoDescEditorControl : UserControl
    {
        #region Fields and Properties
        private IoDescription ioDesc;
        private bool mouseIsDown;

        public IoDescription IoDesc
        {
            get { return ioDesc; }
            set {
                if (ioDesc == value)
                    return;              
                if (value == null)
                    return;
                ioDesc = value;
                List<IoByteControl2> controls = new List<IoByteControl2>();
                for (int i = 0; i < ioDesc.Size; i++)
                {
                    IoByteControl2 con = new IoByteControl2();
                    con.Margin = new System.Windows.Forms.Padding(0);
                    controls.Add(con);
                    con.MouseDown += new MouseEventHandler(con_MouseDown);
                    con.MouseUp += new MouseEventHandler(con_MouseUp);
                    con.MouseMove += new MouseEventHandler(con_MouseMove);
                    con.DragEnter += new DragEventHandler(con_DragEnter);
                    con.AllowDrop = true;
                }

                this.flowLayoutPanel.Controls.AddRange(controls.ToArray());
            }
        }

        void con_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetData(typeof(IoByteControl)) != null) {
                e.Effect = DragDropEffects.Move;
                IoByteControl byteCon = (IoByteControl)sender as IoByteControl;
                if (byteCon == null)
                    return;
                FlowLayoutPanel p = (FlowLayoutPanel)byteCon.Parent;
                if (p != this.flowLayoutPanel)
                    return;

                int myIndex = p.Controls.GetChildIndex(byteCon);

                IoByteControl q = (IoByteControl)e.Data.GetData(typeof(IoByteControl));

                p.Controls.SetChildIndex(q, myIndex);
            } 
        }

        void con_MouseMove(object sender, MouseEventArgs e) {
            if (mouseIsDown) {
                IoByteControl bitCon = (IoByteControl)sender as IoByteControl;

                bitCon.DoDragDrop(bitCon, DragDropEffects.All);
                mouseIsDown = false;
            }
        }

        void con_MouseUp(object sender, MouseEventArgs e) {
            this.mouseIsDown = false;
        }

        void con_MouseDown(object sender, MouseEventArgs e) {
            this.mouseIsDown = true;
        }

        #endregion

        #region Initialization
        public IoDescEditorControl()
        {
            InitializeComponent();
            this.mouseIsDown = false;
            this.flowLayoutPanel.Margin = new Padding(0);
            this.flowLayoutPanel.AllowDrop = true;
        }
        #endregion
    }
}
