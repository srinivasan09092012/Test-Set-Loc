using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Watchdog.Domain
{
    public class UXConfigDataItem : ServiceConfigMetaData
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("applicationPoolName")]
        public string ApplicationPoolName { get; set; }

        [XmlAttribute("endpoint")]
        public string Endpoint { get; set; }

        public string GetEndpointURL(string baseAddress)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(baseAddress);
            builder.Append("/");
            builder.Append(Endpoint);

            return builder.ToString();
        }
    }
}
