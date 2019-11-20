//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace HP.HSP.UA3.Core.BAS.CQRS.Caching
{
    public static class GZipHelper
    {
        public static string Compress(this string text)
        {
            byte[] gzipBuffer = null;
            var buffer = Encoding.UTF8.GetBytes(text);
            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream();
                using (var stream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    stream.Write(buffer, 0, buffer.Length);
                }

                memoryStream.Position = 0;
                var compressed = new byte[memoryStream.Length];
                memoryStream.Read(compressed, 0, compressed.Length);
                gzipBuffer = new byte[compressed.Length + 4];
                Buffer.BlockCopy(compressed, 0, gzipBuffer, 4, compressed.Length);
                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzipBuffer, 0, 4);
                compressed = null;
                return Convert.ToBase64String(gzipBuffer);
            }
            finally
            {
                buffer = null;
                gzipBuffer = null;
                if (memoryStream != null)
                {
                    memoryStream.Dispose();
                }
            }
        }

        public static string Decompress(this string compressedText)
        {
            byte[] buffer = null;
            var gzipBuffer = Convert.FromBase64String(compressedText);
            MemoryStream memoryStream = null;

            try
            {
                memoryStream = new MemoryStream();
                int dataLength = BitConverter.ToInt32(gzipBuffer, 0);
                memoryStream.Write(gzipBuffer, 4, gzipBuffer.Length - 4);
                buffer = new byte[dataLength];
                memoryStream.Position = 0;
                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gzipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
            finally
            {
                gzipBuffer = null;
                buffer = null;
                if (memoryStream != null)
                {
                    memoryStream.Dispose();
                }
            }
        }
    }
}
