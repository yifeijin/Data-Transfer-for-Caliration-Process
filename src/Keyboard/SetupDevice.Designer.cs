namespace Keyboard
{
    partial class SetupDevices
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.AddDeviceButton = new System.Windows.Forms.Button();
            this.DeviceIdText = new System.Windows.Forms.TextBox();
            this.typeCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.deviceGridView = new System.Windows.Forms.DataGridView();
            this.startBT = new System.Windows.Forms.Button();
            this.DeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scannerLB = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.deviceGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Device ID:";
            // 
            // AddDeviceButton
            // 
            this.AddDeviceButton.Location = new System.Drawing.Point(12, 179);
            this.AddDeviceButton.Name = "AddDeviceButton";
            this.AddDeviceButton.Size = new System.Drawing.Size(113, 23);
            this.AddDeviceButton.TabIndex = 4;
            this.AddDeviceButton.Text = "Add Device";
            this.AddDeviceButton.UseVisualStyleBackColor = true;
            this.AddDeviceButton.Click += new System.EventHandler(this.AddDeviceButton_Click);
            // 
            // DeviceIdText
            // 
            this.DeviceIdText.Location = new System.Drawing.Point(12, 25);
            this.DeviceIdText.Name = "DeviceIdText";
            this.DeviceIdText.Size = new System.Drawing.Size(247, 20);
            this.DeviceIdText.TabIndex = 7;
            // 
            // typeCB
            // 
            this.typeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCB.FormattingEnabled = true;
            this.typeCB.Items.AddRange(new object[] {
            "Normal Device",
            "Reference Device"});
            this.typeCB.Location = new System.Drawing.Point(12, 82);
            this.typeCB.Name = "typeCB";
            this.typeCB.Size = new System.Drawing.Size(247, 21);
            this.typeCB.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Device Type:";
            // 
            // deviceGridView
            // 
            this.deviceGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.deviceGridView.Location = new System.Drawing.Point(284, 8);
            this.deviceGridView.Name = "deviceGridView";
            this.deviceGridView.Size = new System.Drawing.Size(453, 194);
            this.deviceGridView.TabIndex = 10;
            // 
            // startBT
            // 
            this.startBT.Location = new System.Drawing.Point(149, 179);
            this.startBT.Name = "startBT";
            this.startBT.Size = new System.Drawing.Size(110, 23);
            this.startBT.TabIndex = 11;
            this.startBT.Text = "Start Calibration";
            this.startBT.UseVisualStyleBackColor = true;
            this.startBT.Click += new System.EventHandler(this.startBT_Click);
            // 
            // DeviceName
            // 
            this.DeviceName.HeaderText = "Device Name";
            this.DeviceName.Name = "DeviceName";
            // 
            // DeviceType
            // 
            this.DeviceType.HeaderText = "Device Type";
            this.DeviceType.Name = "DeviceType";
            // 
            // scannerLB
            // 
            this.scannerLB.AutoSize = true;
            this.scannerLB.Location = new System.Drawing.Point(9, 137);
            this.scannerLB.Name = "scannerLB";
            this.scannerLB.Size = new System.Drawing.Size(64, 13);
            this.scannerLB.TabIndex = 12;
            this.scannerLB.Text = "Scanner ID:";
            // 
            // SetupDevices
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 214);
            this.Controls.Add(this.scannerLB);
            this.Controls.Add(this.startBT);
            this.Controls.Add(this.deviceGridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.typeCB);
            this.Controls.Add(this.DeviceIdText);
            this.Controls.Add(this.AddDeviceButton);
            this.Controls.Add(this.label1);
            this.Name = "SetupDevices";
            this.Text = "Setup Device";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetDevices_FormClosing);
            this.Load += new System.EventHandler(this.SetDevices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.deviceGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddDeviceButton;
        private System.Windows.Forms.TextBox DeviceIdText;
        private System.Windows.Forms.ComboBox typeCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView deviceGridView;
        private System.Windows.Forms.Button startBT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceType;
        private System.Windows.Forms.Label scannerLB;
    }
}