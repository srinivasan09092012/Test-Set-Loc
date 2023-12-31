﻿namespace WindowsFormsApp1
{
    partial class Decipher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Decipher));
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
            this.newguid = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Decode = new System.Windows.Forms.Button();
            this.Encode = new System.Windows.Forms.Button();
            this.HashStreamIdOriginal = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnCopyToNotePad = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnSQLHexToString = new System.Windows.Forms.Button();
            this.btnStringToSQLHex = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // textToCompOrDecomp
            // 
            this.textToCompOrDecomp.Location = new System.Drawing.Point(38, 49);
            this.textToCompOrDecomp.MaxLength = 2147483647;
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
            this.clear.Location = new System.Drawing.Point(1506, 215);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(152, 33);
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
            this.Copy.Size = new System.Drawing.Size(152, 33);
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
            this.GUIDConverter.Text = "Standard GUID";
            this.GUIDConverter.UseVisualStyleBackColor = true;
            this.GUIDConverter.Click += new System.EventHandler(this.GUIDConverter_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.newguid);
            this.groupBox1.Controls.Add(this.GUIDConverter);
            this.groupBox1.Controls.Add(this.RawConverter);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(38, 722);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(492, 61);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GUID";
            // 
            // newguid
            // 
            this.newguid.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.newguid.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newguid.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.newguid.Location = new System.Drawing.Point(326, 21);
            this.newguid.Name = "newguid";
            this.newguid.Size = new System.Drawing.Size(136, 33);
            this.newguid.TabIndex = 10;
            this.newguid.Text = "New GUID";
            this.newguid.UseVisualStyleBackColor = true;
            this.newguid.Click += new System.EventHandler(this.newguid_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Encrypt);
            this.groupBox2.Controls.Add(this.Decrypt);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(1089, 628);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(329, 68);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AES (AppSetting Encrypted Values)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Decompress);
            this.groupBox3.Controls.Add(this.Compress);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(38, 628);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(367, 60);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "GZip (Redis Cache Values)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Decode);
            this.groupBox4.Controls.Add(this.Encode);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(490, 628);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(339, 60);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Base 64";
            // 
            // Decode
            // 
            this.Decode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Decode.Location = new System.Drawing.Point(17, 21);
            this.Decode.Name = "Decode";
            this.Decode.Size = new System.Drawing.Size(136, 33);
            this.Decode.TabIndex = 5;
            this.Decode.Text = "Decode(B64)";
            this.Decode.UseVisualStyleBackColor = true;
            this.Decode.Click += new System.EventHandler(this.Decode_Click);
            // 
            // Encode
            // 
            this.Encode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Encode.Location = new System.Drawing.Point(169, 21);
            this.Encode.Name = "Encode";
            this.Encode.Size = new System.Drawing.Size(136, 33);
            this.Encode.TabIndex = 4;
            this.Encode.Text = "Encode(B64)";
            this.Encode.UseVisualStyleBackColor = true;
            this.Encode.Click += new System.EventHandler(this.Encode_Click);
            // 
            // HashStreamIdOriginal
            // 
            this.HashStreamIdOriginal.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.HashStreamIdOriginal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HashStreamIdOriginal.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.HashStreamIdOriginal.Location = new System.Drawing.Point(22, 21);
            this.HashStreamIdOriginal.Name = "HashStreamIdOriginal";
            this.HashStreamIdOriginal.Size = new System.Drawing.Size(192, 33);
            this.HashStreamIdOriginal.TabIndex = 10;
            this.HashStreamIdOriginal.Text = "Convert To StreamID";
            this.HashStreamIdOriginal.UseVisualStyleBackColor = true;
            this.HashStreamIdOriginal.Click += new System.EventHandler(this.HashStreamIdOriginal_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.HashStreamIdOriginal);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(589, 722);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(355, 61);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Convert Hash (Commits StreamIDOriginal)";
            // 
            // btnCopyToNotePad
            // 
            this.btnCopyToNotePad.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyToNotePad.Location = new System.Drawing.Point(1506, 151);
            this.btnCopyToNotePad.Name = "btnCopyToNotePad";
            this.btnCopyToNotePad.Size = new System.Drawing.Size(152, 33);
            this.btnCopyToNotePad.TabIndex = 6;
            this.btnCopyToNotePad.Text = "Copy to Notepad++";
            this.btnCopyToNotePad.UseVisualStyleBackColor = true;
            this.btnCopyToNotePad.Click += new System.EventHandler(this.btnCopyToNotePad_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnSQLHexToString);
            this.groupBox6.Controls.Add(this.btnStringToSQLHex);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(1079, 722);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(374, 77);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Commits Payload";
            // 
            // btnSQLHexToString
            // 
            this.btnSQLHexToString.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSQLHexToString.Location = new System.Drawing.Point(17, 21);
            this.btnSQLHexToString.Name = "btnSQLHexToString";
            this.btnSQLHexToString.Size = new System.Drawing.Size(149, 33);
            this.btnSQLHexToString.TabIndex = 5;
            this.btnSQLHexToString.Text = "SQL-HEXToString";
            this.btnSQLHexToString.UseVisualStyleBackColor = true;
            this.btnSQLHexToString.Click += new System.EventHandler(this.btnSQLHexToString_Click);
            // 
            // btnStringToSQLHex
            // 
            this.btnStringToSQLHex.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStringToSQLHex.Location = new System.Drawing.Point(203, 21);
            this.btnStringToSQLHex.Name = "btnStringToSQLHex";
            this.btnStringToSQLHex.Size = new System.Drawing.Size(148, 33);
            this.btnStringToSQLHex.TabIndex = 4;
            this.btnStringToSQLHex.Text = "StringToSQL-HEX";
            this.btnStringToSQLHex.UseVisualStyleBackColor = true;
            this.btnStringToSQLHex.Click += new System.EventHandler(this.btnStringToSQLHex_Click);
            // 
            // Decipher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1686, 839);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.btnCopyToNotePad);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Copy);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.textToCompOrDecomp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Decipher";
            this.Text = "Decipher Utility";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
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
        private System.Windows.Forms.Button HashStreamIdOriginal;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button newguid;
        private System.Windows.Forms.Button btnCopyToNotePad;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnSQLHexToString;
        private System.Windows.Forms.Button btnStringToSQLHex;
    }
}

