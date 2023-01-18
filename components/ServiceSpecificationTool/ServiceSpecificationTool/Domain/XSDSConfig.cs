using System;
using System.Xml.Serialization;

namespace ServiceSpecificationTool.Domain
{
    [Serializable]
    public class XSDSConfig
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("dllLoaction")]
        public string DllLocation { get; set; }

        public void ValidateXSDS()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                throw new ArgumentNullException("XSDS configuration has not been specified.");
            }
        }
    }
}
