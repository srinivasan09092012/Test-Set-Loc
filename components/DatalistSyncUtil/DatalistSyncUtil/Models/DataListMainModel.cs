//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace DatalistSyncUtil
{
    [Serializable]
    public class DataListMainModel
    {
        public DataListMainModel()
        {
            this.Items = new List<CodeItemModel>();
            this.ListLinkitem = new List<CodeItemModel>();
        }

        public Guid ID { get; set; }

        public string ContentID { get; set; }

        public string Description { get; set; }

        public string ModuleName { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsEditable { get; set; }

        public Guid TenantID { get; set; }

        public Guid TenantModuleID { get; set; }

        public string ReleaseStatus { get; set; }

        public string Status { get; set; }

        public List<CodeItemModel> Items { get; set; }

        public int ItemsCount { get; set; }

        public List<ItemAttribute> DataListAttributes { get; set; }

        public List<CodeItemModel> ListLinkitem { get; set; }
    }
}