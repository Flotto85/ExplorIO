using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ExplorIO.Data;

namespace ExplorIO
{
    public partial class FormNewProject : Form
    {
        #region Fields and Properties
        bool cancelClose;

        public Project Project
        {
            get { return this.projectControl.Project; }
            set { this.projectControl.Project = value; }
        }        
        #endregion

        #region Initialization
        public FormNewProject()
        {
            InitializeComponent();
            cancelClose = false;

            this.FormClosing += new FormClosingEventHandler(FormNewProject_FormClosing);
        }
        #endregion

        #region Event Handler
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (projectControl.ValidateData())
            {
                cancelClose = false;
                this.projectControl.UpdateProject();
            }
            else
                cancelClose = true;
        }

        private void FormNewProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = cancelClose;
        }
        #endregion
    }
}
