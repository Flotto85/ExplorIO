namespace ExplorIO
{
    partial class FormMain
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuesProjektToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projektLadenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projektSpeichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projektToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geräteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schnittstelleHinzufügenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.eigenschaftenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.deviceTreeControl = new ExplorIO.UI.DevicesTreeControl();
            this.ioDescEditorControl1 = new ExplorIO.UI.IoDescEditorControl2();
            this.menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripMain
            // 
            this.statusStripMain.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.statusStripMain.Location = new System.Drawing.Point(0, 597);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1291, 22);
            this.statusStripMain.TabIndex = 0;
            this.statusStripMain.Text = "statusStripMain";
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.projektToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1291, 24);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStrip";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuesProjektToolStripMenuItem,
            this.projektLadenToolStripMenuItem,
            this.projektSpeichernToolStripMenuItem,
            this.toolStripMenuItem1,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "&Datei";
            // 
            // neuesProjektToolStripMenuItem
            // 
            this.neuesProjektToolStripMenuItem.Name = "neuesProjektToolStripMenuItem";
            this.neuesProjektToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.neuesProjektToolStripMenuItem.Text = "&Neues Projekt";
            // 
            // projektLadenToolStripMenuItem
            // 
            this.projektLadenToolStripMenuItem.Name = "projektLadenToolStripMenuItem";
            this.projektLadenToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.projektLadenToolStripMenuItem.Text = "Projekt &Laden";
            // 
            // projektSpeichernToolStripMenuItem
            // 
            this.projektSpeichernToolStripMenuItem.Name = "projektSpeichernToolStripMenuItem";
            this.projektSpeichernToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.projektSpeichernToolStripMenuItem.Text = "Projekt &Speichern";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.beendenToolStripMenuItem.Text = "&Beenden";
            // 
            // projektToolStripMenuItem
            // 
            this.projektToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geräteToolStripMenuItem,
            this.schnittstelleHinzufügenToolStripMenuItem,
            this.toolStripMenuItem2,
            this.eigenschaftenToolStripMenuItem});
            this.projektToolStripMenuItem.Name = "projektToolStripMenuItem";
            this.projektToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projektToolStripMenuItem.Text = "&Projekt";
            // 
            // geräteToolStripMenuItem
            // 
            this.geräteToolStripMenuItem.Name = "geräteToolStripMenuItem";
            this.geräteToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.geräteToolStripMenuItem.Text = "&Geräte";
            // 
            // schnittstelleHinzufügenToolStripMenuItem
            // 
            this.schnittstelleHinzufügenToolStripMenuItem.Name = "schnittstelleHinzufügenToolStripMenuItem";
            this.schnittstelleHinzufügenToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.schnittstelleHinzufügenToolStripMenuItem.Text = "&Schnittstelle hinzufügen";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(198, 6);
            // 
            // eigenschaftenToolStripMenuItem
            // 
            this.eigenschaftenToolStripMenuItem.Name = "eigenschaftenToolStripMenuItem";
            this.eigenschaftenToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.eigenschaftenToolStripMenuItem.Text = "&Eigenschaften";
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.deviceTreeControl);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.ioDescEditorControl1);
            this.splitContainer.Size = new System.Drawing.Size(1291, 573);
            this.splitContainer.SplitterDistance = 213;
            this.splitContainer.SplitterWidth = 8;
            this.splitContainer.TabIndex = 2;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "iox";
            this.openFileDialog.FileName = "default";
            this.openFileDialog.Filter = "Schnittstellendateien (*.iox)|*.iox";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "iox";
            this.saveFileDialog.Filter = "Schnittstellendateien (*.iox)|*.iox";
            // 
            // deviceTreeControl
            // 
            this.deviceTreeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceTreeControl.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deviceTreeControl.Location = new System.Drawing.Point(0, 0);
            this.deviceTreeControl.Margin = new System.Windows.Forms.Padding(4);
            this.deviceTreeControl.Name = "deviceTreeControl";
            this.deviceTreeControl.Project = null;
            this.deviceTreeControl.Size = new System.Drawing.Size(211, 571);
            this.deviceTreeControl.TabIndex = 0;
            // 
            // ioDescEditorControl1
            // 
            this.ioDescEditorControl1.AutoScroll = true;
            this.ioDescEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ioDescEditorControl1.IoDesc = null;
            this.ioDescEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.ioDescEditorControl1.Name = "ioDescEditorControl1";
            this.ioDescEditorControl1.Size = new System.Drawing.Size(1068, 571);
            this.ioDescEditorControl1.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 619);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExplorIO";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuesProjektToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projektLadenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projektSpeichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projektToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geräteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schnittstelleHinzufügenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem eigenschaftenToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private UI.DevicesTreeControl deviceTreeControl;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private UI.IoDescEditorControl2 ioDescEditorControl1;
    }
}

