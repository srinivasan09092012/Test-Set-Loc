//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace UserAccountMigration.Domain
{
    [Serializable]
    public class MigrationConfig
    {
        public MigrationConfig()
        {
            this.Initialize();
        }

        public Environment Environment { get; set; }

        [XmlArrayItem("Process")]
        public List<ProcessConfig> Processes { get; set; }

        public void Initialize()
        {
            this.Environment = new Environment();
            this.Processes = new List<ProcessConfig>();
        }

        public void Validate()
        {
            if (this.Environment == null)
            {
                throw new ArgumentNullException("Environment");
            }
            else
            {
                this.Environment.Validate();
            }

            if (this.Processes == null || this.Processes.Count == 0)
            {
                throw new ArgumentNullException("Processes");
            }
            else
            {
                foreach (ProcessConfig process in this.Processes)
                {
                    process.Validate();
                }
            }
        }
    }
}
