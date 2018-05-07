//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
////using HP.HSP.UA3.Administration.BAS.BroadcastMessage.Contracts.Constants;
using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace DatalistSyncUtil.Domain
{
    [Serializable]
    public class HelpContentLanguageModel
    {
        public Guid HelpNodeId { get; set; }

        public string HelpNodeNM { get; set; }

        public string Language { get; set; }

        public string Title { get; set; }

        public string HtmlContent { get; set; }

        public string OperatorId { get; set; }

        public bool HelpModified { get; set; }

        public bool TitleModified { get; set; }

        public string Status { get; set; }
    }
}
