//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil
{
    public partial class DatalistComparer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Button btnSourceFile;
        private System.Windows.Forms.OpenFileDialog openDatalistFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTargetConnection;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView sourceTreeList;
        private System.Windows.Forms.TreeView targetTreeList;
        private System.Windows.Forms.Button btnLoadTarget;
        private System.Windows.Forms.ComboBox tenantList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deltaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datalistItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ComboBox moduleList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSourceConnection;
        private System.Windows.Forms.ComboBox sourceModuleList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sourceTenantList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSourceLoad;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.leftPanel = new System.Windows.Forms.Panel();
            this.SourceControlNames = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.sourceModuleList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sourceTreeList = new System.Windows.Forms.TreeView();
            this.sourceTenantList = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSourceConnection = new System.Windows.Forms.TextBox();
            this.btnSourceLoad = new System.Windows.Forms.Button();
            this.btnSourceFile = new System.Windows.Forms.Button();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.TargetControlNames = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.moduleList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tenantList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoadTarget = new System.Windows.Forms.Button();
            this.targetTreeList = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTargetConnection = new System.Windows.Forms.TextBox();
            this.openDatalistFile = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deltaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datalistItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.htmlBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.SourceControlNames);
            this.leftPanel.Controls.Add(this.label7);
            this.leftPanel.Controls.Add(this.sourceModuleList);
            this.leftPanel.Controls.Add(this.label5);
            this.leftPanel.Controls.Add(this.label2);
            this.leftPanel.Controls.Add(this.sourceTreeList);
            this.leftPanel.Controls.Add(this.sourceTenantList);
            this.leftPanel.Controls.Add(this.label6);
            this.leftPanel.Controls.Add(this.txtSourceConnection);
            this.leftPanel.Controls.Add(this.btnSourceLoad);
            this.leftPanel.Controls.Add(this.btnSourceFile);
            this.leftPanel.Location = new System.Drawing.Point(0, 3);
            this.leftPanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(538, 673);
            this.leftPanel.TabIndex = 0;
            // 
            // SourceControlNames
            // 
            this.SourceControlNames.DisplayMember = "SourceControlNames";
            this.SourceControlNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SourceControlNames.FormattingEnabled = true;
            this.SourceControlNames.Location = new System.Drawing.Point(392, 27);
            this.SourceControlNames.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SourceControlNames.Name = "SourceControlNames";
            this.SourceControlNames.Size = new System.Drawing.Size(88, 21);
            this.SourceControlNames.Sorted = true;
            this.SourceControlNames.TabIndex = 36;
            this.SourceControlNames.ValueMember = "TenantID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(337, 31);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Control:";
            // 
            // sourceModuleList
            // 
            this.sourceModuleList.DisplayMember = "ModuleName";
            this.sourceModuleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourceModuleList.FormattingEnabled = true;
            this.sourceModuleList.Location = new System.Drawing.Point(206, 27);
            this.sourceModuleList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.sourceModuleList.Name = "sourceModuleList";
            this.sourceModuleList.Size = new System.Drawing.Size(127, 21);
            this.sourceModuleList.Sorted = true;
            this.sourceModuleList.TabIndex = 31;
            this.sourceModuleList.ValueMember = "TenantModuleID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 6);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Source:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(159, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Module:";
            // 
            // sourceTreeList
            // 
            this.sourceTreeList.Location = new System.Drawing.Point(-1, 52);
            this.sourceTreeList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.sourceTreeList.Name = "sourceTreeList";
            this.sourceTreeList.Size = new System.Drawing.Size(534, 620);
            this.sourceTreeList.TabIndex = 5;
            // 
            // sourceTenantList
            // 
            this.sourceTenantList.DisplayMember = "TenantName";
            this.sourceTenantList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourceTenantList.FormattingEnabled = true;
            this.sourceTenantList.Location = new System.Drawing.Point(48, 28);
            this.sourceTenantList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.sourceTenantList.Name = "sourceTenantList";
            this.sourceTenantList.Size = new System.Drawing.Size(107, 21);
            this.sourceTenantList.Sorted = true;
            this.sourceTenantList.TabIndex = 29;
            this.sourceTenantList.ValueMember = "TenantID";
            this.sourceTenantList.SelectedIndexChanged += new System.EventHandler(this.SourceTenantList_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Tenant:";
            // 
            // txtSourceConnection
            // 
            this.txtSourceConnection.Location = new System.Drawing.Point(57, 4);
            this.txtSourceConnection.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSourceConnection.Name = "txtSourceConnection";
            this.txtSourceConnection.ReadOnly = true;
            this.txtSourceConnection.Size = new System.Drawing.Size(443, 20);
            this.txtSourceConnection.TabIndex = 27;
            // 
            // btnSourceLoad
            // 
            this.btnSourceLoad.Location = new System.Drawing.Point(484, 26);
            this.btnSourceLoad.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSourceLoad.Name = "btnSourceLoad";
            this.btnSourceLoad.Size = new System.Drawing.Size(46, 25);
            this.btnSourceLoad.TabIndex = 27;
            this.btnSourceLoad.Text = "Load";
            this.btnSourceLoad.UseVisualStyleBackColor = true;
            this.btnSourceLoad.Click += new System.EventHandler(this.BtnSourceLoad_Click);
            // 
            // btnSourceFile
            // 
            this.btnSourceFile.Location = new System.Drawing.Point(503, 4);
            this.btnSourceFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSourceFile.Name = "btnSourceFile";
            this.btnSourceFile.Size = new System.Drawing.Size(24, 19);
            this.btnSourceFile.TabIndex = 2;
            this.btnSourceFile.UseVisualStyleBackColor = true;
            this.btnSourceFile.Click += new System.EventHandler(this.BtnSourceFile_Click);
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.TargetControlNames);
            this.rightPanel.Controls.Add(this.label8);
            this.rightPanel.Controls.Add(this.moduleList);
            this.rightPanel.Controls.Add(this.label4);
            this.rightPanel.Controls.Add(this.tenantList);
            this.rightPanel.Controls.Add(this.label3);
            this.rightPanel.Controls.Add(this.btnLoadTarget);
            this.rightPanel.Controls.Add(this.targetTreeList);
            this.rightPanel.Controls.Add(this.label1);
            this.rightPanel.Controls.Add(this.txtTargetConnection);
            this.rightPanel.Location = new System.Drawing.Point(2, 3);
            this.rightPanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(523, 671);
            this.rightPanel.TabIndex = 1;
            // 
            // TargetControlNames
            // 
            this.TargetControlNames.DisplayMember = "TargetControlNames";
            this.TargetControlNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TargetControlNames.FormattingEnabled = true;
            this.TargetControlNames.Location = new System.Drawing.Point(383, 27);
            this.TargetControlNames.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TargetControlNames.Name = "TargetControlNames";
            this.TargetControlNames.Size = new System.Drawing.Size(88, 21);
            this.TargetControlNames.Sorted = true;
            this.TargetControlNames.TabIndex = 35;
            this.TargetControlNames.ValueMember = "TenantID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(329, 31);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Control:";
            // 
            // moduleList
            // 
            this.moduleList.DisplayMember = "ModuleName";
            this.moduleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleList.FormattingEnabled = true;
            this.moduleList.Location = new System.Drawing.Point(197, 27);
            this.moduleList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.moduleList.Name = "moduleList";
            this.moduleList.Size = new System.Drawing.Size(128, 21);
            this.moduleList.Sorted = true;
            this.moduleList.TabIndex = 26;
            this.moduleList.ValueMember = "TenantModuleID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(147, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Module:";
            // 
            // tenantList
            // 
            this.tenantList.DisplayMember = "TenantName";
            this.tenantList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tenantList.FormattingEnabled = true;
            this.tenantList.Location = new System.Drawing.Point(48, 27);
            this.tenantList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tenantList.Name = "tenantList";
            this.tenantList.Size = new System.Drawing.Size(95, 21);
            this.tenantList.Sorted = true;
            this.tenantList.TabIndex = 24;
            this.tenantList.ValueMember = "TenantID";
            this.tenantList.SelectedIndexChanged += new System.EventHandler(this.TenantList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Tenant:";
            // 
            // btnLoadTarget
            // 
            this.btnLoadTarget.Location = new System.Drawing.Point(475, 26);
            this.btnLoadTarget.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnLoadTarget.Name = "btnLoadTarget";
            this.btnLoadTarget.Size = new System.Drawing.Size(46, 25);
            this.btnLoadTarget.TabIndex = 7;
            this.btnLoadTarget.Text = "Load";
            this.btnLoadTarget.UseVisualStyleBackColor = true;
            this.btnLoadTarget.Click += new System.EventHandler(this.BtnLoadTarget_Click);
            // 
            // targetTreeList
            // 
            this.targetTreeList.Location = new System.Drawing.Point(2, 52);
            this.targetTreeList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.targetTreeList.Name = "targetTreeList";
            this.targetTreeList.Size = new System.Drawing.Size(521, 620);
            this.targetTreeList.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Target:";
            // 
            // txtTargetConnection
            // 
            this.txtTargetConnection.Location = new System.Drawing.Point(58, 4);
            this.txtTargetConnection.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTargetConnection.Name = "txtTargetConnection";
            this.txtTargetConnection.ReadOnly = true;
            this.txtTargetConnection.Size = new System.Drawing.Size(464, 20);
            this.txtTargetConnection.TabIndex = 2;
            // 
            // openDatalistFile
            // 
            this.openDatalistFile.FileName = "openFileDialog1";
            this.openDatalistFile.Filter = "Datalist files|*.list";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(9, 22);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.leftPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rightPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1068, 682);
            this.splitContainer1.SplitterDistance = 529;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1069, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deltaToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // deltaToolStripMenuItem
            // 
            this.deltaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datalistItemToolStripMenuItem,
            this.htmlBlockToolStripMenuItem,
			this.menusToolStripMenuItem});
            this.deltaToolStripMenuItem.Name = "deltaToolStripMenuItem";
            this.deltaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deltaToolStripMenuItem.Text = "Delta";
            // 
            // datalistItemToolStripMenuItem
            // 
            this.datalistItemToolStripMenuItem.Name = "datalistItemToolStripMenuItem";
            this.datalistItemToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.datalistItemToolStripMenuItem.Text = "Datalist";
            this.datalistItemToolStripMenuItem.Click += new System.EventHandler(this.DatalistItemToolStripMenuItem_Click);
            // 
            // menusToolStripMenuItem
            // 
            this.menusToolStripMenuItem.Name = "menusToolStripMenuItem";
            this.menusToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.menusToolStripMenuItem.Text = "Menus";
            this.menusToolStripMenuItem.Click += new System.EventHandler(this.MenusToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // htmlBlockToolStripMenuItem
            // 
            this.htmlBlockToolStripMenuItem.Name = "htmlBlockToolStripMenuItem";
            this.htmlBlockToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.htmlBlockToolStripMenuItem.Text = "HtmlBlock";
            this.htmlBlockToolStripMenuItem.Click += new System.EventHandler(this.HtmlBlockToolStripMenuItem_Click);
            // 
            // DatalistComparer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 573);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "DatalistComparer";
            this.Text = "Datalist Comparer";
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem menusToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox TargetControlNames;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox SourceControlNames;
        private System.Windows.Forms.ToolStripMenuItem htmlBlockToolStripMenuItem;
    }
}