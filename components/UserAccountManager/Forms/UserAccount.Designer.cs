namespace UserAccountManager.Forms
{
    partial class UserAccountForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IdenttyGroupBox = new System.Windows.Forms.GroupBox();
            this.AccountExpirationDateTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ExpiredCheckbox = new System.Windows.Forms.CheckBox();
            this.LockedCheckbox = new System.Windows.Forms.CheckBox();
            this.PhoneTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DisplayNameTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MiddleNameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LastNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FirstNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LocaleTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SecurityGroupBox = new System.Windows.Forms.GroupBox();
            this.CheckAvailableButton = new System.Windows.Forms.Button();
            this.ConfirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.ConfirmPasswordLabel = new System.Windows.Forms.Label();
            this.GroupsListBox = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.ProfileGroupBox = new System.Windows.Forms.GroupBox();
            this.VOSTagsDataGridView = new System.Windows.Forms.DataGridView();
            this.GeneralIdTtextBox = new System.Windows.Forms.TextBox();
            this.GeneralIdLabel = new System.Windows.Forms.Label();
            this.VOSTagsLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CreateUserProfileCheckBox = new System.Windows.Forms.CheckBox();
            this.RegQualifiersGroupBox = new System.Windows.Forms.GroupBox();
            this.RegQualifiersDataGridView = new System.Windows.Forms.DataGridView();
            this.keyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegQualifiersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.userVOSTagIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.effectiveDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VOSTagsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            this.IdenttyGroupBox.SuspendLayout();
            this.SecurityGroupBox.SuspendLayout();
            this.ProfileGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VOSTagsDataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.RegQualifiersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RegQualifiersDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegQualifiersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VOSTagsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
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
            // IdenttyGroupBox
            // 
            this.IdenttyGroupBox.Controls.Add(this.AccountExpirationDateTextBox);
            this.IdenttyGroupBox.Controls.Add(this.label8);
            this.IdenttyGroupBox.Controls.Add(this.ExpiredCheckbox);
            this.IdenttyGroupBox.Controls.Add(this.LockedCheckbox);
            this.IdenttyGroupBox.Controls.Add(this.PhoneTextBox);
            this.IdenttyGroupBox.Controls.Add(this.label7);
            this.IdenttyGroupBox.Controls.Add(this.DisplayNameTextBox);
            this.IdenttyGroupBox.Controls.Add(this.label6);
            this.IdenttyGroupBox.Controls.Add(this.MiddleNameTextBox);
            this.IdenttyGroupBox.Controls.Add(this.label5);
            this.IdenttyGroupBox.Controls.Add(this.EmailTextBox);
            this.IdenttyGroupBox.Controls.Add(this.label4);
            this.IdenttyGroupBox.Controls.Add(this.LastNameTextBox);
            this.IdenttyGroupBox.Controls.Add(this.label3);
            this.IdenttyGroupBox.Controls.Add(this.FirstNameTextBox);
            this.IdenttyGroupBox.Controls.Add(this.label2);
            this.IdenttyGroupBox.Location = new System.Drawing.Point(12, 40);
            this.IdenttyGroupBox.Name = "IdenttyGroupBox";
            this.IdenttyGroupBox.Size = new System.Drawing.Size(760, 175);
            this.IdenttyGroupBox.TabIndex = 1;
            this.IdenttyGroupBox.TabStop = false;
            this.IdenttyGroupBox.Text = "Identity";
            // 
            // AccountExpirationDateTextBox
            // 
            this.AccountExpirationDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AccountExpirationDateTextBox.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.AccountExpirationDateTextBox.Location = new System.Drawing.Point(549, 133);
            this.AccountExpirationDateTextBox.Mask = "00/00/0000";
            this.AccountExpirationDateTextBox.Name = "AccountExpirationDateTextBox";
            this.AccountExpirationDateTextBox.Size = new System.Drawing.Size(84, 20);
            this.AccountExpirationDateTextBox.TabIndex = 15;
            this.AccountExpirationDateTextBox.ValidatingType = typeof(System.DateTime);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(421, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Account Expiration Date";
            // 
            // ExpiredCheckbox
            // 
            this.ExpiredCheckbox.AutoSize = true;
            this.ExpiredCheckbox.Enabled = false;
            this.ExpiredCheckbox.Location = new System.Drawing.Point(549, 104);
            this.ExpiredCheckbox.Name = "ExpiredCheckbox";
            this.ExpiredCheckbox.Size = new System.Drawing.Size(104, 17);
            this.ExpiredCheckbox.TabIndex = 13;
            this.ExpiredCheckbox.Text = "Account Expired";
            this.ExpiredCheckbox.UseVisualStyleBackColor = true;
            // 
            // LockedCheckbox
            // 
            this.LockedCheckbox.AutoSize = true;
            this.LockedCheckbox.Enabled = false;
            this.LockedCheckbox.Location = new System.Drawing.Point(549, 81);
            this.LockedCheckbox.Name = "LockedCheckbox";
            this.LockedCheckbox.Size = new System.Drawing.Size(105, 17);
            this.LockedCheckbox.TabIndex = 12;
            this.LockedCheckbox.Text = "Account Locked";
            this.LockedCheckbox.UseVisualStyleBackColor = true;
            // 
            // PhoneTextBox
            // 
            this.PhoneTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PhoneTextBox.Location = new System.Drawing.Point(91, 135);
            this.PhoneTextBox.Mask = "(999) 000-0000";
            this.PhoneTextBox.Name = "PhoneTextBox";
            this.PhoneTextBox.Size = new System.Drawing.Size(84, 20);
            this.PhoneTextBox.TabIndex = 11;
            this.PhoneTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Phone";
            // 
            // DisplayNameTextBox
            // 
            this.DisplayNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DisplayNameTextBox.Location = new System.Drawing.Point(91, 65);
            this.DisplayNameTextBox.Name = "DisplayNameTextBox";
            this.DisplayNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.DisplayNameTextBox.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(6, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Display Name";
            // 
            // MiddleNameTextBox
            // 
            this.MiddleNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MiddleNameTextBox.Location = new System.Drawing.Point(329, 31);
            this.MiddleNameTextBox.Name = "MiddleNameTextBox";
            this.MiddleNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.MiddleNameTextBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(251, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Middle Name";
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmailTextBox.Location = new System.Drawing.Point(91, 98);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Size = new System.Drawing.Size(324, 20);
            this.EmailTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(6, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Email";
            // 
            // LastNameTextBox
            // 
            this.LastNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastNameTextBox.Location = new System.Drawing.Point(549, 31);
            this.LastNameTextBox.Name = "LastNameTextBox";
            this.LastNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.LastNameTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(485, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Last Name";
            // 
            // FirstNameTextBox
            // 
            this.FirstNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FirstNameTextBox.Location = new System.Drawing.Point(91, 29);
            this.FirstNameTextBox.Name = "FirstNameTextBox";
            this.FirstNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.FirstNameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "First Name";
            // 
            // LocaleTextBox
            // 
            this.LocaleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LocaleTextBox.Location = new System.Drawing.Point(110, 28);
            this.LocaleTextBox.Name = "LocaleTextBox";
            this.LocaleTextBox.Size = new System.Drawing.Size(75, 20);
            this.LocaleTextBox.TabIndex = 1;
            this.LocaleTextBox.Text = "en-US";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label10.Location = new System.Drawing.Point(9, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Locale";
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserNameTextBox.Location = new System.Drawing.Point(101, 28);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.UserNameTextBox.TabIndex = 1;
            this.UserNameTextBox.TextChanged += new System.EventHandler(this.UserNameTextBox_TextChanged);
            this.UserNameTextBox.Leave += new System.EventHandler(this.UserNameTextBox_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "UserName";
            // 
            // SecurityGroupBox
            // 
            this.SecurityGroupBox.Controls.Add(this.CheckAvailableButton);
            this.SecurityGroupBox.Controls.Add(this.ConfirmPasswordTextBox);
            this.SecurityGroupBox.Controls.Add(this.ConfirmPasswordLabel);
            this.SecurityGroupBox.Controls.Add(this.GroupsListBox);
            this.SecurityGroupBox.Controls.Add(this.label9);
            this.SecurityGroupBox.Controls.Add(this.PasswordTextBox);
            this.SecurityGroupBox.Controls.Add(this.PasswordLabel);
            this.SecurityGroupBox.Controls.Add(this.UserNameTextBox);
            this.SecurityGroupBox.Controls.Add(this.label1);
            this.SecurityGroupBox.Location = new System.Drawing.Point(12, 233);
            this.SecurityGroupBox.Name = "SecurityGroupBox";
            this.SecurityGroupBox.Size = new System.Drawing.Size(760, 138);
            this.SecurityGroupBox.TabIndex = 0;
            this.SecurityGroupBox.TabStop = false;
            this.SecurityGroupBox.Text = "Security";
            // 
            // CheckAvailableButton
            // 
            this.CheckAvailableButton.Enabled = false;
            this.CheckAvailableButton.Location = new System.Drawing.Point(245, 28);
            this.CheckAvailableButton.Name = "CheckAvailableButton";
            this.CheckAvailableButton.Size = new System.Drawing.Size(109, 23);
            this.CheckAvailableButton.TabIndex = 2;
            this.CheckAvailableButton.Text = "Check if Available";
            this.CheckAvailableButton.UseVisualStyleBackColor = true;
            this.CheckAvailableButton.Visible = false;
            this.CheckAvailableButton.Click += new System.EventHandler(this.CheckAvailableButton_Click);
            // 
            // ConfirmPasswordTextBox
            // 
            this.ConfirmPasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConfirmPasswordTextBox.Location = new System.Drawing.Point(101, 101);
            this.ConfirmPasswordTextBox.Name = "ConfirmPasswordTextBox";
            this.ConfirmPasswordTextBox.PasswordChar = '*';
            this.ConfirmPasswordTextBox.Size = new System.Drawing.Size(137, 20);
            this.ConfirmPasswordTextBox.TabIndex = 6;
            // 
            // ConfirmPasswordLabel
            // 
            this.ConfirmPasswordLabel.AutoSize = true;
            this.ConfirmPasswordLabel.Location = new System.Drawing.Point(6, 101);
            this.ConfirmPasswordLabel.Name = "ConfirmPasswordLabel";
            this.ConfirmPasswordLabel.Size = new System.Drawing.Size(91, 13);
            this.ConfirmPasswordLabel.TabIndex = 5;
            this.ConfirmPasswordLabel.Text = "Confirm Password";
            // 
            // GroupsListBox
            // 
            this.GroupsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GroupsListBox.FormattingEnabled = true;
            this.GroupsListBox.Location = new System.Drawing.Point(432, 28);
            this.GroupsListBox.Name = "GroupsListBox";
            this.GroupsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.GroupsListBox.Size = new System.Drawing.Size(313, 93);
            this.GroupsListBox.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label9.Location = new System.Drawing.Point(392, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Groups";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PasswordTextBox.Location = new System.Drawing.Point(101, 64);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(137, 20);
            this.PasswordTextBox.TabIndex = 4;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(6, 64);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(53, 13);
            this.PasswordLabel.TabIndex = 3;
            this.PasswordLabel.Text = "Password";
            // 
            // ProfileGroupBox
            // 
            this.ProfileGroupBox.Controls.Add(this.VOSTagsDataGridView);
            this.ProfileGroupBox.Controls.Add(this.GeneralIdTtextBox);
            this.ProfileGroupBox.Controls.Add(this.GeneralIdLabel);
            this.ProfileGroupBox.Controls.Add(this.LocaleTextBox);
            this.ProfileGroupBox.Controls.Add(this.label10);
            this.ProfileGroupBox.Controls.Add(this.VOSTagsLabel);
            this.ProfileGroupBox.Enabled = false;
            this.ProfileGroupBox.Location = new System.Drawing.Point(12, 539);
            this.ProfileGroupBox.Name = "ProfileGroupBox";
            this.ProfileGroupBox.Size = new System.Drawing.Size(760, 254);
            this.ProfileGroupBox.TabIndex = 5;
            this.ProfileGroupBox.TabStop = false;
            this.ProfileGroupBox.Text = "Profile";
            // 
            // VOSTagsDataGridView
            // 
            this.VOSTagsDataGridView.AllowUserToDeleteRows = false;
            this.VOSTagsDataGridView.AllowUserToResizeRows = false;
            this.VOSTagsDataGridView.AutoGenerateColumns = false;
            this.VOSTagsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VOSTagsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userVOSTagIdDataGridViewTextBoxColumn,
            this.codeDataGridViewTextBoxColumn,
            this.typeCodeDataGridViewTextBoxColumn,
            this.effectiveDateDataGridViewTextBoxColumn,
            this.endDateDataGridViewTextBoxColumn});
            this.VOSTagsDataGridView.DataSource = this.VOSTagsBindingSource;
            this.VOSTagsDataGridView.Location = new System.Drawing.Point(12, 125);
            this.VOSTagsDataGridView.Name = "VOSTagsDataGridView";
            this.VOSTagsDataGridView.Size = new System.Drawing.Size(733, 123);
            this.VOSTagsDataGridView.TabIndex = 5;
            // 
            // GeneralIdTtextBox
            // 
            this.GeneralIdTtextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GeneralIdTtextBox.Location = new System.Drawing.Point(110, 63);
            this.GeneralIdTtextBox.Name = "GeneralIdTtextBox";
            this.GeneralIdTtextBox.Size = new System.Drawing.Size(247, 20);
            this.GeneralIdTtextBox.TabIndex = 3;
            // 
            // GeneralIdLabel
            // 
            this.GeneralIdLabel.AutoSize = true;
            this.GeneralIdLabel.Location = new System.Drawing.Point(9, 63);
            this.GeneralIdLabel.Name = "GeneralIdLabel";
            this.GeneralIdLabel.Size = new System.Drawing.Size(87, 13);
            this.GeneralIdLabel.TabIndex = 2;
            this.GeneralIdLabel.Text = "General Identifier";
            // 
            // VOSTagsLabel
            // 
            this.VOSTagsLabel.AutoSize = true;
            this.VOSTagsLabel.Location = new System.Drawing.Point(7, 100);
            this.VOSTagsLabel.Name = "VOSTagsLabel";
            this.VOSTagsLabel.Size = new System.Drawing.Size(56, 13);
            this.VOSTagsLabel.TabIndex = 4;
            this.VOSTagsLabel.Text = "VOS Tags";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(616, 799);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(697, 799);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 829);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusStripLabel
            // 
            this.StatusStripLabel.Name = "StatusStripLabel";
            this.StatusStripLabel.Size = new System.Drawing.Size(144, 17);
            this.StatusStripLabel.Text = "Initializing user account....";
            // 
            // CreateUserProfileCheckBox
            // 
            this.CreateUserProfileCheckBox.AutoSize = true;
            this.CreateUserProfileCheckBox.Enabled = false;
            this.CreateUserProfileCheckBox.Location = new System.Drawing.Point(12, 516);
            this.CreateUserProfileCheckBox.Name = "CreateUserProfileCheckBox";
            this.CreateUserProfileCheckBox.Size = new System.Drawing.Size(120, 17);
            this.CreateUserProfileCheckBox.TabIndex = 4;
            this.CreateUserProfileCheckBox.Text = "Create a &user profile";
            this.CreateUserProfileCheckBox.UseVisualStyleBackColor = true;
            this.CreateUserProfileCheckBox.CheckedChanged += new System.EventHandler(this.CreateUserProfileCheckBox_CheckedChanged);
            // 
            // RegQualifiersGroupBox
            // 
            this.RegQualifiersGroupBox.Controls.Add(this.RegQualifiersDataGridView);
            this.RegQualifiersGroupBox.Location = new System.Drawing.Point(12, 389);
            this.RegQualifiersGroupBox.Name = "RegQualifiersGroupBox";
            this.RegQualifiersGroupBox.Size = new System.Drawing.Size(760, 108);
            this.RegQualifiersGroupBox.TabIndex = 3;
            this.RegQualifiersGroupBox.TabStop = false;
            this.RegQualifiersGroupBox.Text = "Registration Qualifiers";
            // 
            // RegQualifiersDataGridView
            // 
            this.RegQualifiersDataGridView.AllowUserToResizeRows = false;
            this.RegQualifiersDataGridView.AutoGenerateColumns = false;
            this.RegQualifiersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RegQualifiersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.keyDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn});
            this.RegQualifiersDataGridView.DataSource = this.RegQualifiersBindingSource;
            this.RegQualifiersDataGridView.Location = new System.Drawing.Point(9, 21);
            this.RegQualifiersDataGridView.Name = "RegQualifiersDataGridView";
            this.RegQualifiersDataGridView.Size = new System.Drawing.Size(733, 81);
            this.RegQualifiersDataGridView.TabIndex = 0;
            // 
            // keyDataGridViewTextBoxColumn
            // 
            this.keyDataGridViewTextBoxColumn.DataPropertyName = "Key";
            this.keyDataGridViewTextBoxColumn.HeaderText = "Key";
            this.keyDataGridViewTextBoxColumn.Name = "keyDataGridViewTextBoxColumn";
            this.keyDataGridViewTextBoxColumn.Width = 200;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.Width = 300;
            // 
            // RegQualifiersBindingSource
            // 
            this.RegQualifiersBindingSource.DataSource = typeof(UserAccountManager.Domain.RegistrationQualifier);
            // 
            // userVOSTagIdDataGridViewTextBoxColumn
            // 
            this.userVOSTagIdDataGridViewTextBoxColumn.DataPropertyName = "UserVOSTagId";
            this.userVOSTagIdDataGridViewTextBoxColumn.HeaderText = "UserVOSTagId";
            this.userVOSTagIdDataGridViewTextBoxColumn.Name = "userVOSTagIdDataGridViewTextBoxColumn";
            this.userVOSTagIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            this.codeDataGridViewTextBoxColumn.HeaderText = "Code";
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.Width = 200;
            // 
            // typeCodeDataGridViewTextBoxColumn
            // 
            this.typeCodeDataGridViewTextBoxColumn.DataPropertyName = "TypeCode";
            this.typeCodeDataGridViewTextBoxColumn.HeaderText = "Type Code";
            this.typeCodeDataGridViewTextBoxColumn.Name = "typeCodeDataGridViewTextBoxColumn";
            this.typeCodeDataGridViewTextBoxColumn.Width = 200;
            // 
            // effectiveDateDataGridViewTextBoxColumn
            // 
            this.effectiveDateDataGridViewTextBoxColumn.DataPropertyName = "EffectiveDate";
            this.effectiveDateDataGridViewTextBoxColumn.HeaderText = "Effective Date";
            this.effectiveDateDataGridViewTextBoxColumn.Name = "effectiveDateDataGridViewTextBoxColumn";
            // 
            // endDateDataGridViewTextBoxColumn
            // 
            this.endDateDataGridViewTextBoxColumn.DataPropertyName = "EndDate";
            this.endDateDataGridViewTextBoxColumn.HeaderText = "End Date";
            this.endDateDataGridViewTextBoxColumn.Name = "endDateDataGridViewTextBoxColumn";
            // 
            // VOSTagsBindingSource
            // 
            this.VOSTagsBindingSource.DataSource = typeof(UserAccountManager.Domain.UserVOSTag);
            // 
            // UserAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 851);
            this.Controls.Add(this.RegQualifiersGroupBox);
            this.Controls.Add(this.CreateUserProfileCheckBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ProfileGroupBox);
            this.Controls.Add(this.SecurityGroupBox);
            this.Controls.Add(this.IdenttyGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 760);
            this.Name = "UserAccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Account";
            this.Load += new System.EventHandler(this.UserAccountForm_Load);
            this.Shown += new System.EventHandler(this.UserAccountForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.IdenttyGroupBox.ResumeLayout(false);
            this.IdenttyGroupBox.PerformLayout();
            this.SecurityGroupBox.ResumeLayout(false);
            this.SecurityGroupBox.PerformLayout();
            this.ProfileGroupBox.ResumeLayout(false);
            this.ProfileGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VOSTagsDataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.RegQualifiersGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RegQualifiersDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegQualifiersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VOSTagsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox IdenttyGroupBox;
        private System.Windows.Forms.TextBox EmailTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LastNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FirstNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MiddleNameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox PhoneTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox DisplayNameTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox SecurityGroupBox;
        private System.Windows.Forms.TextBox ConfirmPasswordTextBox;
        private System.Windows.Forms.Label ConfirmPasswordLabel;
        private System.Windows.Forms.ListBox GroupsListBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.GroupBox ProfileGroupBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusStripLabel;
        private System.Windows.Forms.Label VOSTagsLabel;
        private System.Windows.Forms.Button CheckAvailableButton;
        private System.Windows.Forms.TextBox LocaleTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox GeneralIdTtextBox;
        private System.Windows.Forms.Label GeneralIdLabel;
        private System.Windows.Forms.CheckBox CreateUserProfileCheckBox;
        private System.Windows.Forms.DataGridView VOSTagsDataGridView;
        private System.Windows.Forms.BindingSource VOSTagsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn userVOSTagIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn effectiveDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox RegQualifiersGroupBox;
        private System.Windows.Forms.DataGridView RegQualifiersDataGridView;
        private System.Windows.Forms.BindingSource RegQualifiersBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.CheckBox ExpiredCheckbox;
        private System.Windows.Forms.CheckBox LockedCheckbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox AccountExpirationDateTextBox;
    }
}