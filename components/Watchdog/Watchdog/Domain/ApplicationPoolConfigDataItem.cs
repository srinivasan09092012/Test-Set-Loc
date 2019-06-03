using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Watchdog.Domain
{
    public class ApplicationPoolConfigDataItem : ServiceConfigMetaData
    {
        [XmlAttribute("serverName")]
        public string ServerName { get; set; }

        [XmlAttribute("time")]
        public string Time { get; set; }        
    }
}
