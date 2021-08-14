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
    [Table("PROVIDER_SERVICE_LOCATION")]
    public partial class PROVIDER_SERVICE_LOCATION
    {
        public PROVIDER_SERVICE_LOCATION()
        {           
        }

        [Key]
        [Column("PROVIDER_SERVICE_LOCATION_ID")]
        public Guid PROVIDER_SERVICE_LOCATION_ID { get; set; }

        [Column("PRACTICE_LOCATION_ID")]
        public Guid PRACTICE_LOCATION_ID { get; set; }

        [Column("PROVIDER_ID")]
        public Guid PROVIDER_ID { get; set; }

        [Column("NPI")]
        public long? NPI { get; set; }

        [Required]
        [StringLength(50)]
        [Column("PROVIDER_BILLING_ID")]
        public string PROVIDER_BILLING_ID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("PROVIDER_TYPE_CD")]
        public string PROVIDER_TYPE_CD { get; set; }

        [Column("EFFECTIVE_DT")]
        public DateTime EFFECTIVE_DT { get; set; }

        [Column("END_DT")]
        public DateTime END_DT { get; set; }

        [Column("INCORPORATION_DT")]
        public DateTime? INCORPORATION_DT { get; set; }

        [Column("IS_INCORPORATED")]
        public bool IS_INCORPORATED { get; set; }

        [StringLength(100)]
        [Column("ORGANIZATION_TYPE")]
        public string ORGANIZATION_TYPE { get; set; }

        [Required]
        [StringLength(25)]
        [Column("PREFERRED_COMM_METHOD_CD")]
        public string PREFERRED_COMM_METHOD_CD { get; set; }

        [Column("IS_ELECTRONIC_SUBMISSION")]
        public bool IS_ELECTRONIC_SUBMISSION { get; set; }

        [Column("HAS_ELECTRONIC_SUB_WAIVER")]
        public bool HAS_ELECTRONIC_SUBMISSION_WAIVER { get; set; }

        [Column("IS_SIGNATURE_ON_FILE")]
        public bool IS_SIGNATURE_ON_FILE { get; set; }

        [StringLength(200)]
        [Column("TAX_CLASSIFICATION")]
        public string TAX_CLASSIFICATION { get; set; }

        [Column("BUSINESS_START_DT")]
        public DateTime? BUSINESS_START_DT { get; set; }

        [StringLength(1)]
        [Column("REVALIDATION_FLAG")]
        public string REVALIDATION_FLAG { get; set; }

        [Required]
        [StringLength(50)]
        [Column("OPERATOR_ID")]
        public string OPERATOR_ID { get; set; }

        [Column("IS_ACTIVE")]
        public bool IS_ACTIVE { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LAST_MODIFIED_TS { get; set; }

        [Column("CREATION_TS")]
        public DateTime CREATION_TS { get; set; }

        [Column("REVALIDATION_DT")]
        public DateTime REVALIDATION_DT { get; set; }

        [Column("ORIGINAL_ENROLLMENT_DT")]
        public DateTime? ORIGINAL_ENROLLMENT_DT { get; set; }

        [Column("LAST_ENROLLMENT_DT")]
        public DateTime? LAST_ENROLLMENT_DT { get; set; }

        [Column("LAST_APPROVAL_DT")]
        public DateTime? LAST_APPROVAL_DT { get; set; }

        [Column("PRE_ENROLLMENT_SITE_VISIT_DT")]
        public DateTime? PRE_ENROLLMENT_SITE_VISIT_DT { get; set; }

        [Column("POST_ENROLLMENT_SITE_VISIT_DT")]
        public DateTime? POST_ENROLLMENT_SITE_VISIT_DT { get; set; }

        [Column("BACKGROUND_CHECK_DT")]
        public DateTime? BACKGROUND_CHECK_DT { get; set; }

        [Column("IS_SUPRESS_RA")]
        public bool IS_SUPRESS_RA { get; set; }

        [Column("IS_WITHHOLD_FICA")]
        public bool IS_WITHHOLD_FICA { get; set; }

        [Column("IS_OPEN_LIEN")]
        public bool IS_OPEN_LIEN { get; set; }

        [Column("IS_HEALTHCARE")]
        public bool IS_HEALTHCARE { get; set; }

        [StringLength(100)]
        [Column("FIRST_NAME")]
        public string FIRST_NAME { get; set; }

        [StringLength(100)]
        [Column("LAST_NAME")]
        public string LAST_NAME { get; set; }

        [StringLength(40)]
        [Column("TITLE")]
        public string TITLE { get; set; }

        [StringLength(15)]
        [Column("PHONE_NUM")]
        public string PHONE_NUM { get; set; }

        [StringLength(5)]
        [Column("PHONE_NUM_EXTN")]
        public string PHONE_NUM_EXTN { get; set; }

        [StringLength(150)]
        [Column("EMAIL_ADDRESS")]
        public string EMAIL_ADDRESS { get; set; }

        [StringLength(15)]
        [Column("FAX_PHONE_NUM")]
        public string FAX_PHONE_NUM { get; set; }

        [Column("IS_REVALIDATION_NOTICE")]
        public bool IS_REVALIDATION_NOTICE { get; set; }

        [StringLength(100)]
        [Column("ENRLMT_TYPE_CD")]
        public string ENRLMT_TYPE_CD { get; set; }

        [Column("IS_CHAIN_AFFILIATED")]
        public bool IS_CHAIN_AFFILIATED { get; set; }

        [Column("ENROLLMENT_APP_DETAIL_ID")]
        public Guid? EnrollmentFileId { get; set; }

        [Column("IS_OPERATED_BY_A_MGMT_COMPANY")]
        public bool IS_OPERATED_BY_A_MGMT_COMPANY { get; set; }

        [Column("IS_RGD_WITH_THE_SECY_OF_STATE")]
        public bool IS_RGD_WITH_THE_SECY_OF_STATE { get; set; }

        [StringLength(100)]
        [Column("MIDDLE_NAME")]
        public string MIDDLE_NAME { get; set; }

        [Column("IS_DOMESTIC_OWNED_CORPORATION")]
        public bool? IS_DOMESTIC_OWNED_CORPORATION { get; set; }

        [Column("IS_FOREIGN_OWNED_CORPORATION")]
        public bool? IS_FOREIGN_OWNED_CORPORATION { get; set; }

        [StringLength(250)]
        [Column("WEBSITE_URL")]
        public string WEBSITE_URL { get; set; }

        [Column("PCP_STATUS")]
        public bool? IS_PRIMARY_CARE_PHYSICIAN { get; set; }

        [Column("GENDER_RESTRICTION_CD")]
        [StringLength(200)]
        public string GenderRestrictionCode { get; set; }

        [Column("ENROLLMENT_PROGRAM_TYPE")]
        [StringLength(200)]
        public string EnrollmentProgramType { get; set; }

        /// <summary>
        /// Preferred communication language code
        /// <uol>
        /// <li><b>Column:</b> PREFERRED_COMM_lANG_CD </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PREFERRED_COMM_LANG_CD")]
        [StringLength(200)]
        public string PreferredCommunicationLanguage { get; set; }

        /// <summary>
        /// Age Restriction
        /// <uol>
        /// <li><b>Column:</b> AGE_RESTRICTION </li> 
        /// <li><b>Data Type:</b> Number </li>
        /// <li><b>Required:</b> Yes </li>
        /// </uol>
        /// </summary>
        [Column("AGE_RESTRICTION")]
        public bool? IsAgeRestricted { get; set; }

        /// <summary>
        /// Age Restriction Min
        /// <uol>
        /// <li><b>Column:</b> AGE_RESTRICTION_MIN </li> 
        /// <li><b>Data Type:</b> Number </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("AGE_RESTRICTION_MIN")]
        public int? AgeRestrictionMinimum { get; set; }

        /// <summary>
        /// Age Restriction Max
        /// <uol>
        /// <li><b>Column:</b> AGE_RESTRICTION_MAX </li> 
        /// <li><b>Data Type:</b> Number </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("AGE_RESTRICTION_MAX")]
        public int? AgeRestrictionMaximum { get; set; }

        /// <summary>
        /// Whether Opt Out Provider Directory
        /// <uol>
        /// <li><b>Column:</b> IS_OPT_OUT_PROVIDER_DIRECTORY </li> 
        /// <li><b>Data Type:</b> Bool </li>
        /// <li><b>Required:</b> Yes </li>
        /// </uol>
        /// </summary>
        [Column("IS_OPT_OUT_PROVIDER_DIRECTORY")]
        public bool? IsOptOutProviderDirectory { get; set; }

        /// <summary>
        /// Accept New Patients
        /// <uol>
        /// <li><b>Column:</b> IS_ACCEPTING_PATIENTS </li> 
        /// <li><b>Data Type:</b> Number </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("IS_ACCEPTING_PATIENTS")]
        public bool IsAcceptingPatients { get; set; }

        /// <summary>
        /// Type of New Patients Accepted
        /// <uol>
        /// <li><b>Column:</b> Is_Accepting_Patients </li> 
        /// <li><b>Data Type:</b> Number </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("TYPE_OF_PATIENTS_CD")]
        public string AcceptNewPatientsCode { get; set; }

        /// <summary>
        /// Location Termination Reason
        /// <uol>
        /// <li><b>Column:</b> TERMINATION_REASON </li> 
        /// <li><b>Data Type:</b> string </li>
        /// <li><b>Required:</b> Yes </li>
        /// </uol>
        /// </summary>
        [Column("TERMINATION_REASON")]
        [StringLength(200)]
        public string TERMINATION_REASON { get; set; }

        /// <summary>
        /// Location Termination Date
        /// <uol>
        /// <li><b>Column:</b> TERMINATION_DT </li> 
        /// <li><b>Data Type:</b> DateTime </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("TERMINATION_DT")]
        public DateTime? TERMINATION_DT { get; set; }

        /// <summary>
        /// Registration Type of Provider
        /// <uol>
        /// <li><b>Column:</b> REGISTRATION_TYPE </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("REGISTRATION_TYPE")]
        public string RegistrationType { get; set; }

        /// <summary>
        /// Whether Accept New Patients With Special Needs
        /// <uol>
        /// <li><b>Column:</b> IS_ACCEPT_SP_NEED_PATIENTS </li> 
        /// <li><b>Data Type:</b> Bool </li>
        /// <li><b>Required:</b> Yes </li>
        /// </uol>
        /// </summary>
        [Column("IS_ACCEPT_SP_NEED_PATIENTS")]
        public bool? IsAcceptNewSpNeedPatients { get; set; }
    }
}
