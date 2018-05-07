//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace DatalistSyncUtil
{
    [Serializable]
    public class HtmlBlockLanguage
    {
        public Guid HtmlBlockId { get; set; }

        public string ContentId { get; set; }

        public string LocaleId { get; set; }

        public string Html { get; set; }

        public bool IsActive { get; set; }

        public string OperatorId { get; set; }

        public bool HtmlModified { get; set; }

        public bool IsActiveModified { get; set; }

        public DateTime LastModifiedTS { get; set; }

        public string Status { get; set; }
    }
}
