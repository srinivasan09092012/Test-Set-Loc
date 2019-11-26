using System;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Helpers;

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
                textToCompOrDecomp.Text = GZipHelper.Compress(textToCompOrDecomp.Text);
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
                textToCompOrDecomp.Text = GZipHelper.Decompress(textToCompOrDecomp.Text);
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

                textToCompOrDecomp.Text = Cryptographer.Decrypt(aesIV, key, paddingMode, textToCompOrDecomp.Text);
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

                textToCompOrDecomp.Text = Cryptographer.Encrypt(aesIV, key, paddingMode, textToCompOrDecomp.Text);
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't AES Encrypt this string!!" + Environment.NewLine + ex.Message;
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            textToCompOrDecomp.Text = "";
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textToCompOrDecomp.Text);
        }

        private void RawConverter_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = ParseHex(textToCompOrDecomp.Text.Replace("-", ""));
                Guid guid = new Guid(bytes);
                textToCompOrDecomp.Text = guid.ToString("N").ToUpperInvariant();
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't RAW convert this string!!" + Environment.NewLine + ex.Message;
            }
        }

        private void GUIDConverter_Click(object sender, EventArgs e)
        {
            try
            {
                Guid guid = new Guid(textToCompOrDecomp.Text);
                textToCompOrDecomp.Text = Guid.Parse(BitConverter.ToString(guid.ToByteArray()).Replace("-", "")).ToString().ToLowerInvariant();                
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't convert to a GUID!!" + Environment.NewLine + ex.Message;
            }
        }

        static byte[] ParseHex(string text)
        {
            byte[] ret = new byte[text.Length / 2];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = Convert.ToByte(text.Substring(i * 2, 2), 16);
            }
            return ret;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void Decode_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = System.Convert.FromBase64String(textToCompOrDecomp.Text);
                textToCompOrDecomp.Text = System.Text.ASCIIEncoding.ASCII.GetString(data);
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't BASE 64 decode this string" + Environment.NewLine + ex.Message;
            }
        }

        private void Encode_Click(object sender, EventArgs e)
        {
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(textToCompOrDecomp.Text);
                textToCompOrDecomp.Text = System.Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't BASE 64 decode this string" + Environment.NewLine + ex.Message;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
