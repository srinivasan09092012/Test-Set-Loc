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
    /// The Module Events table holds information related to integration module events
    /// such as the Module and Event Name
    /// <uol>
    /// <li><b>Table:</b>MODULE_EVENTS</li> 
    /// <li><b>References:</b>None</li> 
    /// <li><b>Referenced By:</b>MODULE_EVENT_SUBSCRIPTION</li> 
    /// </uol>
    /// </summary>
    [Table("MODULE_EVENTS")]
    public partial class ModuleEvent
    {
        /// <summary>
        ///  Unique GUID to identify Module Events.
        /// <uol>
        /// <li><b>Column:</b>MODULE_EVENT_ID</li> 
        /// <li><b>Data Type:</b>Guid</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>ModuleEventID</li> 
        /// </uol>
        /// </summary>
        [Key]
        [Column("MODULE_EVENT_ID")]
        public Guid ModuleEventID { get; set; }

        /// <summary>
        ///  Unique GUID to identify Module.
        /// <uol>
        /// <li><b>Column:</b>MODULE_ID</li> 
        /// <li><b>Data Type:</b>byte[]</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("MODULE_ID")]
        public Guid ModuleID { get; set; }

        /// <summary>
        ///  The name of the Event Type.
        /// <uol>
        /// <li><b>Column:</b>EVENT_TYPE_NAME</li> 
        /// <li><b>Data Type:</b>string</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [StringLength(200)]
        [Column("EVENT_TYPE_NAME")]
        public string EventTypeName { get; set; }

        /// <summary>
        /// If isActive set to true then the event is currently being used.  
        /// If false then the event is not being used.
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

        /// <summary>
        /// If is_Sequential set to true then the published event will be added to Redis queue and processed sequentially.  
        /// If false then the event will be published to the pub sub channel in Redis and processed in parallel. in parallel. Default value is false.
        /// <uol>
        /// <li><b>Column:</b>IS_SEQUENTIAL</li> 
        /// <li><b>Data Type:</b>bool</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("IS_SEQUENTIAL")]
        public bool IsSequential { get; set; }
    }
}
