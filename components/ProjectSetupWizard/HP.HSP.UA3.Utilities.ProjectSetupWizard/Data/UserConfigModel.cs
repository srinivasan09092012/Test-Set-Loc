//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace HP.HSP.UA3.Utilities.ProjectSetupWizard.Data
{
    [Serializable()]
    [GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [XmlType(Namespace = "urn:hp.com:hsp:ua3:bsuinessProjectSetupWizard:schemas")]
    [XmlRoot(Namespace = "urn:hp.com:hsp:ua3:bsuinessProjectSetupWizard:schemas", IsNullable = false, ElementName = "UserConfig")]
    public class UserConfigModel
    {
        [XmlAttribute("sourcePath")]
        public string SourcePath { get; set; }

        public UserConfigModel()
        { }
    }
}
