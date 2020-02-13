using RedisDeveloperUtility.AzureBlobUploadDownload;
using RedisDeveloperUtility.UpdateTenantCache;
using System;
namespace RedisDeveloperUtility
{
    class Launcher
    {
        static void Main(string[] args)
        {
          
            //1. Download Medicaid for India
            //2. Download Commercial for India
            //3. Download Medicaid for USA
            //4. Download Commercial for USA
            //5. Upload the Medicaid and Commercial for India
            //6. Upload the Medicaid and Commercial for USA
            //7. Update Tenant Cache
            // Testing Push



            if (args != null && args[0] == "1")
            {
                BlobUploadNDownload.BlobManager("Medicaid", "IN", "download", args[1]);
            }
            else if (args != null && args[0] == "2")
            {
                BlobUploadNDownload.BlobManager("Commercial", "IN", "download", args[1]);
            }
            else if (args != null && args[0] == "3")
            {
                BlobUploadNDownload.BlobManager("Medicaid", "US", "download", args[1]);
            }
            else if (args != null && args[0] == "4")
            {
                BlobUploadNDownload.BlobManager("Commercial", "US", "download", args[1]);
            }
            else if (args != null && args[0] == "5")
            {
                BlobUploadNDownload.BlobManager("", "IN", "upload", args[1]);
            }
            else if (args != null && args[0] == "6")
            {
                BlobUploadNDownload.BlobManager("", "US", "upload", args[1]);
            }
            else if (args != null && args[0] == "7")
            {
                UpdateCache.MakeTenantActiveDisableOtherTenants(args[1]);
            }
        }
    }
}
