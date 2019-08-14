using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSCP;

namespace SftpFileTransfer
{
    public class SftpFileTransfer
    {
        public bool DownloadFiles(SftpFileTransferModel config)
        {
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = config.SftpConfig.ServerIP,
                UserName = config.SftpConfig.UserName,
                Password = config.SftpConfig.Password,
                SshHostKeyFingerprint = config.SftpConfig.SshHostKeyFingerprint
            };
            Parallel.ForEach(config.DownloadFromSftpConfig, (filetransferConfig)=>
            {
                if (false == filetransferConfig.FileServerDirectory.EndsWith(@"\"))
                    filetransferConfig.FileServerDirectory += @"\";
                if (false == filetransferConfig.SftpDirectory.EndsWith(@"/"))
                    filetransferConfig.SftpDirectory += @"/";
                try
                {
                    Program.LogInformation("\nDownload from Path=" + filetransferConfig.SftpDirectory + "\t to" + filetransferConfig.FileServerDirectory + " - Start");
                    StartSftpDownloadSession(sessionOptions, filetransferConfig);
                    Program.LogInformation("\nDownload from Path=" + filetransferConfig.SftpDirectory + "\t to" + filetransferConfig.FileServerDirectory + " - End");
                }
                catch (Exception ex)
                {
                    Program.LogInformation("Download from: " + filetransferConfig.SftpDirectory + "\t To: " + filetransferConfig.FileServerDirectory + "\t***Failed***");
                    Program.LogInformation(ex.Message + "\n" + ex.StackTrace);
                }
            });

            return true;
        }
        public bool UploadFiles(SftpFileTransferModel config)
        {
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = config.SftpConfig.ServerIP,
                UserName = config.SftpConfig.UserName,
                Password = config.SftpConfig.Password,
                SshHostKeyFingerprint = config.SftpConfig.SshHostKeyFingerprint
            };
            Parallel.ForEach(config.UploadToSftpConfig, (filetransferConfig)=>{
                if (false == filetransferConfig.FileServerDirectory.EndsWith(@"\"))
                    filetransferConfig.FileServerDirectory += @"\";
                if (false == filetransferConfig.SftpDirectory.EndsWith(@"/"))
                    filetransferConfig.SftpDirectory += @"/";
                try
                {
                    Program.LogInformation("\nUpload from Path=" + filetransferConfig.FileServerDirectory + "\t to" + filetransferConfig.SftpDirectory + " - Start");
                    StartSftpUploadSession(sessionOptions, filetransferConfig);
                    Program.LogInformation("\nUpload from Path=" + filetransferConfig.FileServerDirectory + "\t to" + filetransferConfig.SftpDirectory + " - End");
                }
                catch (Exception ex)
                {
                    Program.LogInformation("Upload from path from: " + filetransferConfig.FileServerDirectory + "\t To: " + filetransferConfig.SftpDirectory + "\t***Failed***");
                    Program.LogInformation(ex.Message + "\n" + ex.StackTrace);
                }
            });
            return true;
        }
        void StartSftpDownloadSession(SessionOptions sessionOptions, FileTransferModel filetransferConfig)
        {
            using (Session session = new Session())
            {
                session.Open(sessionOptions);
                RemoteDirectoryInfo remoteDirectoryInfo = session.ListDirectory(filetransferConfig.SftpDirectory);
                Parallel.ForEach(remoteDirectoryInfo.Files, (remoteFileInfo) =>
                {
                    string destinationUncPathWithFileName = "";

                    //ignore if subdirectory or parent directory
                    if (remoteFileInfo.IsDirectory == true)
                        return;

                    //skip if filename ends with .filepart. ie, file is partially uploaded or uploading
                    if (true == remoteFileInfo.Name.EndsWith(".filepart"))
                        return;

                    //when same filename already exist on destination path, transfer file with added hh-mm-sec-milliseconds end of file
                    if (true == IsSameFileNameExistOnDestinationFileServerPath(remoteFileInfo.Name, filetransferConfig.FileServerDirectory))
                    {
                        string ext = Path.GetExtension(remoteFileInfo.Name);
                        string filenameWithoutExt = Path.GetFileNameWithoutExtension(remoteFileInfo.Name);
                        filenameWithoutExt += " " + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + DateTime.Now.Millisecond;
                        destinationUncPathWithFileName = filetransferConfig.FileServerDirectory + filenameWithoutExt + ext;
                        Program.LogInformation("Destination path has same FileName=" + remoteFileInfo.Name + "********Renaming to= " + filenameWithoutExt + ext);
                    }
                    else
                    {
                        destinationUncPathWithFileName = filetransferConfig.FileServerDirectory + remoteFileInfo.Name;
                    }

                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;
                    TransferOperationResult transferResult;
                    Program.LogInformation("Copying from= " + filetransferConfig.SftpDirectory + remoteFileInfo.Name + "\t To= " + destinationUncPathWithFileName);
                    transferResult = session.GetFiles(filetransferConfig.SftpDirectory + remoteFileInfo.Name, destinationUncPathWithFileName, true, transferOptions);

                    transferResult.Check(); // Throw on any error
                }
                );
            }
        }
        void StartSftpUploadSession(SessionOptions sessionOptions, FileTransferModel filetransferConfig)
        {
            using (Session session = new Session())
            {
                session.Open(sessionOptions);
                string[] fileNames = Directory.GetFiles(filetransferConfig.FileServerDirectory);
                Parallel.ForEach(fileNames, (sourceFileNameWithPath) =>
                 {
                     string destinationSftpPathWithFileName = "";
                     string sourceFileName = Path.GetFileName(sourceFileNameWithPath);

                    //when same filename already exist on destination path, transfer file with added hh-mm-sec-milliseconds end of file
                    if (true == IsSameFileNameExistOnDestinationSftpServerPath(session, sourceFileName, filetransferConfig.SftpDirectory))
                     {
                         string ext = Path.GetExtension(sourceFileName);
                         string filenameWithoutExt = Path.GetFileNameWithoutExtension(sourceFileName);
                         filenameWithoutExt += " " + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + DateTime.Now.Millisecond;
                         destinationSftpPathWithFileName = filetransferConfig.FileServerDirectory + filenameWithoutExt + ext;
                         Program.LogInformation("Destination path has same FileName=" + sourceFileName + "********Renaming to= " + filenameWithoutExt + ext);
                     }
                     else
                     {
                         destinationSftpPathWithFileName = filetransferConfig.SftpDirectory + sourceFileName;
                     }

                     TransferOptions transferOptions = new TransferOptions();
                     transferOptions.TransferMode = TransferMode.Binary;
                     TransferOperationResult transferResult;
                     Program.LogInformation("Copying from= " + filetransferConfig.FileServerDirectory + sourceFileName + "\t To= " + destinationSftpPathWithFileName);
                     transferResult = session.PutFiles(filetransferConfig.FileServerDirectory + sourceFileName, destinationSftpPathWithFileName, true, transferOptions);

                     transferResult.Check(); // Throw on any error
                });
            }
        }
        bool IsSameFileNameExistOnDestinationFileServerPath(string sourceFileName, string destinationUncPath)
        {
            return File.Exists(destinationUncPath + sourceFileName);

        }
        bool IsSameFileNameExistOnDestinationSftpServerPath(Session session, string sourceFileName, string destinationSftpPath)
        {
            return session.FileExists(destinationSftpPath + sourceFileName);
        }
    }
}
