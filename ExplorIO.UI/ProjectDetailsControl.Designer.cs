namespace ExplorIO.UI
{
    partial class ProjectDetailsControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.mtbProjectNo = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCustomerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbProjectName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbProjectDesc = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Projektnummer:";
            // 
            // mtbProjectNo
            // 
            this.mtbProjectNo.Location = new System.Drawing.Point(119, 7);
            this.mtbProjectNo.Mask = "00000";
            this.mtbProjectNo.Name = "mtbProjectNo";
            this.mtbProjectNo.Size = new System.Drawing.Size(100, 20);
            this.mtbProjectNo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kunde:";
            // 
            // tbCustomerName
            // 
            this.tbCustomerName.Location = new System.Drawing.Point(119, 33);
            this.tbCustomerName.Name = "tbCustomerName";
            this.tbCustomerName.Size = new System.Drawing.Size(100, 20);
            this.tbCustomerName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Projektname:";
            // 
            // tbProjectName
            // 
            this.tbProjectName.Location = new System.Drawing.Point(119, 59);
            this.tbProjectName.Name = "tbProjectName";
            this.tbProjectName.Size = new System.Drawing.Size(100, 20);
            this.tbProjectName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Projektbeschreibung:";
            // 
            // tbProjectDesc
            // 
            this.tbProjectDesc.Location = new System.Drawing.Point(119, 85);
            this.tbProjectDesc.Multiline = true;
            this.tbProjectDesc.Name = "tbProjectDesc";
            this.tbProjectDesc.Size = new System.Drawing.Size(132, 107);
            this.tbProjectDesc.TabIndex = 4;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ProjectDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbProjectDesc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbProjectName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbCustomerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mtbProjectNo);
            this.Controls.Add(this.label1);
            this.Name = "ProjectDetailsControl";
            this.Size = new System.Drawing.Size(262, 204);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mtbProjectNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCustomerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbProjectName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbProjectDesc;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
