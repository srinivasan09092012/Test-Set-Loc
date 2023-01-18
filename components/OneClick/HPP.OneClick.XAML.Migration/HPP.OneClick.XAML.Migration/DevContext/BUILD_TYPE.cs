namespace HPP.OneClick.XAML.Migration.DevContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UA3_DEVOPS.BUILD_TYPE")]
    public partial class BUILD_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BUILD_TYPE()
        {
            JOB_CONFIG = new HashSet<JOB_CONFIG>();
        }

        [Key]
        public Guid BUILD_TYPE_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string BUILD_TYPE_NAME { get; set; }

        [Required]
        [StringLength(200)]
        public string OPERATOR_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CREATED_TS { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LAST_MODIFIED_TS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JOB_CONFIG> JOB_CONFIG { get; set; }
    }
}
