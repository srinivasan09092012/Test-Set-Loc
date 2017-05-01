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
                LogMessage(0, "Process ended");
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

        private static void RefactorDirectory(string source, string dest, bool recursive, int level)
        {
            LogMessage(level, string.Format("Refactoring directory '{0}' to '{1}'", source, dest));
            level++;
            DirectoryInfo dir = new DirectoryInfo(source);
            DirectoryInfo[] dirs = dir.GetDirectories();
            if (Directory.Exists(dest))
            {
                Directory.Delete(dest, true);
            }
            Directory.CreateDirectory(dest);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower() != ".cache"
                    && file.Extension.ToLower() != ".vspscc"
                    && file.Extension.ToLower() != ".vssscc"
                    )
                {
                    LogMessage(level, string.Format("Refactoring file '{0}'", file.Name));
                    string newFileName = refactorConfig.Refactor(file.Name);
                    string tempPath = Path.Combine(dest, newFileName);
                    file.CopyTo(tempPath, true);
                    File.SetAttributes(tempPath, File.GetAttributes(tempPath) & ~FileAttributes.ReadOnly);
                    switch (file.Extension.ToLower())
                    {
                        case ".asax":
                        case ".bat":
                        case ".cd":
                        case ".cmd":
                        case ".config":
                        case ".cs":
                        case ".cshtml":
                        case ".csproj":
                        case ".css":
                        case ".js":
                        case ".runsettings":
                        case ".sln":
                        case ".svcinfo":
                        case ".svcmap":
                        case ".testsettings":
                        case ".wsdl":
                        case ".xsd":
                        case ".xml":
                            string contents = refactorConfig.Refactor(File.ReadAllText(tempPath));
                            File.WriteAllText(tempPath, contents);
                            break;
                    }
                }
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
                        string newDirName = refactorConfig.Refactor(subdir.Name);
                        string tempPath = Path.Combine(dest, newDirName);
                        RefactorDirectory(subdir.FullName, tempPath, recursive, level);
                    }
                }
            }
        }

        private static void RefactorModule(ModuleConfig module, Enumerations.ProjectTypes type)
        {
            LogMessage(1, string.Format("Refactoring started for type: '{0}'", type.ToString()));
            string sourcePath = refactorConfig.SourceDir + module.Name + "\\" + module.Branch + "\\" + type.ToString();
            string destPath = refactorConfig.SourceDir + module.Name + "\\" + module.Branch + "_Refactored\\" + type.ToString();
            RefactorDirectory(sourcePath, destPath, true, 2);
        }
    }
}
