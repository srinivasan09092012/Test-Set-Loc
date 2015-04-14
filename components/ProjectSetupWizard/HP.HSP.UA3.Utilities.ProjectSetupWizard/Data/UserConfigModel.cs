using System;
using System.CodeDom.Compiler;
using System.Text;
using System.Xml.Serialization;

namespace HP.HSP.UA3.Utilities.ProjectSetupWizard.Data
{
    [Serializable()]
    [GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [XmlType(Namespace = "urn:hp.com:hsp:ua3:bsuinessProjectSetupWizard:schemas")]
    [XmlRoot(Namespace = "urn:hp.com:hsp:ua3:bsuinessProjectSetupWizard:schemas", IsNullable = false, ElementName = "UserConfig")]
    public class UserConfigModel
    {
        [XmlAttribute("sourcePath")]
        public string SourcePath { get; set; }

        public UserConfigModel()
        { }
    }
}
