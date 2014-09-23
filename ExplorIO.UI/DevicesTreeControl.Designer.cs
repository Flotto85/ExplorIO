namespace ExplorIO.UI
{
    partial class DevicesTreeControl
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
            this.treeViewDevices = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeViewDevices
            // 
            this.treeViewDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDevices.Location = new System.Drawing.Point(0, 0);
            this.treeViewDevices.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeViewDevices.Name = "treeViewDevices";
            this.treeViewDevices.Size = new System.Drawing.Size(377, 718);
            this.treeViewDevices.TabIndex = 0;
            // 
            // DevicesTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeViewDevices);
            this.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DevicesTreeControl";
            this.Size = new System.Drawing.Size(377, 718);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewDevices;
    }
}
