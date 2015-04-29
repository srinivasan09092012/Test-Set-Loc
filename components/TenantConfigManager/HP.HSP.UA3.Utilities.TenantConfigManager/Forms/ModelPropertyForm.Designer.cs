namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class ModelPropertyForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ModelDisplayTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ModelScopeTextBox = new System.Windows.Forms.TextBox();
            this.ShowIdsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ModelTypeTextBox = new System.Windows.Forms.TextBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ModelPropertiesGridView = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataTypeDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataRestrictionTypeDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.displayTypeDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.isRequiredDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.maxLengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelContentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hintTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accessKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createRestrictionSecurityRightIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readRestrictionSecurityRightIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateRestrictionSecurityRightIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteRestrictionSecurityRightIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelPropertiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ModelPropertiesGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modelPropertiesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ModelDisplayTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ModelScopeTextBox);
            this.groupBox1.Controls.Add(this.ShowIdsCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ModelTypeTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(983, 67);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Model";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(706, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Display";
            // 
            // ModelDisplayTextBox
            // 
            this.ModelDisplayTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModelDisplayTextBox.Location = new System.Drawing.Point(750, 19);
            this.ModelDisplayTextBox.Name = "ModelDisplayTextBox";
            this.ModelDisplayTextBox.ReadOnly = true;
            this.ModelDisplayTextBox.Size = new System.Drawing.Size(100, 20);
            this.ModelDisplayTextBox.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(435, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Scope";
            // 
            // ModelScopeTextBox
            // 
            this.ModelScopeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModelScopeTextBox.Location = new System.Drawing.Point(479, 19);
            this.ModelScopeTextBox.Name = "ModelScopeTextBox";
            this.ModelScopeTextBox.ReadOnly = true;
            this.ModelScopeTextBox.Size = new System.Drawing.Size(211, 20);
            this.ModelScopeTextBox.TabIndex = 8;
            // 
            // ShowIdsCheckBox
            // 
            this.ShowIdsCheckBox.AutoSize = true;
            this.ShowIdsCheckBox.Location = new System.Drawing.Point(869, 21);
            this.ShowIdsCheckBox.Name = "ShowIdsCheckBox";
            this.ShowIdsCheckBox.Size = new System.Drawing.Size(72, 17);
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
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type";
            // 
            // ModelTypeTextBox
            // 
            this.ModelTypeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModelTypeTextBox.Location = new System.Drawing.Point(56, 19);
            this.ModelTypeTextBox.Name = "ModelTypeTextBox";
            this.ModelTypeTextBox.ReadOnly = true;
            this.ModelTypeTextBox.Size = new System.Drawing.Size(362, 20);
            this.ModelTypeTextBox.TabIndex = 0;
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetButton.Enabled = false;
            this.ResetButton.Location = new System.Drawing.Point(12, 377);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 9;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(921, 377);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 8;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(840, 377);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ModelPropertiesGridView
            // 
            this.ModelPropertiesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModelPropertiesGridView.AutoGenerateColumns = false;
            this.ModelPropertiesGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.ModelPropertiesGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.ModelPropertiesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ModelPropertiesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.dataTypeDataGridViewComboBoxColumn,
            this.dataRestrictionTypeDataGridViewComboBoxColumn,
            this.displayTypeDataGridViewComboBoxColumn,
            this.isRequiredDataGridViewCheckBoxColumn,
            this.maxLengthDataGridViewTextBoxColumn,
            this.labelContentIdDataGridViewTextBoxColumn,
            this.defaultTextDataGridViewTextBoxColumn,
            this.hintTypeDataGridViewTextBoxColumn,
            this.accessKeyDataGridViewTextBoxColumn,
            this.createRestrictionSecurityRightIdDataGridViewTextBoxColumn,
            this.readRestrictionSecurityRightIdDataGridViewTextBoxColumn,
            this.updateRestrictionSecurityRightIdDataGridViewTextBoxColumn,
            this.deleteRestrictionSecurityRightIdDataGridViewTextBoxColumn});
            this.ModelPropertiesGridView.DataSource = this.modelPropertiesBindingSource;
            this.ModelPropertiesGridView.Location = new System.Drawing.Point(13, 85);
            this.ModelPropertiesGridView.Name = "ModelPropertiesGridView";
            this.ModelPropertiesGridView.Size = new System.Drawing.Size(983, 281);
            this.ModelPropertiesGridView.TabIndex = 6;
            this.ModelPropertiesGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ModelPropertiesGridView_CellEnter);
            this.ModelPropertiesGridView.CurrentCellChanged += new System.EventHandler(this.ModelPropertiesGridView_CurrentCellChanged);
            this.ModelPropertiesGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.ModelPropertiesGridView_DefaultValuesNeeded);
            this.ModelPropertiesGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.ModelPropertiesGridView_UserDeletingRow);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.idDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle15;
            this.idDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idDataGridViewTextBoxColumn.MinimumWidth = 225;
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
            this.idDataGridViewTextBoxColumn.Width = 225;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.nameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle16;
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 200;
            // 
            // dataTypeDataGridViewComboBoxColumn
            // 
            this.dataTypeDataGridViewComboBoxColumn.DataPropertyName = "DataType";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dataTypeDataGridViewComboBoxColumn.DefaultCellStyle = dataGridViewCellStyle17;
            this.dataTypeDataGridViewComboBoxColumn.HeaderText = "Data Type";
            this.dataTypeDataGridViewComboBoxColumn.Name = "dataTypeDataGridViewComboBoxColumn";
            this.dataTypeDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataTypeDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataTypeDataGridViewComboBoxColumn.Width = 76;
            // 
            // dataRestrictionTypeDataGridViewComboBoxColumn
            // 
            this.dataRestrictionTypeDataGridViewComboBoxColumn.DataPropertyName = "DataRestrictionType";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dataRestrictionTypeDataGridViewComboBoxColumn.DefaultCellStyle = dataGridViewCellStyle18;
            this.dataRestrictionTypeDataGridViewComboBoxColumn.HeaderText = "Data Restriction Type";
            this.dataRestrictionTypeDataGridViewComboBoxColumn.Name = "dataRestrictionTypeDataGridViewComboBoxColumn";
            this.dataRestrictionTypeDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataRestrictionTypeDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataRestrictionTypeDataGridViewComboBoxColumn.Width = 123;
            // 
            // displayTypeDataGridViewComboBoxColumn
            // 
            this.displayTypeDataGridViewComboBoxColumn.DataPropertyName = "DisplayType";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.displayTypeDataGridViewComboBoxColumn.DefaultCellStyle = dataGridViewCellStyle19;
            this.displayTypeDataGridViewComboBoxColumn.HeaderText = "Display Type";
            this.displayTypeDataGridViewComboBoxColumn.Name = "displayTypeDataGridViewComboBoxColumn";
            this.displayTypeDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.displayTypeDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.displayTypeDataGridViewComboBoxColumn.Width = 86;
            // 
            // isRequiredDataGridViewCheckBoxColumn
            // 
            this.isRequiredDataGridViewCheckBoxColumn.DataPropertyName = "IsRequired";
            this.isRequiredDataGridViewCheckBoxColumn.HeaderText = "Required";
            this.isRequiredDataGridViewCheckBoxColumn.Name = "isRequiredDataGridViewCheckBoxColumn";
            this.isRequiredDataGridViewCheckBoxColumn.Width = 56;
            // 
            // maxLengthDataGridViewTextBoxColumn
            // 
            this.maxLengthDataGridViewTextBoxColumn.DataPropertyName = "MaxLength";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle20.Format = "N0";
            dataGridViewCellStyle20.NullValue = "-1";
            this.maxLengthDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle20;
            this.maxLengthDataGridViewTextBoxColumn.HeaderText = "Max Length";
            this.maxLengthDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.maxLengthDataGridViewTextBoxColumn.Name = "maxLengthDataGridViewTextBoxColumn";
            this.maxLengthDataGridViewTextBoxColumn.Width = 81;
            // 
            // labelContentIdDataGridViewTextBoxColumn
            // 
            this.labelContentIdDataGridViewTextBoxColumn.DataPropertyName = "LabelContentId";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.labelContentIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle21;
            this.labelContentIdDataGridViewTextBoxColumn.HeaderText = "Label Content ID";
            this.labelContentIdDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.labelContentIdDataGridViewTextBoxColumn.Name = "labelContentIdDataGridViewTextBoxColumn";
            this.labelContentIdDataGridViewTextBoxColumn.Width = 200;
            // 
            // defaultTextDataGridViewTextBoxColumn
            // 
            this.defaultTextDataGridViewTextBoxColumn.DataPropertyName = "DefaultText";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.defaultTextDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle22;
            this.defaultTextDataGridViewTextBoxColumn.HeaderText = "Default Text";
            this.defaultTextDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.defaultTextDataGridViewTextBoxColumn.Name = "defaultTextDataGridViewTextBoxColumn";
            this.defaultTextDataGridViewTextBoxColumn.Width = 83;
            // 
            // hintTypeDataGridViewTextBoxColumn
            // 
            this.hintTypeDataGridViewTextBoxColumn.DataPropertyName = "HintType";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.hintTypeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle23;
            this.hintTypeDataGridViewTextBoxColumn.HeaderText = "Hint Type";
            this.hintTypeDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.hintTypeDataGridViewTextBoxColumn.Name = "hintTypeDataGridViewTextBoxColumn";
            this.hintTypeDataGridViewTextBoxColumn.Width = 72;
            // 
            // accessKeyDataGridViewTextBoxColumn
            // 
            this.accessKeyDataGridViewTextBoxColumn.DataPropertyName = "AccessKey";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.accessKeyDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle24;
            this.accessKeyDataGridViewTextBoxColumn.HeaderText = "Access Key";
            this.accessKeyDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.accessKeyDataGridViewTextBoxColumn.Name = "accessKeyDataGridViewTextBoxColumn";
            this.accessKeyDataGridViewTextBoxColumn.Width = 81;
            // 
            // createRestrictionSecurityRightIdDataGridViewTextBoxColumn
            // 
            this.createRestrictionSecurityRightIdDataGridViewTextBoxColumn.DataPropertyName = "CreateRestrictionSecurityRightId";
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.createRestrictionSecurityRightIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle25;
            this.createRestrictionSecurityRightIdDataGridViewTextBoxColumn.HeaderText = "Create Restriction Security Right ID";
            this.createRestrictionSecurityRightIdDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.createRestrictionSecurityRightIdDataGridViewTextBoxColumn.Name = "createRestrictionSecurityRightIdDataGridViewTextBoxColumn";
            this.createRestrictionSecurityRightIdDataGridViewTextBoxColumn.Width = 146;
            // 
            // readRestrictionSecurityRightIdDataGridViewTextBoxColumn
            // 
            this.readRestrictionSecurityRightIdDataGridViewTextBoxColumn.DataPropertyName = "ReadRestrictionSecurityRightId";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.readRestrictionSecurityRightIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle26;
            this.readRestrictionSecurityRightIdDataGridViewTextBoxColumn.HeaderText = "Read Restriction Security Right ID";
            this.readRestrictionSecurityRightIdDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.readRestrictionSecurityRightIdDataGridViewTextBoxColumn.Name = "readRestrictionSecurityRightIdDataGridViewTextBoxColumn";
            this.readRestrictionSecurityRightIdDataGridViewTextBoxColumn.Width = 141;
            // 
            // updateRestrictionSecurityRightIdDataGridViewTextBoxColumn
            // 
            this.updateRestrictionSecurityRightIdDataGridViewTextBoxColumn.DataPropertyName = "UpdateRestrictionSecurityRightId";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.updateRestrictionSecurityRightIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle27;
            this.updateRestrictionSecurityRightIdDataGridViewTextBoxColumn.HeaderText = "Update Restriction Security Right ID";
            this.updateRestrictionSecurityRightIdDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.updateRestrictionSecurityRightIdDataGridViewTextBoxColumn.Name = "updateRestrictionSecurityRightIdDataGridViewTextBoxColumn";
            this.updateRestrictionSecurityRightIdDataGridViewTextBoxColumn.Width = 150;
            // 
            // deleteRestrictionSecurityRightIdDataGridViewTextBoxColumn
            // 
            this.deleteRestrictionSecurityRightIdDataGridViewTextBoxColumn.DataPropertyName = "DeleteRestrictionSecurityRightId";
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.deleteRestrictionSecurityRightIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle28;
            this.deleteRestrictionSecurityRightIdDataGridViewTextBoxColumn.HeaderText = "Delete Restriction Security Right ID";
            this.deleteRestrictionSecurityRightIdDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.deleteRestrictionSecurityRightIdDataGridViewTextBoxColumn.Name = "deleteRestrictionSecurityRightIdDataGridViewTextBoxColumn";
            this.deleteRestrictionSecurityRightIdDataGridViewTextBoxColumn.Width = 146;
            // 
            // modelPropertiesBindingSource
            // 
            this.modelPropertiesBindingSource.DataSource = typeof(HP.HSP.UA3.Core.UX.Data.Configuration.ModelPropertyModel);
            // 
            // ModelPropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 412);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ModelPropertiesGridView);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1024, 450);
            this.Name = "ModelPropertyForm";
            this.Text = "Model Properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelPropertyForm_FormClosing);
            this.Load += new System.EventHandler(this.ModelPropertyForm_Load);
            this.Shown += new System.EventHandler(this.ModelPropertyForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ModelPropertiesGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modelPropertiesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ShowIdsCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ModelTypeTextBox;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridView ModelPropertiesGridView;
        private System.Windows.Forms.BindingSource modelPropertiesBindingSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ModelDisplayTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ModelScopeTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataTypeDataGridViewComboBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataRestrictionTypeDataGridViewComboBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn displayTypeDataGridViewComboBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isRequiredDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxLengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn labelContentIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hintTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accessKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createRestrictionSecurityRightIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn readRestrictionSecurityRightIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updateRestrictionSecurityRightIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deleteRestrictionSecurityRightIdDataGridViewTextBoxColumn;
    }
}