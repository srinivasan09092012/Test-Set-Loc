namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class EmailTemplateAddressForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShowIdsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TemplateTextBox = new System.Windows.Forms.TextBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.CancelProcButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.AddressItemsGridView = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.displayNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailTemplateAddressBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddressItemsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailTemplateAddressBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ShowIdsCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TemplateTextBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(850, 67);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Email Template";
            // 
            // ShowIdsCheckBox
            // 
            this.ShowIdsCheckBox.AutoSize = true;
            this.ShowIdsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowIdsCheckBox.Location = new System.Drawing.Point(465, 25);
            this.ShowIdsCheckBox.Name = "ShowIdsCheckBox";
            this.ShowIdsCheckBox.Size = new System.Drawing.Size(69, 17);
            this.ShowIdsCheckBox.TabIndex = 10;
            this.ShowIdsCheckBox.Text = "Show IDs";
            this.ShowIdsCheckBox.UseVisualStyleBackColor = true;
            this.ShowIdsCheckBox.CheckedChanged += new System.EventHandler(this.ShowIdsCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Content ID";
            // 
            // TemplateTextBox
            // 
            this.TemplateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TemplateTextBox.Location = new System.Drawing.Point(82, 25);
            this.TemplateTextBox.Name = "TemplateTextBox";
            this.TemplateTextBox.ReadOnly = true;
            this.TemplateTextBox.Size = new System.Drawing.Size(362, 20);
            this.TemplateTextBox.TabIndex = 8;
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetButton.Enabled = false;
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetButton.Location = new System.Drawing.Point(11, 376);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 9;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // CancelProcButton
            // 
            this.CancelProcButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelProcButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelProcButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelProcButton.Location = new System.Drawing.Point(788, 377);
            this.CancelProcButton.Name = "CancelProcButton";
            this.CancelProcButton.Size = new System.Drawing.Size(75, 23);
            this.CancelProcButton.TabIndex = 8;
            this.CancelProcButton.Text = "Cancel";
            this.CancelProcButton.UseVisualStyleBackColor = true;
            this.CancelProcButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Location = new System.Drawing.Point(707, 377);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // AddressItemsGridView
            // 
            this.AddressItemsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddressItemsGridView.AutoGenerateColumns = false;
            this.AddressItemsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.AddressItemsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.AddressItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AddressItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.Type,
            this.displayNameDataGridViewTextBoxColumn,
            this.emailAddressDataGridViewTextBoxColumn});
            this.AddressItemsGridView.DataSource = this.emailTemplateAddressBindingSource;
            this.AddressItemsGridView.Location = new System.Drawing.Point(12, 85);
            this.AddressItemsGridView.Name = "AddressItemsGridView";
            this.AddressItemsGridView.Size = new System.Drawing.Size(851, 280);
            this.AddressItemsGridView.TabIndex = 6;
            this.AddressItemsGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.AddressItemsGridView_CellEnter);
            this.AddressItemsGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.AddressItemsGridView_DefaultValuesNeeded);
            this.AddressItemsGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.AddressItemsGridView_UserDeletingRow);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.idDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
            this.idDataGridViewTextBoxColumn.Width = 41;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Type.DefaultCellStyle = dataGridViewCellStyle2;
            this.Type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Type.HeaderText = "Type";
            this.Type.MinimumWidth = 50;
            this.Type.Name = "Type";
            this.Type.Width = 50;
            // 
            // displayNameDataGridViewTextBoxColumn
            // 
            this.displayNameDataGridViewTextBoxColumn.DataPropertyName = "DisplayName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.displayNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.displayNameDataGridViewTextBoxColumn.HeaderText = "DisplayName";
            this.displayNameDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.displayNameDataGridViewTextBoxColumn.Name = "displayNameDataGridViewTextBoxColumn";
            this.displayNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // emailAddressDataGridViewTextBoxColumn
            // 
            this.emailAddressDataGridViewTextBoxColumn.DataPropertyName = "EmailAddress";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.emailAddressDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.emailAddressDataGridViewTextBoxColumn.HeaderText = "EmailAddress";
            this.emailAddressDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.emailAddressDataGridViewTextBoxColumn.Name = "emailAddressDataGridViewTextBoxColumn";
            this.emailAddressDataGridViewTextBoxColumn.Width = 200;
            // 
            // emailTemplateAddressBindingSource
            // 
            this.emailTemplateAddressBindingSource.DataSource = typeof(HP.HSP.UA3.Core.UX.Data.Configuration.LocaleConfigurationEmailTemplateAddressModel);
            this.emailTemplateAddressBindingSource.CurrentItemChanged += new System.EventHandler(this.emailTemplateAddressBindingSource_CurrentItemChanged);
            // 
            // EmailTemplateAddressForm
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelProcButton;
            this.ClientSize = new System.Drawing.Size(875, 412);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.CancelProcButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.AddressItemsGridView);
            this.Controls.Add(this.groupBox1);
            this.Name = "EmailTemplateAddressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Email Template Addresses";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EmailTemplateAddressForm_FormClosing);
            this.Load += new System.EventHandler(this.EmailTemplateAddressFormLoad);
            this.Shown += new System.EventHandler(this.EmailTemplateAddressForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddressItemsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailTemplateAddressBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ShowIdsCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TemplateTextBox;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button CancelProcButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridView AddressItemsGridView;
        private System.Windows.Forms.BindingSource emailTemplateAddressBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailAddressDataGridViewTextBoxColumn;
    }
}