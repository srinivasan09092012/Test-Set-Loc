﻿namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class SecurityFunctionsForm
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
            this.SecurityFunctionsGridView = new System.Windows.Forms.DataGridView();
            this.securityFunctionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShowIdsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SecurityRoleNameTextBox = new System.Windows.Forms.TextBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.CancelProcButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentIdDataGridViewButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isSystemFunctionDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isRoleBasedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SecurityRights = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityFunctionsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityFunctionsBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SecurityFunctionsGridView
            // 
            this.SecurityFunctionsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SecurityFunctionsGridView.AutoGenerateColumns = false;
            this.SecurityFunctionsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.SecurityFunctionsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.SecurityFunctionsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SecurityFunctionsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.contentIdDataGridViewButtonColumn,
            this.isActiveDataGridViewCheckBoxColumn,
            this.isSystemFunctionDataGridViewCheckBoxColumn,
            this.isRoleBasedDataGridViewCheckBoxColumn,
            this.SecurityRights});
            this.SecurityFunctionsGridView.DataSource = this.securityFunctionsBindingSource;
            this.SecurityFunctionsGridView.Location = new System.Drawing.Point(13, 85);
            this.SecurityFunctionsGridView.Name = "SecurityFunctionsGridView";
            this.SecurityFunctionsGridView.Size = new System.Drawing.Size(983, 281);
            this.SecurityFunctionsGridView.TabIndex = 11;
            this.SecurityFunctionsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SecurityFunctionsGridView_CellContentClick);
            this.SecurityFunctionsGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.SecurityFunctionsGridView_CellEnter);
            this.SecurityFunctionsGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.SecurityFunctionsGridView_DefaultValuesNeeded);
            this.SecurityFunctionsGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.SecurityFunctionsGridView_UserDeletingRow);
            // 
            // securityFunctionsBindingSource
            // 
            this.securityFunctionsBindingSource.DataSource = typeof(HP.HSP.UA3.Core.UX.Data.Security.SecurityFunctionModel);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ShowIdsCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.SecurityRoleNameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(983, 67);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Security Role";
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
            // SecurityRoleNameTextBox
            // 
            this.SecurityRoleNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SecurityRoleNameTextBox.Location = new System.Drawing.Point(56, 19);
            this.SecurityRoleNameTextBox.Name = "SecurityRoleNameTextBox";
            this.SecurityRoleNameTextBox.ReadOnly = true;
            this.SecurityRoleNameTextBox.Size = new System.Drawing.Size(362, 20);
            this.SecurityRoleNameTextBox.TabIndex = 0;
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetButton.Enabled = false;
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetButton.Location = new System.Drawing.Point(13, 377);
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
            this.CancelProcButton.Location = new System.Drawing.Point(922, 377);
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
            this.SaveButton.Location = new System.Drawing.Point(841, 377);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 12;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
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
            // contentIdDataGridViewButtonColumn
            // 
            this.contentIdDataGridViewButtonColumn.DataPropertyName = "ContentId";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.contentIdDataGridViewButtonColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.contentIdDataGridViewButtonColumn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.contentIdDataGridViewButtonColumn.HeaderText = "Label Content ID";
            this.contentIdDataGridViewButtonColumn.MinimumWidth = 225;
            this.contentIdDataGridViewButtonColumn.Name = "contentIdDataGridViewButtonColumn";
            this.contentIdDataGridViewButtonColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.contentIdDataGridViewButtonColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.contentIdDataGridViewButtonColumn.Width = 225;
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
            // isSystemFunctionDataGridViewCheckBoxColumn
            // 
            this.isSystemFunctionDataGridViewCheckBoxColumn.DataPropertyName = "IsSystemFunction";
            this.isSystemFunctionDataGridViewCheckBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.isSystemFunctionDataGridViewCheckBoxColumn.HeaderText = "System Function";
            this.isSystemFunctionDataGridViewCheckBoxColumn.MinimumWidth = 50;
            this.isSystemFunctionDataGridViewCheckBoxColumn.Name = "isSystemFunctionDataGridViewCheckBoxColumn";
            this.isSystemFunctionDataGridViewCheckBoxColumn.Width = 82;
            // 
            // isRoleBasedDataGridViewCheckBoxColumn
            // 
            this.isRoleBasedDataGridViewCheckBoxColumn.DataPropertyName = "IsRoleBased";
            this.isRoleBasedDataGridViewCheckBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.isRoleBasedDataGridViewCheckBoxColumn.HeaderText = "Role Based";
            this.isRoleBasedDataGridViewCheckBoxColumn.MinimumWidth = 50;
            this.isRoleBasedDataGridViewCheckBoxColumn.Name = "isRoleBasedDataGridViewCheckBoxColumn";
            this.isRoleBasedDataGridViewCheckBoxColumn.Width = 61;
            // 
            // SecurityRights
            // 
            this.SecurityRights.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SecurityRights.HeaderText = "Rights";
            this.SecurityRights.MinimumWidth = 90;
            this.SecurityRights.Name = "SecurityRights";
            this.SecurityRights.Text = "Edit";
            this.SecurityRights.ToolTipText = "Maintain security rights.";
            this.SecurityRights.UseColumnTextForButtonValue = true;
            this.SecurityRights.Width = 90;
            // 
            // SecurityFunctionsForm
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelProcButton;
            this.ClientSize = new System.Drawing.Size(1008, 412);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.CancelProcButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.SecurityFunctionsGridView);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1024, 450);
            this.Name = "SecurityFunctionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Security Functions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SecurityFunctionForm_FormClosing);
            this.Load += new System.EventHandler(this.SecurityFunctionForm_Load);
            this.Shown += new System.EventHandler(this.SecurityFunctionForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.SecurityFunctionsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityFunctionsBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView SecurityFunctionsGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ShowIdsCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SecurityRoleNameTextBox;
        private System.Windows.Forms.BindingSource securityFunctionsBindingSource;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button CancelProcButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn contentIdDataGridViewButtonColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSystemFunctionDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isRoleBasedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn SecurityRights;
    }
}