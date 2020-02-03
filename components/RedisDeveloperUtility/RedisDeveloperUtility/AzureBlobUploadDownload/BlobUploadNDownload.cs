using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDeveloperUtility.AzureBlobUploadDownload
{
    class BlobUploadNDownload
    {
        public static void BlobManager(string typeOfTenant, string countryCode, string upLoadDownLoad, string env)
        {
            if (upLoadDownLoad.ToLower().Equals("download"))
            {
                DownloadFile(typeOfTenant, countryCode, env);
            }
            else
            {
                UploadFile(countryCode, env);
            }
        }

        private static void UploadFile(string countryCode, string env)
        {
            string storageConnection = string.Empty;
            string azureDumpFilePath = string.Format(ConfigurationManager.AppSettings["azureDumpFilePath"], env);
            string azureFolderPath = string.Empty;
            if (countryCode.ToLower().Equals("us"))
            {
                storageConnection = ConfigurationManager.AppSettings["blobStorageUSSASKey"];
                azureFolderPath = ConfigurationManager.AppSettings["azureUSFolderPath"];
            }
            else if (countryCode.ToLower().Equals("in"))
            {
                storageConnection = ConfigurationManager.AppSettings["blobStorageINSASKey"];
                azureFolderPath = ConfigurationManager.AppSettings["azureINFolderPath"];
            }
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);

            string localDumpFilePath = string.Format(ConfigurationManager.AppSettings["localDumpFilePath"], env);

            //create a block blob 
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            //create a container 
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(azureFolderPath);
            CloudBlockBlob cloudBlockBlob = null;
            //Deleting the Medicaid Dump Files
            DeleteFilesFromAzureBlob(cloudBlockBlob, cloudBlobContainer, azureDumpFilePath, "Medicaid");

            Console.WriteLine("Export to Explorer Started :: Medicaid :::" + DateTime.Now);
            UploadFileToAzureBlob(cloudBlockBlob, cloudBlobContainer, azureDumpFilePath, localDumpFilePath, "Medicaid");
            Console.WriteLine("Export to Explorer Completed :: Medicaid :::" + DateTime.Now);

            //Deleting the Commercial Dump Files 
            DeleteFilesFromAzureBlob(cloudBlockBlob, cloudBlobContainer, azureDumpFilePath, "Commercial");

            Console.WriteLine("Export to Explorer Started :: Commercial :::" + DateTime.Now);
            UploadFileToAzureBlob(cloudBlockBlob, cloudBlobContainer, azureDumpFilePath, localDumpFilePath, "Commercial");
            Console.WriteLine("Export to Explorer Completed :: Commercial :::" + DateTime.Now);
        }

        private static void UploadFileToAzureBlob(CloudBlockBlob cloudBlockBlob, CloudBlobContainer cloudBlobContainer, string azureDumpFilePath, string localDumpFilePath, string type)
        {
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}.rdb", type));
            cloudBlockBlob.UploadFromFile(localDumpFilePath + string.Format("dump-{0}.rdb", type));
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}-5.rdb", type));
            cloudBlockBlob.UploadFromFile(localDumpFilePath + string.Format("dump-{0}-5.rdb", type));
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}-4.rdb", type));
            cloudBlockBlob.UploadFromFile(localDumpFilePath + string.Format("dump-{0}-4.rdb", type));
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}-3.rdb", type));
            cloudBlockBlob.UploadFromFile(localDumpFilePath + string.Format("dump-{0}-3.rdb", type));
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}-2.rdb", type));
            cloudBlockBlob.UploadFromFile(localDumpFilePath + string.Format("dump-{0}-2.rdb", type));
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}-1.rdb", type));
            cloudBlockBlob.UploadFromFile(localDumpFilePath + string.Format("dump-{0}-1.rdb", type));

        }

        private static void DeleteFilesFromAzureBlob(CloudBlockBlob cloudBlockBlob, CloudBlobContainer cloudBlobContainer, string azureDumpFilePath, string type)
        {
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}-5.rdb", type));
            if (cloudBlockBlob.Exists())
            {
                cloudBlockBlob.Delete();
            }
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}-4.rdb", type));
            if (cloudBlockBlob.Exists())
            {
                cloudBlockBlob.Delete();
            }
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}-3.rdb", type));
            if (cloudBlockBlob.Exists())
            {
                cloudBlockBlob.Delete();
            }
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}-2.rdb", type));
            if (cloudBlockBlob.Exists())
            {
                cloudBlockBlob.Delete();
            }
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}-1.rdb", type));
            if (cloudBlockBlob.Exists())
            {
                cloudBlockBlob.Delete();
            }
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureDumpFilePath + string.Format("dump-{0}.rdb", type));
            if (cloudBlockBlob.Exists())
            {
                cloudBlockBlob.Delete();
            }
        }

        private static void DownloadFile(string typeOfTenant, string countryCode, string env)
        {
            string azureDumpFilePath = string.Format(ConfigurationManager.AppSettings["azureDumpFilePath"], env);

            if (!System.IO.Directory.Exists(azureDumpFilePath))
            {
                System.IO.Directory.CreateDirectory(azureDumpFilePath);
            }

            string fileName = string.Empty;
            string azureFolderPath = string.Empty;


            string storageConnection = string.Empty;
            if (countryCode.ToLower().Equals("us"))
            {
                storageConnection = ConfigurationManager.AppSettings["blobStorageUSSASKey"];
                azureFolderPath = ConfigurationManager.AppSettings["azureUSFolderPath"];
            }
            else if (countryCode.ToLower().Equals("in"))
            {
                storageConnection = ConfigurationManager.AppSettings["blobStorageINSASKey"];
                azureFolderPath = ConfigurationManager.AppSettings["azureINFolderPath"];
            }

            if (typeOfTenant.ToLower().Equals("medicaid"))
            {
                fileName = azureDumpFilePath + @"dump-Medicaid.rdb";
            }
            else if (typeOfTenant.ToLower().Equals("commercial"))
            {
                fileName = azureDumpFilePath + @"dump-Commercial.rdb";
            }
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);

            //create a block blob 
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            try
            {
                //create a container 
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(azureFolderPath);


                CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(fileName);

                blob.DownloadToFile(fileName, System.IO.FileMode.Create);
            }
            catch (Exception exp)
            {
                Console.WriteLine("Internal Error Please revisit after some time...");
                Console.WriteLine("Possible issue: Server might be going through dump refresh");
                Console.WriteLine(exp.StackTrace);
            }
        }
    }
}
