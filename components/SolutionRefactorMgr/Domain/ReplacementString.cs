﻿using System;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

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

        [XmlAttribute("filename")]
        public string filename { get; set; }

        [XmlAttribute("connectionString")]
        public string connectionString { get; set; }

        [XmlAttribute("providerName")]
        public string providerName { get; set; }

        [XmlAttribute("regexReplace")]
        public bool RegexReplace { get; set; }

        public Regex FromRegex { get; set; }

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
                ////It's ok to replace with an empty string.
                ////throw new ArgumentNullException("Replacement string to text has not been specified.");
            }
        }

        public void Initialize()
        {
            this.To = this.To.Replace("[NEWLINE]", Environment.NewLine);

            if (this.RegexReplace)
            {
                this.FromRegex = new Regex(this.From);
            }
        }
    }
}
