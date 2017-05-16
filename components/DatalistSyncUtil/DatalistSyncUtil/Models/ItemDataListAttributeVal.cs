// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace DatalistSyncUtil
{
    public class ItemDataListItemAttributeVal
    {
        public Guid DataListAttributeID { get; set; }

        public string DataListAttributeName { get; set; }

        public string DataListAttributeValue { get; set; }

        public Guid DataListItemID { get; set; }

        public Guid DataListTypeID { get; set; }

        public string DataListTypeName { get; set; }

        public Guid DataListValueID { get; set; }

        public Guid ID { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string Status { get; set; }

        public string ParentContentId { get; set; }

        public string ItemCode { get; set; }

        public bool IsEditable { get; set; }
    }
}
