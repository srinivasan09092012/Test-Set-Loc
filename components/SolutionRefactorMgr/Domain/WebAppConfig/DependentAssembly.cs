using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain.WebAppConfig
{
    public class DependentAssembly
    {
        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlAttribute("publicKeyToken")]
        public string publicKeyToken { get; set; }

        [XmlAttribute("culture")]
        public string culture { get; set; }

        [XmlAttribute("oldVersion")]
        public string oldVersion { get; set; }

        [XmlAttribute("newVersion")]
        public string newVersion { get; set; }

        public void Validate()
        {
        }
    }
}
