using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain.CsProj
{
    public class Import
    {
        [XmlAttribute("Project")]
        public string Project { get; set; }

        [XmlAttribute("Condition")]
        public string Condition { get; set; }

        public void Validate()
        {
        }
    }
}
