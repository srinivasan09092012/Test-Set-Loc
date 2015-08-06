namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class BasicChangeDetailsForm
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
            this.objectPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // objectPropertyGrid
            // 
            this.objectPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.objectPropertyGrid.Name = "objectPropertyGrid";
            this.objectPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.objectPropertyGrid.Size = new System.Drawing.Size(509, 359);
            this.objectPropertyGrid.TabIndex = 0;
            // 
            // BasicChangeDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 359);
            this.Controls.Add(this.objectPropertyGrid);
            this.MinimumSize = new System.Drawing.Size(527, 404);
            this.Name = "BasicChangeDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BasicChangeDetailsForm";
            this.Load += new System.EventHandler(this.BasicChangeDetailsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid objectPropertyGrid;
    }
}