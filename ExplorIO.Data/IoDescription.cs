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

        private SortedList<int, IoDescriptionItem> items;

        [DataMember]
        public IoDescriptionItem[] Items
        {
            get { return items.Values.ToArray(); }
            protected set { 
                this.items = new SortedList<int, IoDescriptionItem>(); 
                foreach (IoDescriptionItem item in value){
                    this.items.Add(item.Address, item);
                }
            }
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
            this.items = new SortedList<int, IoDescriptionItem>();
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

        public void AddItem(IoDescriptionItem item)
        {
            if (item.Address < 0 || item.Address > (this.size - 1))
                throw new ArgumentOutOfRangeException("address");
            if (item == null)
                throw new ArgumentNullException("item");
            int start = item.Address;
            int end = start + item.Size;

            this.items.Add(item.Address, item);
            this.PerformPropertyChanged("Items");
        }

        public void RemoveItem(IoDescriptionItem item)
        {
            if (this.items.ContainsKey(item.Address))
            {
                this.items.Remove(item.Address);
                this.PerformPropertyChanged("Items");
            }
        }

        public void MoveItem(IoDescriptionItem item, int newAddress)
        {
            if (!items.ContainsKey(item.Address))
                throw new ArgumentException("The item is not part of the description", "item");
            if (newAddress < 0)
                throw new ArgumentException("Address can not be smaller than 0", "address");
            if (newAddress + item.Size > this.Size)            
                throw new ArgumentException("Address plus item size is larger than description size", "address");
            if (item.Address == newAddress)
                return;

            IoDescriptionItem nextItem = items.FirstOrDefault(x => x.Key >= newAddress && x.Key != item.Address).Value;
            if (items.ContainsKey(newAddress) || (nextItem != null && nextItem.Address <= (newAddress + (item.Size - 1))))
            {
                throw new InvalidOperationException("Destination of item is not empty");
            }
            IoDescriptionItem itemBefore = items.LastOrDefault(x => x.Key <= newAddress && x.Key != item.Address).Value;
            if ((itemBefore != null && (itemBefore.Address + (itemBefore.Size - 1) >= newAddress)))
            {
                throw new InvalidOperationException("Destination of item is not empty");
            }

            items.Remove(item.Address);
            item.Address = newAddress;
            items.Add(item.Address, item);

            this.PerformPropertyChanged("Items");
        }

        public IoDescriptionItem GetItemAtAddress(int address)
        {
            IoDescriptionItem item = null;
            for (int i = address; i >= 0; i--)
            {
                if (this.items.ContainsKey(i))
                {
                    if (address == this.items[i].Address ||
                        address < (this.items[i].Address + this.items[i].Size))
                    {
                        item = items[i];
                    }
                    break;
                }
            }
            return item;
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
