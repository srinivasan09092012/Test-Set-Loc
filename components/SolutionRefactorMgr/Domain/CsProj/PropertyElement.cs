using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain.CsProj
{
    public class PropertyElement
    {
        [XmlAttribute("ElementName")]
        public string ElementName { get; set; }

        [XmlAttribute("ElementText")]
        public string ElementText { get; set; }
        public void Validate()
        {
        }
    }
}
