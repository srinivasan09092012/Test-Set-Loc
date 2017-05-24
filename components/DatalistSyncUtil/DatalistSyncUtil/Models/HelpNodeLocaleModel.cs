//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DatalistSyncUtil.Domain
{
    [DataContract]
    public class HelpNodeLocaleModel
    {
        [Key]
        [DataMember]
        public Guid HelpNodeId { get; set; }

        [DataMember]
        public string LocaleId { get; set; }

        [DataMember]
        public string NodeTitle { get; set; }

        [DataMember]
        public string NodeSummary { get; set; }

        [DataMember]
        public string NodeTxt { get; set; }

        [DataMember]
        public string HelpNodeNM { get; set; }

        [DataMember]
        public string HelpNodeTypeCD { get; set; }

        [DataMember]
        public Guid TenantModuleId { get; set; }

        [DataMember]
        public Guid? TenantId { get; set; }

        [DataMember]
        public Guid? ModuleId { get; set; }

        [DataMember]
        public Guid? HelpMitaId { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime LastModifiedTS { get; set; }

        [DataMember]
        public Guid ParentId { get; set; }

        [DataMember]
        public string HowToHelpNodeNM { get; set; }

        [DataMember]
        public string MitaTitle { get; set; }

        [DataMember]
        public string MitaUrl { get; set; }

        [DataMember]
        public string Status { get; set; }
    }
}
