using System;
using System.Xml.Serialization;

namespace UserAccountManager.Domain
{
    [Serializable]
    public class BaseService
    {
        [XmlAttribute("behaviorConfiguration")]
        public string BehaviorConfiguration { get; set; }

        [XmlAttribute("bindingConfiguration")]
        public string BindingConfiguration { get; set; }

        [XmlAttribute("binding")]
        public string Binding { get; set; }

        [XmlAttribute("endpointAddress")]
        public string EndpointAddress { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Binding))
            {
                throw new ArgumentNullException("UserQueryService binding is required.");
            }

            if (string.IsNullOrWhiteSpace(this.BindingConfiguration))
            {
                throw new ArgumentNullException("UserQueryService bidingConfiguration is required.");
            }

            if (string.IsNullOrWhiteSpace(this.EndpointAddress))
            {
                throw new ArgumentNullException("UserQueryService endpointAddress is required.");
            }
        }
    }
}
