//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("PROVIDER")]
    public partial class PROVIDER
    {
        public PROVIDER()
        {
            this.PROVIDER_SERVICE_LOCATION = new HashSet<PROVIDER_SERVICE_LOCATION>();
        }

        [Key]
        [Column("PROVIDER_ID")]
        public Guid PROVIDER_ID { get; set; }

        [Required]
        [Column("BASE_ID")]
        public long BASE_ID { get; set; }

        [StringLength(50)]
        [Column("BUSINESS_NAME")]
        public string BUSINESS_NAME { get; set; }

        [Required]
        [StringLength(100)]
        [Column("PROVIDER_CATEGORY_CD")]
        public string PROVIDER_CATEGORY_CD { get; set; }

        [Column("HAS_OWNERSHIP_INTEREST")]
        public bool? HAS_OWNERSHIP_INTEREST { get; set; }

        [StringLength(50)]
        [Column("DOING_BUSINESS_AS_NAME")]
        public string DOING_BUSINESS_AS_NAME { get; set; }

        [Required]
        [StringLength(50)]
        [Column("OPERATOR_ID")]
        public string OPERATOR_ID { get; set; }

        [Column("CREATION_TS")]
        public DateTime CREATION_TS { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LAST_MODIFIED_TS { get; set; }

        [Column("IS_ACTIVE")]
        public bool IS_ACTIVE { get; set; }

        public virtual ICollection<PROVIDER_SERVICE_LOCATION> PROVIDER_SERVICE_LOCATION { get; set; }
    }
}
