//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil.Views
{
     public partial class MenuPreviewPage
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
            this.PreviewTreeList = new System.Windows.Forms.TreeView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Submit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PreviewTreeList
            // 
            this.PreviewTreeList.Location = new System.Drawing.Point(6, 10);
            this.PreviewTreeList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PreviewTreeList.Name = "PreviewTreeList";
            this.PreviewTreeList.Size = new System.Drawing.Size(858, 582);
            this.PreviewTreeList.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(746, 596);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(807, 596);
            this.Submit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(56, 24);
            this.Submit.TabIndex = 10;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_btn);
            // 
            // MenuPreviewPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 622);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.PreviewTreeList);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MenuPreviewPage";
            this.Text = "MenuPreviewPage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView PreviewTreeList;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button Submit;
    }
}
