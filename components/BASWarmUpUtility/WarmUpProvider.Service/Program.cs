//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.ServiceProcess;

namespace WarmUpProvider.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WarmUpService()
            };

            //Setting this so the service can access EndpointInformation.json file
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            ServiceBase.Run(ServicesToRun);
        }
    }
}
