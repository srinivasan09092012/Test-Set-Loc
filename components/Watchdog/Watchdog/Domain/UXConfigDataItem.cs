﻿//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

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
