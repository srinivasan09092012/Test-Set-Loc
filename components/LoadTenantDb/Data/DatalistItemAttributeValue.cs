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
    public class DatalistItemAttributeValue
    {
        public DatalistItemAttributeValue()
            : base()
        {
        }

        public string DataListsAttributeValueId { get; set; }

        public string DataListsItemId { get; set; }

        public string DataListAttributeId { get; set; }

        public string DataListAttributeText { get; set; }

        public string DataListsItemValueId { get; set; }

        public string DataListsItemValueText { get; set; }
    }
}
