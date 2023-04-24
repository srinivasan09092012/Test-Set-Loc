using EnvDTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPPLocalNugetMgrVSIXProject.HPP
{
    internal static class FileUtility
    {
        public static void OpenFolderWithFileExplorer(string folderPath)
        {
            if (!string.IsNullOrEmpty(folderPath))
            {
                string cmd = "explorer.exe";
                string arg = "/e, " + folderPath;
                System.Diagnostics.Process.Start(cmd, arg);
            }
        }

        public static void OpenLocalPackageFolder(Project project)
        {
            string folderToOpen = null;

            if (Directory.Exists(project.LocalPackageLibFolder))
            {
                folderToOpen = project.LocalPackageLibFolder;
            }
            else if (Directory.Exists(project.LocalPackageFolder))
            {
                folderToOpen = project.LocalPackageFolder;
            }
            else if (Directory.Exists(project.LocalPackageProjectFolder))
            {
                folderToOpen = project.LocalPackageProjectFolder;
            }
            else if (Directory.Exists(project.LocalPackagesFolder))
            {
                folderToOpen = project.LocalPackagesFolder;
            }

            if (!string.IsNullOrEmpty(folderToOpen))
            {
                OpenFolderWithFileExplorer(folderToOpen);
            }
        }

        public static bool DeleteFolder(string folderPath)
        {
            bool result = false;
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
                if (!Directory.Exists(folderPath))
                {
                    result = true;
                }
            }

            return result;
        }

        public static void CopyFiles(string sourceFolder, string targetFolder)
        {
            if (Directory.Exists(sourceFolder) && Directory.Exists(targetFolder))
            {
                var sourceFilePathList = Directory.GetFiles(sourceFolder).ToList<string>();
                foreach (var sourceFilePath in sourceFilePathList)
                {
                    if (!sourceFilePath.EndsWith(".json"))
                    {
                        var sourceFileName = System.IO.Path.GetFileName(sourceFilePath);
                        var targetFilePath = Path.Combine(targetFolder, sourceFileName);
                        System.IO.File.Copy(sourceFilePath, targetFilePath, true);
                    }
                }
            }
        }
    }
}
