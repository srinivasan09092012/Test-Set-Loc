namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    partial class Confirmation_LocalizationModel
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
            this.definitionsGroupBox = new System.Windows.Forms.GroupBox();
            this.definitionsGridView = new System.Windows.Forms.DataGridView();
            this.definitionAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.definitionModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.definitionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.definitiontype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.definitionScope = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.definitionDisplaySize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loadPushButton = new System.Windows.Forms.Button();
            this.propertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.propertiesGridView = new System.Windows.Forms.DataGridView();
            this.propertyAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParentLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyDataRestrictionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyDisplayType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyIgnoreDirtyData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyIsRequired = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyMaxLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyLabelContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyDefaultText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyHintType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyAccessKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyCompareTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyCompareToContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyViewRight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyEditRight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancelPushButton = new System.Windows.Forms.Button();
            this.definitionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.definitionsGridView)).BeginInit();
            this.propertiesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // definitionsGroupBox
            // 
            this.definitionsGroupBox.Controls.Add(this.definitionsGridView);
            this.definitionsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.definitionsGroupBox.Name = "definitionsGroupBox";
            this.definitionsGroupBox.Size = new System.Drawing.Size(1221, 245);
            this.definitionsGroupBox.TabIndex = 11;
            this.definitionsGroupBox.TabStop = false;
            this.definitionsGroupBox.Text = "Model Definitions";
            // 
            // definitionsGridView
            // 
            this.definitionsGridView.AllowUserToAddRows = false;
            this.definitionsGridView.AllowUserToDeleteRows = false;
            this.definitionsGridView.AllowUserToResizeRows = false;
            this.definitionsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.definitionsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.definitionAction,
            this.definitionModule,
            this.definitionId,
            this.definitiontype,
            this.definitionScope,
            this.definitionDisplaySize});
            this.definitionsGridView.Location = new System.Drawing.Point(6, 19);
            this.definitionsGridView.MultiSelect = false;
            this.definitionsGridView.Name = "definitionsGridView";
            this.definitionsGridView.ReadOnly = true;
            this.definitionsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.definitionsGridView.Size = new System.Drawing.Size(1215, 220);
            this.definitionsGridView.StandardTab = true;
            this.definitionsGridView.TabIndex = 1;
            this.definitionsGridView.TabStop = false;
            this.definitionsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.definitionssGridView_CellContentClick);
            // 
            // definitionAction
            // 
            this.definitionAction.HeaderText = "Action";
            this.definitionAction.Name = "definitionAction";
            this.definitionAction.ReadOnly = true;
            // 
            // definitionModule
            // 
            this.definitionModule.HeaderText = "Module";
            this.definitionModule.Name = "definitionModule";
            this.definitionModule.ReadOnly = true;
            this.definitionModule.Width = 130;
            // 
            // definitionId
            // 
            this.definitionId.HeaderText = "ID";
            this.definitionId.Name = "definitionId";
            this.definitionId.ReadOnly = true;
            this.definitionId.Width = 200;
            // 
            // definitiontype
            // 
            this.definitiontype.HeaderText = "Type";
            this.definitiontype.Name = "definitiontype";
            this.definitiontype.ReadOnly = true;
            this.definitiontype.Width = 600;
            // 
            // definitionScope
            // 
            this.definitionScope.HeaderText = "Scope";
            this.definitionScope.Name = "definitionScope";
            this.definitionScope.ReadOnly = true;
            this.definitionScope.Width = 50;
            // 
            // definitionDisplaySize
            // 
            this.definitionDisplaySize.HeaderText = "Display Size";
            this.definitionDisplaySize.Name = "definitionDisplaySize";
            this.definitionDisplaySize.ReadOnly = true;
            this.definitionDisplaySize.Width = 50;
            // 
            // loadPushButton
            // 
            this.loadPushButton.Location = new System.Drawing.Point(1034, 488);
            this.loadPushButton.Name = "loadPushButton";
            this.loadPushButton.Size = new System.Drawing.Size(75, 23);
            this.loadPushButton.TabIndex = 1;
            this.loadPushButton.Text = "&Load...";
            this.loadPushButton.UseVisualStyleBackColor = true;
            this.loadPushButton.Click += new System.EventHandler(this.ProcessLoad);
            // 
            // propertiesGroupBox
            // 
            this.propertiesGroupBox.Controls.Add(this.propertiesGridView);
            this.propertiesGroupBox.Location = new System.Drawing.Point(12, 263);
            this.propertiesGroupBox.Name = "propertiesGroupBox";
            this.propertiesGroupBox.Size = new System.Drawing.Size(1221, 216);
            this.propertiesGroupBox.TabIndex = 12;
            this.propertiesGroupBox.TabStop = false;
            this.propertiesGroupBox.Text = "Model Properties";
            // 
            // propertiesGridView
            // 
            this.propertiesGridView.AllowUserToAddRows = false;
            this.propertiesGridView.AllowUserToDeleteRows = false;
            this.propertiesGridView.AllowUserToResizeRows = false;
            this.propertiesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.propertiesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.propertyAction,
            this.propertyModule,
            this.propertyId,
            this.ParentLink,
            this.propertyName,
            this.propertyDataType,
            this.propertyDataRestrictionType,
            this.propertyDisplayType,
            this.propertyIgnoreDirtyData,
            this.propertyIsRequired,
            this.propertyMaxLength,
            this.propertyLabelContentId,
            this.propertyDefaultText,
            this.propertyHintType,
            this.propertyAccessKey,
            this.propertyHeight,
            this.propertyCompareTo,
            this.propertyCompareToContent,
            this.propertyViewRight,
            this.propertyEditRight});
            this.propertiesGridView.Location = new System.Drawing.Point(6, 19);
            this.propertiesGridView.MultiSelect = false;
            this.propertiesGridView.Name = "propertiesGridView";
            this.propertiesGridView.ReadOnly = true;
            this.propertiesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.propertiesGridView.Size = new System.Drawing.Size(1215, 189);
            this.propertiesGridView.TabIndex = 4;
            this.propertiesGridView.TabStop = false;
            this.propertiesGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.propertiesGridView_CellContentClick);
            // 
            // propertyAction
            // 
            this.propertyAction.HeaderText = "Action";
            this.propertyAction.Name = "propertyAction";
            this.propertyAction.ReadOnly = true;
            // 
            // propertyModule
            // 
            this.propertyModule.HeaderText = "Module";
            this.propertyModule.Name = "propertyModule";
            this.propertyModule.ReadOnly = true;
            this.propertyModule.Width = 130;
            // 
            // propertyId
            // 
            this.propertyId.HeaderText = "ID";
            this.propertyId.Name = "propertyId";
            this.propertyId.ReadOnly = true;
            this.propertyId.Width = 200;
            // 
            // ParentLink
            // 
            this.ParentLink.HeaderText = "ParentLink";
            this.ParentLink.Name = "ParentLink";
            this.ParentLink.ReadOnly = true;
            this.ParentLink.Width = 300;
            // 
            // propertyName
            // 
            this.propertyName.HeaderText = "Name";
            this.propertyName.Name = "propertyName";
            this.propertyName.ReadOnly = true;
            this.propertyName.Width = 220;
            // 
            // propertyDataType
            // 
            this.propertyDataType.HeaderText = "Data Type";
            this.propertyDataType.Name = "propertyDataType";
            this.propertyDataType.ReadOnly = true;
            // 
            // propertyDataRestrictionType
            // 
            this.propertyDataRestrictionType.HeaderText = "Data Resctriction Type";
            this.propertyDataRestrictionType.Name = "propertyDataRestrictionType";
            this.propertyDataRestrictionType.ReadOnly = true;
            // 
            // propertyDisplayType
            // 
            this.propertyDisplayType.HeaderText = "Display Type";
            this.propertyDisplayType.Name = "propertyDisplayType";
            this.propertyDisplayType.ReadOnly = true;
            // 
            // propertyIgnoreDirtyData
            // 
            this.propertyIgnoreDirtyData.HeaderText = "Ignore Dirty Data";
            this.propertyIgnoreDirtyData.Name = "propertyIgnoreDirtyData";
            this.propertyIgnoreDirtyData.ReadOnly = true;
            // 
            // propertyIsRequired
            // 
            this.propertyIsRequired.HeaderText = "Is Required";
            this.propertyIsRequired.Name = "propertyIsRequired";
            this.propertyIsRequired.ReadOnly = true;
            // 
            // propertyMaxLength
            // 
            this.propertyMaxLength.HeaderText = "Max Length";
            this.propertyMaxLength.Name = "propertyMaxLength";
            this.propertyMaxLength.ReadOnly = true;
            // 
            // propertyLabelContentId
            // 
            this.propertyLabelContentId.HeaderText = "Label Content Id";
            this.propertyLabelContentId.Name = "propertyLabelContentId";
            this.propertyLabelContentId.ReadOnly = true;
            // 
            // propertyDefaultText
            // 
            this.propertyDefaultText.HeaderText = "Default Text";
            this.propertyDefaultText.Name = "propertyDefaultText";
            this.propertyDefaultText.ReadOnly = true;
            // 
            // propertyHintType
            // 
            this.propertyHintType.HeaderText = "Hint Type";
            this.propertyHintType.Name = "propertyHintType";
            this.propertyHintType.ReadOnly = true;
            // 
            // propertyAccessKey
            // 
            this.propertyAccessKey.HeaderText = "Access Key";
            this.propertyAccessKey.Name = "propertyAccessKey";
            this.propertyAccessKey.ReadOnly = true;
            // 
            // propertyHeight
            // 
            this.propertyHeight.HeaderText = "Height";
            this.propertyHeight.Name = "propertyHeight";
            this.propertyHeight.ReadOnly = true;
            // 
            // propertyCompareTo
            // 
            this.propertyCompareTo.HeaderText = "Compare To";
            this.propertyCompareTo.Name = "propertyCompareTo";
            this.propertyCompareTo.ReadOnly = true;
            // 
            // propertyCompareToContent
            // 
            this.propertyCompareToContent.HeaderText = "Compare To Content";
            this.propertyCompareToContent.Name = "propertyCompareToContent";
            this.propertyCompareToContent.ReadOnly = true;
            // 
            // propertyViewRight
            // 
            this.propertyViewRight.HeaderText = "View Right";
            this.propertyViewRight.Name = "propertyViewRight";
            this.propertyViewRight.ReadOnly = true;
            // 
            // propertyEditRight
            // 
            this.propertyEditRight.HeaderText = "Edit Right";
            this.propertyEditRight.Name = "propertyEditRight";
            this.propertyEditRight.ReadOnly = true;
            // 
            // cancelPushButton
            // 
            this.cancelPushButton.Location = new System.Drawing.Point(1129, 488);
            this.cancelPushButton.Name = "cancelPushButton";
            this.cancelPushButton.Size = new System.Drawing.Size(75, 23);
            this.cancelPushButton.TabIndex = 14;
            this.cancelPushButton.Text = "Cancel";
            this.cancelPushButton.UseVisualStyleBackColor = true;
            this.cancelPushButton.Click += new System.EventHandler(this.cancelPushButton_Click);
            // 
            // Confirmation_LocalizationModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 512);
            this.Controls.Add(this.cancelPushButton);
            this.Controls.Add(this.propertiesGroupBox);
            this.Controls.Add(this.definitionsGroupBox);
            this.Controls.Add(this.loadPushButton);
            this.Name = "Confirmation_LocalizationModel";
            this.Text = "Model Configuration";
            this.Load += new System.EventHandler(this.Confirmation_LocalizationModel_Load);
            this.definitionsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.definitionsGridView)).EndInit();
            this.propertiesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertiesGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox definitionsGroupBox;
        private System.Windows.Forms.DataGridView definitionsGridView;
        private System.Windows.Forms.Button loadPushButton;
        private System.Windows.Forms.GroupBox propertiesGroupBox;
        private System.Windows.Forms.DataGridView propertiesGridView;
        private System.Windows.Forms.Button cancelPushButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn definitionAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn definitionModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn definitionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn definitiontype;
        private System.Windows.Forms.DataGridViewTextBoxColumn definitionScope;
        private System.Windows.Forms.DataGridViewTextBoxColumn definitionDisplaySize;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParentLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyDataRestrictionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyDisplayType;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyIgnoreDirtyData;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyIsRequired;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyMaxLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyLabelContentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyDefaultText;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyHintType;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyAccessKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyCompareTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyCompareToContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyViewRight;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyEditRight;
    }
}