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
    public class DatalistItemLink
    {
        public DatalistItemLink()
            : base()
        {
        }

        public string ParentId { get; set; }

        public string ChildId { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public bool Active { get; set; }

        public DateTime? LastModified { get; set; }

        public string DataListItemId { get; set; }
    }
}
