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
    public partial class DeviceDetailsControl : UserControl
    {
        #region Fields and Properties
        private Device device;

        public Device Device
        {
            get { return device; }
            set {
                if (device != null)
                {
                    device.PropertyChanged -= new PropertyChangedEventHandler(OnDevicePropertyChanged);
                }
                device = value;
                if (device != null)
                {
                    device.PropertyChanged += new PropertyChangedEventHandler(OnDevicePropertyChanged);                   
                }
                this.UpdateControl();
            }
        }
        #endregion

        #region Initialization
        public DeviceDetailsControl()
        {
            InitializeComponent();
            this.cobDeviceType.DataSource = Enum.GetValues(typeof(DeviceType));

            this.tbName.Leave += new EventHandler(OnTbNameLeave);
            this.cobDeviceType.SelectedIndexChanged += new EventHandler(OnCobDeviceTypeSelectedIndexChanged);

            this.cobDeviceType.SelectedIndex = 0;
            this.UpdateControl();
        }
        #endregion

        #region Interface
        public bool ValidateData()
        {
            bool result = true;
            result &= this.tbName.Text != string.Empty;
            if (!result)
                errorProvider.SetError(this.tbName, "Bitte geben Sie einen Namen für das Gerät ein");
            else
                errorProvider.Clear();
            return result;
        }

        public void UpdateDevice()
        {
            if (this.device.DeviceType != (DeviceType)this.cobDeviceType.SelectedItem)
                this.device.DeviceType = (DeviceType)this.cobDeviceType.SelectedItem;
            if (this.device.Name != this.tbName.Text)
                this.device.Name = this.tbName.Text;
        }
        #endregion

        #region Event Handler
        private void OnDevicePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateControl();
        }

        private void OnCobDeviceTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateDevice();
        }

        private void OnTbNameLeave(object sender, EventArgs e)
        {
            this.UpdateDevice();
        }
        #endregion

        #region Tools
        private void UpdateControl()
        {
            if (this.device == null)
            {
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
                if (this.tbName.Text != this.device.Name)
                {
                    this.tbName.Text = this.device.Name;
                }
                if ((DeviceType)this.cobDeviceType.SelectedItem == this.device.DeviceType)
                {
                    this.cobDeviceType.SelectedItem = this.device.DeviceType;
                }
            }
        }
        #endregion 
    }
}
