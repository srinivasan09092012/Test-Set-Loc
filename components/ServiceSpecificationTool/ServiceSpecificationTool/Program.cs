using HPE.HSP.UA3.Core.API.Logger;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using ServiceSpecificationTool.Domain;
using ServiceSpecificationTool.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ServiceSpecificationTool
{
    public class Program
    {
        private static int pendAddCount = 0;
        private static int pendEditCount = 0;
        private static int pendRenameCount = 0;
        private static int retryCount = 10;

        private static List<ModuleConfig> modulesFromSource = null;
        private static List<Documentation> modulesFromDocumentation = null;
        private static List<ServiceEnvironments> serviceEnvironments = null;

        private static List<string> eachModuleServices = null;
        private static List<string> downServices = null;
        private static List<string> servicesFromModules = null;
        private static List<string> servicesFromServiceURL = null;
        private static List<string> servicesFromConfiguration = null;
        private static List<string> serviceURLsConfigurationMissing = null;
        private static List<string> missingServicesFromConfigurationOrServiceURL = null;
        private static List<string> wsdlURLS = null;
        private static List<string> subFoldersInSideManagedCare = new List<string>() { "Capitation", "EDI", "MCE", "MCO", "MCOPortal",
                                                                "Member", "MemberPortal", "Network", "Provider", "ProviderPortal", "TPL" };
        private static SandcastleConfig sandcastleConfiguration = null;
        private static TfsTeamProjectCollection tpc = null;
        private static Workspace localWorkSpace = null;
        private static DirectoryInfo directoryInfo;
        private static TextWriter tw = null;
        private static Stopwatch stopWatch = new Stopwatch();
        private static string currentBranch = null;
        private static bool branch = false;
        private static bool enterWrongBranch = false;
        private static bool sflag = false;
        static void Main(string[] args)
        {
            stopWatch.Start();
            try
            {
                Console.WriteLine("This utility will be used to:");
                Console.WriteLine("1. Generate .CHM File");
                Console.WriteLine("2. Generate WSDLs");
                Console.WriteLine("3. Generate XSDLs");
                Console.WriteLine("4. Generate WSDLs Difference");
                Console.WriteLine("5. Publish");
                Console.WriteLine("Please enter the option 1, 2 ,3 4 or 5");
                Console.WriteLine("------------------------------------");
                string input = Console.ReadLine().Trim();
                Console.WriteLine("------------------------------------");
                try
                {
                    LoadConfiguration();

                    switch (input)
                    {
                        case "1":
                            AuthenticateAzureTFS();

                            GetLatestAndBuildSolution();

                            NewSandcastleFolderstructure();

                            break;
                        case "2":

                            GetServicesFromServiceURLs();

                            break;
                        case "3":

                            GenerateXSDLS();

                            break;
                        case "4":

                            GenerateWSDLSDifference();

                            break;
                        case "5":

                            PublishFolder();

                            break;
                        default:
                            Console.WriteLine("Invalid Selection");
                            break;
                    }
                }

                catch (Exception ex)
                {
                    LoggerManager.Logger.LogError("Error " + "" + "{0}", ex);
                    LogMessage(0, "Process halted with fatal exception. See log for details.", ex);
                }
            }
            finally
            {
                FinalizeTfs();
                stopWatch.Stop();
                Console.WriteLine("Time taken : {0}", stopWatch.Elapsed);
                //tw.Close();
                Console.Write("Press any key to close");
                Console.ReadKey();
            }
        }

        #region Common methods
        private static void LoadConfiguration()
        {
            LogMessage(0, string.Format("-------Loading configuration-------"));

            string filePath = AppDomain.CurrentDomain.BaseDirectory + Constants.ConfigFile;
            if (File.Exists(filePath))
            {
                string fileContents = File.ReadAllText(filePath);
                sandcastleConfiguration = Serializer.XmlDeserialize<SandcastleConfig>(fileContents, string.Empty);
                LogMessage(0, string.Format("-------Validation configuration-------"));
                sandcastleConfiguration.Validate();
                LogMessage(0, string.Format("-------Validation configuration completed-------"));
                modulesFromSource = sandcastleConfiguration.ModuleConfigs.ToList();
                modulesFromDocumentation = sandcastleConfiguration.Documentation.ToList();
                serviceEnvironments = sandcastleConfiguration.ServiceEnvironments.ToList();
                sandcastleConfiguration.DependencyModules.ToList();
                LogMessage(0, string.Format("-------Loading configuration completed-------"));
            }
            else
            {
                LoggerManager.Logger.LogError(string.Format("Configuration file '{0}' does not exist.", filePath));
            }
        }

        private static void RunBatFiles(string batfileName, string projectName)
        {
            try
            {
                string status = string.Empty; string batAndProjectName = string.Empty; int exitCode;

                StackFrame frame = new StackFrame(1);

                var method = frame.GetMethod();
                var type = method.DeclaringType;
                var name = method.Name;

                ProcessStartInfo processInfo;
                Process process;

                var batName = batfileName.Split('.');
                string[] batFiles = Directory.GetFiles("../../BatFiles/");
                var batFileName = batFiles.SingleOrDefault(x => x.Contains(batfileName));

                if (batFileName.Contains("WSDLsGeneration") || batFileName.Contains("BuildCoreAPISolution") || batFileName.Contains("XSDsGeneration") || batFileName.Contains("WSDLs-Diff") || batFileName.Contains("Publish"))
                {
                    processInfo = new ProcessStartInfo(batFileName, projectName);
                }
                else
                {
                    processInfo = new ProcessStartInfo(batFileName, projectName); // '"' + projectName + '"' + " " + sandcastleConfiguration.BuildOutputDirectory);
                }

                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;
                process = Process.Start(processInfo);
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                exitCode = process.ExitCode;

                if (error != string.Empty)
                {
                    LoggerManager.Logger.LogError(batfileName + ":" + projectName + " : " + error);
                    status = "Failed";
                }

                if (batFileName.Contains("WSDLsGeneration"))
                {
                    projectName = projectName.Split(' ')[0].Split('"')[0].ToString();
                }

                switch (batfileName)
                {
                    case SandcastleConfig.BuildBasProjectBatFile:

                        batAndProjectName = batfileName.Split('.')[0] + " for " + Path.GetFileName(projectName.Split(' ')[0].Split('"')[0].ToString());
                        ResultLogSetting(output, error, exitCode, batAndProjectName);
                        break;
                    case SandcastleConfig.BuildUXProjectBatFile:

                        batAndProjectName = batfileName.Split('.')[0] + " for " + Path.GetFileName(projectName.Split(' ')[0].Split('"')[0].ToString());
                        ResultLogSetting(output, error, exitCode, batAndProjectName);
                        break;

                    case SandcastleConfig.BuildUXSolutionBatFile:

                        batAndProjectName = batfileName.Split('.')[0] + " for " + Path.GetFileName(projectName.Split(' ')[0].Split('"')[0].ToString());
                        ResultLogSetting(output, error, exitCode, batAndProjectName);
                        break;

                    case SandcastleConfig.BuildBasSolutionBatFile:

                        batAndProjectName = batfileName.Split('.')[0] + " for " + Path.GetFileName(projectName.Split(' ')[0].Split('"')[0].ToString());
                        ResultLogSetting(output, error, exitCode, batAndProjectName);
                        break;

                    case SandcastleConfig.GenerateCHMFilesBatFile:

                        batAndProjectName = batfileName.Split('.')[0] + " for " + Path.GetFileName(projectName.Split(' ')[0].Split('"')[0].ToString());
                        ResultLogSetting(output, error, exitCode, batAndProjectName);
                        break;

                    case SandcastleConfig.GenerateXSDsBatFile:

                        batAndProjectName = batfileName.Split('.')[0] + " for " + Path.GetFileName(projectName);
                        if (!output.Contains("Error") && error == string.Empty)
                            output = "Build succeeded.";
                        else
                            output = "Error";

                        ResultLogSetting(output, error, exitCode, batAndProjectName);

                        break;
                    case SandcastleConfig.GenerateWSDLsDifferenceBatFile:

                        batAndProjectName = batfileName.Split('.')[0] + " for " + Path.GetFileName(projectName);

                        ResultLogSetting(output, error, exitCode, batAndProjectName);

                        break;
                    case SandcastleConfig.GenerateWSDLsBatFile:

                        batAndProjectName = batfileName.Split('.')[0] + " for " + Path.GetFileName(projectName);
                        if (!output.Contains("Error") && error == string.Empty)
                            output = "Build succeeded.";
                        else
                            output = "Error";
                        ResultLogSetting(output, error, exitCode, batAndProjectName);

                        break;
                    case SandcastleConfig.BuildCoreAPISolutionBatFile:

                        batAndProjectName = batfileName.Split('.')[0] + " for " + Path.GetFileName(projectName);
                        if (output.Contains("Build succeeded.") && error == string.Empty)
                            output = "Build succeeded.";
                        else
                            output = "Error";
                        ResultLogSetting(output, error, exitCode, batAndProjectName);

                        break;
                    case SandcastleConfig.PublishSandcastleDocumentationBatFile:
                        var projfilename = Path.GetFileName(projectName.Split(' ')[0]).Split('.')[0];
                        batAndProjectName = "Publishing the " + projfilename + " " + sandcastleConfiguration.currentBranch;
                        if (!output.Contains("Error"))
                            output = "Build succeeded.";
                        else
                            output = "Error while publishing";
                        ResultLogSetting(output, error, exitCode, batAndProjectName);

                        break;
                    default:
                        Console.WriteLine("Invalid Bat file please check batfileName");
                        break;
                }
            }

            catch (Exception ex)
            {
                LoggerManager.Logger.LogError(batfileName + " : " + ex.InnerException);
            }
        }

        private static void ResultLogSetting(string resultOutput, string error, int exitcode, string batAndProjectName)
        {
            string statusMsg = string.Empty;

            if (resultOutput.Contains("Build succeeded."))
            {
                statusMsg = " Successfully completed " + batAndProjectName;
            }
            else
            {
                statusMsg = "Error " + batAndProjectName;
                LoggerManager.Logger.LogError(resultOutput);
            }

            LogMessage(0, string.Format("------------------------------------"));
            LogMessage(0, "Output >> " + statusMsg);
            LogMessage(0, "Error >> " + (String.IsNullOrEmpty(error) ? "(none)" : error));
            LogMessage(0, string.Format("ExitCode: " + exitcode.ToString(), "ExecuteCommand"));
            LogMessage(0, string.Format("------------------------------------"));
        }

        private static void RunDependencyCoreBatFiles(string batfileName, string projectName)
        {
            try
            {
                string status = string.Empty;
                var batName = batfileName.Split('.');

                int exitCode;
                ProcessStartInfo processInfo;
                Process process;
                var batFileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"..\..\..\" + batfileName;
                processInfo = new ProcessStartInfo(batFileName, projectName);
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;

                process = Process.Start(processInfo);
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();
                exitCode = process.ExitCode;

                if (error != string.Empty)
                {
                    LoggerManager.Logger.LogError(batfileName + ":" + projectName + " : " + error);
                }

                LogMessage(0, string.Format("------------------------------------"));
                LogMessage(0, "Status >> " + (String.IsNullOrEmpty(status) ? " Succefully completed " + batfileName + " for " + Path.GetFileName(projectName) : status + batfileName + " for " + Path.GetFileName(projectName)));
                LogMessage(0, "Output >> " + (String.IsNullOrEmpty(output) ? "(none)" : output));
                LogMessage(0, "Error >> " + (String.IsNullOrEmpty(error) ? "(none)" : error));
                LogMessage(0, string.Format("ExitCode: " + exitCode.ToString(), "ExecuteCommand"));
                LogMessage(0, string.Format("------------------------------------"));
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogError(batfileName + " : " + ex.InnerException);
            }
        }

        private static void RunWSDLBatFiles(string batfileName, string projectName)
        {
            try
            {
                var batName = batfileName.Split('.');

                int exitCode;
                ProcessStartInfo processInfo;
                Process process;
                string[] batFiles = Directory.GetFiles("../../BatFiles/");
                var batFileName = batFiles.SingleOrDefault(x => x.Contains(batfileName));
                processInfo = new ProcessStartInfo(batFileName, projectName);
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;

                process = Process.Start(processInfo);
                string output = string.Empty;
                string error = string.Empty;
                string status = string.Empty;

                output = process.StandardOutput.ReadLine();

                while (process.StandardOutput.Peek() > -1)
                {
                    output = output + process.StandardOutput.ReadLine();
                }

                while (process.StandardError.Peek() > -1)
                {
                    if (!process.StandardError.ReadToEnd().Contains("WARNING:"))
                        error = error + process.StandardError.ReadToEnd();
                }

                process.WaitForExit();
                exitCode = process.ExitCode;

                if (error != string.Empty)
                {
                    LoggerManager.Logger.LogError(batfileName + ":" + projectName + " : " + error);
                    status = "Error";
                }

                LogMessage(0, string.Format("------------------------------------"));
                LogMessage(0, string.Format("Status >> " + (String.IsNullOrEmpty(status) ? " Succefully completed " + batfileName + " for " + Path.GetFileName(projectName) : status + batfileName + " for " + Path.GetFileName(projectName))));
                LogMessage(0, string.Format("Output >>" + (String.IsNullOrEmpty(output) ? "(none)" : output)));
                LogMessage(0, string.Format("Error >>" + (String.IsNullOrEmpty(error) ? "(none)" : error)));
                LogMessage(0, string.Format("ExitCode: " + exitCode.ToString(), "ExecuteCommand"));
                LogMessage(0, string.Format("------------------------------------"));
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogError(batfileName + " : " + ex.InnerException);
            }
        }

        private static void LogMessage(int level, string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                string tabs = new string(' ', level * 2);
                LoggerManager.Logger.LogInformational(tabs + msg);
                Console.WriteLine(tabs + msg);
            }
        }

        private static void LogMessage(int level, string msg, Exception ex)
        {
            if (!string.IsNullOrEmpty(msg) || ex != null)
            {
                string tabs = new string(' ', level * 2);
                LoggerManager.Logger.LogInformational(tabs + msg, ex);
                Console.WriteLine(tabs + msg);
            }
        }

        private static void FinalizeTfs()
        {
            if (localWorkSpace != null)
            {
                localWorkSpace = null;
            }
        }

        private static void OnNonFatalError(Object sender, ExceptionEventArgs e)
        {
            if (e.Exception != null)
            {
                LoggerManager.Logger.LogError(string.Format("Non-fatal exception: " + e.Exception.Message));
            }
            else
            {
                LoggerManager.Logger.LogError(string.Format("Non-fatal exception: " + e.Failure.Message));
            }
        }

        private static void OnGetting(Object sender, GettingEventArgs e)
        {
            LoggerManager.Logger.LogError(string.Format("Getting: " + e.TargetLocalItem + ", status: " + e.Status));
        }

        private static void OnBeforeCheckinPendingChange(Object sender, ProcessingChangeEventArgs e)
        {
            LoggerManager.Logger.LogError("Checking in " + e.PendingChange.LocalItem);
        }

        private static void OnNewPendingChange(Object sender, PendingChangeEventArgs e)
        {
            LoggerManager.Logger.LogError("Pending " + PendingChange.GetLocalizedStringForChangeType(e.PendingChange.ChangeType) + " on " + e.PendingChange.LocalItem);
        }

        private static void RefactorDirectory(string source, string origDest, string newDest, string moduleName, bool recursive, int level, bool checkTFSActivity)
        {
            try
            {
                if (Directory.Exists(source))
                {
                    bool processDirectory = true;
                    LogMessage(level, string.Format("Refactoring directory '{0}' to '{1}'", source, origDest));
                    level++;

                    if (sandcastleConfiguration.EditMode == Enumerations.EditModeTypes.Copy)
                    {
                        if (string.Compare(source, newDest, false) != 0)
                        {
                            if (Directory.Exists(newDest))
                            {
                                TfsUndo(newDest, checkTFSActivity);
                                Directory.Delete(newDest, true);
                            }
                            Directory.CreateDirectory(newDest);
                            TfsPendAdd(newDest, checkTFSActivity);
                        }
                        else
                        {
                            processDirectory = false;
                        }
                    }
                    else
                    {
                        if (string.Compare(source, newDest, false) != 0)
                        {
                            TfsPendRename(source, newDest, checkTFSActivity);
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
                            if (!file.Name.Contains(".shfbproj") && !file.Name.Contains(".log"))
                            {
                                RefactorFile(file, newDest, level, checkTFSActivity);
                            }
                        }

                        if (recursive)
                        {
                            foreach (DirectoryInfo subdir in dirs)
                            {
                                if (!subdir.Name.Contains(".log") || !subdir.Name.Contains(".shfbproj"))
                                {
                                    string newDirName = subdir.Name;
                                    string origSubDirDest = Path.Combine(newDest, subdir.Name);
                                    string newSubDirDest = Path.Combine(newDest, newDirName);
                                    RefactorDirectory(subdir.FullName, origSubDirDest, newSubDirDest, moduleName, recursive, level, checkTFSActivity);
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage(0, "Error in RefactorDirectory. " + ex.Message);
            }
        }

        public static void CreateZipFile(string source, string origDest, string newDest, bool recursive, int level, bool checkTFSActivity)
        {
            try
            {
                if (Directory.Exists(source))
                {
                    level++;
                    bool processDirectory = true;

                    if (sandcastleConfiguration.EditMode == Enumerations.EditModeTypes.Copy)
                    {
                        if (string.Compare(source, newDest, false) != 0)
                        {
                            if (Directory.Exists(newDest))
                            {
                                TfsUndo(newDest, checkTFSActivity);
                                Directory.Delete(newDest, true);
                            }
                            Directory.CreateDirectory(newDest);
                            TfsPendAdd(newDest, checkTFSActivity);
                        }
                        else
                        {
                            processDirectory = false;
                        }
                    }
                    else
                    {
                        if (string.Compare(source, origDest, false) != 0)
                        {
                            TfsPendRename(source, origDest, checkTFSActivity);
                            source = origDest;
                        }
                    }

                    if (processDirectory)
                    {
                        DirectoryInfo dir = new DirectoryInfo(source);
                        DirectoryInfo[] dirs = dir.GetDirectories();
                        FileInfo[] files = dir.GetFiles();
                        foreach (FileInfo file in files)
                        {
                            if (!file.Name.Contains(".shfbproj"))
                            {
                                RefactorFile(file, newDest, level, checkTFSActivity);
                            }
                        }
                        if (recursive)
                        {
                            foreach (DirectoryInfo subdir in dirs)
                            {
                                if (subdir.Name != "TempFolder")
                                {
                                    if (!subdir.Name.Contains(".log") || !subdir.Name.Contains(".shfbproj"))
                                    {
                                        string newDirName = subdir.Name;
                                        string origSubDirDest = Path.Combine(origDest, subdir.Name);
                                        string newSubDirDest = Path.Combine(newDest, newDirName);
                                        CreateZipFile(subdir.FullName, origSubDirDest, newSubDirDest, recursive, level, checkTFSActivity);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage(0, "Error while Creating Zip file Creation: " + ex.Message);
            }
        }

        private static void RefactorFile(FileInfo file, string dest, int level, bool checkTFSActivity)
        {
            Domain.FileType fileType = sandcastleConfiguration.FileTypes.Find(f => f.Extension.ToLower() == file.Extension.ToLower());
            bool fileQualifies = fileType != null;

            if (fileQualifies && !string.IsNullOrWhiteSpace(fileType.QualifyIfPathContains))
            {
                fileQualifies = file.DirectoryName.ToLower().Contains(fileType.QualifyIfPathContains.ToLower());
            }

            if (fileQualifies && !string.IsNullOrWhiteSpace(fileType.IgnoreIfPathContains))
            {
                fileQualifies = !file.DirectoryName.ToLower().Contains(fileType.IgnoreIfPathContains.ToLower());
            }

            if (fileQualifies)
            {
                string fileName = file.Name;
                string newFileName = fileName;

                string origPath = Path.Combine(dest, file.Name);
                string newPath = Path.Combine(dest, newFileName);

                string fileContents = File.ReadAllText(file.FullName);
                string newFileContents = fileContents;
                bool contentsChanged = false;

                if (sandcastleConfiguration.EditMode == Enumerations.EditModeTypes.Copy)
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
                    TfsPendAdd(newPath, checkTFSActivity);
                }
                else
                {
                    if (string.Compare(fileName, newFileName, true) != 0)
                    {
                        TfsPendRename(origPath, newPath, checkTFSActivity);
                        LogMessage(level, string.Format("Renaming file '{0}'", file.Name));
                    }

                    if (contentsChanged)
                    {
                        TfsPendEdit(newPath, checkTFSActivity);
                        File.WriteAllText(newPath, newFileContents);
                        LogMessage(level, string.Format("Refactoring file '{0}'", file.Name));
                    }
                }
            }
            else
            {
                if (sandcastleConfiguration.EditMode == Enumerations.EditModeTypes.Copy)
                {
                    string newPath = Path.Combine(dest, file.Name);
                    File.Copy(file.FullName, newPath);
                    File.SetAttributes(newPath, File.GetAttributes(newPath) & ~FileAttributes.ReadOnly);
                    TfsPendAdd(newPath, checkTFSActivity);
                    LogMessage(level, string.Format("Copying file '{0}'", file.Name));
                }
                else
                {
                    LogMessage(level, string.Format("Skipping file '{0}'", file.Name));
                }
            }
        }

        private static void TfsPendEdit(string path, bool checkPendEditOrNot)
        {
            bool retry = true;
            int retryAttempts = 0;
            if (checkPendEditOrNot == true)
            {
                while (retry && retryAttempts <= retryCount)
                {
                    try
                    {
                        if (localWorkSpace != null)
                        {
                            localWorkSpace.PendEdit(path);
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
        }

        private static void TfsUndo(string path, bool checkUndoOrNot)
        {
            bool retry = true;
            int retryAttempts = 0;
            if (checkUndoOrNot == true)
            {
                while (retry && retryAttempts <= retryCount)
                {
                    try
                    {
                        if (localWorkSpace != null)
                        {
                            localWorkSpace.Undo(path);
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

        private static void TfsPendAdd(string path, bool checkAddOrNot)
        {
            bool retry = true;
            int retryAttempts = 0;
            if (checkAddOrNot == true)
            {
                while (retry && retryAttempts <= retryCount)
                {
                    try
                    {
                        if (localWorkSpace != null)
                        {
                            localWorkSpace.PendAdd(path);
                            pendAddCount++;
                        }
                        else
                        {
                            //AuthenticateAzureTFS();
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

        private static void TfsPendRename(string oldPath, string newPath, bool checkPendRenameOrNot)
        {
            bool retry = true;
            int retryAttempts = 0;
            if (checkPendRenameOrNot == true)
            {
                while (retry && retryAttempts <= retryCount)
                {
                    try
                    {
                        if (localWorkSpace != null)
                        {
                            localWorkSpace.PendRename(oldPath, newPath);
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
        }

        private static string GetBranch()
        {
            string[] branch;

            branch = sandcastleConfiguration.currentBranch.Split('.');

            if (branch.LastOrDefault() != "0")
            {
                if (branch[2].Count() == 2 || branch[2] == "Dev")
                {
                    if (branch[2] == "Dev")
                    {
                        currentBranch = branch[2];
                    }
                    else
                    {
                        currentBranch = "0" + branch[2];
                    }
                }
                Program.branch = false;
            }else
            {
                currentBranch = "Dev";
            }
            
            return currentBranch;
        }

        #endregion

        #region .CHM Generation

        private static TfsTeamProjectCollection AuthenticateAzureTFS()
        {
            try
            {
                if (sandcastleConfiguration.UseSourceControl)
                {
                    LogMessage(0, string.Format("-------Initializing connection to AzureDevOps-------"));

                    NetworkCredential credential = new NetworkCredential(sandcastleConfiguration.TFSUserName, sandcastleConfiguration.TFSPassword);
                    BasicAuthCredential basicCredntial = new BasicAuthCredential(credential);
                    TfsClientCredentials tfsCredntial = new TfsClientCredentials(basicCredntial);
                    tpc = new TfsTeamProjectCollection(new Uri(sandcastleConfiguration.TfsServer), tfsCredntial);
                    tfsCredntial.AllowInteractive = false;
                    tpc.EnsureAuthenticated();
                    VersionControlServer versionControl = tpc.GetService<VersionControlServer>();
                    versionControl.NonFatalError += OnNonFatalError;
                    versionControl.Getting += OnGetting;
                    versionControl.BeforeCheckinPendingChange += OnBeforeCheckinPendingChange;
                    versionControl.NewPendingChange += OnNewPendingChange;
                    localWorkSpace = versionControl.QueryWorkspaces(sandcastleConfiguration.TfsWorkspace, versionControl.AuthorizedUser, Environment.MachineName)[0];
                    Workstation.Current.EnsureUpdateWorkspaceInfoCache(versionControl, versionControl.AuthorizedUser);
                    LogMessage(0, string.Format("-------Successfully connected to AzureDevOps-------"));
                }
            }
            catch (Exception ex)
            {
                LogMessage(0, string.Format("-------Please Connect to Pluse and Try again-------", ex));
                return tpc;
            }
            return tpc;
        }

        private static void GetLatestAndBuildSolution()
        {
            try
            {
                string severPath, workSpace;

                GetBranch();
                
                for (int i = 0; i < modulesFromSource.Count(); i++)
                {
                    bool exists;

                    var tempPath = string.Empty;

                    if (!modulesFromSource[i].ModuleName.ToLower().Contains(("Portal").ToLower()))
                    {
                        tempPath = "\\" + currentBranch + "\\BAS";
                    }
                    else
                    {
                        tempPath = "\\" + currentBranch + "\\UX";
                    }

                    severPath = modulesFromSource[i].Serverpath + tempPath;
                    workSpace = sandcastleConfiguration.SourceDir + Path.GetFileName(modulesFromSource[i].Serverpath) + tempPath;
                    exists = Directory.Exists(workSpace);
                    CheckDirectory(workSpace);
                    GetLatestcode(severPath, workSpace);
                    BuildSolution(workSpace.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void GetLatestcode(string severPath, string workSpace)
        {
            try
            {
                if (localWorkSpace == null)
                {
                    AuthenticateAzureTFS();
                }
                localWorkSpace.GetWorkingFolderForServerItem(severPath);
                localWorkSpace.GetWorkingFolderForLocalItem(workSpace);
                localWorkSpace.GetLocalItemForServerItem(severPath);
                GetRequest request = new GetRequest(new ItemSpec(severPath, RecursionType.Full), VersionSpec.Latest);
                GetStatus status = localWorkSpace.Get(request, GetOptions.GetAll | GetOptions.Overwrite);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Getting All Main & Contratct projects path 
        /// </summary>
        /// <param name="workSpace"></param>
        private static void BuildSolution(string workSpace)
        {
            try
            {
                string[] foldersUnderBase;
                string[] filesUnderFolders;

                List<string> buildProjects = new List<string>();

                var sModuleName = workSpace.Split('\\');
                LogMessage(0, string.Format("-------Compailing the Latest code for " + sModuleName[3] + " Module-------"));
                foldersUnderBase = Directory.GetDirectories(workSpace);
                var uxSolutionFile = Directory.GetFiles(workSpace).SingleOrDefault(a => a.EndsWith(".sln"));
                if (uxSolutionFile != null)
                {
                    if (uxSolutionFile.Contains("UX"))
                    {
                        SendUXSolutionFileTOBuild(uxSolutionFile);
                        uxSolutionFile = string.Empty;
                    }
                    else
                    {
                        SendSolutionFileToBuild(uxSolutionFile);
                        uxSolutionFile = string.Empty;
                    }
                }
                for (int x = 0; x < foldersUnderBase.Length; x++)
                {
                    var sfoldername = foldersUnderBase[x].Split('\\').LastOrDefault();

                    var getMainSol = Directory.GetFiles(foldersUnderBase[x]);
                    var projcetname = string.Empty;
                    if (!sfoldername.Contains("Portal"))
                    {
                        if (getMainSol.Count() > 0)
                        {
                            getMainSol = Array.FindAll(getMainSol, c => c.Contains(".sln") || c.Contains(".csproj"));
                            if (getMainSol.Count() > 0)
                            {
                                var projOrSol = getMainSol.FirstOrDefault().ToString();
                                if (projOrSol.EndsWith(".sln"))
                                {
                                    SendSolutionFileToBuild(projOrSol);
                                }
                                GetProjectFile(sfoldername, projOrSol, foldersUnderBase[x], workSpace);
                            }
                        }
                    }
                    else
                    {
                        filesUnderFolders = Directory.GetFiles(foldersUnderBase[x]);
                        projcetname = filesUnderFolders.Where(a => a.Contains(".Providers.csproj")).FirstOrDefault();
                        if (projcetname != null && !projcetname.Contains("Tests") && !projcetname.Contains("Test"))
                        {
                            SendUXProjFileToBuild(projcetname);
                        }
                    }
                }
                LogMessage(0, string.Format("-------Compailing the Latest code completed for " + sModuleName[3] + " Module-------"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void GetProjectFile(string folderName, string projOrSol, string foldersUnderBase, string workSpace)
        {
            if (!projOrSol.Contains("Tests") || !projOrSol.Contains("Test"))
            {
                var folders = Directory.GetDirectories(foldersUnderBase);
                if (folders.Count() > 0 && !projOrSol.EndsWith(".csproj"))
                {
                    for (int y = 0; y < folders.Length; y++)
                    {
                        var filesUnderFolders = Directory.GetFiles(folders[y]);
                        if (filesUnderFolders.Any(a => a.EndsWith(".csproj")) || filesUnderFolders.Any(a => a.EndsWith(".sln")))
                        {
                            projOrSol = filesUnderFolders.Where(a => a.EndsWith(".csproj") || filesUnderFolders.Any(b => b.EndsWith(".sln"))).FirstOrDefault();
                            if (!projOrSol.Contains("Test"))
                            {
                                if (!workSpace.Contains("Notification"))
                                {
                                    if (projOrSol.EndsWith("." + folderName + ".csproj") || projOrSol.EndsWith("." + folderName + ".Contracts.csproj")
                                        || projOrSol.EndsWith("." + folderName + ".OData.csproj") || projOrSol.Contains("." + folderName)
                                        || projOrSol.EndsWith(".csproj"))
                                    {
                                        SendProjectFileToBuild(projOrSol);
                                    }
                                }
                                else
                                {
                                    if (projOrSol.EndsWith(".csproj") && projOrSol.Contains(folderName) && !projOrSol.Contains("Test") || projOrSol.EndsWith(".Contracts.csproj"))
                                    {
                                        SendProjectFileToBuild(projOrSol);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    SendProjectFileToBuild(projOrSol);
                }
            }
        }

        private static void SendProjectFileToBuild(string basProjectPath)
        {
            if (!basProjectPath.Contains("Test"))
            {
                string projconfig = basProjectPath + " " + sandcastleConfiguration.BuildOutputDirectory; //'"' + " /p:platform=" + "x64";
                RunBatFiles(SandcastleConfig.BuildBasProjectBatFile, projconfig);
            }
        }
        private static void SendSolutionFileToBuild(string solutionPath)
        {
            if (!solutionPath.Contains("Test"))
            {
                RunBatFiles(SandcastleConfig.BuildBasSolutionBatFile, solutionPath);
            }
        }
        private static void SendUXSolutionFileTOBuild(string uxSolutionPath)
        {
            RunBatFiles(SandcastleConfig.BuildUXSolutionBatFile, uxSolutionPath);
        }
        private static void SendUXProjFileToBuild(string uxProjectPath)
        {
            string projconfig = uxProjectPath + " " + sandcastleConfiguration.BuildOutputDirectory;
            RunBatFiles(SandcastleConfig.BuildUXProjectBatFile, projconfig);
        }

        /// <summary>
        /// Getting all modules from TFS and based on module creating floder structer.
        /// </summary>
        private static void NewSandcastleFolderstructure()
        {
            try
            {
                string serverPath = string.Empty; string workSpacePath = string.Empty;

                if (enterWrongBranch == false)
                {
                    for (int i = 0; i < modulesFromDocumentation.Count(); i++)
                    {
                        serverPath = modulesFromDocumentation[i].Serverpath.ToString();

                        workSpacePath = modulesFromDocumentation[i].Workspacepath;

                        if (!Directory.Exists(workSpacePath + "\\" + sandcastleConfiguration.PrevBranch))
                        {
                            GetLatestcode(serverPath + "//" + sandcastleConfiguration.PrevBranch, workSpacePath + "\\" + sandcastleConfiguration.PrevBranch);
                            FileAttributes attributes = File.GetAttributes(workSpacePath);
                            attributes = File.GetAttributes(workSpacePath);
                            attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
                        }
                        CreateFolder(workSpacePath, sandcastleConfiguration.currentBranch);
                    }
                }
                else
                {
                    LogMessage(0, "-------Your enterded wrong Branch. please check and start again-------");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creating Folder structure
        /// </summary>
        /// <param name="worklocation"></param>
        /// <param name="patchOrModule"></param>
        public static void CreateFolder(string worklocation, string patchOrModule)
        {
            try
            {
                string patchOrRelease; string previousPatchOrReleasePath; string currentPatchOrReleasePath;

                previousPatchOrReleasePath = worklocation + @"\" + sandcastleConfiguration.PrevBranch;
                currentPatchOrReleasePath = worklocation + @"\" + patchOrModule;

                patchOrRelease = currentPatchOrReleasePath;

                List<string> folders = new List<string>() { patchOrRelease };

                FileAttributes attributes = File.GetAttributes(worklocation);
                attributes = File.GetAttributes(worklocation);
                attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);

                for (int i = 0; i < folders.Count(); i++)
                {
                    CheckDirectory(folders[i]);
                    TfsPendAdd(folders[i], true);
                }
                GetSandCastleSolutionFromRecentBranch(previousPatchOrReleasePath, currentPatchOrReleasePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Copy sandcastle solution to New patch.
        /// </summary>
        /// <param name="previousPatchOrReleasePath"></param>
        /// <param name="currentPatchOrReleasePath"></param>
        public static void GetSandCastleSolutionFromRecentBranch(string previousPatchOrReleasePath, string currentPatchOrReleasePath)
        {
            try
            {
                if (previousPatchOrReleasePath != null && currentPatchOrReleasePath != null)
                {
                    DirectoryInfo sourceDir = new DirectoryInfo(previousPatchOrReleasePath);
                    DirectoryInfo DestDir = new DirectoryInfo(currentPatchOrReleasePath);
                    FileInfo[] sourceFiles = sourceDir.GetFiles();

                    foreach (FileInfo sFiles in sourceFiles)
                    {
                        if (sFiles.Name.Contains(".shfbproj"))
                        {
                            if (!Directory.GetFiles(currentPatchOrReleasePath).Any(x => x.EndsWith(sFiles.Name)))
                            {
                                File.Copy(sFiles.FullName, Path.Combine(currentPatchOrReleasePath, sFiles.Name), true);
                                FileAttributes attributes = File.GetAttributes(sFiles.FullName);
                                attributes = File.GetAttributes(sFiles.FullName);
                                attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
                            }
                            CheckCopyFileContent(currentPatchOrReleasePath + "\\" + sFiles.Name, Path.Combine(currentPatchOrReleasePath, sFiles.Name));
                        }
                        else
                        {
                            LogMessage(0, string.Format("Solution project(Shfproj) is not available in previous Branch or Release:" + previousPatchOrReleasePath));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Copy the solution in sandcastle documantation.
        /// </summary>
        /// <param name="fileName"></param>
        private static void CheckCopyFileContent(string fileName, string destinationPath)
        {
            string currentBranchName = null; string previousBranchName = null;
            string[] filesArray = fileName.Split(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            string fileContents = File.ReadAllText(fileName);
            string newFileContents = fileContents;
            currentBranchName = sandcastleConfiguration.currentBranch.Split('.')[2];
            previousBranchName = sandcastleConfiguration.PrevBranch.Split('.')[2];

            if (currentBranchName.Count() == 2)
            {
                currentBranchName = "0" + currentBranchName;
            }
            if (previousBranchName.Count() == 2)
            {
                previousBranchName = "0" + previousBranchName;
            }
            try
            {
                bool contentsChanged = false;
                var rows = File.ReadAllLines(fileName);
                var outPutDir = string.Empty;
                if (sandcastleConfiguration.IncludeFileContents)
                {
                    for (int rowIndex = 0; rowIndex != rows.Length; rowIndex++)
                        if (rows[rowIndex].Contains("DocumentationSource") && rows[rowIndex].Contains("Dev") || rows[rowIndex].Contains("DocumentationSource") && rows[rowIndex].Contains(previousBranchName))
                        {
                            string lineContent = string.Empty;
                            var rowcontent = rows[rowIndex].Split('\\');

                            foreach (var eachitem in rowcontent)
                            {
                                outPutDir = @"\bin\Release\";

                                if (eachitem == "bin" || eachitem == "Release" || eachitem == "x64")
                                {
                                    lineContent += @"\" + eachitem;
                                }
                            }
                            if (lineContent.Contains("x64"))
                            {
                                outPutDir = lineContent + @"\";
                            }
                            rows[rowIndex] = rows[rowIndex].Replace("Dev", currentBranchName).Replace(lineContent + @"\", outPutDir);
                            contentsChanged = true;
                        }
                    File.SetAttributes(fileName, File.GetAttributes(fileName) & ~FileAttributes.ReadOnly);
                    File.WriteAllLines(fileName, rows);
                }

                if (sandcastleConfiguration.EditMode == Enumerations.EditModeTypes.Copy)
                {
                    File.SetAttributes(destinationPath, File.GetAttributes(destinationPath) & ~FileAttributes.ReadOnly);
                    RunBatFiles(SandcastleConfig.GenerateCHMFilesBatFile, fileName);
                    AddingAllPendChangesUnderPath(Path.GetDirectoryName(destinationPath), true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static bool RefactorFileContents(string filePath, string currentBranch, out string refactoredFileContents)
        {
            bool contentsChanged = false;
            refactoredFileContents = string.Empty;
            var crntBranchName = sandcastleConfiguration.currentBranch.Split('.')[2];
            var prevBranchName = sandcastleConfiguration.PrevBranch.Split('.')[2];
            var sBranchCount = currentBranch.Count();
            string[] prvBranchName = sandcastleConfiguration.PrevBranch.Split('.');

            if (currentBranch.Count() == 2)
            {
                crntBranchName = "0" + crntBranchName;
            }
            if (prevBranchName.Count() == 2)
            {
                prevBranchName = "0" + prevBranchName;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter writer = new StreamWriter(ms))
                    {
                        string line = null;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains("DocumentationSource") && line.Contains("Dev") || line.Contains("DocumentationSource") && line.Contains(prevBranchName))
                            {
                                string[] releaseOrPatch = { "Dev", prevBranchName };
                                foreach (var eachItem in releaseOrPatch)
                                {
                                    if (line.Contains(eachItem))
                                    {
                                        var changedContent = Regex.Replace(line, eachItem, crntBranchName);
                                        contentsChanged = true;
                                    }
                                }
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

        #endregion

        #region WSDLS 

        private static void GetServicesFromServiceURLs()
        {

            LogMessage(0, string.Format("-------WSDLs Generation Started-------"));

            LogMessage(0, string.Format("-------Getting All services from TFS-------"));

            wsdlURLS = new List<string>();

            servicesFromServiceURL = new List<string>();

            try
            {
                var enviroment = serviceEnvironments.FirstOrDefault(x => x.Active == "True").Serverpath.Trim();

                HttpWebRequest myRequest;

                var regex = new Regex("<a [^>]*href=(?:'(?<href>.*?)')|(?:\"(?<href>.*?)\")", RegexOptions.IgnoreCase);

                myRequest = (HttpWebRequest)WebRequest.Create(enviroment);

                myRequest.Method = "GET";

                var resp = (HttpWebResponse)myRequest.GetResponse();

                var result = new StreamReader(resp.GetResponseStream()).ReadToEnd();

                wsdlURLS = regex.Matches(result).OfType<Match>().Select(m => m.Groups["href"].Value).ToList();

                try
                {
                    downServices = new List<string>();
                    foreach (var servicemodules in wsdlURLS.Where(x => x.StartsWith("/HP")).ToList())
                    {
                        try
                        {
                            if (servicemodules.Contains(".zip"))
                            {
                                break;
                            }
                            var formingURL = enviroment + servicemodules + "R6.0/";
                            myRequest = (HttpWebRequest)WebRequest.Create(formingURL);
                            myRequest.Method = "GET";
                            var step1 = (HttpWebResponse)myRequest.GetResponse();
                            var responce1 = new StreamReader(step1.GetResponseStream()).ReadToEnd();
                            List<string> lstTemp1 = regex.Matches(responce1).OfType<Match>().Select(m => m.Groups["href"].Value).ToList();

                            if (!servicemodules.Contains("ManagedCare"))
                            {
                                foreach (var ursl in lstTemp1.Where(x => x.EndsWith(".svc")))
                                {
                                    try
                                    {
                                        formingURL = enviroment + ursl;
                                        myRequest = (HttpWebRequest)WebRequest.Create(formingURL);
                                        myRequest.Method = "GET";
                                        var step2 = (HttpWebResponse)myRequest.GetResponse();
                                        var responce2 = new StreamReader(step2.GetResponseStream()).ReadToEnd();
                                        servicesFromServiceURL.Add(formingURL);
                                    }
                                    catch (Exception ex)
                                    {
                                        LogMessage(0, string.Format("------------------------------------"));
                                        LogMessage(0, string.Format("Error:" + servicemodules + ex));
                                        downServices.Add(enviroment + servicemodules);
                                        LogMessage(0, string.Format("------------------------------------"));
                                    }
                                }
                            }
                            else
                            {
                                foreach (var ursl in lstTemp1.Where(x => x.EndsWith(".svc")))
                                {
                                    formingURL = enviroment + ursl;
                                    myRequest = (HttpWebRequest)WebRequest.Create(formingURL);
                                    myRequest.Method = "GET";
                                    var step2 = (HttpWebResponse)myRequest.GetResponse();
                                    var responce2 = new StreamReader(step2.GetResponseStream()).ReadToEnd();
                                    servicesFromServiceURL.Add(formingURL);

                                    List<string> lstTemp2 = regex.Matches(responce1).OfType<Match>().Select(m => m.Groups["href"].Value).ToList();

                                    foreach (var folderNames in subFoldersInSideManagedCare)
                                    {
                                        var folderCheck = lstTemp2.Where(x => x.Contains(folderNames)).FirstOrDefault();
                                        if (folderCheck != null)
                                        {
                                            formingURL = enviroment + folderCheck;
                                            myRequest = (HttpWebRequest)WebRequest.Create(formingURL);
                                            myRequest.Method = "GET";
                                            var step3 = (HttpWebResponse)myRequest.GetResponse();
                                            var responce3 = new StreamReader(step3.GetResponseStream()).ReadToEnd();
                                            List<string> lstTemp3 = regex.Matches(responce3).OfType<Match>().Select(m => m.Groups["href"].Value).ToList();
                                            foreach (var subFolderursl in lstTemp3.Where(x => x.EndsWith(".svc")))
                                            {
                                                formingURL = enviroment + subFolderursl;
                                                myRequest = (HttpWebRequest)WebRequest.Create(formingURL);
                                                myRequest.Method = "GET";
                                                var step4 = (HttpWebResponse)myRequest.GetResponse();
                                                var responce4 = new StreamReader(step4.GetResponseStream()).ReadToEnd();
                                                servicesFromServiceURL.Add(formingURL);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogMessage(0, string.Format("------------------------------------"));
                            LogMessage(0, string.Format("Error:" + servicemodules + ex));
                            downServices.Add(enviroment + servicemodules);
                            LogMessage(0, string.Format("------------------------------------"));
                            sflag = true;
                            //throw ex;
                        }
                    }
                    LogMessage(0, string.Format("-------Succefully getting all services from TFS-------"));
                    GetServicesFromModules();

                }
                catch (Exception ex)
                {
                    LogMessage(0, "Error:WSDLs Generation " + ex.Message);
                }
            }
            catch
            {
                LogMessage(0, string.Format("-------Please Conncet to Pluse and Try again-------"));
                return;
            }
        }

        private static void GetServicesFromModules()
        {
            try
            {
                if (servicesFromServiceURL != null)
                {
                    servicesFromModules = new List<string>();
                    try
                    {
                        FindNewServies();

                        if (missingServicesFromConfigurationOrServiceURL.Count() > 0)
                        {
                            LogMessage(0, "-------Some ServiceURLS are Down Please Verify Log file-------");
                            foreach (var servicesURLsDown in missingServicesFromConfigurationOrServiceURL)
                            {
                                LoggerManager.Logger.LogWarning(servicesURLsDown);
                            }
                            LoggerManager.Logger.LogWarning("---------------------------------------------------");
                        }

                        if (serviceURLsConfigurationMissing.Count() > 0)
                        {
                            LogMessage(0, "-------Below List of services are Not available in ServiceURLConfig.txt please configure Start Again-------");
                            foreach (var newServicesURL in serviceURLsConfigurationMissing)
                            {
                                LogMessage(0, string.Format(newServicesURL));
                            }
                            LoggerManager.Logger.LogWarning("---------------------------------------------------");

                            return;
                        }
                        for (int i = 0; i < modulesFromSource.Count(); i++)
                        {
                            if (!modulesFromSource[i].ModuleName.Contains("Portal"))
                            {
                                GeneratingWSDLS(modulesFromSource[i].ModuleName);
                            }
                        }

                        LogMessage(0, string.Format("-------WSDLs Generation completed succefully-------"));
                    }
                    catch (Exception ex)
                    {
                        LogMessage(0, "Error:" + ex.Message);
                    }
                }
                else
                {
                    LogMessage(0, string.Format("-------Error:No Service are avalible please re-run again-------"));
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// WSDLS Generation based on NewFilepath
        /// </summary>
        /// <param name="newFilepath"></param>
        public static void GeneratingWSDLS(string sModuleName)
        {
            try
            {
                servicesFromModules = servicesFromConfiguration.Where(x => x.Split(',')[1].EndsWith(sModuleName)).ToList();
                servicesFromModules = servicesFromModules.Select(x => x.Substring(0, x.IndexOf(',') > 1 ? x.IndexOf(',') : x.Length)).ToList();

                string activeServerName, wSDLSFolderpath = null;
                var ac = sModuleName;
                LoggerManager.Logger.LogInformational("-------WSDLs generation is Started-------");
                bool checkModuleAvalibleOrNot;
                if (sModuleName == "Eventing")
                {
                    modulesFromDocumentation.Where(w => w.ModuleName == "Administration").Select(w => w.ModuleName = "Eventing").ToList();
                }
                checkModuleAvalibleOrNot = modulesFromDocumentation.Any(x => x.ModuleName == sModuleName);

                if (checkModuleAvalibleOrNot == true)
                {
                    var workSpcePath = modulesFromDocumentation.Where(y => y.ModuleName == sModuleName).FirstOrDefault().Workspacepath + "\\" + sandcastleConfiguration.currentBranch + "\\WSDLs";

                    wSDLSFolderpath = workSpcePath;

                    CheckDirectory(wSDLSFolderpath);

                    foreach (var activeEnvironemt in serviceEnvironments)
                    {
                        if (activeEnvironemt.Active == "True")
                        {
                            activeServerName = activeEnvironemt.Serverpath;

                            foreach (var eachServicesFromModules in servicesFromModules)
                            {
                                checkServiceURlActiveOrNot(eachServicesFromModules);

                                var checkContainOrNot = downServices.Find(x => x.Contains(eachServicesFromModules));
                                if (sflag == false)
                                {

                                    var WsdlGenURL = eachServicesFromModules + "?wsdl";
                                    RunBatFiles(SandcastleConfig.GenerateWSDLsBatFile, WsdlGenURL + " /d:" + wSDLSFolderpath);
                                }
                                else
                                {
                                    LogMessage(0, string.Format("-------Error:Service down:" + eachServicesFromModules + "-------"));
                                }

                                sflag = false;
                            }
                        }
                    }

                    directoryInfo = new DirectoryInfo(wSDLSFolderpath);
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        if (file.Extension.ToLower() != ".wsdl")
                            file.Delete();
                    }
                    AddingAllPendChangesUnderPath(Path.GetDirectoryName(directoryInfo.FullName), true);
                    servicesFromModules.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void checkServiceURlActiveOrNot(string serviceURL)
        {
            sflag = false;

            try
            {
                HttpWebRequest myRequest;
                myRequest = (HttpWebRequest)WebRequest.Create(serviceURL);
                myRequest.Method = "GET";
                var resp = (HttpWebResponse)myRequest.GetResponse();
                var result = new StreamReader(resp.GetResponseStream()).ReadToEnd();
            }
            catch
            {
                sflag = true;
            }
        }
        public static void FindNewServies()
        {
            try
            {
                servicesFromConfiguration = new List<string>();
                var ServiceURLConfigFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"..\..\..\ServiceURLConfig.txt";

                using (StreamReader reader = new StreamReader(ServiceURLConfigFilePath))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (StreamWriter writer = new StreamWriter(ms))
                        {
                            eachModuleServices = new List<string>();
                            servicesFromModules = new List<string>();
                            foreach (var activServiceEnviroment in serviceEnvironments)
                            {
                                if (activServiceEnviroment.Active == "True")
                                {
                                    string line = null;

                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        if (line != string.Empty)
                                        {
                                            var ServiceFrmConfigFile = activServiceEnviroment.Serverpath.Trim() + line.Split(':')[1] + "," + line.Split(':')[0];
                                            servicesFromConfiguration.Add(ServiceFrmConfigFile);
                                        }
                                    }

                                    missingServicesFromConfigurationOrServiceURL = new List<string>();
                                    missingServicesFromConfigurationOrServiceURL.AddRange(servicesFromConfiguration.Select(x => x.Substring(0, x.IndexOf(',') > 1 ? x.IndexOf(',') : x.Length)).Except(servicesFromServiceURL, StringComparer.OrdinalIgnoreCase));
                                    serviceURLsConfigurationMissing = new List<string>();
                                    serviceURLsConfigurationMissing.AddRange(servicesFromServiceURL.Except(servicesFromConfiguration.Select(x => x.Substring(0, x.IndexOf(',') > 1 ? x.IndexOf(',') : x.Length)), StringComparer.OrdinalIgnoreCase));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region XSDLS

        private static void GenerateXSDLS()
        {
            var pathFormate = string.Empty; var getSolutionFile = string.Empty; string localWorkSpacePath; string serverPath; string dllPath;

            LogMessage(0, string.Format("-------XSDs Generation Started-------"));

            currentBranch = sandcastleConfiguration.currentBranch.Split('.')[2];

            if (currentBranch.Count() == 2)
            {
                currentBranch = "0" + currentBranch;
            }
            try
            {
                foreach (var moduleName in sandcastleConfiguration.DependencyModules)
                {
                    pathFormate = @"Core\" + currentBranch + @"\API\" + moduleName;

                    localWorkSpacePath = sandcastleConfiguration.SourceDir + pathFormate;
                    serverPath = sandcastleConfiguration.ServerDir + "/Source/" + pathFormate;
                    dllPath = string.Empty;

                    if (moduleName != "Logger")
                    {
                        dllPath = Directory.GetDirectories(localWorkSpacePath).Where(x => x.EndsWith("Providers")).SingleOrDefault() + @"\bin\x64\Release";
                    }
                    else
                    {
                        dllPath = Directory.GetDirectories(localWorkSpacePath).Where(x => x.EndsWith(moduleName)).SingleOrDefault() + @"\bin\x64\Release";
                    }

                    getSolutionFile = Directory.GetFiles(localWorkSpacePath).Where(x => x.EndsWith(".sln")).SingleOrDefault();
                    RunBatFiles(SandcastleConfig.BuildCoreAPISolutionBatFile, localWorkSpacePath);
                    DependencyDlls(moduleName, dllPath);
                }

                GetLocalWorkSpacePath();
                LogMessage(0, string.Format("-------XSDs Generation completed succefully-------"));
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogError("Error " + "" + "{0}", ex);
                throw ex;
            }
        }

        private static void GetLocalWorkSpacePath()
        {
            string serverPath; string localWorkSpacePath; string sandcastleSolutionPath; string dllFolderPath = string.Empty;
            var foldername = string.Empty;

            string branchName = GetBranch();

            try
            {
                foreach (var module in modulesFromSource)
                {
                    if (!module.ModuleName.Contains("Portal"))
                    {
                        localWorkSpacePath = @"\" + branchName + @"\BAS";

                        serverPath = module.Serverpath + localWorkSpacePath;

                        if (module.ModuleName == "Eventing")
                        {
                            modulesFromDocumentation.Where(w => w.ModuleName == "Administration").Select(w => w.ModuleName = "Eventing").ToList();
                        }
                        localWorkSpacePath = sandcastleConfiguration.SourceDir + Path.GetFileName(module.Serverpath) + localWorkSpacePath;

                        sandcastleSolutionPath = modulesFromDocumentation.Any(x => x.ModuleName == module.ModuleName).ToString();

                        if (sandcastleSolutionPath == "True")
                        {
                            sandcastleSolutionPath = modulesFromDocumentation.Where(x => x.ModuleName == module.ModuleName).FirstOrDefault().Workspacepath + "\\" + sandcastleConfiguration.currentBranch.ToString() + "\\XSDs";

                            GenerateXSDs(localWorkSpacePath, sandcastleSolutionPath);
                        }
                        else
                        {
                            LogMessage(0, "Excluded:Unable to find this moduleName in module configuration " + module.ModuleName);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void GenerateXSDs(string localWorkSpacePath, string sandcastleSolutionPath)
        {
            string dllFolderPath = string.Empty; var foldername = string.Empty; string solutionFilePath; string outputDirectory;

            try
            {
                directoryInfo = new DirectoryInfo(localWorkSpacePath);

                foreach (var path in directoryInfo.GetDirectories())
                {
                    dllFolderPath = string.Empty;

                    if (!path.FullName.Contains("Test"))
                    {
                        solutionFilePath = Directory.GetFiles(path.FullName.ToString()).Where(x => x.EndsWith(".sln")).SingleOrDefault();

                        if (solutionFilePath != null)
                        {
                            var getFileName = Path.GetFileName(solutionFilePath.ToString()).Split('.');
                            foldername = string.Empty;
                            foreach (var filename in getFileName)
                            {
                                if (filename != "sln")
                                    foldername = foldername + filename + ".";
                            }

                            if (!solutionFilePath.ToString().Contains(".BroadcastMessage") && !solutionFilePath.ToString().Contains(".Tests"))
                            {
                                var getContractProjectPath = Directory.Exists(Directory.GetDirectories(path.FullName).Where(x => x.EndsWith(".Contracts")).SingleOrDefault());

                                if (getContractProjectPath == true)
                                {
                                    var projectFile = Directory.GetFiles(Directory.GetDirectories(path.FullName).Where(x => x.EndsWith(".Contracts")).FirstOrDefault().ToString()).Where(x => x.EndsWith(".Contracts.csproj")).FirstOrDefault();

                                    if (projectFile != null)
                                    {
                                        RunBatFiles(SandcastleConfig.BuildBasProjectBatFile, projectFile.ToString() + " " + sandcastleConfiguration.BuildOutputDirectory);// + '"' + " /p:platform=" + "x64");

                                    }
                                    var dllPath = Directory.GetDirectories(Directory.GetDirectories
                                                             (Directory.GetDirectories(path.FullName)
                                                            .Where(x => x.EndsWith(".Contracts")).SingleOrDefault())
                                                            .Where(y => y.EndsWith("bin")).SingleOrDefault()).ToList();

                                    if (dllPath.Count > 0)
                                    {
                                        foreach (string sFindReleaseFolder in dllPath)
                                        {
                                            if (sFindReleaseFolder.Contains("Release"))
                                            {
                                                dllFolderPath = sFindReleaseFolder;
                                            }
                                        }

                                        if (dllFolderPath != string.Empty)
                                        {
                                            var xsdGenerationURL = Directory.GetFiles(dllFolderPath).Where(x => x.EndsWith(foldername + "Contracts.dll")).FirstOrDefault();

                                            CopyDependencyDlls(dllFolderPath);

                                            outputDirectory = sandcastleSolutionPath + "\\" + foldername;


                                            xsdGenerationURL = xsdGenerationURL + " /d:" + outputDirectory;

                                            if (outputDirectory.Contains("Eventing"))
                                            {
                                                if (outputDirectory.Contains("BAS.EventAdmin"))
                                                {
                                                    CheckDirectory(outputDirectory.Trim());
                                                    RunBatFiles(SandcastleConfig.GenerateXSDsBatFile, xsdGenerationURL);
                                                }
                                            }
                                            else
                                            {
                                                CheckDirectory(outputDirectory.Trim());
                                                RunBatFiles(SandcastleConfig.GenerateXSDsBatFile, xsdGenerationURL);
                                            }
                                            AddingAllPendChangesUnderPath(Path.GetDirectoryName(Path.GetDirectoryName(outputDirectory)), true);
                                        }
                                        else
                                        {
                                            LogMessage(0, "Excluded:Unable to find Contract Dll for " + Path.GetFileName(solutionFilePath));
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        LogMessage(0, "Excluded:Unable to find Release/Debug folders for " + Path.GetFileName(solutionFilePath));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage(0, "Error " + ex.Message);
            }
        }

        private static void CheckEventingOrNot(string xsdGenerationURL)
        {
            try
            {
                RunBatFiles(SandcastleConfig.GenerateXSDsBatFile, xsdGenerationURL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CheckDirectory(string workSpacePath)
        {
            try
            {
                directoryInfo = new DirectoryInfo(workSpacePath);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                    TfsPendAdd(workSpacePath, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CopyDependencyDlls(string dllsFolderPath)
        {
            var sDependencyDlls = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\TempDependencyDlls"));
            if (Directory.Exists(sDependencyDlls) && dllsFolderPath != string.Empty)
            {
                foreach (string sfiles in Directory.GetFiles(sDependencyDlls, "*.*", SearchOption.AllDirectories))
                {
                    FileAttributes attributes = File.GetAttributes(sDependencyDlls);
                    attributes = File.GetAttributes(dllsFolderPath);
                    attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
                    File.Copy(sfiles, dllsFolderPath + "\\" + Path.GetFileName(sfiles), true);
                }
            }
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        public static void DependencyDlls(string module, string dllFilepath)
        {
            string[] constants = { "Interface", "Providers" }; string file; var dependencydllsPath = string.Empty;

            List<string> coreApiDlls = new List<string>();

            var dll = string.Empty;

            if (module != "Logger")
            {
                foreach (var sConstant in constants)
                {
                    dll = Directory.GetFiles(dllFilepath).Where(x => x.EndsWith(sConstant + ".dll")).SingleOrDefault();
                    coreApiDlls.Add(dll);
                }
            }
            else
            {
                dll = Directory.GetFiles(dllFilepath).Where(x => x.EndsWith("Logger.dll")).SingleOrDefault();
                coreApiDlls.Add(dll);
            }

            foreach (var coreAPIDll in coreApiDlls)
            {
                if (coreAPIDll != null)
                {
                    file = Path.GetFileName(coreAPIDll.ToString());

                    dependencydllsPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\TempDependencyDlls"));
                    CheckDirectory(dependencydllsPath);
                    Directory.GetFiles(dependencydllsPath).Where(x => x.EndsWith(file)).SingleOrDefault();
                    File.Copy(coreAPIDll, dependencydllsPath + "\\" + file, true);
                }
            }
        }

        #endregion

        #region WSDLS Difference  

        private static void GenerateWSDLSDifference()
        {
            string currentBranchOrReleasePath; string oldWSDLFolder; string newWDSDLFolder; string newWSDLDiffFolder;

            LogMessage(0, string.Format("-------WSDLs-Diff Generation Started-------"));

            foreach (var module in modulesFromDocumentation)
            {
                if (!module.ModuleName.Contains("Portal"))
                {
                    currentBranchOrReleasePath = module.Workspacepath + "\\" + sandcastleConfiguration.currentBranch;
                    oldWSDLFolder = module.Workspacepath + "\\" + sandcastleConfiguration.PrevBranch + "\\WSDLs";
                    newWDSDLFolder = module.Workspacepath + "\\" + sandcastleConfiguration.currentBranch + "\\WSDLs";
                    newWSDLDiffFolder = currentBranchOrReleasePath + "\\WSDL-Diff-Reports";

                    if (Directory.Exists(oldWSDLFolder) && Directory.Exists(newWDSDLFolder))
                    {
                        if (Directory.GetFiles(oldWSDLFolder).Count() > 0 && Directory.GetFiles(newWDSDLFolder).Count() > 0)
                        {
                            CheckDirectory(newWSDLDiffFolder);
                            currentBranchOrReleasePath = currentBranchOrReleasePath + "\\WSDLs";

                            if (Directory.GetFiles(currentBranchOrReleasePath).ToList().Count() > 0)
                            {
                                foreach (var currentWsdl in Directory.GetFiles(currentBranchOrReleasePath).Where(x => x.EndsWith(".wsdl")))
                                {
                                    var oldWsdlServics = Directory.GetFiles(oldWSDLFolder).Where(x => x.EndsWith(".wsdl")).ToList();
                                    var wsdlsPresentOrNot = oldWsdlServics.Where(x => x.Contains(Path.GetFileName(currentWsdl))).SingleOrDefault();

                                    if (wsdlsPresentOrNot != null)
                                    {
                                        foreach (var sOldWsdls in Directory.GetFiles(oldWSDLFolder))
                                        {
                                            if (Path.GetFileName(sOldWsdls) == Path.GetFileName(currentWsdl) && !sOldWsdls.Contains("tempuri.org.wsdl"))
                                            {
                                                var destination = newWSDLDiffFolder + "\\" + Path.GetFileName(currentWsdl);
                                                CheckDirectory(destination);
                                                RunWSDLBatFiles(SandcastleConfig.GenerateWSDLsDifferenceBatFile, sOldWsdls + " " + currentWsdl + " " + destination);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        LogMessage(0, string.Format("-------" + Path.GetFileName(currentWsdl) + " Service is not available in previous patch Not able to Create WSDLs-Diff-Report-------"));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        LogMessage(0, string.Format("-------WSDLs are not avalible please run WSDLs and Try again for " + newWSDLDiffFolder + "-------"));
                        //return;
                    }
                }
            }
            LogMessage(0, string.Format("-------WSDLs-Diff Generation completed succefully-------"));
        }

        #endregion

        #region Publish code

        private static void PublishFolder()
        {
            string source; string temporaryPath; string lastFolderName; string newPathName; string zipFileName; string newUpdatedpath;
            try
            {
                foreach (var modules in modulesFromDocumentation)
                {
                    try
                    {
                        AddingAllPendChangesUnderPath(modules.Workspacepath + "\\" + sandcastleConfiguration.currentBranch, true);

                        LogMessage(0, string.Format("------------------------------------"));
                        LogMessage(0, string.Format("Publishing start for: " + modules.ModuleName));
                        var currentPathFolder = modules.Workspacepath + "\\" + sandcastleConfiguration.currentBranch;
                        if (Directory.Exists(currentPathFolder))
                        {
                            newUpdatedpath = string.Empty;
                            foreach (var sNewPath in currentPathFolder.Split('\\'))
                            {
                                if (sNewPath.Trim() != "Modules")
                                {
                                    if (newUpdatedpath != string.Empty)
                                    {
                                        newUpdatedpath = newUpdatedpath + "\\" + sNewPath;
                                    }
                                    else
                                    {
                                        newUpdatedpath = sNewPath;
                                    }
                                }
                            }

                            RefactorDirectory(currentPathFolder, currentPathFolder, newUpdatedpath, modules.ModuleName, true, 1, false);
                            source = newUpdatedpath;

                            temporaryPath = newUpdatedpath + "\\TempFolder";
                            CreateZipFile(source, source, temporaryPath, true, 1, false);
                            ZipFile.CreateFromDirectory(temporaryPath, source + "\\" + modules.ModuleName + " " + sandcastleConfiguration.currentBranch + ".zip");
                            lastFolderName = Path.GetFileName(Path.GetDirectoryName(newUpdatedpath));
                            newPathName = Path.GetDirectoryName(newUpdatedpath);
                            zipFileName = newPathName + ".zip";
                            directoryInfo = new DirectoryInfo(temporaryPath);

                            directoryInfo.Delete(true);
                            directoryInfo = new DirectoryInfo(zipFileName);

                            var x = Path.GetExtension(zipFileName);
                            ZipFile.CreateFromDirectory(newPathName, zipFileName);
                            directoryInfo = new DirectoryInfo(newPathName);
                            if (File.Exists(zipFileName))
                            {
                                var publishURL = sandcastleConfiguration.PublishServerURL + "//MSDeploy.axd?site=";
                                string publisFolderName = zipFileName + " " + sandcastleConfiguration.ServerDestination + "\\" + lastFolderName + " " + publishURL.Trim() + " " + sandcastleConfiguration.SiteName.Trim() + " " + sandcastleConfiguration.ServerUserName + " " + sandcastleConfiguration.ServerPassword.Trim();
                                RunBatFiles(SandcastleConfig.PublishSandcastleDocumentationBatFile, publisFolderName);
                            }
                            directoryInfo.Delete(true);
                            File.Delete(zipFileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerManager.Logger.LogError("Error for " + modules.ModuleName + " " + "{0}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void AddingAllPendChangesUnderPath(string path, bool checkTFSActivity)
        {
            if (checkTFSActivity == true)
            {
                foreach (string sfiles in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                {
                    if (!sfiles.EndsWith(".log"))
                    {
                        TfsPendAdd(sfiles, checkTFSActivity);
                    }
                }
            }
        }

        #endregion
    }
}