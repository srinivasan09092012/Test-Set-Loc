//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Helpers;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using HPE.HSP.UA3.Core.API.AuthManagement.Interface;
using HPE.HSP.UA3.Core.API.Logger;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading.Tasks;
using WarmUpProvider.Domain;

namespace WarmUpProvider.Helpers
{
    public class WarmUpHelper
    {
        private Guid tenantId;
        private IAuthClaimsProvider authProvider = null;
        private List<TenantWarmUpModel> retryEnpointLists = new List<TenantWarmUpModel>();

        public void StartUp()
        {
            this.tenantId = Guid.Parse(ConfigurationManager.AppSettings["TenantId"]);
            IUnityContainer container = BASUnityContainer.Container;
            this.authProvider = container.Resolve<IAuthClaimsProvider>();
            this.WarmUpEndpoints();
        }

        public void WarmUpEndpoints()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            this.LoggerHelper("-----------Starting to Warm up Endpoints-----------");

            try
            {
                FileHelper fileHelper = new FileHelper();
                List<TenantWarmUpModel> tenantWarmUpModels = new List<TenantWarmUpModel>();

                tenantWarmUpModels = fileHelper.ReadEndpointDataFromJSON();
                List<Task> tasks = new List<Task>();

                foreach (var tenant in tenantWarmUpModels)
                {
                    foreach (var moduleEndpoint in tenant.Endpoints)
                    {
                        string absoluteURL = tenant.RootURL + moduleEndpoint.EndPoint;
                        tasks.Add(Task.Run(() =>
                        {
                            this.WarmUpServices(moduleEndpoint, absoluteURL, this.tenantId.ToString(), this.authProvider);
                        }));

                        if (tasks.Count == 15)
                        {
                            Task.WaitAll(tasks.ToArray());
                            tasks.Clear();
                        }
                    }
                }

                Task.WaitAll(tasks.ToArray());
                tasks.Clear();
            }
            catch (Exception ex)
            {
                this.LoggerHelper("Error warming up Provider Endpoints!", WarmUpEnums.LogType.Error, ex);
                throw ex;
            }

            sw.Stop();

            this.LoggerHelper("Total time took to warm up endpoints: " + sw.Elapsed.Hours + ":" + sw.Elapsed.Minutes + ":" + sw.Elapsed.Seconds);
            this.LoggerHelper("-----------Warming up Endpoints Completed-----------");
        }

        public void WarmUpServices(ModuleEndpointModel moduleEndpoint, string absoluteURL, string tenantId, IAuthClaimsProvider authClaimsProvider)
        {
            try
            {
                IServiceChannelFactory serviceChannelFactory = new ServiceChannelFactory(string.IsNullOrEmpty(moduleEndpoint.Binding) ? "BasicHttpBinding" : moduleEndpoint.Binding, absoluteURL, authClaimsProvider);

                serviceChannelFactory.Invoke<IServiceAvailability>(
                    proxy => proxy.IsServiceAvailable());

                this.LoggerHelper("Service Available. Module Name = " + moduleEndpoint.ModuleName + ", Application Name " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Information);

                if (moduleEndpoint.CheckHealthStatus)
                {
                    serviceChannelFactory.Invoke<IServiceAvailability>(
                                proxy => proxy.IsServiceHealthy(tenantId));

                    this.LoggerHelper("Service Health Check Successfull. Module Name = " + moduleEndpoint.ModuleName + ", Application Name " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Information);
                }
            }
            catch (ActionNotSupportedException ex)
            {
                this.LoggerHelper("Service warm up failed. Module Name = " + moduleEndpoint.ModuleName + ", Application Name " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Warning, ex);
            }
            catch (Exception ex)
            {
                this.LoggerHelper("Service warm up failed. Module Name = " + moduleEndpoint.ModuleName + ", Application Name " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Warning, ex);
            }
        }

        private void LoggerHelper(string message, WarmUpEnums.LogType logType = WarmUpEnums.LogType.Information, Exception ex = null)
        {
            switch (logType)
            {
                case WarmUpEnums.LogType.Information:
                    LoggerManager.Logger.LogInformational(message);
                    break;

                case WarmUpEnums.LogType.Warning:
                    if (ex == null)
                    {
                        LoggerManager.Logger.LogWarning(message);
                    }
                    else
                    {
                        LoggerManager.Logger.LogWarning(message, ex);
                    }
                    break;

                case WarmUpEnums.LogType.Error:
                    if (ex == null)
                    {
                        LoggerManager.Logger.LogError(message);
                    }
                    else
                    {
                        LoggerManager.Logger.LogError(message, ex);
                    }
                    break;

                default:
                    LoggerManager.Logger.LogInformational(message);
                    break;
            }
        }
    }
}