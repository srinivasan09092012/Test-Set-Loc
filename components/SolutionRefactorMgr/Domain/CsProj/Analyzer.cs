using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain.CsProj
{
    public class Analyzer
    {
        [XmlAttribute("Include")]
        public string Include { get; set; }

        public void Validate()
        {
        }
    }
}
