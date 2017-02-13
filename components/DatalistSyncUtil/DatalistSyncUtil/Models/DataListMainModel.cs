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
    }
}
