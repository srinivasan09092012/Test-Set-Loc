//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using RightsModels;

namespace RightsExcel
{
    public class ReportItemBase
    {
        public ReportItemBase(DataListItemModel item)
        {
            this.Id = item.DataListItemId;
            this.Name = item.ItemKey;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
