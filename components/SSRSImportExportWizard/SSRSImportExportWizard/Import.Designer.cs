namespace SSRSImportExportWizard
{
    partial class ImportControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label5 = new System.Windows.Forms.Label();
            this.lblTestSuccess = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImportTestConnection = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDownloadPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnViewReportObjects = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExportTitle = new System.Windows.Forms.Label();
            this.TargetURL = new System.Windows.Forms.TextBox();
            this.ExportBodyPanel = new System.Windows.Forms.Panel();
            this.txtReportServerPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ExportBodyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Path*";
            // 
            // lblTestSuccess
            // 
            this.lblTestSuccess.AutoSize = true;
            this.lblTestSuccess.ForeColor = System.Drawing.Color.Green;
            this.lblTestSuccess.Location = new System.Drawing.Point(324, 231);
            this.lblTestSuccess.Name = "lblTestSuccess";
            this.lblTestSuccess.Size = new System.Drawing.Size(0, 17);
            this.lblTestSuccess.TabIndex = 9;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(8, 578);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 17);
            this.lblError.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(422, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(204, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "*Report server credentials";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(111, 62);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(183, 26);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.Text = "Password2015";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(111, 30);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(183, 26);
            this.txtUserName.TabIndex = 5;
            this.txtUserName.Text = "ua3dev\\hciadmin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password*";
            // 
            // btnImportTestConnection
            // 
            this.btnImportTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportTestConnection.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportTestConnection.Location = new System.Drawing.Point(200, 245);
            this.btnImportTestConnection.Name = "btnImportTestConnection";
            this.btnImportTestConnection.Size = new System.Drawing.Size(108, 32);
            this.btnImportTestConnection.TabIndex = 12;
            this.btnImportTestConnection.Text = "Test connection";
            this.btnImportTestConnection.UseVisualStyleBackColor = true;
            this.btnImportTestConnection.Click += new System.EventHandler(this.btnImportTestConnection_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "Target*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "User Name*";
            // 
            // txtDownloadPath
            // 
            this.txtDownloadPath.Location = new System.Drawing.Point(91, 56);
            this.txtDownloadPath.Name = "txtDownloadPath";
            this.txtDownloadPath.Size = new System.Drawing.Size(604, 22);
            this.txtDownloadPath.TabIndex = 14;
            this.txtDownloadPath.Text = "C:\\UA3\\Reports\\SSRS_Reports";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(14, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Authentication";
            // 
            // btnViewReportObjects
            // 
            this.btnViewReportObjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewReportObjects.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewReportObjects.Location = new System.Drawing.Point(565, 570);
            this.btnViewReportObjects.Name = "btnViewReportObjects";
            this.btnViewReportObjects.Size = new System.Drawing.Size(136, 38);
            this.btnViewReportObjects.TabIndex = 14;
            this.btnViewReportObjects.Text = "View Report Items";
            this.btnViewReportObjects.UseVisualStyleBackColor = true;
            this.btnViewReportObjects.Click += new System.EventHandler(this.btnViewReportObjects_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ExportTitle);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 58);
            this.panel1.TabIndex = 13;
            // 
            // ExportTitle
            // 
            this.ExportTitle.AutoSize = true;
            this.ExportTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportTitle.Location = new System.Drawing.Point(3, 10);
            this.ExportTitle.Name = "ExportTitle";
            this.ExportTitle.Size = new System.Drawing.Size(186, 29);
            this.ExportTitle.TabIndex = 0;
            this.ExportTitle.Text = "Import Reports";
            // 
            // TargetURL
            // 
            this.TargetURL.Location = new System.Drawing.Point(94, 82);
            this.TargetURL.Name = "TargetURL";
            this.TargetURL.Size = new System.Drawing.Size(604, 22);
            this.TargetURL.TabIndex = 18;
            this.TargetURL.Text = "https://reports.dev.ua3.eslabs.ssn.hp.com/ReportServer/ReportService2010.asmx";
            // 
            // ExportBodyPanel
            // 
            this.ExportBodyPanel.Controls.Add(this.txtReportServerPath);
            this.ExportBodyPanel.Controls.Add(this.label6);
            this.ExportBodyPanel.Controls.Add(this.txtDownloadPath);
            this.ExportBodyPanel.Controls.Add(this.label5);
            this.ExportBodyPanel.Controls.Add(this.lblTestSuccess);
            this.ExportBodyPanel.Controls.Add(this.btnImportTestConnection);
            this.ExportBodyPanel.Controls.Add(this.groupBox1);
            this.ExportBodyPanel.Location = new System.Drawing.Point(3, 66);
            this.ExportBodyPanel.Name = "ExportBodyPanel";
            this.ExportBodyPanel.Size = new System.Drawing.Size(704, 498);
            this.ExportBodyPanel.TabIndex = 15;
            // 
            // txtReportServerPath
            // 
            this.txtReportServerPath.Location = new System.Drawing.Point(182, 96);
            this.txtReportServerPath.Name = "txtReportServerPath";
            this.txtReportServerPath.Size = new System.Drawing.Size(513, 22);
            this.txtReportServerPath.TabIndex = 16;
            this.txtReportServerPath.Text = "Tenant 3 - Customer A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Report server path";
            // 
            // ImportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnViewReportObjects);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TargetURL);
            this.Controls.Add(this.ExportBodyPanel);
            this.Name = "ImportControl";
            this.Size = new System.Drawing.Size(710, 611);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ExportBodyPanel.ResumeLayout(false);
            this.ExportBodyPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTestSuccess;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnImportTestConnection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDownloadPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnViewReportObjects;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ExportTitle;
        private System.Windows.Forms.TextBox TargetURL;
        private System.Windows.Forms.Panel ExportBodyPanel;
        private System.Windows.Forms.TextBox txtReportServerPath;
        private System.Windows.Forms.Label label6;
    }
}
