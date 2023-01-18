//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRouterByPassTool.Entities.PublishedEvents
{
    /// <summary>
    /// This is the domain class identifying the REQ object.
    /// <uol>
    /// <li><b>References:</b></li>
    /// <li><b>Referenced By:</b> None</li>
    /// </uol>
    /// </summary>
    public abstract class PublishedEvent
    {
        #region Properties
        /// <summary>
        /// The COMMITID for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Key]
        [Column("COMMITID")]
        public Guid CommitId { get; set; }

        /// <summary>
        /// The EVENT_NAMESPACE for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("EVENT_NAMESPACE")]
        public string EventNamespace { get; set; }

        /// <summary>
        /// The EVENT_TYPENAME for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("EVENT_TYPENAME")]
        public string EventTypeName { get; set; }

        /// <summary>
        /// The MODULE_NAME for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("MODULE_NAME")]
        public string ModuleName { get; set; }

        /// <summary>
        /// The EVENT_PAYLOAD for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> byte</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("EVENT_PAYLOAD")]
        public byte[] EventPayload { get; set; }

        /// <summary>
        /// The AGGREGATE_ROOT_TYPE for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("AGGREGATE_ROOT_TYPE")]
        public string AggregateRootType { get; set; }

        /// <summary>
        /// The AGGREGATE_ROOT_ID for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("AGGREGATE_ROOT_ID")]
        public string AggregateRootId { get; set; }

        /// <summary>
        /// The VERSION for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("VERSION")]
        public string Version { get; set; }

        /// <summary>
        /// The CREATED_TS for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> DateTime</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("CREATED_TS")]
        public DateTime? CreatedTS { get; set; }

        /// <summary>
        /// The LAST_MODIFIED_TS for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> DateTime</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("LAST_MODIFIED_TS")]
        public DateTime? LastModifiedTS { get; set; }

        /// <summary>
        /// The SUBSCRIPTION_STATUS for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> byte</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("SUBSCRIPTION_STATUS")]
        public byte SubscriptionStatus { get; set; }

        /// <summary>
        /// The GROUP_ID for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("GROUP_ID")]
        public Guid GroupId { get; set; }

        /// <summary>
        /// The EVENT_FILTER_METADATA for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("EVENT_FILTER_METADATA")]
        public string EventFilterMetadata { get; set; }

        /// <summary>
        /// The IS_LAST_FROM_GROUP for the REQ Segment
        /// <uol>
        /// <li><b>Data Type:</b> short</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("IS_LAST_FROM_GROUP")]
        public bool? IsLastFromGroup { get; set; }
        #endregion
    }
}
