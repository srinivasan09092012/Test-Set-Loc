namespace HPP.OneClick.XAML.Migration.DevContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UA3_DEVOPS.MODULE")]
    public partial class MODULE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MODULE()
        {
            JOB_CONFIG = new HashSet<JOB_CONFIG>();
            PACKAGEs = new HashSet<PACKAGE>();
            SOLUTIONs = new HashSet<SOLUTION>();
        }

        [Key]
        public Guid MODULE_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string MODULE_NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string OPERATOR_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CREATED_TS { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LAST_MODIFIED_TS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JOB_CONFIG> JOB_CONFIG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACKAGE> PACKAGEs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOLUTION> SOLUTIONs { get; set; }
    }
}
