//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace RightsModels
{
    public class DataListLinkModel
    {
        public string ParentListId { get; set; }
        public string ChildListId { get; set; }
        public string ParentListItemId { get; set; }
        public string ChildListItemId { get; set; }
    }
}
