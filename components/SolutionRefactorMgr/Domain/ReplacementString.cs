using System;
using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain
{
    public class ReplacementString
    {
        [XmlAttribute("original")]
        public string Original { get; set; }

        [XmlAttribute("new")]
        public string New { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Original))
            {
                throw new ArgumentNullException("Replacement string original text has not been specified.");
            }
        }
    }
}
