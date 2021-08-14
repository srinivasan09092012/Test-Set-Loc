//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Watchdog.Domain
{
    public class UXMonitoring 
    {
        [XmlElement("WebServer")]
        public List<UxWebServerConfig> WebServers { get; set; }
        
    }

}