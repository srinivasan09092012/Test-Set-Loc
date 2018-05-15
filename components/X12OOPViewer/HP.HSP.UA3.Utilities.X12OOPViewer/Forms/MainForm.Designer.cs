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
            this.sourceTextBox = new System.Windows.Forms.TextBox();
            this.parseButton = new System.Windows.Forms.Button();
            this.parseGroupBox = new System.Windows.Forms.GroupBox();
            this.parseTextBox = new System.Windows.Forms.TextBox();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.xmlncRadioButton = new System.Windows.Forms.RadioButton();
            this.xmlcRadioButton = new System.Windows.Forms.RadioButton();
            this.x12wsRadioButton = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.sourceGroupBox.SuspendLayout();
            this.parseGroupBox.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1131, 24);
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
            this.sourceGroupBox.Controls.Add(this.sourceTextBox);
            this.sourceGroupBox.Location = new System.Drawing.Point(12, 27);
            this.sourceGroupBox.Name = "sourceGroupBox";
            this.sourceGroupBox.Size = new System.Drawing.Size(1107, 120);
            this.sourceGroupBox.TabIndex = 1;
            this.sourceGroupBox.TabStop = false;
            this.sourceGroupBox.Text = "X12 String";
            //
            // sourceTextBox
            //
            this.sourceTextBox.AcceptsReturn = true;
            this.sourceTextBox.AcceptsTab = true;
            this.sourceTextBox.AllowDrop = true;
            this.sourceTextBox.Location = new System.Drawing.Point(6, 19);
            this.sourceTextBox.MaxLength = 0;
            this.sourceTextBox.Multiline = true;
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.sourceTextBox.Size = new System.Drawing.Size(1095, 95);
            this.sourceTextBox.TabIndex = 0;
            //
            // parseButton
            //
            this.parseButton.Location = new System.Drawing.Point(1038, 171);
            this.parseButton.Name = "parseButton";
            this.parseButton.Size = new System.Drawing.Size(75, 23);
            this.parseButton.TabIndex = 2;
            this.parseButton.Text = "Parse";
            this.parseButton.UseVisualStyleBackColor = true;
            this.parseButton.Click += new System.EventHandler(this.ParseButton_Click);
            //
            // parseGroupBox
            //
            this.parseGroupBox.Controls.Add(this.parseTextBox);
            this.parseGroupBox.Location = new System.Drawing.Point(12, 207);
            this.parseGroupBox.Name = "parseGroupBox";
            this.parseGroupBox.Size = new System.Drawing.Size(1107, 384);
            this.parseGroupBox.TabIndex = 3;
            this.parseGroupBox.TabStop = false;
            this.parseGroupBox.Text = "Parsed X12 String";
            //
            // parseTextBox
            //
            this.parseTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parseTextBox.Location = new System.Drawing.Point(6, 19);
            this.parseTextBox.MaxLength = 0;
            this.parseTextBox.Multiline = true;
            this.parseTextBox.Name = "parseTextBox";
            this.parseTextBox.ReadOnly = true;
            this.parseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.parseTextBox.Size = new System.Drawing.Size(1095, 359);
            this.parseTextBox.TabIndex = 0;
            //
            // optionsGroupBox
            //
            this.optionsGroupBox.Controls.Add(this.xmlncRadioButton);
            this.optionsGroupBox.Controls.Add(this.xmlcRadioButton);
            this.optionsGroupBox.Controls.Add(this.x12wsRadioButton);
            this.optionsGroupBox.Location = new System.Drawing.Point(12, 154);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(555, 47);
            this.optionsGroupBox.TabIndex = 4;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
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
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 603);
            this.Controls.Add(this.optionsGroupBox);
            this.Controls.Add(this.parseGroupBox);
            this.Controls.Add(this.parseButton);
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
            this.parseGroupBox.ResumeLayout(false);
            this.parseGroupBox.PerformLayout();
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
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
        private System.Windows.Forms.GroupBox parseGroupBox;
        private System.Windows.Forms.TextBox parseTextBox;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.RadioButton xmlncRadioButton;
        private System.Windows.Forms.RadioButton xmlcRadioButton;
        private System.Windows.Forms.RadioButton x12wsRadioButton;
    }
}

