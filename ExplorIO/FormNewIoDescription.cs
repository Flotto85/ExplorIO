using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ExplorIO.Data;

namespace ExplorIO
{
    public partial class FormNewIoDescription : Form
    {
        #region Fields and Properties
        public IoDescription IoDesc
        {
            get { return this.ioDescDetailsControl.IoDesc; }
        }

        public Device[] AvailableDevices
        {
            get {
                return this.ioDescDetailsControl.AvailableDevices; 
            }
            set {
                this.ioDescDetailsControl.AvailableDevices = value;
                if (value != null && (value.Length > 0) && this.ioDescDetailsControl.IoDesc == null)
                    this.ioDescDetailsControl.IoDesc = new IoDescription(value[0], value[0]);
            }
        }

        #endregion

        #region Initialization
        public FormNewIoDescription()
        {
            InitializeComponent();
            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }
        #endregion

        #region Event Handler
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ioDescDetailsControl.IoDesc != null)
                this.ioDescDetailsControl.UpdateIoDescription();
        }
        #endregion

        #region Interface
        public void ResetIoDesc()
        {
            this.ioDescDetailsControl.IoDesc = null;
        }
        #endregion
    }
}
