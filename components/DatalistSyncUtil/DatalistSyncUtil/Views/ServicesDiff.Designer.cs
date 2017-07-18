//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil.Views
{
    partial class ServicesDiff
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
            this.moduleList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.diffServices = new System.Windows.Forms.TabControl();
            this.newSvc = new System.Windows.Forms.TabPage();
            this.newServicesView = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LabelItemKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaseUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IocContainer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewServicesSelectAllCB = new System.Windows.Forms.CheckBox();
            this.updSvc = new System.Windows.Forms.TabPage();
            this.TargetServices = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn19 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tgtName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tgtSecurityRightItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tgtLabelItemKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tgtDefaultText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tgtBaseUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tgtIocContainer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn20 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn21 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateServicesSelectAllCB = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SourceServices = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn13 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.srcSecurityRightItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.srcLabelItemKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.srcDefaultText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.srcBaseUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.srcIocContainer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SecurityRightItemIDModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LabelItemKeyModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultTextModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaseUrlModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IOCContainerModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActiveModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.srcIsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn15 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreviewUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnIncludeServices = new System.Windows.Forms.Button();
            this.diffServices.SuspendLayout();
            this.newSvc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newServicesView)).BeginInit();
            this.updSvc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceServices)).BeginInit();
            this.SuspendLayout();
            // 
            // moduleList
            // 
            this.moduleList.DisplayMember = "ModuleName";
            this.moduleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleList.FormattingEnabled = true;
            this.moduleList.Location = new System.Drawing.Point(74, 12);
            this.moduleList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.moduleList.Name = "moduleList";
            this.moduleList.Size = new System.Drawing.Size(209, 21);
            this.moduleList.Sorted = true;
            this.moduleList.TabIndex = 34;
            this.moduleList.ValueMember = "TenantModuleID";
            this.moduleList.SelectedIndexChanged += new System.EventHandler(this.ModuleList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 33;
            this.label4.Text = "Module:";
            // 
            // diffServices
            // 
            this.diffServices.Controls.Add(this.newSvc);
            this.diffServices.Controls.Add(this.updSvc);
            this.diffServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diffServices.Location = new System.Drawing.Point(12, 48);
            this.diffServices.Name = "diffServices";
            this.diffServices.SelectedIndex = 0;
            this.diffServices.Size = new System.Drawing.Size(1416, 459);
            this.diffServices.TabIndex = 35;
            // 
            // newSvc
            // 
            this.newSvc.Controls.Add(this.newServicesView);
            this.newSvc.Controls.Add(this.NewServicesSelectAllCB);
            this.newSvc.Location = new System.Drawing.Point(4, 22);
            this.newSvc.Name = "newSvc";
            this.newSvc.Padding = new System.Windows.Forms.Padding(3);
            this.newSvc.Size = new System.Drawing.Size(1408, 433);
            this.newSvc.TabIndex = 0;
            this.newSvc.Text = "New Services";
            this.newSvc.UseVisualStyleBackColor = true;
            // 
            // newServicesView
            // 
            this.newServicesView.AllowUserToAddRows = false;
            this.newServicesView.AllowUserToDeleteRows = false;
            this.newServicesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.newServicesView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.dataGridViewTextBoxColumn1,
            this.Code,
            this.LabelItemKey,
            this.DefaultText,
            this.BaseUrl,
            this.IocContainer,
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.newServicesView.Location = new System.Drawing.Point(5, 29);
            this.newServicesView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.newServicesView.Name = "newServicesView";
            this.newServicesView.RowTemplate.Height = 24;
            this.newServicesView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newServicesView.Size = new System.Drawing.Size(1297, 398);
            this.newServicesView.TabIndex = 10;
            this.newServicesView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SelectColumn_Clicked);
            // 
            // Select
            // 
            this.Select.HeaderText = "";
            this.Select.Name = "Select";
            this.Select.Width = 25;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 265;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "SecurityRightItemID";
            this.Code.HeaderText = "SecurityRightItemID";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 200;
            // 
            // LabelItemKey
            // 
            this.LabelItemKey.DataPropertyName = "LabelItemKey";
            this.LabelItemKey.HeaderText = "LabelItemKey";
            this.LabelItemKey.Name = "LabelItemKey";
            this.LabelItemKey.Width = 200;
            // 
            // DefaultText
            // 
            this.DefaultText.DataPropertyName = "DefaultText";
            this.DefaultText.HeaderText = "DefaultText";
            this.DefaultText.Name = "DefaultText";
            // 
            // BaseUrl
            // 
            this.BaseUrl.DataPropertyName = "BaseUrl";
            this.BaseUrl.HeaderText = "Base Url";
            this.BaseUrl.Name = "BaseUrl";
            this.BaseUrl.Width = 300;
            // 
            // IocContainer
            // 
            this.IocContainer.DataPropertyName = "IocContainer";
            this.IocContainer.HeaderText = "IOC Container";
            this.IocContainer.Name = "IocContainer";
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn2.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxColumn2.Width = 60;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn3.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.ReadOnly = true;
            this.dataGridViewCheckBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn3.Visible = false;
            this.dataGridViewCheckBoxColumn3.Width = 70;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn4.HeaderText = "Status";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // NewServicesSelectAllCB
            // 
            this.NewServicesSelectAllCB.AutoSize = true;
            this.NewServicesSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewServicesSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewServicesSelectAllCB.Location = new System.Drawing.Point(5, 6);
            this.NewServicesSelectAllCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewServicesSelectAllCB.Name = "NewServicesSelectAllCB";
            this.NewServicesSelectAllCB.Size = new System.Drawing.Size(80, 17);
            this.NewServicesSelectAllCB.TabIndex = 9;
            this.NewServicesSelectAllCB.Text = "Select All";
            this.NewServicesSelectAllCB.UseVisualStyleBackColor = true;
            this.NewServicesSelectAllCB.CheckedChanged += new System.EventHandler(this.NewServicesSelectAllCB_CheckedChanged);
            // 
            // updSvc
            // 
            this.updSvc.Controls.Add(this.TargetServices);
            this.updSvc.Controls.Add(this.UpdateServicesSelectAllCB);
            this.updSvc.Controls.Add(this.label6);
            this.updSvc.Controls.Add(this.label7);
            this.updSvc.Controls.Add(this.SourceServices);
            this.updSvc.Location = new System.Drawing.Point(4, 22);
            this.updSvc.Name = "updSvc";
            this.updSvc.Padding = new System.Windows.Forms.Padding(3);
            this.updSvc.Size = new System.Drawing.Size(1408, 433);
            this.updSvc.TabIndex = 1;
            this.updSvc.Text = "Updated Services";
            this.updSvc.UseVisualStyleBackColor = true;
            // 
            // TargetServices
            // 
            this.TargetServices.AllowUserToAddRows = false;
            this.TargetServices.AllowUserToDeleteRows = false;
            this.TargetServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TargetServices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn19,
            this.tgtName,
            this.tgtSecurityRightItemID,
            this.tgtLabelItemKey,
            this.tgtDefaultText,
            this.tgtBaseUrl,
            this.tgtIocContainer,
            this.dataGridViewCheckBoxColumn20,
            this.dataGridViewCheckBoxColumn21,
            this.dataGridViewTextBoxColumn33});
            this.TargetServices.Location = new System.Drawing.Point(717, 31);
            this.TargetServices.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TargetServices.Name = "TargetServices";
            this.TargetServices.RowTemplate.Height = 24;
            this.TargetServices.Size = new System.Drawing.Size(681, 396);
            this.TargetServices.TabIndex = 19;
            this.TargetServices.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TargetServices_Scroll);
            // 
            // dataGridViewCheckBoxColumn19
            // 
            this.dataGridViewCheckBoxColumn19.HeaderText = "";
            this.dataGridViewCheckBoxColumn19.Name = "dataGridViewCheckBoxColumn19";
            this.dataGridViewCheckBoxColumn19.Visible = false;
            this.dataGridViewCheckBoxColumn19.Width = 25;
            // 
            // tgtName
            // 
            this.tgtName.DataPropertyName = "Name";
            this.tgtName.HeaderText = "Name";
            this.tgtName.Name = "tgtName";
            this.tgtName.ReadOnly = true;
            this.tgtName.Width = 180;
            // 
            // tgtSecurityRightItemID
            // 
            this.tgtSecurityRightItemID.DataPropertyName = "SecurityRightItemID";
            this.tgtSecurityRightItemID.HeaderText = "SecurityRightItemID";
            this.tgtSecurityRightItemID.Name = "tgtSecurityRightItemID";
            this.tgtSecurityRightItemID.ReadOnly = true;
            this.tgtSecurityRightItemID.Width = 140;
            // 
            // tgtLabelItemKey
            // 
            this.tgtLabelItemKey.DataPropertyName = "LabelItemKey";
            this.tgtLabelItemKey.HeaderText = "LabelItemKey";
            this.tgtLabelItemKey.Name = "tgtLabelItemKey";
            // 
            // tgtDefaultText
            // 
            this.tgtDefaultText.DataPropertyName = "DefaultText";
            this.tgtDefaultText.HeaderText = "DefaultText";
            this.tgtDefaultText.Name = "tgtDefaultText";
            // 
            // tgtBaseUrl
            // 
            this.tgtBaseUrl.DataPropertyName = "BaseUrl";
            this.tgtBaseUrl.HeaderText = "BaseUrl";
            this.tgtBaseUrl.Name = "tgtBaseUrl";
            // 
            // tgtIocContainer
            // 
            this.tgtIocContainer.DataPropertyName = "IOCContainer";
            this.tgtIocContainer.HeaderText = "IOCContainer";
            this.tgtIocContainer.Name = "tgtIocContainer";
            // 
            // dataGridViewCheckBoxColumn20
            // 
            this.dataGridViewCheckBoxColumn20.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn20.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn20.Name = "dataGridViewCheckBoxColumn20";
            this.dataGridViewCheckBoxColumn20.ReadOnly = true;
            this.dataGridViewCheckBoxColumn20.Width = 50;
            // 
            // dataGridViewCheckBoxColumn21
            // 
            this.dataGridViewCheckBoxColumn21.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn21.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn21.Name = "dataGridViewCheckBoxColumn21";
            this.dataGridViewCheckBoxColumn21.ReadOnly = true;
            this.dataGridViewCheckBoxColumn21.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn21.Visible = false;
            this.dataGridViewCheckBoxColumn21.Width = 55;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn33.HeaderText = "Status";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            this.dataGridViewTextBoxColumn33.Visible = false;
            this.dataGridViewTextBoxColumn33.Width = 50;
            // 
            // UpdateServicesSelectAllCB
            // 
            this.UpdateServicesSelectAllCB.AutoSize = true;
            this.UpdateServicesSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateServicesSelectAllCB.Location = new System.Drawing.Point(69, 8);
            this.UpdateServicesSelectAllCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.UpdateServicesSelectAllCB.Name = "UpdateServicesSelectAllCB";
            this.UpdateServicesSelectAllCB.Size = new System.Drawing.Size(80, 17);
            this.UpdateServicesSelectAllCB.TabIndex = 18;
            this.UpdateServicesSelectAllCB.Text = "Select All";
            this.UpdateServicesSelectAllCB.UseVisualStyleBackColor = true;
            this.UpdateServicesSelectAllCB.CheckedChanged += new System.EventHandler(this.UpdateServicesSelectAllCB_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(683, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Target:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 8);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Source:";
            // 
            // SourceServices
            // 
            this.SourceServices.AllowUserToAddRows = false;
            this.SourceServices.AllowUserToDeleteRows = false;
            this.SourceServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SourceServices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn13,
            this.dataGridViewTextBoxColumn19,
            this.srcSecurityRightItemID,
            this.srcLabelItemKey,
            this.srcDefaultText,
            this.srcBaseUrl,
            this.srcIocContainer,
            this.SecurityRightItemIDModified,
            this.LabelItemKeyModified,
            this.DefaultTextModified,
            this.BaseUrlModified,
            this.IOCContainerModified,
            this.IsActiveModified,
            this.srcIsActive,
            this.dataGridViewCheckBoxColumn15,
            this.dataGridViewTextBoxColumn21});
            this.SourceServices.Location = new System.Drawing.Point(5, 31);
            this.SourceServices.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SourceServices.Name = "SourceServices";
            this.SourceServices.RowTemplate.Height = 24;
            this.SourceServices.Size = new System.Drawing.Size(717, 396);
            this.SourceServices.TabIndex = 13;
            this.SourceServices.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SelectUpdateServices_Clicked);
            this.SourceServices.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.SourceServices_RowAdded);
            this.SourceServices.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SourceServices_Scroll);
            // 
            // dataGridViewCheckBoxColumn13
            // 
            this.dataGridViewCheckBoxColumn13.HeaderText = "";
            this.dataGridViewCheckBoxColumn13.Name = "dataGridViewCheckBoxColumn13";
            this.dataGridViewCheckBoxColumn13.Width = 25;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn19.HeaderText = "Name";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Width = 180;
            // 
            // srcSecurityRightItemID
            // 
            this.srcSecurityRightItemID.DataPropertyName = "SecurityRightItemID";
            this.srcSecurityRightItemID.HeaderText = "SecurityRightItemID";
            this.srcSecurityRightItemID.Name = "srcSecurityRightItemID";
            this.srcSecurityRightItemID.ReadOnly = true;
            this.srcSecurityRightItemID.Width = 140;
            // 
            // srcLabelItemKey
            // 
            this.srcLabelItemKey.DataPropertyName = "LabelItemKey";
            this.srcLabelItemKey.HeaderText = "LabelItemKey";
            this.srcLabelItemKey.Name = "srcLabelItemKey";
            // 
            // srcDefaultText
            // 
            this.srcDefaultText.DataPropertyName = "DefaultText";
            this.srcDefaultText.HeaderText = "DefaultText";
            this.srcDefaultText.Name = "srcDefaultText";
            this.srcDefaultText.Width = 80;
            // 
            // srcBaseUrl
            // 
            this.srcBaseUrl.DataPropertyName = "BaseUrl";
            this.srcBaseUrl.HeaderText = "BaseUrl";
            this.srcBaseUrl.Name = "srcBaseUrl";
            // 
            // srcIocContainer
            // 
            this.srcIocContainer.DataPropertyName = "IOCContainer";
            this.srcIocContainer.HeaderText = "IOCContainer";
            this.srcIocContainer.Name = "srcIocContainer";
            // 
            // SecurityRightItemIDModified
            // 
            this.SecurityRightItemIDModified.DataPropertyName = "SecRightItemModified";
            this.SecurityRightItemIDModified.HeaderText = "SecurityRightItemIDModified";
            this.SecurityRightItemIDModified.Name = "SecurityRightItemIDModified";
            this.SecurityRightItemIDModified.Visible = false;
            // 
            // LabelItemKeyModified
            // 
            this.LabelItemKeyModified.DataPropertyName = "LabelItemModified";
            this.LabelItemKeyModified.HeaderText = "LabelItemKeyModified";
            this.LabelItemKeyModified.Name = "LabelItemKeyModified";
            this.LabelItemKeyModified.Visible = false;
            // 
            // DefaultTextModified
            // 
            this.DefaultTextModified.DataPropertyName = "DefaultTextModified";
            this.DefaultTextModified.HeaderText = "DefaultTextModified";
            this.DefaultTextModified.Name = "DefaultTextModified";
            this.DefaultTextModified.Visible = false;
            // 
            // BaseUrlModified
            // 
            this.BaseUrlModified.DataPropertyName = "BaseURLModified";
            this.BaseUrlModified.HeaderText = "BaseUrlModified";
            this.BaseUrlModified.Name = "BaseUrlModified";
            this.BaseUrlModified.Visible = false;
            // 
            // IOCContainerModified
            // 
            this.IOCContainerModified.DataPropertyName = "IOCContainerModified";
            this.IOCContainerModified.HeaderText = "IOCContainerModified";
            this.IOCContainerModified.Name = "IOCContainerModified";
            this.IOCContainerModified.Visible = false;
            // 
            // IsActiveModified
            // 
            this.IsActiveModified.DataPropertyName = "IsActiveModified";
            this.IsActiveModified.HeaderText = "IsActiveModified";
            this.IsActiveModified.Name = "IsActiveModified";
            this.IsActiveModified.Visible = false;
            // 
            // srcIsActive
            // 
            this.srcIsActive.DataPropertyName = "IsActive";
            this.srcIsActive.HeaderText = "Active";
            this.srcIsActive.Name = "srcIsActive";
            this.srcIsActive.ReadOnly = true;
            this.srcIsActive.Width = 50;
            // 
            // dataGridViewCheckBoxColumn15
            // 
            this.dataGridViewCheckBoxColumn15.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn15.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn15.Name = "dataGridViewCheckBoxColumn15";
            this.dataGridViewCheckBoxColumn15.ReadOnly = true;
            this.dataGridViewCheckBoxColumn15.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn15.ThreeState = true;
            this.dataGridViewCheckBoxColumn15.Visible = false;
            this.dataGridViewCheckBoxColumn15.Width = 55;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn21.HeaderText = "Status";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.Visible = false;
            this.dataGridViewTextBoxColumn21.Width = 60;
            // 
            // PreviewUpdate
            // 
            this.PreviewUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviewUpdate.Location = new System.Drawing.Point(904, 509);
            this.PreviewUpdate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PreviewUpdate.Name = "PreviewUpdate";
            this.PreviewUpdate.Size = new System.Drawing.Size(132, 26);
            this.PreviewUpdate.TabIndex = 37;
            this.PreviewUpdate.Text = "Preview && Update";
            this.PreviewUpdate.UseVisualStyleBackColor = true;
            this.PreviewUpdate.Click += new System.EventHandler(this.PreviewUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1047, 509);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 26);
            this.btnClose.TabIndex = 36;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnIncludeServices
            // 
            this.btnIncludeServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIncludeServices.Location = new System.Drawing.Point(12, 513);
            this.btnIncludeServices.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnIncludeServices.Name = "btnIncludeServices";
            this.btnIncludeServices.Size = new System.Drawing.Size(72, 25);
            this.btnIncludeServices.TabIndex = 38;
            this.btnIncludeServices.Text = "Include";
            this.btnIncludeServices.UseVisualStyleBackColor = true;
            this.btnIncludeServices.Click += new System.EventHandler(this.BtnIncludeServices_Click);
            // 
            // ServicesDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1451, 539);
            this.Controls.Add(this.btnIncludeServices);
            this.Controls.Add(this.PreviewUpdate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.diffServices);
            this.Controls.Add(this.moduleList);
            this.Controls.Add(this.label4);
            this.Name = "ServicesDiff";
            this.Text = "ServicesDiff";
            this.diffServices.ResumeLayout(false);
            this.newSvc.ResumeLayout(false);
            this.newSvc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newServicesView)).EndInit();
            this.updSvc.ResumeLayout(false);
            this.updSvc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceServices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox moduleList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl diffServices;
        private System.Windows.Forms.TabPage newSvc;
        private System.Windows.Forms.TabPage updSvc;
        private System.Windows.Forms.CheckBox NewServicesSelectAllCB;
        private System.Windows.Forms.DataGridView newServicesView;
        private System.Windows.Forms.Button PreviewUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView SourceServices;
        private System.Windows.Forms.Button btnIncludeServices;
        private System.Windows.Forms.CheckBox UpdateServicesSelectAllCB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView TargetServices;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabelItemKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultText;
        private System.Windows.Forms.DataGridViewTextBoxColumn BaseUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn IocContainer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn tgtName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tgtSecurityRightItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn tgtLabelItemKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn tgtDefaultText;
        private System.Windows.Forms.DataGridViewTextBoxColumn tgtBaseUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn tgtIocContainer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn20;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn srcSecurityRightItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn srcLabelItemKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn srcDefaultText;
        private System.Windows.Forms.DataGridViewTextBoxColumn srcBaseUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn srcIocContainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn SecurityRightItemIDModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabelItemKeyModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultTextModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn BaseUrlModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn IOCContainerModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsActiveModified;
        private System.Windows.Forms.DataGridViewCheckBoxColumn srcIsActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
    }
}