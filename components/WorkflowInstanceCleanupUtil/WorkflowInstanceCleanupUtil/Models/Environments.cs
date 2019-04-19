//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Configuration;

namespace WorkflowInstanceCleanupUtil.Models
{
    public class Environments : ConfigurationElementCollection
    {
        public WorkflowEnvironment this[int index]
        {
            get
            {
                return this.BaseGet(index) as WorkflowEnvironment;
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

        public new WorkflowEnvironment this[string responseString]
        {
            get
            {
                return (WorkflowEnvironment)this.BaseGet(responseString);
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
            return new WorkflowEnvironment();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((WorkflowEnvironment)element).Environment;
        }
    }
}
