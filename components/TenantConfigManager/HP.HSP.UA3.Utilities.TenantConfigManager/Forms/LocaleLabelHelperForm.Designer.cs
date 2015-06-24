namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class LocaleLabelHelperForm
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
            this.LabelsGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LabelContentIdTextBox = new System.Windows.Forms.TextBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.CancelProcButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.labelsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.localeLabelIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocaleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tooltipDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.LabelsGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labelsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelsGridView
            // 
            this.LabelsGridView.AllowUserToAddRows = false;
            this.LabelsGridView.AllowUserToDeleteRows = false;
            this.LabelsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelsGridView.AutoGenerateColumns = false;
            this.LabelsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.LabelsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.LabelsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LabelsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.localeLabelIdDataGridViewTextBoxColumn,
            this.LocaleId,
            this.textDataGridViewTextBoxColumn,
            this.tooltipDataGridViewTextBoxColumn});
            this.LabelsGridView.DataSource = this.labelsBindingSource;
            this.LabelsGridView.Enabled = false;
            this.LabelsGridView.Location = new System.Drawing.Point(13, 106);
            this.LabelsGridView.Name = "LabelsGridView";
            this.LabelsGridView.Size = new System.Drawing.Size(983, 265);
            this.LabelsGridView.TabIndex = 29;
            this.LabelsGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.LabelsGridView_CellEnter);
            this.LabelsGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.LabelsGridView_DefaultValuesNeeded);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.EditButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.LabelContentIdTextBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(983, 88);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Label";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Content ID";
            // 
            // LabelContentIdTextBox
            // 
            this.LabelContentIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelContentIdTextBox.Location = new System.Drawing.Point(78, 30);
            this.LabelContentIdTextBox.Name = "LabelContentIdTextBox";
            this.LabelContentIdTextBox.Size = new System.Drawing.Size(457, 20);
            this.LabelContentIdTextBox.TabIndex = 0;
            this.LabelContentIdTextBox.TextChanged += new System.EventHandler(this.LabelContentIdTextBox_TextChanged);
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetButton.Enabled = false;
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetButton.Location = new System.Drawing.Point(12, 377);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 27;
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
            this.CancelProcButton.TabIndex = 26;
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
            this.SaveButton.TabIndex = 25;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditButton.Location = new System.Drawing.Point(541, 30);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(75, 23);
            this.EditButton.TabIndex = 26;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // labelsBindingSource
            // 
            this.labelsBindingSource.DataSource = typeof(HP.HSP.UA3.Utilities.TenantConfigManager.Data.LocaleLabelHelperModel);
            this.labelsBindingSource.CurrentItemChanged += new System.EventHandler(this.labelsBindingSource_CurrentItemChanged);
            // 
            // localeLabelIdDataGridViewTextBoxColumn
            // 
            this.localeLabelIdDataGridViewTextBoxColumn.DataPropertyName = "LocaleLabelId";
            this.localeLabelIdDataGridViewTextBoxColumn.Frozen = true;
            this.localeLabelIdDataGridViewTextBoxColumn.HeaderText = "ID";
            this.localeLabelIdDataGridViewTextBoxColumn.MinimumWidth = 250;
            this.localeLabelIdDataGridViewTextBoxColumn.Name = "localeLabelIdDataGridViewTextBoxColumn";
            this.localeLabelIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.localeLabelIdDataGridViewTextBoxColumn.Visible = false;
            this.localeLabelIdDataGridViewTextBoxColumn.Width = 250;
            // 
            // LocaleId
            // 
            this.LocaleId.DataPropertyName = "LocaleId";
            this.LocaleId.Frozen = true;
            this.LocaleId.HeaderText = "Locale";
            this.LocaleId.Name = "LocaleId";
            this.LocaleId.ReadOnly = true;
            this.LocaleId.Width = 64;
            // 
            // textDataGridViewTextBoxColumn
            // 
            this.textDataGridViewTextBoxColumn.DataPropertyName = "Text";
            this.textDataGridViewTextBoxColumn.HeaderText = "Text";
            this.textDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.textDataGridViewTextBoxColumn.Name = "textDataGridViewTextBoxColumn";
            this.textDataGridViewTextBoxColumn.Width = 200;
            // 
            // tooltipDataGridViewTextBoxColumn
            // 
            this.tooltipDataGridViewTextBoxColumn.DataPropertyName = "Tooltip";
            this.tooltipDataGridViewTextBoxColumn.HeaderText = "Tooltip";
            this.tooltipDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.tooltipDataGridViewTextBoxColumn.Name = "tooltipDataGridViewTextBoxColumn";
            this.tooltipDataGridViewTextBoxColumn.Width = 200;
            // 
            // LocaleLabelHelperForm
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelProcButton;
            this.ClientSize = new System.Drawing.Size(1008, 412);
            this.Controls.Add(this.LabelsGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.CancelProcButton);
            this.Controls.Add(this.SaveButton);
            this.MinimumSize = new System.Drawing.Size(1024, 450);
            this.Name = "LocaleLabelHelperForm";
            this.Text = "Locale Label Helper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LocaleLabelHelperForm_FormClosing);
            this.Load += new System.EventHandler(this.LocaleLabelHelperForm_Load);
            this.Shown += new System.EventHandler(this.LocaleLabelHelperForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.LabelsGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labelsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView LabelsGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LabelContentIdTextBox;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button CancelProcButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.BindingSource labelsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn localeCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn localeLabelIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocaleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn textDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tooltipDataGridViewTextBoxColumn;
    }
}