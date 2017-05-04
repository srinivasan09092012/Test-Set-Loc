//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace DatalistSyncUtil
{
    [Serializable]
    public class DataListItemLink
    {
        public Guid ParentID { get; set; }

        public Guid ChildID { get; set; }

        public bool IsActive { get; set; }

        public string ContentID { get; set; }

        public string ParentDataList { get; set; }

        public string ParentCode { get; set; }

        public string ChildDataList { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }

        public Guid TenantID { get; set; }

        public Guid DataListID { get; set; }

        public Guid DataListTypeID { get; set; }
    }
}