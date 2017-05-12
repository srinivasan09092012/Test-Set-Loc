using System;
using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain
{
    public class LineDelete
    {
        [XmlAttribute("contains")]
        public string Contains { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Contains))
            {
                throw new ArgumentNullException("Line delete string contains has not been specified.");
            }
        }
    }
}
