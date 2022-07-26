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
    /// The Module Event Subscription table holds information related to module event subscribers
    /// such as the Module and Event Name
    /// <uol>
    /// <li><b>Table:</b>MODULE_EVENTS_SUBSCRIPTION</li> 
    /// <li><b>References:</b>None</li> 
    /// <li><b>Referenced By:</b>MODULE_EVENT</li> 
    /// </uol>
    /// </summary>
    [Table("MODULE_EVENTS_SUBSCRIPTION")]
        public partial class ModuleEventSubscription
        {
            /// <summary>
            ///  Unique GUID to identify Module Event Subscription.
            /// <uol>
            /// <li><b>Column:</b>EVENT_SUBSCRIPTION_ID</li> 
            /// <li><b>Data Type:</b>Guid</li> 
            /// <li><b>Required:</b>Yes</li> 
            /// <li><b>Unique Index:</b>EventSubscriptionID</li> 
            /// </uol>
            /// </summary>
            [Key]
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
            /// The event subscriber name
            /// <uol>
            /// <li><b>Column:</b>SUBSCRIBER_NAME</li> 
            /// <li><b>Data Type:</b>string</li> 
            /// <li><b>Required:</b>Yes</li> 
            /// <li><b>Unique Index:</b>None</li> 
            /// </uol>
            /// </summary>
            [StringLength(200)]
            [Column("SUBSCRIBER_NAME")]
            public string SubscriberName { get; set; }

            /// <summary>
            /// The event subscriber delivery URL used to publish event
            /// <uol>
            /// <li><b>Column:</b>DELIVERY_URL</li> 
            /// <li><b>Data Type:</b>string</li> 
            /// <li><b>Required:</b>Yes</li> 
            /// <li><b>Unique Index:</b>None</li> 
            /// </uol>
            /// </summary>
            [StringLength(500)]
            [Column("DELIVERY_URL")]
            public string DeliveryURL { get; set; }

            /// <summary>
            /// If isActive set to true then the event is currently being used.  
            /// If false then the event is not being used.
            /// <uol>
            /// <li><b>Column:</b>ISACTIVE</li> 
            /// <li><b>Data Type:</b>bool</li> 
            /// <li><b>Required:</b>Yes</li> 
            /// <li><b>Unique Index:</b>None</li> 
            /// </uol>
            /// </summary>
            [Column("ISACTIVE")]
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

            [Column("ISQUEUE")]
            public bool IsQueue { get; set; }

            /// <summary>
            /// Gets or sets the binding configuration
            /// </summary>
            /// <value>
            /// Binding Config.
            /// </value>
            [Column("BINDING_CONFIG")]
            [StringLength(100)]
            public string BindingConfig { get; set; }

            /// <summary>
            /// Gets or sets the binding name
            /// </summary>
            /// <value>
            /// Binding name.
            /// </value>
            [Column("BINDING_NAME")]
            [StringLength(50)]
            public string BindingName { get; set; }

            /// <summary>
            /// Gets or sets the Subscriber Domain
            /// </summary>
            /// <value>
            /// Subscriber Domain
            /// </value>
            [Column("SUBSCRIBER_DOMAIN")]
            [StringLength(20)]
            public string SubscriberDomain { get; set; }
        }
    
}
