//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace HP.HSP.UA3.Utilities.ProjectSetupWizard.Common
{
    public static class FileIOUtility
    {
        private static List<string> _projectFiles;
        private static List<string> _excludeDirNames;

        public static void CreateBusinessModuleDirectory(string templateRootDir, string moduleRootDir, string moduleName)
        {
            _projectFiles = new List<string>();
            _excludeDirNames = new List<string>() { "[ServiceName]" };
            ProcessTemplateDirectory(templateRootDir, moduleRootDir, moduleName, null);
            ProcessProjectFiles();
        }

        public static void CreateServiceDirectory(string templateRootDir, string serviceRootDir, string moduleName, string serviceName)
        {
            _projectFiles = new List<string>();
            _excludeDirNames = null;
            ProcessTemplateDirectory(templateRootDir, serviceRootDir, moduleName, serviceName);
            ProcessProjectFiles();
        }

        public static void DeleteDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                DeleteDirectory(di.FullName);
                di.Delete();
            }
        }

        private static void ConvertFileContents(string moduleFile, string moduleName, string serviceName)
        {
            Random rnd = new Random();
            int servicePort = rnd.Next(40100, 40999);
            string contents = File.ReadAllText(moduleFile);
            contents = contents.Replace("[COMGuid]", Guid.NewGuid().ToString());
            contents = contents.Replace("[ModuleName]", moduleName);
            contents = contents.Replace("[ModuleGuid]", Guid.NewGuid().ToString("n"));
            contents = contents.Replace("[NewGuid]", Guid.NewGuid().ToString("n"));
            contents = contents.Replace("[ServiceName]", serviceName);
            if (!String.IsNullOrEmpty(serviceName))
            {
                contents = contents.Replace("[ServiceNameWithLowerCaseFirstChar]", char.ToLower(serviceName[0]) + serviceName.Substring(1));
            }
            contents = contents.Replace("[ServicePort]", servicePort.ToString());
            File.WriteAllText(moduleFile, contents);
        }

        private static string ConvertName(string templateFileName, string moduleName, string serviceName)
        {
            return templateFileName.Replace("[ModuleName]", moduleName).Replace("[ServiceName]", serviceName);
        }

        private static void ProcessProjectFiles()
        {
            Dictionary<string, string> guids = new Dictionary<string, string>();
            foreach (string file in _projectFiles)
            {
                if (file.EndsWith(".csproj"))
                {
                    string oldGuid = ParseProjectGuid(file);
                    string newGuid = Guid.NewGuid().ToString("b").ToUpper();
                    guids.Add(oldGuid, newGuid);
                }
            }

            foreach (string file in _projectFiles)
            {
                ReplaceProjectGuids(file, guids);
            }
        }

        private static string ParseProjectGuid(string file)
        {
            string projectGuid = string.Empty;

            XNamespace xns = "http://schemas.microsoft.com/developer/msbuild/2003";
            XDocument xDoc = XDocument.Parse(File.ReadAllText(file));
            XElement xRoot = xDoc.Root;
            IEnumerable<XElement> xPropertyGroups = from x in xRoot.Elements(xns + "PropertyGroup") select x;
            if (xPropertyGroups.Count<XElement>() > 0)
            {
                foreach (XElement xPropertyGroup in xPropertyGroups)
                {
                    XElement xProjectGuid = xPropertyGroup.Element(xns + "ProjectGuid");
                    projectGuid = xProjectGuid.Value;
                    break;
                }
            }

            return projectGuid;
        }

        private static void ProcessTemplateDirectory(string templateDir, string targetDir, string moduleName, string serviceName)
        {
            bool processDir = true;
            if (_excludeDirNames != null)
            {
                foreach (string excludeDirName in _excludeDirNames)
                {
                    if (templateDir.Contains(excludeDirName))
                    {
                        processDir = false;
                        break;
                    }
                }
            }

            if (processDir)
            {
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }

                List<string> templateDirFiles = new List<string>(Directory.GetFiles(templateDir));
                foreach (string templateDirFile in templateDirFiles)
                {
                    string templateFileName = Path.GetFileName(templateDirFile);
                    string moduleFileName = ConvertName(templateFileName, moduleName, serviceName);
                    string moduleDirFile = Path.Combine(targetDir, moduleFileName);

                    if (!File.Exists(moduleDirFile))
                    {
                        File.Copy(templateDirFile, moduleDirFile, true);
                        File.SetAttributes(moduleDirFile, FileAttributes.Normal);
                        ConvertFileContents(moduleDirFile, moduleName, serviceName);
                    }

                    if (moduleDirFile.EndsWith(".csproj") || moduleDirFile.EndsWith(".sln"))
                    {
                        _projectFiles.Add(moduleDirFile);
                    }
                }

                List<string> templateDirChildDirs = new List<string>(Directory.GetDirectories(templateDir));
                foreach (string templateDirChildDir in templateDirChildDirs)
                {
                    string templateChildDirName = templateDirChildDir.Replace(templateDir + "\\", "");
                    string moduleDirChildDir = Path.Combine(targetDir, ConvertName(templateChildDirName, moduleName, serviceName));
                    ProcessTemplateDirectory(templateDirChildDir, moduleDirChildDir, moduleName, serviceName);
                }
            }
        }

        private static void ReplaceProjectGuids(string file, Dictionary<string, string> guids)
        {
            string contents = File.ReadAllText(file);

            foreach (string oldGuid in guids.Keys)
            {
                contents = contents.Replace(oldGuid.ToUpper(), guids[oldGuid]);
                contents = contents.Replace(oldGuid.ToLower(), guids[oldGuid]);
            }

            File.WriteAllText(file, contents);
        }
    }
}