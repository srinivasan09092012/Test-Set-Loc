using System;
using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain
{
    [Serializable]
    public class ModuleConfig
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("branch")]
        public string Branch { get; set; }

        [XmlAttribute("types")]
        public Enumerations.ProjectTypes Type { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                throw new ArgumentNullException("Module configuration name has not been specified.");
            }

            if (string.IsNullOrWhiteSpace(this.Branch.ToString()))
            {
                throw new ArgumentNullException("Module configuration branch has not been specified.");
            }

            if (string.IsNullOrWhiteSpace(this.Type.ToString()))
            {
                throw new ArgumentNullException("Module configuration type has not been specified.");
            }
        }
    }
}
