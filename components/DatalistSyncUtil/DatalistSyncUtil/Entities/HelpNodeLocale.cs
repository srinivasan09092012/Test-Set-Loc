//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("HELP_NODE_LOCALE")]
    public partial class HelpNodeLocale
    {
        [Key]
        [Column("HELP_NODE_ID", Order = 0)]
        public Guid HelpNodeId { get; set; }

        [Key]
        [Column("LOCALE_ID", Order = 1)]
        [StringLength(5)]
        public string LocaleId { get; set; }

        [StringLength(200)]
        [Column("NODE_TITLE")]
        public string NodeTitle { get; set; }

        [StringLength(2000)]
        [Column("NODE_SUMMARY")]
        public string NodeSummary { get; set; }

        [Column("NODE_TXT")]
        public string NodeTxt { get; set; }

        [Required]
        [StringLength(50)]
        [Column("OPERATOR_ID")]
        public string OperatorID { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModifiedTimeStamp { get; set; }

        public virtual HelpNode HelpNode { get; set; }
    }
}
