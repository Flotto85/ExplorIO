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
    public partial class FormManageDevices : Form
    {
        #region Fields and Properties
        private Project project;

        public Project Project
        {
            get { return project; }
            set {
                if (project != null)
                {
                    this.project.PropertyChanged -= new PropertyChangedEventHandler(OnProjectPropertyChanged);
                }
                project = value;
                if (project != null)
                {
                    this.project.PropertyChanged += new PropertyChangedEventHandler(OnProjectPropertyChanged);
                    this.lbDevices.Items.AddRange(project.Devices);
                    if (lbDevices.Items.Count > 0)
                        this.lbDevices.SelectedIndex = 0;
                    this.Enabled = true;
                }
            }
        }
        #endregion

        #region Initialization
        public FormManageDevices()
        {
            InitializeComponent();
            this.deviceDetailsControl.Device = new Device();
            this.btnAddDevice.Click += new EventHandler(OnBtnAddDeviceClick);
            this.btnRemoveDevice.Click += new EventHandler(OnBtnRemoveDeviceClick);
            this.lbDevices.SelectedIndexChanged += new EventHandler(lbDevices_SelectedIndexChanged);
            this.Enabled = false;
            this.btnRemoveDevice.Enabled = false;
        }
        #endregion

        #region Tools
        private void UpdateDeviceList()
        {
            this.lbDevices.Items.Clear();
            foreach (Device dev in project.Devices)
            {
                this.lbDevices.Items.Add(dev);
            }
            if (lbDevices.Items.Count > 0)
                this.lbDevices.SelectedIndex = 0;
        }
        #endregion

        #region Event Handler
        private void OnProjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateDeviceList();
        }

        private void lbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbDevices.Items.Count >= lbDevices.SelectedIndex
                && this.lbDevices.Items[lbDevices.SelectedIndex] != null)
            {
                this.btnRemoveDevice.Enabled = true;
            }
            else
            {
                this.btnRemoveDevice.Enabled = false;
            }
        }

        private void OnBtnRemoveDeviceClick(object sender, EventArgs e)
        {
            if (this.lbDevices.Items.Count >= lbDevices.SelectedIndex)
            {
                if (this.deviceDetailsControl.Device != null)
                    this.project.RemoveDevice((Device)this.lbDevices.Items[lbDevices.SelectedIndex]);
            }
        }

        private void OnBtnAddDeviceClick(object sender, EventArgs e)
        {
            if (this.deviceDetailsControl.ValidateData())
            {
                this.deviceDetailsControl.UpdateDevice();
                Device d = (Device)this.deviceDetailsControl.Device;
                this.project.AddDevice(d);
                this.deviceDetailsControl.Device = new Device();
            }
        }
        #endregion
    }
}
