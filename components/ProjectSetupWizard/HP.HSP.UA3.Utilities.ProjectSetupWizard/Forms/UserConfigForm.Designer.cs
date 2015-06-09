namespace HP.HSP.UA3.Utilities.ProjectSetupWizard.Forms
{
    partial class UserConfigForm
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SourceDirectoryBrowseButton = new System.Windows.Forms.Button();
            this.SourceDirectoryTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Location = new System.Drawing.Point(632, 119);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.SourceDirectoryBrowseButton);
            this.groupBox1.Controls.Add(this.SourceDirectoryTextbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(695, 101);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Paths";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(109, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(424, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "example: C:\\Projects\\HP\\USHC_AMERICAS_US_ADU_HSP_UA3\\Source";
            // 
            // SourceDirectoryBrowseButton
            // 
            this.SourceDirectoryBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SourceDirectoryBrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SourceDirectoryBrowseButton.Location = new System.Drawing.Point(652, 30);
            this.SourceDirectoryBrowseButton.Name = "SourceDirectoryBrowseButton";
            this.SourceDirectoryBrowseButton.Size = new System.Drawing.Size(32, 20);
            this.SourceDirectoryBrowseButton.TabIndex = 2;
            this.SourceDirectoryBrowseButton.Text = "...";
            this.SourceDirectoryBrowseButton.UseVisualStyleBackColor = true;
            this.SourceDirectoryBrowseButton.Click += new System.EventHandler(this.SourceDirectoryBrowseButton_Click);
            // 
            // SourceDirectoryTextbox
            // 
            this.SourceDirectoryTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SourceDirectoryTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SourceDirectoryTextbox.Location = new System.Drawing.Point(109, 30);
            this.SourceDirectoryTextbox.Name = "SourceDirectoryTextbox";
            this.SourceDirectoryTextbox.ReadOnly = true;
            this.SourceDirectoryTextbox.Size = new System.Drawing.Size(537, 20);
            this.SourceDirectoryTextbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source Directory";
            // 
            // UserConfigForm
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 154);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(735, 192);
            this.Name = "UserConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Configuration";
            this.Load += new System.EventHandler(this.UserConfigForm_Load);
            this.Shown += new System.EventHandler(this.UserConfigForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SourceDirectoryBrowseButton;
        private System.Windows.Forms.TextBox SourceDirectoryTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;

    }
}