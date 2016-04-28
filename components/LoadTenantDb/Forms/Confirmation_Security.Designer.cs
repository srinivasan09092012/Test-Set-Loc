//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class ConfirmationSecurityDialog
    {
        private System.Windows.Forms.GroupBox rightsGroupBox;
        private System.Windows.Forms.CheckedListBox rightAttributesListBox;
        private System.Windows.Forms.Button loadPushButton;
        private System.Windows.Forms.GroupBox rightAttributesGroupBox;
        private System.Windows.Forms.GroupBox functionAttributesGroupBox;
        private System.Windows.Forms.CheckedListBox functionAttributesListBox;
        private System.Windows.Forms.GroupBox functionsGroupBox;
        private System.Windows.Forms.GroupBox roleAttributesGroupBox;
        private System.Windows.Forms.CheckedListBox roleAttributesListBox;
        private System.Windows.Forms.GroupBox rolesGroupBox;
        private System.Windows.Forms.DataGridView rightsGridView;
        private System.Windows.Forms.DataGridView functionsGridView;
        private System.Windows.Forms.DataGridView rolesGridView;
        private System.Windows.Forms.Button cancelPushButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn rightAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn rightModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn rightName;
        private System.Windows.Forms.DataGridViewTextBoxColumn rightContentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn rightLinkToFunction;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionContentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleContentId;
        
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
            this.rightsGroupBox = new System.Windows.Forms.GroupBox();
            this.rightsGridView = new System.Windows.Forms.DataGridView();
            this.loadPushButton = new System.Windows.Forms.Button();
            this.rightAttributesGroupBox = new System.Windows.Forms.GroupBox();
            this.rightAttributesListBox = new System.Windows.Forms.CheckedListBox();
            this.functionAttributesGroupBox = new System.Windows.Forms.GroupBox();
            this.functionAttributesListBox = new System.Windows.Forms.CheckedListBox();
            this.functionsGroupBox = new System.Windows.Forms.GroupBox();
            this.functionsGridView = new System.Windows.Forms.DataGridView();
            this.roleAttributesGroupBox = new System.Windows.Forms.GroupBox();
            this.roleAttributesListBox = new System.Windows.Forms.CheckedListBox();
            this.rolesGroupBox = new System.Windows.Forms.GroupBox();
            this.rolesGridView = new System.Windows.Forms.DataGridView();
            this.cancelPushButton = new System.Windows.Forms.Button();
            this.roleAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightLinkToFunction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionContentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightsGridView)).BeginInit();
            this.rightAttributesGroupBox.SuspendLayout();
            this.functionAttributesGroupBox.SuspendLayout();
            this.functionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.functionsGridView)).BeginInit();
            this.roleAttributesGroupBox.SuspendLayout();
            this.rolesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rolesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // rightsGroupBox
            // 
            this.rightsGroupBox.Controls.Add(this.rightsGridView);
            this.rightsGroupBox.Location = new System.Drawing.Point(13, 497);
            this.rightsGroupBox.Name = "rightsGroupBox";
            this.rightsGroupBox.Size = new System.Drawing.Size(992, 238);
            this.rightsGroupBox.TabIndex = 6;
            this.rightsGroupBox.TabStop = false;
            this.rightsGroupBox.Text = "Rights";
            // 
            // rightsGridView
            // 
            this.rightsGridView.AllowUserToAddRows = false;
            this.rightsGridView.AllowUserToDeleteRows = false;
            this.rightsGridView.AllowUserToResizeRows = false;
            this.rightsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rightsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rightAction,
            this.rightModule,
            this.rightName,
            this.rightContentId,
            this.rightLinkToFunction});
            this.rightsGridView.Location = new System.Drawing.Point(20, 18);
            this.rightsGridView.MultiSelect = false;
            this.rightsGridView.Name = "rightsGridView";
            this.rightsGridView.ReadOnly = true;
            this.rightsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rightsGridView.Size = new System.Drawing.Size(964, 214);
            this.rightsGridView.TabIndex = 7;
            this.rightsGridView.TabStop = false;
            // 
            // loadPushButton
            // 
            this.loadPushButton.Location = new System.Drawing.Point(1182, 741);
            this.loadPushButton.Name = "loadPushButton";
            this.loadPushButton.Size = new System.Drawing.Size(75, 23);
            this.loadPushButton.TabIndex = 1;
            this.loadPushButton.Text = "&Load...";
            this.loadPushButton.UseVisualStyleBackColor = true;
            this.loadPushButton.Click += new System.EventHandler(this.LoadSecurityRoles);
            // 
            // rightAttributesGroupBox
            // 
            this.rightAttributesGroupBox.Controls.Add(this.rightAttributesListBox);
            this.rightAttributesGroupBox.Location = new System.Drawing.Point(1011, 497);
            this.rightAttributesGroupBox.Name = "rightAttributesGroupBox";
            this.rightAttributesGroupBox.Size = new System.Drawing.Size(246, 238);
            this.rightAttributesGroupBox.TabIndex = 2;
            this.rightAttributesGroupBox.TabStop = false;
            this.rightAttributesGroupBox.Text = "Right Attributes";
            // 
            // rightAttributesListBox
            // 
            this.rightAttributesListBox.Location = new System.Drawing.Point(9, 18);
            this.rightAttributesListBox.Name = "rightAttributesListBox";
            this.rightAttributesListBox.Size = new System.Drawing.Size(231, 214);
            this.rightAttributesListBox.TabIndex = 4;
            // 
            // functionAttributesGroupBox
            // 
            this.functionAttributesGroupBox.Controls.Add(this.functionAttributesListBox);
            this.functionAttributesGroupBox.Location = new System.Drawing.Point(1010, 253);
            this.functionAttributesGroupBox.Name = "functionAttributesGroupBox";
            this.functionAttributesGroupBox.Size = new System.Drawing.Size(246, 238);
            this.functionAttributesGroupBox.TabIndex = 4;
            this.functionAttributesGroupBox.TabStop = false;
            this.functionAttributesGroupBox.Text = "Function Attributes";
            // 
            // functionAttributesListBox
            // 
            this.functionAttributesListBox.Location = new System.Drawing.Point(9, 19);
            this.functionAttributesListBox.Name = "functionAttributesListBox";
            this.functionAttributesListBox.Size = new System.Drawing.Size(232, 214);
            this.functionAttributesListBox.TabIndex = 3;
            // 
            // functionsGroupBox
            // 
            this.functionsGroupBox.Controls.Add(this.functionsGridView);
            this.functionsGroupBox.Location = new System.Drawing.Point(12, 253);
            this.functionsGroupBox.Name = "functionsGroupBox";
            this.functionsGroupBox.Size = new System.Drawing.Size(992, 238);
            this.functionsGroupBox.TabIndex = 3;
            this.functionsGroupBox.TabStop = false;
            this.functionsGroupBox.Text = "Functions";
            // 
            // functionsGridView
            // 
            this.functionsGridView.AllowUserToAddRows = false;
            this.functionsGridView.AllowUserToDeleteRows = false;
            this.functionsGridView.AllowUserToResizeRows = false;
            this.functionsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.functionsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.functionAction,
            this.functionModule,
            this.functionName,
            this.functionContentId,
            this.functionLink});
            this.functionsGridView.Location = new System.Drawing.Point(21, 20);
            this.functionsGridView.MultiSelect = false;
            this.functionsGridView.Name = "functionsGridView";
            this.functionsGridView.ReadOnly = true;
            this.functionsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.functionsGridView.Size = new System.Drawing.Size(964, 212);
            this.functionsGridView.TabIndex = 4;
            this.functionsGridView.TabStop = false;
            // 
            // roleAttributesGroupBox
            // 
            this.roleAttributesGroupBox.Controls.Add(this.roleAttributesListBox);
            this.roleAttributesGroupBox.Location = new System.Drawing.Point(1011, 9);
            this.roleAttributesGroupBox.Name = "roleAttributesGroupBox";
            this.roleAttributesGroupBox.Size = new System.Drawing.Size(246, 238);
            this.roleAttributesGroupBox.TabIndex = 4;
            this.roleAttributesGroupBox.TabStop = false;
            this.roleAttributesGroupBox.Text = "Role Attributes";
            // 
            // roleAttributesListBox
            // 
            this.roleAttributesListBox.Location = new System.Drawing.Point(9, 20);
            this.roleAttributesListBox.Name = "roleAttributesListBox";
            this.roleAttributesListBox.Size = new System.Drawing.Size(231, 214);
            this.roleAttributesListBox.TabIndex = 2;
            // 
            // rolesGroupBox
            // 
            this.rolesGroupBox.Controls.Add(this.rolesGridView);
            this.rolesGroupBox.Location = new System.Drawing.Point(13, 9);
            this.rolesGroupBox.Name = "rolesGroupBox";
            this.rolesGroupBox.Size = new System.Drawing.Size(992, 238);
            this.rolesGroupBox.TabIndex = 10;
            this.rolesGroupBox.TabStop = false;
            this.rolesGroupBox.Text = "Roles";
            // 
            // rolesGridView
            // 
            this.rolesGridView.AllowUserToAddRows = false;
            this.rolesGridView.AllowUserToDeleteRows = false;
            this.rolesGridView.AllowUserToResizeRows = false;
            this.rolesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rolesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roleAction,
            this.roleModule,
            this.roleName,
            this.roleContentId});
            this.rolesGridView.Location = new System.Drawing.Point(20, 20);
            this.rolesGridView.MultiSelect = false;
            this.rolesGridView.Name = "rolesGridView";
            this.rolesGridView.ReadOnly = true;
            this.rolesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rolesGridView.Size = new System.Drawing.Size(964, 211);
            this.rolesGridView.StandardTab = true;
            this.rolesGridView.TabIndex = 1;
            this.rolesGridView.TabStop = false;
            // 
            // cancelPushButton
            // 
            this.cancelPushButton.Location = new System.Drawing.Point(1101, 741);
            this.cancelPushButton.Name = "cancelPushButton";
            this.cancelPushButton.Size = new System.Drawing.Size(75, 23);
            this.cancelPushButton.TabIndex = 0;
            this.cancelPushButton.Text = "Cancel";
            this.cancelPushButton.UseVisualStyleBackColor = true;
            this.cancelPushButton.Click += new System.EventHandler(this.CancelPushButton_Click);
            // 
            // roleAction
            // 
            this.roleAction.HeaderText = "Action";
            this.roleAction.Name = "roleAction";
            this.roleAction.ReadOnly = true;
            // 
            // roleModule
            // 
            this.roleModule.HeaderText = "Module";
            this.roleModule.Name = "roleModule";
            this.roleModule.ReadOnly = true;
            this.roleModule.Width = 130;
            // 
            // roleName
            // 
            this.roleName.HeaderText = "Name";
            this.roleName.Name = "roleName";
            this.roleName.ReadOnly = true;
            this.roleName.Width = 220;
            // 
            // roleContentId
            // 
            this.roleContentId.HeaderText = "Content ID";
            this.roleContentId.Name = "roleContentId";
            this.roleContentId.ReadOnly = true;
            this.roleContentId.Width = 420;
            // 
            // rightAction
            // 
            this.rightAction.HeaderText = "Action";
            this.rightAction.Name = "rightAction";
            this.rightAction.ReadOnly = true;
            // 
            // rightModule
            // 
            this.rightModule.HeaderText = "Module";
            this.rightModule.Name = "rightModule";
            this.rightModule.ReadOnly = true;
            this.rightModule.Width = 130;
            // 
            // rightName
            // 
            this.rightName.HeaderText = "Name";
            this.rightName.Name = "rightName";
            this.rightName.ReadOnly = true;
            this.rightName.Width = 220;
            // 
            // rightContentId
            // 
            this.rightContentId.HeaderText = "Content ID";
            this.rightContentId.Name = "rightContentId";
            this.rightContentId.ReadOnly = true;
            this.rightContentId.Width = 420;
            // 
            // rightLinkToFunction
            // 
            this.rightLinkToFunction.HeaderText = "Link to Function";
            this.rightLinkToFunction.Name = "rightLinkToFunction";
            this.rightLinkToFunction.ReadOnly = true;
            this.rightLinkToFunction.Width = 420;
            // 
            // functionAction
            // 
            this.functionAction.HeaderText = "Action";
            this.functionAction.Name = "functionAction";
            this.functionAction.ReadOnly = true;
            // 
            // functionModule
            // 
            this.functionModule.HeaderText = "Module";
            this.functionModule.Name = "functionModule";
            this.functionModule.ReadOnly = true;
            this.functionModule.Width = 130;
            // 
            // functionName
            // 
            this.functionName.HeaderText = "Name";
            this.functionName.Name = "functionName";
            this.functionName.ReadOnly = true;
            this.functionName.Width = 220;
            // 
            // functionContentId
            // 
            this.functionContentId.HeaderText = "Content ID";
            this.functionContentId.Name = "functionContentId";
            this.functionContentId.ReadOnly = true;
            this.functionContentId.Width = 420;
            // 
            // functionLink
            // 
            this.functionLink.HeaderText = "Link to Role";
            this.functionLink.Name = "functionLink";
            this.functionLink.ReadOnly = true;
            this.functionLink.Width = 420;
            // 
            // ConfirmationSecurityDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 769);
            this.Controls.Add(this.cancelPushButton);
            this.Controls.Add(this.roleAttributesGroupBox);
            this.Controls.Add(this.functionAttributesGroupBox);
            this.Controls.Add(this.rolesGroupBox);
            this.Controls.Add(this.functionsGroupBox);
            this.Controls.Add(this.rightAttributesGroupBox);
            this.Controls.Add(this.loadPushButton);
            this.Controls.Add(this.rightsGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ConfirmationSecurityDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Security Roles Configuration";
            this.Load += new System.EventHandler(this.ConfirmationSecurityLoad);
            this.rightsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightsGridView)).EndInit();
            this.rightAttributesGroupBox.ResumeLayout(false);
            this.functionAttributesGroupBox.ResumeLayout(false);
            this.functionsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.functionsGridView)).EndInit();
            this.roleAttributesGroupBox.ResumeLayout(false);
            this.rolesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rolesGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}