using System.Configuration;

namespace SSRSImportExportWizard
{
    partial class ExportControl
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
            this.ExportTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.ExportBodyPanel = new System.Windows.Forms.Panel();
            this.btnExportFolderBrowser = new System.Windows.Forms.Button();
            this.txtDownloadPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTestSuccess = new System.Windows.Forms.Label();
            this.btnExportTestConnection = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sourceURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.ExportBodyPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExportTitle
            // 
            this.ExportTitle.AutoSize = true;
            this.ExportTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportTitle.Location = new System.Drawing.Point(3, 10);
            this.ExportTitle.Name = "ExportTitle";
            this.ExportTitle.Size = new System.Drawing.Size(187, 29);
            this.ExportTitle.TabIndex = 0;
            this.ExportTitle.Text = "Export Reports";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ExportTitle);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 58);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(571, 367);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 38);
            this.button1.TabIndex = 8;
            this.button1.TabStop = false;
            this.button1.Text = "View Reports";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExportBodyPanel
            // 
            this.ExportBodyPanel.Controls.Add(this.btnExportFolderBrowser);
            this.ExportBodyPanel.Controls.Add(this.txtDownloadPath);
            this.ExportBodyPanel.Controls.Add(this.label5);
            this.ExportBodyPanel.Controls.Add(this.lblTestSuccess);
            this.ExportBodyPanel.Controls.Add(this.btnExportTestConnection);
            this.ExportBodyPanel.Controls.Add(this.groupBox1);
            this.ExportBodyPanel.Location = new System.Drawing.Point(3, 66);
            this.ExportBodyPanel.Name = "ExportBodyPanel";
            this.ExportBodyPanel.Size = new System.Drawing.Size(704, 295);
            this.ExportBodyPanel.TabIndex = 8;
            this.ExportBodyPanel.TabStop = true;
            // 
            // btnExportFolderBrowser
            // 
            this.btnExportFolderBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportFolderBrowser.Font = new System.Drawing.Font("Calibri", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportFolderBrowser.Location = new System.Drawing.Point(590, 54);
            this.btnExportFolderBrowser.Name = "btnExportFolderBrowser";
            this.btnExportFolderBrowser.Size = new System.Drawing.Size(64, 27);
            this.btnExportFolderBrowser.TabIndex = 3;
            this.btnExportFolderBrowser.Text = "Browse";
            this.btnExportFolderBrowser.UseVisualStyleBackColor = true;
            this.btnExportFolderBrowser.Click += new System.EventHandler(this.btnExportFolderBrowser_Click);
            // 
            // txtDownloadPath
            // 
            this.txtDownloadPath.Location = new System.Drawing.Point(131, 56);
            this.txtDownloadPath.Name = "txtDownloadPath";
            this.txtDownloadPath.Size = new System.Drawing.Size(456, 22);
            this.txtDownloadPath.TabIndex = 2;
            this.txtDownloadPath.Text = ConfigurationManager.AppSettings["DownloadPath"];
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Download Path";
            // 
            // lblTestSuccess
            // 
            this.lblTestSuccess.AutoSize = true;
            this.lblTestSuccess.ForeColor = System.Drawing.Color.Green;
            this.lblTestSuccess.Location = new System.Drawing.Point(326, 231);
            this.lblTestSuccess.Name = "lblTestSuccess";
            this.lblTestSuccess.Size = new System.Drawing.Size(0, 17);
            this.lblTestSuccess.TabIndex = 9;
            // 
            // btnExportTestConnection
            // 
            this.btnExportTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportTestConnection.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportTestConnection.Location = new System.Drawing.Point(188, 223);
            this.btnExportTestConnection.Name = "btnExportTestConnection";
            this.btnExportTestConnection.Size = new System.Drawing.Size(120, 32);
            this.btnExportTestConnection.TabIndex = 7;
            this.btnExportTestConnection.Text = "Test connection";
            this.btnExportTestConnection.UseVisualStyleBackColor = true;
            this.btnExportTestConnection.Click += new System.EventHandler(this.btnExportTestConnection_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(14, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Authentication";
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
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Text = string.Empty;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(111, 30);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(183, 26);
            this.txtUserName.TabIndex = 5;
            this.txtUserName.Text = ConfigurationManager.AppSettings["UserName"];
            this.txtUserName.Click += new System.EventHandler(this.txtUserName_TextChanged);
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
            // sourceURL
            // 
            this.sourceURL.Location = new System.Drawing.Point(134, 82);
            this.sourceURL.Name = "sourceURL";
            this.sourceURL.Size = new System.Drawing.Size(564, 22);
            this.sourceURL.TabIndex = 1;
            this.sourceURL.Text = ConfigurationManager.AppSettings["ReportURL"];
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Source*";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(8, 372);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 17);
            this.lblError.TabIndex = 9;
            // 
            // ExportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.sourceURL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExportBodyPanel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "ExportControl";
            this.Size = new System.Drawing.Size(710, 420);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ExportBodyPanel.ResumeLayout(false);
            this.ExportBodyPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ExportTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel ExportBodyPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sourceURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportTestConnection;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblTestSuccess;
        private System.Windows.Forms.TextBox txtDownloadPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExportFolderBrowser;
    }
}
