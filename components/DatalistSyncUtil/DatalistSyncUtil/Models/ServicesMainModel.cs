//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalistSyncUtil
{
    [Serializable]
    public class ServicesMainModel
    {
        public Guid ServiceID { get; set; }

        public Guid TenantModuleID { get; set; }

        public string Name { get; set; }

        public Guid SecurityRightItemID { get; set; }

        public bool SecRightItemModified { get; set; }

        public string LabelItemKey { get; set; }

        public bool LabelItemModified { get; set; }

        public string DefaultText { get; set; }

        public bool DefaultTextModified { get; set; }

        public string BaseURL { get; set; }

        public bool BaseURLModified { get; set; }

        public string IOCContainer { get; set; }

        public bool IOCContainerModified { get; set; }

        public string OperatorID { get; set; }

        public bool IsActive { get; set; }

        public bool IsActiveModified { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public Guid? TenantId { get; set; }

        public string Status { get; set; }
    }
}
