using HPE.HSP.UA3.Core.API.Logger.Loggers;
using SolutionRefactorMgr.Domain;
using SolutionRefactorMgr.Utilities;
using System;
using System.IO;

namespace SolutionRefactorMgr
{
    public class Program
    {
        private static CoreLogger logger = new CoreLogger();
        private static RefactorConfig refactorConfig = null;

        public static void Main(string[] args)
        {
            try
            {
                LogMessage(0, "Process started");
                LoadConfigration();
                ProcessConfiguration();
                LogMessage(0, "Process complete");
            }
            catch (Exception ex)
            {
                LogMessage(0, "Process halted with fatal exception. See log for details.", ex);
            }
            finally
            {
                Console.Write("Press any key to close");
                Console.ReadKey();
            }
        }

        private static void LoadConfigration()
        {
            LogMessage(0, "Loading configuration");
            string filePath = AppDomain.CurrentDomain.BaseDirectory + Constants.ConfigFile;
            if (File.Exists(filePath))
            {
                string fileContents = File.ReadAllText(filePath);
                refactorConfig = Serializer.XmlDeserialize<RefactorConfig>(fileContents, string.Empty);
                LogMessage(0, "Validation configuration");
                refactorConfig.Validate();
            }
            else
            {
                throw new ApplicationException(string.Format("Configuration file '{0}' does not exist.", filePath));
            }
        }

        private static void LogMessage(int level, string msg)
        {
            string tabs = new string(' ', level * 2);
            logger.LogInformational(tabs + msg);
            Console.WriteLine(tabs + msg);
        }

        private static void LogMessage(int level, string msg, Exception ex)
        {
            string tabs = new string(' ', level * 2);
            logger.LogFatal(tabs + msg, ex);
            Console.WriteLine(tabs + msg);
        }

        private static void ProcessConfiguration()
        {
            foreach (ModuleConfig module in refactorConfig.ModuleConfigs)
            {
                LogMessage(0, string.Format("Refactoring started for module: '{0}' branch: '{1}'.", module.Name, module.Branch.ToString()));
                switch (module.Type)
                {
                    case Enumerations.ProjectTypes.All:
                        RefactorModule(module, Enumerations.ProjectTypes.API);
                        RefactorModule(module, Enumerations.ProjectTypes.BAS);
                        RefactorModule(module, Enumerations.ProjectTypes.Batch);
                        RefactorModule(module, Enumerations.ProjectTypes.UX);
                        break;

                    default:
                        RefactorModule(module, module.Type);
                        break;
                }
            }
        }

        private static void RefactorDirectory(string source, string origDest, string newDest, bool recursive, int level)
        {
            LogMessage(level, string.Format("Refactoring directory '{0}' to '{1}'", source, origDest));
            level++;

            if (refactorConfig.EditMode == Enumerations.EditModeTypes.Copy)
            {
                if (Directory.Exists(newDest))
                {
                    Directory.Delete(newDest, true);
                }
                Directory.CreateDirectory(newDest);
            }
            else
            {
                if (string.Compare(origDest, newDest, false) != 0)
                {
                    //Rename directory in TFS
                }
            }

            DirectoryInfo dir = new DirectoryInfo(source);
            DirectoryInfo[] dirs = dir.GetDirectories();

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                RefactorFile(file, newDest, level);
            }

            if (recursive)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    LogMessage(level, string.Format("Refactoring subdirectory '{0}'", subdir.Name));
                    if (!subdir.Name.Contains("bin") 
                        && !subdir.Name.Contains("obj") 
                        && !subdir.Name.Contains(".vs") 
                        && !subdir.Name.Contains("TestResults") 
                        && !subdir.Name.Contains("Packages")
                        )
                    {
                        string newDirName = refactorConfig.RefactorFileName(subdir.Name);
                        string origSubDirDest = Path.Combine(newDest, subdir.Name);
                        string newSubDirDest = Path.Combine(newDest, newDirName);
                        RefactorDirectory(subdir.FullName, origSubDirDest, newSubDirDest, recursive, level);
                    }
                }
            }
        }

        private static void RefactorFile(FileInfo file, string dest, int level)
        {
            if (file.Extension.ToLower() != ".cache"
                && file.Extension.ToLower() != ".vspscc"
                && file.Extension.ToLower() != ".vssscc"
                )
            {
                LogMessage(level, string.Format("Refactoring file '{0}'", file.Name));
                string fileName = file.Name;
                string fileContents = File.ReadAllText(file.FullName);
                string newFileName = string.Empty;
                string newFileContents = string.Empty;
                bool fileQualifies = false;
                switch (file.Extension.ToLower())
                {
                    case ".asax":
                    case ".aspx":
                    case ".bat":
                    case ".cd":
                    case ".classdiagram":
                    case ".cmd":
                    case ".config":
                    case ".cs":
                    case ".cshtml":
                    case ".csproj":
                    case ".css":
                    case ".disco":
                    case ".js":
                    case ".layerdiagram":
                    case ".modelproj":
                    case ".runsettings":
                    case ".pubxml":
                    case ".settings":
                    case ".sln":
                    case ".snk":
                    case ".svc":
                    case ".svcinfo":
                    case ".svcmap":
                    case ".testsettings":
                    case ".uml":
                    case ".user":
                    case ".wsdl":
                    case ".xsd":
                    case ".xml":
                        fileQualifies = true;
                        break;
                }

                if (fileQualifies)
                {
                    newFileName = refactorConfig.RefactorFileName(file.Name);
                    string newPath = Path.Combine(dest, newFileName);
                    newFileContents = refactorConfig.RefactorFileContents(file.FullName);

                    if (refactorConfig.EditMode == Enumerations.EditModeTypes.Copy)
                    {
                        File.WriteAllText(newPath, newFileContents);
                        File.SetAttributes(newPath, File.GetAttributes(newPath) & ~FileAttributes.ReadOnly);
                    }
                    else
                    {
                        //Check out file from TFS

                        if (string.Compare(fileName, newFileName, true) != 0)
                        {
                            //Rename the file in TFS
                        }

                        if (string.Compare(fileContents, newFileContents, false) != 0)
                        {
                            File.WriteAllText(newFileName, newFileContents);
                        }
                    }
                }
                else
                {
                    if (refactorConfig.EditMode == Enumerations.EditModeTypes.Copy)
                    {
                        string newPath = Path.Combine(dest, file.Name);
                        File.WriteAllText(newPath, fileContents);
                        File.SetAttributes(newPath, File.GetAttributes(file.FullName) & ~FileAttributes.ReadOnly);
                    }
                }
            }
        }

        private static void RefactorModule(ModuleConfig module, Enumerations.ProjectTypes type)
        {
            LogMessage(1, string.Format("Refactoring started for type: '{0}'", type.ToString()));
            string sourcePath = refactorConfig.SourceDir + module.Name + "\\" + module.Branch + "\\" + type.ToString();
            string destPath = sourcePath;
            if (refactorConfig.EditMode == Enumerations.EditModeTypes.Copy)
            {
                destPath = refactorConfig.SourceDir + module.Name + "\\" + module.Branch + "_Copy\\" + type.ToString();
            }
            RefactorDirectory(sourcePath, destPath, destPath, true, 2);
        }
    }
}
