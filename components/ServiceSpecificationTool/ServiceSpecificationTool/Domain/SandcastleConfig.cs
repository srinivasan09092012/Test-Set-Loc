using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using ServiceSpecificationTool.Domain;

namespace ServiceSpecificationTool.Domain
{
    [Serializable]
    public class SandcastleConfig
    {
        [XmlAttribute("editMode")]
        public Enumerations.EditModeTypes EditMode { get; set; }

        [XmlAttribute("includeFolderNames")]
        public bool IncludeFolderNames { get; set; }

        [XmlAttribute("includeFileNames")]
        public bool IncludeFileNames { get; set; }

        [XmlAttribute("includeFileContents")]
        public bool IncludeFileContents { get; set; }

        [XmlAttribute("useSourceControl")]
        public bool UseSourceControl { get; set; }
        public string SourceDir { get; set; }
        public string ServerDir { get; set; }
        public string currentBranch { get; set; }
        public string PrevBranch { get; set; }
        public string BuildOutputDirectory { get; set; }
        public string TfsServer { get; set; }
        public string TfsWorkspace { get; set; }
        public string TFSUserName { get; set; }
        public string TFSPassword { get; set; }
        public string PublishServerURL { get; set; }
        public string ServerDestination { get; set; }
        public string SiteName { get; set; }
        public string ServerUserName { get; set; }
        public string ServerPassword { get; set; }
        public List<ModuleConfig> ModuleConfigs { get; set; }
        public List<FileType> FileTypes { get; set; }
        public List<Documentation> Documentation { get; set; }
        public List<ServiceEnvironments> ServiceEnvironments { get; set; }
        public List<PackageConfig> PackageConfigs { get; set; }
        public List<XSDSConfig> XSDSConfig { get; set; }

        public string[] DependencyModules = { "AuthManagement", "BusinessRules", "Logger" };

        public const string BuildBasProjectBatFile = "BuildBASProject.bat";
        public const string BuildBasSolutionBatFile = "BuildBASSolution.bat";
        public const string BuildCoreAPISolutionBatFile = "BuildCoreAPISolution.bat";
        public const string BuildUXSolutionBatFile = "BuildUXSolution.bat";
        public const string BuildUXProjectBatFile = "BuildUXProject.bat";
        public const string GenerateCHMFilesBatFile = "GenerateCHMFile.bat";
        public const string GenerateWSDLsDifferenceBatFile = "WSDLs-Diff.bat";
        public const string GenerateWSDLsBatFile = "WSDLsGeneration.bat";
        public const string GenerateXSDsBatFile = "XSDsGeneration.bat";
        public const string PublishSandcastleDocumentationBatFile = "Publish.bat";

        public void Validate()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.SourceDir))
                {
                    throw new ArgumentNullException("Source directory has not been specified.");
                }
                else
                {
                    if (!this.SourceDir.EndsWith("\\"))
                    {
                        this.SourceDir += "\\";
                    }

                    if (!Directory.Exists(this.SourceDir))
                    {
                        throw new ArgumentException("Source directory does not exist.");
                    }
                }
                if (this.UseSourceControl && string.IsNullOrWhiteSpace(this.TfsServer))
                {
                    throw new ArgumentNullException("Source control requires that you specify the TFS source control server.");
                }

                else if (string.IsNullOrWhiteSpace(this.TfsWorkspace))
                {
                    this.TfsWorkspace = null;
                }
                if (string.IsNullOrWhiteSpace(this.BuildOutputDirectory))
                {
                    throw new ArgumentNullException("Build Output directory has not been specified.");
                }
                else
                {
                    if (!Directory.Exists(this.BuildOutputDirectory))
                    {
                        //throw new ArgumentException("Build Output directory does not exist.");
                    }
                }
                if (this.ModuleConfigs != null && this.ModuleConfigs.Count > 0)
                {
                    foreach (ModuleConfig module in this.ModuleConfigs)
                    {
                        module.Validate();
                    }
                }
                if (this.XSDSConfig != null && this.XSDSConfig.Count > 0)
                {
                    foreach (XSDSConfig xSDS in this.XSDSConfig)
                    {
                        xSDS.ValidateXSDS();
                    }
                }

                if (this.PackageConfigs != null && this.PackageConfigs.Count > 0)
                {
                    foreach (PackageConfig package in this.PackageConfigs)
                    {
                        package.Validate();
                    }
                }
                if (this.ServiceEnvironments != null && this.ServiceEnvironments.Count > 0)
                {
                    foreach (ServiceEnvironments package in this.ServiceEnvironments)
                    {
                        package.ValidateEnvironment();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
