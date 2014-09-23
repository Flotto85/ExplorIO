namespace ExplorIO.UI
{
    partial class IoByteControl
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
            this.flowLayoutPanelBits = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanelBits
            // 
            this.flowLayoutPanelBits.AllowDrop = true;
            this.flowLayoutPanelBits.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flowLayoutPanelBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelBits.Location = new System.Drawing.Point(4, 2);
            this.flowLayoutPanelBits.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelBits.Name = "flowLayoutPanelBits";
            this.flowLayoutPanelBits.Size = new System.Drawing.Size(736, 92);
            this.flowLayoutPanelBits.TabIndex = 0;
            // 
            // IoByteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.flowLayoutPanelBits);
            this.DoubleBuffered = true;
            this.Name = "IoByteControl";
            this.Padding = new System.Windows.Forms.Padding(4, 2, 15, 2);
            this.Size = new System.Drawing.Size(755, 96);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBits;
    }
}
