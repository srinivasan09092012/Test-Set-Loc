using System;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace SolutionRefactorMgr.Domain
{
    public class ReplacementStringWithinRange : ReplacementString
    {
        [XmlAttribute("rangeQualifier")]
        public string RangeQualifier { get; set; }

        [XmlAttribute("rangeLineCount")]
        public int RangeLineCount { get; set; }

        public new void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(this.RangeQualifier))
            {
                throw new ArgumentNullException("Range qualifier string text has not been specified.");
            }
            if (this.RangeLineCount <= 0)
            {
                throw new ArgumentNullException("Range line count must be greater or equal to 1.");
            }
        }

        public new void Initialize()
        {
            base.Initialize();
        }
    }
}
