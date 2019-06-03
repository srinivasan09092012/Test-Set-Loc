using System.Collections.Generic;
using System.Xml.Serialization;

namespace Watchdog.Domain
{
    public class UXConfig : ServiceConfigMetaData
    {
        public UXConfig()
        {            
        }

        [XmlAttribute("healthurl")]
        public string HealthUrl { get; set; }

        [XmlAttribute("baseaddress")]
        public string BaseAddress { get; set; }

        [XmlAttribute("Username")]
        public string Username { get; set; }

        [XmlAttribute("Password")]
        public string Password { get; set; }

        [XmlAttribute("LoggedInUsername")]
        public string LoggedInUsername { get; set; }

        [XmlAttribute("ApplicationPool")]
        public string ApplicationPoolName { get; set; }               

        [XmlAttribute("ServerName")]
        public string ServerName { get; set; }

        [XmlAttribute("SleepTime")]
        public string SleepTime { get; set; }
    }
}
