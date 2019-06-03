using System.Collections.Generic;
using System.Xml.Serialization;

namespace Watchdog.Domain
{
    public class WindowsServices
    {
        [XmlElement("Service")]
        public List<WindowsServiceConfigItem> WindowsServiceList { get; set; }
    }
}
