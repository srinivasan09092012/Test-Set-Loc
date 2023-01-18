//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace DatalistSyncUtil
{
    public class SelectedItem
    {
        public string ContentID { get; set; }

        public string Code { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }

        public bool Selected { get; set; }

        public Guid ID { get; set; }
    }
}
