using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ExplorIO.Data
{
    [DataContract]
    internal class SerializationObject
    {
        [DataMember]
        protected Project Project;
        [DataMember]
        protected List<IoDescription> Descriptions;
        [DataMember]
        protected List<Device> Devices;

        public SerializationObject()
        {
            this.Descriptions = new List<IoDescription>();
            this.Devices = new List<Device>();
        }

        public static SerializationObject FromProject(Project project)
        {
            SerializationObject seri = new SerializationObject();
            seri.Project = project;
            seri.Devices.AddRange(project.Devices);

            if (project.Devices != null && project.Devices.Length > 0)
            {
                foreach (Device dev in project.Devices)
                {
                    if (dev.InputIoDescriptions != null && dev.InputIoDescriptions.Length > 0)
                    {
                        foreach (IoDescription desc in dev.InputIoDescriptions)
                        {
                            if (!seri.Descriptions.Contains(desc))
                                seri.Descriptions.Add(desc);
                        }

                    }
                    if (dev.OutputIoDescriptions != null && dev.OutputIoDescriptions.Length > 0)
                    {
                        foreach (IoDescription desc in dev.OutputIoDescriptions)
                        {
                            if (!seri.Descriptions.Contains(desc))
                                seri.Descriptions.Add(desc);
                        }
                    }
                }
            }

            return seri;
        }

        public static Project ToProject(SerializationObject serializationObject)
        {
            Project pro = serializationObject.Project;
            foreach (Device dev in serializationObject.Devices)
            {
                pro.AddDevice(dev);
            }
            foreach (IoDescription desc in serializationObject.Descriptions)
            {
                desc.ReInitDevices();
            }
            return pro;
        }
    }
}
