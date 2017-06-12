namespace SSRSImportExportWizard
{
    partial class ReportCompare
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
            this.ReportCompareBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // ReportCompareBrowser
            // 
            this.ReportCompareBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportCompareBrowser.Location = new System.Drawing.Point(0, 0);
            this.ReportCompareBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.ReportCompareBrowser.Name = "ReportCompareBrowser";
            this.ReportCompareBrowser.Size = new System.Drawing.Size(734, 653);
            this.ReportCompareBrowser.TabIndex = 0;
            // 
            // ReportCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 653);
            this.Controls.Add(this.ReportCompareBrowser);
            this.MinimizeBox = false;
            this.Name = "ReportCompare";
            this.Text = "Report Compare";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReportCompare_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser ReportCompareBrowser;
    }
}