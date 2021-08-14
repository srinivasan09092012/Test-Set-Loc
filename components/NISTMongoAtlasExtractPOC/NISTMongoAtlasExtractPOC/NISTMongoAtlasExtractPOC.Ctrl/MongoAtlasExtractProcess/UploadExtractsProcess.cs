//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HPE.HSP.UA3.Core.API.Logger;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace NISTMongoAtlasExtractPOC.Ctrl
{
    public class UploadExtractsProcess
    {
        ////***************************************************************************************************************/
        //// Job Configuration parameters
        ////***************************************************************************************************************/
        //// local output folder for capturing response content
        private string localDir = ConfigurationManager.AppSettings.Get("localRootDir");
        private string evidenceVault = ConfigurationManager.AppSettings.Get("evidenceVault");
        private string baseContainerName = "mongodbauditlogs";

        private List<HelperClasses.FilePath> extractDirs;
     
        public UploadExtractsProcess(ref List<HelperClasses.FilePath> extractDirs)
        {
            LoggerManager.Logger.LogInformational("Evidence Vault Upload Initiated.  Vault: " + this.evidenceVault + " ************************************");

            this.extractDirs = extractDirs;
        }
       
        public void UploadExtracts()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Lsa\\fipsalgorithmpolicy");
            if (key != null)
            {
                var keyValue = key.GetValue("enabled");
                if (keyValue != null && keyValue.ToString() == "1")
                {
                   CloudStorageAccount.UseV1MD5 = false; //// enable FIPS compliant cryptographic algorithm
                }
            }

           CloudBlobContainer container = this.GetAndOrCreateContainer(this.evidenceVault, this.baseContainerName);

            foreach (HelperClasses.FilePath dir in this.extractDirs)
            {
                LoggerManager.Logger.LogInformational("Beginning upload of " + dir.Path + ".");
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                string evidenceDir = dir.SubDirectories.Replace("\\", "/") + dir.FileName;
   
                //// remove leading backslash to avoid creation of "blank" directories
                if (evidenceDir.Substring(0, 1) == "\\")
                {
                    evidenceDir = evidenceDir.Remove(0, 1);
                }

                var blockBlob = container.GetBlockBlobReference(evidenceDir);
                using (var fileStream = System.IO.File.OpenRead(dir.Path))
                {
                    blockBlob.UploadFromStream(fileStream);
                }
      
                LoggerManager.Logger.LogInformational("File " + dir.Path + " successfully written to Evidence Vault as '" + evidenceDir + "' in " + stopwatch.ElapsedMilliseconds.ToString() + " MS.");
            }

            LoggerManager.Logger.LogInformational("Evidence Vault Upload Completed.  Vault: " + this.evidenceVault + " ************************************");
        }

        private CloudBlobContainer GetAndOrCreateContainer(string connStr, string containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connStr);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            BlobRequestOptions requestOptions = new BlobRequestOptions() { RetryPolicy = new NoRetry() };
            container.CreateIfNotExists(requestOptions, null);
            return container;
        }
    }
}
