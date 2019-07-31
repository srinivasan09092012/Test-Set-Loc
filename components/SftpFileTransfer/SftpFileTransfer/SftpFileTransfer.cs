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
                if (false == filetransferConfig.FileServerPathWithoutFileName.EndsWith(@"\"))
                    filetransferConfig.FileServerPathWithoutFileName += @"\";
                if (false == filetransferConfig.SftpPathWithoutFileName.EndsWith(@"/"))
                    filetransferConfig.SftpPathWithoutFileName += @"/";
                try
                {
                    Program.LogInformation("\nDownload from Path=" + filetransferConfig.SftpPathWithoutFileName + "\t to" + filetransferConfig.FileServerPathWithoutFileName + " - Start");
                    StartSftpDownloadSession(sessionOptions, filetransferConfig);
                    Program.LogInformation("\nDownload from Path=" + filetransferConfig.SftpPathWithoutFileName + "\t to" + filetransferConfig.FileServerPathWithoutFileName + " - End");
                }
                catch (Exception ex)
                {
                    Program.LogInformation("Download from: " + filetransferConfig.SftpPathWithoutFileName + "\t To: " + filetransferConfig.FileServerPathWithoutFileName + "\t***Failed***");
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
                if (false == filetransferConfig.FileServerPathWithoutFileName.EndsWith(@"\"))
                    filetransferConfig.FileServerPathWithoutFileName += @"\";
                if (false == filetransferConfig.SftpPathWithoutFileName.EndsWith(@"/"))
                    filetransferConfig.SftpPathWithoutFileName += @"/";
                try
                {
                    Program.LogInformation("\nUpload from Path=" + filetransferConfig.FileServerPathWithoutFileName + "\t to" + filetransferConfig.SftpPathWithoutFileName + " - Start");
                    StartSftpUploadSession(sessionOptions, filetransferConfig);
                    Program.LogInformation("\nUpload from Path=" + filetransferConfig.FileServerPathWithoutFileName + "\t to" + filetransferConfig.SftpPathWithoutFileName + " - End");
                }
                catch (Exception ex)
                {
                    Program.LogInformation("Upload from path from: " + filetransferConfig.FileServerPathWithoutFileName + "\t To: " + filetransferConfig.SftpPathWithoutFileName + "\t***Failed***");
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
                RemoteDirectoryInfo remoteDirectoryInfo = session.ListDirectory(filetransferConfig.SftpPathWithoutFileName);
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
                    if (true == IsSameFileNameExistOnDestinationFileServerPath(remoteFileInfo.Name, filetransferConfig.FileServerPathWithoutFileName))
                    {
                        string ext = Path.GetExtension(remoteFileInfo.Name);
                        string filenameWithoutExt = Path.GetFileNameWithoutExtension(remoteFileInfo.Name);
                        filenameWithoutExt += " " + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + DateTime.Now.Millisecond;
                        destinationUncPathWithFileName = filetransferConfig.FileServerPathWithoutFileName + filenameWithoutExt + ext;
                        Program.LogInformation("Destination path has same FileName=" + remoteFileInfo.Name + "********Renaming to= " + filenameWithoutExt + ext);
                    }
                    else
                    {
                        destinationUncPathWithFileName = filetransferConfig.FileServerPathWithoutFileName + remoteFileInfo.Name;
                    }

                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;
                    TransferOperationResult transferResult;
                    Program.LogInformation("Copying from= " + filetransferConfig.SftpPathWithoutFileName + remoteFileInfo.Name + "\t To= " + destinationUncPathWithFileName);
                    transferResult = session.GetFiles(filetransferConfig.SftpPathWithoutFileName + remoteFileInfo.Name, destinationUncPathWithFileName, true, transferOptions);

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
                string[] fileNames = Directory.GetFiles(filetransferConfig.FileServerPathWithoutFileName);
                Parallel.ForEach(fileNames, (sourceFileNameWithPath) =>
                 {
                     string destinationSftpPathWithFileName = "";
                     string sourceFileName = Path.GetFileName(sourceFileNameWithPath);

                    //when same filename already exist on destination path, transfer file with added hh-mm-sec-milliseconds end of file
                    if (true == IsSameFileNameExistOnDestinationSftpServerPath(session, sourceFileName, filetransferConfig.SftpPathWithoutFileName))
                     {
                         string ext = Path.GetExtension(sourceFileName);
                         string filenameWithoutExt = Path.GetFileNameWithoutExtension(sourceFileName);
                         filenameWithoutExt += " " + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + DateTime.Now.Millisecond;
                         destinationSftpPathWithFileName = filetransferConfig.FileServerPathWithoutFileName + filenameWithoutExt + ext;
                         Program.LogInformation("Destination path has same FileName=" + sourceFileName + "********Renaming to= " + filenameWithoutExt + ext);
                     }
                     else
                     {
                         destinationSftpPathWithFileName = filetransferConfig.SftpPathWithoutFileName + sourceFileName;
                     }

                     TransferOptions transferOptions = new TransferOptions();
                     transferOptions.TransferMode = TransferMode.Binary;
                     TransferOperationResult transferResult;
                     Program.LogInformation("Copying from= " + filetransferConfig.FileServerPathWithoutFileName + sourceFileName + "\t To= " + destinationSftpPathWithFileName);
                     transferResult = session.PutFiles(filetransferConfig.FileServerPathWithoutFileName + sourceFileName, destinationSftpPathWithFileName, true, transferOptions);

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
