using Common.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace Controller.IO
{
    public class BackupHelper
    {
        private DirectoryInfo webSourceDirInfo;
        private DirectoryInfo webTargetDirInfo;
        private DirectoryInfo bootStrapComponents;
        private readonly ILogger backupLoggerEngine;

        public BackupHelper(string webSourcePath, string webTargetPath, ILogger loggerEngine)
        {
            backupLoggerEngine = loggerEngine;
            webSourceDirInfo = new DirectoryInfo(webSourcePath);
            webTargetDirInfo = new DirectoryInfo(webTargetPath);
            bootStrapComponents = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Common.Constants.WebSolutionStructure.Folders.BootStrap);
        }

        public void CopyTargetSite()
        {
            //TODO: failure on this method should stop the entire process for current setting file
            this.CopyFolderAndContent(webSourceDirInfo, webTargetDirInfo.FullName);
            backupLoggerEngine.writeEntry(string.Format("Checkpoint 1 : Default SandCastle Sources \n\nFrom: {0} \nTo: {1}", webSourceDirInfo.FullName, webTargetDirInfo.FullName), LogginSeetings.LevelType.InformationApplication,1043,1);

            this.CopyFolderAndContent(bootStrapComponents, webTargetDirInfo.FullName);
            backupLoggerEngine.writeEntry(string.Format("Checkpoint 2 : Boostrap Components \n\nFrom: {0} \nTo: {1}", bootStrapComponents.FullName, webTargetDirInfo.FullName), LogginSeetings.LevelType.InformationApplication,1043,1);

            foreach (FileInfo file in webSourceDirInfo.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                copyFile(file.FullName, webTargetDirInfo.FullName + @"\" + file.Name);
            }
        }

        public void CopyFolderAndContent(DirectoryInfo rootDirectory, string directoryTarget)
        {
            foreach (DirectoryInfo directory in rootDirectory.EnumerateDirectories())
            {
                var subDirectoryInfo = createDirectory(directoryTarget + @"\" + directory.Name);

                if (subDirectoryInfo != null)
                {
                    foreach (FileInfo file in directory.GetFiles("*.*", SearchOption.TopDirectoryOnly))
                    {
                        copyFile(file.FullName, subDirectoryInfo.FullName + @"\" + file.Name);
                    }

                    CopyFolderAndContent(directory, subDirectoryInfo.FullName);
                }
            }
        }

        private DirectoryInfo createDirectory(string path)
        {
            try
            {
                return Directory.CreateDirectory(path);
            }
            catch (Exception exe)
            {
                backupLoggerEngine.writeEntry(string.Format("Folder {0} could not be created \nError: {1}", path, exe.Message), LogginSeetings.LevelType.ErrorApplication,1011,1);
                return null;
            }
        }

        private void copyFile(string source, string destination)
        {
            try
            {
                File.Copy(source, destination, true);
            }
            catch (Exception exe)
            {
                backupLoggerEngine.writeEntry(string.Format("File {0} could not be copied to {1} \nError: {2}", source,destination, exe.Message), LogginSeetings.LevelType.ErrorApplication,1011,1);
            }
        }
    }
}
