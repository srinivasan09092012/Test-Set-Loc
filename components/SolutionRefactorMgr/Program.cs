using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using HPE.HSP.UA3.Core.API.Logger.Loggers;
using SolutionRefactorMgr.Domain;
using SolutionRefactorMgr.Utilities;
using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace SolutionRefactorMgr
{
    public class Program
    {
        private static CoreLogger logger = new CoreLogger();
        private static RefactorConfig refactorConfig = null;
        private static TfsTeamProjectCollection tpc = null;
        private static Workspace workspace = null;

        public static void Main(string[] args)
        {
            try
            {
                LogMessage(0, "Process started");
                LoadConfigration();
                InitializeTfs();
                Refactor();
                LogMessage(0, "Process complete");
            }
            catch (Exception ex)
            {
                LogMessage(0, "Process halted with fatal exception. See log for details.", ex);
            }
            finally
            {
                FinalizeTfs();
                Console.Write("Press any key to close");
                Console.ReadKey();
            }
        }

        private static void FinalizeTfs()
        {
            if (workspace != null)
            {
                workspace = null;
            }
        }

        private static bool IsBinaryFile(string fileContents)
        {
            bool isBinary = false;

            ////Skip check due to unknown special character types to exclude and the file type configuration should 
            //// limit chanegs to non-binary files.

            ////for (int i = 0; i < fileContents.Length; i++)
            ////{
            ////    // 160 is some sort of alternative space
            ////    // 169 is the copyright "©" symbol (That's in all files)
            ////    if (fileContents[i] > 127 & fileContents[i] != 160 & fileContents[i] != 169 & fileContents[i] != 8211)
            ////    {
            ////        isBinary = true;
            ////        break;
            ////    }
            ////}

            return isBinary;
        }

        private static void InitializeTfs()
        {
            if (refactorConfig.UseSourceControl)
            {
                LogMessage(0, string.Format("Initializing connection to TFS..."));
                tpc = new TfsTeamProjectCollection(new Uri(refactorConfig.TfsServer));
                VersionControlServer versionControl = tpc.GetService<VersionControlServer>();
                versionControl.NonFatalError += OnNonFatalError;
                versionControl.Getting += OnGetting;
                versionControl.BeforeCheckinPendingChange += OnBeforeCheckinPendingChange;
                versionControl.NewPendingChange += OnNewPendingChange;
                LogMessage(0, string.Format("NOTE: IF YOU GET A MSG THAT YOU DON'T HAVE PERMISSIONS TO CHECKOUT, CHANGE THE TFSWORKSPACE ELEMENT IN THE REFACTOR.CONFIG TO YOUR UA3 WORKSPACE NAME."));
                workspace = versionControl.QueryWorkspaces(refactorConfig.TfsWorkspace, versionControl.AuthorizedUser, Environment.MachineName)[0];
                LogMessage(0, string.Format("Successfully connected to TFS"));
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

        private static void OnNonFatalError(Object sender, ExceptionEventArgs e)
        {
            if (e.Exception != null)
            {
                LogMessage(1, "Non-fatal exception: " + e.Exception.Message);
            }
            else
            {
                LogMessage(1, "Non-fatal failure: " + e.Failure.Message);
            }
        }

        private static void OnGetting(Object sender, GettingEventArgs e)
        {
            LogMessage(1, "Getting: " + e.TargetLocalItem + ", status: " + e.Status);
        }

        private static void OnBeforeCheckinPendingChange(Object sender, ProcessingChangeEventArgs e)
        {
            LogMessage(1, "Checking in " + e.PendingChange.LocalItem);
        }

        private static void OnNewPendingChange(Object sender, PendingChangeEventArgs e)
        {
            LogMessage(1, "Pending " + PendingChange.GetLocalizedStringForChangeType(e.PendingChange.ChangeType) + " on " + e.PendingChange.LocalItem);
        }

        private static void Refactor()
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
                        RefactorModule(module, Enumerations.ProjectTypes.EAI);
                        RefactorModule(module, Enumerations.ProjectTypes.UX);
                        break;

                    default:
                        RefactorModule(module, module.Type);
                        break;
                }
            }

            foreach (PackageConfig package in refactorConfig.PackageConfigs)
            {
                string version = string.IsNullOrEmpty(package.QualifierVersion) ? string.Empty : string.Format("' version: '{0}", package.QualifierVersion);
                LogMessage(0, string.Format("Refactoring started for packages starting with: '{0}'.", package.QualifierPrefix + version));
                RefactorPackages(package, refactorConfig.SourceDir + "Packages\\_Source\\");
                RefactorPackages(package, refactorConfig.SourceDir + "Packages\\");
            }
        }

        private static void RefactorDirectory(string source, string origDest, string newDest, bool recursive, int level)
        {
            if(Directory.Exists(source))
            {
                bool processDirectory = true;
                LogMessage(level, string.Format("Refactoring directory '{0}' to '{1}'", source, origDest));
                level++;

                if (refactorConfig.EditMode == Enumerations.EditModeTypes.Copy)
                {
                    if (string.Compare(source, newDest, false) != 0)
                    {
                        if (Directory.Exists(newDest))
                        {
                            TfsUndo(newDest);
                            Directory.Delete(newDest, true);
                        }
                        Directory.CreateDirectory(newDest);
                        TfsPendAdd(newDest);
                    }
                    else
                    {
                        processDirectory = false;
                        LogMessage(level, string.Format("Skipping copy for dir '{0}' to '{1}' due to same name.", origDest, newDest));
                    }
                }
                else
                {
                    if (string.Compare(source, newDest, false) != 0)
                    {
                        TfsPendRename(source, newDest);
                        source = newDest;
                    }
                }

                if (processDirectory)
                {
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
                                string newDirName = subdir.Name;
                                if (refactorConfig.IncludeFolderNames)
                                {
                                    newDirName = RefactorFileName(subdir.Name);
                                }

                                string origSubDirDest = Path.Combine(newDest, subdir.Name);
                                string newSubDirDest = Path.Combine(newDest, newDirName);
                                RefactorDirectory(subdir.FullName, origSubDirDest, newSubDirDest, recursive, level);
                            }
                        }
                    }
                }
            }
        }

        private static void RefactorFile(FileInfo file, string dest, int level)
        {
            bool fileQualifies = refactorConfig.FileTypes.Find(f => f == file.Extension.ToLower()) != null;
            if (fileQualifies)
            {
                string fileName = file.Name;
                string newFileName = fileName;
                if (refactorConfig.IncludeFileNames)
                {
                    newFileName = RefactorFileName(file.Name);
                }

                string origPath = Path.Combine(dest, file.Name);
                string newPath = Path.Combine(dest, newFileName);

                string fileContents = File.ReadAllText(file.FullName);
                string newFileContents = fileContents;
                bool isBinary = IsBinaryFile(fileContents);
                bool contentsChanged = false;
                if (refactorConfig.IncludeFileContents && !isBinary)
                {
                    contentsChanged = RefactorFileContents(file.FullName, out newFileContents);
                }

                if (refactorConfig.EditMode == Enumerations.EditModeTypes.Copy)
                {
                    if (!contentsChanged)
                    {
                        File.Copy(file.FullName, newPath);
                        LogMessage(level, string.Format("Copying file '{0}'", file.Name));
                    }
                    else
                    {
                        File.WriteAllText(newPath, newFileContents);
                        LogMessage(level, string.Format("Refactoring file '{0}'", file.Name));
                    }
                    File.SetAttributes(newPath, File.GetAttributes(newPath) & ~FileAttributes.ReadOnly);
                    TfsPendAdd(newPath);
                }
                else
                {
                    if (string.Compare(fileName, newFileName, true) != 0)
                    {
                        TfsPendRename(origPath, newPath);
                        LogMessage(level, string.Format("Renaming file '{0}'", file.Name));
                    }

                    if (contentsChanged)
                    {
                        TfsPendEdit(newPath);
                        File.WriteAllText(newPath, newFileContents);
                        LogMessage(level, string.Format("Refactoring file '{0}'", file.Name));
                    }
                }
            }
            else
            {
                if (refactorConfig.EditMode == Enumerations.EditModeTypes.Copy)
                {
                    string newPath = Path.Combine(dest, file.Name);
                    File.Copy(file.FullName, newPath);
                    File.SetAttributes(newPath, File.GetAttributes(newPath) & ~FileAttributes.ReadOnly);
                    TfsPendAdd(newPath);
                    LogMessage(level, string.Format("Copying file '{0}'", file.Name));
                }
                else
                {
                    LogMessage(level, string.Format("Skipping file '{0}'", file.Name));
                }
            }
        }

        private static bool RefactorFileContents(string filePath, out string refactoredFileContents)
        {
            bool contentsChanged = false;
            refactoredFileContents = string.Empty;

            using (StreamReader reader = new StreamReader(filePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter writer = new StreamWriter(ms))
                    {
                        string line = null;
                        while ((line = reader.ReadLine()) != null)
                        {
                            bool deleteLine = false;
                            foreach (LineDelete lineDelete in refactorConfig.LineDeletes)
                            {
                                if (line.Contains(lineDelete.Contains))
                                {
                                    deleteLine = true;
                                    contentsChanged = true;
                                    break;
                                }
                            }

                            if (!deleteLine)
                            {
                                foreach (ReplacementString replacement in refactorConfig.ReplacementStrings)
                                {
                                    if (line.Contains(replacement.Qualifier) && line.Contains(replacement.From))
                                    {
                                        replacement.To = replacement.To.Replace("[NEWLINE]", Environment.NewLine);
                                        line = line.Replace(replacement.From, replacement.To);
                                        contentsChanged = true;
                                    }
                                }

                                writer.WriteLine(line);
                            }
                        }
                        writer.Flush();
                        ms.Position = 0;
                        using (StreamReader sr = new StreamReader(ms))
                        {
                            refactoredFileContents = sr.ReadToEnd();
                        }
                    }
                }
            }

            return contentsChanged;
        }

        private static string RefactorFileName(string origValue)
        {
            string newValue = origValue;

            foreach (ReplacementString replacement in refactorConfig.ReplacementStrings)
            {
                if (newValue.Contains(replacement.Qualifier) && newValue.Contains(replacement.From))
                {
                    newValue = newValue.Replace(replacement.From, replacement.To);
                }
            }

            return newValue;
        }

        private static void RefactorModule(ModuleConfig module, Enumerations.ProjectTypes type)
        {
            LogMessage(1, string.Format("Refactoring started for type: '{0}'", type.ToString()));
            string sourcePath = refactorConfig.SourceDir + module.Name + "\\" + module.Branch;
            if (type != Enumerations.ProjectTypes.NA)
            {
                sourcePath += "\\" + type.ToString();
            }
            string destPath = sourcePath;
            if (refactorConfig.EditMode == Enumerations.EditModeTypes.Copy)
            {
                destPath = refactorConfig.SourceDir + module.Name + "\\" + module.Branch;
                if (type != Enumerations.ProjectTypes.NA)
                {
                    destPath += "\\" + type.ToString();
                }
            }
            RefactorDirectory(sourcePath, destPath, destPath, true, 2);
        }

        private static void RefactorPackages(PackageConfig package, string sourcePath)
        {
            LogMessage(1, string.Format("Refactoring started for path: '{0}'", sourcePath));
            string destPath = sourcePath;

            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo subdir in dirs)
            {
                if(subdir.Name.ToLower().StartsWith(package.QualifierPrefix.ToLower()) &&
                    (string.IsNullOrEmpty(package.QualifierVersion) || subdir.Name.ToLower().Contains(package.QualifierVersion)))
                {
                    string newDirName = subdir.Name;
                    if (refactorConfig.IncludeFolderNames)
                    {
                        newDirName = RefactorFileName(subdir.Name);
                    }

                    RefactorDirectory(subdir.FullName, destPath + newDirName, destPath + newDirName, true, 2);
                    DirectoryInfo newPackageDir = new DirectoryInfo(destPath + newDirName);
                    if (package.ReformatSource)
                    {
                        ReformatPackageSource(newPackageDir, 2);
                    }

                    if (package.RebuildPackage)
                    {
                        RebuildNugetPackage(newPackageDir, 2);
                    }
                }
            }
        }

        private static void RebuildNugetPackage(DirectoryInfo packageDir, int level)
        {
            LogMessage(level, string.Format("Rebuilding package source: '{0}'", packageDir.FullName));
            FileInfo nuspecFile;
            FileInfo nupkgFile;
            FileInfo[] files = packageDir.GetFiles("*.nuspec");
            if (files.Length == 1)
            {
                nuspecFile = files[0];
            }
            else
            {
                throw new ApplicationException(string.Format("Unable to find nuspec file for package '{0}'.", packageDir.FullName));
            }

            files = packageDir.GetFiles("*.nupkg");
            if (files.Length == 1)
            {
                nupkgFile = files[0];
            }
            else
            {
                throw new ApplicationException(string.Format("Unable to find nupkg file for package '{0}'.", packageDir.FullName));
            }
            TfsPendEdit(nupkgFile.FullName);

            string command = string.Format("pack {0}", nuspecFile.FullName);
            using (Process cmd = new Process())
            {
                cmd.StartInfo.FileName = "NuGet.exe";
                cmd.StartInfo.Arguments = command;
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                LogMessage(level, cmd.StandardOutput.ReadToEnd());
            }
            string sourcePkg = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), nuspecFile.Name.Replace(".nuspec", ".nupkg"));
            if (File.Exists(nupkgFile.FullName))
            {
                File.Delete(nupkgFile.FullName);
            }

            File.Move(sourcePkg, nupkgFile.FullName);
        }

        private static void ReformatPackageSource(DirectoryInfo packageDir, int level)
        {
            LogMessage(level, string.Format("Reformating package source: '{0}'", packageDir.FullName));
            FileInfo nuspecFile;
            FileInfo[] files = packageDir.GetFiles("*.nuspec");
            if (files.Length == 1)
            {
                nuspecFile = files[0];
            }
            else
            {
                throw new ApplicationException(string.Format("Unable to find nuspec file for package '{0}'.", packageDir.FullName));
            }
            TfsPendEdit(nuspecFile.FullName);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(nuspecFile.FullName);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("ms", "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd");
            XmlNodeList assemblyFiles = xmlDoc.SelectNodes("//ms:file", nsmgr);
            if (assemblyFiles != null && assemblyFiles.Count > 0)
            {
                foreach(XmlNode assemblyFile in assemblyFiles)
                {
                    assemblyFile.Attributes["src"].Value = assemblyFile.Attributes["target"].Value;
                }
            }
            xmlDoc.Save(nuspecFile.FullName);
        }

        private static void TfsPendAdd(string path)
        {
            if(workspace != null)
            {
                workspace.PendAdd(path);
            }
        }

        private static void TfsPendDelete(string path)
        {
            if (workspace != null)
            {
                workspace.PendDelete(path);
            }
        }

        private static void TfsPendEdit(string path)
        {
            if (workspace != null)
            {
                workspace.PendEdit(path);
            }
        }

        private static void TfsPendRename(string oldPath, string newPath)
        {
            if (workspace != null)
            {
                workspace.PendRename(oldPath, newPath);
            }
        }

        private static void TfsUndo(string path)
        {
            if (workspace != null)
            {
                workspace.Undo(path);
            }
        }
    }
}
