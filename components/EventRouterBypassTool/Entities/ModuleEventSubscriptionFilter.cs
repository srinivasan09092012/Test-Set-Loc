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
    /// The Module Event Subscription Filter table holds event filter set criteria to enable subscribers to customize
    /// criteria used to determine whether they want to receive the event or not.
    /// <uol>
    /// <li><b>Table:</b>MODULE_EVENTS_SUBSCR_FILTER</li> 
    /// <li><b>References:</b>None</li> 
    /// <li><b>Referenced By:</b>MODULE_EVENTS_SUBSCRIPTION</li> 
    /// </uol>
    /// </summary>
    [Table("MODULE_EVENTS_SUBSCR_FILTER")]
    public partial class ModuleEventSubscriptionFilter
    {
        /// <summary>
        ///  Unique GUID to identify Module Event Subscription Filter.
        /// <uol>
        /// <li><b>Column:</b>SUBSCRIPTION_FILTER_ID</li> 
        /// <li><b>Data Type:</b>Guid</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>EventSubscriptionFilterID</li> 
        /// </uol>
        /// </summary>
        [Key]
        [Column("SUBSCRIPTION_FILTER_ID")]
        public Guid EventSubscriptionFilterID { get; set; }

        /// <summary>
        ///  Unique GUID to identify Module Event Subscription.
        /// <uol>
        /// <li><b>Column:</b>EVENT_SUBSCRIPTION_ID</li> 
        /// <li><b>Data Type:</b>Guid</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>EventSubscriptionID</li> 
        /// </uol>
        /// </summary>
        [Column("EVENT_SUBSCRIPTION_ID")]
        public Guid EventSubscriptionID { get; set; }

        /// <summary>
        ///  Unique GUID to identify Module Events.
        /// <uol>
        /// <li><b>Column:</b>MODULE_EVENT_ID</li> 
        /// <li><b>Data Type:</b>Guid</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("MODULE_EVENT_ID")]
        public Guid ModuleEventID { get; set; }

        /// <summary>
        /// The event subscription filter set name
        /// <uol>
        /// <li><b>Column:</b>FILTER_SET_NAME</li> 
        /// <li><b>Data Type:</b>string</li> 
        /// <li><b>Required:</b>No</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [StringLength(200)]
        [Column("FILTER_SET_NAME")]
        public string FilterSetName { get; set; }

        /// <summary>
        /// The event subscriber filter set criteria Json
        /// <uol>
        /// <li><b>Column:</b>FILTER_SET_CRITERIA</li> 
        /// <li><b>Data Type:</b>string</li> 
        /// <li><b>Required:</b>No</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [StringLength(4000)]
        [Column("FILTER_SET_CRITERIA")]
        public string FilterSetCriteria { get; set; }

        /// <summary>
        /// If isActive set to true then the event is currently being used.  
        /// If false then the event is not being used.
        /// <uol>
        /// <li><b>Column:</b>IS_VALID</li> 
        /// <li><b>Data Type:</b>bool</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("IS_VALID")]
        public bool IsValid { get; set; }

        /// <summary>
        /// Operator ID that last updated the event subscription filter set record
        /// <uol>
        /// <li><b>Column:</b> OPERATOR_ID</li>
        /// <li><b>Data Type:</b> string, StringLength(50)</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [StringLength(50)]
        [Column("OPERATOR_ID")]
        public string OperatorId { get; set; }

        /// <summary>
        ///  The date and time the row was created. 
        /// <uol>
        /// <li><b>Column:</b>CREATED_TS</li> 
        /// <li><b>Data Type:</b>DateTime</li> 
        /// <li><b>Required:</b>No</li> 
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
        /// <li><b>Required:</b>No</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("LAST_MODIFIED_TS")]
        public DateTime? ModifiedDate { get; set; }
    }
}
