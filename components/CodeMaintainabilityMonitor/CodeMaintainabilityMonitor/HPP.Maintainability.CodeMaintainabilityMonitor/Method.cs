using System.Collections.Generic;
using System.Xml.Serialization;

namespace HPP.Maintainability.CodeMaintainabilityMonitor
{
    [XmlRoot(ElementName = "Method")]
    public class Method
    {
        public Method()
        {
            Metrics = new List<Metric>();
        }

        public List<Metric> Metrics;

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("File")]
        public string File { get; set; }

        [XmlAttribute("Line")]
        public string Line { get; set; }
    }
}
