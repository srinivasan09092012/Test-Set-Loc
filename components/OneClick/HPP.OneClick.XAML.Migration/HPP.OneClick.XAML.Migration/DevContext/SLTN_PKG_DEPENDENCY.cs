namespace HPP.OneClick.XAML.Migration.DevContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UA3_DEVOPS.SLTN_PKG_DEPENDENCY")]
    public partial class SLTN_PKG_DEPENDENCY
    {
        [Key]
        public Guid SLTN_PKG_DEPENDENCY_ID { get; set; }

        public Guid PACKAGE_ID { get; set; }

        public Guid SOLUTION_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string OPERATOR_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CREATED_TS { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LAST_MODIFIED_TS { get; set; }

        public virtual PACKAGE PACKAGE { get; set; }

        public virtual SOLUTION SOLUTION { get; set; }
    }
}
