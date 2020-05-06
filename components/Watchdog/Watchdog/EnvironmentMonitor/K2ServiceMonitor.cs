//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------
using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Description;
using System.Web.Services.Discovery;
using System.Xml;
using System.Xml.Schema;
using Watchdog.Domain;

namespace Watchdog.EnvironmentMonitor
{
    public class K2ServiceMonitor : Monitor.HealthMonitorBase
    {
        private K2ServiceConfigDataItem serviceConfigData = null;
        private string iisServerName = string.Empty;
        private string endpointAddress = string.Empty;
        private string tenantId = string.Empty;

        public K2ServiceMonitor(K2ServiceConfigDataItem serviceData, string iisServerName, string siteName, string endpointAddress, string tenantId, ILogger logger)
            : base(serviceData, logger)
        {
            this.serviceConfigData = serviceData;
            this.iisServerName = iisServerName;
            this.endpointAddress = endpointAddress;
            this.tenantId = tenantId;
            this.serviceConfigData.SiteName = siteName;
            this.applicationHealthInformation.ServiceType = "K2 Service - " + serviceConfigData.ApplicationPoolName;
            this.applicationHealthInformation.ApplicationPool = serviceConfigData.ApplicationPoolName;
            this.applicationHealthInformation.Endpoint = this.endpointAddress;
            this.applicationHealthInformation.ServiceName = serviceConfigData.Name;
            this.applicationHealthInformation.ApplicationTenantId = this.tenantId;
        }

        public override bool CheckServiceAvailabilityAsync()
        {
            bool isServiceAvailable = false;
            try
            {
                var endpoints = this.GetEndpointsForContracts();
                if (endpoints != null)
                {
                    isServiceAvailable = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("K2 Service - Unavailable to download contracts: " + serviceConfigData.Name, ex);
            }

            return isServiceAvailable;
        }
        
        protected override void RestartService()
        {
            IISHelper.RestartServiceForApplicationPool(iisServerName,serviceConfigData.ApplicationPoolName,logger,ref applicationHealthInformation);            
            IISHelper.RestartServiceForSite(iisServerName,logger,ref applicationHealthInformation,serviceConfigData);            
        }

        protected override Process GetProcessIdForService()
        {
            return IISHelper.GetProcessIdForService(serviceConfigData.ApplicationPoolName);
        }

        protected override void BuildServiceHealthInformation(Tuple<double, double, float, double> cpuMemData)
        {
            this.applicationHealthInformation.CPUUsagePercent = cpuMemData.Item1;
            this.applicationHealthInformation.MemoryUsagePercent = cpuMemData.Item2;
            this.applicationHealthInformation.processMemInKB = cpuMemData.Item3;
            this.applicationHealthInformation.processMemInGB = cpuMemData.Item4;
            this.applicationHealthInformation.Status = Constants.Status.Running;
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

        private Dictionary<string, IEnumerable<ServiceEndpoint>> GetEndpointsForContracts()
        {
            var endpointsForContracts = new Dictionary<string, IEnumerable<ServiceEndpoint>>();

            var metaSet = GetMetaDataFromWsdl();

            ServiceContractGenerator scGenerator = new ServiceContractGenerator();

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
      
        private void AddDocumentToResults(object document, Collection<MetadataSection> results)
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