namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    partial class Confirmation_Services
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
            this.loadPushButton = new System.Windows.Forms.Button();
            this.servicesGridView = new System.Windows.Forms.DataGridView();
            this.serviceAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.securityRightId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iocContainer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.servicesGroupBox = new System.Windows.Forms.GroupBox();
            this.cancelPushButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.servicesGridView)).BeginInit();
            this.servicesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadPushButton
            // 
            this.loadPushButton.Location = new System.Drawing.Point(1077, 500);
            this.loadPushButton.Name = "loadPushButton";
            this.loadPushButton.Size = new System.Drawing.Size(75, 23);
            this.loadPushButton.TabIndex = 1;
            this.loadPushButton.Text = "&Load...";
            this.loadPushButton.UseVisualStyleBackColor = true;
            this.loadPushButton.Click += new System.EventHandler(this.ProcessLoad);
            // 
            // servicesGridView
            // 
            this.servicesGridView.AllowUserToAddRows = false;
            this.servicesGridView.AllowUserToDeleteRows = false;
            this.servicesGridView.AllowUserToResizeRows = false;
            this.servicesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.servicesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serviceAction,
            this.serviceModule,
            this.serviceId,
            this.serviceName,
            this.baseUrl,
            this.labelContentId,
            this.defaultText,
            this.securityRightId,
            this.iocContainer});
            this.servicesGridView.Location = new System.Drawing.Point(21, 19);
            this.servicesGridView.MultiSelect = false;
            this.servicesGridView.Name = "servicesGridView";
            this.servicesGridView.ReadOnly = true;
            this.servicesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.servicesGridView.Size = new System.Drawing.Size(1200, 457);
            this.servicesGridView.TabIndex = 4;
            this.servicesGridView.TabStop = false;
            // 
            // serviceAction
            // 
            this.serviceAction.HeaderText = "Action";
            this.serviceAction.Name = "serviceAction";
            this.serviceAction.ReadOnly = true;
            // 
            // menuItemModule
            // 
            this.serviceModule.HeaderText = "Module";
            this.serviceModule.Name = "menuItemModule";
            this.serviceModule.ReadOnly = true;
            this.serviceModule.Width = 130;
            // 
            // serviceId
            // 
            this.serviceId.HeaderText = "Service Id";
            this.serviceId.Name = "serviceId";
            this.serviceId.ReadOnly = true;
            this.serviceId.Width = 225;
            // 
            // serviceName
            // 
            this.serviceName.HeaderText = "Name";
            this.serviceName.Name = "serviceName";
            this.serviceName.ReadOnly = true;
            this.serviceName.Width = 200;
            // 
            // baseUrl
            // 
            this.baseUrl.HeaderText = "Base URL";
            this.baseUrl.Name = "baseUrl";
            this.baseUrl.ReadOnly = true;
            this.baseUrl.Width = 200;
            // 
            // labelContentId
            // 
            this.labelContentId.HeaderText = "Label Content ID";
            this.labelContentId.Name = "labelContentId";
            this.labelContentId.ReadOnly = true;
            this.labelContentId.Width = 225;
            // 
            // defaultText
            // 
            this.defaultText.HeaderText = "Default Text";
            this.defaultText.Name = "defaultText";
            this.defaultText.ReadOnly = true;
            // 
            // securityRightId
            // 
            this.securityRightId.HeaderText = "Security Right ID";
            this.securityRightId.Name = "securityRightId";
            this.securityRightId.ReadOnly = true;
            // 
            // iocContainer
            // 
            this.iocContainer.HeaderText = "IOC Container";
            this.iocContainer.Name = "iocContainer";
            this.iocContainer.ReadOnly = true;
            // 
            // servicesGroupBox
            // 
            this.servicesGroupBox.Controls.Add(this.servicesGridView);
            this.servicesGroupBox.Location = new System.Drawing.Point(12, 12);
            this.servicesGroupBox.Name = "servicesGroupBox";
            this.servicesGroupBox.Size = new System.Drawing.Size(1238, 482);
            this.servicesGroupBox.TabIndex = 4;
            this.servicesGroupBox.TabStop = false;
            this.servicesGroupBox.Text = "Services";
            // 
            // cancelPushButton
            // 
            this.cancelPushButton.Location = new System.Drawing.Point(1158, 500);
            this.cancelPushButton.Name = "cancelPushButton";
            this.cancelPushButton.Size = new System.Drawing.Size(75, 23);
            this.cancelPushButton.TabIndex = 6;
            this.cancelPushButton.Text = "Cancel";
            this.cancelPushButton.UseVisualStyleBackColor = true;
            this.cancelPushButton.Click += new System.EventHandler(this.cancelPushButton_Click);
            // 
            // Confirmation_Services
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 529);
            this.Controls.Add(this.cancelPushButton);
            this.Controls.Add(this.loadPushButton);
            this.Controls.Add(this.servicesGroupBox);
            this.Name = "Confirmation_Services";
            this.Text = "Service Configuration";
            this.Load += new System.EventHandler(this.Confirmation_Services_Load);
            ((System.ComponentModel.ISupportInitialize)(this.servicesGridView)).EndInit();
            this.servicesGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button loadPushButton;
        private System.Windows.Forms.DataGridView servicesGridView;
        private System.Windows.Forms.GroupBox servicesGroupBox;
        private System.Windows.Forms.Button cancelPushButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn labelContentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultText;
        private System.Windows.Forms.DataGridViewTextBoxColumn securityRightId;
        private System.Windows.Forms.DataGridViewTextBoxColumn iocContainer;
    }
}