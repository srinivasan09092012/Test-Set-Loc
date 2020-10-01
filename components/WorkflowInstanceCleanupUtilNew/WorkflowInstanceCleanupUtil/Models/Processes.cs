//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Configuration;

namespace WorkflowInstanceCleanupUtil.Models
{
    public class Processes : ConfigurationElementCollection
    {
        public WorkflowProcess this[int index]
        {
            get
            {
                return BaseGet(index) as WorkflowProcess;
            }

            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }

        public new WorkflowProcess this[string responseString]
        {
            get
            {
                return (WorkflowProcess)this.BaseGet(responseString);
            }

            set
            {
                if (this.BaseGet(responseString) != null)
                {
                    this.BaseRemoveAt(this.BaseIndexOf(this.BaseGet(responseString)));
                }

                this.BaseAdd(value);
            }
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new WorkflowProcess();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((WorkflowProcess)element).ProcessName;
        }
    }
}
