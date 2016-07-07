namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    partial class Confirmation_LocalizationMessages
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
            this.messagesGridView = new System.Windows.Forms.DataGridView();
            this.actionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moduleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messagesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messagesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // messagesGroupBox
            // 
            this.messagesGroupBox.Controls.Add(this.cancelButton);
            this.messagesGroupBox.Controls.Add(this.loadButton);
            this.messagesGroupBox.Controls.Add(this.messagesGridView);
            this.messagesGroupBox.Location = new System.Drawing.Point(12, 12);
            this.messagesGroupBox.Name = "messagesGroupBox";
            this.messagesGroupBox.Size = new System.Drawing.Size(830, 454);
            this.messagesGroupBox.TabIndex = 0;
            this.messagesGroupBox.TabStop = false;
            this.messagesGroupBox.Text = "Messages";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(668, 432);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(749, 431);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadAllLocalizationMessages);
            // 
            // messagesGridView
            // 
            this.messagesGridView.AllowUserToAddRows = false;
            this.messagesGridView.AllowUserToDeleteRows = false;
            this.messagesGridView.AllowUserToResizeRows = false;
            this.messagesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.messagesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.actionColumn,
            this.messageContentId,
            this.localeColumn,
            this.moduleColumn,
            this.typeColumn,
            this.textColumn,
            this.idColumn});
            this.messagesGridView.Location = new System.Drawing.Point(6, 20);
            this.messagesGridView.Name = "messagesGridView";
            this.messagesGridView.Size = new System.Drawing.Size(818, 406);
            this.messagesGridView.TabIndex = 0;
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
            this.typeColumn.HeaderText = "Type";
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
            // Confirmation_LocalizationMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 478);
            this.Controls.Add(this.messagesGroupBox);
            this.Name = "Confirmation_LocalizationMessages";
            this.Text = "Confirmation_LocalizationMessages";
            this.Load += new System.EventHandler(this.ConfirmationLocalizationMessagesLoad);
            this.messagesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messagesGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox messagesGroupBox;
        private System.Windows.Forms.DataGridView messagesGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageContentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn localeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn moduleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button loadButton;
    }
}