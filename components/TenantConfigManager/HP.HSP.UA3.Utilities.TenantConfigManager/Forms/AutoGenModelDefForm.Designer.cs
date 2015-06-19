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
            this.TypeTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AutoGenModelButton = new System.Windows.Forms.Button();
            this.ScopeTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DisplaySizeDropdown = new System.Windows.Forms.ComboBox();
            this.CancelProcButton = new System.Windows.Forms.Button();
            this.CascasdeCheckbox = new System.Windows.Forms.CheckBox();
            this.GenerateLabelsCheckBox = new System.Windows.Forms.CheckBox();
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
            this.ModeDropdown.Location = new System.Drawing.Point(86, 38);
            this.ModeDropdown.Name = "ModeDropdown";
            this.ModeDropdown.Size = new System.Drawing.Size(99, 21);
            this.ModeDropdown.TabIndex = 3;
            this.ModeDropdown.SelectedIndexChanged += new System.EventHandler(this.ModeDropdown_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Mode";
            // 
            // TypeTextbox
            // 
            this.TypeTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TypeTextbox.Location = new System.Drawing.Point(86, 12);
            this.TypeTextbox.Name = "TypeTextbox";
            this.TypeTextbox.Size = new System.Drawing.Size(625, 20);
            this.TypeTextbox.TabIndex = 1;
            this.TypeTextbox.TextChanged += new System.EventHandler(this.TypeTextbox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Type";
            // 
            // AutoGenModelButton
            // 
            this.AutoGenModelButton.Enabled = false;
            this.AutoGenModelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AutoGenModelButton.Location = new System.Drawing.Point(539, 133);
            this.AutoGenModelButton.Name = "AutoGenModelButton";
            this.AutoGenModelButton.Size = new System.Drawing.Size(103, 22);
            this.AutoGenModelButton.TabIndex = 8;
            this.AutoGenModelButton.Text = "Auto-Generate";
            this.AutoGenModelButton.UseVisualStyleBackColor = true;
            this.AutoGenModelButton.Click += new System.EventHandler(this.AutoGenModelButton_Click);
            // 
            // ScopeTextbox
            // 
            this.ScopeTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScopeTextbox.Location = new System.Drawing.Point(86, 65);
            this.ScopeTextbox.Name = "ScopeTextbox";
            this.ScopeTextbox.Size = new System.Drawing.Size(309, 20);
            this.ScopeTextbox.TabIndex = 5;
            this.ScopeTextbox.Text = "*";
            this.ScopeTextbox.TextChanged += new System.EventHandler(this.ScopeTextbox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scope";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 6;
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
            this.DisplaySizeDropdown.Location = new System.Drawing.Point(86, 95);
            this.DisplaySizeDropdown.Name = "DisplaySizeDropdown";
            this.DisplaySizeDropdown.Size = new System.Drawing.Size(99, 21);
            this.DisplaySizeDropdown.TabIndex = 7;
            this.DisplaySizeDropdown.SelectedIndexChanged += new System.EventHandler(this.DisplaySizeDropdown_SelectedIndexChanged);
            // 
            // CancelProcButton
            // 
            this.CancelProcButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelProcButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelProcButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelProcButton.Location = new System.Drawing.Point(648, 133);
            this.CancelProcButton.Name = "CancelProcButton";
            this.CancelProcButton.Size = new System.Drawing.Size(75, 23);
            this.CancelProcButton.TabIndex = 9;
            this.CancelProcButton.Text = "Cancel";
            this.CancelProcButton.UseVisualStyleBackColor = true;
            this.CancelProcButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CascasdeCheckbox
            // 
            this.CascasdeCheckbox.AutoSize = true;
            this.CascasdeCheckbox.Checked = true;
            this.CascasdeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CascasdeCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CascasdeCheckbox.Location = new System.Drawing.Point(86, 133);
            this.CascasdeCheckbox.Name = "CascasdeCheckbox";
            this.CascasdeCheckbox.Size = new System.Drawing.Size(182, 17);
            this.CascasdeCheckbox.TabIndex = 10;
            this.CascasdeCheckbox.Text = "Cascade Generate Missing Types";
            this.CascasdeCheckbox.UseVisualStyleBackColor = true;
            // 
            // GenerateLabelsCheckBox
            // 
            this.GenerateLabelsCheckBox.AutoSize = true;
            this.GenerateLabelsCheckBox.Checked = true;
            this.GenerateLabelsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GenerateLabelsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateLabelsCheckBox.Location = new System.Drawing.Point(291, 133);
            this.GenerateLabelsCheckBox.Name = "GenerateLabelsCheckBox";
            this.GenerateLabelsCheckBox.Size = new System.Drawing.Size(101, 17);
            this.GenerateLabelsCheckBox.TabIndex = 11;
            this.GenerateLabelsCheckBox.Text = "Generate Labels";
            this.GenerateLabelsCheckBox.UseVisualStyleBackColor = true;
            // 
            // AutoGenModelDefForm
            // 
            this.AcceptButton = this.AutoGenModelButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelProcButton;
            this.ClientSize = new System.Drawing.Size(735, 168);
            this.Controls.Add(this.GenerateLabelsCheckBox);
            this.Controls.Add(this.CascasdeCheckbox);
            this.Controls.Add(this.CancelProcButton);
            this.Controls.Add(this.DisplaySizeDropdown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ScopeTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ModeDropdown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TypeTextbox);
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
        private System.Windows.Forms.TextBox TypeTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button AutoGenModelButton;
        private System.Windows.Forms.TextBox ScopeTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox DisplaySizeDropdown;
        private System.Windows.Forms.Button CancelProcButton;
        private System.Windows.Forms.CheckBox CascasdeCheckbox;
        private System.Windows.Forms.CheckBox GenerateLabelsCheckBox;
    }
}