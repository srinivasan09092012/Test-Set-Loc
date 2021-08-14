//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Description;
using System.Web.Services.Discovery;
using System.Xml;
using System.Xml.Schema;

namespace Watchdog.EnvironmentMonitor
{
    public class WCFUtility
    {
        private static DiscoveryClientProtocol disco = new DiscoveryClientProtocol();

        public static MetadataSet GetMetaDataFromWsdl()
        {
            Collection<MetadataSection> results = new Collection<MetadataSection>();
            if (disco != null)
            {
                foreach (object document in disco.Documents.Values)
                {
                    AddDocumentToResults(document, results);
                }
            }

            return new MetadataSet(results);
        }

        public static void GetEndpointAddress(string endpointAddress)
        {
            disco.AllowAutoRedirect = true;
            disco.UseDefaultCredentials = true;
            disco.DiscoverAny(endpointAddress);
            disco.ResolveAll();
        }

        public static Dictionary<string, IEnumerable<ServiceEndpoint>> GetEndpointsForContracts()
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

        public static void AddDocumentToResults(object document, Collection<MetadataSection> results)
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
