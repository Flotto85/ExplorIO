namespace ExplorIO.UI
{
    partial class IoDescDetailsControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.cobInputDevice = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cobOutputDevice = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudInputStartAddress = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudOutputStartAddress = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudIoSize = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudInputStartAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputStartAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIoSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Gerät:";
            // 
            // cobInputDevice
            // 
            this.cobInputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobInputDevice.FormattingEnabled = true;
            this.cobInputDevice.Location = new System.Drawing.Point(207, 31);
            this.cobInputDevice.Name = "cobInputDevice";
            this.cobInputDevice.Size = new System.Drawing.Size(121, 21);
            this.cobInputDevice.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Output Gerät:";
            // 
            // cobOutputDevice
            // 
            this.cobOutputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobOutputDevice.FormattingEnabled = true;
            this.cobOutputDevice.Location = new System.Drawing.Point(14, 31);
            this.cobOutputDevice.Name = "cobOutputDevice";
            this.cobOutputDevice.Size = new System.Drawing.Size(121, 21);
            this.cobOutputDevice.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(151, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "→";
            // 
            // nudInputStartAddress
            // 
            this.nudInputStartAddress.Location = new System.Drawing.Point(207, 80);
            this.nudInputStartAddress.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudInputStartAddress.Name = "nudInputStartAddress";
            this.nudInputStartAddress.Size = new System.Drawing.Size(70, 20);
            this.nudInputStartAddress.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Start Adresse:";
            // 
            // nudOutputStartAddress
            // 
            this.nudOutputStartAddress.Location = new System.Drawing.Point(14, 80);
            this.nudOutputStartAddress.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudOutputStartAddress.Name = "nudOutputStartAddress";
            this.nudOutputStartAddress.Size = new System.Drawing.Size(68, 20);
            this.nudOutputStartAddress.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(204, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Start Adresse:";
            // 
            // nudIoSize
            // 
            this.nudIoSize.Location = new System.Drawing.Point(14, 128);
            this.nudIoSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudIoSize.Name = "nudIoSize";
            this.nudIoSize.Size = new System.Drawing.Size(68, 20);
            this.nudIoSize.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Schnittstellengröße:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(88, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Byte";
            // 
            // IoDescDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudIoSize);
            this.Controls.Add(this.nudOutputStartAddress);
            this.Controls.Add(this.nudInputStartAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cobOutputDevice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cobInputDevice);
            this.Controls.Add(this.label1);
            this.Name = "IoDescDetailsControl";
            this.Size = new System.Drawing.Size(340, 161);
            ((System.ComponentModel.ISupportInitialize)(this.nudInputStartAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputStartAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIoSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cobInputDevice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cobOutputDevice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudInputStartAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudOutputStartAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudIoSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}
