using HPE.HSP.UA3.Core.API.AuthManagement.Providers;
using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using Microsoft.Web.Administration;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Web.Services.Discovery;
using System.Xml;
using System.Xml.Schema;
using Watchdog.Domain;

namespace Watchdog.Monitor
{
    public class BASMonitor : HealthMonitorBase
    {
        private BASConfigDataItem serviceConfigData = null;
        private string iisServerName = string.Empty;
        private string endpointAddress = string.Empty;        
        private string tenantId = string.Empty;
        private string userName = ConfigurationManager.AppSettings["ADUserName"];
        private string password = ConfigurationManager.AppSettings["ADPassword"];
        private string domain = ConfigurationManager.AppSettings["ADDomain"];
        private string adendpoint = ConfigurationManager.AppSettings["ADEndPoint"];
        private string relayidentifier = ConfigurationManager.AppSettings["RelayIdentity"];

        public BASMonitor(BASConfigDataItem serviceData, string iisServerName, string siteName, string endpointAddress, string tenantId, ILogger logger)
            : base(serviceData, logger)
        {
            this.serviceConfigData = serviceData;
            this.iisServerName = iisServerName;
            this.endpointAddress = endpointAddress;
            this.tenantId = tenantId;            
            this.serviceConfigData.SiteName = siteName;
            this.applicationHealthInformation.ServiceType = "BAS - " + serviceConfigData.ApplicationPoolName;
            this.applicationHealthInformation.ApplicationPool = serviceConfigData.ApplicationPoolName;
            this.applicationHealthInformation.Endpoint = this.endpointAddress;
            this.applicationHealthInformation.ServiceName = serviceConfigData.Name;
            this.applicationHealthInformation.ApplicationTenantId = this.tenantId;
        }

        public override bool CheckServiceAvailabilityAsync()
        {
            bool isServiceHealthy = false;

            bool isApplicationPoolActiveOrNot = false;

            bool isServiceActiveOrNot = false;

            isServiceActiveOrNot = this.RestartServiceForSite(isServiceActiveOrNot);

            isApplicationPoolActiveOrNot = this.RestartServiceForApplicationPool(isApplicationPoolActiveOrNot);

            if (isServiceActiveOrNot && isApplicationPoolActiveOrNot)
            {
                if (string.Compare(serviceConfigData.Type, "wcf", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    isServiceHealthy = IsWcfServiceAvailable();
                }
                else
                {
                    isServiceHealthy = IsOdataServiceAvailable();
                }
                isServiceHealthy = true;
            }
            
            return isServiceHealthy;
        }

        private bool RestartServiceForSite(bool isServiceActiveOrNot)
        {
            if (!string.IsNullOrEmpty(this.iisServerName) && !string.IsNullOrEmpty(serviceConfigData.Name))
            {
                try
                {
                    using (ServerManager manager = ServerManager.OpenRemote(this.iisServerName))
                    {
                        Site siteName = manager.Sites.FirstOrDefault(s => s.Name.Equals(serviceConfigData.SiteName));
                        
                        if (siteName != null)
                        {
                            bool siteNameStopped = siteName.State == ObjectState.Stopped || siteName.State == ObjectState.Stopping;
                            this.StartSiteNameService(siteName, siteNameStopped);
                            isServiceActiveOrNot = true;
                        }
                        else
                        {
                            this.applicationHealthInformation.RestartStatus = "Failed";
                            throw new Exception("Site Name Pool doesn't exist : " + serviceConfigData.Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = "Failed";
                    logger.LogError("Error occured while attempting to restart the application pool : " + serviceConfigData.ApplicationPoolName, ex);
                    throw ex;
                }
            }

            return isServiceActiveOrNot;
        }
    

        protected override void RestartService()
        {
            if (!string.IsNullOrEmpty(this.iisServerName) && !string.IsNullOrEmpty(serviceConfigData.ApplicationPoolName))
            {
                try
                {
                    using (ServerManager manager = ServerManager.OpenRemote(this.iisServerName))
                    {
                        ApplicationPool appPool = manager.ApplicationPools.FirstOrDefault(ap => ap.Name == serviceConfigData.ApplicationPoolName);
                        
                        if (appPool != null)
                        {
                            this.applicationHealthInformation.ApplicationPool = appPool.Name;
                            logger.LogInformational("Application pool status : " + appPool.State.ToString());
                            //Get the current state of the app pool
                            bool appPoolRunning = appPool.State == ObjectState.Started || appPool.State == ObjectState.Starting;
                            bool appPoolStopped = appPool.State == ObjectState.Stopped || appPool.State == ObjectState.Stopping;

                            //appPoolStopped = this.StopApplicationPool(appPool, appPoolRunning);

                            this.StartApplicationPool(appPool, appPoolStopped);
                        }
                        else
                        {
                            this.applicationHealthInformation.RestartStatus = "Failed";
                            throw new Exception("Application Pool doesn't exist : " + serviceConfigData.ApplicationPoolName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = "Failed";
                    logger.LogError("Error occured while attempting to restart the application pool : " + serviceConfigData.ApplicationPoolName, ex);
                    throw ex;
                }
            }
        }

        private bool RestartServiceForApplicationPool(bool isApplicationPoolActiveOrNot)
        {
            bool isApplicationPoolSuccessfullyStarted = false;
            if (!string.IsNullOrEmpty(this.iisServerName) && !string.IsNullOrEmpty(serviceConfigData.ApplicationPoolName))
            {
                try
                {
                    using (ServerManager manager = ServerManager.OpenRemote(this.iisServerName))
                    {
                        ApplicationPool appPool = manager.ApplicationPools.FirstOrDefault(ap => ap.Name == serviceConfigData.ApplicationPoolName);

                        if (appPool != null)
                        {
                            bool appPoolStopped = appPool.State == ObjectState.Stopped || appPool.State == ObjectState.Stopping;
                            if(appPoolStopped)
                            isApplicationPoolActiveOrNot = this.StartingApplicationPool(appPool, appPoolStopped, isApplicationPoolSuccessfullyStarted);
                            else
                            isApplicationPoolActiveOrNot = true;
                        }
                        else
                        {
                            this.applicationHealthInformation.RestartStatus = "Failed";
                            throw new Exception("Application Pool doesn't exist : " + serviceConfigData.ApplicationPoolName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = "Failed";
                    logger.LogError("Error occured while attempting to restart the application pool : " + serviceConfigData.ApplicationPoolName, ex);
                    throw ex;
                }
            }

            return isApplicationPoolActiveOrNot;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Process GetProcessIdForService()
        {
            Process process = null;
            int processId = 0;
            string commandLine = String.Empty;
            ManagementClass MgmtClass = new ManagementClass("Win32_Process");
            foreach (ManagementObject mo in MgmtClass.GetInstances())
            {
                if (mo["Name"].ToString() == "w3wp.exe")
                {                    
                    processId = Convert.ToInt32(mo["ProcessId"]);
                    commandLine = mo["CommandLine"].ToString();
                    if (commandLine.ToLower().Contains(serviceConfigData.ApplicationPoolName.ToLower()))
                    {
                        process = Process.GetProcessById(processId);
                        break;
                    }
                }
            }
            
             return process;
        }

        protected override void BuildServiceHealthInformation(Tuple<double, double, float, double> cpuMemData)
        {
            this.applicationHealthInformation.CPUUsagePercent = cpuMemData.Item1;
            this.applicationHealthInformation.MemoryUsagePercent = cpuMemData.Item2;
            this.applicationHealthInformation.processMemInKB = cpuMemData.Item3;
            this.applicationHealthInformation.processMemInGB = cpuMemData.Item4;
            this.applicationHealthInformation.Status = "Running";
        }

        private bool IsWcfServiceAvailable()
        {
            bool isServiceAvailable = false;

            var operationName = "IsServiceAvailable";

            ServiceContractGenerator generator = new ServiceContractGenerator();            

            var endpoints = this.GetEndpointsForContracts(ref generator);

            Assembly compiledAssembly = this.GetCompiledAssemblyFromServiceContracts(generator);

            if (compiledAssembly != null && compiledAssembly.GetTypes().ToList().Count > 0)
            {
                var serviceContracts = compiledAssembly.GetTypes().ToList();

                ServiceEndpoint serviceEndpoint = endpoints.First().Value.First();

                object servRequest = compiledAssembly.CreateInstance("IsServiceAvailableRequest", false, System.Reflection.BindingFlags.CreateInstance, null,
                    null, CultureInfo.CurrentCulture, null);

                Type targetType = serviceContracts.First(t => t.IsInterface && t.Name == serviceEndpoint.Contract.Name);
                ChannelFactory factory = null;
                object proxy = null;
                try
                {
                    factory = Activator.CreateInstance(typeof(ChannelFactory<>).MakeGenericType(targetType), serviceEndpoint.Binding, serviceEndpoint.Address) as ChannelFactory;
                    
                    if (serviceEndpoint.Binding.Name.ToLower().Contains("http"))
                    {
                        MethodInfo createFactory = factory.GetType().GetMethod("CreateChannel", new Type[] { });
                        proxy = createFactory.Invoke(factory, new object[] { });
                    }
                    else if (serviceEndpoint.Binding.Name.ToLower().Contains("federation"))
                    {
                        
                        ADFSClaimsProvider provider = new ADFSClaimsProvider(userName, password, domain,adendpoint, relayidentifier);
                        SecurityToken claims = provider.GenerateClaimsToken();
                        MethodInfo createFactory = factory.GetType().GetMethod("CreateChannelWithIssuedToken", new Type[] { typeof(SecurityToken) });
                        proxy = createFactory.Invoke(factory, new object[] { claims });
                    }

                    object val = proxy.GetType().GetMethod(operationName).Invoke(proxy, new object[] { servRequest });

                    isServiceAvailable = (bool)val.GetType().GetField("IsServiceAvailableResult").GetValue(val);

                    ((IClientChannel)proxy).Close();
                    factory.Close();
                }
                catch(Exception ex)
                {
                    logger.LogError("Error occured during service method invocation : " + serviceConfigData.Name, ex);

                    IClientChannel clientInstance = (IClientChannel)proxy;
                    if (clientInstance.State == CommunicationState.Faulted)
                    {
                        clientInstance.Abort();
                        factory.Abort();
                    }
                    else if (clientInstance.State != CommunicationState.Closed)
                    {
                        clientInstance.Close();
                        factory.Close();
                    }
                }
            }
            return isServiceAvailable;
        } 

        private bool IsOdataServiceAvailable()
        {
            bool isServiceAvailable = false;

            HttpWebResponse response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(this.endpointAddress);
                request.Method = "GET";

                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    isServiceAvailable = true;
                }
            }
            catch (WebException ex)
            {
                logger.LogError("Error occured during monitoring of odata servive : " + serviceConfigData.Name, ex);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return isServiceAvailable;
        }        

        private void StartApplicationPool(ApplicationPool appPool, bool isAppPoolStopped)
        {
            if (isAppPoolStopped)
            {
                try
                {
                    appPool.Start();
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = "Failed";
                    logger.LogError("Error occured while recycling application pool", ex);
                }

                if (appPool.State == ObjectState.Started)
                {
                    this.applicationHealthInformation.RestartStatus = "Restarted";                    
                    logger.LogInformational("Application pool: " + serviceConfigData.ApplicationPoolName + " has been recycled successfully");
                }                
            }            
        }

        private bool StartingApplicationPool(ApplicationPool appPool, bool isAppPoolStopped, bool isApplicationPoolSuccessfullyStarted)
        {
            if (isAppPoolStopped)
            {
                try
                {
                    appPool.Start();
                    Thread.Sleep(10000);
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = "Failed";
                    logger.LogError("Error occured while recycling application pool", ex);
                }

                if (appPool.State == ObjectState.Started)
                {
                    this.applicationHealthInformation.RestartStatus = "Restarted";
                    isApplicationPoolSuccessfullyStarted = true;
                    logger.LogInformational("Application pool: " + serviceConfigData.ApplicationPoolName + " has been recycled successfully");
                }
            }
            return isApplicationPoolSuccessfullyStarted;
        }

        private void StartSiteNameService(Site siteName, bool siteNameStopped)
        {
            if (siteNameStopped)
            {
                try
                {
                    siteName.Start();
                    Thread.Sleep(10000);
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = "Failed";
                    logger.LogError("Error occured while recycling site service", ex);
                }

                if (siteName.State == ObjectState.Started)
                {
                    this.applicationHealthInformation.RestartStatus = "Restarted";

                    logger.LogInformational("Site Name: " + serviceConfigData.Name + " has been recycled successfully");
                }
            }
        }

        private MetadataSet GetMetaDataFromWsdl()
        {
            DiscoveryClientProtocol disco = new DiscoveryClientProtocol();
            disco.AllowAutoRedirect = true;
            disco.UseDefaultCredentials = true;
            disco.DiscoverAny(this.endpointAddress);
            disco.ResolveAll();

            Collection<MetadataSection> results = new Collection<MetadataSection>();

            foreach (object document in disco.Documents.Values)
            {
                AddDocumentToResults(document, results);
            }

            return new MetadataSet(results);
        }

        private Dictionary<string, IEnumerable<ServiceEndpoint>> GetEndpointsForContracts(ref ServiceContractGenerator scGenerator)
        {
            var endpointsForContracts = new Dictionary<string, IEnumerable<ServiceEndpoint>>();

            var metaSet = GetMetaDataFromWsdl();

            // Import all contracts and endpoints
            WsdlImporter importer = new WsdlImporter(metaSet);
            Collection<ContractDescription> contracts = importer.ImportAllContracts();
            ServiceEndpointCollection allEndpoints = importer.ImportAllEndpoints();

            // Generate type information for each contract
            foreach (ContractDescription contract in contracts)
            {
                scGenerator.GenerateServiceContractType(contract);
                endpointsForContracts[contract.Name] = allEndpoints.Where(x => x.Contract.Name == contract.Name).ToList();
            }

            IEnumerable<MetadataConversionError> codegenWarnings = scGenerator.Errors;
            if (codegenWarnings != null)
            {
                foreach (MetadataConversionError error in codegenWarnings)
                {
                    if (!error.IsWarning)
                    {
                        throw new Exception("Unable to generate service contracts.");
                    }
                }
            }

            return endpointsForContracts;
        }

        private Assembly GetCompiledAssemblyFromServiceContracts(ServiceContractGenerator scGenerator)
        {
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");
            CompilerParameters compilerParameters = new CompilerParameters(new string[] { "System.dll", "System.ServiceModel.dll", "System.Runtime.Serialization.dll" });
            compilerParameters.GenerateInMemory = true;

            CompilerResults results = codeDomProvider.CompileAssemblyFromDom(compilerParameters, scGenerator.TargetCompileUnit);

            if (results.Errors != null && results.Errors.HasErrors)
            {
                logger.LogError("Error occured during compiling assembly for contract in service : " + serviceConfigData.Name);

                foreach (CompilerError error in results.Errors)
                {
                    logger.LogError(error.ToString());
                }

                throw new Exception("There were errors during generated code compilation for service : " + serviceConfigData.Name);
            }

            return results.CompiledAssembly;
        }

        void AddDocumentToResults(object document, Collection<MetadataSection> results)
        {
            System.Web.Services.Description.ServiceDescription wsdl = document as System.Web.Services.Description.ServiceDescription;
            XmlSchema schema = document as XmlSchema;
            XmlElement xmlDoc = document as XmlElement;

            if (wsdl != null)
            {
                results.Add(MetadataSection.CreateFromServiceDescription(wsdl));
            }
            else if (schema != null)
            {
                results.Add(MetadataSection.CreateFromSchema(schema));
            }
            else if (xmlDoc != null && xmlDoc.LocalName == "Policy")
            {
                results.Add(MetadataSection.CreateFromPolicy(xmlDoc, null));
            }
            else
            {
                MetadataSection mexDoc = new MetadataSection();
                mexDoc.Metadata = document;
                results.Add(mexDoc);
            }
        }
    }
}
