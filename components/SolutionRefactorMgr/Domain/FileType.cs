using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain
{
    public class FileType
    {
        [XmlAttribute("ext")]
        public string Extension { get; set; }

        [XmlAttribute("qualifyIfPathContains")]
        public string QualifyIfPathContains { get; set; }

        [XmlAttribute("ignoreIfPathContains")]
        public string IgnoreIfPathContains { get; set; }

        [XmlAttribute("qualifyIfNameContains")]
        public string QualifyIfNameContains { get; set; }

        public void Validate()
        {
        }
    }
}
