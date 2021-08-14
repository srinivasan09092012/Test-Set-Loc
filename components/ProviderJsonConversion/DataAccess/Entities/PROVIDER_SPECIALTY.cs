//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("PROVIDER_SPECIALTY")]
    public partial class PROVIDER_SPECIALTY
    {
        [Key]
        [Column("PROVIDER_SPECIALTY_ID")]
        public Guid PROVIDER_SPECIALTY_ID { get; set; }

        [Column("PROVIDER_SERVICE_LOCATION_ID")]
        public Guid PROVIDER_SERVICE_LOCATION_ID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("SPECIALTY_CD")]
        public string SPECIALTY_CD { get; set; }

        [Column("EFFECTIVE_DT")]
        public DateTime EFFECTIVE_DT { get; set; }

        [Column("END_DT")]
        public DateTime END_DT { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LAST_MODIFIED_TS { get; set; }

        [Column("CREATION_TS")]
        public DateTime CREATION_TS { get; set; }

        [Required]
        [StringLength(50)]
        [Column("OPERATOR_ID")]
        public string OPERATOR_ID { get; set; }

        [Column("IS_PRIMARY")]
        public bool IS_PRIMARY { get; set; }

        [Column("IS_ACTIVE")]
        public bool IS_ACTIVE { get; set; }

        [StringLength(100)]
        [Column("TAXONOMY_CD")]
        public string TAXONOMY_CD { get; set; }

        public virtual PROVIDER_SERVICE_LOCATION PROVIDER_SERVICE_LOCATION { get; set; }
    }
}
