//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Data
{
    public class DatalistItemLanguage
    {
        public DatalistItemLanguage()
            : base()
        {
            this.Locale = "en-us";
        }

        public string Locale { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string DataListItemId { get; set; }

        public string LongDescription { get; set; }
    }
}
