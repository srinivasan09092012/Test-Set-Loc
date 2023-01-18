using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common.ModuleSettings
{
    public class ContentPage
    {
        [XmlElement(ElementName = "ListPage")]
        public List<string> ListPage;
    }
}
