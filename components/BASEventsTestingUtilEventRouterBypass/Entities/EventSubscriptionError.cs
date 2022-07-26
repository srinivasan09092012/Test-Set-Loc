//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRouterByPassTool.Entities
{
    /// <summary>
    /// This is the domain class identifying the EVENT_SUBSCRIPTIONS_ERR object.
    /// <uol>
    /// <li><b>Table:</b> EVENT_SUBSCRIPTIONS_ERR </li>
    /// <li><b>References:</b></li>
    /// <li><b>Referenced By:</b> None</li>
    /// </uol>
    /// </summary>
    [Table("EVENT_SUBSCRIPTIONS_ERR")]
    public class EventSubscriptionError
    {
        /// <summary>
        /// The ERR_QUEUE_ID(PK) for the EVENT_SUBSCRIPTIONS_ERR Segment
        /// <uol>
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Key, Column("ERR_QUEUE_ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// The EVENT_NAMESPACE for the EVENT_SUBSCRIPTIONS_ERR Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Max Length:</b> 2000</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("EVENT_NAMESPACE")]
        public string EventNamespace { get; set; }

        /// <summary>
        /// The EVENT_TYPENAME for the EVENT_SUBSCRIPTIONS_ERR Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Max Length:</b> 2000</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("EVENT_TYPENAME")]
        public string EventTypeName { get; set; }

        /// <summary>
        /// The EVENT_PAYLOAD for the EVENT_SUBSCRIPTIONS_ERR Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Max Length:</b> 2000</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("EVENT_PAYLOAD")]
        public byte[] EventPayload { get; set; }

        /// <summary>
        ///  The commit id of the published event.
        /// <uol>
        /// <li><b>Column:</b>COMMITID</li> 
        /// <li><b>Data Type:</b>Guid</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("COMMITID")]
        public Guid CommitID { get; set; }

        /// <summary>
        ///  The aggregate root type of an event.
        /// <uol>
        /// <li><b>Column:</b>AGGREGATE_ROOT_TYPE</li> 
        /// <li><b>Data Type:</b>string</li> 
        /// <li><b>Required:</b>No</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("AGGREGATE_ROOT_TYPE")]
        public string AggregateRootType { get; set; }

        /// <summary>
        ///  The aggregate root id of an event.
        /// <uol>
        /// <li><b>Column:</b>AGGREGATE_ROOT_ID</li> 
        /// <li><b>Data Type:</b>string</li> 
        /// <li><b>Required:</b>No</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("AGGREGATE_ROOT_ID")]
        public string AggregateRootID { get; set; }

        /// <summary>
        ///  Version of the event.
        /// <uol>
        /// <li><b>Column:</b>VERSION</li> 
        /// <li><b>Data Type:</b>string</li> 
        /// <li><b>Required:</b>No</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("VERSION")]
        public string Version { get; set; }

        /// <summary>
        ///  The time event created
        /// <uol>
        /// <li><b>Column:</b>EVENT_CREATED_TS</li> 
        /// <li><b>Data Type:</b>DateTime</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("EVENT_CREATED_TS")]
        public DateTime EventCreated { get; set; }

        /// <summary>
        ///  The module name of the publishing module
        /// <uol>
        /// <li><b>Column:</b>MODULE_NAME</li> 
        /// <li><b>Data Type:</b>string</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        /// 
        [Column("MODULE_NAME")]
        public string ModuleName { get; set; }

        /// <summary>
        ///  The module name of the subscribing module
        /// <uol>
        /// <li><b>Column:</b>SUBSCRIBER_NAME</li> 
        /// <li><b>Data Type:</b>string</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        /// 
        [Column("SUBSCRIBER_NAME")]
        public string SubscriberName { get; set; }

        /// <summary>
        ///  The error message captured when error occured while subscription.
        /// <uol>
        /// <li><b>Column:</b>ERROR_MESSAGE</li> 
        /// <li><b>Data Type:</b>string</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("ERROR_MESSAGE")]
        [StringLength(4000)]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///  The number of times subscription attempted
        /// <uol>
        /// <li><b>Column:</b>RETRY_COUNT</li> 
        /// <li><b>Data Type:</b>int</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("RETRY_COUNT")]
        public int RetryCount { get; set; }

        /// <summary>
        ///  The dead message flag
        /// <uol>
        /// <li><b>Column:</b>bool</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("MESSAGE_DEAD_FLAG")]
        public bool DeadFlag { get; set; }

        /// <summary>
        ///  The system error flag
        /// <uol>
        /// <li><b>Column:</b>string</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("ERROR_CODE")]
        public string ErrorCode { get; set; }

        /// <summary>
        ///  The success flag
        /// <uol>
        /// <li><b>Column:</b>bool</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("SUCCESS_FLAG")]
        public bool SuccessFlag { get; set; }

        /// <summary>
        ///  The time event was modified
        /// <uol>
        /// <li><b>Column:</b>LAST_MODIFIED_TS</li> 
        /// <li><b>Data Type:</b>DateTime</li> 
        /// <li><b>Required:</b>No</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("LAST_MODIFIED_TS")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        ///  The flag that overrides the retry irrespective of all other factors. If the column is set to true the 
        ///  event will be retried even if its marked dead or have been retried more than a set number of times.
        /// <uol>
        /// <li><b>Column:</b>IS_RETRY_OVERRIDE</li> 
        /// <li><b>Data Type:</b>boolean</li> 
        /// <li><b>Required:</b>Yes</li> 
        /// <li><b>Unique Index:</b>None</li> 
        /// </uol>
        /// </summary>
        [Column("IS_RETRY_OVERRIDE")]
        public bool IsRetryOverride { get; set; }

        /// <summary>
        /// The EVENT_SEQUENCE_ID of the errored event
        /// <uol>
        /// <li><b>Column:</b>EVENT_SEQUENCE_ID</li> 
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("EVENT_SEQUENCE_ID")]
        [MaxLength(200)]
        public string EventSequenceId { get; set; }

        /// <summary>
        /// The GROUP_ID of the errored event
        /// <uol>
        /// <li><b>Column:</b>GROUP_ID</li> 
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("GROUP_ID")]
        public Guid? GroupId { get; set; }
    }
}
