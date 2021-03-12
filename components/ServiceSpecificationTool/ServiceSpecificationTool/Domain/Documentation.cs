using System;
using System.Xml.Serialization;

namespace ServiceSpecificationTool.Domain
{
    [Serializable]

    public class Documentation
    {
        [XmlAttribute("docModuleName")]
        public string DocModuleName { get; set; }

        [XmlAttribute("moduleName")]
        public string ModuleName { get; set; }

        [XmlAttribute("workspacepath")]
        public string Workspacepath { get; set; }

        [XmlAttribute("serverpath")]
        public string Serverpath { get; set; }

        public void ValidateDocument()
        {
            if (string.IsNullOrWhiteSpace(this.DocModuleName))
            {
                throw new ArgumentNullException("Documentation configuration Document Module Name has not been specified.");
            }

            if (string.IsNullOrWhiteSpace(this.Serverpath.ToString()))
            {
                throw new ArgumentNullException("Documentation configuration Serverpath has not been specified.");
            }
            if (string.IsNullOrWhiteSpace(this.ModuleName.ToString()))
            {
                throw new ArgumentNullException("Documentation configuration ModuleName has not been specified.");
            }
        } 
    }
}
