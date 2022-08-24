//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Entities
{
    /// <summary>
    /// Table containing the practice location address association
    /// <uol>
    /// <li><b>Table:</b> PRACTICE_LCTN_ADR_ASSCN</li> 
    /// </uol>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Table("PRACTICE_LCTN_ADR_ASSCN")]
    public partial class PracticeLocationAddressAssociation
    {
        /// <summary>
        /// Practice Location Address Association Unique Id, primary key identifier (GUID)
        /// <uol>
        /// <li><b>Column:</b> PRACTICE_LCTN_ADR_ASSCN_ID (PK)</li> 
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> Yes</li>
        /// <li><b>Unique Index:</b> PracticeLocationAddressAssociationId</li>
        /// </uol>
        /// </summary>
        [Key]
        [Column("PRACTICE_LCTN_ADR_ASSCN_ID")]
        public Guid PracticeLocationAddressAssociationId { get; set; }

        /// <summary>
        /// Practice Location Unique Id
        /// <uol>
        /// <li><b>Column:</b> PRACTICE_LOCATION_ID (FK)</li> 
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("PRACTICE_LOCATION_ID")]
        public Guid PracticeLocationId { get; set; }

        /// <summary>
        /// Address Unique Id
        /// <uol>
        /// <li><b>Column:</b> ADDRESS_ID (FK)</li> 
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("ADDRESS_ID")]
        public Guid AddressId { get; set; }

        /// <summary>
        /// Address Type Cd
        /// <uol>
        /// <li><b>Column:</b> ADDRESS_TYPE_CD</li> 
        /// <li><b>Data Type:</b> String</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("ADDRESS_TYPE_CD")]
        [Required]
        [NonUnicode, StringLength(100)]
        public string AddressTypeCd { get; set; }

        /// <summary>
        /// Effective Date
        /// <uol>
        /// <li><b>Column:</b> EFFECTIVE_DT</li> 
        /// <li><b>Data Type:</b> DateTime </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("EFFECTIVE_DT")]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// End Date
        /// <uol>
        /// <li><b>Column:</b> END_DT</li> 
        /// <li><b>Data Type:</b> DateTime</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("END_DT")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Operator Id
        /// <uol>
        /// <li><b>Column:</b> OPERATOR_ID </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("OPERATOR_ID")]
        [Required]
        [NonUnicode, StringLength(200)]
        public string OperatorId { get; set; }

        /// <summary>
        /// Creation Time Stamp
        /// <uol>
        /// <li><b>Column:</b> CREATION_TS </li> 
        /// <li><b>Data Type:</b> DateTime </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("CREATION_TS")]
        public DateTime CreationTs { get; set; }

        /// <summary>
        /// LastModified Time Stamp
        /// <uol>
        /// <li><b>Column:</b> LAST_MODIFIED_TS </li> 
        /// <li><b>Data Type:</b> Date Time </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModifiedTs { get; set; }

        /// <summary>
        /// Is Active
        /// <uol>
        /// <li><b>Column:</b> IS_ACTIVE</li> 
        /// <li><b>Data Type:</b> Bool </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Address Name
        /// <uol>
        /// <li><b>Column:</b> ADDRESS_NAME</li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("ADDRESS_NAME")]
        [NonUnicode, StringLength(250)]
        public string AddressName { get; set; }

        /// <summary>
        /// Email Address
        /// <uol>
        /// <li><b>Column:</b> EMAIL_ADR </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("EMAIL_ADR")]
        [NonUnicode, StringLength(250)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Phone Type 1
        /// <uol>
        /// <li><b>Column:</b> PHONE_TYPE_1 </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_TYPE_1")]
        [NonUnicode, StringLength(50)]
        public string PhoneType1 { get; set; }

        /// <summary>
        /// Phone Number 1
        /// <uol>
        /// <li><b>Column:</b> PHONE_NUM_1 </li> 
        /// <li><b>Data Type:</b> Long </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_NUM_1")]
        public long? PhoneNumber1 { get; set; }

        /// <summary>
        /// Phone Extention 1
        /// <uol>
        /// <li><b>Column:</b> PHONE_EXT_1</li> 
        /// <li><b>Data Type:</b> Short </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_EXT_1")]
        public short? PhoneExtention1 { get; set; }

        /// <summary>
        /// Phone Type 2
        /// <uol>
        /// <li><b>Column:</b> PHONE_TYPE_2 </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_TYPE_2")]
        [NonUnicode, StringLength(50)]
        public string PhoneType2 { get; set; }

        /// <summary>
        /// Phone Number 2
        /// <uol>
        /// <li><b>Column:</b> PHONE_NUM_2 </li> 
        /// <li><b>Data Type:</b> Long </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_NUM_2")]
        public long? PhoneNumber2 { get; set; }

        /// <summary>
        /// Phone Extention 2
        /// <uol>
        /// <li><b>Column:</b> PHONE_EXT_2</li> 
        /// <li><b>Data Type:</b> Short </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_EXT_2")]
        public short? PhoneExtention2 { get; set; }

        /// <summary>
        /// Phone Type 3
        /// <uol>
        /// <li><b>Column:</b> PHONE_TYPE_3 </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_TYPE_3")]
        [NonUnicode, StringLength(50)]
        public string PhoneType3 { get; set; }

        /// <summary>
        /// Phone Number 3
        /// <uol>
        /// <li><b>Column:</b> PHONE_NUM_3 </li> 
        /// <li><b>Data Type:</b> Long </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_NUM_3")]
        public long? PhoneNumber3 { get; set; }

        /// <summary>
        /// Phone Extention 3
        /// <uol>
        /// <li><b>Column:</b> PHONE_EXT_3</li> 
        /// <li><b>Data Type:</b> Short </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_EXT_3")]
        public short? PhoneExtention3 { get; set; }

        /// <summary>
        /// Phone Type 4
        /// <uol>
        /// <li><b>Column:</b> PHONE_TYPE_4 </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_TYPE_4")]
        [NonUnicode, StringLength(50)]
        public string PhoneType4 { get; set; }

        /// <summary>
        /// Phone Number 4
        /// <uol>
        /// <li><b>Column:</b> PHONE_NUM_4 </li> 
        /// <li><b>Data Type:</b> Long </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_NUM_4")]
        public long? PhoneNumber4 { get; set; }

        /// <summary>
        /// Phone Extention 4
        /// <uol>
        /// <li><b>Column:</b> PHONE_EXT_4</li> 
        /// <li><b>Data Type:</b> Short </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("PHONE_EXT_4")]
        public short? PhoneExtention4 { get; set; }

        /// <summary>
        /// Billing Agent Name
        /// <uol>
        /// <li><b>Column:</b> BILLING_AGENT_NAME</li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("BILLING_AGENT_NAME")]
        [NonUnicode, StringLength(70)]
        public string BillingAgentName { get; set; }

        /// <summary>
        /// Contact First Name
        /// <uol>
        /// <li><b>Column:</b> CONTACT_FIRST_NAME</li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("CONTACT_FIRST_NAME")]
        [NonUnicode, StringLength(100)]
        public string ContactFirstName { get; set; }

        /// <summary>
        /// Contact Middle Name
        /// <uol>
        /// <li><b>Column:</b> CONTACT_MIDDLE_NAME </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("CONTACT_MIDDLE_NAME")]
        [NonUnicode, StringLength(100)]
        public string ContactMiddleName { get; set; }

        /// <summary>
        /// Contact Last Name
        /// <uol>
        /// <li><b>Column:</b> CONTACT_LAST_NAME</li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("CONTACT_LAST_NAME")]
        [NonUnicode, StringLength(100)]
        public string ContactLastName { get; set; }

        /// <summary>
        /// Whether Accept New Patients With Special Needs
        /// <uol>
        /// <li><b>Column:</b> IS_ACCEPT_NEW_SP_NEED_PATIENTS </li> 
        /// <li><b>Data Type:</b> Bool </li>
        /// <li><b>Required:</b> Yes </li>
        /// </uol>
        /// </summary>
        [Column("IS_ACCEPT_NEW_SP_NEED_PATIENTS")]
        public bool IsAcceptNewPatientsWithSpecialNeeds { get; set; }

        /// <summary>
        /// Whether Ada Compliant
        /// <uol>
        /// <li><b>Column:</b> IS_ADA_COMPLIANT </li> 
        /// <li><b>Data Type:</b> Bool </li>
        /// <li><b>Required:</b> Yes </li>
        /// </uol>
        /// </summary>
        [Column("IS_ADA_COMPLIANT")]
        public bool IsAdaCompliant { get; set; }

        /// <summary>
        /// Age Restriction Code
        /// <uol>
        /// <li><b>Column:</b> AGE_RESTRICTION_CD </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("AGE_RESTRICTION_CD")]
        [NonUnicode, StringLength(50)]
        public string AgeRestrictionCd { get; set; }

        /// <summary>
        /// Other Restriction
        /// <uol>
        /// <li><b>Column:</b> OTHER_RSTCTN </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("OTHER_RSTCTN")]
        [NonUnicode, StringLength(250)]
        public string OtherRestriction { get; set; }

        /// <summary>
        /// Preferred Communication Code
        /// <uol>
        /// <li><b>Column:</b> PREFERRED_COMMUNICATION_CD </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("PREFERRED_COMMUNICATION_CD")]
        [NonUnicode, StringLength(50)]
        public string PreferredCommunicationCd { get; set; }

        /// <summary>
        /// After Hours Arrangement
        /// <uol>
        /// <li><b>Column:</b> AFTER_HOURS_ARRANGEMENT </li> 
        /// <li><b>Data Type:</b> String </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("AFTER_HOURS_ARRANGEMENT")]
        [NonUnicode, StringLength(200)]
        public string AfterHoursArrangement { get; set; }

        /// <summary>
        /// Is Near Public Transportation
        /// <uol>
        /// <li><b>Column:</b> IS_NEAR_PUBLIC_TRANSPORTATION </li> 
        /// <li><b>Data Type:</b> bool </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("IS_NEAR_PUBLIC_TRANSPORTATION")]
        public bool? IsNearPublicTransportation { get; set; }

        [ForeignKey("AddressId")]
        public virtual ADDRESS ADDRESS { get; set; }

        ////[ForeignKey("PracticeLocationId")]
        ////public virtual PRACTICE_LOCATION PRACTICE_LOCATION { get; set; }
    }
}
