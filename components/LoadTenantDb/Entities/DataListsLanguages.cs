//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
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
    using System.Data.Entity.Spatial;

    [Table("TM_DATA_LISTS_LANGUAGES")]
    public partial class DataListsLanguages
    {
        [Key]
        [Column("TM_DATA_LISTS_ITEM_ID", Order = 0)]
        public Guid DataListsItemId { get; set; }

        [Key]
        [Column("LOCAL_ID", Order = 1)]
        [StringLength(5)]
        public string LocalId { get; set; }

        [Required]
        [StringLength(400)]
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [StringLength(2000)]
        [Column("LONG_DESCRIPTION")]
        public string LongDescription { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModified { get; set; }

        public virtual DataListsItems DataListsItem { get; set; }
    }
}
