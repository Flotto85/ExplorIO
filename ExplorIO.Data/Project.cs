using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace ExplorIO.Data
{
    [DataContract]
    public class Project : INotifyPropertyChanged
    {
        #region Fields and Properties
        private uint projNo;
        private string custName;
        private string projName;
        private string projDesc;

        private List<Device> devices;

        [DataMember]
        public uint ProjectNumber {
            get { return projNo; }
            set
            {
                if (projNo != value)
                {
                    projNo = value;
                    this.PerformPropertyChanged("ProjectNumber");
                }
            }
        }

        [DataMember]
        public string CustomerName {
            get { return custName; }
            set
            {
                if (custName != value)
                {
                    custName = value;
                    this.PerformPropertyChanged("CustomerName");
                }
            }
        }

        [DataMember]
        public string ProjectName
        {
            get { return this.projName; }
            set
            {
                if (projName != value)
                {
                    this.projName = value;
                    this.PerformPropertyChanged("ProjectName");
                }
            }
        }

        [DataMember]
        public string ProjectDescription
        {
            get { return this.projDesc; }
            set
            {
                if (projDesc != value)
                {
                    this.projDesc = value;
                    this.PerformPropertyChanged("ProjectDescription");
                }
            }
        }

        [DataMember]
        public Device[] Devices
        {
            get { return devices.ToArray(); }
            protected set { this.devices = new List<Device>(value); }
        }
        #endregion

        #region Initialization
        public Project()
        {
            this.devices = new List<Device>();
        }

        public Project(uint projectNo)
            : this()
        {
            this.ProjectNumber = projectNo;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Interface
        public void AddDevice(Device device)
        {
            if (this.devices == null)
                this.devices = new List<Device>();
            if (device == null)
                return;
            this.devices.Add(device);
            this.PerformPropertyChanged("Devices");
        }

        public void RemoveDevice(Device device)
        {
            if (this.devices == null)
                this.devices = new List<Device>();
            if (device != null && this.devices.Contains(device))
            {
                while (device.InputIoDescriptions.Length != 0)
                {
                    device.InputIoDescriptions[0].RemoveFromDevices();
                }
                while (device.OutputIoDescriptions.Length != 0)
                {
                    device.OutputIoDescriptions[0].RemoveFromDevices();
                }
                this.devices.Remove(device);
                this.PerformPropertyChanged("Devices");
            }
        }
        #endregion

        #region Static Interface
        public static Project FromFile(string filePath)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Project));

            XmlReader reader = XmlReader.Create(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));

            Project seriObj = (Project)serializer.ReadObject(reader);

            reader.Close();

            return seriObj;
        }

        public static void ToFile(string filePath, Project project)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Project));

            XmlWriter writer = XmlWriter.Create(filePath);

            serializer.WriteObject(writer, (object)project);
            writer.Close();
        }
        #endregion

        #region Tools
        private void PerformPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion
    }
}
