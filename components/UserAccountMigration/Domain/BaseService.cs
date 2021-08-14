//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Xml.Serialization;

namespace UserAccountMigration.Domain
{
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
