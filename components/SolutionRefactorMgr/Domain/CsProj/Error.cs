using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain.CsProj
{
    public class Error
    {
        [XmlAttribute("Condition")]
        public string Condition { get; set; }

        [XmlAttribute("Text")]
        public string Text { get; set; }
        public void Validate()
        {
        }
    }
}
