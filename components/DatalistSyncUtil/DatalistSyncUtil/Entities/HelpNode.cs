//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatalistSyncUtil.Entities
{
    [Table("HELP_NODE")]
    public partial class HelpNode
    {
        public HelpNode()
        {
            this.HelpNodeLink = new HashSet<HelpNodeLink>();
            this.HelpNodeLink1 = new HashSet<HelpNodeLink>();
            this.HelpNodeLocale = new HashSet<HelpNodeLocale>();
        }

        [Key]
        [Column("HELP_NODE_ID")]
        public Guid HelpNodeId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("HELP_NODE_NM")]
        public string HelpNodeNM { get; set; }

        [Required]
        [StringLength(50)]
        [Column("HELP_NODE_TYPE_CD")]
        public string HelpNodeTypeCD { get; set; }

        [Column("TENANT_ID")]
        [Required]
        public Guid TenantId { get; set; }

        [Column("MODULE_ID")]
        public Guid? ModuleId { get; set; }

        [Column("HELP_MITA_ID")]
        public Guid? HelpMitaId { get; set; }

        [Column("TM_ID")]
        [Required]
        public Guid TenantModuleId { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Column("OPERATOR_ID")]
        [Required]
        [StringLength(200)]
        public string OperatorID { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModifiedTimeStamp { get; set; }

        [Column("ORDER_INDEX")]
        public byte OrderIndex { get; set; }

        [Column("HOW_TO_HELP_NODE_NM")]
        [StringLength(100)]
        public string HowToHelpNodeNM { get; set; }

        public virtual HelpMita HelpMita { get; set; }

        public virtual ICollection<HelpNodeLink> HelpNodeLink { get; set; }

        public virtual ICollection<HelpNodeLink> HelpNodeLink1 { get; set; }

        [ForeignKey("TenantModuleId")]
        public virtual ICollection<TenantModule> TenantModule { get; set; }

        public virtual ICollection<HelpNodeLocale> HelpNodeLocale { get; set; }
    }
}
