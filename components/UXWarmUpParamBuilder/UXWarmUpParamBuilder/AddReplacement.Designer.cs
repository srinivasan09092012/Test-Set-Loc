namespace UXWarmUpParamBuilder
{
    partial class AddReplacement
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRepKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRepValue = new System.Windows.Forms.TextBox();
            this.buttonAddRep = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Replacement Key";
            // 
            // textBoxRepKey
            // 
            this.textBoxRepKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRepKey.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRepKey.Location = new System.Drawing.Point(230, 23);
            this.textBoxRepKey.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxRepKey.Name = "textBoxRepKey";
            this.textBoxRepKey.Size = new System.Drawing.Size(593, 34);
            this.textBoxRepKey.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Replacement Value";
            // 
            // textBoxRepValue
            // 
            this.textBoxRepValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRepValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRepValue.Location = new System.Drawing.Point(230, 83);
            this.textBoxRepValue.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxRepValue.Name = "textBoxRepValue";
            this.textBoxRepValue.Size = new System.Drawing.Size(593, 34);
            this.textBoxRepValue.TabIndex = 3;
            // 
            // buttonAddRep
            // 
            this.buttonAddRep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddRep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.buttonAddRep.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddRep.Location = new System.Drawing.Point(227, 141);
            this.buttonAddRep.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAddRep.Name = "buttonAddRep";
            this.buttonAddRep.Size = new System.Drawing.Size(596, 50);
            this.buttonAddRep.TabIndex = 4;
            this.buttonAddRep.Text = "ADD";
            this.buttonAddRep.UseVisualStyleBackColor = false;
            this.buttonAddRep.Click += new System.EventHandler(this.buttonAddRep_Click);
            // 
            // AddReplacement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(210)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(859, 242);
            this.Controls.Add(this.buttonAddRep);
            this.Controls.Add(this.textBoxRepValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxRepKey);
            this.Controls.Add(this.label1);
            this.Name = "AddReplacement";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Replacement Values";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRepKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRepValue;
        private System.Windows.Forms.Button buttonAddRep;
    }
}