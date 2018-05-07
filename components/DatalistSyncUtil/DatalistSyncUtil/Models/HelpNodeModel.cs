//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DatalistSyncUtil.Domain
{
    [Serializable]
    public class HelpNodeModel
    {
        public HelpNodeModel()
        {
            this.HelpContentLanguages = new List<HelpContentLanguageModel>();
        }

        public Guid HelpNodeId { get; set; }

        public string HelpNodeNM { get; set; }

        public string HelpNodeTypeCD { get; set; }

        public int NodeDepth { get; set; }

        public Guid TenantModuleId { get; set; }

        public Guid TenantId { get; set; }

        public Guid? HelpMitaId { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastModifiedTS { get; set; }

        public Guid ParentId { get; set; }

        public string ParentHelpNodeName { get; set; }

        public string ParentHelpNodeTypeCD { get; set; }

        public string OperatorId { get; set; }

        public string HowToHelpNodeNM { get; set; }

        public string Status { get; set; }

        public List<HelpContentLanguageModel> HelpContentLanguages { get; set; }
    }
}
