using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain.CsProj
{
    public class Reference
    {
        [XmlAttribute("Dependency")]
        public string Dependency { get; set; }

        [XmlAttribute("Include")]
        public string Include { get; set; }

        [XmlAttribute("HintPath")]
        public string HintPath { get; set; }

        public void Validate()
        {
        }
    }
}
