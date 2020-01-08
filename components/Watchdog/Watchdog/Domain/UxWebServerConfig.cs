//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Watchdog.Domain
{
    public class UxWebServerConfig : ServiceConfigMetaData
    {
        public string Servername { get; set; }

        [XmlElement("Application")]
        public List<UXConfig> Applications { get; set; }
    }
}
