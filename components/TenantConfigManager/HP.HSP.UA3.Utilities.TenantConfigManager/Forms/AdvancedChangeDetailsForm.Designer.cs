namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    partial class AdvancedChangeDetailsForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.originalObjectPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.updatedObjectPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.originalObjectPropertyGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.updatedObjectPropertyGrid, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.508197F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.49181F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(706, 341);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Original";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(356, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Updated";
            // 
            // originalObjectPropertyGrid
            // 
            this.originalObjectPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.originalObjectPropertyGrid.Location = new System.Drawing.Point(3, 18);
            this.originalObjectPropertyGrid.Name = "originalObjectPropertyGrid";
            this.originalObjectPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.originalObjectPropertyGrid.Size = new System.Drawing.Size(347, 320);
            this.originalObjectPropertyGrid.TabIndex = 2;
            // 
            // updatedObjectPropertyGrid
            // 
            this.updatedObjectPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updatedObjectPropertyGrid.Location = new System.Drawing.Point(356, 18);
            this.updatedObjectPropertyGrid.Name = "updatedObjectPropertyGrid";
            this.updatedObjectPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.updatedObjectPropertyGrid.Size = new System.Drawing.Size(347, 320);
            this.updatedObjectPropertyGrid.TabIndex = 3;
            // 
            // AdvancedChangeDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 360);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(739, 405);
            this.Name = "AdvancedChangeDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Updated Item Details";
            this.Load += new System.EventHandler(this.AdvancedChangeDetailsForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PropertyGrid originalObjectPropertyGrid;
        private System.Windows.Forms.PropertyGrid updatedObjectPropertyGrid;
    }
}