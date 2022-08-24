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

    [Table("HELP_NODE_LINK")]
    public partial class HelpNodeLink
    {
        [Key]
        [Column("HELP_NODE_LINK_ID")]
        public Guid HelpNodeLinkId { get; set; }

        [Column("PARENT_NODE_ID")]
        public Guid ParentNodeId { get; set; }

        [Column("CHILD_NODE_ID")]
        public Guid ChildNodeId { get; set; }

        [Column("IS_ENABLED")]
        public bool IsEnabled { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(200)]
        [Column("OPERATOR_ID")]
        public string OperatorID { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime? LastModifiedTimeStamp { get; set; }

        public virtual HelpNode HelpNode { get; set; }

        public virtual HelpNode HelpNode1 { get; set; }
    }
}
