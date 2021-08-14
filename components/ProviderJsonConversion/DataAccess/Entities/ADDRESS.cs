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
    /// <summary>
    /// Table containing the address details 
    /// /// <uol>
    /// <li><b>Table:</b>ADDRESS</li>    
    /// <li><b>References:</b> ADDRESS_ID </li> 
    /// <li><b>Referenced By:</b> none </li> 
    /// </uol>
    /// </summary>    
    [Table("ADDRESS")]
    [ExcludeFromCodeCoverage]
    public partial class ADDRESS
    {
        public ADDRESS()
        {
           this.PracticeLocationAddressAssociation = new HashSet<PracticeLocationAddressAssociation>();
        }

        /// <summary>
        /// System assigned GUID to uniquely identify a Address
        /// <uol>
        /// <li><b>Column:</b>ADDRESS_ID (PK)</li> 
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> Yes</li>
        /// <li><b>Unique Index:</b>ADDRESS_ID_PK</li>
        /// </uol>
        /// </summary>        
        [Key]
        [Column("ADDRESS_ID")]
        public Guid ADDRESS_ID { get; set; }

        /// <summary>
        /// Address Line 1
        /// <uol>
        /// <li><b>Column:</b> ADR_LINE_1 </li> 
        /// <li><b>Data Type:</b> string </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column("ADR_LINE_1")]
        public string ADR_LINE_1 { get; set; }

        /// <summary>
        /// Address Line 2
        /// <uol>
        /// <li><b>Column:</b> ADR_LINE_2 </li> 
        /// <li><b>Data Type:</b> string </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [StringLength(100)]
        [Column("ADR_LINE_2")]
        public string ADR_LINE_2 { get; set; }

        /// <summary>
        /// CITY
        /// <uol>
        /// <li><b>Column:</b> CITY </li> 
        /// <li><b>Data Type:</b> string </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column("CITY")]
        public string CITY { get; set; }

        /// <summary>
        /// STATE CD
        /// <uol>
        /// <li><b>Column:</b> STATE_CD </li> 
        /// <li><b>Data Type:</b> string </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column("STATE_CD")]
        public string STATE_CD { get; set; }

        /// <summary>
        /// POSTAL CD
        /// <uol>
        /// <li><b>Column:</b> POSTAL_CD </li> 
        /// <li><b>Data Type:</b> string </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Required]
        [StringLength(15)]
        [Column("POSTAL_CD")]
        public string POSTAL_CD { get; set; }

        /// <summary>
        /// COUNTY CD
        /// <uol>
        /// <li><b>Column:</b> COUNTY_CD </li> 
        /// <li><b>Data Type:</b> string </li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [StringLength(100)]
        [Column("COUNTY_CD")]
        public string COUNTY_CD { get; set; }

        /// <summary>
        /// COUNTRY CD
        /// <uol>
        /// <li><b>Column:</b> COUNTRY CD </li> 
        /// <li><b>Data Type:</b> string </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column("COUNTRY_CD")]
        public string COUNTRY_CD { get; set; }

        /// <summary>
        /// GEO CD
        /// <uol>
        /// <li><b>Column:</b> GEO_CD </li> 
        /// <li><b>Data Type:</b> string </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [StringLength(32)]
        [Column("GEO_CD")]
        public string GEO_CD { get; set; }

        /// <summary>
        /// LATITUDE COORDINATE NUMBER
        /// <uol>
        /// <li><b>Column:</b> LATITUDE_COORDINATE_NUM </li> 
        /// <li><b>Data Type:</b> decimal </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("LATITUDE_COORDINATE_NUM")]
        public decimal? LATITUDE_COORDINATE_NUM { get; set; }

        /// <summary>
        /// LONGITUDE COORDINATE NUMBER
        /// <uol>
        /// <li><b>Column:</b> LONGITUDE_COORDINATE_NUM </li> 
        /// <li><b>Data Type:</b> decimal </li>
        /// <li><b>Required:</b> No </li>
        /// </uol>
        /// </summary>
        [Column("LONGITUDE_COORDINATE_NUM")]
        public decimal? LONGITUDE_COORDINATE_NUM { get; set; }

        /// <summary>
        /// OPERATOR ID
        /// <uol>
        /// <li><b>Column:</b> OPERATOR_ID </li> 
        /// <li><b>Data Type:</b> string </li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("OPERATOR_ID")]
        public string OPERATOR_ID { get; set; }

        /// <summary>
        /// The creation date time stamp of the Address record 
        /// <uol>
        /// <li><b>Column:</b> CREATED_TS</li> 
        /// <li><b>Data Type:</b> DateTime</li>        
        /// </uol>
        /// </summary> 
        [Column("CREATION_TS")]
        public DateTime CREATION_TS { get; set; }

        /// <summary>
        /// Last modified timestamp, DB default to UTC systimestamp
        /// <uol>
        /// <li><b>Column:</b> LAST_MODIFIED_TS</li> 
        /// <li><b>Data Type:</b> DateTime</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("LAST_MODIFIED_TS")]
        public DateTime LAST_MODIFIED_TS { get; set; }

        /// <summary>
        /// The active flag
        /// <uol>
        /// <li><b>Column:</b> IS_ACTIVE</li> 
        /// <li><b>Data Type:</b> bool</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("IS_ACTIVE")]
        public bool IS_ACTIVE { get; set; }

        /// <summary>
        /// Collection of details associated with a Provider Location Address, navigable to list of PRACTICE_LCTN_ADR_ASSCN EF Entities (lazy load)
        /// </summary>    
        public virtual ICollection<PracticeLocationAddressAssociation> PracticeLocationAddressAssociation { get; set; }
    }
}
