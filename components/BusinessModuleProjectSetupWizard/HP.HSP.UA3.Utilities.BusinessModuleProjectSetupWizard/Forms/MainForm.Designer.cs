﻿namespace HP.HSP.UA3.Utilities.BusinessModuleProjectSetupWizard.Forms
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hELPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutTenentConfigurationManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.CreateNewButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BusinessModuleDropdown = new System.Windows.Forms.ComboBox();
            this.DetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DeleteBASButton = new System.Windows.Forms.Button();
            this.CreateBASButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BASDropdown = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.DeleteBatchButton = new System.Windows.Forms.Button();
            this.BatchDropdown = new System.Windows.Forms.ComboBox();
            this.CreateBatchButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.DetailsGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem,
            this.editToolStripMenuItem,
            this.hELPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            this.fIleToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fIleToolStripMenuItem.Text = "&FILE";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveToolStripMenuItem.Text = "S&ave";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(135, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userConfigurationToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.editToolStripMenuItem.Text = "&EDIT";
            // 
            // userConfigurationToolStripMenuItem
            // 
            this.userConfigurationToolStripMenuItem.Name = "userConfigurationToolStripMenuItem";
            this.userConfigurationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.userConfigurationToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.userConfigurationToolStripMenuItem.Text = "User Configuration";
            this.userConfigurationToolStripMenuItem.Click += new System.EventHandler(this.userConfigurationToolStripMenuItem_Click);
            // 
            // hELPToolStripMenuItem
            // 
            this.hELPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.aboutTenentConfigurationManagerToolStripMenuItem});
            this.hELPToolStripMenuItem.Name = "hELPToolStripMenuItem";
            this.hELPToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.hELPToolStripMenuItem.Text = "&HELP";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(275, 6);
            // 
            // aboutTenentConfigurationManagerToolStripMenuItem
            // 
            this.aboutTenentConfigurationManagerToolStripMenuItem.Name = "aboutTenentConfigurationManagerToolStripMenuItem";
            this.aboutTenentConfigurationManagerToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.aboutTenentConfigurationManagerToolStripMenuItem.Text = "&About Business Module Project Wizard";
            this.aboutTenentConfigurationManagerToolStripMenuItem.Click += new System.EventHandler(this.aboutTenentConfigurationManagerToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DeleteButton);
            this.groupBox1.Controls.Add(this.CreateNewButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BusinessModuleDropdown);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(984, 72);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Business Module";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Location = new System.Drawing.Point(291, 28);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // CreateNewButton
            // 
            this.CreateNewButton.Location = new System.Drawing.Point(372, 28);
            this.CreateNewButton.Name = "CreateNewButton";
            this.CreateNewButton.Size = new System.Drawing.Size(75, 23);
            this.CreateNewButton.TabIndex = 3;
            this.CreateNewButton.Text = "Create New";
            this.CreateNewButton.UseVisualStyleBackColor = true;
            this.CreateNewButton.Click += new System.EventHandler(this.CreateNewButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Module";
            // 
            // BusinessModuleDropdown
            // 
            this.BusinessModuleDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BusinessModuleDropdown.Enabled = false;
            this.BusinessModuleDropdown.FormattingEnabled = true;
            this.BusinessModuleDropdown.Location = new System.Drawing.Point(65, 30);
            this.BusinessModuleDropdown.Name = "BusinessModuleDropdown";
            this.BusinessModuleDropdown.Size = new System.Drawing.Size(220, 21);
            this.BusinessModuleDropdown.TabIndex = 1;
            this.BusinessModuleDropdown.SelectedIndexChanged += new System.EventHandler(this.BusinessModuleDropdown_SelectedIndexChanged);
            // 
            // DetailsGroupBox
            // 
            this.DetailsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailsGroupBox.Controls.Add(this.groupBox4);
            this.DetailsGroupBox.Controls.Add(this.groupBox3);
            this.DetailsGroupBox.Enabled = false;
            this.DetailsGroupBox.Location = new System.Drawing.Point(13, 106);
            this.DetailsGroupBox.Name = "DetailsGroupBox";
            this.DetailsGroupBox.Size = new System.Drawing.Size(983, 223);
            this.DetailsGroupBox.TabIndex = 3;
            this.DetailsGroupBox.TabStop = false;
            this.DetailsGroupBox.Text = "Business Module Details";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DeleteBASButton);
            this.groupBox3.Controls.Add(this.BASDropdown);
            this.groupBox3.Controls.Add(this.CreateBASButton);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(19, 35);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(938, 83);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Business Application Services (BAS)";
            // 
            // DeleteBASButton
            // 
            this.DeleteBASButton.Enabled = false;
            this.DeleteBASButton.Location = new System.Drawing.Point(296, 34);
            this.DeleteBASButton.Name = "DeleteBASButton";
            this.DeleteBASButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteBASButton.TabIndex = 6;
            this.DeleteBASButton.Text = "Delete";
            this.DeleteBASButton.UseVisualStyleBackColor = true;
            this.DeleteBASButton.Click += new System.EventHandler(this.DeleteBASButton_Click);
            // 
            // CreateBASButton
            // 
            this.CreateBASButton.Location = new System.Drawing.Point(377, 34);
            this.CreateBASButton.Name = "CreateBASButton";
            this.CreateBASButton.Size = new System.Drawing.Size(75, 23);
            this.CreateBASButton.TabIndex = 7;
            this.CreateBASButton.Text = "Create New";
            this.CreateBASButton.UseVisualStyleBackColor = true;
            this.CreateBASButton.Click += new System.EventHandler(this.CreateBASButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Service";
            // 
            // BASDropdown
            // 
            this.BASDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BASDropdown.Enabled = false;
            this.BASDropdown.FormattingEnabled = true;
            this.BASDropdown.Location = new System.Drawing.Point(70, 36);
            this.BASDropdown.Name = "BASDropdown";
            this.BASDropdown.Size = new System.Drawing.Size(220, 21);
            this.BASDropdown.TabIndex = 5;
            this.BASDropdown.SelectedIndexChanged += new System.EventHandler(this.BASDropdown_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.DeleteBatchButton);
            this.groupBox4.Controls.Add(this.BatchDropdown);
            this.groupBox4.Controls.Add(this.CreateBatchButton);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(19, 124);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(938, 83);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Batch Services (Batch)";
            // 
            // DeleteBatchButton
            // 
            this.DeleteBatchButton.Enabled = false;
            this.DeleteBatchButton.Location = new System.Drawing.Point(296, 34);
            this.DeleteBatchButton.Name = "DeleteBatchButton";
            this.DeleteBatchButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteBatchButton.TabIndex = 6;
            this.DeleteBatchButton.Text = "Delete";
            this.DeleteBatchButton.UseVisualStyleBackColor = true;
            this.DeleteBatchButton.Click += new System.EventHandler(this.DeleteBatchButton_Click);
            // 
            // BatchDropdown
            // 
            this.BatchDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BatchDropdown.Enabled = false;
            this.BatchDropdown.FormattingEnabled = true;
            this.BatchDropdown.Location = new System.Drawing.Point(70, 36);
            this.BatchDropdown.Name = "BatchDropdown";
            this.BatchDropdown.Size = new System.Drawing.Size(220, 21);
            this.BatchDropdown.TabIndex = 5;
            this.BatchDropdown.SelectedIndexChanged += new System.EventHandler(this.BatchDropdown_SelectedIndexChanged);
            // 
            // CreateBatchButton
            // 
            this.CreateBatchButton.Location = new System.Drawing.Point(377, 34);
            this.CreateBatchButton.Name = "CreateBatchButton";
            this.CreateBatchButton.Size = new System.Drawing.Size(75, 23);
            this.CreateBatchButton.TabIndex = 7;
            this.CreateBatchButton.Text = "Create New";
            this.CreateBatchButton.UseVisualStyleBackColor = true;
            this.CreateBatchButton.Click += new System.EventHandler(this.CreateBatchButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Service";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 341);
            this.Controls.Add(this.DetailsGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(1024, 379);
            this.Name = "MainForm";
            this.Text = "Business Module Project Wizard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DetailsGroupBox.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hELPToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem aboutTenentConfigurationManagerToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CreateNewButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox BusinessModuleDropdown;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.GroupBox DetailsGroupBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button DeleteBatchButton;
        private System.Windows.Forms.ComboBox BatchDropdown;
        private System.Windows.Forms.Button CreateBatchButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button DeleteBASButton;
        private System.Windows.Forms.ComboBox BASDropdown;
        private System.Windows.Forms.Button CreateBASButton;
        private System.Windows.Forms.Label label2;
    }
}

