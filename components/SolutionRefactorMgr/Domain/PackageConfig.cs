using System;
using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain
{
    [Serializable]
    public class PackageConfig
    {
        [XmlAttribute("qualifierPrefix")]
        public string QualifierPrefix { get; set; }

        [XmlAttribute("rebuildPackage")]
        public bool RebuildPackage { get; set; }

        [XmlAttribute("reformatSource")]
        public bool ReformatSource { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.QualifierPrefix))
            {
                throw new ArgumentNullException("Package qualifier prefix name has not been specified.");
            }
        }
    }
}
