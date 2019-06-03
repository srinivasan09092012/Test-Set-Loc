using System.Xml.Serialization;

namespace Watchdog.Domain
{
    public class ServiceConfigMetaData
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("applicationDownAction")]
        public string ApplicationDownAction { get; set; }

        [XmlAttribute("monitor")]
        public bool Monitor { get; set; }

        [XmlAttribute("maxRetryCount")]
        public int MaxRetryCount { get; set; }

        [XmlAttribute("performanceSampleCount")]
        public int PerformanceSampleCount { get; set; }
    }
}
