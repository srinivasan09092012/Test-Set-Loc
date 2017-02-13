namespace DatalistSyncUtil
{
    partial class DatalistComparer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatalistComparer));
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.SourceTreeList = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSourceFile = new System.Windows.Forms.Button();
            this.txtSourceFile = new System.Windows.Forms.TextBox();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.ModuleList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TenantList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoadTarget = new System.Windows.Forms.Button();
            this.TargetTreeList = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTargetConnection = new System.Windows.Forms.TextBox();
            this.OpenDatalistFile = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deltaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datalistItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LeftPanel.SuspendLayout();
            this.RightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.SourceTreeList);
            this.LeftPanel.Controls.Add(this.label2);
            this.LeftPanel.Controls.Add(this.btnSourceFile);
            this.LeftPanel.Controls.Add(this.txtSourceFile);
            this.LeftPanel.Location = new System.Drawing.Point(0, 3);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(717, 829);
            this.LeftPanel.TabIndex = 0;
            // 
            // SourceTreeList
            // 
            this.SourceTreeList.Location = new System.Drawing.Point(-1, 64);
            this.SourceTreeList.Name = "SourceTreeList";
            this.SourceTreeList.Size = new System.Drawing.Size(711, 762);
            this.SourceTreeList.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Source File:";
            // 
            // btnSourceFile
            // 
            this.btnSourceFile.Image = ((System.Drawing.Image)(resources.GetObject("btnSourceFile.Image")));
            this.btnSourceFile.Location = new System.Drawing.Point(671, 31);
            this.btnSourceFile.Name = "btnSourceFile";
            this.btnSourceFile.Size = new System.Drawing.Size(32, 23);
            this.btnSourceFile.TabIndex = 2;
            this.btnSourceFile.UseVisualStyleBackColor = true;
            this.btnSourceFile.Click += new System.EventHandler(this.btnSourceFile_Click);
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.Location = new System.Drawing.Point(98, 32);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.ReadOnly = true;
            this.txtSourceFile.Size = new System.Drawing.Size(567, 22);
            this.txtSourceFile.TabIndex = 1;
            // 
            // RightPanel
            // 
            this.RightPanel.Controls.Add(this.ModuleList);
            this.RightPanel.Controls.Add(this.label4);
            this.RightPanel.Controls.Add(this.TenantList);
            this.RightPanel.Controls.Add(this.label3);
            this.RightPanel.Controls.Add(this.btnLoadTarget);
            this.RightPanel.Controls.Add(this.TargetTreeList);
            this.RightPanel.Controls.Add(this.label1);
            this.RightPanel.Controls.Add(this.txtTargetConnection);
            this.RightPanel.Location = new System.Drawing.Point(3, 3);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(697, 826);
            this.RightPanel.TabIndex = 1;
            // 
            // ModuleList
            // 
            this.ModuleList.DisplayMember = "ModuleName";
            this.ModuleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModuleList.FormattingEnabled = true;
            this.ModuleList.Location = new System.Drawing.Point(350, 33);
            this.ModuleList.Name = "ModuleList";
            this.ModuleList.Size = new System.Drawing.Size(278, 24);
            this.ModuleList.Sorted = true;
            this.ModuleList.TabIndex = 26;
            this.ModuleList.ValueMember = "TenantModuleID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(284, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 25;
            this.label4.Text = "Module:";
            // 
            // TenantList
            // 
            this.TenantList.DisplayMember = "TenantName";
            this.TenantList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TenantList.FormattingEnabled = true;
            this.TenantList.Location = new System.Drawing.Point(77, 34);
            this.TenantList.Name = "TenantList";
            this.TenantList.Size = new System.Drawing.Size(202, 24);
            this.TenantList.Sorted = true;
            this.TenantList.TabIndex = 24;
            this.TenantList.ValueMember = "TenantID";
            this.TenantList.SelectedIndexChanged += new System.EventHandler(this.TenantList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Tenant:";
            // 
            // btnLoadTarget
            // 
            this.btnLoadTarget.Location = new System.Drawing.Point(634, 32);
            this.btnLoadTarget.Name = "btnLoadTarget";
            this.btnLoadTarget.Size = new System.Drawing.Size(51, 30);
            this.btnLoadTarget.TabIndex = 7;
            this.btnLoadTarget.Text = "Load";
            this.btnLoadTarget.UseVisualStyleBackColor = true;
            this.btnLoadTarget.Click += new System.EventHandler(this.btnLoadTarget_Click);
            // 
            // TargetTreeList
            // 
            this.TargetTreeList.Location = new System.Drawing.Point(3, 64);
            this.TargetTreeList.Name = "TargetTreeList";
            this.TargetTreeList.Size = new System.Drawing.Size(694, 762);
            this.TargetTreeList.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Target:";
            // 
            // txtTargetConnection
            // 
            this.txtTargetConnection.Location = new System.Drawing.Point(77, 5);
            this.txtTargetConnection.Name = "txtTargetConnection";
            this.txtTargetConnection.ReadOnly = true;
            this.txtTargetConnection.Size = new System.Drawing.Size(617, 22);
            this.txtTargetConnection.TabIndex = 2;
            // 
            // OpenDatalistFile
            // 
            this.OpenDatalistFile.FileName = "openFileDialog1";
            this.OpenDatalistFile.Filter = "Datalist files|*.list";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LeftPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.RightPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1424, 839);
            this.splitContainer1.SplitterDistance = 706;
            this.splitContainer1.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1461, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deltaToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // deltaToolStripMenuItem
            // 
            this.deltaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datalistItemToolStripMenuItem});
            this.deltaToolStripMenuItem.Name = "deltaToolStripMenuItem";
            this.deltaToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.deltaToolStripMenuItem.Text = "Delta";
            // 
            // datalistItemToolStripMenuItem
            // 
            this.datalistItemToolStripMenuItem.Name = "datalistItemToolStripMenuItem";
            this.datalistItemToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.datalistItemToolStripMenuItem.Text = "Datalist Item";
            this.datalistItemToolStripMenuItem.Click += new System.EventHandler(this.datalistItemToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // DatalistComparer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 871);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "DatalistComparer";
            this.Text = "Datalist Comparer";
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            this.RightPanel.ResumeLayout(false);
            this.RightPanel.PerformLayout();
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

        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Button btnSourceFile;
        private System.Windows.Forms.TextBox txtSourceFile;
        private System.Windows.Forms.OpenFileDialog OpenDatalistFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTargetConnection;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView SourceTreeList;
        private System.Windows.Forms.TreeView TargetTreeList;
        private System.Windows.Forms.Button btnLoadTarget;
        private System.Windows.Forms.ComboBox TenantList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deltaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datalistItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ComboBox ModuleList;
        private System.Windows.Forms.Label label4;
    }
}