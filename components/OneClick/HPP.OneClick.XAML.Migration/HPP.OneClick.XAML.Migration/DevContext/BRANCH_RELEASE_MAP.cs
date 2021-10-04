namespace HPP.OneClick.XAML.Migration.DevContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UA3_DEVOPS.BRANCH_RELEASE_MAP")]
    public partial class BRANCH_RELEASE_MAP
    {
        [Key]
        public Guid BRANCH_RELEASE_MAP_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string BRANCH_NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string HPP_RELEASE { get; set; }

        [Required]
        [StringLength(50)]
        public string OPERATOR_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CREATED_TS { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LAST_MODIFIED_TS { get; set; }
    }
}
