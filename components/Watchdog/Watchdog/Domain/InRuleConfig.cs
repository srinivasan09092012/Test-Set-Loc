using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Watchdog.Domain
{
    public class InRuleConfig : ServiceConfigMetaData
    {
        public InRuleConfig()
        {
            InRuleServiceList = new List<InRuleConfigDataItem>();
        }

        [XmlAttribute("baseAddress")]
        public string BaseAddress { get; set; }

        [XmlAttribute("sitename")]
        public string SiteName { get; set; }

        [XmlAttribute("serverName")]
        public string ServerName { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("Service")]
        public List<InRuleConfigDataItem> InRuleServiceList { get; set; }
    }
}
