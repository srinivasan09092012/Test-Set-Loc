//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil.Views
{
    public partial class PreviewPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TreeView PreviewTreeList;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Button btnCancel;

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
            this.PreviewTreeList = new System.Windows.Forms.TreeView();
            this.Submit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PreviewTreeList
            // 
            this.PreviewTreeList.Location = new System.Drawing.Point(12, 12);
            this.PreviewTreeList.Name = "PreviewTreeList";
            this.PreviewTreeList.Size = new System.Drawing.Size(1142, 716);
            this.PreviewTreeList.TabIndex = 6;
            // 
            // Submit
            // 
            this.Submit.Enabled = false;
            this.Submit.Location = new System.Drawing.Point(1079, 734);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(75, 29);
            this.Submit.TabIndex = 7;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(998, 734);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PreviewPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 765);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.PreviewTreeList);
            this.Name = "PreviewPage";
            this.Text = "Preview";
            this.ResumeLayout(false);

        }

        #endregion        
    }
}