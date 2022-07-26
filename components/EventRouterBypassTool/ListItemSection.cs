//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using EventRouterByPassTool.Common;
using System.Configuration;

namespace EventRouterByPassTool
{
    public class ListItemSection : ConfigurationSection
    {
        #region Properties
        [ConfigurationProperty(EventRouterBypassConstants.DataListConstants.Values, IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ListItemCollection),
            AddItemName = EventRouterBypassConstants.DataListConstants.Add,
            ClearItemsName = EventRouterBypassConstants.DataListConstants.Clear,
            RemoveItemName = EventRouterBypassConstants.DataListConstants.Remove)]

        public ListItemCollection Values
        {
            get
            {
                return (ListItemCollection)base[EventRouterBypassConstants.DataListConstants.Values];
            }
        }
        #endregion
    }
}
