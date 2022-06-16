using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using HPE.HSP.UA3.Core.API.Logger.Loggers;
using SolutionRefactorMgr.Domain;
using SolutionRefactorMgr.Domain.WebAppConfig;
using SolutionRefactorMgr.Domain.CsProj;
using SolutionRefactorMgr.Utilities;
using System;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace SolutionRefactorMgr
{
    public class Program
    {
        private static int pendAddCount = 0;
        private static int pendDeleteCount = 0;
        private static int pendEditCount = 0;
        private static int pendRenameCount = 0;
        private static int exceptionsWritten = 0;
        private static int retryCount = 10;

        private static CoreLogger logger = new CoreLogger();
        private static RefactorConfig refactorConfig = null;
        private static TfsTeamProjectCollection tpc = null;
        private static Workspace workspace = null;
        private static TextWriter tw = null;
        private static PendingChange[] allPendingChangesinWorkspace = null;

        public static void Main(string[] args)
        {
            string exceptionSummaryDirectory = Directory.GetCurrentDirectory() + "\\Logs";
            string exceptionSummaryFile = Directory.GetCurrentDirectory() + "\\Logs\\ExceptionSummary.txt";

            if (!Directory.Exists(exceptionSummaryDirectory))
            {
                Directory.CreateDirectory(exceptionSummaryDirectory);
            }

            tw = new StreamWriter(exceptionSummaryFile);

            try
            {
                LogMessage(0, "Process started");
                LoadConfiguration();
                InitializeTfs();
                Refactor();
                ReportCounts();
                LogMessage(0, "Process complete");
                LogMessage(0, " ");
            }
            catch (Exception ex)
            {
                LogMessage(0, "Process halted with fatal exception. See log for details.", ex);
            }
            finally
            {
                FinalizeTfs();
                tw.Close();
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

                tpc = AuthenticateAzureTFS();

                VersionControlServer versionControl = tpc.GetService<VersionControlServer>();
                versionControl.NonFatalError += OnNonFatalError;
                versionControl.Getting += OnGetting;
                versionControl.BeforeCheckinPendingChange += OnBeforeCheckinPendingChange;
                versionControl.NewPendingChange += OnNewPendingChange;

                LogMessage(0, string.Format("NOTE: IF YOU GET A MSG THAT YOU DON'T HAVE PERMISSIONS TO CHECKOUT, CHANGE THE TFSWORKSPACE ELEMENT IN THE REFACTOR.CONFIG TO YOUR UA3 WORKSPACE NAME."));

                workspace = versionControl.QueryWorkspaces(refactorConfig.TfsWorkspace, versionControl.AuthorizedUser, Environment.MachineName)[0];
                Workstation.Current.EnsureUpdateWorkspaceInfoCache(versionControl, versionControl.AuthorizedUser);
                LogMessage(0, string.Format("Successfully connected to TFS"));

                allPendingChangesinWorkspace = workspace.GetPendingChanges();

            }
        }

        private static TfsTeamProjectCollection AuthenticateAzureTFS()
        {
            NetworkCredential credential = new NetworkCredential(refactorConfig.TFSUserName, refactorConfig.TFSPassword);
            BasicAuthCredential basicCredntial = new BasicAuthCredential(credential);
            TfsClientCredentials tfsCredntial = new TfsClientCredentials(basicCredntial);
            tpc = new TfsTeamProjectCollection(new Uri(refactorConfig.TfsServer), tfsCredntial);
            tfsCredntial.AllowInteractive = false;
            tpc.EnsureAuthenticated();
            return tpc;
        }

        private static void LoadConfiguration()
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
            if (!string.IsNullOrEmpty(msg))
            {
                string tabs = new string(' ', level * 2);
                logger.LogInformational(tabs + msg);
                Console.WriteLine(tabs + msg);
            }
        }

        private static void LogMessage(int level, string msg, Exception ex)
        {
            if (!string.IsNullOrEmpty(msg) || ex != null)
            {
                string tabs = new string(' ', level * 2);
                logger.LogFatal(tabs + msg, ex);
                Console.WriteLine(tabs + msg);
            }
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
                        RefactorModule(module, Enumerations.ProjectTypes.Reports);
                        RefactorModule(module, Enumerations.ProjectTypes.UX);
                        RefactorModule(module, Enumerations.ProjectTypes.ConfigTools);
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
            if (Directory.Exists(source))
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
            Domain.FileType fileType = refactorConfig.FileTypes.Find(f => f.Extension.ToLower() == file.Extension.ToLower());
            bool fileQualifies = fileType != null;

            if (fileQualifies && !string.IsNullOrWhiteSpace(fileType.QualifyIfPathContains))
            {
                fileQualifies = file.DirectoryName.ToLower().Contains(fileType.QualifyIfPathContains.ToLower());
            }

            if (fileQualifies && !string.IsNullOrWhiteSpace(fileType.IgnoreIfPathContains))
            {
                fileQualifies = !file.DirectoryName.ToLower().Contains(fileType.IgnoreIfPathContains.ToLower());
            }

            if (fileQualifies && !string.IsNullOrWhiteSpace(fileType.QualifyIfNameContains))
            {
                fileQualifies = file.FullName.ToLower().Contains(fileType.QualifyIfNameContains.ToLower());
            }

            if (refactorConfig.RefactorPartialContent)
            {
                ReplacementString obj = refactorConfig.ReplacementStrings.Find(f => f.filename.Contains(file.Name.ToLower()));
                fileQualifies = obj != null;
            }

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
                        if (!PendEditExists(newPath))
                        {
                            TfsPendEdit(newPath);
                        }
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
                else if (refactorConfig.EditMode == Enumerations.EditModeTypes.UpgradePackage && file.Extension.ToLower() == ".csproj")
                {
                    //Handle csproj file related changes
                    string projectFilePath = Path.Combine(dest, file.Name);
                    bool projectUpdated = false;
                    if (File.Exists(projectFilePath))
                    {
                        string projectFileContents = File.ReadAllText(projectFilePath);
                        projectUpdated = UpdateProjectFile(projectFilePath, out projectFileContents);
                        if (projectUpdated)
                        {
                            if (!PendEditExists(projectFilePath))
                            {
                                TfsPendEdit(projectFilePath);
                            }
                            else { pendEditCount++; }
                            File.WriteAllText(projectFilePath, projectFileContents);
                            LogMessage(level, string.Format("Refactoring file '{0}'", new FileInfo(projectFilePath).Name));
                            
                        }
                        else
                        {
                            LogMessage(level, string.Format("Skipping file '{0}'", new FileInfo(projectFilePath).Name));
                        }
                    }
                    else
                    {
                        LogMessage(level, string.Format("File '{0}' not found", new FileInfo(projectFilePath).Name));
                    }

                    //Handle Packages.config file related changes
                    string packageConfigFilePath = projectFilePath.Substring(0, projectFilePath.LastIndexOf("\\")) + "\\packages.config";
                    if (projectUpdated && File.Exists(packageConfigFilePath))
                    {
                        string packagesConfigFileContents = File.ReadAllText(packageConfigFilePath);
                        if (UpdatePackagesConfigFile(packageConfigFilePath, out packagesConfigFileContents))
                        {
                            if (!PendEditExists(packageConfigFilePath))
                            {
                                TfsPendEdit(packageConfigFilePath);
                            }
                            else { pendEditCount++; }
                            File.WriteAllText(packageConfigFilePath, packagesConfigFileContents);
                            LogMessage(level, string.Format("Refactoring file '{0}'", new FileInfo(packageConfigFilePath).Name));
                        }
                        else
                        {
                            LogMessage(level, string.Format("Skipping file '{0}'", new FileInfo(packageConfigFilePath).Name));
                        }
                    }
                    else
                    {
                        LogMessage(level, string.Format("File '{0}' not found", new FileInfo(packageConfigFilePath).Name));
                    }

                    //Handle web/app.config file related changes
                    string webConfigFilePath = projectFilePath.Substring(0, projectFilePath.LastIndexOf("\\")) + "\\web.config";
                    string appConfigFilePath = projectFilePath.Substring(0, projectFilePath.LastIndexOf("\\")) + "\\app.config";
                    string configFilePath = File.Exists(webConfigFilePath) ? webConfigFilePath : appConfigFilePath;
                    if (projectUpdated && File.Exists(configFilePath))
                    {
                        string configFileContents = File.ReadAllText(configFilePath);
                        if (UpdateConfigFile(configFilePath, out configFileContents))
                        {
                            if (!PendEditExists(configFilePath))
                            {
                                TfsPendEdit(configFilePath);
                            }
                            else { pendEditCount++; }
                            File.WriteAllText(configFilePath, configFileContents);
                            LogMessage(level, string.Format("Refactoring file '{0}'", new FileInfo(configFilePath).Name));
                        }
                        else
                        {
                            LogMessage(level, string.Format("Skipping file '{0}'", new FileInfo(configFilePath).Name));
                        }
                    }
                    else
                    {
                        LogMessage(level, string.Format("File '{0}' not found", new FileInfo(configFilePath).Name));
                    }
                }
                else
                {
                    LogMessage(level, string.Format("Skipping file '{0}'", file.Name));
                }
            }
        }

        private static bool UpdateProjectFile(string projectFilePath, out string refactoredFileContents)
        {
            string xmlNs = "http://schemas.microsoft.com/developer/msbuild/2003";
            string FilePath = projectFilePath;
            bool referenceAdded = false;
            bool referenceUpdated = false;
            XmlDocument projectFile = new XmlDocument();
            projectFile.Load(FilePath);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(projectFile.NameTable);
            nsmgr.AddNamespace("MsBuild", xmlNs);

            List<string> pathParts = projectFilePath.Split('\\').ToList();
            string folderLevels = string.Empty;
            for (int i = pathParts.Count - 1; i >= 0; i--)
            {
                if (pathParts[i] != "Source")
                    folderLevels += "..\\";
                else
                {
                    folderLevels = folderLevels.Substring(0, folderLevels.Length - 3);
                    break;
                }
            }
            #region "Reference" Element
            string referenceName = string.Empty;
            XmlNodeList references = projectFile.SelectNodes("//MsBuild:Reference", nsmgr);
            XmlElement referencesItemGroup = references.Count != 0 ? (XmlElement)references[0].ParentNode : null;

            //Package upgrade will happen only if
            //root package is already referred and to a lower version than the one being updated to.
            //Any new dependency to the package being upgraded will get a reference entry as configured.
            string rootPackageInclude = refactorConfig.References.Find(f => f.Dependency == "false").Include;
            string rootPackageName = rootPackageInclude.IndexOf(',') > 0 ? rootPackageInclude.Substring(0, rootPackageInclude.IndexOf(',')) : rootPackageInclude;
            var rootReference = references.Cast<XmlElement>().Where(n => n.Attributes["Include"].Value.Contains(rootPackageName + ",")).Select(mod => mod).ToList();
            if (rootReference.Count == 0)
            {
                rootReference = references.Cast<XmlElement>().Where(n => n.Attributes["Include"].Value == rootPackageName).Select(mod => mod).ToList();
            }
            var rootReferenceHintpath = rootReference.Count > 0 ? rootReference[0].ChildNodes.Cast<XmlElement>().Where(n => n.Name == "HintPath").Select(mod => mod).ToList() : new List<XmlElement>();

            if (rootReference.Count != 0 && (rootReferenceHintpath.Count > 0 ? IsUpgrade(rootReferenceHintpath[0].InnerText, refactorConfig.References.Find(f => f.Dependency == "false").HintPath) : true))
            {
                foreach (Reference reference in refactorConfig.References)
                {
                    referenceName = reference.Include.Substring(0, reference.Include.IndexOf(',') != -1 ? reference.Include.IndexOf(',') : reference.Include.Length);
                    var existingReference = references.Cast<XmlElement>().Where(n => n.Attributes["Include"].Value.Contains(referenceName + ",")).Select(mod => mod).ToList();
                    if (existingReference.Count == 0)
                    {
                        existingReference = references.Cast<XmlElement>().Where(n => n.Attributes["Include"].Value == referenceName).Select(mod => mod).ToList();
                    }
                    if (existingReference.Count == 0)
                    {
                        AddReference(projectFile, referencesItemGroup, reference.Include, !string.IsNullOrEmpty(reference.HintPath) ? folderLevels + reference.HintPath : string.Empty);
                        referenceAdded = true;
                    }
                    else if (UpdateReference(projectFile, existingReference[0], reference, folderLevels))
                    {
                        referenceUpdated = true;
                    }
                }
            }
            #endregion
            if (referenceUpdated || referenceAdded)
            {
                #region "Content" Element
                XmlNodeList contents = projectFile.SelectNodes("//MsBuild:Content", nsmgr);
                XmlElement contentsItemGroup = contents.Count != 0 ? (XmlElement)contents[0].ParentNode : projectFile.CreateElement("ItemGroup", xmlNs);
                if (refactorConfig.Contents.Count > 0)
                {
                    foreach (Content content in refactorConfig.Contents)
                    {
                        AddContent(projectFile, contentsItemGroup, content);
                    }
                    if (contents.Count < 1)
                    {
                        XmlNodeList nones = projectFile.SelectNodes("//MsBuild:None", nsmgr);
                        XmlElement noneItemGroup = nones.Count != 0 ? (XmlElement)nones[0].ParentNode : null;

                        XmlNodeList compiles = projectFile.SelectNodes("//MsBuild:Compile", nsmgr);
                        XmlElement compileItemGroup = compiles.Count != 0 ? (XmlElement)compiles[0].ParentNode : null;

                        if (nones.Count > 1)
                        {
                            projectFile.DocumentElement.InsertBefore(contentsItemGroup, noneItemGroup);
                        }
                        else if (compiles.Count > 1)
                        {
                            projectFile.DocumentElement.InsertAfter(contentsItemGroup, compileItemGroup);
                        }
                    }
                }
                #endregion

                #region "Import" Element
                XmlNodeList existingImports = projectFile.SelectNodes("//MsBuild:Import", nsmgr);

                foreach (Import import in refactorConfig.Imports)
                {
                    string strProject = import.Project.Replace("[SOURCEFOLDER]", folderLevels);
                    string strCondition = import.Condition.Replace("[SOURCEFOLDER]", folderLevels);

                    var importFound = existingImports.Cast<XmlElement>().Where(n => n.Attributes["Project"].Value == strProject).Select(mod => mod).ToList();

                    if (importFound.Count == 0)
                        AddImport(projectFile, strProject, strCondition);
                }
                #endregion

                #region "Target" Element
                XmlNodeList existingtargets = projectFile.SelectNodes("//MsBuild:Target", nsmgr);

                foreach (Target target in refactorConfig.Targets)
                {
                    //bool targetExists = false;
                    var targetFound = existingtargets.Cast<XmlElement>().Where(n => n.Attributes["Name"].Value == target.Name).Select(mod => mod).ToList();


                    if (targetFound.Count > 0)
                    {
                        UpdateTarget(projectFile, target, folderLevels, targetFound[0], nsmgr);
                    }
                    else
                    {
                        AddTarget(projectFile, target, folderLevels);
                    }
                }
                #endregion

                #region "AddFiles"
                foreach (FileDetails fileDetails in refactorConfig.AddFiles)
                {
                    string pathToAdd = fileDetails.PathToAdd.Replace("[PROJECTFOLDER]", Path.GetDirectoryName(projectFilePath));

                    File.Copy(fileDetails.SourcePath, pathToAdd, true);
                    File.SetAttributes(pathToAdd, File.GetAttributes(pathToAdd) & ~FileAttributes.ReadOnly);
                    TfsPendAdd(pathToAdd);
                }
                #endregion
            }

            refactoredFileContents = GetFormattedXml(projectFile.OuterXml);
            return referenceUpdated || referenceAdded;
        }

        private static void UpdateTarget(XmlDocument csprojFile, Target target, string folderLevels, XmlElement targetFound, XmlNamespaceManager nsmgr)
        {
            string xmlNs = "http://schemas.microsoft.com/developer/msbuild/2003";
            var existingErrorElement = targetFound.SelectNodes("//MsBuild:Error", nsmgr);
            foreach (Error error in target.ErrorList)
            {
                var errorFound = existingErrorElement.Cast<XmlElement>().Where(n => n.Attributes["Condition"].Value == error.Condition.Replace("[SOURCEFOLDER]", folderLevels)).Select(mod => mod).ToList();
                if (errorFound.Count == 0)
                {
                    XmlElement errorElement = csprojFile.CreateElement("Error", xmlNs);
                    errorElement.SetAttribute("Condition", error.Condition.Replace("[SOURCEFOLDER]", folderLevels));
                    errorElement.SetAttribute("Text", error.Text.Replace("[SOURCEFOLDER]", folderLevels));
                    targetFound.AppendChild(errorElement);
                }
            }
        }

        private static void AddTarget(XmlDocument csprojFile, Target target, string folderLevels)
        {
            string xmlNs = "http://schemas.microsoft.com/developer/msbuild/2003";

            XmlElement targetElement = csprojFile.CreateElement("Target", xmlNs);
            targetElement.SetAttribute("Name", target.Name);
            if (!string.IsNullOrEmpty(target.BeforeTargets))
                targetElement.SetAttribute("BeforeTargets", target.BeforeTargets);
            if (!string.IsNullOrEmpty(target.AfterTargets))
                targetElement.SetAttribute("AfterTargets", target.AfterTargets);
            if (!string.IsNullOrEmpty(target.DependsOnTargets))
                targetElement.SetAttribute("DependsOnTargets", target.DependsOnTargets);
            if (!string.IsNullOrEmpty(target.Condition))
                targetElement.SetAttribute("Condition", target.Condition);

            XmlElement propertyGroup = csprojFile.CreateElement("PropertyGroup", xmlNs);
            foreach (PropertyElement property in target.PropertyGroup)
            {
                XmlElement propertyElement = csprojFile.CreateElement(property.ElementName, xmlNs);
                propertyElement.InnerText = property.ElementText;
                propertyGroup.AppendChild(propertyElement);
            }

            targetElement.AppendChild(propertyGroup);

            foreach (Error error in target.ErrorList)
            {
                //string strErrorCondition = error.Condition.Replace("[SOURCEFOLDER]", folderLevels);
                //string strErrorText = error.Text.Replace("[SOURCEFOLDER]", folderLevels);
                XmlElement errorElement = csprojFile.CreateElement("Error", xmlNs);
                errorElement.SetAttribute("Condition", error.Condition.Replace("[SOURCEFOLDER]", folderLevels));
                errorElement.SetAttribute("Text", error.Text.Replace("[SOURCEFOLDER]", folderLevels));
                targetElement.AppendChild(errorElement);
            }
            csprojFile.DocumentElement.AppendChild(targetElement);
        }

        private static void AddImport(XmlDocument csprojFile, string strProject, string strCondition)
        {
            string xmlNs = "http://schemas.microsoft.com/developer/msbuild/2003";
            XmlElement importElement = csprojFile.CreateElement("Import", xmlNs);
            importElement.SetAttribute("Project", strProject);
            importElement.SetAttribute("Condition", strCondition);
            csprojFile.DocumentElement.AppendChild(importElement);
        }

        private static void AddContent(XmlDocument csprojFile, XmlElement contentsItemGroup, Content content)
        {
            string xmlNs = "http://schemas.microsoft.com/developer/msbuild/2003";
            XmlElement contentElement = csprojFile.CreateElement("Content", xmlNs);
            contentElement.SetAttribute("Include", content.Include);

            contentsItemGroup.AppendChild(contentElement);
        }

        private static void AddReference(XmlDocument csprojFile, XmlElement referencesItemGroupElement, string strInclude, string strHintPath)
        {
            string xmlNs = "http://schemas.microsoft.com/developer/msbuild/2003";
            XmlElement reference = csprojFile.CreateElement("Reference", xmlNs);
            reference.SetAttribute("Include", strInclude);
            if (!string.IsNullOrEmpty(strHintPath))
            {
                XmlElement hintPath = csprojFile.CreateElement("HintPath", xmlNs);
                hintPath.InnerText = strHintPath;
                reference.AppendChild(hintPath);
            }
            referencesItemGroupElement.AppendChild(reference);
        }

        private static bool UpdateReference(XmlDocument projectFile, XmlElement existingReference, Reference reference, string folderLevels)
        {
            bool referenceUpdated = false;
            string xmlNs = "http://schemas.microsoft.com/developer/msbuild/2003";

            bool hintPathExists = false;
            if (existingReference.ChildNodes.Count > 0)
            {
                List<XmlNode> lstNodesToRemove = new List<XmlNode>();
                foreach (XmlNode childNode in existingReference.ChildNodes)
                {
                    if (childNode.Name == "HintPath" && !string.IsNullOrEmpty(reference.HintPath) && childNode.InnerText != folderLevels + reference.HintPath)
                    {
                        if (!IsUpgrade(childNode.InnerText, reference.HintPath))
                        {
                            return false;
                        }
                        childNode.InnerText = folderLevels + reference.HintPath;
                        hintPathExists = true;
                        referenceUpdated = true;
                    }
                    //Any Private and Specific elements in the reference will be removed
                    if (childNode.Name == "Private" || childNode.Name == "SpecificVersion")
                    {
                        lstNodesToRemove.Add(childNode);
                    }
                }

                foreach (XmlNode nodeToRemove in lstNodesToRemove)
                {
                    existingReference.RemoveChild(nodeToRemove);
                    referenceUpdated = true;
                }
            }
            else if (!string.IsNullOrEmpty(reference.HintPath) && !hintPathExists)
            {
                XmlElement hintPath = projectFile.CreateElement("HintPath", xmlNs);
                hintPath.InnerText = folderLevels + reference.HintPath;
                existingReference.AppendChild(hintPath);
                referenceUpdated = true;
            }

            if (existingReference.Attributes["Include"].Value != reference.Include)
            {
                existingReference.RemoveAttribute("Include");
                existingReference.SetAttribute("Include", reference.Include);
                referenceUpdated = true;
            }

            return referenceUpdated;
        }

        private static bool PendEditExists(string path)
        {
            IEnumerable<PendingChange> pc = allPendingChangesinWorkspace.ToList<PendingChange>().Where(a => a.LocalItem.ToLower() == path.ToLower()).Where(a => a.ChangeType == ChangeType.Edit);

            return pc.Count<PendingChange>() > 0;
            
        }

        private static bool IsUpgrade(string fromHintPath, string toHintPath)
        {
            //Using version available in the folder to determine if it is an upgrade
            bool isUpGrade = false;
            List<string> from = fromHintPath.Split('\\').ToList();
            List<string> to = toHintPath.Split('\\').ToList();
            List<string> fromPackageName = new List<string>();
            List<string> toPackageName = new List<string>();

            for (int i = 0; i < from.Count; i++)
            {
                if (from[i] == "Packages" || from[i] == "packages")
                {
                    fromPackageName = from[i + 1].Split('.').ToList();
                    break;
                }
            }
            for (int i = 0; i < to.Count; i++)
            {
                if (to[i] == "Packages" || to[i] == "packages")
                {
                    toPackageName = to[i + 1].Split('.').ToList();
                    break;
                }
            }
            if (fromPackageName.Count != toPackageName.Count)
            {
                while (fromPackageName.Count > toPackageName.Count)
                {
                    toPackageName.Add("0");
                }
                while (fromPackageName.Count < toPackageName.Count)
                {
                    fromPackageName.Add("0");
                }
            }
            for (int i = 0; i < fromPackageName.Count; i++)
            {
                if (int.TryParse(fromPackageName[i], out int fromVersion) && int.TryParse(toPackageName[i], out int toVersion))
                {
                    if (toVersion > fromVersion)
                        return true;
                    else if (toVersion < fromVersion)
                        return false;
                }
                else if (fromPackageName[i] != toPackageName[i])
                {
                    if (fromPackageName[0] == "HP" || fromPackageName[0] == "HPE" || fromPackageName[0] == "HPP" || fromPackageName[0] == "MMS")
                        return true;
                }
            }

            return isUpGrade;
        }

        private static bool UpdatePackagesConfigFile(string packagesConfigPath, out string refactoredFileContents)
        {
            string FilePath = packagesConfigPath;
            bool fileUpdated = false;
            XmlDocument packagesConfigFile = new XmlDocument();
            packagesConfigFile.Load(FilePath);
            XmlElement packagesElement = packagesConfigFile.DocumentElement;
            XmlNodeList packages = packagesConfigFile.SelectNodes("/packages/package");

            foreach (Package package in refactorConfig.Packages)
            {
                var existingPackageEntry = packages.Cast<XmlElement>().Where(n => n.Attributes["id"].Value == package.id).Select(mod => mod).ToList();

                if (existingPackageEntry.Count == 0)
                {
                    AddPackageEntry(packagesConfigFile, packagesElement, package.id, package.version, package.targetFramework);
                    fileUpdated = true;
                }
                else if (existingPackageEntry[0].Attributes["version"].Value != package.version || existingPackageEntry[0].Attributes["targetFramework"].Value != package.targetFramework)
                {
                    List<string> existingVersion = existingPackageEntry[0].Attributes["version"].Value.Split('.').ToList();
                    List<string> toVersion = package.version.Split('.').ToList();

                    while (existingVersion.Count > toVersion.Count)
                    {
                        toVersion.Add("0");
                    }
                    while (existingVersion.Count < toVersion.Count)
                    {
                        existingVersion.Add("0");
                    }
                    bool isUpgrade = false;
                    for (int i = 0; i < existingVersion.Count; i++)
                    {
                        if (int.TryParse(existingVersion[i], out int fromVersion) && int.TryParse(toVersion[i], out int targetVersion))
                        {
                            if (targetVersion > fromVersion)
                            {
                                isUpgrade = true;
                                break;
                            }
                        }
                    }
                    if (isUpgrade)
                    {
                        existingPackageEntry[0].RemoveAttribute("version");
                        existingPackageEntry[0].RemoveAttribute("targetFramework");
                        existingPackageEntry[0].SetAttribute("version", package.version);
                        existingPackageEntry[0].SetAttribute("targetFramework", package.targetFramework);
                        fileUpdated = true;
                    }
                }
            }

            refactoredFileContents = GetFormattedXml(packagesConfigFile.OuterXml);
            return fileUpdated;
        }

        private static void AddPackageEntry(XmlDocument packagesConfigFile, XmlElement packagesElement, string id, string version, string targetFramework)
        {
            XmlElement packageElement = packagesConfigFile.CreateElement("package");
            packageElement.SetAttribute("id", id);
            packageElement.SetAttribute("version", version);
            packageElement.SetAttribute("targetFramework", targetFramework);
            packagesElement.AppendChild(packageElement);
        }

        private static bool UpdateConfigFile(string configFilePath, out string refactoredFileContents)
        {
            string xmlNs = "urn:schemas-microsoft-com:asm.v1";
            bool fileUpdated = false;
            XmlDocument configFile = new XmlDocument();
            configFile.Load(configFilePath);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(configFile.NameTable);
            nsmgr.AddNamespace("ConfigNS", xmlNs);

            XmlElement assemblyBinding = (XmlElement)configFile.SelectSingleNode("//ConfigNS:assemblyBinding", nsmgr);
            if (assemblyBinding == null)
            {
                XmlElement configuration = (XmlElement)configFile.SelectSingleNode("configuration", nsmgr);
                XmlElement runtime = (XmlElement)configFile.SelectSingleNode("configuration/runtime", nsmgr);
                if (runtime == null)
                {
                    assemblyBinding = configFile.CreateElement("assemblyBinding", xmlNs);
                    runtime = configFile.CreateElement("runtime");
                    runtime.AppendChild(assemblyBinding);
                    configuration.AppendChild(runtime);
                }
            }

            if (assemblyBinding != null)
            {
                foreach (DependentAssembly dependentAssly in refactorConfig.WebAppConfig)
                {
                    XmlElement dependentAssembly = (XmlElement)assemblyBinding.SelectSingleNode("//ConfigNS:assemblyIdentity[@name='" + dependentAssly.name + "']", nsmgr);
                    if (dependentAssembly == null)
                    {
                        AddDependentAssembly(configFile, assemblyBinding, dependentAssly);
                        fileUpdated = true;
                    }
                    else
                    {
                        foreach (XmlElement dependentAssemblyChild in dependentAssembly.ParentNode.ChildNodes)
                        {
                            if (dependentAssemblyChild.Name == "assemblyIdentity")
                            {
                                if (dependentAssemblyChild.Attributes["publicKeyToken"].Value != dependentAssly.publicKeyToken || dependentAssemblyChild.HasAttribute("culture") ? dependentAssemblyChild.Attributes["culture"].Value != dependentAssly.culture:false)
                                {
                                    dependentAssemblyChild.RemoveAttribute("publicKeyToken");
                                    dependentAssemblyChild.RemoveAttribute("culture");
                                    dependentAssemblyChild.SetAttribute("publicKeyToken", dependentAssly.publicKeyToken);
                                    dependentAssemblyChild.SetAttribute("culture", dependentAssly.culture);
                                    fileUpdated = true;
                                }
                            }
                            else if (dependentAssemblyChild.Name == "bindingRedirect")
                            {
                                if (dependentAssemblyChild.Attributes["oldVersion"].Value != dependentAssly.oldVersion || dependentAssemblyChild.Attributes["newVersion"].Value != dependentAssly.newVersion)
                                {
                                    dependentAssemblyChild.RemoveAttribute("oldVersion");
                                    dependentAssemblyChild.RemoveAttribute("newVersion");
                                    dependentAssemblyChild.SetAttribute("oldVersion", dependentAssly.oldVersion);
                                    dependentAssemblyChild.SetAttribute("newVersion", dependentAssly.newVersion);
                                    fileUpdated = true;
                                }
                            }
                        }
                    }
                }
            }
            refactoredFileContents = GetFormattedXml(configFile.OuterXml);
            return fileUpdated;
        }

        public static void AddDependentAssembly(XmlDocument configFile, XmlElement assemblyBinding, DependentAssembly dependentAssly)
        {
            string xmlNs = "urn:schemas-microsoft-com:asm.v1";
            XmlElement dependentAssembly = configFile.CreateElement("dependentAssembly", xmlNs);
            XmlElement assemblyIdentity = configFile.CreateElement("assemblyIdentity", xmlNs);
            assemblyIdentity.SetAttribute("name", dependentAssly.name);
            assemblyIdentity.SetAttribute("publicKeyToken", dependentAssly.publicKeyToken);
            assemblyIdentity.SetAttribute("culture", dependentAssly.culture);
            XmlElement bindingRedirect = configFile.CreateElement("bindingRedirect", xmlNs);
            bindingRedirect.SetAttribute("oldVersion", dependentAssly.oldVersion);
            bindingRedirect.SetAttribute("newVersion", dependentAssly.newVersion);

            dependentAssembly.AppendChild(assemblyIdentity);
            dependentAssembly.AppendChild(bindingRedirect);

            assemblyBinding.AppendChild(dependentAssembly);
        }

        public static string GetFormattedXml(string xml)
        {
            string result = "";
            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                document.LoadXml(xml);
                writer.Formatting = Formatting.Indented;
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();
                mStream.Position = 0;
                StreamReader sReader = new StreamReader(mStream);
                result = sReader.ReadToEnd();
            }

            catch (XmlException)
            {
                LogMessage(2, "There was an issue in Formatting the XML");
            }
            finally
            {
                mStream.Close();
                writer.Close();
            }
            return result;
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
                                    if (refactorConfig.RefactorPartialContent)
                                    {
                                        if (line.Contains(replacement.Qualifier) && filePath.ToLower().Contains(replacement.filename) && !contentsChanged)
                                        {
                                            string newstring = replacement.connectionString + (line.Contains(replacement.providerName) ? replacement.providerName : "");
                                            line = System.Text.RegularExpressions.Regex.Replace(line, replacement.From, newstring);
                                            contentsChanged = true;
                                            break;
                                        }

                                    }
                                    else
                                    {
                                        contentsChanged |= RefactorLine(replacement, ref line);
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

        private static bool RefactorLine(ReplacementString replacement, ref string line)
        {
            if (replacement.RegexReplace)
            {
                if (line.Contains(replacement.Qualifier) && replacement.FromRegex.IsMatch(line))
                {
                    line = replacement.FromRegex.Replace(line, replacement.To);
                    return true;
                }
            }
            else
            {
                if (line.Contains(replacement.Qualifier) && line.Contains(replacement.From))
                {
                    line = line.Replace(replacement.From, replacement.To);
                    return true;
                }
            }

            return false;
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
                if (type == Enumerations.ProjectTypes.K2_Workflow)
                {
                    sourcePath += "\\" + type.ToString().Replace("_", " ");
                }
                else if (type == Enumerations.ProjectTypes.ProviderManagement_EnrollmentTestClient)
                {
                    sourcePath += "\\" + type.ToString().Replace("_", ".");
                }
                else
                {
                    sourcePath += "\\" + type.ToString();
                }
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
                if (subdir.Name.ToLower().StartsWith(package.QualifierPrefix.ToLower()) &&
                    (string.IsNullOrEmpty(package.QualifierVersion) || subdir.Name.ToLower().EndsWith(package.QualifierVersion)))
                {
                    string newDirName = subdir.Name;
                    if (refactorConfig.IncludeFolderNames)
                    {
                        newDirName = RefactorFileName(subdir.Name);
                    }

                    RefactorDirectory(subdir.FullName, destPath + newDirName, destPath + newDirName, true, 2);
                    DirectoryInfo newPackageDir = new DirectoryInfo(destPath + newDirName);
                    bool packageSourceReformatted = true;

                    if (package.ReformatSource)
                    {
                        packageSourceReformatted = ReformatPackageSource(newPackageDir, 2);
                    }

                    if (packageSourceReformatted && package.RebuildPackage)
                    {
                        RebuildNugetPackage(newPackageDir, 2);
                    }
                }
            }
        }

        private static void ReportCounts()
        {
            LogMessage(0, " ");
            LogMessage(0, "===========================================================================");
            LogMessage(0, "Processing Summary");
            LogMessage(0, "===========================================================================");
            LogMessage(0, string.Format("Pending Adds:       {0}", pendAddCount.ToString("#,##0")));
            LogMessage(0, string.Format("Pending Edits:      {0}", pendEditCount.ToString("#,##0")));
            LogMessage(0, string.Format("Pending Deletes:    {0}", pendDeleteCount.ToString("#,##0")));
            LogMessage(0, string.Format("Pending Renames:    {0}", pendRenameCount.ToString("#,##0")));
            LogMessage(0, string.Format("Exceptions Written: {0}", exceptionsWritten.ToString("#,##0")));
            LogMessage(0, " ");
        }

        private static void RebuildNugetPackage(DirectoryInfo packageDir, int level)
        {
            LogMessage(level, string.Format("Rebuilding package source: '{0}'", packageDir.FullName));
            FileInfo nuspecFile = null;
            FileInfo nupkgFile = null;

            FileInfo[] files = packageDir.GetFiles(packageDir.Name + ".nuspec");
            if (files.Length == 1)
            {
                nuspecFile = files[0];
            }
            else
            {
                LogMessage(level, string.Format("Unable to find nuspec file for package '{0}'.", packageDir.FullName), new Exception());
                tw.WriteLine("Unable to find nuspec file for package '{0}'.", packageDir.FullName);
                exceptionsWritten++;
            }

            files = packageDir.GetFiles(packageDir.Name + ".nupkg");
            if (files.Length == 1)
            {
                nupkgFile = files[0];
            }
            else
            {
                LogMessage(level, string.Format("Unable to find nupkg file for package '{0}'.", packageDir.FullName), new Exception());
                tw.WriteLine("Unable to find nupkg file for package '{0}'.", packageDir.FullName);
                exceptionsWritten++;
            }

            if (nuspecFile != null && nupkgFile != null)
            {
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

                if (File.Exists(sourcePkg))
                {
                    File.Move(sourcePkg, nupkgFile.FullName);
                }
                else
                {
                    LogMessage(level, string.Format("Unable to find generated nupkg file at '{0}'. Nupkg file generation may have failed.", sourcePkg), new Exception());
                    tw.WriteLine("Nupkg file generation may have failed. Unable to find and copy '{0}' to '{1}'.", sourcePkg, nupkgFile.FullName);
                    exceptionsWritten++;
                }
            }
        }

        private static bool ReformatPackageSource(DirectoryInfo packageDir, int level)
        {
            LogMessage(level, string.Format("Reformating package source: '{0}'", packageDir.FullName));
            FileInfo nuspecFile = null;

            FileInfo[] files = packageDir.GetFiles(packageDir.Name + ".nuspec");
            if (files.Length == 1)
            {
                nuspecFile = files[0];
            }
            else
            {
                LogMessage(level, string.Format("Unable to find nuspec file for package '{0}'.", packageDir.FullName), new Exception());
                tw.WriteLine("Unable to find nuspec file for package '{0}'.", packageDir.FullName);
                exceptionsWritten++;
                return false;
            }

            if (nuspecFile != null)
            {
                TfsPendEdit(nuspecFile.FullName);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(nuspecFile.FullName);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                nsmgr.AddNamespace("ms", "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd");
                XmlNodeList assemblyFiles = xmlDoc.SelectNodes("//ms:file", nsmgr);

                if (assemblyFiles != null && assemblyFiles.Count > 0)
                {
                    foreach (XmlNode assemblyFile in assemblyFiles)
                    {
                        assemblyFile.Attributes["src"].Value = assemblyFile.Attributes["target"].Value;
                    }
                }

                xmlDoc.Save(nuspecFile.FullName);
            }

            return true;
        }

        private static void TfsPendAdd(string path)
        {
            bool retry = true;
            int retryAttempts = 0;

            while (retry && retryAttempts <= retryCount)
            {
                try
                {
                    if (workspace != null)
                    {
                        workspace.PendAdd(path);
                        pendAddCount++;
                    }

                    retry = false;
                }
                catch
                {
                    retryAttempts++;
                }
            }
        }

        private static void TfsPendDelete(string path)
        {
            bool retry = true;
            int retryAttempts = 0;

            while (retry && retryAttempts <= retryCount)
            {
                try
                {
                    if (workspace != null)
                    {
                        workspace.PendDelete(path);
                        pendDeleteCount++;
                    }

                    retry = false;
                }
                catch
                {
                    retryAttempts++;
                }
            }
        }

        private static void TfsPendEdit(string path)
        {
            bool retry = true;
            int retryAttempts = 0;

            while (retry && retryAttempts <= retryCount)
            {
                try
                {
                    if (workspace != null)
                    {
                        workspace.PendEdit(path);
                        pendEditCount++;
                    }

                    retry = false;
                }
                catch
                {
                    retryAttempts++;
                }
            }
        }

        private static void TfsPendRename(string oldPath, string newPath)
        {
            bool retry = true;
            int retryAttempts = 0;

            while (retry && retryAttempts <= retryCount)
            {
                try
                {
                    if (workspace != null)
                    {
                        workspace.PendRename(oldPath, newPath);
                        pendRenameCount++;
                    }

                    retry = false;
                }
                catch
                {
                    retryAttempts++;
                }
            }
        }

        private static void TfsUndo(string path)
        {
            bool retry = true;
            int retryAttempts = 0;

            while (retry && retryAttempts <= retryCount)
            {
                try
                {
                    if (workspace != null)
                    {
                        workspace.Undo(path);
                    }

                    retry = false;
                }
                catch
                {
                    retryAttempts++;
                }
            }
        }
    }
}
