namespace HPP.OneClick.XAML.Migration.DevContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UA3_DEVOPS.JOB_CONFIG_HISTORY")]
    public partial class JOB_CONFIG_HISTORY
    {
        [Key]
        public Guid JOB_CONFIG_HISTORY_ID { get; set; }

        public Guid JOB_CONFIG_ID { get; set; }

        [Required]
        [StringLength(1500)]
        public string SOLUTION_NAME_FULL_PATH { get; set; }

        [Required]
        [StringLength(200)]
        public string TARGET_BUILD_ENVRMT { get; set; }

        [Required]
        [StringLength(50)]
        public string APP_RELEASE_NUM { get; set; }

        [Required]
        [StringLength(50)]
        public string BUILD_PLATFORM { get; set; }

        [Required]
        [StringLength(50)]
        public string BUILD_CONFIG { get; set; }

        [Required]
        [StringLength(4000)]
        public string MSBUILD_ARGS { get; set; }

        [Required]
        [StringLength(200)]
        public string DEPLOYMENT_MODEL { get; set; }

        public bool SKIP_PACKAGE_BUILD_FLAG { get; set; }

        public bool SKIP_PACKAGE_DEPLOYMENT_FLAG { get; set; }

        public bool SKIP_PACKAGE_STAGING_FLAG { get; set; }

        public bool SKIP_BUILD_FLDR_CLEAN_FLAG { get; set; }

        public bool CLEAN_WORKSPACE_FLAG { get; set; }

        public bool EXECUTE_TESTCASES_FLAG { get; set; }

        public bool ENABLED_FLAG { get; set; }

        [Required]
        [StringLength(50)]
        public string TESTCASE_PLATFORM { get; set; }

        [Required]
        [StringLength(4000)]
        public string TESTCASE_FILTER { get; set; }

        [Required]
        [StringLength(4000)]
        public string TESTSOURCE_SPEC { get; set; }

        public Guid MODULE_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string DEPLOY_PACKAGE_NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string OPERATOR_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LAST_MODIFIED_TS { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CREATED_TS { get; set; }

        [StringLength(50)]
        public string PRODUCT_NAME { get; set; }

        public Guid BUILD_TYPE_ID { get; set; }

        public string COMBINED_BUILD_ARGS { get; set; }
    }
}
