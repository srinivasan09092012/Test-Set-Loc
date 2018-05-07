//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
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

    [Table("TM_DATA_LISTS_ATTRIBUTES")]
    public partial class DataListAttributes
    {
        public DataListAttributes()
        {
            this.DataListsAttributeValue = new HashSet<DataListAttributeValues>();
        }

        [Column("TM_DATA_LISTS_ID")]
        public Guid DataListsId { get; set; }

        [Key]
        [Column("TM_DATA_LISTS_ATTR_ID")]
        public Guid DataListsAttributeId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("TYPE_NAME")]
        public string TypeName { get; set; }

        [Column("TYPE_DATA_LISTS_ID")]
        public Guid TypeDataListsId { get; set; }

        [Column("TYPE_DEFAULT_ITEM_ID")]
        public Guid TypeDefaultItemId { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModified { get; set; }

        public virtual DataLists DataListsAttributeValueFK1 { get; set; }

        public virtual DataLists DataListsAttributeValueFK2 { get; set; }

        public virtual ICollection<DataListAttributeValues> DataListsAttributeValue { get; set; }

        public virtual DataListsItems DataListsItem { get; set; }
    }
}
