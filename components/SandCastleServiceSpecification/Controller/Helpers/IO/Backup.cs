using System;
using System.IO;
using System.Reflection;

namespace APISvcSpec.IO
{
    public class Backup
    {
        public Backup()
        {
            webSourceDirInfo = new DirectoryInfo(Common.Constant.WebSolutionStructure.PathAndRoutes.webRootSource);
            webTargetDirInfo = new DirectoryInfo(Common.Constant.WebSolutionStructure.PathAndRoutes.webRootTarget + Common.Constant.WebSolutionStructure.Folders.outPutFolderName);
            bootStrapComponents = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Common.Constant.WebSolutionStructure.Folders.BootStrap);
        }

         public Backup(string webSourcePath, string webTargetPath)
        {
            webSourceDirInfo = new DirectoryInfo(webSourcePath);
            webTargetDirInfo = new DirectoryInfo(webTargetPath);
            bootStrapComponents = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Common.Constant.WebSolutionStructure.Folders.BootStrap);
        }

        private static DirectoryInfo webSourceDirInfo; // = new DirectoryInfo(Common.Constant.WebSolutionStructure.PathAndRoutes.webRootSource);
        private static DirectoryInfo webTargetDirInfo;//  = new DirectoryInfo(Common.Constant.WebSolutionStructure.PathAndRoutes.webRootTarget + Common.Constant.WebSolutionStructure.Folders.outPutFolderName);
        private static DirectoryInfo bootStrapComponents;// = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Common.Constant.WebSolutionStructure.Folders.BootStrap);

        public  string WebSourceDirInfo { get => webSourceDirInfo.FullName; }
        public  string WebTargetDirInfo { get => webTargetDirInfo.FullName;  }

        public void BackUpWebSite(bool zipBackup = false)
        {
            
            if (!webSourceDirInfo.Exists)
            {
                Console.WriteLine("Source web site not found. aborting process");
                ////logging, abort the process due lack of source folder and files
            }
            
            Console.WriteLine("- Step 1: Starting web site backup" + DateTime.UtcNow );
            Console.WriteLine("");

            if (webTargetDirInfo.Exists)
            {
                Directory.Delete(webTargetDirInfo.FullName, true);
            }

            CopyFolderAndContent(webSourceDirInfo, webTargetDirInfo.FullName);

            foreach (FileInfo file in webSourceDirInfo.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                copyFile(file.FullName, webTargetDirInfo.FullName + @"\" + file.Name);
            }

            Console.WriteLine("- Step 1: Done Backup completed " + DateTime.UtcNow);

            Console.WriteLine("");
            Console.WriteLine("- Step 2: Loading boostrap components");
            Console.WriteLine("");

            CopyFolderAndContent(bootStrapComponents, webTargetDirInfo.FullName);
            Console.WriteLine("- Step 2: Done boostrap components Loaded");
        }

        public void CopyFolderAndContent(DirectoryInfo rootDirectory, string directoryTarget)
        {
            foreach (DirectoryInfo directory in rootDirectory.EnumerateDirectories())
            {
                var subDirectoryInfo = createDirectory(directoryTarget + @"\" + directory.Name);

                foreach (FileInfo file in directory.GetFiles("*.*", SearchOption.TopDirectoryOnly))
                {
                    copyFile(file.FullName, subDirectoryInfo.FullName + @"\" + file.Name);
                }

                CopyFolderAndContent(directory, subDirectoryInfo.FullName);
            }
        }

        /// <summary>
        /// Copy File from one location to another, handle exceptions
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void copyFile(string source, string target)
        {
            try
            {
                File.Copy(source, target);
            }
            catch(Exception exe)
            {
                //TODO:logging
                ///abort process
            }

        }

        /// <summary>
        /// create directory folder and handle exceptions
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private DirectoryInfo createDirectory(string directory)
        {
            try
            {
                return Directory.CreateDirectory(directory);
            }
            catch (DirectoryNotFoundException exe) //// Folder not exist, incorrect setting in Admin Module
            {
                //TODO:logging
                //////abort process
                return null;
            }
            catch (IOException exe)//// File in use / Drive not exist - Incorrect Setting in Admin Module.
            {
                //TODO:logging
                //////abort process
                return null;
            }
        }
    }
}
