//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;

namespace WorkflowInstanceCleanupUtil.Lib.Models
{
    public class K2EnvironmentModel
    {
        public string Environment { get; set; }

        public string Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }

        public List<K2EnvironmentModel> GetEnvironmentConfiguration()
        {
            ICollection<K2EnvironmentModel> environments = new List<K2EnvironmentModel>();
            var config = WorkflowEnvironmentConfig.GetConfig();
            foreach (WorkflowEnvironment item in config.Environments)
            {
                K2EnvironmentModel workflowEnvironment = new K2EnvironmentModel();
                workflowEnvironment.Environment = item.Environment;
                workflowEnvironment.Server = item.Server;
                workflowEnvironment.UserName = item.Username;
                workflowEnvironment.Password = item.Password;
                workflowEnvironment.Port = item.Port;
                environments.Add(workflowEnvironment);
            }

            return environments.ToList();
        }
    }
}
