namespace Keyboard
{
    partial class EditValue
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
            this.ODText = new System.Windows.Forms.TextBox();
            this.ConfirmBT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.Animation;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Value:";
            // 
            // ODText
            // 
            this.ODText.Location = new System.Drawing.Point(12, 25);
            this.ODText.Name = "ODText";
            this.ODText.Size = new System.Drawing.Size(240, 20);
            this.ODText.TabIndex = 1;
            // 
            // ConfirmBT
            // 
            this.ConfirmBT.Location = new System.Drawing.Point(12, 51);
            this.ConfirmBT.Name = "ConfirmBT";
            this.ConfirmBT.Size = new System.Drawing.Size(86, 23);
            this.ConfirmBT.TabIndex = 8;
            this.ConfirmBT.Text = "Confirm";
            this.ConfirmBT.UseVisualStyleBackColor = true;
            this.ConfirmBT.Click += new System.EventHandler(this.ConfirmBT_Click);
            // 
            // EditValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 86);
            this.Controls.Add(this.ConfirmBT);
            this.Controls.Add(this.ODText);
            this.Controls.Add(this.label1);
            this.Name = "EditValue";
            this.Text = "EditValue";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditValue_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ODText;
        private System.Windows.Forms.Button ConfirmBT;
    }
}