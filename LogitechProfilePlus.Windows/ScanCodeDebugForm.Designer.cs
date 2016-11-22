namespace LogitechProfilePlus.Windows
{
    partial class ScanCodeDebugForm
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
            this.btnScanCodeUpdate = new System.Windows.Forms.Button();
            this.lstScanCodeHistory = new System.Windows.Forms.ListBox();
            this.btnScanCodeDown = new System.Windows.Forms.Button();
            this.btnScanCodeUp = new System.Windows.Forms.Button();
            this.txtScanCodeDebug = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnScanCodeUpdate
            // 
            this.btnScanCodeUpdate.Location = new System.Drawing.Point(164, 11);
            this.btnScanCodeUpdate.Name = "btnScanCodeUpdate";
            this.btnScanCodeUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnScanCodeUpdate.TabIndex = 47;
            this.btnScanCodeUpdate.Text = "button1";
            this.btnScanCodeUpdate.UseVisualStyleBackColor = true;
            // 
            // lstScanCodeHistory
            // 
            this.lstScanCodeHistory.FormattingEnabled = true;
            this.lstScanCodeHistory.Location = new System.Drawing.Point(91, 38);
            this.lstScanCodeHistory.Name = "lstScanCodeHistory";
            this.lstScanCodeHistory.Size = new System.Drawing.Size(148, 147);
            this.lstScanCodeHistory.TabIndex = 46;
            // 
            // btnScanCodeDown
            // 
            this.btnScanCodeDown.Location = new System.Drawing.Point(10, 67);
            this.btnScanCodeDown.Name = "btnScanCodeDown";
            this.btnScanCodeDown.Size = new System.Drawing.Size(75, 23);
            this.btnScanCodeDown.TabIndex = 45;
            this.btnScanCodeDown.Text = "Down";
            this.btnScanCodeDown.UseVisualStyleBackColor = true;
            // 
            // btnScanCodeUp
            // 
            this.btnScanCodeUp.Location = new System.Drawing.Point(10, 38);
            this.btnScanCodeUp.Name = "btnScanCodeUp";
            this.btnScanCodeUp.Size = new System.Drawing.Size(75, 23);
            this.btnScanCodeUp.TabIndex = 44;
            this.btnScanCodeUp.Text = "Up";
            this.btnScanCodeUp.UseVisualStyleBackColor = true;
            // 
            // txtScanCodeDebug
            // 
            this.txtScanCodeDebug.Location = new System.Drawing.Point(10, 12);
            this.txtScanCodeDebug.Name = "txtScanCodeDebug";
            this.txtScanCodeDebug.Size = new System.Drawing.Size(148, 20);
            this.txtScanCodeDebug.TabIndex = 43;
            // 
            // ScanCodeDebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 213);
            this.Controls.Add(this.btnScanCodeUpdate);
            this.Controls.Add(this.lstScanCodeHistory);
            this.Controls.Add(this.btnScanCodeDown);
            this.Controls.Add(this.btnScanCodeUp);
            this.Controls.Add(this.txtScanCodeDebug);
            this.Name = "ScanCodeDebugForm";
            this.Text = "ScanCodeDebugForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScanCodeUpdate;
        private System.Windows.Forms.ListBox lstScanCodeHistory;
        private System.Windows.Forms.Button btnScanCodeDown;
        private System.Windows.Forms.Button btnScanCodeUp;
        private System.Windows.Forms.TextBox txtScanCodeDebug;
    }
}