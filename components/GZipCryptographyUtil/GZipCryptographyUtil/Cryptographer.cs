//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HP.HSP.UA3.Core.BAS.CQRS.Helpers
{
    /// <summary>
    /// Provides common data encryption operations.
    /// </summary>
    public static class Cryptographer
    {
        /// <summary>
        /// Encrypts data using the AES algorithm.
        /// </summary>
        /// <param name="iv">Encryption initialization vector.</param>
        /// <param name="key">Encryption key.</param>
        /// <param name="paddingMode">Encryption padding mode.</param>
        /// <param name="data">Data to encrypt.</param>
        /// <returns>Encrypted data base64 string.</returns>
        public static string Encrypt(string iv, string key, PaddingMode paddingMode, string data)
        {
            if (string.IsNullOrEmpty(iv))
            {
                throw new ArgumentException("iv");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("key");
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentException("data");
            }

            using (AesCryptoServiceProvider encryptor = new AesCryptoServiceProvider())
            {
                encryptor.IV = Convert.FromBase64String(iv);
                encryptor.Key = Convert.FromBase64String(key);
                encryptor.Padding = paddingMode;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (ICryptoTransform ct = encryptor.CreateEncryptor(encryptor.Key, encryptor.IV))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(Encoding.Default.GetBytes(data), 0, Encoding.Default.GetBytes(data).Length);
                            cs.FlushFinalBlock();

                            return Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts data using the AES algorithm.
        /// </summary>
        /// <param name="iv">Encryption initialization vector.</param>
        /// <param name="key">Encryption key.</param>
        /// <param name="paddingMode">Encryption padding mode.</param>
        /// <param name="data">Data to decrypt.</param>
        /// <returns>Decrypted data string.</returns>
        public static string Decrypt(string iv, string key, PaddingMode paddingMode, string data)
        {
            if (string.IsNullOrEmpty(iv))
            {
                throw new ArgumentException("iv");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("key");
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentException("data");
            }

            using (AesCryptoServiceProvider encryptor = new AesCryptoServiceProvider())
            {
                encryptor.IV = Convert.FromBase64String(iv);
                encryptor.Key = Convert.FromBase64String(key);
                encryptor.Padding = paddingMode;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (ICryptoTransform ct = encryptor.CreateDecryptor(encryptor.Key, encryptor.IV))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            byte[] byteData = Convert.FromBase64String(data);
                            cs.Write(byteData, 0, byteData.Length);
                            cs.FlushFinalBlock();

                            return Encoding.Default.GetString(ms.ToArray());
                        }
                    }
                }
            }
        }
    }
}
