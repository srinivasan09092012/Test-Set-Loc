using System.Configuration;

namespace SSRSImportExportWizard
{
    partial class ViewDataSource
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
            this.TargetURL = new System.Windows.Forms.TextBox();
            this.lblTestSuccess = new System.Windows.Forms.Label();
            this.btnViewDataSourceObjects = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ViewDSTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDataSourceTestConnection = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.ExportBodyPanel = new System.Windows.Forms.Panel();
            this.txtRootPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ExportBodyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TargetURL
            // 
            this.TargetURL.Location = new System.Drawing.Point(89, 82);
            this.TargetURL.Name = "TargetURL";
            this.TargetURL.Size = new System.Drawing.Size(609, 22);
            this.TargetURL.TabIndex = 1;
            this.TargetURL.Text = ConfigurationManager.AppSettings["ReportURL"];
            // 
            // lblTestSuccess
            // 
            this.lblTestSuccess.AutoSize = true;
            this.lblTestSuccess.ForeColor = System.Drawing.Color.Green;
            this.lblTestSuccess.Location = new System.Drawing.Point(319, 213);
            this.lblTestSuccess.Name = "lblTestSuccess";
            this.lblTestSuccess.Size = new System.Drawing.Size(0, 17);
            this.lblTestSuccess.TabIndex = 9;
            // 
            // btnViewDataSourceObjects
            // 
            this.btnViewDataSourceObjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewDataSourceObjects.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewDataSourceObjects.Location = new System.Drawing.Point(574, 316);
            this.btnViewDataSourceObjects.Name = "btnViewDataSourceObjects";
            this.btnViewDataSourceObjects.Size = new System.Drawing.Size(136, 38);
            this.btnViewDataSourceObjects.TabIndex = 7;
            this.btnViewDataSourceObjects.TabStop = false;
            this.btnViewDataSourceObjects.Text = "View DataSource";
            this.btnViewDataSourceObjects.UseVisualStyleBackColor = true;
            this.btnViewDataSourceObjects.Click += new System.EventHandler(this.btnViewDataSourceObjects_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ViewDSTitle);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 58);
            this.panel1.TabIndex = 19;
            // 
            // ViewDSTitle
            // 
            this.ViewDSTitle.AutoSize = true;
            this.ViewDSTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewDSTitle.Location = new System.Drawing.Point(3, 10);
            this.ViewDSTitle.Name = "ViewDSTitle";
            this.ViewDSTitle.Size = new System.Drawing.Size(255, 29);
            this.ViewDSTitle.TabIndex = 0;
            this.ViewDSTitle.Text = "Report Data Sources";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 18);
            this.label1.TabIndex = 23;
            this.label1.Text = "Target";
            // 
            // btnDataSourceTestConnection
            // 
            this.btnDataSourceTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataSourceTestConnection.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataSourceTestConnection.Location = new System.Drawing.Point(182, 205);
            this.btnDataSourceTestConnection.Name = "btnDataSourceTestConnection";
            this.btnDataSourceTestConnection.Size = new System.Drawing.Size(121, 32);
            this.btnDataSourceTestConnection.TabIndex = 6;
            this.btnDataSourceTestConnection.Text = "Test connection";
            this.btnDataSourceTestConnection.UseVisualStyleBackColor = true;
            this.btnDataSourceTestConnection.Click += new System.EventHandler(this.btnDataSourceTestConnection_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(111, 62);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(183, 26);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.Text = string.Empty;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(111, 30);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(183, 26);
            this.txtUserName.TabIndex = 4;
            this.txtUserName.Text = ConfigurationManager.AppSettings["UserName"];
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 100);
            this.groupBox1.TabIndex = 3;
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
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(8, 319);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 17);
            this.lblError.TabIndex = 22;
            // 
            // ExportBodyPanel
            // 
            this.ExportBodyPanel.Controls.Add(this.txtRootPath);
            this.ExportBodyPanel.Controls.Add(this.label5);
            this.ExportBodyPanel.Controls.Add(this.lblTestSuccess);
            this.ExportBodyPanel.Controls.Add(this.btnDataSourceTestConnection);
            this.ExportBodyPanel.Controls.Add(this.groupBox1);
            this.ExportBodyPanel.Location = new System.Drawing.Point(3, 66);
            this.ExportBodyPanel.Name = "ExportBodyPanel";
            this.ExportBodyPanel.Size = new System.Drawing.Size(704, 244);
            this.ExportBodyPanel.TabIndex = 21;
            this.ExportBodyPanel.TabStop = true;
            // 
            // txtRootPath
            // 
            this.txtRootPath.Location = new System.Drawing.Point(165, 54);
            this.txtRootPath.Name = "txtRootPath";
            this.txtRootPath.Size = new System.Drawing.Size(530, 22);
            this.txtRootPath.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 18);
            this.label5.TabIndex = 15;
            this.label5.Text = "Server Directory*";
            // 
            // ViewDataSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TargetURL);
            this.Controls.Add(this.btnViewDataSourceObjects);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.ExportBodyPanel);
            this.Name = "ViewDataSource";
            this.Size = new System.Drawing.Size(710, 420);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ExportBodyPanel.ResumeLayout(false);
            this.ExportBodyPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TargetURL;
        private System.Windows.Forms.Label lblTestSuccess;
        private System.Windows.Forms.Button btnViewDataSourceObjects;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ViewDSTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDataSourceTestConnection;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Panel ExportBodyPanel;
        private System.Windows.Forms.TextBox txtRootPath;
        private System.Windows.Forms.Label label5;
    }
}
