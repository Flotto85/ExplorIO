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
    public partial class DevicesTreeControl : UserControl
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
                    if (this.project.Devices != null && this.project.Devices.Length > 0)
                    {
                        foreach (Device d in this.project.Devices)
                        {
                            d.PropertyChanged -= new PropertyChangedEventHandler(OnDevicePropertyChanged);
                            if (d.InputIoDescriptions != null && d.InputIoDescriptions.Length > 0)
                            {
                                foreach (IoDescription desc in d.InputIoDescriptions)
                                {
                                    desc.PropertyChanged -= new PropertyChangedEventHandler(OnIoDescPropertyChanged);
                                }
                            }
                            if (d.OutputIoDescriptions != null && d.OutputIoDescriptions.Length > 0)
                            {
                                foreach (IoDescription desc in d.OutputIoDescriptions)
                                {
                                    desc.PropertyChanged -= new PropertyChangedEventHandler(OnIoDescPropertyChanged);
                                }
                            }
                        }
                    }
                }
                project = value;
                if (project != null)
                {
                    this.project.PropertyChanged += new PropertyChangedEventHandler(OnProjectPropertyChanged);
                    if (this.project.Devices != null && this.project.Devices.Length > 0)
                    {
                        foreach (Device d in this.project.Devices)
                        {
                            d.PropertyChanged += new PropertyChangedEventHandler(OnDevicePropertyChanged);
                            if (d.InputIoDescriptions != null && d.InputIoDescriptions.Length > 0)
                            {
                                foreach (IoDescription desc in d.InputIoDescriptions)
                                {
                                    desc.PropertyChanged += new PropertyChangedEventHandler(OnIoDescPropertyChanged);
                                }
                            }
                            if (d.OutputIoDescriptions != null && d.OutputIoDescriptions.Length > 0)
                            {
                                foreach (IoDescription desc in d.OutputIoDescriptions)
                                {
                                    desc.PropertyChanged += new PropertyChangedEventHandler(OnIoDescPropertyChanged);
                                }
                            }
                        }
                    }
                }

                this.InitTree();
            }
        }
        #endregion

        #region Initialization
        public DevicesTreeControl()
        {
            InitializeComponent();
            this.treeViewDevices.AfterSelect += new TreeViewEventHandler(treeViewDevices_AfterSelect);
        }
        #endregion

        #region Events
        public event EventHandler<TreeViewEventArgs> SelectedNodeChanged;
        #endregion 

        #region Event Handler
        private void OnIoDescPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InitTree();
        }

        private void OnDevicePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InitTree();
        }

        private void OnProjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.InitTree();
        }

        private void treeViewDevices_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.SelectedNodeChanged != null)
                this.SelectedNodeChanged(this, e);
        }
        #endregion

        #region Interface
        public void InitTree()
        {
            this.treeViewDevices.Nodes.Clear();
            if(this.project == null)
                return;
            TreeNode mainNode = new TreeNode(project.ProjectNumber.ToString());
            mainNode.Tag = this.project;
            if (project.Devices != null && this.project.Devices.Length > 0)
            {
                foreach (Device dev in project.Devices)
                {
                    TreeNode deviceNode = new TreeNode(dev.ToString());
                    deviceNode.Tag = dev;
                    if (dev.OutputIoDescriptions != null && dev.OutputIoDescriptions.Length > 0)
                    {
                        foreach (IoDescription desc in dev.OutputIoDescriptions)
                        {
                            if (desc.InputDevice == null)
                                continue;
                            TreeNode descNode = new TreeNode("→ " + desc.InputDevice.ToString());
                            descNode.Tag = desc;
                            deviceNode.Nodes.Add(descNode);
                        }
                    }
                    if (dev.InputIoDescriptions != null && dev.InputIoDescriptions.Length > 0)
                    {
                        foreach (IoDescription desc in dev.InputIoDescriptions)
                        {
                            if (desc.OutputDevice == null)
                                continue;
                            TreeNode descNode = new TreeNode("← " + desc.OutputDevice.ToString());
                            descNode.Tag = desc;
                            deviceNode.Nodes.Add(descNode);
                        }
                    }
                    mainNode.Nodes.Add(deviceNode);
                }
            }
            mainNode.ExpandAll();
            this.treeViewDevices.Nodes.Add(mainNode);
        }
        #endregion 
    }
}
