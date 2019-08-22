using System;
using System.IO;
using System.Reflection;

namespace APISvcSpec.IO
{
    public class Backup
    {
        private static DirectoryInfo webSourceDirInfo;
        private static DirectoryInfo webTargetDirInfo;
        private static DirectoryInfo bootStrapComponents;

        public Backup()
        {
            webSourceDirInfo = new DirectoryInfo(Common.Constants.WebSolutionStructure.PathAndRoutes.webRootSource);
            webTargetDirInfo = new DirectoryInfo(Common.Constants.WebSolutionStructure.PathAndRoutes.webRootTarget + Common.Constants.WebSolutionStructure.Folders.outPutFolderName);
            bootStrapComponents = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Common.Constants.WebSolutionStructure.Folders.BootStrap);
        }

         public Backup(string webSourcePath, string webTargetPath)
        {
            webSourceDirInfo = new DirectoryInfo(webSourcePath);
            webTargetDirInfo = new DirectoryInfo(webTargetPath);
            bootStrapComponents = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Common.Constants.WebSolutionStructure.Folders.BootStrap);
        }

        public void BackUpWebSite(bool zipBackup = false)
        {
            Console.WriteLine("Starting Source Web Site Backup on: " + DateTime.UtcNow);
            Console.WriteLine("");

            if (!webSourceDirInfo.Exists)
            {
                Console.WriteLine("Source web site not found. aborting process");
                ////TODO: logging, abort the process due lack of source folder and files
                ///TODO: LOGGIN NOT PRESENT IN THE PROCESS, MUST HAVE ONE
            }

            if (webTargetDirInfo.Exists)
            {
                Directory.Delete(webTargetDirInfo.FullName, true);
            }

            this.CopyFolderAndContent(webSourceDirInfo, webTargetDirInfo.FullName);

            foreach (FileInfo file in webSourceDirInfo.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                copyFile(file.FullName, webTargetDirInfo.FullName + @"\" + file.Name);
            }

            Console.WriteLine(".....done: " + DateTime.UtcNow);
            Console.WriteLine("");

            Console.WriteLine("Loading boostrap components. " + DateTime.UtcNow);
            Console.WriteLine("");

            this.CopyFolderAndContent(bootStrapComponents, webTargetDirInfo.FullName);
            Console.WriteLine("....done." + DateTime.UtcNow);
            Console.WriteLine("");
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
            catch
            { }
        }

        /// <summary>
        /// create directory folder and handle exceptions
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private DirectoryInfo createDirectory(string directory)
        {
            return Directory.CreateDirectory(directory);
        }
    }
}
