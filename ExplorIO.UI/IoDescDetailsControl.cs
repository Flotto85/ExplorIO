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
    public partial class IoDescDetailsControl : UserControl
    {
        #region Fields and Properties
        private IoDescription ioDesc;
        private Device[] availableDevices;

        public Device[] AvailableDevices
        {
            get { return availableDevices; }
            set {
                if (value == availableDevices)
                    return;
                availableDevices = value;
                this.cobOutputDevice.Items.Clear();
                this.cobInputDevice.Items.Clear();
                if (availableDevices != null)
                {
                    this.cobOutputDevice.Items.AddRange(availableDevices);
                    this.cobInputDevice.Items.AddRange(availableDevices);
                    this.cobInputDevice.SelectedIndex = 0;
                    this.cobOutputDevice.SelectedIndex = 0;
                }
                this.UpdateControl();
            }
        }

        public IoDescription IoDesc
        {
            get { return ioDesc; }
            set {
                if (ioDesc == value)
                    return;
                if (ioDesc != null)
                {
                    ioDesc.PropertyChanged -= new PropertyChangedEventHandler(OnIoDescPropertyChanged);
                }
                ioDesc = value;
                if (ioDesc != null)
                {
                    ioDesc.PropertyChanged += new PropertyChangedEventHandler(OnIoDescPropertyChanged);
                }
                this.UpdateControl();
            }
        }
        #endregion

        #region Initialization
        public IoDescDetailsControl()
        {
            InitializeComponent();
            this.cobInputDevice.SelectedIndexChanged += new EventHandler(ControlElementValueChanged);
            this.cobOutputDevice.SelectedIndexChanged += new EventHandler(ControlElementValueChanged);
            this.nudInputStartAddress.ValueChanged += new EventHandler(ControlElementValueChanged);
            this.nudOutputStartAddress.ValueChanged += new EventHandler(ControlElementValueChanged);
            this.nudIoSize.ValueChanged += new EventHandler(ControlElementValueChanged);
            this.UpdateControl();
        }
        #endregion

        #region Event Handler
        private void OnIoDescPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateControl();
        }

        private void ControlElementValueChanged(object sender, EventArgs e)
        {
            UpdateIoDescription();
        }
        #endregion

        #region Tools
        private void UpdateControl()
        {
            if (this.AvailableDevices == null || this.IoDesc == null)
            {
                this.Enabled = false;
            }
            else
            {
                this.cobOutputDevice.SelectedItem = this.IoDesc.OutputDevice;
                this.cobInputDevice.SelectedItem = this.IoDesc.InputDevice;
                this.nudInputStartAddress.Value = this.IoDesc.InputStartAddress;
                this.nudOutputStartAddress.Value = this.IoDesc.OutputStartAddress;
                this.nudIoSize.Value = this.ioDesc.Size;
                this.Enabled = true;
            }
        }
        #endregion

        #region Interface
        public void UpdateIoDescription()
        {
            if (ioDesc == null)
                return;
            if (this.cobOutputDevice.SelectedItem != this.IoDesc.OutputDevice)
                this.IoDesc.SetOutputDevice((Device)this.cobOutputDevice.SelectedItem);
            if (this.cobInputDevice.SelectedItem != this.IoDesc.InputDevice)
                this.IoDesc.SetInputDevice((Device)this.cobInputDevice.SelectedItem);
            if (this.nudInputStartAddress.Value != this.IoDesc.InputStartAddress)
                this.IoDesc.InputStartAddress = (uint)this.nudInputStartAddress.Value;
            if (this.nudOutputStartAddress.Value != this.IoDesc.OutputStartAddress)
                this.IoDesc.OutputStartAddress = (uint)this.nudOutputStartAddress.Value;
            if (this.nudIoSize.Value != this.ioDesc.Size)
                this.ioDesc.Size = (uint)this.nudIoSize.Value;
        }
        #endregion
    }
}
