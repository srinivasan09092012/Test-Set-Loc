//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

using System.Xml.Serialization;

namespace Watchdog.Domain
{
    public class Tenant
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("Id")]
        public string Id { get; set; }
    }
}
