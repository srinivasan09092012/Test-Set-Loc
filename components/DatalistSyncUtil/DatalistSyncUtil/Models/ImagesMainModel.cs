//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace DatalistSyncUtil
{
    [Serializable]
    public class ImagesMainModel
    {
        public ImagesMainModel()
        {
            this.ImageLanguages = new List<ImageLanguage>();
        }

        public Guid ImageId { get; set; }

        public Guid TenantModuleId { get; set; }

        public string ContentId { get; set; }

        public string Description { get; set; }

        public bool DescriptionModified { get; set; }

        public bool IsActive { get; set; }

        public bool ActiveModified { get; set; }

        public string OperatorId { get; set; }

        public DateTime LastModifiedTS { get; set; }

        public Guid TenantID { get; set; }

        public bool IsEditable { get; set; }

        public string ReleaseStatus { get; set; }

        public List<ImageLanguage> ImageLanguages { get; set; }

        public string Status { get; set; }
    }
}
