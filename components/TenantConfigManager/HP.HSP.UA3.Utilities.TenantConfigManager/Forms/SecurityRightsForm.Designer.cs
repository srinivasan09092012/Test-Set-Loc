namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class SecurityRightsForm
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
            this.ResetButton = new System.Windows.Forms.Button();
            this.CancelProcButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SecurityRightsGridView = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.contentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.securityRightsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShowIdsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SecurityFunctionNameTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityRightsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityRightsBindingSource)).BeginInit();
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
            this.ResetButton.TabIndex = 19;
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
            this.CancelProcButton.TabIndex = 18;
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
            this.SaveButton.TabIndex = 17;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SecurityRightsGridView
            // 
            this.SecurityRightsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SecurityRightsGridView.AutoGenerateColumns = false;
            this.SecurityRightsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.SecurityRightsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.SecurityRightsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SecurityRightsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.contentIdDataGridViewTextBoxColumn,
            this.isActiveDataGridViewCheckBoxColumn});
            this.SecurityRightsGridView.DataSource = this.securityRightsBindingSource;
            this.SecurityRightsGridView.Location = new System.Drawing.Point(13, 85);
            this.SecurityRightsGridView.Name = "SecurityRightsGridView";
            this.SecurityRightsGridView.Size = new System.Drawing.Size(983, 281);
            this.SecurityRightsGridView.TabIndex = 16;
            this.SecurityRightsGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.SecurityRightsGridView_CellEnter);
            this.SecurityRightsGridView.CurrentCellChanged += new System.EventHandler(this.securityRightsBindingSource_CurrentCellChanged);
            this.SecurityRightsGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.SecurityRightsGridView_DefaultValuesNeeded);
            this.SecurityRightsGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.SecurityRightsGridView_UserDeletingRow);
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
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.typeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.typeDataGridViewTextBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.typeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // contentIdDataGridViewTextBoxColumn
            // 
            this.contentIdDataGridViewTextBoxColumn.DataPropertyName = "ContentId";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.contentIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.contentIdDataGridViewTextBoxColumn.HeaderText = "Label Content ID";
            this.contentIdDataGridViewTextBoxColumn.MinimumWidth = 225;
            this.contentIdDataGridViewTextBoxColumn.Name = "contentIdDataGridViewTextBoxColumn";
            this.contentIdDataGridViewTextBoxColumn.Width = 225;
            // 
            // isActiveDataGridViewCheckBoxColumn
            // 
            this.isActiveDataGridViewCheckBoxColumn.DataPropertyName = "IsActive";
            this.isActiveDataGridViewCheckBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.isActiveDataGridViewCheckBoxColumn.HeaderText = "Active";
            this.isActiveDataGridViewCheckBoxColumn.MinimumWidth = 50;
            this.isActiveDataGridViewCheckBoxColumn.Name = "isActiveDataGridViewCheckBoxColumn";
            this.isActiveDataGridViewCheckBoxColumn.Width = 50;
            // 
            // securityRightsBindingSource
            // 
            this.securityRightsBindingSource.DataSource = typeof(HP.HSP.UA3.Core.UX.Data.Security.SecurityRightModel);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ShowIdsCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.SecurityFunctionNameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(983, 67);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Security Function";
            // 
            // ShowIdsCheckBox
            // 
            this.ShowIdsCheckBox.AutoSize = true;
            this.ShowIdsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowIdsCheckBox.Location = new System.Drawing.Point(434, 19);
            this.ShowIdsCheckBox.Name = "ShowIdsCheckBox";
            this.ShowIdsCheckBox.Size = new System.Drawing.Size(69, 17);
            this.ShowIdsCheckBox.TabIndex = 7;
            this.ShowIdsCheckBox.Text = "Show IDs";
            this.ShowIdsCheckBox.UseVisualStyleBackColor = true;
            this.ShowIdsCheckBox.Click += new System.EventHandler(this.ShowIdsCheckBox_CheckedChanged);
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
            // SecurityFunctionNameTextBox
            // 
            this.SecurityFunctionNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SecurityFunctionNameTextBox.Location = new System.Drawing.Point(56, 19);
            this.SecurityFunctionNameTextBox.Name = "SecurityFunctionNameTextBox";
            this.SecurityFunctionNameTextBox.ReadOnly = true;
            this.SecurityFunctionNameTextBox.Size = new System.Drawing.Size(362, 20);
            this.SecurityFunctionNameTextBox.TabIndex = 0;
            // 
            // SecurityRightsForm
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelProcButton;
            this.ClientSize = new System.Drawing.Size(1008, 412);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.CancelProcButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.SecurityRightsGridView);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1024, 450);
            this.Name = "SecurityRightsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Security Rights";
            this.Load += new System.EventHandler(this.SecurityRightsForm_Load);
            this.Shown += new System.EventHandler(this.SecurityRightsForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.SecurityRightsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityRightsBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button CancelProcButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridView SecurityRightsGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ShowIdsCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SecurityFunctionNameTextBox;
        private System.Windows.Forms.BindingSource securityRightsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
    }
}