﻿using System;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Compress_Click(object sender, EventArgs e)
        {
            try
            {
                textToCompOrDecomp.Text = HP.HSP.UA3.Core.BAS.CQRS.Caching.GZipHelper.Compress(textToCompOrDecomp.Text);
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't GZip Compress this string!!" + Environment.NewLine + ex.Message;
            }
        }


        private void Decompress_Click(object sender, EventArgs e)
        {
            try
            {
                textToCompOrDecomp.Text = HP.HSP.UA3.Core.BAS.CQRS.Caching.GZipHelper.Decompress(textToCompOrDecomp.Text);
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't GZip De-compress this string!!" + Environment.NewLine + ex.Message;
            }
        }

        private void Decrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string key = ConfigurationManager.AppSettings["AESKey"];
                string aesIV = ConfigurationManager.AppSettings["AESIV"];
                PaddingMode paddingMode = (PaddingMode)Enum.Parse(typeof(PaddingMode), ConfigurationManager.AppSettings["PaddingMode"]);

                textToCompOrDecomp.Text = HP.HSP.UA3.Core.BAS.CQRS.Helpers.Cryptographer.Decrypt(aesIV, key, paddingMode, textToCompOrDecomp.Text);
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't AES Decrypt this string!!" + Environment.NewLine + ex.Message;
            }
        }

        private void Encrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string key = ConfigurationManager.AppSettings["AESKey"];
                string aesIV = ConfigurationManager.AppSettings["AESIV"];
                PaddingMode paddingMode = (PaddingMode)Enum.Parse(typeof(PaddingMode), ConfigurationManager.AppSettings["PaddingMode"]);

                textToCompOrDecomp.Text = HP.HSP.UA3.Core.BAS.CQRS.Helpers.Cryptographer.Encrypt(aesIV, key, paddingMode, textToCompOrDecomp.Text);
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't AES Encrypt this string!!" + Environment.NewLine + ex.Message;
            }
        }
    }
}
