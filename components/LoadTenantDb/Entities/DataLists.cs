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

    [Table("TM_DATA_LISTS")]
    public partial class DataLists
    {
        public DataLists()
        {
            this.DataListsAttributeFK1 = new HashSet<DataListAttributes>();
            this.DataListsAttributeFK2 = new HashSet<DataListAttributes>();
            this.DataListsItems = new HashSet<DataListsItems>();
        }

        [Key]
        [Column("TM_DATA_LISTS_ID")]
        public Guid DataListsId { get; set; }

        [Column("TM_ID")]
        public Guid TenantModuleId { get; set; }

        [Required]
        [Column("CONTENT_ID")]
        [StringLength(100)]
        public string ContentId { get; set; }

        [Required]
        [Column("TM_DATA_LISTS_NAME")]
        [StringLength(100)]
        public string DataListsName { get; set; }

        [Column("DESCRIPTION")]
        [StringLength(4000)]
        public string Description { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModified { get; set; }

        [Column("TENANT_ID")]
        public Guid TenantId { get; set; }

        public virtual ICollection<DataListAttributes> DataListsAttributeFK1 { get; set; }

        public virtual ICollection<DataListAttributes> DataListsAttributeFK2 { get; set; }

        public virtual ICollection<DataListsItems> DataListsItems { get; set; }
    }
}
