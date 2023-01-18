using System.Xml.Serialization;

namespace HPP.Maintainability.CodeMaintainabilityMonitor
{
    [XmlRootAttribute(ElementName = "Metric")]
    public class Metric
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Value")]
        public string Value { get; set; }
    }
}
