namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class MenuItemsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ResetButton = new System.Windows.Forms.Button();
            this.CancelProcButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.MenuItemsGridView = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderIndexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isVisibleDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.baseUrlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelContentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cssClassDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.securityRightIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iocContainerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Items4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.menuItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MenuItemDisplayTextBox = new System.Windows.Forms.TextBox();
            this.ShowIdsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MenuItemNameTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.MenuItemsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuItemsBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetButton.Enabled = false;
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetButton.Location = new System.Drawing.Point(12, 377);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 14;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // CancelProcButton
            // 
            this.CancelProcButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelProcButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelProcButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelProcButton.Location = new System.Drawing.Point(1272, 377);
            this.CancelProcButton.Name = "CancelProcButton";
            this.CancelProcButton.Size = new System.Drawing.Size(75, 23);
            this.CancelProcButton.TabIndex = 13;
            this.CancelProcButton.Text = "Cancel";
            this.CancelProcButton.UseVisualStyleBackColor = true;
            this.CancelProcButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Location = new System.Drawing.Point(1191, 377);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 12;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // MenuItemsGridView
            // 
            this.MenuItemsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MenuItemsGridView.AutoGenerateColumns = false;
            this.MenuItemsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.MenuItemsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.MenuItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MenuItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.orderIndexDataGridViewTextBoxColumn,
            this.isVisibleDataGridViewCheckBoxColumn,
            this.baseUrlDataGridViewTextBoxColumn,
            this.labelContentIdDataGridViewTextBoxColumn,
            this.defaultTextDataGridViewTextBoxColumn,
            this.cssClassDataGridViewTextBoxColumn,
            this.securityRightIdDataGridViewTextBoxColumn,
            this.iocContainerDataGridViewTextBoxColumn,
            this.Items4});
            this.MenuItemsGridView.DataSource = this.menuItemsBindingSource;
            this.MenuItemsGridView.Location = new System.Drawing.Point(13, 85);
            this.MenuItemsGridView.Name = "MenuItemsGridView";
            this.MenuItemsGridView.Size = new System.Drawing.Size(1334, 281);
            this.MenuItemsGridView.TabIndex = 11;
            this.MenuItemsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MenuItemsGridView_CellContentClick);
            this.MenuItemsGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.MenuItemsGridView_CellEnter);
            this.MenuItemsGridView.CurrentCellChanged += new System.EventHandler(this.MenuItemsGridView_CurrentCellChanged);
            this.MenuItemsGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.MenuItemsGridView_DefaultValuesNeeded);
            this.MenuItemsGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.MenuItemsGridView_UserDeletingRow);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.idDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.idDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idDataGridViewTextBoxColumn.MinimumWidth = 225;
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
            this.idDataGridViewTextBoxColumn.Width = 225;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.nameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 200;
            // 
            // orderIndexDataGridViewTextBoxColumn
            // 
            this.orderIndexDataGridViewTextBoxColumn.DataPropertyName = "OrderIndex";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = "0";
            this.orderIndexDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.orderIndexDataGridViewTextBoxColumn.HeaderText = "Order";
            this.orderIndexDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.orderIndexDataGridViewTextBoxColumn.Name = "orderIndexDataGridViewTextBoxColumn";
            this.orderIndexDataGridViewTextBoxColumn.Width = 58;
            // 
            // isVisibleDataGridViewCheckBoxColumn
            // 
            this.isVisibleDataGridViewCheckBoxColumn.DataPropertyName = "IsVisible";
            this.isVisibleDataGridViewCheckBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.isVisibleDataGridViewCheckBoxColumn.HeaderText = "Visible";
            this.isVisibleDataGridViewCheckBoxColumn.MinimumWidth = 50;
            this.isVisibleDataGridViewCheckBoxColumn.Name = "isVisibleDataGridViewCheckBoxColumn";
            this.isVisibleDataGridViewCheckBoxColumn.Width = 50;
            // 
            // baseUrlDataGridViewTextBoxColumn
            // 
            this.baseUrlDataGridViewTextBoxColumn.DataPropertyName = "BaseUrl";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.baseUrlDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.baseUrlDataGridViewTextBoxColumn.HeaderText = "Base Url";
            this.baseUrlDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.baseUrlDataGridViewTextBoxColumn.Name = "baseUrlDataGridViewTextBoxColumn";
            this.baseUrlDataGridViewTextBoxColumn.Width = 200;
            // 
            // labelContentIdDataGridViewTextBoxColumn
            // 
            this.labelContentIdDataGridViewTextBoxColumn.DataPropertyName = "LabelContentId";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.labelContentIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.labelContentIdDataGridViewTextBoxColumn.HeaderText = "Label Content ID";
            this.labelContentIdDataGridViewTextBoxColumn.MinimumWidth = 225;
            this.labelContentIdDataGridViewTextBoxColumn.Name = "labelContentIdDataGridViewTextBoxColumn";
            this.labelContentIdDataGridViewTextBoxColumn.Width = 225;
            // 
            // defaultTextDataGridViewTextBoxColumn
            // 
            this.defaultTextDataGridViewTextBoxColumn.DataPropertyName = "DefaultText";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.defaultTextDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.defaultTextDataGridViewTextBoxColumn.HeaderText = "Default Text";
            this.defaultTextDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.defaultTextDataGridViewTextBoxColumn.Name = "defaultTextDataGridViewTextBoxColumn";
            // 
            // cssClassDataGridViewTextBoxColumn
            // 
            this.cssClassDataGridViewTextBoxColumn.DataPropertyName = "CssClass";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.cssClassDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.cssClassDataGridViewTextBoxColumn.HeaderText = "Css Class";
            this.cssClassDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.cssClassDataGridViewTextBoxColumn.Name = "cssClassDataGridViewTextBoxColumn";
            this.cssClassDataGridViewTextBoxColumn.Width = 71;
            // 
            // securityRightIdDataGridViewTextBoxColumn
            // 
            this.securityRightIdDataGridViewTextBoxColumn.DataPropertyName = "SecurityRightId";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.securityRightIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.securityRightIdDataGridViewTextBoxColumn.HeaderText = "Security Right ID";
            this.securityRightIdDataGridViewTextBoxColumn.MinimumWidth = 225;
            this.securityRightIdDataGridViewTextBoxColumn.Name = "securityRightIdDataGridViewTextBoxColumn";
            this.securityRightIdDataGridViewTextBoxColumn.Width = 225;
            // 
            // iocContainerDataGridViewTextBoxColumn
            // 
            this.iocContainerDataGridViewTextBoxColumn.DataPropertyName = "IocContainer";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.iocContainerDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.iocContainerDataGridViewTextBoxColumn.HeaderText = "IOC Container";
            this.iocContainerDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.iocContainerDataGridViewTextBoxColumn.Name = "iocContainerDataGridViewTextBoxColumn";
            this.iocContainerDataGridViewTextBoxColumn.Width = 90;
            // 
            // Items4
            // 
            this.Items4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Items4.HeaderText = "Items";
            this.Items4.MinimumWidth = 90;
            this.Items4.Name = "Items4";
            this.Items4.Text = "Edit";
            this.Items4.ToolTipText = "Maintain menu items.";
            this.Items4.UseColumnTextForButtonValue = true;
            this.Items4.Width = 90;
            // 
            // menuItemsBindingSource
            // 
            this.menuItemsBindingSource.DataSource = typeof(HP.HSP.UA3.Core.UX.Data.Navigation.MenuItemModel);
            this.menuItemsBindingSource.CurrentChanged += new System.EventHandler(this.MenuItemsGridView_CurrentCellChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.MenuItemDisplayTextBox);
            this.groupBox1.Controls.Add(this.ShowIdsCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.MenuItemNameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1334, 67);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Menu Item";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(441, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Display";
            // 
            // MenuItemDisplayTextBox
            // 
            this.MenuItemDisplayTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuItemDisplayTextBox.Location = new System.Drawing.Point(485, 19);
            this.MenuItemDisplayTextBox.Name = "MenuItemDisplayTextBox";
            this.MenuItemDisplayTextBox.ReadOnly = true;
            this.MenuItemDisplayTextBox.Size = new System.Drawing.Size(100, 20);
            this.MenuItemDisplayTextBox.TabIndex = 10;
            // 
            // ShowIdsCheckBox
            // 
            this.ShowIdsCheckBox.AutoSize = true;
            this.ShowIdsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowIdsCheckBox.Location = new System.Drawing.Point(604, 21);
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
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // MenuItemNameTextBox
            // 
            this.MenuItemNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuItemNameTextBox.Location = new System.Drawing.Point(56, 19);
            this.MenuItemNameTextBox.Name = "MenuItemNameTextBox";
            this.MenuItemNameTextBox.ReadOnly = true;
            this.MenuItemNameTextBox.Size = new System.Drawing.Size(362, 20);
            this.MenuItemNameTextBox.TabIndex = 0;
            // 
            // MenuItemsForm
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelProcButton;
            this.ClientSize = new System.Drawing.Size(1359, 412);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.CancelProcButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.MenuItemsGridView);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1024, 450);
            this.Name = "MenuItemsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Menu Items";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelPropertyForm_FormClosing);
            this.Load += new System.EventHandler(this.ModelPropertyForm_Load);
            this.Shown += new System.EventHandler(this.ModelPropertyForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.MenuItemsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuItemsBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button CancelProcButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridView MenuItemsGridView;
        private System.Windows.Forms.BindingSource menuItemsBindingSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MenuItemDisplayTextBox;
        private System.Windows.Forms.CheckBox ShowIdsCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MenuItemNameTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderIndexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isVisibleDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseUrlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn labelContentIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cssClassDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn securityRightIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iocContainerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn Items4;
    }
}