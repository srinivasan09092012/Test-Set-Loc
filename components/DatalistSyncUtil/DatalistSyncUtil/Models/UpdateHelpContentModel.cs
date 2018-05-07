//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
////using HP.HSP.UA3.Administration.BAS.BroadcastMessage.Contracts.Constants;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace DatalistSyncUtil.Domain
{
    //// <summary>
    //// <para>AddHelpContent Model</para>
    //// <para>Created when a new HelpContent  is added.</para>
    //// </summary>   
    [Serializable]
    [DataContract]
    public class UpdateHelpContentModel
    {
        public UpdateHelpContentModel()
        {
            this.HelpContentLanguages = new List<HelpContentLanguageModel>();
        }

        [DataMember]
        public string TenantModuleId { get; set; }

        [DataMember]
        public string TenantId { get; set; }

        [DataMember]
        public string HelpType { get; set; }

        [DataMember]
        public string ParentHelpNodeId { get; set; }

        [DataMember]
        public string FullHelpNodeName { get; set; }

        [DataMember]
        public List<HelpContentLanguageModel> HelpContentLanguages { get; set; }

        [DataMember]
        public string OperatorId { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public List<string> Languages { get; set; }

        [DataMember]
        public string HelpNodeId { get; set; }
    }
}
