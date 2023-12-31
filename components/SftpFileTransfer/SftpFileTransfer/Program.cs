﻿using HPE.HSP.UA3.Core.API.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSCP;

namespace SftpFileTransfer
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Program.LogInformation("\n--------------Program started-----------");
            try
            {
                SftpFileTransfer fileTransferSftp = new SftpFileTransfer();
                SftpFileTransferModel sftpFileTransferConfig = null;

                //1. Deserialize configuration            
                if (false == DeserializeConfigFile(ref sftpFileTransferConfig))
                    return;
                Program.LogInformation("Deserializing Config.Json - Success");

                Parallel.Invoke(() =>
                {
                //2. Download Files from sftp server 
                Program.LogInformation("\nDownload Operations - Start.");
                    fileTransferSftp.DownloadFiles(sftpFileTransferConfig);
                    Program.LogInformation("Download Operations - Complete.");

                },
                () =>
                {
                //3. Upload Files to sftp server 
                Program.LogInformation("\nUpload Operations - Start.");
                    fileTransferSftp.UploadFiles(sftpFileTransferConfig);
                    Program.LogInformation("Upload Operations - Complete.");
                }
                );
            }catch(Exception ex)
            {
                Program.LogInformation("Program got terminated: Exception Message:" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        public static void LogInformation(string log)
        {
            LoggerManager.Logger.LogInformational(log);
            Console.WriteLine(log);
        }
        static bool DeserializeConfigFile(ref SftpFileTransferModel  sftpFileTransferConfig)
        {
            try
            {
                sftpFileTransferConfig = JsonConvert.DeserializeObject<SftpFileTransferModel>(File.ReadAllText(@"config.json"));
                return true;
            }
            catch (Exception ex)
            {
                Program.LogInformation("Deserialization Error- Config file is invalid or corrupted.\n" + ex.Message);
            }
            return false;
        }
        [Obsolete]
        static void SerializeConfigToJsonTest()
        {
            SftpFileTransferModel sftpFileTransferConfig = new SftpFileTransferModel();
            sftpFileTransferConfig.SftpConfig = new SftpModel("10.0.0.8",
                                                            "vtcfguser",
                                                            "ShrTh3!nstr0",
                                                            22,
                                                            "ssh-rsa 2048 NTFvEOmbu8Aa/W3KXpnTDfQgXctIlwB8PXADt7EbHUI=");
            sftpFileTransferConfig.DownloadFromSftpConfig = new FileTransferModel[]
            {
                new FileTransferModel{ SftpDirectory="abc1",FileServerDirectory="dfd1"},
                new FileTransferModel { SftpDirectory = "abc2", FileServerDirectory = "dfd2" },
                new FileTransferModel { SftpDirectory = "abc3", FileServerDirectory = "dfd3" },
                new FileTransferModel { SftpDirectory = "abc4", FileServerDirectory = "dfd4" },
                new FileTransferModel { SftpDirectory = "abc5", FileServerDirectory = "dfd5" }
            };
            sftpFileTransferConfig.UploadToSftpConfig = new FileTransferModel[]
            {
                new FileTransferModel{ SftpDirectory="kkk1",FileServerDirectory="zzz1"},
                new FileTransferModel{ SftpDirectory="kkk2",FileServerDirectory="zzz2"},
                new FileTransferModel{ SftpDirectory="kkk3",FileServerDirectory="zzz3"},
                new FileTransferModel{ SftpDirectory="kkk4",FileServerDirectory="zzz4"},
                new FileTransferModel{ SftpDirectory="kkk5",FileServerDirectory="zzz5"},
            };

            //Serialize to File
            File.WriteAllText(@"c:\temp\Test.json", JsonConvert.SerializeObject(sftpFileTransferConfig));
            sftpFileTransferConfig = null;
        }
    }
}
