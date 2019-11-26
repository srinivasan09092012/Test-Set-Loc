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
            this.clear = new System.Windows.Forms.Button();
            this.Copy = new System.Windows.Forms.Button();
            this.RawConverter = new System.Windows.Forms.Button();
            this.GUIDConverter = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Decode = new System.Windows.Forms.Button();
            this.Encode = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // textToCompOrDecomp
            // 
            this.textToCompOrDecomp.Location = new System.Drawing.Point(38, 49);
            this.textToCompOrDecomp.MaxLength = 1500000;
            this.textToCompOrDecomp.Multiline = true;
            this.textToCompOrDecomp.Name = "textToCompOrDecomp";
            this.textToCompOrDecomp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textToCompOrDecomp.Size = new System.Drawing.Size(1448, 563);
            this.textToCompOrDecomp.TabIndex = 0;
            // 
            // Compress
            // 
            this.Compress.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Compress.Location = new System.Drawing.Point(211, 20);
            this.Compress.Name = "Compress";
            this.Compress.Size = new System.Drawing.Size(136, 33);
            this.Compress.TabIndex = 2;
            this.Compress.Text = "GZip Compress";
            this.Compress.UseVisualStyleBackColor = true;
            this.Compress.Click += new System.EventHandler(this.Compress_Click);
            // 
            // Decompress
            // 
            this.Decompress.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Decompress.Location = new System.Drawing.Point(13, 20);
            this.Decompress.Name = "Decompress";
            this.Decompress.Size = new System.Drawing.Size(178, 33);
            this.Decompress.TabIndex = 3;
            this.Decompress.Text = "GZip Decompress";
            this.Decompress.UseVisualStyleBackColor = true;
            this.Decompress.Click += new System.EventHandler(this.Decompress_Click);
            // 
            // Decrypt
            // 
            this.Decrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Decrypt.Location = new System.Drawing.Point(6, 21);
            this.Decrypt.Name = "Decrypt";
            this.Decrypt.Size = new System.Drawing.Size(136, 33);
            this.Decrypt.TabIndex = 4;
            this.Decrypt.Text = "AES Decrypt";
            this.Decrypt.UseVisualStyleBackColor = true;
            this.Decrypt.Click += new System.EventHandler(this.Decrypt_Click);
            // 
            // Encrypt
            // 
            this.Encrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Encrypt.Location = new System.Drawing.Point(175, 21);
            this.Encrypt.Name = "Encrypt";
            this.Encrypt.Size = new System.Drawing.Size(136, 33);
            this.Encrypt.TabIndex = 5;
            this.Encrypt.Text = "AES Encrypt";
            this.Encrypt.UseVisualStyleBackColor = true;
            this.Encrypt.Click += new System.EventHandler(this.Encrypt_Click);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(1506, 148);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(136, 33);
            this.clear.TabIndex = 6;
            this.clear.Text = "Clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // Copy
            // 
            this.Copy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Copy.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Copy.Location = new System.Drawing.Point(1506, 84);
            this.Copy.Name = "Copy";
            this.Copy.Size = new System.Drawing.Size(136, 33);
            this.Copy.TabIndex = 7;
            this.Copy.Text = "Copy";
            this.Copy.UseVisualStyleBackColor = true;
            this.Copy.Click += new System.EventHandler(this.Copy_Click);
            // 
            // RawConverter
            // 
            this.RawConverter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RawConverter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RawConverter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.RawConverter.Location = new System.Drawing.Point(6, 21);
            this.RawConverter.Name = "RawConverter";
            this.RawConverter.Size = new System.Drawing.Size(136, 33);
            this.RawConverter.TabIndex = 8;
            this.RawConverter.Text = "RAW";
            this.RawConverter.UseVisualStyleBackColor = true;
            this.RawConverter.Click += new System.EventHandler(this.RawConverter_Click);
            // 
            // GUIDConverter
            // 
            this.GUIDConverter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GUIDConverter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GUIDConverter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GUIDConverter.Location = new System.Drawing.Point(163, 21);
            this.GUIDConverter.Name = "GUIDConverter";
            this.GUIDConverter.Size = new System.Drawing.Size(136, 33);
            this.GUIDConverter.TabIndex = 9;
            this.GUIDConverter.Text = "STANDARD GUID";
            this.GUIDConverter.UseVisualStyleBackColor = true;
            this.GUIDConverter.Click += new System.EventHandler(this.GUIDConverter_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GUIDConverter);
            this.groupBox1.Controls.Add(this.RawConverter);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(38, 627);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 61);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GUID";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Encrypt);
            this.groupBox2.Controls.Add(this.Decrypt);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(752, 627);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(329, 68);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AES";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Decompress);
            this.groupBox3.Controls.Add(this.Compress);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(1119, 627);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(367, 60);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "GZip";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Decode);
            this.groupBox4.Controls.Add(this.Encode);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(387, 628);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(317, 60);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "BASE 64 ";
            // 
            // Decode
            // 
            this.Decode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Decode.Location = new System.Drawing.Point(168, 21);
            this.Decode.Name = "Decode";
            this.Decode.Size = new System.Drawing.Size(136, 33);
            this.Decode.TabIndex = 5;
            this.Decode.Text = "Decode";
            this.Decode.UseVisualStyleBackColor = true;
            this.Decode.Click += new System.EventHandler(this.Decode_Click);
            // 
            // Encode
            // 
            this.Encode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Encode.Location = new System.Drawing.Point(6, 21);
            this.Encode.Name = "Encode";
            this.Encode.Size = new System.Drawing.Size(136, 33);
            this.Encode.TabIndex = 4;
            this.Encode.Text = "Encode";
            this.Encode.UseVisualStyleBackColor = true;
            this.Encode.Click += new System.EventHandler(this.Encode_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1687, 717);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Copy);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.textToCompOrDecomp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Gzip Utility to Compress or Decompress";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textToCompOrDecomp;
        private System.Windows.Forms.Button Compress;
        private System.Windows.Forms.Button Decompress;
        private System.Windows.Forms.Button Decrypt;
        private System.Windows.Forms.Button Encrypt;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button Copy;
        private System.Windows.Forms.Button RawConverter;
        private System.Windows.Forms.Button GUIDConverter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button Decode;
        private System.Windows.Forms.Button Encode;
    }
}

