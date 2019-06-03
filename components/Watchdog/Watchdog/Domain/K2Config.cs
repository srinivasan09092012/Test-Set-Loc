using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Watchdog.Domain
{
   public class K2Config : ServiceConfigMetaData
    {
        public K2Config()
        {
            K2ServiceList = new List<K2ConfigDataItem>();
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
        public List<K2ConfigDataItem> K2ServiceList { get; set; }
    }        
}
