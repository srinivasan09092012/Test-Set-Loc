//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class ConfirmationLocalizationDatalistDialog
    {
        private System.Windows.Forms.Button cancelPushButton;
        private System.Windows.Forms.Button loadPushButtonAll;
        private System.Windows.Forms.GroupBox datalistGroupBox;
        private System.Windows.Forms.DataGridView datalistsGridView;

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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.cancelPushButton = new System.Windows.Forms.Button();
            this.loadPushButtonAll = new System.Windows.Forms.Button();
            this.datalistGroupBox = new System.Windows.Forms.GroupBox();
            this.datalistsGridView = new System.Windows.Forms.DataGridView();
            this.datalistAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.module = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datalistName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datalistContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datalistItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datalistItemContentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datalistItemOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datalistGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datalistsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelPushButton
            // 
            this.cancelPushButton.Location = new System.Drawing.Point(1026, 502);
            this.cancelPushButton.Name = "cancelPushButton";
            this.cancelPushButton.Size = new System.Drawing.Size(75, 23);
            this.cancelPushButton.TabIndex = 1;
            this.cancelPushButton.Text = "Cancel";
            this.cancelPushButton.UseVisualStyleBackColor = true;
            this.cancelPushButton.Click += new System.EventHandler(this.CancelPushButton_Click);
            // 
            // loadPushButtonAll
            // 
            this.loadPushButtonAll.Location = new System.Drawing.Point(1107, 502);
            this.loadPushButtonAll.Name = "loadPushButtonAll";
            this.loadPushButtonAll.Size = new System.Drawing.Size(75, 23);
            this.loadPushButtonAll.TabIndex = 2;
            this.loadPushButtonAll.Text = "&Load";
            this.loadPushButtonAll.UseVisualStyleBackColor = true;
            this.loadPushButtonAll.Click += new System.EventHandler(this.LoadAllLocalizationDatalists);
            // 
            // datalistGroupBox
            // 
            this.datalistGroupBox.Controls.Add(this.datalistsGridView);
            this.datalistGroupBox.Location = new System.Drawing.Point(12, 8);
            this.datalistGroupBox.Name = "datalistGroupBox";
            this.datalistGroupBox.Size = new System.Drawing.Size(1170, 488);
            this.datalistGroupBox.TabIndex = 6;
            this.datalistGroupBox.TabStop = false;
            this.datalistGroupBox.Text = "Datalists";
            // 
            // datalistsGridView
            // 
            this.datalistsGridView.AllowUserToAddRows = false;
            this.datalistsGridView.AllowUserToDeleteRows = false;
            this.datalistsGridView.AllowUserToResizeRows = false;
            this.datalistsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datalistsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.datalistAction,
            this.itemAction,
            this.localeName,
            this.module,
            this.datalistName,
            this.datalistContentId,
            this.datalistItem,
            this.datalistItemContentID,
            this.datalistItemOrder});
            this.datalistsGridView.Location = new System.Drawing.Point(19, 25);
            this.datalistsGridView.MultiSelect = false;
            this.datalistsGridView.Name = "datalistsGridView";
            this.datalistsGridView.ReadOnly = true;
            this.datalistsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datalistsGridView.Size = new System.Drawing.Size(1133, 446);
            this.datalistsGridView.StandardTab = true;
            this.datalistsGridView.TabIndex = 0;
            this.datalistsGridView.TabStop = false;
            // 
            // datalistAction
            // 
            this.datalistAction.HeaderText = "DataList Action";
            this.datalistAction.Name = "datalistAction";
            this.datalistAction.ReadOnly = true;
            // 
            // itemAction
            // 
            this.itemAction.HeaderText = "DataList Item Action";
            this.itemAction.Name = "itemAction";
            this.itemAction.ReadOnly = true;
            // 
            // localeName
            // 
            this.localeName.HeaderText = "Locale";
            this.localeName.Name = "localeName";
            this.localeName.ReadOnly = true;
            this.localeName.Width = 90;
            // 
            // module
            // 
            this.module.HeaderText = "Module";
            this.module.Name = "module";
            this.module.ReadOnly = true;
            this.module.Width = 125;
            // 
            // datalistName
            // 
            this.datalistName.HeaderText = "DataList Name";
            this.datalistName.Name = "datalistName";
            this.datalistName.ReadOnly = true;
            this.datalistName.Width = 220;
            // 
            // datalistContentId
            // 
            this.datalistContentId.HeaderText = "DataList Content ID";
            this.datalistContentId.Name = "datalistContentId";
            this.datalistContentId.ReadOnly = true;
            this.datalistContentId.Width = 330;
            // 
            // datalistItem
            // 
            this.datalistItem.HeaderText = "DataList Item";
            this.datalistItem.Name = "datalistItem";
            this.datalistItem.ReadOnly = true;
            this.datalistItem.Width = 220;
            // 
            // datalistItemContentID
            // 
            this.datalistItemContentID.HeaderText = "DataList Item Content ID";
            this.datalistItemContentID.Name = "datalistItemContentID";
            this.datalistItemContentID.ReadOnly = true;
            this.datalistItemContentID.Width = 330;
            // 
            // datalistItemOrder
            // 
            this.datalistItemOrder.HeaderText = "DataList Item Order";
            this.datalistItemOrder.Name = "datalistItemOrder";
            this.datalistItemOrder.ReadOnly = true;
            this.datalistItemOrder.Width = 140;
            // 
            // ConfirmationLocalizationDatalistDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 535);
            this.Controls.Add(this.datalistGroupBox);
            this.Controls.Add(this.cancelPushButton);
            this.Controls.Add(this.loadPushButtonAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ConfirmationLocalizationDatalistDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Localization Datalists Configuration";
            this.Load += new System.EventHandler(this.ConfirmationLocalizationDatalistsLoad);
            this.datalistGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datalistsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn datalistAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn localeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn module;
        private System.Windows.Forms.DataGridViewTextBoxColumn datalistName;
        private System.Windows.Forms.DataGridViewTextBoxColumn datalistContentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn datalistItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn datalistItemContentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn datalistItemOrder;


    }
}