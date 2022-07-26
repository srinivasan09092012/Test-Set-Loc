//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Configuration;

namespace EventRouterByPassTool
{
    public class ListItemCollection : ConfigurationElementCollection
    {
        #region Constructor
        public ListItemCollection()
        {
            ListItemConfigElement value = (ListItemConfigElement)CreateNewElement();
            Add(value);
        }
        #endregion

        #region Properties
        new public ListItemConfigElement this[string Name]
        {
            get
            {
                return (ListItemConfigElement)BaseGet(Name);
            }
        }
        #endregion

        #region Public Methods
        public ListItemConfigElement this[int index]
        {
            get
            {
                return (ListItemConfigElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public int IndexOf(ListItemConfigElement value)
        {
            return BaseIndexOf(value);
        }

        public void Add(ListItemConfigElement value)
        {
            BaseAdd(value);
        }

        public void Remove(ListItemConfigElement value)
        {
            if (BaseIndexOf(value) >= 0)
                BaseRemove(value.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
        #endregion

        #region Overriden Methods
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ListItemConfigElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((ListItemConfigElement)element).Name;
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }
        #endregion
    }
}
