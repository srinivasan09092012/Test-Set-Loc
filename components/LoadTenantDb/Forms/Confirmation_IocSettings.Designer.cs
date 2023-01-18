namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    partial class Confirmation_IocSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.iocSettingdataGridView = new System.Windows.Forms.DataGridView();
            this.loadButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.iocConfigurationAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iocConfigurationModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iocConfigurationValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iocSettingdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.iocSettingdataGridView);
            this.groupBox1.Location = new System.Drawing.Point(21, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(716, 350);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IOC Configuration";
            // 
            // iocSettingdataGridView
            // 
            this.iocSettingdataGridView.AllowUserToAddRows = false;
            this.iocSettingdataGridView.AllowUserToDeleteRows = false;
            this.iocSettingdataGridView.AllowUserToResizeRows = false;
            this.iocSettingdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.iocSettingdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iocConfigurationAction,
            this.iocConfigurationModule,
            this.iocConfigurationValue});
            this.iocSettingdataGridView.Location = new System.Drawing.Point(6, 21);
            this.iocSettingdataGridView.MultiSelect = false;
            this.iocSettingdataGridView.Name = "iocSettingdataGridView";
            this.iocSettingdataGridView.ReadOnly = true;
            this.iocSettingdataGridView.RowTemplate.Height = 24;
            this.iocSettingdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.iocSettingdataGridView.Size = new System.Drawing.Size(704, 314);
            this.iocSettingdataGridView.TabIndex = 0;
            this.iocSettingdataGridView.TabStop = false;
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(641, 389);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(95, 33);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(536, 392);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(93, 30);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // iocConfigurationAction
            // 
            this.iocConfigurationAction.HeaderText = "Action";
            this.iocConfigurationAction.Name = "iocConfigurationAction";
            // 
            // iocConfigurationModule
            // 
            this.iocConfigurationModule.HeaderText = "Module";
            this.iocConfigurationModule.Name = "iocConfigurationModule";
            // 
            // iocConfigurationValue
            // 
            this.iocConfigurationValue.HeaderText = "Value";
            this.iocConfigurationValue.Name = "iocConfigurationValue";
            this.iocConfigurationValue.Width = 550;
            // 
            // Confirmation_IocSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 521);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "Confirmation_IocSettings";
            this.Text = "Model Configuration";
            this.Load += new System.EventHandler(this.Confirmation_IocSettings_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iocSettingdataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridView iocSettingdataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn iocConfigurationAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn iocConfigurationModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn iocConfigurationValue;
    }
}