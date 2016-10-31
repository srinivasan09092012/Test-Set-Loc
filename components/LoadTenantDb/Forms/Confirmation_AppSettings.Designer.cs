namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    partial class Confirmation_AppSettings
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
            this.appSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.appSettingsGridView = new System.Windows.Forms.DataGridView();
            this.appSettingAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appSettingModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appSettingKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appSettingValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loadPushButton = new System.Windows.Forms.Button();
            this.cancelPushButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.appSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appSettingsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // appSettingsGroupBox
            // 
            this.appSettingsGroupBox.Controls.Add(this.appSettingsGridView);
            this.appSettingsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.appSettingsGroupBox.Name = "appSettingsGroupBox";
            this.appSettingsGroupBox.Size = new System.Drawing.Size(747, 349);
            this.appSettingsGroupBox.TabIndex = 11;
            this.appSettingsGroupBox.TabStop = false;
            this.appSettingsGroupBox.Text = "Application Settings";
            // 
            // appSettingsGridView
            // 
            this.appSettingsGridView.AllowUserToAddRows = false;
            this.appSettingsGridView.AllowUserToDeleteRows = false;
            this.appSettingsGridView.AllowUserToResizeRows = false;
            this.appSettingsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.appSettingsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.appSettingAction,
            this.appSettingModule,
            this.appSettingKey,
            this.appSettingValue});
            this.appSettingsGridView.Location = new System.Drawing.Point(18, 25);
            this.appSettingsGridView.MultiSelect = false;
            this.appSettingsGridView.Name = "appSettingsGridView";
            this.appSettingsGridView.ReadOnly = true;
            this.appSettingsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.appSettingsGridView.Size = new System.Drawing.Size(712, 308);
            this.appSettingsGridView.StandardTab = true;
            this.appSettingsGridView.TabIndex = 1;
            this.appSettingsGridView.TabStop = false;
            // 
            // appSettingAction
            // 
            this.appSettingAction.HeaderText = "Action";
            this.appSettingAction.Name = "appSettingAction";
            this.appSettingAction.ReadOnly = true;
            // 
            // appSettingModule
            // 
            this.appSettingModule.HeaderText = "Module";
            this.appSettingModule.Name = "appSettingModule";
            this.appSettingModule.ReadOnly = true;
            this.appSettingModule.Width = 130;
            // 
            // appSettingKey
            // 
            this.appSettingKey.HeaderText = "Key";
            this.appSettingKey.Name = "appSettingKey";
            this.appSettingKey.ReadOnly = true;
            this.appSettingKey.Width = 200;
            // 
            // appSettingValue
            // 
            this.appSettingValue.HeaderText = "Value";
            this.appSettingValue.Name = "appSettingValue";
            this.appSettingValue.ReadOnly = true;
            this.appSettingValue.Width = 200;
            // 
            // loadPushButton
            // 
            this.loadPushButton.Location = new System.Drawing.Point(1034, 488);
            this.loadPushButton.Name = "loadPushButton";
            this.loadPushButton.Size = new System.Drawing.Size(75, 23);
            this.loadPushButton.TabIndex = 1;
            this.loadPushButton.Text = "&Load...";
            this.loadPushButton.UseVisualStyleBackColor = true;
            this.loadPushButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // cancelPushButton
            // 
            this.cancelPushButton.Location = new System.Drawing.Point(1129, 488);
            this.cancelPushButton.Name = "cancelPushButton";
            this.cancelPushButton.Size = new System.Drawing.Size(75, 23);
            this.cancelPushButton.TabIndex = 14;
            this.cancelPushButton.Text = "Cancel";
            this.cancelPushButton.UseVisualStyleBackColor = true;
            this.cancelPushButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(686, 379);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(56, 29);
            this.loadButton.TabIndex = 15;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(593, 379);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(72, 29);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // Confirmation_AppSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 512);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.cancelPushButton);
            this.Controls.Add(this.appSettingsGroupBox);
            this.Controls.Add(this.loadPushButton);
            this.Name = "Confirmation_AppSettings";
            this.Text = "Model Configuration";
            this.Load += new System.EventHandler(this.Confirmation_AppSettings_Load);
            this.appSettingsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.appSettingsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox appSettingsGroupBox;
        private System.Windows.Forms.DataGridView appSettingsGridView;
        private System.Windows.Forms.Button loadPushButton;
        private System.Windows.Forms.Button cancelPushButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn appSettingAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn appSettingModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn appSettingKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn appSettingValue;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button cancelButton;
    }
}