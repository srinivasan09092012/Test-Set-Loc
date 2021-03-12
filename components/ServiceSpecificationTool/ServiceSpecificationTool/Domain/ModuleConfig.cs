using System;
using System.Xml.Serialization;

namespace ServiceSpecificationTool.Domain
{
    [Serializable]
    public class ModuleConfig
    {
        [XmlAttribute("moduleName")]
        public string ModuleName { get; set; }

        [XmlAttribute("status")]
        public string Status { get; set; }

        [XmlAttribute("serverpath")]
        public string Serverpath { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.ModuleName))
            {
                throw new ArgumentNullException("Module configuration Modulename has not been specified.");
            }

            if (string.IsNullOrWhiteSpace(this.Serverpath.ToString()))
            {
                throw new ArgumentNullException("Module configuration Serverpath has not been specified.");
            }
        }
    }
}
