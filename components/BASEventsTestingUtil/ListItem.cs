using System;
using System.Configuration;

namespace BASEventsTestingUtil
{
    public class ListItem
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class ListItemSection : ConfigurationSection
    {
        [ConfigurationProperty("values", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ListItemCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ListItemCollection Values
        {
            get
            {
                return (ListItemCollection)base["values"];
            }
        }
    }

    public class ListItemCollection : ConfigurationElementCollection
    {
        public ListItemCollection()
        {
            ListItemConfigElement value = (ListItemConfigElement)CreateNewElement();
            Add(value);
        }

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

        new public ListItemConfigElement this[string Name]
        {
            get
            {
                return (ListItemConfigElement)BaseGet(Name);
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
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
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
    }

    public class ListItemConfigElement : ConfigurationElement
    {
        public ListItemConfigElement(String name, String value)
        {
            this.Name = name;
            this.Value = value;
        }

        public ListItemConfigElement()
        {
        }

        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("value", DefaultValue = "", IsRequired = true)]
        public string Value
        {
            get
            {
                return (string)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }
    }
}
