//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace DatalistSyncUtil
{
    [Serializable]
    public class ItemAttribute
    {
        public string ParentContentId { get; set; }

        public string ContentID { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public Guid DataListID { get; set; }

        public Guid DataListTypeID { get; set; }

        public Guid DefaultTypeValue { get; set; }

        public Guid ID { get; set; }

        public Guid TenantID { get; set; }

        public string TypeName { get; set; }

        public bool IsEditable { get; set; }
    }
}
