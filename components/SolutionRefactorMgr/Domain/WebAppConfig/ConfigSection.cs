using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain.WebAppConfig
{
    class ConfigSection
    {
        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlAttribute("type")]
        public string type { get; set; }

        [XmlAttribute("requirePermission")]
        public string requirePermission { get; set; }

        [XmlAttribute("sectionText")]
        public string sectionText { get; set; }

        public void Validate()
        {
        }
    }
}
