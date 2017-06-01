namespace SSRSImportExportWizard
{
    partial class ImportExportMain
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
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateDSMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replacementStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.targetURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportExportPanel = new System.Windows.Forms.Panel();
            this.ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(741, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportMenu,
            this.ImportMenu,
            this.UpdateDSMenu,
            this.ExitMenu});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.fileToolStripMenuItem.Text = "Options";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // ExportMenu
            // 
            this.ExportMenu.Name = "ExportMenu";
            this.ExportMenu.Size = new System.Drawing.Size(214, 26);
            this.ExportMenu.Text = "Export";
            this.ExportMenu.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // ImportMenu
            // 
            this.ImportMenu.Name = "ImportMenu";
            this.ImportMenu.Size = new System.Drawing.Size(214, 26);
            this.ImportMenu.Text = "Import";
            this.ImportMenu.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // UpdateDSMenu
            // 
            this.UpdateDSMenu.Name = "UpdateDSMenu";
            this.UpdateDSMenu.Size = new System.Drawing.Size(214, 26);
            this.UpdateDSMenu.Text = "Update DataSource";
            this.UpdateDSMenu.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replacementStringToolStripMenuItem,
            this.targetURLToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // replacementStringToolStripMenuItem
            // 
            this.replacementStringToolStripMenuItem.Name = "replacementStringToolStripMenuItem";
            this.replacementStringToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.replacementStringToolStripMenuItem.Text = "Replacement String";
            // 
            // targetURLToolStripMenuItem
            // 
            this.targetURLToolStripMenuItem.Name = "targetURLToolStripMenuItem";
            this.targetURLToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.targetURLToolStripMenuItem.Text = "Target URL";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // ImportExportPanel
            // 
            this.ImportExportPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ImportExportPanel.Location = new System.Drawing.Point(12, 43);
            this.ImportExportPanel.Name = "ImportExportPanel";
            this.ImportExportPanel.Size = new System.Drawing.Size(721, 647);
            this.ImportExportPanel.TabIndex = 1;
            // 
            // ExitMenu
            // 
            this.ExitMenu.Name = "ExitMenu";
            this.ExitMenu.Size = new System.Drawing.Size(214, 26);
            this.ExitMenu.Text = "Exit";
            this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
            // 
            // ImportExportMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 702);
            this.Controls.Add(this.ImportExportPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ImportExportMain";
            this.Text = "Import Export Wizard";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replacementStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem targetURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportMenu;
        private System.Windows.Forms.ToolStripMenuItem UpdateDSMenu;
        private System.Windows.Forms.ToolStripMenuItem ExportMenu;
        private System.Windows.Forms.Panel ImportExportPanel;
        private System.Windows.Forms.ToolStripMenuItem ExitMenu;
    }
}

