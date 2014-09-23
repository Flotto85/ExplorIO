using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ExplorIO.Data
{
   [DataContract(IsReference=true)]
    public class IoDescription : INotifyPropertyChanged
    {
        #region Fields and Properties
        private Device inputDevice;
        private Device outputDevice;
        private uint inputStartAddress;
        private uint outputStartAddress;
        private uint size;

        private List<IoDescriptionItem> items;

        [DataMember]
        public IoDescriptionItem[] Items
        {
            get { return items.ToArray(); }
            protected set { this.items = new List<IoDescriptionItem>(value); }
        }

        [DataMember]
        public Device InputDevice
        {
            get { return inputDevice; }
            protected set { this.inputDevice = value; }
        }

        [DataMember]
        public Device OutputDevice
        {
            get { return outputDevice; }
            protected set { this.outputDevice = value; }
        }

        [DataMember]
        public uint InputStartAddress 
        {
            get { return this.inputStartAddress; }
            set
            {
                if (this.inputStartAddress == value)
                    return;
                this.inputStartAddress = value;
                this.PerformPropertyChanged("InputStartAddress");
            }
        }

        [DataMember]
        public uint OutputStartAddress
        {
            get { return this.outputStartAddress; }
            set
            {
                if (this.outputStartAddress == value)
                    return;
                this.outputStartAddress = value;
                this.PerformPropertyChanged("OutputStartAddress");
            }
        }

        [DataMember]
        public uint Size
        {
            get { return this.size; }
            set
            {
                if (this.size == value)
                    return;
                this.size = value;
                this.PerformPropertyChanged("Size");
            }
        }
        #endregion

        #region Initialization
        public IoDescription(Device inputDevice, Device outputDevice) : this()
        {
            if (inputDevice == null || outputDevice == null)
                throw new ArgumentNullException();
            this.SetInputDevice(inputDevice);
            this.SetOutputDevice(outputDevice);
        }

        protected IoDescription()
        {
            this.items = new List<IoDescriptionItem>();
        }
        #endregion

        #region Interface
        public void RemoveFromDevices()
        {
            this.outputDevice.RemoveOutputIoDescription(this);
            this.inputDevice.RemoveInputIoDescription(this);
        }

        public void SetInputDevice(Device inDevice)
        {
            if (inDevice == null || inDevice == inputDevice)
                return;
            if (inputDevice != null)
                this.inputDevice.RemoveInputIoDescription(this);
            inputDevice = inDevice;
            this.inputDevice.AddInputIoDescription(this);
            this.PerformPropertyChanged("InputDevice");
        }

        public void SetOutputDevice(Device outDevice)
        {
            if (outDevice == null || outDevice == outputDevice)
                return;
            if (outputDevice != null)
                this.outputDevice.RemoveOutputIoDescription(this);
            outputDevice = outDevice;
            this.outputDevice.AddOutputIoDescription(this);
            this.PerformPropertyChanged("OutputDevice");
        }

        public void AddItem(int address, IoDescriptionItem item)
        {
            if (address < 0 || address > (this.size - 1))
                throw new ArgumentOutOfRangeException("address");
            if (item == null)
                throw new ArgumentNullException("item");
            int start = address;
            int end = start + item.Size;
            //foreach (int itemAddress in this.items.Keys)
            //{
            //    int from = itemAddress;
            //    int to = this.items[itemAddress].Size + from;

            //    if ((start > from && start < to) || (end > from && end < to) || (start < from && end > to))
            //        throw new InvalidOperationException("Address areas intersect");
            // }

            this.items.Add(item);
        }

        internal void ReInitDevices()
        {
            Device tmpDev = inputDevice;
            this.inputDevice = null;
            this.InputDevice = tmpDev;
            tmpDev = outputDevice;
            this.outputDevice = null;
            this.OutputDevice = tmpDev;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Tools
        private void PerformPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
