//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using EventRouterByPassTool.Common;
using System;
using System.Configuration;

namespace EventRouterByPassTool
{
    public class ListItemConfigElement : ConfigurationElement
    {
        #region Constructor
        public ListItemConfigElement(String name, String value)
        {
            this.Name = name;
            this.Value = value;
        }

        public ListItemConfigElement()
        {
        }
        #endregion

        #region Properties
        [ConfigurationProperty(EventRouterBypassConstants.DataListConstants.Name, DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this[EventRouterBypassConstants.DataListConstants.Name];
            }
            set
            {
                this[EventRouterBypassConstants.DataListConstants.Name] = value;
            }
        }

        [ConfigurationProperty(EventRouterBypassConstants.DataListConstants.Value, DefaultValue = "", IsRequired = true)]
        public string Value
        {
            get
            {
                return (string)this[EventRouterBypassConstants.DataListConstants.Value];
            }
            set
            {
                this[EventRouterBypassConstants.DataListConstants.Value] = value;
            }
        }
        #endregion
    }
}
