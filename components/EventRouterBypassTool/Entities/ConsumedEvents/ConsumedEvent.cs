//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRouterByPassTool.Entities.ConsumedEvents
{
    /// <summary>
    /// This is the domain class identifying the RES object.
    /// <uol>
    /// <li><b>References:</b></li>
    /// <li><b>Referenced By:</b> None</li>
    /// </uol>
    /// </summary>
    public abstract class ConsumedEvent
    {
        #region Properties
        /// <summary>
        /// The COMMITID for the RES Segment
        /// <uol>
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Key, Column("COMMITID", Order = 2)]
        public Guid CommitId { get; set; }

        /// <summary>
        /// The COMMITID for the RES Segment
        /// <uol>
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Key, Column("EVENT_TYPENAME", Order = 0)]
        public string EventTypeName { get; set; }

        /// <summary>
        /// The MODULE_NAME for the RES Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("MODULE_NAME")]
        public string ModuleName { get; set; }

        /// <summary>
        /// The SUBSCRIBER_NAME for the RES Segment
        /// <uol>
        /// <li><b>Data Type:</b> string</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Key, Column("SUBSCRIBER_NAME", Order = 1)]
        public string SubscriberName { get; set; }

        /// <summary>
        /// The CREATED_TS for the RES Segment
        /// <uol>
        /// <li><b>Data Type:</b> DateTime</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("CREATED_TS")]
        public DateTime CreatedTS { get; set; }

        /// <summary>
        /// The CREATED_TS for the RES Segment
        /// <uol>
        /// <li><b>Data Type:</b> DateTime</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("LAST_MODIFIED_TS")]
        public DateTime? LastModifiedTs { get; set; }

        /// <summary>
        /// The SUBSCRIPTION_STATUS for the RES Segment
        /// <uol>
        /// <li><b>Data Type:</b> byte</li>
        /// <li><b>Required:</b> No</li>
        /// </uol>
        /// </summary>
        [Column("SUBSCRIPTION_STATUS")]
        public byte SubscriptionStatus { get; set; }

        /// <summary>
        /// The GROUP ID for the RES Segment
        /// <uol>
        /// <li><b>Data Type:</b> Guid</li>
        /// <li><b>Required:</b> Yes</li>
        /// </uol>
        /// </summary>
        [Column("GROUP_ID")]
        public Guid GroupId { get; set; }
        #endregion
    }
}
