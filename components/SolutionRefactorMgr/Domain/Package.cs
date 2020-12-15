using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain
{
    public class Package
    {
        [XmlAttribute("id")]
        public string id { get; set; }

        [XmlAttribute("version")]
        public string version { get; set; }

        [XmlAttribute("targetFramework")]
        public string targetFramework { get; set; }

        

        public void Validate()
        {
        }
    }
}
