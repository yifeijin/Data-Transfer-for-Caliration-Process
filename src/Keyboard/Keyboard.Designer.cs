namespace Keyboard
{
    partial class Keyboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Keyboard));
            this.gridView = new System.Windows.Forms.DataGridView();
            this.AddDeviceButton = new System.Windows.Forms.Button();
            this.exportBT = new System.Windows.Forms.Button();
            this.CheckBT = new System.Windows.Forms.Button();
            this.SourceLB = new System.Windows.Forms.Label();
            this.barcodeBT = new System.Windows.Forms.Button();
            this.LoadBT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView
            // 
            this.gridView.AllowUserToOrderColumns = true;
            this.gridView.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Location = new System.Drawing.Point(12, 41);
            this.gridView.MultiSelect = false;
            this.gridView.Name = "gridView";
            this.gridView.Size = new System.Drawing.Size(843, 425);
            this.gridView.TabIndex = 27;
            this.gridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellDoubleClick);
            this.gridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridView_CellPainting);
            this.gridView.SelectionChanged += new System.EventHandler(this.gridTable_SelectionChanged);
            // 
            // AddDeviceButton
            // 
            this.AddDeviceButton.Location = new System.Drawing.Point(12, 12);
            this.AddDeviceButton.Name = "AddDeviceButton";
            this.AddDeviceButton.Size = new System.Drawing.Size(124, 23);
            this.AddDeviceButton.TabIndex = 28;
            this.AddDeviceButton.Text = "Add Device";
            this.AddDeviceButton.UseVisualStyleBackColor = true;
            this.AddDeviceButton.Click += new System.EventHandler(this.AddDeviceButton_Click);
            // 
            // exportBT
            // 
            this.exportBT.Location = new System.Drawing.Point(585, 472);
            this.exportBT.Name = "exportBT";
            this.exportBT.Size = new System.Drawing.Size(126, 23);
            this.exportBT.TabIndex = 29;
            this.exportBT.Text = "Export";
            this.exportBT.UseVisualStyleBackColor = true;
            this.exportBT.Click += new System.EventHandler(this.exportBT_Click);
            // 
            // CheckBT
            // 
            this.CheckBT.Location = new System.Drawing.Point(742, 472);
            this.CheckBT.Name = "CheckBT";
            this.CheckBT.Size = new System.Drawing.Size(113, 23);
            this.CheckBT.TabIndex = 30;
            this.CheckBT.Text = "Check";
            this.CheckBT.UseVisualStyleBackColor = true;
            this.CheckBT.Click += new System.EventHandler(this.checkBT_Click);
            // 
            // SourceLB
            // 
            this.SourceLB.AutoSize = true;
            this.SourceLB.Location = new System.Drawing.Point(354, 12);
            this.SourceLB.Name = "SourceLB";
            this.SourceLB.Size = new System.Drawing.Size(44, 13);
            this.SourceLB.TabIndex = 31;
            this.SourceLB.Text = "Source:";
            // 
            // barcodeBT
            // 
            this.barcodeBT.Location = new System.Drawing.Point(180, 12);
            this.barcodeBT.Name = "barcodeBT";
            this.barcodeBT.Size = new System.Drawing.Size(124, 23);
            this.barcodeBT.TabIndex = 33;
            this.barcodeBT.Text = "Generate Barcode";
            this.barcodeBT.UseVisualStyleBackColor = true;
            this.barcodeBT.Click += new System.EventHandler(this.barcodeBT_Click);
            // 
            // LoadBT
            // 
            this.LoadBT.Location = new System.Drawing.Point(431, 472);
            this.LoadBT.Name = "LoadBT";
            this.LoadBT.Size = new System.Drawing.Size(124, 23);
            this.LoadBT.TabIndex = 34;
            this.LoadBT.Text = "Load";
            this.LoadBT.UseVisualStyleBackColor = true;
            this.LoadBT.Click += new System.EventHandler(this.importBt_Click);
            // 
            // Keyboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 507);
            this.Controls.Add(this.LoadBT);
            this.Controls.Add(this.barcodeBT);
            this.Controls.Add(this.SourceLB);
            this.Controls.Add(this.CheckBT);
            this.Controls.Add(this.exportBT);
            this.Controls.Add(this.AddDeviceButton);
            this.Controls.Add(this.gridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Keyboard";
            this.Text = "Raw Keyboard Input";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Keyboard_FormClosing);
            this.Load += new System.EventHandler(this.Keyboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView gridView;
        private System.Windows.Forms.Button AddDeviceButton;
        private System.Windows.Forms.Button exportBT;
        private System.Windows.Forms.Button CheckBT;
        private System.Windows.Forms.Label SourceLB;
        private System.Windows.Forms.Button barcodeBT;
        private System.Windows.Forms.Button LoadBT;
    }
}

