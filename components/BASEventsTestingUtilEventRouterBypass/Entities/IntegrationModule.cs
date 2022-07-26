//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRouterByPassTool.Entities
{
    /// <summary>
    /// The Module table holds information related to integration module
    /// such as the Module Name and if its an active module.
    /// <uol>
    /// <li><b>Table:</b>MODULE</li> 
    /// <li><b>References:</b>None</li> 
    /// <li><b>Referenced By:</b>MODULE_EVENTS</li> 
    /// </uol>
    /// </summary>
    [Table("MODULE")]
    public partial class IntegrationModule
    {
        /// <summary>
        /// Unique GUID to identify Module.
        /// <uol>
        /// <li><b>Column:</b>MODULE_ID</li> 
        /// <li><b>Data Type:</b>Guid</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>ModuleID</li> 
        /// </uol>
        /// </summary>
        [Key]
        [Column("MODULE_ID")]
        public Guid ModuleID { get; set; }

        /// <summary>
        /// Name of the Module.
        /// <uol>
        /// <li><b>Column:</b>MODULE_NAME</li> 
        /// <li><b>Data Type:</b>string</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b></li> 
        /// </uol>
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("MODULE_NAME")]
        public string ModuleName { get; set; }

        /// <summary>
        /// If isActive set to true then the Module is 
        /// currently being used.  If false then the Module
        /// is not being used.
        /// <uol>
        /// <li><b>Column:</b>IS_ACTIVE</li> 
        /// <li><b>Data Type:</b>bool</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        /// <summary>
        ///  The date and time the row was created. 
        /// <uol>
        /// <li><b>Column:</b>CREATED_TS</li> 
        /// <li><b>Data Type:</b>DateTime</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("CREATED_TS")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        ///  The last date and time the row was modified. 
        /// <uol>
        /// <li><b>Column:</b>LAST_MODIFIED_TS</li> 
        /// <li><b>Data Type:</b>DateTime</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("MODIFIED_TS")]
        public DateTime? ModifiedDate { get; set; }
    }
}
