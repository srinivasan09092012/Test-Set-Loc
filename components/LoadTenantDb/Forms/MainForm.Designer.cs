//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class MainForm
    {
        private System.Windows.Forms.CheckedListBox moduleListBox;
        private System.Windows.Forms.GroupBox moduleGroupBox;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox configTypeComboBox;
        private System.Windows.Forms.Button tenantConfigFileOpen;
        private System.Windows.Forms.TextBox tenantConfigFileTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        
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
            this.moduleListBox = new System.Windows.Forms.CheckedListBox();
            this.moduleGroupBox = new System.Windows.Forms.GroupBox();
            this.nextButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.configTypeComboBox = new System.Windows.Forms.ComboBox();
            this.tenantConfigFileOpen = new System.Windows.Forms.Button();
            this.tenantConfigFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.moduleGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // moduleListBox
            // 
            this.moduleListBox.FormattingEnabled = true;
            this.moduleListBox.Location = new System.Drawing.Point(13, 18);
            this.moduleListBox.Name = "moduleListBox";
            this.moduleListBox.Size = new System.Drawing.Size(260, 139);
            this.moduleListBox.TabIndex = 3;
            this.moduleListBox.SelectedValueChanged += new System.EventHandler(this.ModuleListBox_SelectedValueChanged);
            // 
            // moduleGroupBox
            // 
            this.moduleGroupBox.Controls.Add(this.moduleListBox);
            this.moduleGroupBox.Location = new System.Drawing.Point(7, 97);
            this.moduleGroupBox.Name = "moduleGroupBox";
            this.moduleGroupBox.Size = new System.Drawing.Size(286, 174);
            this.moduleGroupBox.TabIndex = 5;
            this.moduleGroupBox.TabStop = false;
            this.moduleGroupBox.Text = "Module";
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(218, 280);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 4;
            this.nextButton.Text = "&Next >";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButtonClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // configTypeComboBox
            // 
            this.configTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.configTypeComboBox.Location = new System.Drawing.Point(20, 68);
            this.configTypeComboBox.Name = "configTypeComboBox";
            this.configTypeComboBox.Size = new System.Drawing.Size(260, 21);
            this.configTypeComboBox.TabIndex = 2;
            // 
            // tenantConfigFileOpen
            // 
            this.tenantConfigFileOpen.Location = new System.Drawing.Point(245, 23);
            this.tenantConfigFileOpen.Name = "tenantConfigFileOpen";
            this.tenantConfigFileOpen.Size = new System.Drawing.Size(35, 20);
            this.tenantConfigFileOpen.TabIndex = 1;
            this.tenantConfigFileOpen.Text = "...";
            this.tenantConfigFileOpen.UseVisualStyleBackColor = true;
            this.tenantConfigFileOpen.Click += new System.EventHandler(this.TenantConfigFileOpen_Click);
            // 
            // tenantConfigFileTextBox
            // 
            this.tenantConfigFileTextBox.Location = new System.Drawing.Point(20, 23);
            this.tenantConfigFileTextBox.Name = "tenantConfigFileTextBox";
            this.tenantConfigFileTextBox.Size = new System.Drawing.Size(219, 20);
            this.tenantConfigFileTextBox.TabIndex = 0;
            this.tenantConfigFileTextBox.TextChanged += new System.EventHandler(this.TenantConfigFile_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Module Configuration Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Tenant Configuration File";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 312);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tenantConfigFileTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tenantConfigFileOpen);
            this.Controls.Add(this.configTypeComboBox);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.moduleGroupBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Load Tenant Configuration Tables";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.moduleGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}