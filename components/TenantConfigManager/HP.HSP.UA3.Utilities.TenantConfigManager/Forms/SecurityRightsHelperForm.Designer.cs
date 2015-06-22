namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class SecurityRightsHelperForm
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
            this.ResetButton = new System.Windows.Forms.Button();
            this.CancelProcButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SecurityRightNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SecurityRightIdTextBox = new System.Windows.Forms.TextBox();
            this.SecurityRolesGridView = new System.Windows.Forms.DataGridView();
            this.securityRolesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.roleIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isIncludedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityRolesGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityRolesBindingSource)).BeginInit();
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
            this.ResetButton.TabIndex = 22;
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
            this.CancelProcButton.TabIndex = 21;
            this.CancelProcButton.Text = "Cancel";
            this.CancelProcButton.UseVisualStyleBackColor = true;
            this.CancelProcButton.Click += new System.EventHandler(this.CancelProcButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Location = new System.Drawing.Point(840, 377);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 20;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.SecurityRightIdTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.SecurityRightNameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(983, 88);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Security Right";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // SecurityRightNameTextBox
            // 
            this.SecurityRightNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SecurityRightNameTextBox.Location = new System.Drawing.Point(54, 48);
            this.SecurityRightNameTextBox.Name = "SecurityRightNameTextBox";
            this.SecurityRightNameTextBox.Size = new System.Drawing.Size(362, 20);
            this.SecurityRightNameTextBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID";
            // 
            // SecurityRightIdTextBox
            // 
            this.SecurityRightIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SecurityRightIdTextBox.Location = new System.Drawing.Point(54, 22);
            this.SecurityRightIdTextBox.Name = "SecurityRightIdTextBox";
            this.SecurityRightIdTextBox.ReadOnly = true;
            this.SecurityRightIdTextBox.Size = new System.Drawing.Size(362, 20);
            this.SecurityRightIdTextBox.TabIndex = 2;
            // 
            // SecurityRolesGridView
            // 
            this.SecurityRolesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SecurityRolesGridView.AutoGenerateColumns = false;
            this.SecurityRolesGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.SecurityRolesGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.SecurityRolesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SecurityRolesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roleIdDataGridViewTextBoxColumn,
            this.roleNameDataGridViewTextBoxColumn,
            this.functionIdDataGridViewTextBoxColumn,
            this.functionNameDataGridViewTextBoxColumn,
            this.isIncludedDataGridViewTextBoxColumn});
            this.SecurityRolesGridView.DataSource = this.securityRolesBindingSource;
            this.SecurityRolesGridView.Location = new System.Drawing.Point(13, 106);
            this.SecurityRolesGridView.Name = "SecurityRolesGridView";
            this.SecurityRolesGridView.Size = new System.Drawing.Size(983, 265);
            this.SecurityRolesGridView.TabIndex = 24;
            // 
            // securityRolesBindingSource
            // 
            this.securityRolesBindingSource.DataSource = typeof(HP.HSP.UA3.Utilities.TenantConfigManager.Data.SecurityRoleFunctionModel);
            // 
            // roleIdDataGridViewTextBoxColumn
            // 
            this.roleIdDataGridViewTextBoxColumn.DataPropertyName = "RoleId";
            this.roleIdDataGridViewTextBoxColumn.HeaderText = "RoleId";
            this.roleIdDataGridViewTextBoxColumn.Name = "roleIdDataGridViewTextBoxColumn";
            this.roleIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.roleIdDataGridViewTextBoxColumn.Visible = false;
            this.roleIdDataGridViewTextBoxColumn.Width = 63;
            // 
            // roleNameDataGridViewTextBoxColumn
            // 
            this.roleNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.roleNameDataGridViewTextBoxColumn.DataPropertyName = "RoleName";
            this.roleNameDataGridViewTextBoxColumn.HeaderText = "Role";
            this.roleNameDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.roleNameDataGridViewTextBoxColumn.Name = "roleNameDataGridViewTextBoxColumn";
            this.roleNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.roleNameDataGridViewTextBoxColumn.Width = 250;
            // 
            // functionIdDataGridViewTextBoxColumn
            // 
            this.functionIdDataGridViewTextBoxColumn.DataPropertyName = "FunctionId";
            this.functionIdDataGridViewTextBoxColumn.HeaderText = "FunctionId";
            this.functionIdDataGridViewTextBoxColumn.Name = "functionIdDataGridViewTextBoxColumn";
            this.functionIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.functionIdDataGridViewTextBoxColumn.Visible = false;
            this.functionIdDataGridViewTextBoxColumn.Width = 82;
            // 
            // functionNameDataGridViewTextBoxColumn
            // 
            this.functionNameDataGridViewTextBoxColumn.DataPropertyName = "FunctionName";
            this.functionNameDataGridViewTextBoxColumn.HeaderText = "Function";
            this.functionNameDataGridViewTextBoxColumn.MinimumWidth = 250;
            this.functionNameDataGridViewTextBoxColumn.Name = "functionNameDataGridViewTextBoxColumn";
            this.functionNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.functionNameDataGridViewTextBoxColumn.Width = 250;
            // 
            // isIncludedDataGridViewTextBoxColumn
            // 
            this.isIncludedDataGridViewTextBoxColumn.DataPropertyName = "isIncluded";
            this.isIncludedDataGridViewTextBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.isIncludedDataGridViewTextBoxColumn.HeaderText = "Include";
            this.isIncludedDataGridViewTextBoxColumn.MinimumWidth = 25;
            this.isIncludedDataGridViewTextBoxColumn.Name = "isIncludedDataGridViewTextBoxColumn";
            this.isIncludedDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isIncludedDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.isIncludedDataGridViewTextBoxColumn.Width = 67;
            // 
            // SecurityRightsHelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 412);
            this.Controls.Add(this.SecurityRolesGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.CancelProcButton);
            this.Controls.Add(this.SaveButton);
            this.MinimumSize = new System.Drawing.Size(1024, 450);
            this.Name = "SecurityRightsHelperForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Security Right  Helper";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityRolesGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityRolesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button CancelProcButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SecurityRightIdTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SecurityRightNameTextBox;
        private System.Windows.Forms.DataGridView SecurityRolesGridView;
        private System.Windows.Forms.BindingSource securityRolesBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isIncludedDataGridViewTextBoxColumn;
    }
}