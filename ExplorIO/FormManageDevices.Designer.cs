namespace ExplorIO
{
    partial class FormManageDevices
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
            this.lbDevices = new System.Windows.Forms.ListBox();
            this.btnAddDevice = new System.Windows.Forms.Button();
            this.btnRemoveDevice = new System.Windows.Forms.Button();
            this.deviceDetailsControl = new ExplorIO.UI.DeviceDetailsControl();
            this.SuspendLayout();
            // 
            // lbDevices
            // 
            this.lbDevices.FormattingEnabled = true;
            this.lbDevices.Location = new System.Drawing.Point(12, 131);
            this.lbDevices.Name = "lbDevices";
            this.lbDevices.Size = new System.Drawing.Size(145, 225);
            this.lbDevices.TabIndex = 1;
            // 
            // btnAddDevice
            // 
            this.btnAddDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDevice.Location = new System.Drawing.Point(71, 85);
            this.btnAddDevice.Name = "btnAddDevice";
            this.btnAddDevice.Size = new System.Drawing.Size(40, 40);
            this.btnAddDevice.TabIndex = 2;
            this.btnAddDevice.Text = "+";
            this.btnAddDevice.UseVisualStyleBackColor = true;
            // 
            // btnRemoveDevice
            // 
            this.btnRemoveDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveDevice.Location = new System.Drawing.Point(117, 85);
            this.btnRemoveDevice.Name = "btnRemoveDevice";
            this.btnRemoveDevice.Size = new System.Drawing.Size(40, 40);
            this.btnRemoveDevice.TabIndex = 2;
            this.btnRemoveDevice.Text = "-";
            this.btnRemoveDevice.UseVisualStyleBackColor = true;
            // 
            // deviceDetailsControl
            // 
            this.deviceDetailsControl.Device = null;
            this.deviceDetailsControl.Enabled = false;
            this.deviceDetailsControl.Location = new System.Drawing.Point(12, 12);
            this.deviceDetailsControl.Name = "deviceDetailsControl";
            this.deviceDetailsControl.Size = new System.Drawing.Size(184, 67);
            this.deviceDetailsControl.TabIndex = 0;
            // 
            // FormManageDevices
            // 
            this.AcceptButton = this.btnAddDevice;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 371);
            this.Controls.Add(this.btnRemoveDevice);
            this.Controls.Add(this.btnAddDevice);
            this.Controls.Add(this.lbDevices);
            this.Controls.Add(this.deviceDetailsControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormManageDevices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Geräte Bearbeiten";
            this.ResumeLayout(false);

        }

        #endregion

        private UI.DeviceDetailsControl deviceDetailsControl;
        private System.Windows.Forms.ListBox lbDevices;
        private System.Windows.Forms.Button btnAddDevice;
        private System.Windows.Forms.Button btnRemoveDevice;
    }
}