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
    public class AddressDoctorconfig : ServiceConfigMetaData
    {
        public AddressDoctorconfig()
        {
            this.AddressDoctorServices = new List<AddressDoctorConfigDataItem>();
        }
      
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("Service")]
        public List<AddressDoctorConfigDataItem> AddressDoctorServices { get; set; }
    }
}