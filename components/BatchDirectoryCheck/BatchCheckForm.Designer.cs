namespace BatchDirectoryCheck
{
    partial class BatchCheckForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBoxOracleDataSource = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbEnvironMent = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxDbms = new System.Windows.Forms.ComboBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelUserId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.labelService = new System.Windows.Forms.Label();
            this.comboBoxDataSource = new System.Windows.Forms.ComboBox();
            this.label37 = new System.Windows.Forms.Label();
            this.comboBoxTenants = new System.Windows.Forms.ComboBox();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.btnCreateFolders = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.labelModule = new System.Windows.Forms.Label();
            this.comboBoxModules = new System.Windows.Forms.ComboBox();
            this.dataGridAppSettings = new System.Windows.Forms.DataGridView();
            this.Module = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Application = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppSettingKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppSettingValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolderExists = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxOracleDataSource.SuspendLayout();
            this.groupBoxFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAppSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxOracleDataSource
            // 
            this.groupBoxOracleDataSource.Controls.Add(this.label2);
            this.groupBoxOracleDataSource.Controls.Add(this.cmbEnvironMent);
            this.groupBoxOracleDataSource.Controls.Add(this.label4);
            this.groupBoxOracleDataSource.Controls.Add(this.comboBoxDbms);
            this.groupBoxOracleDataSource.Controls.Add(this.buttonConnect);
            this.groupBoxOracleDataSource.Controls.Add(this.textBoxPassword);
            this.groupBoxOracleDataSource.Controls.Add(this.labelUserId);
            this.groupBoxOracleDataSource.Controls.Add(this.label1);
            this.groupBoxOracleDataSource.Controls.Add(this.textBoxUserId);
            this.groupBoxOracleDataSource.Controls.Add(this.labelService);
            this.groupBoxOracleDataSource.Controls.Add(this.comboBoxDataSource);
            this.groupBoxOracleDataSource.Location = new System.Drawing.Point(13, 14);
            this.groupBoxOracleDataSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxOracleDataSource.Name = "groupBoxOracleDataSource";
            this.groupBoxOracleDataSource.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxOracleDataSource.Size = new System.Drawing.Size(666, 202);
            this.groupBoxOracleDataSource.TabIndex = 24;
            this.groupBoxOracleDataSource.TabStop = false;
            this.groupBoxOracleDataSource.Text = "Tenant Schema";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 62;
            this.label2.Text = "Environment:";
            // 
            // cmbEnvironMent
            // 
            this.cmbEnvironMent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnvironMent.FormattingEnabled = true;
            this.cmbEnvironMent.Location = new System.Drawing.Point(180, 29);
            this.cmbEnvironMent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEnvironMent.Name = "cmbEnvironMent";
            this.cmbEnvironMent.Size = new System.Drawing.Size(467, 28);
            this.cmbEnvironMent.TabIndex = 27;
            this.cmbEnvironMent.SelectedIndexChanged += new System.EventHandler(this.cmbEnvironMent_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 88);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 20);
            this.label4.TabIndex = 61;
            this.label4.Text = "Database System:";
            // 
            // comboBoxDbms
            // 
            this.comboBoxDbms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDbms.FormattingEnabled = true;
            this.comboBoxDbms.Location = new System.Drawing.Point(180, 80);
            this.comboBoxDbms.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxDbms.Name = "comboBoxDbms";
            this.comboBoxDbms.Size = new System.Drawing.Size(170, 28);
            this.comboBoxDbms.TabIndex = 60;
            this.comboBoxDbms.SelectedIndexChanged += new System.EventHandler(this.comboBoxDbms_SelectedIndexChanged);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(539, 157);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(108, 35);
            this.buttonConnect.TabIndex = 30;
            this.buttonConnect.Text = "&Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(138, 166);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(388, 26);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            // 
            // labelUserId
            // 
            this.labelUserId.AutoSize = true;
            this.labelUserId.Location = new System.Drawing.Point(28, 130);
            this.labelUserId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserId.Name = "labelUserId";
            this.labelUserId.Size = new System.Drawing.Size(87, 20);
            this.labelUserId.TabIndex = 4;
            this.labelUserId.Text = "Username:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 166);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Password:";
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.Location = new System.Drawing.Point(138, 130);
            this.textBoxUserId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(388, 26);
            this.textBoxUserId.TabIndex = 2;
            this.textBoxUserId.TextChanged += new System.EventHandler(this.textBoxUserId_TextChanged);
            // 
            // labelService
            // 
            this.labelService.AutoSize = true;
            this.labelService.Location = new System.Drawing.Point(358, 88);
            this.labelService.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelService.Name = "labelService";
            this.labelService.Size = new System.Drawing.Size(65, 20);
            this.labelService.TabIndex = 1;
            this.labelService.Text = "Service:";
            // 
            // comboBoxDataSource
            // 
            this.comboBoxDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataSource.FormattingEnabled = true;
            this.comboBoxDataSource.Location = new System.Drawing.Point(436, 80);
            this.comboBoxDataSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxDataSource.Name = "comboBoxDataSource";
            this.comboBoxDataSource.Size = new System.Drawing.Size(211, 28);
            this.comboBoxDataSource.TabIndex = 0;
            this.comboBoxDataSource.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataSource_SelectedIndexChanged);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(8, 37);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(63, 20);
            this.label37.TabIndex = 54;
            this.label37.Text = "Tenant:";
            // 
            // comboBoxTenants
            // 
            this.comboBoxTenants.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTenants.FormattingEnabled = true;
            this.comboBoxTenants.Location = new System.Drawing.Point(164, 29);
            this.comboBoxTenants.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxTenants.Name = "comboBoxTenants";
            this.comboBoxTenants.Size = new System.Drawing.Size(412, 28);
            this.comboBoxTenants.TabIndex = 53;
            this.comboBoxTenants.SelectedIndexChanged += new System.EventHandler(this.comboBoxTenants_SelectedIndexChanged);
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Controls.Add(this.btnCreateFolders);
            this.groupBoxFilter.Controls.Add(this.buttonSearch);
            this.groupBoxFilter.Controls.Add(this.label37);
            this.groupBoxFilter.Controls.Add(this.comboBoxTenants);
            this.groupBoxFilter.Controls.Add(this.labelModule);
            this.groupBoxFilter.Controls.Add(this.comboBoxModules);
            this.groupBoxFilter.Location = new System.Drawing.Point(689, 14);
            this.groupBoxFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxFilter.Size = new System.Drawing.Size(838, 202);
            this.groupBoxFilter.TabIndex = 25;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "Filter";
            // 
            // btnCreateFolders
            // 
            this.btnCreateFolders.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCreateFolders.Location = new System.Drawing.Point(312, 143);
            this.btnCreateFolders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCreateFolders.Name = "btnCreateFolders";
            this.btnCreateFolders.Size = new System.Drawing.Size(231, 35);
            this.btnCreateFolders.TabIndex = 26;
            this.btnCreateFolders.Text = "Create Batch Folders";
            this.btnCreateFolders.UseVisualStyleBackColor = true;
            this.btnCreateFolders.Click += new System.EventHandler(this.btnCreateFolders_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(164, 143);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(108, 35);
            this.buttonSearch.TabIndex = 25;
            this.buttonSearch.Text = "&Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // labelModule
            // 
            this.labelModule.AutoSize = true;
            this.labelModule.Location = new System.Drawing.Point(8, 88);
            this.labelModule.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelModule.Name = "labelModule";
            this.labelModule.Size = new System.Drawing.Size(65, 20);
            this.labelModule.TabIndex = 6;
            this.labelModule.Text = "Module:";
            // 
            // comboBoxModules
            // 
            this.comboBoxModules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModules.FormattingEnabled = true;
            this.comboBoxModules.Location = new System.Drawing.Point(164, 80);
            this.comboBoxModules.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxModules.Name = "comboBoxModules";
            this.comboBoxModules.Size = new System.Drawing.Size(412, 28);
            this.comboBoxModules.TabIndex = 5;
            this.comboBoxModules.SelectedIndexChanged += new System.EventHandler(this.comboBoxModules_SelectedIndexChanged);
            // 
            // dataGridAppSettings
            // 
            this.dataGridAppSettings.AllowUserToAddRows = false;
            this.dataGridAppSettings.AllowUserToDeleteRows = false;
            this.dataGridAppSettings.AllowUserToOrderColumns = true;
            this.dataGridAppSettings.AllowUserToResizeRows = false;
            this.dataGridAppSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridAppSettings.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridAppSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAppSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Module,
            this.Application,
            this.AppSettingKey,
            this.AppSettingValue,
            this.FolderExists});
            this.dataGridAppSettings.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridAppSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dataGridAppSettings.Location = new System.Drawing.Point(24, 240);
            this.dataGridAppSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridAppSettings.MultiSelect = false;
            this.dataGridAppSettings.Name = "dataGridAppSettings";
            this.dataGridAppSettings.RowHeadersVisible = false;
            this.dataGridAppSettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridAppSettings.Size = new System.Drawing.Size(1498, 440);
            this.dataGridAppSettings.TabIndex = 32;
            // 
            // Module
            // 
            this.Module.HeaderText = "Module";
            this.Module.Name = "Module";
            this.Module.ReadOnly = true;
            // 
            // Application
            // 
            this.Application.HeaderText = "Application";
            this.Application.Name = "Application";
            this.Application.ReadOnly = true;
            this.Application.Width = 120;
            // 
            // AppSettingKey
            // 
            this.AppSettingKey.HeaderText = "Application Setting Key";
            this.AppSettingKey.Name = "AppSettingKey";
            this.AppSettingKey.ReadOnly = true;
            this.AppSettingKey.Width = 150;
            // 
            // AppSettingValue
            // 
            this.AppSettingValue.HeaderText = "Value";
            this.AppSettingValue.Name = "AppSettingValue";
            this.AppSettingValue.ReadOnly = true;
            this.AppSettingValue.Width = 550;
            // 
            // FolderExists
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FolderExists.DefaultCellStyle = dataGridViewCellStyle1;
            this.FolderExists.HeaderText = "Folder Available";
            this.FolderExists.Name = "FolderExists";
            this.FolderExists.ReadOnly = true;
            this.FolderExists.Width = 150;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnCreateFolders;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1535, 741);
            this.Controls.Add(this.dataGridAppSettings);
            this.Controls.Add(this.groupBoxFilter);
            this.Controls.Add(this.groupBoxOracleDataSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BatchDirectoryCheck";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxOracleDataSource.ResumeLayout(false);
            this.groupBoxOracleDataSource.PerformLayout();
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAppSettings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxOracleDataSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxDbms;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ComboBox comboBoxTenants;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelUserId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.Label labelService;
        private System.Windows.Forms.ComboBox comboBoxDataSource;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label labelModule;
        private System.Windows.Forms.ComboBox comboBoxModules;
        private System.Windows.Forms.DataGridView dataGridAppSettings;
        private System.Windows.Forms.Button btnCreateFolders;
        private System.Windows.Forms.ComboBox cmbEnvironMent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Module;
        private System.Windows.Forms.DataGridViewTextBoxColumn Application;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppSettingKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppSettingValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolderExists;
    }
}

