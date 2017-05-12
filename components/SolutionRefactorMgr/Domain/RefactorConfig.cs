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

        public string SourceDir { get; set; }

        public List<ModuleConfig> ModuleConfigs { get; set; }

        public List<LineDelete> LineDeletes { get; set; }

        public List<ReplacementString> ReplacementStrings { get; set; }

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

            if (this.ModuleConfigs != null && this.ModuleConfigs.Count > 0)
            {
                foreach (ModuleConfig module in this.ModuleConfigs)
                {
                    module.Validate();
                }
            }
            else
            {
                throw new ArgumentNullException("No module configurations have been specified.");
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

        public string RefactorFileName(string origValue)
        {
            string newValue = origValue;

            foreach (ReplacementString replacement in this.ReplacementStrings)
            {
                newValue = newValue.Replace(replacement.Original, replacement.New);
            }

            return newValue;
        }

        public string RefactorFileContents(string filePath)
        {
            string newValue = string.Empty;

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
                            foreach (LineDelete lineDelete in this.LineDeletes)
                            {
                                if (line.Contains(lineDelete.Contains))
                                {
                                    deleteLine = true;
                                    break;
                                }
                            }

                            if(!deleteLine)
                            {
                                writer.WriteLine(line);
                            }
                        }
                        writer.Flush();
                        ms.Position = 0;
                        using (StreamReader sr = new StreamReader(ms))
                        {
                            newValue = sr.ReadToEnd();
                        }
                    }
                }
            }

            foreach (ReplacementString replacement in this.ReplacementStrings)
            {
                newValue = newValue.Replace(replacement.Original, replacement.New);
            }

            foreach (ReplacementString replacement in this.ReplacementStrings)
            {
                newValue = newValue.Replace(replacement.Original, replacement.New);
            }

            return newValue;
        }
    }
}
