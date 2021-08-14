//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------\

using DatalistSyncUtil.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HP.HSP.UA3.Core.BAS.Help.DataAccess.DataAccess.Entities
{
    [Table("TENANT_MODULE")]
    public partial class TenantModule
    {
        public TenantModule()
        {
            this.HelpNode = new HashSet<HelpNode>();
        }

        [Key]
        [Column("TM_ID")]
        public Guid TmId { get; set; }

        [Column("TENANT_ID")]
        public Guid TenantId { get; set; }

        [Column("MODULE_ID")]
        public Guid ModuleId { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(50)]
        [Column("OPERATOR_ID")]
        public string OperatorId { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModifiedTs { get; set; }

        public virtual ICollection<HelpNode> HelpNode { get; set; }
    }
}
