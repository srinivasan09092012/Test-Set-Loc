//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using Newtonsoft.Json;
using System;

namespace SQLToTaskConverter
{
    /// <summary>
    /// <para>It is used to capture failed task details</para>
    /// </summary>
    public class TmTask
    {
        /// <summary>
        /// <para>Identifier for failed task </para>        
        /// </summary>
        [JsonProperty("$id")]
        public string Id { get; set; }

        /// <summary>
        /// <para>Identifier for Member Id</para>
        /// </summary>
        public string TaskMemberId { get; set; }

        /// <summary>
        /// <para>Identifier for Source module </para>        
        /// </summary>
        public string TaskSourceModuleCode { get; set; }

        /// <summary>
        /// <para>Identifier for Source task </para>        
        /// </summary>
        public string SourceTaskIdentifier { get; set; }

        /// <summary>
        /// <para>Identifier for Task type </para>        
        /// </summary>
        public string TaskTypeCode { get; set; }

        /// <summary>
        /// <para>Identifier for Task status </para>        
        /// </summary>
        public string TaskStatusCode { get; set; }

        /// <summary>
        /// <para>Identifier for Task provider </para>        
        /// </summary>
        public string TaskProviderTypeCode { get; set; }

        /// <summary>
        /// <para>Identifier for Task provider </para>        
        /// </summary>
        public string TaskProviderId { get; set; }

        /// <summary>
        /// <para>Identifier for Action Request </para>        
        /// </summary>
        public string ActionRequestedCode { get; set; }

        /// <summary>
        /// <para>Identifier for Exception type </para>        
        /// </summary>
        public string ExceptionErrorCode { get; set; }

        /// <summary>
        /// <para>Identifier for Assignment Userid </para>        
        /// </summary>
        public string AssignmentUserId { get; set; }

        /// <summary>
        /// <para>Identifier for Task priorityk </para>        
        /// </summary>
        public string TaskPriorityCode { get; set; }

        /// <summary>
        /// <para>Identifier for Severity </para>        
        /// </summary>
        public string SeverityCode { get; set; }

        /// <summary>
        /// <para>Capture date </para>        
        /// </summary>
        public DateTime? FollowupDate { get; set; }

        /// <summary>
        /// <para>Capture completedDate </para>        
        /// </summary>
        public DateTime? RequiredCompletedDate { get; set; }

        /// <summary>
        /// <para>Capture Task Resolution </para>        
        /// </summary>
        public string TaskResolutionReason { get; set; }

        /// <summary>
        /// <para>Capture Task details </para>        
        /// </summary>
        public string TaskComment { get; set; }

        /// <summary>
        /// <para>Capture Task Origin </para>        
        /// </summary>
        public string TaskOrigin { get; set; }

        /// <summary>
        /// <para>Identifier for Task Origin </para>        
        /// </summary>
        public string TaskOriginIdentifier { get; set; }

        /// <summary>
        /// <para>Linked to another failed task </para>        
        /// </summary>
        public LinkAssociations LinkAssociations { get; set; }

        /// <summary>
        /// <para>Identifier for Task comment type </para>        
        /// </summary>
        public string TaskCommentType { get; set; }

        /// <summary>
        /// <para>Identifier for Task requestor </para>        
        /// </summary>
        public string TaskRequestorUserId { get; set; }

        /// <summary>
        /// <para>Capture Date time </para>        
        /// </summary>
        public DateTime? ViewTimeStamp { get; set; }

        /// <summary>
        /// Identifier to enable task template
        /// </summary>
        public bool UseTaskTemplate { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is TmTask))
            {
                return false;
            }

            var other = obj as TmTask;
            if ((other.TaskComment.ToLower() != this.TaskComment.ToLower())
                && (other.TaskMemberId.ToLower() != this.TaskMemberId.ToLower()))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.TaskComment.GetHashCode() ^ this.TaskMemberId.GetHashCode();
        }
    }
}
