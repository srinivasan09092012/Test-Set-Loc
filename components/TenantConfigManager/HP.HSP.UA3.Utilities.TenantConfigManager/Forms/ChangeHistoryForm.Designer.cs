namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class ChangeHistoryForm
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
            this.ShowIdsCheckBox = new System.Windows.Forms.CheckBox();
            this.DataListItemsGridView = new System.Windows.Forms.DataGridView();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ViewDetails = new System.Windows.Forms.DataGridViewButtonColumn();
            this.itemIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenantDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moduleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changeTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changeSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DataListItemsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.changeSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ShowIdsCheckBox
            // 
            this.ShowIdsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowIdsCheckBox.AutoSize = true;
            this.ShowIdsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowIdsCheckBox.Location = new System.Drawing.Point(523, 18);
            this.ShowIdsCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.ShowIdsCheckBox.Name = "ShowIdsCheckBox";
            this.ShowIdsCheckBox.Size = new System.Drawing.Size(84, 21);
            this.ShowIdsCheckBox.TabIndex = 7;
            this.ShowIdsCheckBox.Text = "Show IDs";
            this.ShowIdsCheckBox.UseVisualStyleBackColor = true;
            this.ShowIdsCheckBox.CheckedChanged += new System.EventHandler(this.ShowIdsCheckBox_CheckedChanged);
            // 
            // DataListItemsGridView
            // 
            this.DataListItemsGridView.AllowUserToAddRows = false;
            this.DataListItemsGridView.AllowUserToDeleteRows = false;
            this.DataListItemsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataListItemsGridView.AutoGenerateColumns = false;
            this.DataListItemsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DataListItemsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DataListItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataListItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemIdDataGridViewTextBoxColumn,
            this.tenantDataGridViewTextBoxColumn,
            this.moduleDataGridViewTextBoxColumn,
            this.changeTypeDataGridViewTextBoxColumn,
            this.itemTypeDataGridViewTextBoxColumn,
            this.orderDataGridViewTextBoxColumn,
            this.ViewDetails});
            this.DataListItemsGridView.DataSource = this.changeSetBindingSource;
            this.DataListItemsGridView.Location = new System.Drawing.Point(16, 43);
            this.DataListItemsGridView.Margin = new System.Windows.Forms.Padding(4);
            this.DataListItemsGridView.Name = "DataListItemsGridView";
            this.DataListItemsGridView.ReadOnly = true;
            this.DataListItemsGridView.Size = new System.Drawing.Size(591, 317);
            this.DataListItemsGridView.TabIndex = 1;
            this.DataListItemsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataListItemsGridView_CellContentClick);
            this.DataListItemsGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataListItemsGridView_CellEnter);
            this.DataListItemsGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataListItemsGridView_DefaultValuesNeeded);
            this.DataListItemsGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DataListItemsGridView_UserDeletingRow);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Location = new System.Drawing.Point(507, 368);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(100, 28);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Change details:";
            // 
            // ViewDetails
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ViewDetails.DefaultCellStyle = dataGridViewCellStyle1;
            this.ViewDetails.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ViewDetails.HeaderText = "View";
            this.ViewDetails.Name = "ViewDetails";
            this.ViewDetails.ReadOnly = true;
            this.ViewDetails.Text = "View";
            this.ViewDetails.UseColumnTextForButtonValue = true;
            this.ViewDetails.Width = 43;
            // 
            // itemIdDataGridViewTextBoxColumn
            // 
            this.itemIdDataGridViewTextBoxColumn.DataPropertyName = "ItemId";
            this.itemIdDataGridViewTextBoxColumn.HeaderText = "Item Id";
            this.itemIdDataGridViewTextBoxColumn.Name = "itemIdDataGridViewTextBoxColumn";
            this.itemIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.itemIdDataGridViewTextBoxColumn.Visible = false;
            this.itemIdDataGridViewTextBoxColumn.Width = 74;
            // 
            // tenantDataGridViewTextBoxColumn
            // 
            this.tenantDataGridViewTextBoxColumn.DataPropertyName = "Tenant";
            this.tenantDataGridViewTextBoxColumn.HeaderText = "Tenant";
            this.tenantDataGridViewTextBoxColumn.Name = "tenantDataGridViewTextBoxColumn";
            this.tenantDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenantDataGridViewTextBoxColumn.Width = 78;
            // 
            // moduleDataGridViewTextBoxColumn
            // 
            this.moduleDataGridViewTextBoxColumn.DataPropertyName = "Module";
            this.moduleDataGridViewTextBoxColumn.HeaderText = "Module";
            this.moduleDataGridViewTextBoxColumn.Name = "moduleDataGridViewTextBoxColumn";
            this.moduleDataGridViewTextBoxColumn.ReadOnly = true;
            this.moduleDataGridViewTextBoxColumn.Width = 79;
            // 
            // changeTypeDataGridViewTextBoxColumn
            // 
            this.changeTypeDataGridViewTextBoxColumn.DataPropertyName = "ChangeType";
            this.changeTypeDataGridViewTextBoxColumn.HeaderText = "Change Type";
            this.changeTypeDataGridViewTextBoxColumn.Name = "changeTypeDataGridViewTextBoxColumn";
            this.changeTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.changeTypeDataGridViewTextBoxColumn.Width = 118;
            // 
            // itemTypeDataGridViewTextBoxColumn
            // 
            this.itemTypeDataGridViewTextBoxColumn.DataPropertyName = "ItemType";
            this.itemTypeDataGridViewTextBoxColumn.HeaderText = "Item Type";
            this.itemTypeDataGridViewTextBoxColumn.Name = "itemTypeDataGridViewTextBoxColumn";
            this.itemTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.itemTypeDataGridViewTextBoxColumn.Width = 95;
            // 
            // orderDataGridViewTextBoxColumn
            // 
            this.orderDataGridViewTextBoxColumn.DataPropertyName = "Order";
            this.orderDataGridViewTextBoxColumn.HeaderText = "Order";
            this.orderDataGridViewTextBoxColumn.Name = "orderDataGridViewTextBoxColumn";
            this.orderDataGridViewTextBoxColumn.ReadOnly = true;
            this.orderDataGridViewTextBoxColumn.Visible = false;
            this.orderDataGridViewTextBoxColumn.Width = 70;
            // 
            // changeSetBindingSource
            // 
            this.changeSetBindingSource.DataSource = typeof(HP.HSP.UA3.Utilities.TenantConfigManager.Services.ConfigurationChange);
            // 
            // ChangeHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(620, 409);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShowIdsCheckBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.DataListItemsGridView);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(638, 454);
            this.Name = "ChangeHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataListItemsForm_FormClosing);
            this.Load += new System.EventHandler(this.DataListItemsForm_Load);
            this.Shown += new System.EventHandler(this.DataListItemsForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DataListItemsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.changeSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataListItemsGridView;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.CheckBox ShowIdsCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource changeSetBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenantDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn moduleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn changeTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn ViewDetails;
    }
}