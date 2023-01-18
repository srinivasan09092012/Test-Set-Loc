namespace SSRSImportExportWizard
{
    partial class ExportReportView
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
            this.ExportTreeView = new System.Windows.Forms.TreeView();
            this.btnExportReports = new System.Windows.Forms.Button();
            this.ExportClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExportTreeView
            // 
            this.ExportTreeView.Location = new System.Drawing.Point(12, 12);
            this.ExportTreeView.Name = "ExportTreeView";
            this.ExportTreeView.Size = new System.Drawing.Size(767, 566);
            this.ExportTreeView.TabIndex = 1;
            this.ExportTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ExportTreeView_AfterCheck);
            // 
            // btnExportReports
            // 
            this.btnExportReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportReports.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportReports.Location = new System.Drawing.Point(643, 586);
            this.btnExportReports.Name = "btnExportReports";
            this.btnExportReports.Size = new System.Drawing.Size(136, 38);
            this.btnExportReports.TabIndex = 13;
            this.btnExportReports.Text = "Export Reports";
            this.btnExportReports.UseVisualStyleBackColor = true;
            this.btnExportReports.Click += new System.EventHandler(this.btnExportReports_Click);
            // 
            // ExportClose
            // 
            this.ExportClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportClose.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportClose.Location = new System.Drawing.Point(12, 586);
            this.ExportClose.Name = "ExportClose";
            this.ExportClose.Size = new System.Drawing.Size(136, 38);
            this.ExportClose.TabIndex = 14;
            this.ExportClose.Text = "Close";
            this.ExportClose.UseVisualStyleBackColor = true;
            this.ExportClose.Click += new System.EventHandler(this.ExportClose_Click);
            // 
            // ExportReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 636);
            this.Controls.Add(this.ExportClose);
            this.Controls.Add(this.btnExportReports);
            this.Controls.Add(this.ExportTreeView);
            this.MaximizeBox = false;
            this.Name = "ExportReportView";
            this.Text = "Export Report View";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ExportTreeView;
        private System.Windows.Forms.Button btnExportReports;
        private System.Windows.Forms.Button ExportClose;
    }
}