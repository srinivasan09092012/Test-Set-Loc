using System;
using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain
{
    public class ReplacementString
    {
        [XmlAttribute("qualifier")]
        public string Qualifier { get; set; }

        [XmlAttribute("from")]
        public string From { get; set; }

        [XmlAttribute("to")]
        public string To { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Qualifier))
            {
                throw new ArgumentNullException("Replacement string qualifier text has not been specified.");
            }

            if (string.IsNullOrWhiteSpace(this.From))
            {
                throw new ArgumentNullException("Replacement string from text has not been specified.");
            }

            if (string.IsNullOrWhiteSpace(this.To))
            {
                throw new ArgumentNullException("Replacement string to text has not been specified.");
            }
        }
    }
}
