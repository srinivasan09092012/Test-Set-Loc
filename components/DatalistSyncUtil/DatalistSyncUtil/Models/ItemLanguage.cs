//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace DatalistSyncUtil
{
    [Serializable]
    public class ItemLanguage
    {
        public Guid ItemID { get; set; }

        public string ContentID { get; set; }

        public string Code { get; set; }

        public string LocaleID { get; set; }

        public string Description { get; set; }

        public bool DescriptionModified { get; set; }

        public string LongDescription { get; set; }

        public bool LongDescriptionModified { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }
    }
}
