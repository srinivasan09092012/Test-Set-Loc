using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain
{
    [Serializable]
    public class RefactorConfig
    {
        [XmlAttribute("editMode")]
        public Enumerations.EditModeTypes EditMode { get; set; }

        [XmlAttribute("includeFolderNames")]
        public bool IncludeFolderNames { get; set; }

        [XmlAttribute("includeFileNames")]
        public bool IncludeFileNames { get; set; }

        [XmlAttribute("includeFileContents")]
        public bool IncludeFileContents { get; set; }

        public string SourceDir { get; set; }

        [XmlArrayItem("FileType")]
        public List<string> FileTypes { get; set; }

        public List<ModuleConfig> ModuleConfigs { get; set; }

        public List<PackageConfig> PackageConfigs { get; set; }

        public List<LineDelete> LineDeletes { get; set; }

        public List<ReplacementString> ReplacementStrings { get; set; }

        public string TfsServer { get; set; }

        public void Validate()
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

            if (this.EditMode == Enumerations.EditModeTypes.Inline && string.IsNullOrWhiteSpace(this.TfsServer))
            {
                throw new ArgumentNullException("Inline edit mode requires that you specify the TFS source control server.");
            }

            if (this.ModuleConfigs != null && this.ModuleConfigs.Count > 0)
            {
                foreach (ModuleConfig module in this.ModuleConfigs)
                {
                    module.Validate();
                }
            }

            if (this.PackageConfigs != null && this.PackageConfigs.Count > 0)
            {
                foreach (PackageConfig package in this.PackageConfigs)
                {
                    package.Validate();
                }
            }

            if (this.LineDeletes != null && this.LineDeletes.Count > 0)
            {
                foreach (LineDelete lineDelete in this.LineDeletes)
                {
                    lineDelete.Validate();
                }
            }

            if (this.ReplacementStrings != null && this.ReplacementStrings.Count > 0)
            {
                foreach (ReplacementString replacement in this.ReplacementStrings)
                {
                    replacement.Validate();
                }
            }
        }
    }
}
