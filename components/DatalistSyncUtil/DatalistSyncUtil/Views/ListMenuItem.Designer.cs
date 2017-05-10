//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil.Views
{
    public partial class ListMenuItem
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
            this.menulabel = new System.Windows.Forms.Label();
            this.inactiveCB = new System.Windows.Forms.CheckBox();
            this.chkDateOverride = new System.Windows.Forms.CheckBox();
            this.selectAllChkBox = new System.Windows.Forms.CheckBox();
            this.menuItemView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clearmenu = new System.Windows.Forms.Button();
            this.savemenu = new System.Windows.Forms.Button();
            this.menuClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.menuItemView)).BeginInit();
            this.SuspendLayout();
            // 
            // menulabel
            // 
            this.menulabel.AutoSize = true;
            this.menulabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menulabel.Location = new System.Drawing.Point(32, 9);
            this.menulabel.Name = "menulabel";
            this.menulabel.Size = new System.Drawing.Size(106, 20);
            this.menulabel.TabIndex = 3;
            this.menulabel.Text = "MenuItems:";
            // 
            // inactiveCB
            // 
            this.inactiveCB.AutoSize = true;
            this.inactiveCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inactiveCB.Location = new System.Drawing.Point(263, 46);
            this.inactiveCB.Name = "inactiveCB";
            this.inactiveCB.Size = new System.Drawing.Size(129, 21);
            this.inactiveCB.TabIndex = 17;
            this.inactiveCB.Text = "Show Inactive";
            this.inactiveCB.UseVisualStyleBackColor = true;
            // 
            // chkDateOverride
            // 
            this.chkDateOverride.AutoSize = true;
            this.chkDateOverride.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDateOverride.Location = new System.Drawing.Point(131, 46);
            this.chkDateOverride.Name = "chkDateOverride";
            this.chkDateOverride.Size = new System.Drawing.Size(126, 21);
            this.chkDateOverride.TabIndex = 16;
            this.chkDateOverride.Text = "Override Age";
            this.chkDateOverride.UseVisualStyleBackColor = true;
            // 
            // selectAllChkBox
            // 
            this.selectAllChkBox.AutoSize = true;
            this.selectAllChkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectAllChkBox.Location = new System.Drawing.Point(27, 46);
            this.selectAllChkBox.Name = "selectAllChkBox";
            this.selectAllChkBox.Size = new System.Drawing.Size(98, 21);
            this.selectAllChkBox.TabIndex = 15;
            this.selectAllChkBox.Text = "Select All";
            this.selectAllChkBox.UseVisualStyleBackColor = true;
            this.selectAllChkBox.CheckedChanged += new System.EventHandler(this.SelectAllChkBox_CheckedChanged);
            // 
            // menuItemView
            // 
            this.menuItemView.AllowUserToAddRows = false;
            this.menuItemView.AllowUserToDeleteRows = false;
            this.menuItemView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.menuItemView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.menuItemView.Location = new System.Drawing.Point(-1, 73);
            this.menuItemView.Name = "menuItemView";
            this.menuItemView.RowTemplate.Height = 24;
            this.menuItemView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.menuItemView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.menuItemView.Size = new System.Drawing.Size(833, 349);
            this.menuItemView.TabIndex = 18;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Name";
            this.Column1.HeaderText = "Content ID";
            this.Column1.Name = "Column1";
            this.Column1.Width = 800;
            // 
            // clearmenu
            // 
            this.clearmenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearmenu.Location = new System.Drawing.Point(12, 428);
            this.clearmenu.Name = "clearmenu";
            this.clearmenu.Size = new System.Drawing.Size(75, 36);
            this.clearmenu.TabIndex = 19;
            this.clearmenu.Text = "Clear";
            this.clearmenu.UseVisualStyleBackColor = true;
            this.clearmenu.Click += new System.EventHandler(this.Clearmenu_Click);
            // 
            // savemenu
            // 
            this.savemenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savemenu.Location = new System.Drawing.Point(654, 428);
            this.savemenu.Name = "savemenu";
            this.savemenu.Size = new System.Drawing.Size(75, 36);
            this.savemenu.TabIndex = 20;
            this.savemenu.Text = "Save";
            this.savemenu.UseVisualStyleBackColor = true;
            this.savemenu.Click += new System.EventHandler(this.Savemenu_Click);
            // 
            // menuClose
            // 
            this.menuClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuClose.Location = new System.Drawing.Point(735, 428);
            this.menuClose.Name = "menuClose";
            this.menuClose.Size = new System.Drawing.Size(75, 36);
            this.menuClose.TabIndex = 21;
            this.menuClose.Text = "Cancel";
            this.menuClose.UseVisualStyleBackColor = true;
            this.menuClose.Click += new System.EventHandler(this.MenuClose_Click);
            // 
            // ListMenuItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 476);
            this.Controls.Add(this.menuClose);
            this.Controls.Add(this.savemenu);
            this.Controls.Add(this.clearmenu);
            this.Controls.Add(this.menuItemView);
            this.Controls.Add(this.inactiveCB);
            this.Controls.Add(this.chkDateOverride);
            this.Controls.Add(this.selectAllChkBox);
            this.Controls.Add(this.menulabel);
            this.Name = "ListMenuItem";
            this.Text = "MenuItem";
            ((System.ComponentModel.ISupportInitialize)(this.menuItemView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label menulabel;
        private System.Windows.Forms.CheckBox inactiveCB;
        private System.Windows.Forms.CheckBox chkDateOverride;
        private System.Windows.Forms.CheckBox selectAllChkBox;
        private System.Windows.Forms.DataGridView menuItemView;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentid;
        private System.Windows.Forms.Button clearmenu;
        private System.Windows.Forms.Button savemenu;
        private System.Windows.Forms.Button menuClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}