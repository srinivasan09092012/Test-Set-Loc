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
    using System.Data.Entity.Spatial;

    [Table("TM_DATA_LISTS_ITEM_LINK")]
    public partial class DataListsItemsLinks
    {
        [Key]
        [Column("PARENT_ID_FK", Order = 0)]
        public Guid ParentId { get; set; }

        [Key]
        [Column("CHILD_ID_FK", Order = 1)]
        public Guid ChildId { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModified { get; set; }

        public virtual DataListsItems DataListsItemParent { get; set; }

        public virtual DataListsItems DataListItemChild { get; set; }
    }
}
