using System;
using System.Xml.Serialization;

namespace ServiceSpecificationTool.Domain
{
    [Serializable]
    public class FileType
    {
        [XmlAttribute("ext")]
        public string Extension { get; set; }

        [XmlAttribute("qualifyIfPathContains")]
        public string QualifyIfPathContains { get; set; }

        [XmlAttribute("ignoreIfPathContains")]
        public string IgnoreIfPathContains { get; set; }

        public void Validate()
        {
        }
    }
}
