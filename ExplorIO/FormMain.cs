using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ExplorIO.Data;
using ExplorIO.UI.Presenters;

namespace ExplorIO
{
    public partial class FormMain : Form
    {
        #region Fields and Properties
        FormNewProject frmNewProj;
        FormManageDevices frmDevices;
        FormNewIoDescription frmNewIoDesc;
        Project openProject;
        InterfaceDescriptionEditorPresenter descriptionEditorPresenter;

        public Project OpenedProject
        {
            get { return openProject; }
            set
            {
                openProject = value;
                if (openProject != null)
                {
                    this.projektSpeichernToolStripMenuItem.Enabled = true;
                    this.projektToolStripMenuItem.Enabled = true;
                }
                else
                {
                    this.projektSpeichernToolStripMenuItem.Enabled = false;
                    this.projektToolStripMenuItem.Enabled = false;
                }
            }
        }
        #endregion

        #region Initialization
        public FormMain()
        {
            InitializeComponent();
            this.projektSpeichernToolStripMenuItem.Enabled = false;
            this.projektToolStripMenuItem.Enabled = false;

            this.descriptionEditorPresenter = new InterfaceDescriptionEditorPresenter(this.interfaceDescriptionEditorView1);

            this.neuesProjektToolStripMenuItem.Click += new EventHandler(neuesProjektToolStripMenuItem_Click);
            this.projektLadenToolStripMenuItem.Click += new EventHandler(projektLadenToolStripMenuItem_Click);
            this.projektSpeichernToolStripMenuItem.Click += new EventHandler(projektSpeichernToolStripMenuItem_Click);
            this.beendenToolStripMenuItem.Click += new EventHandler(beendenToolStripMenuItem_Click);
            this.geräteToolStripMenuItem.Click += new EventHandler(geräteToolStripMenuItem_Click);
            this.schnittstelleHinzufügenToolStripMenuItem.Click += new EventHandler(schnittstelleHinzufügenToolStripMenuItem_Click);
            this.deviceTreeControl.SelectedNodeChanged += new EventHandler<TreeViewEventArgs>(deviceTreeControl_SelectedNodeChanged);
        }

        void deviceTreeControl_SelectedNodeChanged(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is IoDescription)
            {
                IoDescription desc = e.Node.Tag as IoDescription;
                this.descriptionEditorPresenter.InterfaceDescription = desc;
            }
        }
        #endregion

        #region Event Handler
        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void neuesProjektToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.frmNewProj == null)
                this.frmNewProj = new FormNewProject();
            this.frmNewProj.Project = new Project();
            DialogResult res = this.frmNewProj.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.OpenedProject = frmNewProj.Project;
                this.deviceTreeControl.Project = this.openProject;
            }
            else
            {
                this.frmNewProj.Project = null;
            }
        }

        private void geräteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.frmDevices = new FormManageDevices();
            this.frmDevices.Project = this.openProject;
            this.frmDevices.ShowDialog();
        }

        private void schnittstelleHinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openProject == null)
            {
                MessageBox.Show("Kein Projekt geöffnet!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.openProject.Devices == null || this.openProject.Devices.Length == 0)
            {
                MessageBox.Show("Bitte fügen Sie zuerst Geräte zum Projekt hinzu!", "Keine Geräte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (frmNewIoDesc == null)
                this.frmNewIoDesc = new FormNewIoDescription();
            this.frmNewIoDesc.AvailableDevices = this.openProject.Devices;
            DialogResult res = this.frmNewIoDesc.ShowDialog();
            if (res != System.Windows.Forms.DialogResult.OK)
            {
                if (this.frmNewIoDesc.IoDesc != null)
                    this.frmNewIoDesc.IoDesc.RemoveFromDevices();
            }
            else
            {
                this.deviceTreeControl.InitTree();
            }
            this.frmNewIoDesc.ResetIoDesc();
        }

        private void projektSpeichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = this.saveFileDialog.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK && this.openProject != null)
            {
                Project.ToFile(saveFileDialog.FileName, this.openProject);
            }
        }

        private void projektLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = this.openFileDialog.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.OpenedProject = Project.FromFile(this.openFileDialog.FileName);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, exp.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.deviceTreeControl.Project = this.openProject;
            }
        }
        #endregion
    
    }
}
