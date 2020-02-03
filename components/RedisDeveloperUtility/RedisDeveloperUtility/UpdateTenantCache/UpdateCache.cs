using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDeveloperUtility.UpdateTenantCache
{
    class UpdateCache
    {

        public static void MakeTenantActiveDisableOtherTenants(string tenantGUID)
        {

            if (!String.IsNullOrEmpty(tenantGUID))
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["redisConnectString"]);
                IDatabase db = redis.GetDatabase();

                string value = Decompress(db.StringGet(ConfigurationManager.AppSettings["redisTenantKey"]));
                List<TenantModel> tmp = JsonConvert.DeserializeObject<List<TenantModel>>(value);
                Guid tenantID = Guid.Parse(tenantGUID);
                tmp.Where(mod => mod.TenantID != tenantID).ToList().ForEach(x => x.IsActive = false);
                tmp.Where(mod => mod.TenantID == tenantID).ToList().ForEach(x => x.IsActive = true);

                value = Compress(JsonConvert.SerializeObject(tmp));
                TimeSpan time = new TimeSpan(720, 60, 60);

                db.StringSet(ConfigurationManager.AppSettings["redisTenantKey"], value, time);

                Environment.Exit(0);
            }
            else
            {
                Environment.Exit(1);
            }

        }


        public static string Decompress(string compressedText)
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

        public static string Compress(string text)
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
    }
}
