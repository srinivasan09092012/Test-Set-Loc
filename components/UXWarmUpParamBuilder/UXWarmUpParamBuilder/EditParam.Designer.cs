namespace UXWarmUpParamBuilder
{
    partial class EditParam
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
            this.richTextBoxJson = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonValidate = new System.Windows.Forms.Button();
            this.buttonSaveJson = new System.Windows.Forms.Button();
            this.labelJsonMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBoxJson
            // 
            this.richTextBoxJson.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxJson.Location = new System.Drawing.Point(12, 49);
            this.richTextBoxJson.Name = "richTextBoxJson";
            this.richTextBoxJson.Size = new System.Drawing.Size(1054, 578);
            this.richTextBoxJson.TabIndex = 0;
            this.richTextBoxJson.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(468, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Json Parameters";
            // 
            // buttonValidate
            // 
            this.buttonValidate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.buttonValidate.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonValidate.Location = new System.Drawing.Point(706, 641);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.Size = new System.Drawing.Size(171, 50);
            this.buttonValidate.TabIndex = 2;
            this.buttonValidate.Text = "Validate Josn";
            this.buttonValidate.UseVisualStyleBackColor = false;
            this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
            // 
            // buttonSaveJson
            // 
            this.buttonSaveJson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.buttonSaveJson.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveJson.Location = new System.Drawing.Point(897, 641);
            this.buttonSaveJson.Name = "buttonSaveJson";
            this.buttonSaveJson.Size = new System.Drawing.Size(171, 50);
            this.buttonSaveJson.TabIndex = 3;
            this.buttonSaveJson.Text = "Save";
            this.buttonSaveJson.UseVisualStyleBackColor = false;
            this.buttonSaveJson.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelJsonMsg
            // 
            this.labelJsonMsg.AutoSize = true;
            this.labelJsonMsg.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJsonMsg.Location = new System.Drawing.Point(26, 652);
            this.labelJsonMsg.Name = "labelJsonMsg";
            this.labelJsonMsg.Size = new System.Drawing.Size(160, 28);
            this.labelJsonMsg.TabIndex = 4;
            this.labelJsonMsg.Text = "Json Parameters";
            // 
            // EditParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(210)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1078, 702);
            this.Controls.Add(this.labelJsonMsg);
            this.Controls.Add(this.buttonSaveJson);
            this.Controls.Add(this.buttonValidate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxJson);
            this.Name = "EditParam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditParam";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxJson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonValidate;
        private System.Windows.Forms.Button buttonSaveJson;
        private System.Windows.Forms.Label labelJsonMsg;
    }
}