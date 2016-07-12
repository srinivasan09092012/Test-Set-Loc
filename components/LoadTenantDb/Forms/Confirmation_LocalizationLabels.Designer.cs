namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    partial class Confirmation_LocalizationLabels
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
            this.messagesGroupBox = new System.Windows.Forms.GroupBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.labelsGridView = new System.Windows.Forms.DataGridView();
            this.actionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moduleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalLabel = new System.Windows.Forms.Label();
            this.messagesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labelsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // messagesGroupBox
            // 
            this.messagesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesGroupBox.Controls.Add(this.totalLabel);
            this.messagesGroupBox.Controls.Add(this.cancelButton);
            this.messagesGroupBox.Controls.Add(this.loadButton);
            this.messagesGroupBox.Controls.Add(this.labelsGridView);
            this.messagesGroupBox.Location = new System.Drawing.Point(16, 15);
            this.messagesGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.messagesGroupBox.Name = "messagesGroupBox";
            this.messagesGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.messagesGroupBox.Size = new System.Drawing.Size(1107, 559);
            this.messagesGroupBox.TabIndex = 0;
            this.messagesGroupBox.TabStop = false;
            this.messagesGroupBox.Text = "Labels";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(891, 531);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButton.Location = new System.Drawing.Point(999, 530);
            this.loadButton.Margin = new System.Windows.Forms.Padding(4);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(100, 28);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadAllLocalizationMessages);
            // 
            // labelsGridView
            // 
            this.labelsGridView.AllowUserToAddRows = false;
            this.labelsGridView.AllowUserToDeleteRows = false;
            this.labelsGridView.AllowUserToResizeRows = false;
            this.labelsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.labelsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.actionColumn,
            this.messageContentId,
            this.localeColumn,
            this.moduleColumn,
            this.typeColumn,
            this.textColumn,
            this.idColumn});
            this.labelsGridView.Location = new System.Drawing.Point(8, 25);
            this.labelsGridView.Margin = new System.Windows.Forms.Padding(4);
            this.labelsGridView.Name = "labelsGridView";
            this.labelsGridView.Size = new System.Drawing.Size(1091, 500);
            this.labelsGridView.TabIndex = 0;
            // 
            // actionColumn
            // 
            this.actionColumn.HeaderText = "Action";
            this.actionColumn.Name = "actionColumn";
            // 
            // messageContentId
            // 
            this.messageContentId.HeaderText = "Content Id";
            this.messageContentId.Name = "messageContentId";
            // 
            // localeColumn
            // 
            this.localeColumn.HeaderText = "Locale";
            this.localeColumn.Name = "localeColumn";
            // 
            // moduleColumn
            // 
            this.moduleColumn.HeaderText = "Module";
            this.moduleColumn.Name = "moduleColumn";
            // 
            // typeColumn
            // 
            this.typeColumn.HeaderText = "Tooltip";
            this.typeColumn.Name = "typeColumn";
            // 
            // textColumn
            // 
            this.textColumn.HeaderText = "Text";
            this.textColumn.Name = "textColumn";
            // 
            // idColumn
            // 
            this.idColumn.HeaderText = "Id";
            this.idColumn.Name = "idColumn";
            this.idColumn.Visible = false;
            // 
            // totalLabel
            // 
            this.totalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.totalLabel.AutoSize = true;
            this.totalLabel.Location = new System.Drawing.Point(7, 536);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(76, 17);
            this.totalLabel.TabIndex = 3;
            this.totalLabel.Text = "Row Count";
            // 
            // Confirmation_LocalizationLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 588);
            this.Controls.Add(this.messagesGroupBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Confirmation_LocalizationLabels";
            this.Text = "Localization Labels";
            this.Load += new System.EventHandler(this.ConfirmationLocalizationLabelsLoad);
            this.messagesGroupBox.ResumeLayout(false);
            this.messagesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labelsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox messagesGroupBox;
        private System.Windows.Forms.DataGridView labelsGridView;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageContentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn localeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn moduleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.Label totalLabel;
    }
}