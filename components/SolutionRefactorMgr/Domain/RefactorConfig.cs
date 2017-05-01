using System;
using System.IO;
using System.Collections.Generic;

namespace SolutionRefactorMgr.Domain
{
    [Serializable]
    public class RefactorConfig
    {
        public string SourceDir { get; set; }

        public List<ModuleConfig> ModuleConfigs { get; set; }

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

            if (this.ReplacementStrings != null && this.ReplacementStrings.Count > 0)
            {
                foreach (ReplacementString replacement in this.ReplacementStrings)
                {
                    replacement.Validate();
                }
            }
            else
            {
                throw new ArgumentNullException("No replacement strings have been specified.");
            }
        }

        public string Refactor(string origValue)
        {
            string newValue = origValue;

            foreach (ReplacementString replacement in this.ReplacementStrings)
            {
                newValue = newValue.Replace(replacement.Original, replacement.New);
            }

            return newValue;
        }
    }
}
