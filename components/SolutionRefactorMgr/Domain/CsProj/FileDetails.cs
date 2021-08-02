using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain
{
    public class FileDetails
    {
        [XmlAttribute("SourcePath")]
        public string SourcePath { get; set; }

        [XmlAttribute("PathToAdd")]
        public string PathToAdd { get; set; }
        public void Validate()
        {
        }
    }
}
