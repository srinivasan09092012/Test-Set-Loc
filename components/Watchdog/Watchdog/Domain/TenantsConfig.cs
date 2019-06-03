using System.Xml.Serialization;

namespace Watchdog.Domain
{
    public class Tenant
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("Id")]
        public string Id { get; set; }
    }
}
