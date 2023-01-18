namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class SecurityRightHelperForm
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
            this.RightsGridView = new System.Windows.Forms.DataGridView();
            this.ResetButton = new System.Windows.Forms.Button();
            this.CancelProcButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SecurityRightNameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IDLabel = new System.Windows.Forms.Label();
            this.SecurityRightIdTextBox = new System.Windows.Forms.TextBox();
            this.roleIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isIncludedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rightsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RightsGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // RightsGridView
            // 
            this.RightsGridView.AllowUserToAddRows = false;
            this.RightsGridView.AllowUserToDeleteRows = false;
            this.RightsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RightsGridView.AutoGenerateColumns = false;
            this.RightsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.RightsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.RightsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RightsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roleIdDataGridViewTextBoxColumn,
            this.functionIdDataGridViewTextBoxColumn,
            this.roleNameDataGridViewTextBoxColumn,
            this.functionNameDataGridViewTextBoxColumn,
            this.isIncludedDataGridViewCheckBoxColumn});
            this.RightsGridView.DataSource = this.rightsBindingSource;
            this.RightsGridView.Enabled = false;
            this.RightsGridView.Location = new System.Drawing.Point(13, 106);
            this.RightsGridView.Name = "RightsGridView";
            this.RightsGridView.Size = new System.Drawing.Size(983, 265);
            this.RightsGridView.TabIndex = 34;
            this.RightsGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RightsGridView_CellEnter);
            this.RightsGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.RightsGridView_DefaultValuesNeeded);
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetButton.Enabled = false;
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetButton.Location = new System.Drawing.Point(12, 377);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 32;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // CancelProcButton
            // 
            this.CancelProcButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelProcButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelProcButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelProcButton.Location = new System.Drawing.Point(921, 377);
            this.CancelProcButton.Name = "CancelProcButton";
            this.CancelProcButton.Size = new System.Drawing.Size(75, 23);
            this.CancelProcButton.TabIndex = 31;
            this.CancelProcButton.Text = "Cancel";
            this.CancelProcButton.UseVisualStyleBackColor = true;
            this.CancelProcButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Location = new System.Drawing.Point(840, 377);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 30;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditButton.Location = new System.Drawing.Point(534, 49);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(75, 23);
            this.EditButton.TabIndex = 26;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // SecurityRightNameTextBox
            // 
            this.SecurityRightNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SecurityRightNameTextBox.Location = new System.Drawing.Point(71, 49);
            this.SecurityRightNameTextBox.Name = "SecurityRightNameTextBox";
            this.SecurityRightNameTextBox.Size = new System.Drawing.Size(457, 20);
            this.SecurityRightNameTextBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.IDLabel);
            this.groupBox1.Controls.Add(this.SecurityRightIdTextBox);
            this.groupBox1.Controls.Add(this.EditButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.SecurityRightNameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(983, 88);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Security Right";
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Location = new System.Drawing.Point(47, 24);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(18, 13);
            this.IDLabel.TabIndex = 28;
            this.IDLabel.Text = "ID";
            // 
            // SecurityRightIdTextBox
            // 
            this.SecurityRightIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SecurityRightIdTextBox.Location = new System.Drawing.Point(71, 22);
            this.SecurityRightIdTextBox.Name = "SecurityRightIdTextBox";
            this.SecurityRightIdTextBox.ReadOnly = true;
            this.SecurityRightIdTextBox.Size = new System.Drawing.Size(352, 20);
            this.SecurityRightIdTextBox.TabIndex = 27;
            // 
            // roleIdDataGridViewTextBoxColumn
            // 
            this.roleIdDataGridViewTextBoxColumn.DataPropertyName = "RoleId";
            this.roleIdDataGridViewTextBoxColumn.Frozen = true;
            this.roleIdDataGridViewTextBoxColumn.HeaderText = "Role ID";
            this.roleIdDataGridViewTextBoxColumn.MinimumWidth = 250;
            this.roleIdDataGridViewTextBoxColumn.Name = "roleIdDataGridViewTextBoxColumn";
            this.roleIdDataGridViewTextBoxColumn.Visible = false;
            this.roleIdDataGridViewTextBoxColumn.Width = 250;
            // 
            // functionIdDataGridViewTextBoxColumn
            // 
            this.functionIdDataGridViewTextBoxColumn.DataPropertyName = "FunctionId";
            this.functionIdDataGridViewTextBoxColumn.Frozen = true;
            this.functionIdDataGridViewTextBoxColumn.HeaderText = "Function ID";
            this.functionIdDataGridViewTextBoxColumn.MinimumWidth = 250;
            this.functionIdDataGridViewTextBoxColumn.Name = "functionIdDataGridViewTextBoxColumn";
            this.functionIdDataGridViewTextBoxColumn.Visible = false;
            this.functionIdDataGridViewTextBoxColumn.Width = 250;
            // 
            // roleNameDataGridViewTextBoxColumn
            // 
            this.roleNameDataGridViewTextBoxColumn.DataPropertyName = "RoleName";
            this.roleNameDataGridViewTextBoxColumn.HeaderText = "Role";
            this.roleNameDataGridViewTextBoxColumn.MinimumWidth = 250;
            this.roleNameDataGridViewTextBoxColumn.Name = "roleNameDataGridViewTextBoxColumn";
            this.roleNameDataGridViewTextBoxColumn.Width = 250;
            // 
            // functionNameDataGridViewTextBoxColumn
            // 
            this.functionNameDataGridViewTextBoxColumn.DataPropertyName = "FunctionName";
            this.functionNameDataGridViewTextBoxColumn.HeaderText = "Function";
            this.functionNameDataGridViewTextBoxColumn.MinimumWidth = 250;
            this.functionNameDataGridViewTextBoxColumn.Name = "functionNameDataGridViewTextBoxColumn";
            this.functionNameDataGridViewTextBoxColumn.Width = 250;
            // 
            // isIncludedDataGridViewCheckBoxColumn
            // 
            this.isIncludedDataGridViewCheckBoxColumn.DataPropertyName = "isIncluded";
            this.isIncludedDataGridViewCheckBoxColumn.HeaderText = "Add";
            this.isIncludedDataGridViewCheckBoxColumn.MinimumWidth = 50;
            this.isIncludedDataGridViewCheckBoxColumn.Name = "isIncludedDataGridViewCheckBoxColumn";
            this.isIncludedDataGridViewCheckBoxColumn.Width = 50;
            // 
            // rightsBindingSource
            // 
            this.rightsBindingSource.DataSource = typeof(HP.HSP.UA3.Utilities.TenantConfigManager.Data.SecurityRightHelperModel);
            this.rightsBindingSource.CurrentItemChanged += new System.EventHandler(this.rightsBindingSource_CurrentItemChanged);
            // 
            // SecurityRightHelperForm
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelProcButton;
            this.ClientSize = new System.Drawing.Size(1008, 412);
            this.Controls.Add(this.RightsGridView);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.CancelProcButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1024, 450);
            this.Name = "SecurityRightHelperForm";
            this.Text = "Security Right Helper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SecurityRightsHelperForm_FormClosing);
            this.Load += new System.EventHandler(this.SecurityRightsHelperForm_Load);
            this.Shown += new System.EventHandler(this.SecurityRightsHelperForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.RightsGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView RightsGridView;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button CancelProcButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SecurityRightNameTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.TextBox SecurityRightIdTextBox;
        private System.Windows.Forms.BindingSource rightsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isIncludedDataGridViewCheckBoxColumn;
    }
}