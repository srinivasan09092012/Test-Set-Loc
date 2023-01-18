//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;
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