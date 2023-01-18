//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
namespace HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TM_DATA_LISTS_ITEM")]
    public partial class DataListsItems
    {
        public DataListsItems()
        {
            this.DataListsAttributeValues = new HashSet<DataListAttributeValues>();
            this.DataListAttributes = new HashSet<DataListAttributes>();
            this.DataListsItemAttributeValues = new HashSet<DataListAttributeValues>();
            this.DataListsItemLinkParent = new HashSet<DataListsItemsLinks>();
            this.DataListsItemLinkChild = new HashSet<DataListsItemsLinks>();
            this.DataListsLanguages = new HashSet<DataListsLanguages>();
        }

        [Column("TM_DATA_LISTS_ID")]
        public Guid DataListsId { get; set; }

        [Required]
        [StringLength(200)]
        [Column("TM_DATA_LISTS_ITEM_KEY")]
        public string DataListsItemKey { get; set; }

        [Column("ORDER_INDEX")]
        public int OrderIndex { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModified { get; set; }

        [Key]
        [Column("TM_DATA_LISTS_ITEM_ID")]
        public Guid DataListsItemId { get; set; }

        [Column("EFF_DT")]
        public DateTime EffectiveDate { get; set; }

        [Column("END_DT")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<DataListAttributeValues> DataListsAttributeValues { get; set; }

        public virtual ICollection<DataListAttributeValues> DataListsItemAttributeValues { get; set; }

        public virtual ICollection<DataListAttributes> DataListAttributes { get; set; }

        public virtual ICollection<DataListsItemsLinks> DataListsItemLinkParent { get; set; }

        public virtual ICollection<DataListsItemsLinks> DataListsItemLinkChild { get; set; }

        public virtual ICollection<DataListsLanguages> DataListsLanguages { get; set; }
    }
}
