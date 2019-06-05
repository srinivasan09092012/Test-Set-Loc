//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved. 
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
    [Table("IRS_TAX_IDENTIFICATION")]
    [ExcludeFromCodeCoverage]
    public partial class IRSTaxIdentification
    {
        public IRSTaxIdentification()
        {
            this.PROVIDER_IRS_TAX_ID_ASSCN = new HashSet<IRSTaxIdAssociation>();
        }

        /// <summary>
        /// System assigned GUID to uniquely identify a IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> IRS_TAX_IDENTIFICATION_ID (PK)</li> 
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> Yes</li>
        /// <li><b>Unique Index:</b>IRS_TAX_IDENTIFICATION_ID_PK</li>
        /// </uol>
        /// </summary>
        [Key]
        [Column("IRS_TAX_IDENTIFICATION_ID")]
        public Guid IRSTaxIdentificationId { get; set; }

        /// <summary>
        /// The Tax type for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> TAX_ID_TYPE_CD</li> 
        /// <li><b>Data Type:</b> string, StringLength(100)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary> 
        [Required]
        [StringLength(100)]
        [Column("TAX_ID_TYPE_CD")]
        public string TaxIdTypeCode { get; set; }

        /// <summary>
        /// The IRS tax id for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> IRS_TAX_ID </li> 
        /// <li><b>Data Type:</b> string, StringLength(9)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary> 
        [Required]
        [StringLength(9)]
        [Column("IRS_TAX_ID")]
        public string IRSTaxId { get; set; }

        /// <summary>
        /// The IRS tax id name for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> TAX_ID_NAME </li> 
        /// <li><b>Data Type:</b> string, StringLength(300)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary> 
        [Required]
        [StringLength(300)]
        [Column("TAX_ID_NAME")]
        public string TaxIdName { get; set; }

        /// <summary>
        /// Effective start date of the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> EFFECTIVE_DT</li> 
        /// <li><b>Data Type:</b> DateTime</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("EFFECTIVE_DT")]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Effective end date of the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> END_DT</li> 
        /// <li><b>Data Type:</b> DateTime</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("END_DT")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The Address Line 1 for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> ADR_LINE_1</li> 
        /// <li><b>Data Type:</b> string, StringLength(100)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary> 
        [Required]
        [StringLength(100)]
        [Column("ADR_LINE_1")]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The Address Line 1 for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> ADR_LINE_2</li> 
        /// <li><b>Data Type:</b> string, StringLength(100)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary> 
        [StringLength(100)]
        [Column("ADR_LINE_2")]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The city for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> CITY</li> 
        /// <li><b>Data Type:</b> string, StringLength(100)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary> 
        [Required]
        [StringLength(100)]
        [Column("CITY")]
        public string City { get; set; }

        /// <summary>
        /// The state for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> STATE_CD</li> 
        /// <li><b>Data Type:</b> string, StringLength(100)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>         
        [Required]
        [StringLength(100)]
        [Column("STATE_CD")]
        public string StateCode { get; set; }

        /// <summary>
        /// The postal code for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> POSTAL_CD</li> 
        /// <li><b>Data Type:</b> string, StringLength(15)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary> 
        [Required]
        [StringLength(15)]
        [Column("POSTAL_CD")]
        public string PostalCode { get; set; }

        /// <summary>
        /// The country code for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> COUNTRY_CD</li> 
        /// <li><b>Data Type:</b> string, StringLength(100)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>  
        [StringLength(100)]
        [Column("COUNTRY_CD")]
        public string CountryCode { get; set; }

        /// <summary>
        /// The phone number for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> PHONE_NUM</li> 
        /// <li><b>Data Type:</b> string, StringLength(15)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>  
        [StringLength(15)]
        [Column("PHONE_NUM")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The phone extenstion for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> PHONE_EXT</li> 
        /// <li><b>Data Type:</b> string, StringLength(5)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>  
        [StringLength(5)]
        [Column("PHONE_EXT")]
        public string PhoneExtension { get; set; }

        /// <summary>
        /// The Is tax id exempt for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> IS_TAX_ID_EXEMPT</li> 
        /// <li><b>Data Type:</b> bool</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>  
        [Column("IS_TAX_ID_EXEMPT")]
        public bool IsTaxIdExempt { get; set; }

        /// <summary>
        /// The Is form W9 exempt for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> IS_FORM_W9</li> 
        /// <li><b>Data Type:</b> bool</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>  
        [Column("IS_FORM_W9")]
        public bool IsFormW9 { get; set; }

        /// <summary>
        /// The Is form W147 for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> IS_FORM_147</li> 
        /// <li><b>Data Type:</b> bool</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>  
        [Column("IS_FORM_147")]
        public bool IsForm147 { get; set; }

        /// <summary>
        /// The Is active for the IRS tax identification record
        /// <uol>
        /// <li><b>Column:</b> IS_ACTIVE</li> 
        /// <li><b>Data Type:</b> bool</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>  
        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        /// <summary>
        /// The operator id which the provider managing employee asscociation record inserted or updated
        /// <uol>
        /// <li><b>Column:</b> OPERATOR_ID</li> 
        /// <li><b>Data Type:</b> string, StringLength(50)</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>   
        [Required]
        [StringLength(50)]
        [Column("OPERATOR_ID")]
        public string OperatorId { get; set; }

        /// <summary>
        /// Last modified timestamp, DB default to UTC systimestamp
        /// <uol>
        /// <li><b>Column:</b> LAST_MODIFIED_TS</li> 
        /// <li><b>Data Type:</b> DateTime</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// The creation date time stamp of the provider managing employee asscociation record 
        /// <uol>
        /// <li><b>Column:</b> CREATION_TS</li> 
        /// <li><b>Data Type:</b> DateTime</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>  
        [Column("CREATION_TS")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Collection of details associated with an Provider Tax Id, navigable to list of PROVIDER_IRS_TAX_ID_ASSCN EF Entities (lazy load)
        /// </summary>  
        public virtual ICollection<IRSTaxIdAssociation> PROVIDER_IRS_TAX_ID_ASSCN { get; set; }
    }
}
