//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil
{
    public partial class ListItems
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
            this.ListItemView = new System.Windows.Forms.DataGridView();
            this.SelectItem = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemViewClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblContentID = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.SelectAllChkBox = new System.Windows.Forms.CheckBox();
            this.ChkDateOverride = new System.Windows.Forms.CheckBox();
            this.InactiveCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ListItemView)).BeginInit();
            this.SuspendLayout();
            // 
            // ListItemView
            // 
            this.ListItemView.AllowUserToAddRows = false;
            this.ListItemView.AllowUserToDeleteRows = false;
            this.ListItemView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListItemView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectItem,
            this.ItemCode,
            this.LastModified,
            this.Active});
            this.ListItemView.Location = new System.Drawing.Point(12, 58);
            this.ListItemView.Name = "ListItemView";
            this.ListItemView.RowTemplate.Height = 24;
            this.ListItemView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ListItemView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListItemView.Size = new System.Drawing.Size(833, 362);
            this.ListItemView.TabIndex = 0;
            // 
            // SelectItem
            // 
            this.SelectItem.DataPropertyName = "Selected";
            this.SelectItem.FalseValue = "false";
            this.SelectItem.HeaderText = string.Empty;
            this.SelectItem.IndeterminateValue = "false";
            this.SelectItem.Name = "SelectItem";
            this.SelectItem.TrueValue = "true";
            this.SelectItem.Width = 30;
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "Code";
            this.ItemCode.HeaderText = "Code";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            this.ItemCode.Width = 450;
            // 
            // LastModified
            // 
            this.LastModified.DataPropertyName = "ModifiedDate";
            this.LastModified.HeaderText = "Modified Date";
            this.LastModified.Name = "LastModified";
            this.LastModified.ReadOnly = true;
            this.LastModified.Width = 200;
            // 
            // Active
            // 
            this.Active.DataPropertyName = "IsActive";
            this.Active.HeaderText = "Active";
            this.Active.Name = "Active";
            this.Active.ReadOnly = true;
            // 
            // ItemViewClose
            // 
            this.ItemViewClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemViewClose.Location = new System.Drawing.Point(770, 426);
            this.ItemViewClose.Name = "ItemViewClose";
            this.ItemViewClose.Size = new System.Drawing.Size(75, 36);
            this.ItemViewClose.TabIndex = 1;
            this.ItemViewClose.Text = "Cancel";
            this.ItemViewClose.UseVisualStyleBackColor = true;
            this.ItemViewClose.Click += new System.EventHandler(this.ItemViewClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Datalist:";
            // 
            // lblContentID
            // 
            this.lblContentID.AutoSize = true;
            this.lblContentID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContentID.Location = new System.Drawing.Point(94, 8);
            this.lblContentID.Name = "lblContentID";
            this.lblContentID.Size = new System.Drawing.Size(0, 20);
            this.lblContentID.TabIndex = 3;
            // 
            // Save
            // 
            this.Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save.Location = new System.Drawing.Point(675, 426);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 36);
            this.Save.TabIndex = 4;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Clear
            // 
            this.Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear.Location = new System.Drawing.Point(12, 426);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 36);
            this.Clear.TabIndex = 5;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // SelectAllChkBox
            // 
            this.SelectAllChkBox.AutoSize = true;
            this.SelectAllChkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAllChkBox.Location = new System.Drawing.Point(12, 36);
            this.SelectAllChkBox.Name = "SelectAllChkBox";
            this.SelectAllChkBox.Size = new System.Drawing.Size(98, 21);
            this.SelectAllChkBox.TabIndex = 7;
            this.SelectAllChkBox.Text = "Select All";
            this.SelectAllChkBox.UseVisualStyleBackColor = true;
            this.SelectAllChkBox.CheckedChanged += new System.EventHandler(this.SelectAllChkBox_CheckedChanged);
            // 
            // ChkDateOverride
            // 
            this.ChkDateOverride.AutoSize = true;
            this.ChkDateOverride.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDateOverride.Location = new System.Drawing.Point(124, 36);
            this.ChkDateOverride.Name = "ChkDateOverride";
            this.ChkDateOverride.Size = new System.Drawing.Size(126, 21);
            this.ChkDateOverride.TabIndex = 8;
            this.ChkDateOverride.Text = "Override Age";
            this.ChkDateOverride.UseVisualStyleBackColor = true;
            this.ChkDateOverride.CheckedChanged += new System.EventHandler(this.ChkDateOverride_CheckedChanged);
            // 
            // InactiveCB
            // 
            this.InactiveCB.AutoSize = true;
            this.InactiveCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InactiveCB.Location = new System.Drawing.Point(262, 36);
            this.InactiveCB.Name = "InactiveCB";
            this.InactiveCB.Size = new System.Drawing.Size(129, 21);
            this.InactiveCB.TabIndex = 14;
            this.InactiveCB.Text = "Show Inactive";
            this.InactiveCB.UseVisualStyleBackColor = true;
            this.InactiveCB.CheckedChanged += new System.EventHandler(this.InactiveCB_CheckedChanged);
            // 
            // ListItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 474);
            this.Controls.Add(this.InactiveCB);
            this.Controls.Add(this.ChkDateOverride);
            this.Controls.Add(this.SelectAllChkBox);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.lblContentID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ItemViewClose);
            this.Controls.Add(this.ListItemView);
            this.MaximizeBox = false;
            this.Name = "ListItems";
            this.Text = "Item View";
            ((System.ComponentModel.ISupportInitialize)(this.ListItemView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ListItemView;
        private System.Windows.Forms.Button ItemViewClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblContentID;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastModified;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Active;
        private System.Windows.Forms.CheckBox SelectAllChkBox;
        private System.Windows.Forms.CheckBox ChkDateOverride;
        private System.Windows.Forms.CheckBox InactiveCB;
    }
}