using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace UserAccountManager.Domain
{
    [Serializable]
    public class EnvironmentsConfig
    {
        public EnvironmentsConfig()
        {
            this.Initialize();
        }

        public List<Environment> Environments { get; set; }

        public void Initialize()
        {
            this.Environments = new List<Environment>();
        }

        public void Validate()
        {
            if (this.Environments == null || this.Environments.Count == 0)
            {
                throw new ArgumentNullException("No environment configurations found.");
            }

            foreach (Environment env in this.Environments)
            {
                env.Validate();
            }
        }
    }
}
