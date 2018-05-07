//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("HELP_MITA")]
    public partial class HelpMita
    {
        public HelpMita()
        {
            this.HelpNode = new HashSet<HelpNode>();
        }

        [Key]
        [Column("HELP_MITA_ID")]
        public Guid HelpMitaId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("MITA_TITLE")]
        public string MitaTitle { get; set; }

        [Required]
        [StringLength(500)]
        [Column("MITA_URL")]
        public string MitaUrl { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(50)]
        [Column("OPERATOR_ID")]
        public string OperatorID { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModifiedTimeStamp { get; set; }

        public virtual ICollection<HelpNode> HelpNode { get; set; }
    }
}
