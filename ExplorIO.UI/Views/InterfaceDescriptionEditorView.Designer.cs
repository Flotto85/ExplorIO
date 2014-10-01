namespace ExplorIO.UI.Views
{
    partial class InterfaceDescriptionEditorView
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
            this.tileGridControl = new ExplorIO.UI.Controls.TileGridControl();
            this.SuspendLayout();
            // 
            // tileGridControl
            // 
            this.tileGridControl.ColumnCount = 0;
            this.tileGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileGridControl.Location = new System.Drawing.Point(0, 0);
            this.tileGridControl.Name = "tileGridControl";
            this.tileGridControl.Size = new System.Drawing.Size(988, 578);
            this.tileGridControl.TabIndex = 0;
            this.tileGridControl.TileSize = 96;
            // 
            // InterfaceDescriptionEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tileGridControl);
            this.Name = "InterfaceDescriptionEditorView";
            this.Size = new System.Drawing.Size(988, 578);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TileGridControl tileGridControl;
    }
}
