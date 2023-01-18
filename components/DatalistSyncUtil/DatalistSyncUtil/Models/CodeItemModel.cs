//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Config.DAOHelpers;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;

namespace DatalistSyncUtil
{
    [Serializable]
    public class CodeItemModel
    {
        public CodeItemModel()
        {
            this.LanguageList = new List<ItemLanguage>();
            this.DataListLink = new List<DataListItemLink>();
        }

        public Guid ID { get; set; }

        public string Code { get; set; }

        public string ContentID { get; set; }

        public Guid DatalistID { get; set; }

        public Guid TenantID { get; set; }

        public bool OrderIndexModified { get; set; }

        public int? OrderIndex { get; set; }

        public List<ItemLanguage> LanguageList { get; set; }

        public List<DataListItemLink> DataListLink { get; set; }

        public bool EffectiveStartDateModified { get; set; }

        public DateTime? EffectiveStartDate { get; set; }

        public bool EffectiveEndDateModified { get; set; }

        public DateTime? EffectiveEndDate { get; set; }
        
        public bool IsActive { get; set; }

        public bool IsEditableModified { get; set; }

        public bool IsEditable { get; set; }

        public string Status { get; set; }  

        public List<ItemDataListItemAttributeVal> Attributes { get; set; }
    }
}
