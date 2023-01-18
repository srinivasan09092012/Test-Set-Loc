namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    partial class Confirmation_MenuItems
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
            this.menuItemsGridView = new System.Windows.Forms.DataGridView();
            this.menuItemAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuItemModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuIdFk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isVisible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cssClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.securityIndexId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageHelpContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mitaHelpContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moduleSectionContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iocContainer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuItemssGroupBox = new System.Windows.Forms.GroupBox();
            this.cancelPushButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.menuItemsGridView)).BeginInit();
            this.menuItemssGroupBox.SuspendLayout();
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
            // menuItemsGridView
            // 
            this.menuItemsGridView.AllowUserToAddRows = false;
            this.menuItemsGridView.AllowUserToDeleteRows = false;
            this.menuItemsGridView.AllowUserToResizeRows = false;
            this.menuItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.menuItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.menuItemAction,
            this.menuItemModule,
            this.menuIdFk,
            this.menuItemId,
            this.parentId,
            this.menuItemName,
            this.orderIndex,
            this.isVisible,
            this.baseUrl,
            this.labelContentId,
            this.defaultText,
            this.cssClass,
            this.securityIndexId,
            this.pageHelpContentId,
            this.mitaHelpContentId,
            this.moduleSectionContentId,
            this.iocContainer});
            this.menuItemsGridView.Location = new System.Drawing.Point(21, 19);
            this.menuItemsGridView.MultiSelect = false;
            this.menuItemsGridView.Name = "menuItemsGridView";
            this.menuItemsGridView.ReadOnly = true;
            this.menuItemsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.menuItemsGridView.Size = new System.Drawing.Size(1200, 457);
            this.menuItemsGridView.TabIndex = 4;
            this.menuItemsGridView.TabStop = false;
            // 
            // menuItemAction
            // 
            this.menuItemAction.HeaderText = "Action";
            this.menuItemAction.Name = "menuItemAction";
            this.menuItemAction.ReadOnly = true;
            // 
            // menuItemModule
            // 
            this.menuItemModule.HeaderText = "Module";
            this.menuItemModule.Name = "menuItemModule";
            this.menuItemModule.ReadOnly = true;
            this.menuItemModule.Width = 130;
            // 
            // menuIdFk
            // 
            this.menuIdFk.HeaderText = "Menu Id";
            this.menuIdFk.Name = "menuIdFk";
            this.menuIdFk.ReadOnly = true;
            // 
            // menuItemId
            // 
            this.menuItemId.HeaderText = "Item Id";
            this.menuItemId.Name = "menuItemId";
            this.menuItemId.ReadOnly = true;
            this.menuItemId.Width = 225;
            // 
            // parentId
            // 
            this.parentId.HeaderText = "Parent ID";
            this.parentId.Name = "parentId";
            this.parentId.ReadOnly = true;
            this.parentId.Width = 225;
            // 
            // menuItemName
            // 
            this.menuItemName.HeaderText = "Name";
            this.menuItemName.Name = "menuItemName";
            this.menuItemName.ReadOnly = true;
            this.menuItemName.Width = 200;
            // 
            // orderIndex
            // 
            this.orderIndex.HeaderText = "Order Index";
            this.orderIndex.Name = "orderIndex";
            this.orderIndex.ReadOnly = true;
            this.orderIndex.Width = 58;
            // 
            // isVisible
            // 
            this.isVisible.HeaderText = "Visible";
            this.isVisible.Name = "isVisible";
            this.isVisible.ReadOnly = true;
            this.isVisible.Width = 50;
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
            // cssClass
            // 
            this.cssClass.HeaderText = "CSS Class";
            this.cssClass.Name = "cssClass";
            this.cssClass.ReadOnly = true;
            // 
            // securityIndexId
            // 
            this.securityIndexId.HeaderText = "Security Index ID";
            this.securityIndexId.Name = "securityIndexId";
            this.securityIndexId.ReadOnly = true;
            // 
            // pageHelpContentId
            // 
            this.pageHelpContentId.HeaderText = "Page Help Content ID";
            this.pageHelpContentId.Name = "pageHelpContentId";
            this.pageHelpContentId.ReadOnly = true;
            // 
            // mitaHelpContentId
            // 
            this.mitaHelpContentId.HeaderText = "Mita Help Content Id";
            this.mitaHelpContentId.Name = "mitaHelpContentId";
            this.mitaHelpContentId.ReadOnly = true;
            // 
            // moduleSectionContentId
            // 
            this.moduleSectionContentId.HeaderText = "Module Section Content Id";
            this.moduleSectionContentId.Name = "moduleSectionContentId";
            this.moduleSectionContentId.ReadOnly = true;
            // 
            // iocContainer
            // 
            this.iocContainer.HeaderText = "IOC Container";
            this.iocContainer.Name = "iocContainer";
            this.iocContainer.ReadOnly = true;
            // 
            // menuItemssGroupBox
            // 
            this.menuItemssGroupBox.Controls.Add(this.menuItemsGridView);
            this.menuItemssGroupBox.Location = new System.Drawing.Point(12, 12);
            this.menuItemssGroupBox.Name = "menuItemssGroupBox";
            this.menuItemssGroupBox.Size = new System.Drawing.Size(1238, 482);
            this.menuItemssGroupBox.TabIndex = 4;
            this.menuItemssGroupBox.TabStop = false;
            this.menuItemssGroupBox.Text = "Menu Items";
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
            // Confirmation_MenuItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 529);
            this.Controls.Add(this.cancelPushButton);
            this.Controls.Add(this.loadPushButton);
            this.Controls.Add(this.menuItemssGroupBox);
            this.Name = "Confirmation_MenuItems";
            this.Text = "Menu Configuration";
            this.Load += new System.EventHandler(this.Confirmation_MenuItems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.menuItemsGridView)).EndInit();
            this.menuItemssGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button loadPushButton;
        private System.Windows.Forms.DataGridView menuItemsGridView;
        private System.Windows.Forms.GroupBox menuItemssGroupBox;
        private System.Windows.Forms.Button cancelPushButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn menuItemAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn menuItemModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn menuIdFk;
        private System.Windows.Forms.DataGridViewTextBoxColumn menuItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn parentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn menuItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn isVisible;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn labelContentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultText;
        private System.Windows.Forms.DataGridViewTextBoxColumn cssClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn securityIndexId;
        private System.Windows.Forms.DataGridViewTextBoxColumn pageHelpContentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn mitaHelpContentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn moduleSectionContentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn iocContainer;
    }
}