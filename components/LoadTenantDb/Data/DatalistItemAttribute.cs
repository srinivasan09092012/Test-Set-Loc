//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Data
{
    public class DatalistItemAttribute
    {
        public DatalistItemAttribute()
            : base()
        {
        }

        public string DataListId { get; set; }

        public string DataListsAttributeId { get; set; }

        public string TypeName { get; set; }

        public string TypeDataListId { get; set; }

        public string TypeDataListIdText { get; set; }

        public string TypeDefaultItemId { get; set; }

        public string TypeDefaultItemIdText { get; set; }

        public bool IsActive { get; set; }
    }
}
