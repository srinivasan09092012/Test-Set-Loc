//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalistSyncUtil
{
    [Serializable]
    public class ImageLanguage
    {
        public Guid ImageId { get; set; }

        public string ContentId { get; set; }

        public string LocaleId { get; set; }

        public string Source { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        public string ToolTip { get; set; }

        public string OperatorId { get; set; }

        public bool SourceModified { get; set; }

        public bool HeightModified { get; set; }

        public bool WidthModified { get; set; }

        public bool ToolTipModified { get; set; }

        public bool IsActive { get; set; }

        public bool IsActiveModified { get; set; }

        public DateTime LastModifiedTS { get; set; }

        public string Status { get; set; }
    }
}
