using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ExplorIO.Data
{
    public enum DeviceType { S7, KRC4, Other }

    [DataContract(IsReference = true)]
    public class Device : INotifyPropertyChanged
    {
        #region Fields and Properties
        private string name;
        private DeviceType deviceType;
        private List<IoDescription> inputIoDescriptions;
        private List<IoDescription> outputIoDescriptions;

        [DataMember]
        public string Name
        {
            get { return name; }
            set 
            {
                if (name != value)
                {
                    name = value;
                    this.PerformPropertyChanged("Name");
                }                
            }
        }

        [DataMember]
        public DeviceType DeviceType
        {
            get { return deviceType; }
            set {
                if (deviceType != value)
                {
                    deviceType = value;
                    this.PerformPropertyChanged("DeviceType");
                }                
            }
        }

        [DataMember]
        public IoDescription[] InputIoDescriptions
        {
            get {
                return inputIoDescriptions.ToArray(); 
            }
            protected set { this.inputIoDescriptions = new List<IoDescription>(value); }
        }

        [DataMember]
        public IoDescription[] OutputIoDescriptions
        {
            get {
                return outputIoDescriptions.ToArray(); 
            }
            protected set { this.outputIoDescriptions = new List<IoDescription>(value); }
        }
        #endregion

        #region Initialization
        public Device()
        {
            this.inputIoDescriptions = new List<IoDescription>();
            this.outputIoDescriptions = new List<IoDescription>();
        }

        public Device(string name)
            :this()
        {
            this.name = name;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Tools
        private void PerformPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion

        #region Interface
        public override string ToString()
        {
            return name + " (" + deviceType.ToString() + ")";
        }

        public void AddInputIoDescription(IoDescription desc)
        {
            try
            {
                if (desc == null || this.inputIoDescriptions.Contains(desc))
                    return;

                this.inputIoDescriptions.Add(desc);
                this.PerformPropertyChanged("InputIoDescription");
            }
            catch (Exception exp)
            {       
                throw;
            }

        }

        public void RemoveInputIoDescription(IoDescription desc)
        {
            if (desc == null || !this.inputIoDescriptions.Contains(desc))
                return;

            this.inputIoDescriptions.Remove(desc);
            this.PerformPropertyChanged("InputIoDescription");
        }

        public void AddOutputIoDescription(IoDescription desc)
        {
            if (desc == null || this.outputIoDescriptions.Contains(desc))
                return;

            this.outputIoDescriptions.Add(desc);
            this.PerformPropertyChanged("OutputIoDescription");
        }

        public void RemoveOutputIoDescription(IoDescription desc)
        {
            if (desc == null || !this.outputIoDescriptions.Contains(desc))
                return;

            this.outputIoDescriptions.Remove(desc);
            this.PerformPropertyChanged("OutputIoDescription");
        }

        internal void ReInitIoDescriptions()
        {
            if (this.inputIoDescriptions != null && this.inputIoDescriptions.Count > 0)
            {
                List<IoDescription> tmpDescs = new List<IoDescription>();
                while (this.inputIoDescriptions.Count > 0)
                {
                    tmpDescs.Add(this.inputIoDescriptions[0]);
                    this.RemoveInputIoDescription(this.inputIoDescriptions[0]);
                }
                foreach (IoDescription desc in tmpDescs)
                {
                    desc.SetInputDevice(this);
                }
            }

            if (this.outputIoDescriptions != null && this.outputIoDescriptions.Count > 0)
            {
                List<IoDescription> tmpDescs = new List<IoDescription>();
                while (this.outputIoDescriptions.Count > 0)
                {
                    tmpDescs.Add(this.outputIoDescriptions[0]);
                    this.RemoveOutputIoDescription(this.outputIoDescriptions[0]);
                }
                foreach (IoDescription desc in tmpDescs)
                {
                    desc.SetOutputDevice(this);
                }
            }
        }
        #endregion
    }
}
