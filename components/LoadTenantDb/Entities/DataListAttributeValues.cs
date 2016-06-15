//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
namespace HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TM_DATA_LISTS_ATTR_VALUE")]
    public partial class DataListAttributeValues
    {
        [Column("TM_DATA_LISTS_ITEM_ID")]
        public Guid DataListsItemId { get; set; }

        [Column("TM_DATA_LIST_ATTR_ID")]
        public Guid DataListAttributeId { get; set; }

        [Column("TYPE_DATA_LISTS_VALUE_ID")]
        public Guid DataListsItemValueId { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModified { get; set; }

        [Key]
        [Column("TM_DATA_LISTS_ATTR_VALUE_ID")]
        public Guid DataListsAttributeValueId { get; set; }

        public virtual DataListsItems DataListsItemFK1 { get; set; }

        public virtual DataListsItems DataListsItemFK2 { get; set; }

        public virtual DataListAttributes DataListsAttributes { get; set; }
    }
}