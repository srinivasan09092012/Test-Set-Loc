//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using RightsModels;
using System.Collections.Generic;

namespace RightsExcel
{
    public class ReportRight : ReportItemBase
    {
        public ReportRight(DataListItemModel item) : base(item)
        {
            this.RoleIds = new List<string>();
            this.FunctionIds = new List<string>();
        }

        public List<string> FunctionIds { get; set; }

        public List<string> RoleIds { get; set; }
    }
}
