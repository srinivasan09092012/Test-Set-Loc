using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common.Information
{
    [XmlRoot(ElementName = "DocInformationModel")]
    public class InformationModel
    {
        [XmlElement(ElementName = "BuildDate")]
        public string BuildDate;

        [XmlElement(ElementName = "BuildVersion")]
        public string BuildVersion;

        [XmlElement(ElementName = "ServerIp")]
        public string ServerIp;

        [XmlElement(ElementName = "TargetPathHomePage")]
        public string TargetPathHomePage;
    }
}
