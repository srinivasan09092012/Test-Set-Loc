namespace WindowsFormsApp1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textToCompOrDecomp = new System.Windows.Forms.TextBox();
            this.Compress = new System.Windows.Forms.Button();
            this.Decompress = new System.Windows.Forms.Button();
            this.Decrypt = new System.Windows.Forms.Button();
            this.Encrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textToCompOrDecomp
            // 
            this.textToCompOrDecomp.Location = new System.Drawing.Point(85, 55);
            this.textToCompOrDecomp.Multiline = true;
            this.textToCompOrDecomp.Name = "textToCompOrDecomp";
            this.textToCompOrDecomp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textToCompOrDecomp.Size = new System.Drawing.Size(992, 506);
            this.textToCompOrDecomp.TabIndex = 0;
            // 
            // Compress
            // 
            this.Compress.Location = new System.Drawing.Point(941, 583);
            this.Compress.Name = "Compress";
            this.Compress.Size = new System.Drawing.Size(136, 33);
            this.Compress.TabIndex = 2;
            this.Compress.Text = "GZip Compress";
            this.Compress.UseVisualStyleBackColor = true;
            this.Compress.Click += new System.EventHandler(this.Compress_Click);
            // 
            // Decompress
            // 
            this.Decompress.Location = new System.Drawing.Point(743, 583);
            this.Decompress.Name = "Decompress";
            this.Decompress.Size = new System.Drawing.Size(178, 33);
            this.Decompress.TabIndex = 3;
            this.Decompress.Text = "GZip DE - Compress";
            this.Decompress.UseVisualStyleBackColor = true;
            this.Decompress.Click += new System.EventHandler(this.Decompress_Click);
            // 
            // Decrypt
            // 
            this.Decrypt.Location = new System.Drawing.Point(394, 583);
            this.Decrypt.Name = "Decrypt";
            this.Decrypt.Size = new System.Drawing.Size(136, 33);
            this.Decrypt.TabIndex = 4;
            this.Decrypt.Text = "AES Decrypt";
            this.Decrypt.UseVisualStyleBackColor = true;
            this.Decrypt.Click += new System.EventHandler(this.Decrypt_Click);
            // 
            // Encrypt
            // 
            this.Encrypt.Location = new System.Drawing.Point(563, 583);
            this.Encrypt.Name = "Encrypt";
            this.Encrypt.Size = new System.Drawing.Size(136, 33);
            this.Encrypt.TabIndex = 5;
            this.Encrypt.Text = "AES Encrypt";
            this.Encrypt.UseVisualStyleBackColor = true;
            this.Encrypt.Click += new System.EventHandler(this.Encrypt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 662);
            this.Controls.Add(this.Encrypt);
            this.Controls.Add(this.Decrypt);
            this.Controls.Add(this.Decompress);
            this.Controls.Add(this.Compress);
            this.Controls.Add(this.textToCompOrDecomp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Gzip Utility to Compress or Decompress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textToCompOrDecomp;
        private System.Windows.Forms.Button Compress;
        private System.Windows.Forms.Button Decompress;
        private System.Windows.Forms.Button Decrypt;
        private System.Windows.Forms.Button Encrypt;
    }
}

