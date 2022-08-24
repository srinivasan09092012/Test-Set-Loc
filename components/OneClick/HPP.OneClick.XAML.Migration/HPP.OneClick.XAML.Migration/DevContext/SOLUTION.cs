namespace HPP.OneClick.XAML.Migration.DevContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UA3_DEVOPS.SOLUTION")]
    public partial class SOLUTION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOLUTION()
        {
            SLTN_PKG_DEPENDENCY = new HashSet<SLTN_PKG_DEPENDENCY>();
        }

        [Key]
        public Guid SOLUTION_ID { get; set; }

        public Guid MODULE_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string SOLUTION_NAME { get; set; }

        [Required]
        [StringLength(200)]
        public string OPERATOR_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CREATED_TS { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LAST_MODIFIED_TS { get; set; }

        public virtual MODULE MODULE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SLTN_PKG_DEPENDENCY> SLTN_PKG_DEPENDENCY { get; set; }
    }
}
