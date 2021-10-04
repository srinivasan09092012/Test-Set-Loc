namespace HPP.OneClick.XAML.Migration.DevContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UA3_DEVOPS.BUILD_QUEUE")]
    public partial class BUILD_QUEUE
    {
        [Key]
        public Guid BUILD_QUEUE_ID { get; set; }

        [Required]
        [StringLength(1500)]
        public string SOLUTION_NAME_FULL_PATH { get; set; }

        [StringLength(50)]
        public string BUILD_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string BUILD_STATUS { get; set; }

        [StringLength(100)]
        public string CHANGESET_ID { get; set; }

        [Required]
        [StringLength(4000)]
        public string TRIGGERED_BY_USER { get; set; }

        [Required]
        [StringLength(4000)]
        public string TRIGGERED_BY_EMAIL { get; set; }

        public string BUILD_PARAMS_JSON { get; set; }

        [Required]
        [StringLength(50)]
        public string OPERATOR_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LAST_MODIFIED_TS { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CREATED_TS { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? BUILD_START_TS { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? BUILD_END_TS { get; set; }
    }
}
