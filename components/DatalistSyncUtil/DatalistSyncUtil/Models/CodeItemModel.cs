﻿//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace DatalistSyncUtil
{
    [Serializable]
    public class CodeItemModel
    {
        public CodeItemModel()
        {
            this.LanguageList = new List<ItemLanguage>();
        }

        public Guid ID { get; set; }

        public string Code { get; set; }

        public string ContentID { get; set; }

        public Guid DatalistID { get; set; }

        public Guid TenantID { get; set; }

        public bool OrderIndexModified { get; set; }

        public int? OrderIndex { get; set; }

        public List<ItemLanguage> LanguageList { get; set; }

        public bool EffectiveStartDateModified { get; set; }

        public DateTime? EffectiveStartDate { get; set; }

        public bool EffectiveEndDateModified { get; set; }

        public DateTime? EffectiveEndDate { get; set; }
        
        public bool IsActive { get; set; }

        public bool IsEditableModified { get; set; }

        public bool IsEditable { get; set; }

        public string Status { get; set; }
    }
}
