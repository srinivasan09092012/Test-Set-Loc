namespace RightsUX
{
    partial class frmRightsMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRightsMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSchema = new System.Windows.Forms.Label();
            this.txtSchema = new System.Windows.Forms.TextBox();
            this.rdoSQL = new System.Windows.Forms.RadioButton();
            this.rdoOracle = new System.Windows.Forms.RadioButton();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblSqlServer = new System.Windows.Forms.Label();
            this.cboDatabaseServer = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboTenant = new System.Windows.Forms.ComboBox();
            this.lblTenant = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.dlgFolderSelector = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSchema);
            this.groupBox1.Controls.Add(this.txtSchema);
            this.groupBox1.Controls.Add(this.rdoSQL);
            this.groupBox1.Controls.Add(this.rdoOracle);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.lblUserName);
            this.groupBox1.Controls.Add(this.lblSqlServer);
            this.groupBox1.Controls.Add(this.cboDatabaseServer);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 173);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database";
            // 
            // lblSchema
            // 
            this.lblSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSchema.Location = new System.Drawing.Point(6, 72);
            this.lblSchema.Name = "lblSchema";
            this.lblSchema.Size = new System.Drawing.Size(85, 15);
            this.lblSchema.TabIndex = 17;
            this.lblSchema.Text = "Initial Catalog";
            this.lblSchema.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSchema
            // 
            this.txtSchema.Location = new System.Drawing.Point(97, 70);
            this.txtSchema.Name = "txtSchema";
            this.txtSchema.Size = new System.Drawing.Size(158, 20);
            this.txtSchema.TabIndex = 3;
            this.txtSchema.TextChanged += new System.EventHandler(this.All_TextChanged);
            // 
            // rdoSQL
            // 
            this.rdoSQL.AutoSize = true;
            this.rdoSQL.Location = new System.Drawing.Point(180, 12);
            this.rdoSQL.Name = "rdoSQL";
            this.rdoSQL.Size = new System.Drawing.Size(87, 19);
            this.rdoSQL.TabIndex = 1;
            this.rdoSQL.TabStop = true;
            this.rdoSQL.Text = "SQL Server";
            this.rdoSQL.UseVisualStyleBackColor = true;
            this.rdoSQL.CheckedChanged += new System.EventHandler(this.Datasource_CheckedChanged);
            // 
            // rdoOracle
            // 
            this.rdoOracle.AutoSize = true;
            this.rdoOracle.Location = new System.Drawing.Point(97, 12);
            this.rdoOracle.Name = "rdoOracle";
            this.rdoOracle.Size = new System.Drawing.Size(61, 19);
            this.rdoOracle.TabIndex = 0;
            this.rdoOracle.TabStop = true;
            this.rdoOracle.Text = "Oracle";
            this.rdoOracle.UseVisualStyleBackColor = true;
            this.rdoOracle.CheckedChanged += new System.EventHandler(this.Datasource_CheckedChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(281, 128);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 30);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(97, 137);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(158, 21);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.All_TextChanged);
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(97, 103);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(158, 21);
            this.txtUserName.TabIndex = 4;
            this.txtUserName.TextChanged += new System.EventHandler(this.All_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.Location = new System.Drawing.Point(6, 141);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(85, 15);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Password";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.Location = new System.Drawing.Point(6, 107);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(85, 15);
            this.lblUserName.TabIndex = 9;
            this.lblUserName.Text = "UserName";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSqlServer
            // 
            this.lblSqlServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSqlServer.Location = new System.Drawing.Point(6, 41);
            this.lblSqlServer.Name = "lblSqlServer";
            this.lblSqlServer.Size = new System.Drawing.Size(85, 15);
            this.lblSqlServer.TabIndex = 7;
            this.lblSqlServer.Text = "Server";
            this.lblSqlServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDatabaseServer
            // 
            this.cboDatabaseServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDatabaseServer.FormattingEnabled = true;
            this.cboDatabaseServer.Location = new System.Drawing.Point(97, 37);
            this.cboDatabaseServer.Name = "cboDatabaseServer";
            this.cboDatabaseServer.Size = new System.Drawing.Size(259, 23);
            this.cboDatabaseServer.TabIndex = 2;
            this.cboDatabaseServer.SelectedIndexChanged += new System.EventHandler(this.All_SelectedIndexChanged);
            this.cboDatabaseServer.TextChanged += new System.EventHandler(this.All_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboTenant);
            this.groupBox2.Controls.Add(this.lblTenant);
            this.groupBox2.Location = new System.Drawing.Point(12, 201);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(374, 60);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report Settings";
            // 
            // cboTenant
            // 
            this.cboTenant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTenant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTenant.FormattingEnabled = true;
            this.cboTenant.Location = new System.Drawing.Point(97, 22);
            this.cboTenant.Name = "cboTenant";
            this.cboTenant.Size = new System.Drawing.Size(259, 23);
            this.cboTenant.Sorted = true;
            this.cboTenant.TabIndex = 7;
            this.cboTenant.SelectedIndexChanged += new System.EventHandler(this.All_SelectedIndexChanged);
            this.cboTenant.TextChanged += new System.EventHandler(this.AnyControl_TextChanged);
            // 
            // lblTenant
            // 
            this.lblTenant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTenant.Location = new System.Drawing.Point(6, 26);
            this.lblTenant.Name = "lblTenant";
            this.lblTenant.Size = new System.Drawing.Size(85, 15);
            this.lblTenant.TabIndex = 0;
            this.lblTenant.Text = "Tenant";
            this.lblTenant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSelectFolder);
            this.groupBox3.Controls.Add(this.lblOutputPath);
            this.groupBox3.Controls.Add(this.txtOutputPath);
            this.groupBox3.Controls.Add(this.txtFileName);
            this.groupBox3.Controls.Add(this.lblFileName);
            this.groupBox3.Location = new System.Drawing.Point(12, 276);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(373, 96);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Image = global::RightsUX.Properties.Resources.DocumentsFolder_16x;
            this.btnSelectFolder.Location = new System.Drawing.Point(329, 54);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(27, 27);
            this.btnSelectFolder.TabIndex = 10;
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.BtnSelectFolder_Click);
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutputPath.Location = new System.Drawing.Point(6, 60);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(85, 15);
            this.lblOutputPath.TabIndex = 8;
            this.lblOutputPath.Text = "Save to Path";
            this.lblOutputPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputPath.Location = new System.Drawing.Point(97, 56);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(226, 21);
            this.txtOutputPath.TabIndex = 9;
            this.txtOutputPath.TextChanged += new System.EventHandler(this.All_TextChanged);
            // 
            // txtFileName
            // 
            this.txtFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(97, 23);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(259, 21);
            this.txtFileName.TabIndex = 8;
            this.txtFileName.TextChanged += new System.EventHandler(this.All_TextChanged);
            // 
            // lblFileName
            // 
            this.lblFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileName.Location = new System.Drawing.Point(6, 27);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(85, 15);
            this.lblFileName.TabIndex = 5;
            this.lblFileName.Text = "File Name";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.Enabled = false;
            this.btnCreateReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateReport.Location = new System.Drawing.Point(279, 383);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(107, 33);
            this.btnCreateReport.TabIndex = 11;
            this.btnCreateReport.Text = "Create Report";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.BtnCreateReport_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(18, 391);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(255, 18);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.113208F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblVersion.Location = new System.Drawing.Point(189, 3);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(198, 10);
            this.lblVersion.TabIndex = 13;
            this.lblVersion.Text = "Version";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmRightsMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(396, 426);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCreateReport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRightsMain";
            this.Text = "Security Report Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRightsMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmRightsMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblSqlServer;
        private System.Windows.Forms.ComboBox cboDatabaseServer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTenant;
        private System.Windows.Forms.ComboBox cboTenant;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderSelector;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.Label lblSchema;
        private System.Windows.Forms.TextBox txtSchema;
        private System.Windows.Forms.RadioButton rdoSQL;
        private System.Windows.Forms.RadioButton rdoOracle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblVersion;
    }
}

