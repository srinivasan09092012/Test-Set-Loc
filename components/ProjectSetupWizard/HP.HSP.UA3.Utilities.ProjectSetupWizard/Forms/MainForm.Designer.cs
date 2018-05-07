//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

namespace HP.HSP.UA3.Utilities.ProjectSetupWizard.Forms
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DeleteApiButton = new System.Windows.Forms.Button();
            this.ApiDropdown = new System.Windows.Forms.ComboBox();
            this.CreateApiButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BatchGroupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DeleteBatchButton = new System.Windows.Forms.Button();
            this.BatchDropdown = new System.Windows.Forms.ComboBox();
            this.CreateBatchButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TargetBranchDropdown = new System.Windows.Forms.ComboBox();
            this.BASGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteBASButton = new System.Windows.Forms.Button();
            this.BASDropdown = new System.Windows.Forms.ComboBox();
            this.CreateBASButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.DetailsGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.BatchGroupBox.SuspendLayout();
            this.BASGroupBox.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(544, 24);
            this.menuStrip1.TabIndex = 0;
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
            this.groupBox1.Size = new System.Drawing.Size(520, 72);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Business Module";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Enabled = false;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteButton.Location = new System.Drawing.Point(316, 30);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // CreateNewButton
            // 
            this.CreateNewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateNewButton.Location = new System.Drawing.Point(397, 30);
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
            this.BusinessModuleDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BusinessModuleDropdown.FormattingEnabled = true;
            this.BusinessModuleDropdown.Location = new System.Drawing.Point(65, 30);
            this.BusinessModuleDropdown.Name = "BusinessModuleDropdown";
            this.BusinessModuleDropdown.Size = new System.Drawing.Size(245, 21);
            this.BusinessModuleDropdown.TabIndex = 1;
            this.BusinessModuleDropdown.SelectedIndexChanged += new System.EventHandler(this.BusinessModuleDropdown_SelectedIndexChanged);
            // 
            // DetailsGroupBox
            // 
            this.DetailsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailsGroupBox.Controls.Add(this.groupBox2);
            this.DetailsGroupBox.Controls.Add(this.label4);
            this.DetailsGroupBox.Controls.Add(this.BatchGroupBox);
            this.DetailsGroupBox.Controls.Add(this.TargetBranchDropdown);
            this.DetailsGroupBox.Controls.Add(this.BASGroupBox);
            this.DetailsGroupBox.Enabled = false;
            this.DetailsGroupBox.Location = new System.Drawing.Point(13, 106);
            this.DetailsGroupBox.Name = "DetailsGroupBox";
            this.DetailsGroupBox.Size = new System.Drawing.Size(519, 339);
            this.DetailsGroupBox.TabIndex = 2;
            this.DetailsGroupBox.TabStop = false;
            this.DetailsGroupBox.Text = "Business Module Details";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.DeleteApiButton);
            this.groupBox2.Controls.Add(this.ApiDropdown);
            this.groupBox2.Controls.Add(this.CreateApiButton);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(19, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(474, 83);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Package API";
            // 
            // DeleteApiButton
            // 
            this.DeleteApiButton.Enabled = false;
            this.DeleteApiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteApiButton.Location = new System.Drawing.Point(296, 34);
            this.DeleteApiButton.Name = "DeleteApiButton";
            this.DeleteApiButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteApiButton.TabIndex = 2;
            this.DeleteApiButton.Text = "Delete";
            this.DeleteApiButton.UseVisualStyleBackColor = true;
            this.DeleteApiButton.Click += new System.EventHandler(this.DeleteApiButton_Click);
            // 
            // ApiDropdown
            // 
            this.ApiDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ApiDropdown.Enabled = false;
            this.ApiDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApiDropdown.FormattingEnabled = true;
            this.ApiDropdown.Location = new System.Drawing.Point(70, 36);
            this.ApiDropdown.Name = "ApiDropdown";
            this.ApiDropdown.Size = new System.Drawing.Size(220, 21);
            this.ApiDropdown.TabIndex = 1;
            this.ApiDropdown.SelectedIndexChanged += new System.EventHandler(this.ApiDropdown_SelectedIndexChanged);
            // 
            // CreateApiButton
            // 
            this.CreateApiButton.Enabled = false;
            this.CreateApiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateApiButton.Location = new System.Drawing.Point(377, 34);
            this.CreateApiButton.Name = "CreateApiButton";
            this.CreateApiButton.Size = new System.Drawing.Size(75, 23);
            this.CreateApiButton.TabIndex = 3;
            this.CreateApiButton.Text = "Create New";
            this.CreateApiButton.UseVisualStyleBackColor = true;
            this.CreateApiButton.Click += new System.EventHandler(this.CreateApiButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "API";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Target Branch";
            // 
            // BatchGroupBox
            // 
            this.BatchGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BatchGroupBox.Controls.Add(this.label6);
            this.BatchGroupBox.Controls.Add(this.DeleteBatchButton);
            this.BatchGroupBox.Controls.Add(this.BatchDropdown);
            this.BatchGroupBox.Controls.Add(this.CreateBatchButton);
            this.BatchGroupBox.Controls.Add(this.label3);
            this.BatchGroupBox.Location = new System.Drawing.Point(19, 148);
            this.BatchGroupBox.Name = "BatchGroupBox";
            this.BatchGroupBox.Size = new System.Drawing.Size(474, 83);
            this.BatchGroupBox.TabIndex = 3;
            this.BatchGroupBox.TabStop = false;
            this.BatchGroupBox.Text = "Batch Services (Batch)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(282, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Use the checklist for How to Create a new Batch Solution.";
            // 
            // DeleteBatchButton
            // 
            this.DeleteBatchButton.Enabled = false;
            this.DeleteBatchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteBatchButton.Location = new System.Drawing.Point(296, 54);
            this.DeleteBatchButton.Name = "DeleteBatchButton";
            this.DeleteBatchButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteBatchButton.TabIndex = 2;
            this.DeleteBatchButton.Text = "Delete";
            this.DeleteBatchButton.UseVisualStyleBackColor = true;
            this.DeleteBatchButton.Click += new System.EventHandler(this.DeleteBatchButton_Click);
            // 
            // BatchDropdown
            // 
            this.BatchDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BatchDropdown.Enabled = false;
            this.BatchDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BatchDropdown.FormattingEnabled = true;
            this.BatchDropdown.Location = new System.Drawing.Point(70, 56);
            this.BatchDropdown.Name = "BatchDropdown";
            this.BatchDropdown.Size = new System.Drawing.Size(220, 21);
            this.BatchDropdown.TabIndex = 0;
            this.BatchDropdown.SelectedIndexChanged += new System.EventHandler(this.BatchDropdown_SelectedIndexChanged);
            // 
            // CreateBatchButton
            // 
            this.CreateBatchButton.Enabled = false;
            this.CreateBatchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateBatchButton.Location = new System.Drawing.Point(377, 54);
            this.CreateBatchButton.Name = "CreateBatchButton";
            this.CreateBatchButton.Size = new System.Drawing.Size(75, 23);
            this.CreateBatchButton.TabIndex = 3;
            this.CreateBatchButton.Text = "Create New";
            this.CreateBatchButton.UseVisualStyleBackColor = true;
            this.CreateBatchButton.Click += new System.EventHandler(this.CreateBatchButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Service";
            // 
            // TargetBranchDropdown
            // 
            this.TargetBranchDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TargetBranchDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TargetBranchDropdown.FormattingEnabled = true;
            this.TargetBranchDropdown.Location = new System.Drawing.Point(107, 29);
            this.TargetBranchDropdown.Name = "TargetBranchDropdown";
            this.TargetBranchDropdown.Size = new System.Drawing.Size(96, 21);
            this.TargetBranchDropdown.TabIndex = 1;
            this.TargetBranchDropdown.SelectedIndexChanged += new System.EventHandler(this.TargetBranchDropdown_SelectedIndexChanged);
            // 
            // BASGroupBox
            // 
            this.BASGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BASGroupBox.Controls.Add(this.DeleteBASButton);
            this.BASGroupBox.Controls.Add(this.BASDropdown);
            this.BASGroupBox.Controls.Add(this.CreateBASButton);
            this.BASGroupBox.Controls.Add(this.label2);
            this.BASGroupBox.Location = new System.Drawing.Point(19, 59);
            this.BASGroupBox.Name = "BASGroupBox";
            this.BASGroupBox.Size = new System.Drawing.Size(474, 83);
            this.BASGroupBox.TabIndex = 2;
            this.BASGroupBox.TabStop = false;
            this.BASGroupBox.Text = "Business Application Services (BAS)";
            // 
            // DeleteBASButton
            // 
            this.DeleteBASButton.Enabled = false;
            this.DeleteBASButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteBASButton.Location = new System.Drawing.Point(296, 34);
            this.DeleteBASButton.Name = "DeleteBASButton";
            this.DeleteBASButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteBASButton.TabIndex = 2;
            this.DeleteBASButton.Text = "Delete";
            this.DeleteBASButton.UseVisualStyleBackColor = true;
            this.DeleteBASButton.Click += new System.EventHandler(this.DeleteBASButton_Click);
            // 
            // BASDropdown
            // 
            this.BASDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BASDropdown.Enabled = false;
            this.BASDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BASDropdown.FormattingEnabled = true;
            this.BASDropdown.Location = new System.Drawing.Point(70, 36);
            this.BASDropdown.Name = "BASDropdown";
            this.BASDropdown.Size = new System.Drawing.Size(220, 21);
            this.BASDropdown.TabIndex = 1;
            this.BASDropdown.SelectedIndexChanged += new System.EventHandler(this.BASDropdown_SelectedIndexChanged);
            // 
            // CreateBASButton
            // 
            this.CreateBASButton.Enabled = false;
            this.CreateBASButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateBASButton.Location = new System.Drawing.Point(377, 34);
            this.CreateBASButton.Name = "CreateBASButton";
            this.CreateBASButton.Size = new System.Drawing.Size(75, 23);
            this.CreateBASButton.TabIndex = 3;
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
            this.label2.TabIndex = 0;
            this.label2.Text = "Service";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 457);
            this.Controls.Add(this.DetailsGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(560, 495);
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
            this.DetailsGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.BatchGroupBox.ResumeLayout(false);
            this.BatchGroupBox.PerformLayout();
            this.BASGroupBox.ResumeLayout(false);
            this.BASGroupBox.PerformLayout();
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
        private System.Windows.Forms.GroupBox BatchGroupBox;
        private System.Windows.Forms.Button DeleteBatchButton;
        private System.Windows.Forms.ComboBox BatchDropdown;
        private System.Windows.Forms.Button CreateBatchButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox BASGroupBox;
        private System.Windows.Forms.Button DeleteBASButton;
        private System.Windows.Forms.ComboBox BASDropdown;
        private System.Windows.Forms.Button CreateBASButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox TargetBranchDropdown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button DeleteApiButton;
        private System.Windows.Forms.ComboBox ApiDropdown;
        private System.Windows.Forms.Button CreateApiButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

