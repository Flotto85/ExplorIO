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
    public partial class ProjectDetailsControl : UserControl
    {
        #region Fields and Properties
        private Project project;

        public Project Project
        {
            get { return project; }
            set {
                if (this.project != null)
                {
                    this.project.PropertyChanged -= new PropertyChangedEventHandler(OnProjectPropertyChanged);
                }
                project = value;
                if (this.project != null)
                {
                    this.project.PropertyChanged += new PropertyChangedEventHandler(OnProjectPropertyChanged);
                    this.Enabled = true;
                }
            }
        }
        #endregion

        #region Initialization
        public ProjectDetailsControl()
        {
            InitializeComponent();
            this.tbCustomerName.Leave += new EventHandler(OnTbCustomerNameLeave);
            this.tbProjectDesc.Leave += new EventHandler(OnTbProjectDescLeave);
            this.tbProjectName.Leave += new EventHandler(OnTbProjectNameLeave);
            this.mtbProjectNo.Leave += new EventHandler(OnMtbProjectNoLeave);
            this.UpdateControl();
        }
        #endregion

        #region Interface
        public void UpdateProject()
        {
            if (this.project == null) return;

            if (mtbProjectNo.Text != this.project.ProjectNumber.ToString())
            {
                uint proNo = 0;
                if (UInt32.TryParse(mtbProjectNo.Text, out proNo))
                {
                    this.project.ProjectNumber = proNo;
                }
            }

            if (tbProjectName.Text != this.project.ProjectName)
            {
                this.project.ProjectName = tbProjectName.Text;
            }

            if (tbProjectDesc.Text != this.project.ProjectDescription)
            {
                this.project.ProjectDescription = tbProjectDesc.Text;
            }

            if (tbCustomerName.Text != this.project.CustomerName)
            {
                this.project.CustomerName = tbCustomerName.Text;
            }
        }

        public bool ValidateData()
        {
            bool result = true;
            result &= this.mtbProjectNo.Text != String.Empty;
            if (result)
            {
                uint tmp;
                result &= UInt32.TryParse(this.mtbProjectNo.Text, out tmp);
            }
            if (!result)
            {
                errorProvider.SetError(mtbProjectNo, "Bitte geben Sie eine gültige Projektnummer an!");
            }
            else
            {
                errorProvider.Clear();
            }
            return result;
        }
        #endregion

        #region Tools
        private void UpdateControl()
        {
            string custName = "";
            string projectNo = "";
            string projectDesc = "";
            string projectName = "";

            if (this.project != null)
            {
                this.Enabled = true;
                custName = this.project.CustomerName;
                projectNo = this.project.ProjectNumber.ToString();
                projectDesc = this.project.ProjectDescription;
                projectName = this.project.ProjectName;
            }
            else
            {
                this.Enabled = false;
            }

            if (custName != this.tbCustomerName.Text)
            {
                this.tbCustomerName.Text = custName;
            }
            if (projectDesc != tbProjectDesc.Text)
            {
                this.tbProjectDesc.Text = projectDesc;
            }
            if (projectName != tbProjectName.Text)
            {
                this.tbProjectName.Text = projectName;
            }
            if (projectNo.ToString() != mtbProjectNo.Text)
            {
                mtbProjectNo.Text = projectNo.ToString();
            }
        }
        #endregion 

        #region Event Handler
        private void OnProjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateControl();
        }

        private void OnMtbProjectNoLeave(object sender, EventArgs e)
        {
            this.UpdateProject();
        }

        private void OnTbProjectNameLeave(object sender, EventArgs e)
        {
            this.UpdateProject();
        }

        private void OnTbProjectDescLeave(object sender, EventArgs e)
        {
            this.UpdateProject();
        }

        private void OnTbCustomerNameLeave(object sender, EventArgs e)
        {
            this.UpdateProject();
        }
        #endregion
    }
}
