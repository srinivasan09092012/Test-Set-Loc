//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace Watchdog.EnvironmentMonitor
{
    public class WatchdogContainer
    {
        public static UnityContainer IocContainer { get; set; }

        public static void InitializeConfiguration()
        {
            IocContainer = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            foreach (ContainerElement container in section.Containers)
            {
                IocContainer.LoadConfiguration(section, container.Name);
            }
        }
    }
}
