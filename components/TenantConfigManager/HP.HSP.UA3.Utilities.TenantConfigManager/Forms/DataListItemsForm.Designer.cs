namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class DataListItemsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShowIdsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DataListTextBox = new System.Windows.Forms.TextBox();
            this.DataListItemsGridView = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataListItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CancelButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataListItemsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ShowIdsCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.DataListTextBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(983, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data List";
            // 
            // ShowIdsCheckBox
            // 
            this.ShowIdsCheckBox.AutoSize = true;
            this.ShowIdsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowIdsCheckBox.Location = new System.Drawing.Point(466, 21);
            this.ShowIdsCheckBox.Name = "ShowIdsCheckBox";
            this.ShowIdsCheckBox.Size = new System.Drawing.Size(69, 17);
            this.ShowIdsCheckBox.TabIndex = 7;
            this.ShowIdsCheckBox.Text = "Show IDs";
            this.ShowIdsCheckBox.UseVisualStyleBackColor = true;
            this.ShowIdsCheckBox.CheckedChanged += new System.EventHandler(this.ShowIdsCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Content ID";
            // 
            // DataListTextBox
            // 
            this.DataListTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DataListTextBox.Location = new System.Drawing.Point(83, 21);
            this.DataListTextBox.Name = "DataListTextBox";
            this.DataListTextBox.ReadOnly = true;
            this.DataListTextBox.Size = new System.Drawing.Size(362, 20);
            this.DataListTextBox.TabIndex = 0;
            // 
            // DataListItemsGridView
            // 
            this.DataListItemsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataListItemsGridView.AutoGenerateColumns = false;
            this.DataListItemsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DataListItemsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DataListItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataListItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.localeIdDataGridViewTextBoxColumn,
            this.contentIdDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn,
            this.textDataGridViewTextBoxColumn,
            this.orderDataGridViewTextBoxColumn});
            this.DataListItemsGridView.DataSource = this.dataListItemBindingSource;
            this.DataListItemsGridView.Location = new System.Drawing.Point(13, 86);
            this.DataListItemsGridView.Name = "DataListItemsGridView";
            this.DataListItemsGridView.Size = new System.Drawing.Size(983, 280);
            this.DataListItemsGridView.TabIndex = 1;
            this.DataListItemsGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataListItemsGridView_CellEnter);
            this.DataListItemsGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataListItemsGridView_DefaultValuesNeeded);
            this.DataListItemsGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DataListItemsGridView_UserDeletingRow);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.idDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.idDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idDataGridViewTextBoxColumn.MinimumWidth = 225;
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
            this.idDataGridViewTextBoxColumn.Width = 225;
            // 
            // localeIdDataGridViewTextBoxColumn
            // 
            this.localeIdDataGridViewTextBoxColumn.DataPropertyName = "LocaleId";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.localeIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.localeIdDataGridViewTextBoxColumn.HeaderText = "Locale ID";
            this.localeIdDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.localeIdDataGridViewTextBoxColumn.Name = "localeIdDataGridViewTextBoxColumn";
            this.localeIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.localeIdDataGridViewTextBoxColumn.Visible = false;
            this.localeIdDataGridViewTextBoxColumn.Width = 78;
            // 
            // contentIdDataGridViewTextBoxColumn
            // 
            this.contentIdDataGridViewTextBoxColumn.DataPropertyName = "ContentId";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.contentIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.contentIdDataGridViewTextBoxColumn.HeaderText = "Content ID";
            this.contentIdDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.contentIdDataGridViewTextBoxColumn.Name = "contentIdDataGridViewTextBoxColumn";
            this.contentIdDataGridViewTextBoxColumn.Width = 200;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.valueDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            // 
            // textDataGridViewTextBoxColumn
            // 
            this.textDataGridViewTextBoxColumn.DataPropertyName = "Text";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.textDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.textDataGridViewTextBoxColumn.HeaderText = "Text";
            this.textDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.textDataGridViewTextBoxColumn.Name = "textDataGridViewTextBoxColumn";
            // 
            // orderDataGridViewTextBoxColumn
            // 
            this.orderDataGridViewTextBoxColumn.DataPropertyName = "Order";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.orderDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.orderDataGridViewTextBoxColumn.HeaderText = "Order";
            this.orderDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.orderDataGridViewTextBoxColumn.Name = "orderDataGridViewTextBoxColumn";
            this.orderDataGridViewTextBoxColumn.Width = 58;
            // 
            // dataListItemBindingSource
            // 
            this.dataListItemBindingSource.DataSource = typeof(HP.HSP.UA3.Core.UX.Data.Configuration.LocaleConfigurationDataListItemModel);
            this.dataListItemBindingSource.CurrentItemChanged += new System.EventHandler(this.dataListItemBindingSource_CurrentItemChanged);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Location = new System.Drawing.Point(921, 377);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Location = new System.Drawing.Point(840, 377);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetButton.Enabled = false;
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetButton.Location = new System.Drawing.Point(12, 377);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 5;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // DataListItemsForm
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(1008, 412);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DataListItemsGridView);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1024, 450);
            this.Name = "DataListItemsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data List Items";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataListItemsForm_FormClosing);
            this.Load += new System.EventHandler(this.DataListItemsForm_Load);
            this.Shown += new System.EventHandler(this.DataListItemsForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataListItemsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListItemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox DataListTextBox;
        private System.Windows.Forms.DataGridView DataListItemsGridView;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.BindingSource dataListItemBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ShowIdsCheckBox;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn localeIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderDataGridViewTextBoxColumn;
    }
}