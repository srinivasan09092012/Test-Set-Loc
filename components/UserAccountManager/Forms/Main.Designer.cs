﻿namespace UserAccountManager.Forms
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchGroupBox = new System.Windows.Forms.GroupBox();
            this.EnvComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LastNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FirstNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ResultsGroupBox = new System.Windows.Forms.GroupBox();
            this.ResultsDataGridView = new System.Windows.Forms.DataGridView();
            this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.middleNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.displayNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserAccountsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.AddButton = new System.Windows.Forms.Button();
            this.OtherActionsGroupBox = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.SearchGroupBox.SuspendLayout();
            this.ResultsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserAccountsBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.OtherActionsGroupBox.SuspendLayout();
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
            // SearchGroupBox
            // 
            this.SearchGroupBox.Controls.Add(this.SearchButton);
            this.SearchGroupBox.Controls.Add(this.EmailTextBox);
            this.SearchGroupBox.Controls.Add(this.label4);
            this.SearchGroupBox.Controls.Add(this.LastNameTextBox);
            this.SearchGroupBox.Controls.Add(this.label3);
            this.SearchGroupBox.Controls.Add(this.FirstNameTextBox);
            this.SearchGroupBox.Controls.Add(this.label2);
            this.SearchGroupBox.Controls.Add(this.UserNameTextBox);
            this.SearchGroupBox.Controls.Add(this.label1);
            this.SearchGroupBox.Enabled = false;
            this.SearchGroupBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SearchGroupBox.Location = new System.Drawing.Point(12, 82);
            this.SearchGroupBox.Name = "SearchGroupBox";
            this.SearchGroupBox.Size = new System.Drawing.Size(577, 140);
            this.SearchGroupBox.TabIndex = 3;
            this.SearchGroupBox.TabStop = false;
            this.SearchGroupBox.Text = "Search User Account";
            // 
            // EnvComboBox
            // 
            this.EnvComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EnvComboBox.Location = new System.Drawing.Point(92, 44);
            this.EnvComboBox.Name = "EnvComboBox";
            this.EnvComboBox.Size = new System.Drawing.Size(137, 21);
            this.EnvComboBox.TabIndex = 2;
            this.EnvComboBox.SelectedIndexChanged += new System.EventHandler(this.EnvComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(11, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Environment";
            // 
            // SearchButton
            // 
            this.SearchButton.Enabled = false;
            this.SearchButton.Location = new System.Drawing.Point(488, 101);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 8;
            this.SearchButton.Text = "&Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmailTextBox.Location = new System.Drawing.Point(94, 101);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Size = new System.Drawing.Size(375, 20);
            this.EmailTextBox.TabIndex = 9;
            this.EmailTextBox.TextChanged += new System.EventHandler(this.EmailTextBox_TextChanged);
            this.EmailTextBox.Leave += new System.EventHandler(this.EmailTextBox_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Email";
            // 
            // LastNameTextBox
            // 
            this.LastNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastNameTextBox.Location = new System.Drawing.Point(332, 65);
            this.LastNameTextBox.Name = "LastNameTextBox";
            this.LastNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.LastNameTextBox.TabIndex = 7;
            this.LastNameTextBox.TextChanged += new System.EventHandler(this.LastNameTextBox_TextChanged);
            this.LastNameTextBox.Leave += new System.EventHandler(this.LastNameTextBox_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Last Name";
            // 
            // FirstNameTextBox
            // 
            this.FirstNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FirstNameTextBox.Location = new System.Drawing.Point(95, 65);
            this.FirstNameTextBox.Name = "FirstNameTextBox";
            this.FirstNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.FirstNameTextBox.TabIndex = 5;
            this.FirstNameTextBox.TextChanged += new System.EventHandler(this.FirstNameTextBox_TextChanged);
            this.FirstNameTextBox.Leave += new System.EventHandler(this.FirstNameTextBox_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "First Name";
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserNameTextBox.Location = new System.Drawing.Point(95, 31);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.UserNameTextBox.TabIndex = 3;
            this.UserNameTextBox.TextChanged += new System.EventHandler(this.UserNameTextBox_TextChanged);
            this.UserNameTextBox.Leave += new System.EventHandler(this.UserNameTextBox_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "UserName";
            // 
            // ResultsGroupBox
            // 
            this.ResultsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsGroupBox.Controls.Add(this.ResultsDataGridView);
            this.ResultsGroupBox.Enabled = false;
            this.ResultsGroupBox.Location = new System.Drawing.Point(12, 241);
            this.ResultsGroupBox.Name = "ResultsGroupBox";
            this.ResultsGroupBox.Size = new System.Drawing.Size(1000, 359);
            this.ResultsGroupBox.TabIndex = 5;
            this.ResultsGroupBox.TabStop = false;
            this.ResultsGroupBox.Text = "Results";
            // 
            // ResultsDataGridView
            // 
            this.ResultsDataGridView.AllowUserToAddRows = false;
            this.ResultsDataGridView.AutoGenerateColumns = false;
            this.ResultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userNameDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.middleNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.displayNameDataGridViewTextBoxColumn,
            this.emailAddressDataGridViewTextBoxColumn,
            this.phoneNumberDataGridViewTextBoxColumn});
            this.ResultsDataGridView.DataSource = this.UserAccountsBindingSource;
            this.ResultsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsDataGridView.Location = new System.Drawing.Point(3, 16);
            this.ResultsDataGridView.MultiSelect = false;
            this.ResultsDataGridView.Name = "ResultsDataGridView";
            this.ResultsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ResultsDataGridView.Size = new System.Drawing.Size(994, 340);
            this.ResultsDataGridView.TabIndex = 0;
            this.ResultsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResultsDataGridView_CellDoubleClick);
            this.ResultsDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.ResultsDataGridView_UserDeletingRow);
            // 
            // userNameDataGridViewTextBoxColumn
            // 
            this.userNameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
            this.userNameDataGridViewTextBoxColumn.Frozen = true;
            this.userNameDataGridViewTextBoxColumn.HeaderText = "UserName";
            this.userNameDataGridViewTextBoxColumn.Name = "userNameDataGridViewTextBoxColumn";
            this.userNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstNameDataGridViewTextBoxColumn
            // 
            this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.HeaderText = "First Name";
            this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
            this.firstNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // middleNameDataGridViewTextBoxColumn
            // 
            this.middleNameDataGridViewTextBoxColumn.DataPropertyName = "MiddleName";
            this.middleNameDataGridViewTextBoxColumn.HeaderText = "Middle Name";
            this.middleNameDataGridViewTextBoxColumn.Name = "middleNameDataGridViewTextBoxColumn";
            this.middleNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            this.lastNameDataGridViewTextBoxColumn.HeaderText = "Last Name";
            this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            this.lastNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // displayNameDataGridViewTextBoxColumn
            // 
            this.displayNameDataGridViewTextBoxColumn.DataPropertyName = "DisplayName";
            this.displayNameDataGridViewTextBoxColumn.HeaderText = "Display Name";
            this.displayNameDataGridViewTextBoxColumn.Name = "displayNameDataGridViewTextBoxColumn";
            this.displayNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.displayNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // emailAddressDataGridViewTextBoxColumn
            // 
            this.emailAddressDataGridViewTextBoxColumn.DataPropertyName = "EmailAddress";
            this.emailAddressDataGridViewTextBoxColumn.HeaderText = "Email Address";
            this.emailAddressDataGridViewTextBoxColumn.Name = "emailAddressDataGridViewTextBoxColumn";
            this.emailAddressDataGridViewTextBoxColumn.ReadOnly = true;
            this.emailAddressDataGridViewTextBoxColumn.Width = 200;
            // 
            // phoneNumberDataGridViewTextBoxColumn
            // 
            this.phoneNumberDataGridViewTextBoxColumn.DataPropertyName = "PhoneNumber";
            this.phoneNumberDataGridViewTextBoxColumn.HeaderText = "Phone Number";
            this.phoneNumberDataGridViewTextBoxColumn.Name = "phoneNumberDataGridViewTextBoxColumn";
            this.phoneNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.phoneNumberDataGridViewTextBoxColumn.Width = 125;
            // 
            // UserAccountsBindingSource
            // 
            this.UserAccountsBindingSource.DataSource = typeof(HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.UserIdentity);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 614);
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
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(17, 31);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(125, 23);
            this.AddButton.TabIndex = 10;
            this.AddButton.Text = "&Add New Account";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // OtherActionsGroupBox
            // 
            this.OtherActionsGroupBox.Controls.Add(this.AddButton);
            this.OtherActionsGroupBox.Enabled = false;
            this.OtherActionsGroupBox.Location = new System.Drawing.Point(595, 82);
            this.OtherActionsGroupBox.Name = "OtherActionsGroupBox";
            this.OtherActionsGroupBox.Size = new System.Drawing.Size(414, 140);
            this.OtherActionsGroupBox.TabIndex = 4;
            this.OtherActionsGroupBox.TabStop = false;
            this.OtherActionsGroupBox.Text = "Other Actions";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 636);
            this.Controls.Add(this.EnvComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.OtherActionsGroupBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ResultsGroupBox);
            this.Controls.Add(this.SearchGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1040, 660);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Account Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.SearchGroupBox.ResumeLayout(false);
            this.SearchGroupBox.PerformLayout();
            this.ResultsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResultsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserAccountsBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.OtherActionsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox SearchGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox EmailTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LastNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FirstNameTextBox;
        private System.Windows.Forms.ComboBox EnvComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox ResultsGroupBox;
        private System.Windows.Forms.BindingSource UserAccountsBindingSource;
        private System.Windows.Forms.DataGridView ResultsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn middleNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailAddressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusStripLabel;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.GroupBox OtherActionsGroupBox;
    }
}

