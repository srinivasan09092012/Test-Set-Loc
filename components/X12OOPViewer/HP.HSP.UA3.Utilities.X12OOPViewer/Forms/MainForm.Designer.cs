namespace HP.HSP.UA3.Utilities.X12OOPViewer.Forms
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
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hELPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutX12OOPViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceGroupBox = new System.Windows.Forms.GroupBox();
            this.ClearButton = new System.Windows.Forms.Button();
            this.sourceTextBox = new System.Windows.Forms.TextBox();
            this.parseButton = new System.Windows.Forms.Button();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.htmlRadioButton = new System.Windows.Forms.RadioButton();
            this.xmlncRadioButton = new System.Windows.Forms.RadioButton();
            this.xmlcRadioButton = new System.Windows.Forms.RadioButton();
            this.x12wsRadioButton = new System.Windows.Forms.RadioButton();
            this.parseRichTextBox = new System.Windows.Forms.RichTextBox();
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.findButton = new System.Windows.Forms.Button();
            this.findCountLabel = new System.Windows.Forms.Label();
            this.parsedWebBrowser = new System.Windows.Forms.WebBrowser();
            this.ValidateButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ParseTabPage = new System.Windows.Forms.TabPage();
            this.ValidationTabPage = new System.Windows.Forms.TabPage();
            this.validateRichTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.sourceGroupBox.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ParseTabPage.SuspendLayout();
            this.ValidationTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem,
            this.hELPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1006, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            this.fIleToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fIleToolStripMenuItem.Text = "&FILE";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // hELPToolStripMenuItem
            // 
            this.hELPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutX12OOPViewerToolStripMenuItem});
            this.hELPToolStripMenuItem.Name = "hELPToolStripMenuItem";
            this.hELPToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.hELPToolStripMenuItem.Text = "HELP";
            // 
            // aboutX12OOPViewerToolStripMenuItem
            // 
            this.aboutX12OOPViewerToolStripMenuItem.Name = "aboutX12OOPViewerToolStripMenuItem";
            this.aboutX12OOPViewerToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.aboutX12OOPViewerToolStripMenuItem.Text = "A&bout X12 OOP Viewer";
            this.aboutX12OOPViewerToolStripMenuItem.Click += new System.EventHandler(this.AboutX12OOPViewerToolStripMenuItem_Click);
            // 
            // sourceGroupBox
            // 
            this.sourceGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceGroupBox.Controls.Add(this.ClearButton);
            this.sourceGroupBox.Controls.Add(this.sourceTextBox);
            this.sourceGroupBox.Location = new System.Drawing.Point(12, 27);
            this.sourceGroupBox.Name = "sourceGroupBox";
            this.sourceGroupBox.Size = new System.Drawing.Size(991, 214);
            this.sourceGroupBox.TabIndex = 1;
            this.sourceGroupBox.TabStop = false;
            this.sourceGroupBox.Text = "X12 String";
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.Location = new System.Drawing.Point(904, 8);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 1;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.AcceptsReturn = true;
            this.sourceTextBox.AcceptsTab = true;
            this.sourceTextBox.AllowDrop = true;
            this.sourceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceTextBox.Location = new System.Drawing.Point(9, 37);
            this.sourceTextBox.MaxLength = 0;
            this.sourceTextBox.Multiline = true;
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.sourceTextBox.Size = new System.Drawing.Size(973, 171);
            this.sourceTextBox.TabIndex = 0;
            // 
            // parseButton
            // 
            this.parseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.parseButton.Location = new System.Drawing.Point(892, 14);
            this.parseButton.Name = "parseButton";
            this.parseButton.Size = new System.Drawing.Size(75, 23);
            this.parseButton.TabIndex = 2;
            this.parseButton.Text = "Parse";
            this.parseButton.UseVisualStyleBackColor = true;
            this.parseButton.Click += new System.EventHandler(this.ParseButton_Click);
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Controls.Add(this.htmlRadioButton);
            this.optionsGroupBox.Controls.Add(this.xmlncRadioButton);
            this.optionsGroupBox.Controls.Add(this.xmlcRadioButton);
            this.optionsGroupBox.Controls.Add(this.x12wsRadioButton);
            this.optionsGroupBox.Location = new System.Drawing.Point(9, 3);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(564, 47);
            this.optionsGroupBox.TabIndex = 4;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // htmlRadioButton
            // 
            this.htmlRadioButton.AutoSize = true;
            this.htmlRadioButton.Location = new System.Drawing.Point(490, 17);
            this.htmlRadioButton.Name = "htmlRadioButton";
            this.htmlRadioButton.Size = new System.Drawing.Size(55, 17);
            this.htmlRadioButton.TabIndex = 3;
            this.htmlRadioButton.TabStop = true;
            this.htmlRadioButton.Text = "HTML";
            this.htmlRadioButton.UseVisualStyleBackColor = true;
            // 
            // xmlncRadioButton
            // 
            this.xmlncRadioButton.AutoSize = true;
            this.xmlncRadioButton.Location = new System.Drawing.Point(325, 17);
            this.xmlncRadioButton.Name = "xmlncRadioButton";
            this.xmlncRadioButton.Size = new System.Drawing.Size(138, 17);
            this.xmlncRadioButton.TabIndex = 2;
            this.xmlncRadioButton.TabStop = true;
            this.xmlncRadioButton.Text = "XML with No Comments";
            this.xmlncRadioButton.UseVisualStyleBackColor = true;
            // 
            // xmlcRadioButton
            // 
            this.xmlcRadioButton.AutoSize = true;
            this.xmlcRadioButton.Location = new System.Drawing.Point(170, 15);
            this.xmlcRadioButton.Name = "xmlcRadioButton";
            this.xmlcRadioButton.Size = new System.Drawing.Size(121, 17);
            this.xmlcRadioButton.TabIndex = 1;
            this.xmlcRadioButton.TabStop = true;
            this.xmlcRadioButton.Text = "XML with Comments";
            this.xmlcRadioButton.UseVisualStyleBackColor = true;
            // 
            // x12wsRadioButton
            // 
            this.x12wsRadioButton.AutoSize = true;
            this.x12wsRadioButton.Checked = true;
            this.x12wsRadioButton.Location = new System.Drawing.Point(7, 17);
            this.x12wsRadioButton.Name = "x12wsRadioButton";
            this.x12wsRadioButton.Size = new System.Drawing.Size(131, 17);
            this.x12wsRadioButton.TabIndex = 0;
            this.x12wsRadioButton.TabStop = true;
            this.x12wsRadioButton.Text = "X12 with White Space";
            this.x12wsRadioButton.UseVisualStyleBackColor = true;
            // 
            // parseRichTextBox
            // 
            this.parseRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.parseRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parseRichTextBox.Location = new System.Drawing.Point(6, 85);
            this.parseRichTextBox.Name = "parseRichTextBox";
            this.parseRichTextBox.ReadOnly = true;
            this.parseRichTextBox.Size = new System.Drawing.Size(961, 373);
            this.parseRichTextBox.TabIndex = 5;
            this.parseRichTextBox.Text = "";
            // 
            // findTextBox
            // 
            this.findTextBox.Location = new System.Drawing.Point(6, 56);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(228, 20);
            this.findTextBox.TabIndex = 6;
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(250, 56);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(75, 23);
            this.findButton.TabIndex = 7;
            this.findButton.Text = "Find";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // findCountLabel
            // 
            this.findCountLabel.AutoSize = true;
            this.findCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findCountLabel.ForeColor = System.Drawing.Color.Red;
            this.findCountLabel.Location = new System.Drawing.Point(331, 64);
            this.findCountLabel.Name = "findCountLabel";
            this.findCountLabel.Size = new System.Drawing.Size(104, 15);
            this.findCountLabel.TabIndex = 10;
            this.findCountLabel.Text = " 0 matches found.";
            // 
            // parsedWebBrowser
            // 
            this.parsedWebBrowser.AllowNavigation = false;
            this.parsedWebBrowser.AllowWebBrowserDrop = false;
            this.parsedWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.parsedWebBrowser.Location = new System.Drawing.Point(9, 85);
            this.parsedWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.parsedWebBrowser.Name = "parsedWebBrowser";
            this.parsedWebBrowser.Size = new System.Drawing.Size(958, 383);
            this.parsedWebBrowser.TabIndex = 11;
            this.parsedWebBrowser.Visible = false;
            this.parsedWebBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // ValidateButton
            // 
            this.ValidateButton.Location = new System.Drawing.Point(892, 6);
            this.ValidateButton.Name = "ValidateButton";
            this.ValidateButton.Size = new System.Drawing.Size(75, 23);
            this.ValidateButton.TabIndex = 12;
            this.ValidateButton.Text = "Validate";
            this.ValidateButton.UseVisualStyleBackColor = true;
            this.ValidateButton.Click += new System.EventHandler(this.ValidateButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.ParseTabPage);
            this.tabControl1.Controls.Add(this.ValidationTabPage);
            this.tabControl1.Location = new System.Drawing.Point(18, 250);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(981, 490);
            this.tabControl1.TabIndex = 13;
            // 
            // ParseTabPage
            // 
            this.ParseTabPage.Controls.Add(this.parseRichTextBox);
            this.ParseTabPage.Controls.Add(this.findCountLabel);
            this.ParseTabPage.Controls.Add(this.parseButton);
            this.ParseTabPage.Controls.Add(this.findButton);
            this.ParseTabPage.Controls.Add(this.parsedWebBrowser);
            this.ParseTabPage.Controls.Add(this.optionsGroupBox);
            this.ParseTabPage.Controls.Add(this.findTextBox);
            this.ParseTabPage.Location = new System.Drawing.Point(4, 22);
            this.ParseTabPage.Name = "ParseTabPage";
            this.ParseTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ParseTabPage.Size = new System.Drawing.Size(973, 464);
            this.ParseTabPage.TabIndex = 0;
            this.ParseTabPage.Text = "Parse";
            this.ParseTabPage.UseVisualStyleBackColor = true;
            // 
            // ValidationTabPage
            // 
            this.ValidationTabPage.Controls.Add(this.validateRichTextBox);
            this.ValidationTabPage.Controls.Add(this.ValidateButton);
            this.ValidationTabPage.Location = new System.Drawing.Point(4, 22);
            this.ValidationTabPage.Name = "ValidationTabPage";
            this.ValidationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ValidationTabPage.Size = new System.Drawing.Size(973, 464);
            this.ValidationTabPage.TabIndex = 1;
            this.ValidationTabPage.Text = "Validate";
            this.ValidationTabPage.UseVisualStyleBackColor = true;
            // 
            // validateRichTextBox
            // 
            this.validateRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.validateRichTextBox.Location = new System.Drawing.Point(6, 35);
            this.validateRichTextBox.Name = "validateRichTextBox";
            this.validateRichTextBox.ReadOnly = true;
            this.validateRichTextBox.Size = new System.Drawing.Size(961, 423);
            this.validateRichTextBox.TabIndex = 13;
            this.validateRichTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 752);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.sourceGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1022, 597);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "X12 OOP Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.sourceGroupBox.ResumeLayout(false);
            this.sourceGroupBox.PerformLayout();
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ParseTabPage.ResumeLayout(false);
            this.ParseTabPage.PerformLayout();
            this.ValidationTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hELPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutX12OOPViewerToolStripMenuItem;
        private System.Windows.Forms.GroupBox sourceGroupBox;
        private System.Windows.Forms.TextBox sourceTextBox;
        private System.Windows.Forms.Button parseButton;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.RadioButton xmlncRadioButton;
        private System.Windows.Forms.RadioButton xmlcRadioButton;
        private System.Windows.Forms.RadioButton x12wsRadioButton;
        private System.Windows.Forms.RichTextBox parseRichTextBox;
        private System.Windows.Forms.TextBox findTextBox;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.Label findCountLabel;
        private System.Windows.Forms.RadioButton htmlRadioButton;
        private System.Windows.Forms.WebBrowser parsedWebBrowser;
        private System.Windows.Forms.Button ValidateButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ParseTabPage;
        private System.Windows.Forms.TabPage ValidationTabPage;
        private System.Windows.Forms.RichTextBox validateRichTextBox;
        private System.Windows.Forms.Button ClearButton;
    }
}

