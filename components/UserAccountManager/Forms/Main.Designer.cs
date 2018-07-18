namespace UserAccountManager.Forms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserSearchGroupBox = new System.Windows.Forms.GroupBox();
            this.PhoneNumberTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.UserGroupsComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.UserSearchButton = new System.Windows.Forms.Button();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LastNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FirstNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EnvComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.UserResultsGroupBox = new System.Windows.Forms.GroupBox();
            this.UserResultActionsGroupBox = new System.Windows.Forms.GroupBox();
            this.UserResetPasswordButton = new System.Windows.Forms.Button();
            this.UserDeleteButton = new System.Windows.Forms.Button();
            this.UserCloneButton = new System.Windows.Forms.Button();
            this.UserResultsDataGridView = new System.Windows.Forms.DataGridView();
            this.SelColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.UserAddButton = new System.Windows.Forms.Button();
            this.UserOtherActionsGroupBox = new System.Windows.Forms.GroupBox();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.UserAccountTab = new System.Windows.Forms.TabPage();
            this.LockedOutCheckbox = new System.Windows.Forms.CheckBox();
            this.AccountExpiredCheckbox = new System.Windows.Forms.CheckBox();
            this.UserNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiddleNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisplayNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhoneNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserAccountsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            this.UserSearchGroupBox.SuspendLayout();
            this.UserResultsGroupBox.SuspendLayout();
            this.UserResultActionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserResultsDataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.UserOtherActionsGroupBox.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.UserAccountTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserAccountsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1024, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(89, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // UserSearchGroupBox
            // 
            this.UserSearchGroupBox.Controls.Add(this.AccountExpiredCheckbox);
            this.UserSearchGroupBox.Controls.Add(this.LockedOutCheckbox);
            this.UserSearchGroupBox.Controls.Add(this.PhoneNumberTextBox);
            this.UserSearchGroupBox.Controls.Add(this.label7);
            this.UserSearchGroupBox.Controls.Add(this.UserGroupsComboBox);
            this.UserSearchGroupBox.Controls.Add(this.label6);
            this.UserSearchGroupBox.Controls.Add(this.UserSearchButton);
            this.UserSearchGroupBox.Controls.Add(this.EmailTextBox);
            this.UserSearchGroupBox.Controls.Add(this.label4);
            this.UserSearchGroupBox.Controls.Add(this.LastNameTextBox);
            this.UserSearchGroupBox.Controls.Add(this.label3);
            this.UserSearchGroupBox.Controls.Add(this.FirstNameTextBox);
            this.UserSearchGroupBox.Controls.Add(this.label2);
            this.UserSearchGroupBox.Controls.Add(this.UserNameTextBox);
            this.UserSearchGroupBox.Controls.Add(this.label1);
            this.UserSearchGroupBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UserSearchGroupBox.Location = new System.Drawing.Point(6, 6);
            this.UserSearchGroupBox.Name = "UserSearchGroupBox";
            this.UserSearchGroupBox.Size = new System.Drawing.Size(578, 168);
            this.UserSearchGroupBox.TabIndex = 0;
            this.UserSearchGroupBox.TabStop = false;
            this.UserSearchGroupBox.Text = "Search";
            // 
            // PhoneNumberTextBox
            // 
            this.PhoneNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PhoneNumberTextBox.Location = new System.Drawing.Point(94, 84);
            this.PhoneNumberTextBox.Mask = "(999) 000-0000";
            this.PhoneNumberTextBox.Name = "PhoneNumberTextBox";
            this.PhoneNumberTextBox.Size = new System.Drawing.Size(137, 20);
            this.PhoneNumberTextBox.TabIndex = 9;
            this.PhoneNumberTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.PhoneNumberTextBox.TextChanged += new System.EventHandler(this.PhoneNumberTextBox_TextChanged);
            this.PhoneNumberTextBox.Leave += new System.EventHandler(this.PhoneNumberTextBox_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Phone Number";
            // 
            // UserGroupsComboBox
            // 
            this.UserGroupsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UserGroupsComboBox.Location = new System.Drawing.Point(320, 31);
            this.UserGroupsComboBox.Name = "UserGroupsComboBox";
            this.UserGroupsComboBox.Size = new System.Drawing.Size(149, 21);
            this.UserGroupsComboBox.TabIndex = 3;
            this.UserGroupsComboBox.SelectedIndexChanged += new System.EventHandler(this.UserGroupsComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(250, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Group";
            // 
            // UserSearchButton
            // 
            this.UserSearchButton.Enabled = false;
            this.UserSearchButton.Location = new System.Drawing.Point(497, 136);
            this.UserSearchButton.Name = "UserSearchButton";
            this.UserSearchButton.Size = new System.Drawing.Size(75, 23);
            this.UserSearchButton.TabIndex = 15;
            this.UserSearchButton.Text = "&Search";
            this.UserSearchButton.UseVisualStyleBackColor = true;
            this.UserSearchButton.Click += new System.EventHandler(this.UserSearchButton_Click);
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmailTextBox.Location = new System.Drawing.Point(94, 110);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Size = new System.Drawing.Size(375, 20);
            this.EmailTextBox.TabIndex = 11;
            this.EmailTextBox.TextChanged += new System.EventHandler(this.EmailTextBox_TextChanged);
            this.EmailTextBox.Leave += new System.EventHandler(this.EmailTextBox_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Email";
            // 
            // LastNameTextBox
            // 
            this.LastNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastNameTextBox.Location = new System.Drawing.Point(320, 58);
            this.LastNameTextBox.Name = "LastNameTextBox";
            this.LastNameTextBox.Size = new System.Drawing.Size(149, 20);
            this.LastNameTextBox.TabIndex = 7;
            this.LastNameTextBox.TextChanged += new System.EventHandler(this.LastNameTextBox_TextChanged);
            this.LastNameTextBox.Leave += new System.EventHandler(this.LastNameTextBox_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Last Name";
            // 
            // FirstNameTextBox
            // 
            this.FirstNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FirstNameTextBox.Location = new System.Drawing.Point(94, 58);
            this.FirstNameTextBox.Name = "FirstNameTextBox";
            this.FirstNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.FirstNameTextBox.TabIndex = 5;
            this.FirstNameTextBox.TextChanged += new System.EventHandler(this.FirstNameTextBox_TextChanged);
            this.FirstNameTextBox.Leave += new System.EventHandler(this.FirstNameTextBox_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "First Name";
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserNameTextBox.Location = new System.Drawing.Point(94, 31);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.UserNameTextBox.TabIndex = 1;
            this.UserNameTextBox.TextChanged += new System.EventHandler(this.UserNameTextBox_TextChanged);
            this.UserNameTextBox.Leave += new System.EventHandler(this.UserNameTextBox_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "UserName";
            // 
            // EnvComboBox
            // 
            this.EnvComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EnvComboBox.Location = new System.Drawing.Point(85, 44);
            this.EnvComboBox.Name = "EnvComboBox";
            this.EnvComboBox.Size = new System.Drawing.Size(168, 21);
            this.EnvComboBox.TabIndex = 2;
            this.EnvComboBox.SelectedIndexChanged += new System.EventHandler(this.EnvComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(13, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Environment";
            // 
            // UserResultsGroupBox
            // 
            this.UserResultsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserResultsGroupBox.Controls.Add(this.UserResultActionsGroupBox);
            this.UserResultsGroupBox.Controls.Add(this.UserResultsDataGridView);
            this.UserResultsGroupBox.Enabled = false;
            this.UserResultsGroupBox.Location = new System.Drawing.Point(6, 180);
            this.UserResultsGroupBox.Name = "UserResultsGroupBox";
            this.UserResultsGroupBox.Size = new System.Drawing.Size(979, 363);
            this.UserResultsGroupBox.TabIndex = 1;
            this.UserResultsGroupBox.TabStop = false;
            this.UserResultsGroupBox.Text = "Results";
            // 
            // UserResultActionsGroupBox
            // 
            this.UserResultActionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UserResultActionsGroupBox.Controls.Add(this.UserResetPasswordButton);
            this.UserResultActionsGroupBox.Controls.Add(this.UserDeleteButton);
            this.UserResultActionsGroupBox.Controls.Add(this.UserCloneButton);
            this.UserResultActionsGroupBox.Location = new System.Drawing.Point(616, 305);
            this.UserResultActionsGroupBox.Name = "UserResultActionsGroupBox";
            this.UserResultActionsGroupBox.Size = new System.Drawing.Size(364, 56);
            this.UserResultActionsGroupBox.TabIndex = 1;
            this.UserResultActionsGroupBox.TabStop = false;
            this.UserResultActionsGroupBox.Text = "Grid Actions";
            // 
            // UserResetPasswordButton
            // 
            this.UserResetPasswordButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UserResetPasswordButton.Location = new System.Drawing.Point(16, 19);
            this.UserResetPasswordButton.Name = "UserResetPasswordButton";
            this.UserResetPasswordButton.Size = new System.Drawing.Size(100, 23);
            this.UserResetPasswordButton.TabIndex = 0;
            this.UserResetPasswordButton.Text = "&Reset Password";
            this.UserResetPasswordButton.UseVisualStyleBackColor = true;
            this.UserResetPasswordButton.Click += new System.EventHandler(this.UserResetPasswordButton_Click);
            // 
            // UserDeleteButton
            // 
            this.UserDeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UserDeleteButton.Location = new System.Drawing.Point(122, 19);
            this.UserDeleteButton.Name = "UserDeleteButton";
            this.UserDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.UserDeleteButton.TabIndex = 1;
            this.UserDeleteButton.Text = "&Delete";
            this.UserDeleteButton.UseVisualStyleBackColor = true;
            this.UserDeleteButton.Click += new System.EventHandler(this.UserDeleteButton_Click);
            // 
            // UserCloneButton
            // 
            this.UserCloneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UserCloneButton.Location = new System.Drawing.Point(203, 19);
            this.UserCloneButton.Name = "UserCloneButton";
            this.UserCloneButton.Size = new System.Drawing.Size(144, 23);
            this.UserCloneButton.TabIndex = 2;
            this.UserCloneButton.Text = "&Clone to Environment";
            this.UserCloneButton.UseVisualStyleBackColor = true;
            this.UserCloneButton.Click += new System.EventHandler(this.UserCloneButton_Click);
            // 
            // UserResultsDataGridView
            // 
            this.UserResultsDataGridView.AllowUserToAddRows = false;
            this.UserResultsDataGridView.AllowUserToResizeRows = false;
            this.UserResultsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserResultsDataGridView.AutoGenerateColumns = false;
            this.UserResultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserResultsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelColumn,
            this.UserNameColumn,
            this.FirstNameColumn,
            this.MiddleNameColumn,
            this.LastNameColumn,
            this.DisplayNameColumn,
            this.EmailAddressColumn,
            this.PhoneNumberColumn});
            this.UserResultsDataGridView.DataSource = this.UserAccountsBindingSource;
            this.UserResultsDataGridView.Location = new System.Drawing.Point(3, 16);
            this.UserResultsDataGridView.MultiSelect = false;
            this.UserResultsDataGridView.Name = "UserResultsDataGridView";
            this.UserResultsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.UserResultsDataGridView.Size = new System.Drawing.Size(973, 283);
            this.UserResultsDataGridView.TabIndex = 0;
            this.UserResultsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UserResultsDataGridView_CellContentClick);
            this.UserResultsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UserResultsDataGridView_CellDoubleClick);
            this.UserResultsDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.UserResultsDataGridView_UserDeletingRow);
            // 
            // SelColumn
            // 
            this.SelColumn.FalseValue = "false";
            this.SelColumn.Frozen = true;
            this.SelColumn.HeaderText = "Sel";
            this.SelColumn.Name = "SelColumn";
            this.SelColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SelColumn.TrueValue = "true";
            this.SelColumn.Width = 35;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 668);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1024, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "StatusStrip";
            // 
            // StatusStripLabel
            // 
            this.StatusStripLabel.Name = "StatusStripLabel";
            this.StatusStripLabel.Size = new System.Drawing.Size(132, 17);
            this.StatusStripLabel.Text = "Initializing application...";
            // 
            // UserAddButton
            // 
            this.UserAddButton.Location = new System.Drawing.Point(17, 31);
            this.UserAddButton.Name = "UserAddButton";
            this.UserAddButton.Size = new System.Drawing.Size(125, 23);
            this.UserAddButton.TabIndex = 10;
            this.UserAddButton.Text = "&Add New Account";
            this.UserAddButton.UseVisualStyleBackColor = true;
            this.UserAddButton.Click += new System.EventHandler(this.UserAddButton_Click);
            // 
            // UserOtherActionsGroupBox
            // 
            this.UserOtherActionsGroupBox.Controls.Add(this.UserAddButton);
            this.UserOtherActionsGroupBox.Location = new System.Drawing.Point(590, 6);
            this.UserOtherActionsGroupBox.Name = "UserOtherActionsGroupBox";
            this.UserOtherActionsGroupBox.Size = new System.Drawing.Size(395, 168);
            this.UserOtherActionsGroupBox.TabIndex = 1;
            this.UserOtherActionsGroupBox.TabStop = false;
            this.UserOtherActionsGroupBox.Text = "User Actions";
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.UserAccountTab);
            this.TabControl.Enabled = false;
            this.TabControl.Location = new System.Drawing.Point(12, 80);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1000, 575);
            this.TabControl.TabIndex = 3;
            // 
            // UserAccountTab
            // 
            this.UserAccountTab.Controls.Add(this.UserSearchGroupBox);
            this.UserAccountTab.Controls.Add(this.UserOtherActionsGroupBox);
            this.UserAccountTab.Controls.Add(this.UserResultsGroupBox);
            this.UserAccountTab.Location = new System.Drawing.Point(4, 22);
            this.UserAccountTab.Name = "UserAccountTab";
            this.UserAccountTab.Padding = new System.Windows.Forms.Padding(3);
            this.UserAccountTab.Size = new System.Drawing.Size(992, 549);
            this.UserAccountTab.TabIndex = 0;
            this.UserAccountTab.Text = "User Accounts";
            this.UserAccountTab.UseVisualStyleBackColor = true;
            // 
            // LockedOutCheckbox
            // 
            this.LockedOutCheckbox.AutoSize = true;
            this.LockedOutCheckbox.Location = new System.Drawing.Point(95, 136);
            this.LockedOutCheckbox.Name = "LockedOutCheckbox";
            this.LockedOutCheckbox.Size = new System.Drawing.Size(82, 17);
            this.LockedOutCheckbox.TabIndex = 13;
            this.LockedOutCheckbox.Text = "Locked Out";
            this.LockedOutCheckbox.UseVisualStyleBackColor = true;
            this.LockedOutCheckbox.CheckedChanged += new System.EventHandler(this.LockedOutCheckbox_CheckedChanged);
            // 
            // AccountExpiredCheckbox
            // 
            this.AccountExpiredCheckbox.AutoSize = true;
            this.AccountExpiredCheckbox.Location = new System.Drawing.Point(204, 136);
            this.AccountExpiredCheckbox.Name = "AccountExpiredCheckbox";
            this.AccountExpiredCheckbox.Size = new System.Drawing.Size(104, 17);
            this.AccountExpiredCheckbox.TabIndex = 14;
            this.AccountExpiredCheckbox.Text = "Account Expired";
            this.AccountExpiredCheckbox.UseVisualStyleBackColor = true;
            this.AccountExpiredCheckbox.CheckedChanged += new System.EventHandler(this.AccountExpiredCheckbox_CheckedChanged);
            // 
            // UserNameColumn
            // 
            this.UserNameColumn.DataPropertyName = "UserName";
            this.UserNameColumn.Frozen = true;
            this.UserNameColumn.HeaderText = "UserName";
            this.UserNameColumn.Name = "UserNameColumn";
            this.UserNameColumn.ReadOnly = true;
            // 
            // FirstNameColumn
            // 
            this.FirstNameColumn.DataPropertyName = "FirstName";
            this.FirstNameColumn.HeaderText = "First Name";
            this.FirstNameColumn.Name = "FirstNameColumn";
            this.FirstNameColumn.ReadOnly = true;
            // 
            // MiddleNameColumn
            // 
            this.MiddleNameColumn.DataPropertyName = "MiddleName";
            this.MiddleNameColumn.HeaderText = "Middle Name";
            this.MiddleNameColumn.Name = "MiddleNameColumn";
            this.MiddleNameColumn.ReadOnly = true;
            // 
            // LastNameColumn
            // 
            this.LastNameColumn.DataPropertyName = "LastName";
            this.LastNameColumn.HeaderText = "Last Name";
            this.LastNameColumn.Name = "LastNameColumn";
            this.LastNameColumn.ReadOnly = true;
            // 
            // DisplayNameColumn
            // 
            this.DisplayNameColumn.DataPropertyName = "DisplayName";
            this.DisplayNameColumn.HeaderText = "Display Name";
            this.DisplayNameColumn.Name = "DisplayNameColumn";
            this.DisplayNameColumn.ReadOnly = true;
            this.DisplayNameColumn.Width = 150;
            // 
            // EmailAddressColumn
            // 
            this.EmailAddressColumn.DataPropertyName = "EmailAddress";
            this.EmailAddressColumn.HeaderText = "Email Address";
            this.EmailAddressColumn.Name = "EmailAddressColumn";
            this.EmailAddressColumn.ReadOnly = true;
            this.EmailAddressColumn.Width = 200;
            // 
            // PhoneNumberColumn
            // 
            this.PhoneNumberColumn.DataPropertyName = "PhoneNumber";
            dataGridViewCellStyle1.Format = "(###) ###-####";
            dataGridViewCellStyle1.NullValue = null;
            this.PhoneNumberColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.PhoneNumberColumn.HeaderText = "Phone Number";
            this.PhoneNumberColumn.Name = "PhoneNumberColumn";
            this.PhoneNumberColumn.ReadOnly = true;
            this.PhoneNumberColumn.Width = 125;
            // 
            // UserAccountsBindingSource
            // 
            this.UserAccountsBindingSource.DataSource = typeof(HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.UserIdentity);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 690);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.EnvComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1040, 660);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Account Manager";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.UserSearchGroupBox.ResumeLayout(false);
            this.UserSearchGroupBox.PerformLayout();
            this.UserResultsGroupBox.ResumeLayout(false);
            this.UserResultActionsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UserResultsDataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.UserOtherActionsGroupBox.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.UserAccountTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UserAccountsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox UserSearchGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UserSearchButton;
        private System.Windows.Forms.TextBox EmailTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LastNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FirstNameTextBox;
        private System.Windows.Forms.ComboBox EnvComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox UserResultsGroupBox;
        private System.Windows.Forms.BindingSource UserAccountsBindingSource;
        private System.Windows.Forms.DataGridView UserResultsDataGridView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusStripLabel;
        private System.Windows.Forms.Button UserAddButton;
        private System.Windows.Forms.GroupBox UserOtherActionsGroupBox;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage UserAccountTab;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox UserGroupsComboBox;
        private System.Windows.Forms.Button UserCloneButton;
        private System.Windows.Forms.Button UserDeleteButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiddleNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailAddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhoneNumberColumn;
        private System.Windows.Forms.GroupBox UserResultActionsGroupBox;
        private System.Windows.Forms.Button UserResetPasswordButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox PhoneNumberTextBox;
        private System.Windows.Forms.CheckBox AccountExpiredCheckbox;
        private System.Windows.Forms.CheckBox LockedOutCheckbox;
    }
}

