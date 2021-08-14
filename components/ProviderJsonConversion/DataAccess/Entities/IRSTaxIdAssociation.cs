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
    [Table("PROVIDER_IRS_TAX_ID_ASSCN")]
    [ExcludeFromCodeCoverage]
    public partial class IRSTaxIdAssociation
    {
        [Key]
        [Column("PROVIDER_IRS_TAX_ASSCN_ID")]
        public Guid IRSTaxIdAssociationId { get; set; }

        [Column("PROVIDER_SERVICE_LOCATION_ID")]
        public Guid ServiceLocationId { get; set; }

        [Column("IRS_TAX_IDENTIFICATION_ID")]
        public Guid IRSTaxIdentificationId { get; set; }

        [Column("EFFECTIVE_DT")]
        public DateTime EffectiveDate { get; set; }

        [Column("END_DT")]
        public DateTime EndDate { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(50)]
        [Column("OPERATOR_ID")]
        public string OperatorId { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModifiedDate { get; set; }

        [Column("CREATION_TS")]
        public DateTime CreationDate { get; set; }

        ////[ForeignKey("IRSTaxIdentificationId")]
        ////public virtual IRSTaxIdentification IRS_TAX_IDENTIFICATION { get; set; }

        [ForeignKey("ServiceLocationId")]
        public virtual PROVIDER_SERVICE_LOCATION PROVIDER_SERVICE_LOCATION { get; set; }
    }
}
