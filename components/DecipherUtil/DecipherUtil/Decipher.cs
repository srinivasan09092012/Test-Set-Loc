using System;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Helpers;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public partial class Decipher : Form
    {
        public Decipher()
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
                string jsonString = GZipHelper.Decompress(textToCompOrDecomp.Text);
                JsonFormatter jsonFormatter = new JsonFormatter();
                jsonString = jsonFormatter.PrettyPrint(jsonString);

                textToCompOrDecomp.Text = jsonString;
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
            textToCompOrDecomp.Focus();
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
                JsonFormatter jsonFormatter = new JsonFormatter();
                string jsonString = jsonFormatter.PrettyPrint(System.Text.ASCIIEncoding.ASCII.GetString(data));
                if (jsonString.Substring(0, 3) == "???")
                {
                    jsonString = jsonString.Substring(3, jsonString.Length-3);
                }
                textToCompOrDecomp.Text = jsonString;
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
                textToCompOrDecomp.Text = "Can't BASE 64 encode this string" + Environment.NewLine + ex.Message;
            }
        }

        static byte[] StrToByteArray(string str)
        {
            Dictionary<string, byte> hexindex = new Dictionary<string, byte>();
            for (int i = 0; i <= 255; i++)
                hexindex.Add(i.ToString("X2"), (byte)i);

            List<byte> hexres = new List<byte>();
            for (int i = 0; i < str.Length; i += 2)
                hexres.Add(hexindex[str.Substring(i, 2)]);

            return hexres.ToArray();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void HashStreamIdOriginal_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] hashBytes = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(textToCompOrDecomp.Text));
                textToCompOrDecomp.Text = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't hash the streamIDOriginal" + Environment.NewLine + ex.Message;
            }
        }

        private void newguid_Click(object sender, EventArgs e)
        {
            try
            {
                textToCompOrDecomp.Text = Guid.NewGuid().ToString();
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't create the NEW GUID" + Environment.NewLine + ex.Message;
            }
        }

        private void btnCopyToNotePad_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(@"C:\Temp\Temporary.json", textToCompOrDecomp.Text);

                if (File.Exists(@"C:\Program Files (x86)\Notepad++\notepad++.exe"))
                {
                    Process.Start(@"C:\Program Files (x86)\Notepad++\notepad++.exe", @"C:\Temp\Temporary.json");
                }
                else
                {
                    Process.Start(@"C:\Program Files\Notepad++\notepad++.exe", @"C:\Temp\Temporary.json");
                }

                Thread.Sleep(500);

            }
            catch 
            {
                Process.Start("notepad", @"C:\Temp\Temporary.json");
                Thread.Sleep(500);
            }
        }

        public static string ConvertToHex(string asciiString)
        {
            var bytes = Encoding.UTF8.GetBytes(asciiString);
            string hex = BitConverter.ToString(bytes);
            hex = hex.Replace("-", string.Empty);

            return hex;
        }

        private void btnSQLHexToString_Click(object sender, EventArgs e)
        {
            string varbinaryStr = textToCompOrDecomp.Text;

            var no_0x_varbinary_str = varbinaryStr.Replace("0x", "").Trim();
            byte[] bytes = StrToByteArray(no_0x_varbinary_str);

            textToCompOrDecomp.Text = Encoding.UTF8.GetString(bytes);
        }

        private void btnStringToSQLHex_Click(object sender, EventArgs e)
        {
            try
            {
                textToCompOrDecomp.Text = "0x" + ConvertToHex(textToCompOrDecomp.Text).ToUpper();
            }
            catch (Exception ex)
            {
                textToCompOrDecomp.Text = "Can't decode this string" + Environment.NewLine + ex.Message;
            }
        }
    }
}
