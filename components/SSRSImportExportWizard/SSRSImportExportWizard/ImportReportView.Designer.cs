namespace SSRSImportExportWizard
{
    partial class ImportReportView
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
            this.ImportTreeView = new System.Windows.Forms.TreeView();
            this.btnImportReports = new System.Windows.Forms.Button();
            this.ImportClose = new System.Windows.Forms.Button();
            this.btnUpdateDS = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ImportTreeView
            // 
            this.ImportTreeView.CheckBoxes = true;
            this.ImportTreeView.Location = new System.Drawing.Point(8, 12);
            this.ImportTreeView.Name = "ImportTreeView";
            this.ImportTreeView.Size = new System.Drawing.Size(767, 566);
            this.ImportTreeView.TabIndex = 2;
            this.ImportTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ImportTreeView_AfterCheck);
            this.ImportTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ImportTreeView_MouseDoubleClick);
            // 
            // btnImportReports
            // 
            this.btnImportReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportReports.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportReports.Location = new System.Drawing.Point(639, 586);
            this.btnImportReports.Name = "btnImportReports";
            this.btnImportReports.Size = new System.Drawing.Size(136, 38);
            this.btnImportReports.TabIndex = 14;
            this.btnImportReports.Text = "Import Reports";
            this.btnImportReports.UseVisualStyleBackColor = true;
            this.btnImportReports.Click += new System.EventHandler(this.btnImportReports_Click);
            // 
            // ImportClose
            // 
            this.ImportClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportClose.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportClose.Location = new System.Drawing.Point(8, 586);
            this.ImportClose.Name = "ImportClose";
            this.ImportClose.Size = new System.Drawing.Size(136, 38);
            this.ImportClose.TabIndex = 15;
            this.ImportClose.Text = "Close";
            this.ImportClose.UseVisualStyleBackColor = true;
            this.ImportClose.Click += new System.EventHandler(this.ImportClose_Click);
            // 
            // btnUpdateDS
            // 
            this.btnUpdateDS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateDS.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateDS.Location = new System.Drawing.Point(150, 586);
            this.btnUpdateDS.Name = "btnUpdateDS";
            this.btnUpdateDS.Size = new System.Drawing.Size(152, 38);
            this.btnUpdateDS.TabIndex = 16;
            this.btnUpdateDS.Text = "View Data Source";
            this.btnUpdateDS.UseVisualStyleBackColor = true;
            this.btnUpdateDS.Click += new System.EventHandler(this.btnUpdateDS_Click);
            // 
            // ImportReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 636);
            this.Controls.Add(this.btnUpdateDS);
            this.Controls.Add(this.ImportClose);
            this.Controls.Add(this.btnImportReports);
            this.Controls.Add(this.ImportTreeView);
            this.Name = "ImportReportView";
            this.Text = "ImportReportView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ImportTreeView;
        private System.Windows.Forms.Button btnImportReports;
        private System.Windows.Forms.Button ImportClose;
        private System.Windows.Forms.Button btnUpdateDS;
    }
}