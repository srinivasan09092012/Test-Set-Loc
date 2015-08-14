namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class AutoGenModelDefForm
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
            this.ModeDropdown = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ClassTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AutoGenModelButton = new System.Windows.Forms.Button();
            this.ScopeTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DisplaySizeDropdown = new System.Windows.Forms.ComboBox();
            this.CancelProcButton = new System.Windows.Forms.Button();
            this.CascasdeCheckbox = new System.Windows.Forms.CheckBox();
            this.GenerateLabelsCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TypeDropdown = new System.Windows.Forms.ComboBox();
            this.ClassDropdown = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ModeDropdown
            // 
            this.ModeDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModeDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ModeDropdown.FormattingEnabled = true;
            this.ModeDropdown.Items.AddRange(new object[] {
            "Debug",
            "Release"});
            this.ModeDropdown.Location = new System.Drawing.Point(86, 71);
            this.ModeDropdown.Name = "ModeDropdown";
            this.ModeDropdown.Size = new System.Drawing.Size(99, 21);
            this.ModeDropdown.TabIndex = 6;
            this.ModeDropdown.SelectedIndexChanged += new System.EventHandler(this.ModeDropdown_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Mode";
            // 
            // ClassTextbox
            // 
            this.ClassTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ClassTextbox.Location = new System.Drawing.Point(86, 45);
            this.ClassTextbox.Name = "ClassTextbox";
            this.ClassTextbox.Size = new System.Drawing.Size(625, 20);
            this.ClassTextbox.TabIndex = 3;
            this.ClassTextbox.TextChanged += new System.EventHandler(this.TypeTextbox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Class";
            // 
            // AutoGenModelButton
            // 
            this.AutoGenModelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoGenModelButton.Enabled = false;
            this.AutoGenModelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AutoGenModelButton.Location = new System.Drawing.Point(539, 162);
            this.AutoGenModelButton.Name = "AutoGenModelButton";
            this.AutoGenModelButton.Size = new System.Drawing.Size(103, 22);
            this.AutoGenModelButton.TabIndex = 13;
            this.AutoGenModelButton.Text = "Auto-Generate";
            this.AutoGenModelButton.UseVisualStyleBackColor = true;
            this.AutoGenModelButton.Click += new System.EventHandler(this.AutoGenModelButton_Click);
            // 
            // ScopeTextbox
            // 
            this.ScopeTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScopeTextbox.Location = new System.Drawing.Point(86, 98);
            this.ScopeTextbox.Name = "ScopeTextbox";
            this.ScopeTextbox.Size = new System.Drawing.Size(309, 20);
            this.ScopeTextbox.TabIndex = 8;
            this.ScopeTextbox.Text = "*";
            this.ScopeTextbox.TextChanged += new System.EventHandler(this.ScopeTextbox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Scope";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Display Size";
            // 
            // DisplaySizeDropdown
            // 
            this.DisplaySizeDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DisplaySizeDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisplaySizeDropdown.FormattingEnabled = true;
            this.DisplaySizeDropdown.Items.AddRange(new object[] {
            "Debug",
            "Release"});
            this.DisplaySizeDropdown.Location = new System.Drawing.Point(86, 128);
            this.DisplaySizeDropdown.Name = "DisplaySizeDropdown";
            this.DisplaySizeDropdown.Size = new System.Drawing.Size(99, 21);
            this.DisplaySizeDropdown.TabIndex = 10;
            this.DisplaySizeDropdown.SelectedIndexChanged += new System.EventHandler(this.DisplaySizeDropdown_SelectedIndexChanged);
            // 
            // CancelProcButton
            // 
            this.CancelProcButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelProcButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelProcButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelProcButton.Location = new System.Drawing.Point(648, 162);
            this.CancelProcButton.Name = "CancelProcButton";
            this.CancelProcButton.Size = new System.Drawing.Size(75, 23);
            this.CancelProcButton.TabIndex = 14;
            this.CancelProcButton.Text = "Cancel";
            this.CancelProcButton.UseVisualStyleBackColor = true;
            this.CancelProcButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CascasdeCheckbox
            // 
            this.CascasdeCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CascasdeCheckbox.AutoSize = true;
            this.CascasdeCheckbox.Checked = true;
            this.CascasdeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CascasdeCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CascasdeCheckbox.Location = new System.Drawing.Point(86, 162);
            this.CascasdeCheckbox.Name = "CascasdeCheckbox";
            this.CascasdeCheckbox.Size = new System.Drawing.Size(182, 17);
            this.CascasdeCheckbox.TabIndex = 11;
            this.CascasdeCheckbox.Text = "Cascade Generate Missing Types";
            this.CascasdeCheckbox.UseVisualStyleBackColor = true;
            // 
            // GenerateLabelsCheckBox
            // 
            this.GenerateLabelsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateLabelsCheckBox.AutoSize = true;
            this.GenerateLabelsCheckBox.Checked = true;
            this.GenerateLabelsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GenerateLabelsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateLabelsCheckBox.Location = new System.Drawing.Point(291, 162);
            this.GenerateLabelsCheckBox.Name = "GenerateLabelsCheckBox";
            this.GenerateLabelsCheckBox.Size = new System.Drawing.Size(101, 17);
            this.GenerateLabelsCheckBox.TabIndex = 12;
            this.GenerateLabelsCheckBox.Text = "Generate Labels";
            this.GenerateLabelsCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Type";
            // 
            // TypeDropdown
            // 
            this.TypeDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TypeDropdown.FormattingEnabled = true;
            this.TypeDropdown.Items.AddRange(new object[] {
            "Current Module",
            "Core"});
            this.TypeDropdown.Location = new System.Drawing.Point(86, 12);
            this.TypeDropdown.Name = "TypeDropdown";
            this.TypeDropdown.Size = new System.Drawing.Size(146, 21);
            this.TypeDropdown.TabIndex = 1;
            this.TypeDropdown.SelectedIndexChanged += new System.EventHandler(this.TypeDropdown_SelectedIndexChanged);
            // 
            // ClassDropdown
            // 
            this.ClassDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClassDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClassDropdown.FormattingEnabled = true;
            this.ClassDropdown.Location = new System.Drawing.Point(86, 44);
            this.ClassDropdown.Name = "ClassDropdown";
            this.ClassDropdown.Size = new System.Drawing.Size(625, 21);
            this.ClassDropdown.TabIndex = 4;
            this.ClassDropdown.Visible = false;
            // 
            // AutoGenModelDefForm
            // 
            this.AcceptButton = this.AutoGenModelButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelProcButton;
            this.ClientSize = new System.Drawing.Size(735, 197);
            this.Controls.Add(this.ClassDropdown);
            this.Controls.Add(this.TypeDropdown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GenerateLabelsCheckBox);
            this.Controls.Add(this.CascasdeCheckbox);
            this.Controls.Add(this.CancelProcButton);
            this.Controls.Add(this.DisplaySizeDropdown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ScopeTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ModeDropdown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ClassTextbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AutoGenModelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutoGenModelDefForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Auto-Generate New Model Definition";
            this.Load += new System.EventHandler(this.AutoGenModelDefForm_Load);
            this.Shown += new System.EventHandler(this.AutoGenModelDefForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ModeDropdown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ClassTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button AutoGenModelButton;
        private System.Windows.Forms.TextBox ScopeTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox DisplaySizeDropdown;
        private System.Windows.Forms.Button CancelProcButton;
        private System.Windows.Forms.CheckBox CascasdeCheckbox;
        private System.Windows.Forms.CheckBox GenerateLabelsCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TypeDropdown;
        private System.Windows.Forms.ComboBox ClassDropdown;
    }
}