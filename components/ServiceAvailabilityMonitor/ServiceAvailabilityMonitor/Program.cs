using log4net;
using log4net.Config;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ServiceAvailabilityMonitor
{
    class Program
    {
        private static readonly ILog logger = LogManager.GetLogger("UA3");

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var urls = GetServiceUrls();

            foreach(Config url in urls)
            {
                try
                {
                    if(WsdlOnlyCheck())
                    {
                        GetMetaDataFromWsdl(url.WsdlAddress);
                    }
                    else
                    {
                        InvokeIsAvailableOperation(url.SvcAddress);
                    }

                    if (LogSuccess())
                    {
                        WriteSuccess(url.SvcAddress);
                    }

                }
                catch(Exception ex)
                {
                    WriteError(url.SvcAddress, ex, url.Message);
                }
            }

            StopConsole();
        }

        [Conditional("DEBUG")]
        public static void StopConsole()
        {
            Console.ReadLine();
        }

        public static bool LogSuccess()
        {
            bool result = false;
            if (!bool.TryParse(ConfigurationManager.AppSettings["LogSuccess"], out result))
            {
                throw new InvalidOperationException("LogSuccess app setting can only be true or false");
            }
            return result;
        }

        public static bool WsdlOnlyCheck()
        {
            bool result = false;
            if(!bool.TryParse(ConfigurationManager.AppSettings["WsdlOnlyCheck"], out result))
            {
                throw new InvalidOperationException("WsdlOnlyCheck app setting can only be true or false");
            }
            return result;
        }

        public static List<Config> GetServiceUrls()
        {
            string ServiceConfigFile = "ServiceConfigs.xml";
            XDocument xml = XDocument.Load(ServiceConfigFile);

            var baseAddress = (from n in xml.Descendants("ServiceConfiguration")
                               select n.Element("BaseAddress").Value).First();

            //var URLName = (from n in xml.Descendants("ServiceUrls")
            //               select n.Elements("ServiceAddress")).First()
            //               .Select( x => x.Value);


            var URLConfig = (from n in xml.Descendants("ServiceAddress")
                           select new Config
                           {
                               Address = (n.Element("Address")).Value,
                               Message = (n.Element("ErrorMessage")).Value,
                               BaseAddress = n.Elements("BaseAddressOverride").Any() ? n.Element("BaseAddressOverride").Value : string.Empty
                           }).ToList();

            URLConfig.ForEach(config =>
            {
                if (string.IsNullOrEmpty(config.BaseAddress))
                {
                    config.BaseAddress = baseAddress;
                }
            });


            return URLConfig;
        }

        public static void WriteSuccess(string url)
        {
            logger.Info(url + ": OK");
        }

        public static void WriteError(string url, Exception ex, string message = "")
        {
            string innerMessage = ex.InnerException != null ? ex.InnerException.Message : string.Empty;

            logger.Error(Environment.NewLine + 
                url + ":ERROR Service Unavailable" +
                Environment.NewLine + message +
                Environment.NewLine + ex.Message + innerMessage);
        }

        public static void WriteEndpoint(string url)
        {
            logger.Info(Environment.NewLine + "Enpoint Found: " + url);
        }

        public static Binding GetBinding(string serviceUrl)
        {
            Binding binding = null;

            if (serviceUrl.Contains(@"https://"))
            {
                var tempBinding = new WSHttpBinding();
                tempBinding.MaxReceivedMessageSize = 5000000;
                tempBinding.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
                {
                    MaxNameTableCharCount = 5000000,
                    MaxDepth = 64
                };
                binding = tempBinding;
            }
            else
            {
                var tempBinding = new BasicHttpBinding();
                tempBinding.MaxReceivedMessageSize = 5000000;
                tempBinding.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
                {
                    MaxNameTableCharCount = 5000000,
                    MaxDepth = 64
                };

                binding = tempBinding;
            }

            return binding;
        }

        public static MetadataSet GetMetaDataFromWsdl(string serviceUrl)
        {
            Uri mexAddress = new Uri(serviceUrl);
            Binding binding = GetBinding(serviceUrl);
            MetadataExchangeClientMode mexMode = MetadataExchangeClientMode.HttpGet;

            // Get the metadata file from the service.
            MetadataExchangeClient mexClient = new MetadataExchangeClient(binding);
            mexClient.ResolveMetadataReferences = true;
            mexClient.MaximumResolvedReferences = 1000;

            var endpoint = new EndpointAddress(mexAddress);
            MetadataSet metaSet = mexClient.GetMetadata(mexAddress, mexMode);

            WsdlImporter importer = new WsdlImporter(metaSet);
            Collection<ContractDescription> contracts = importer.ImportAllContracts();
            ServiceEndpointCollection allEndpoints = importer.ImportAllEndpoints();

            if (LogSuccess())
            {
                allEndpoints.ToList().ForEach(se => WriteEndpoint(se.Address.Uri.AbsoluteUri));
            }

            return metaSet;
        }

        /// <summary>
        /// Common web services methods to invove WCF/ASMX services
        /// </summary>
        /// <returns></returns>
        public static bool InvokeIsAvailableOperation(string serviceUrl)
        {
            var operationName = "IsServiceAvailable";

            var metaSet = GetMetaDataFromWsdl(serviceUrl);

            // Import all contracts and endpoints
            WsdlImporter importer = new WsdlImporter(metaSet);
            Collection<ContractDescription> contracts = importer.ImportAllContracts();
            ServiceEndpointCollection allEndpoints = importer.ImportAllEndpoints();

            // Generate type information for each contract
            ServiceContractGenerator generator = new ServiceContractGenerator();
            var endpointsForContracts = new Dictionary<string, IEnumerable<ServiceEndpoint>>();

            foreach (ContractDescription contract in contracts)
            {
                generator.GenerateServiceContractType(contract);
                // Keep a list of each contract's endpoints
                endpointsForContracts[contract.Name] = allEndpoints.Where(x => x.Contract.Name == contract.Name).ToList();
            }

            if (generator.Errors.Count != 0)
            {
                throw new Exception("There were errors during code compilation.");
            }


            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");

            // Compile the code file to an in-memory assembly
            // Don't forget to add all WCF-related assemblies as references
            CompilerParameters compilerParameters = new CompilerParameters(
                new string[] {
                      "System.dll", "System.ServiceModel.dll",
                    "System.Runtime.Serialization.dll" });
            compilerParameters.GenerateInMemory = true;

            CompilerResults results = codeDomProvider.CompileAssemblyFromDom(
                  compilerParameters, generator.TargetCompileUnit);

            if (results.Errors.Count > 0)
            {
                throw new Exception("There were errors during generated code compilation");
            }

            // Find the proxy type that was generated for the specified contract
            // (identified by a classand ICommunicationbject)
            var types = results.CompiledAssembly.GetTypes().ToList();
            Type clientProxyType = types.First(
              t => t.IsClass && t.GetInterface(typeof(ICommunicationObject).Name) != null);

            // Get the first service endpoint for the contract
            ServiceEndpoint se = endpointsForContracts.First().Value.First();

            // Create an instance of the proxy
            // Pass the endpoint's binding and address as parameters
            // to the ctor
            object instance = results.CompiledAssembly.CreateInstance(
                 clientProxyType.Name,
                  false,
                   System.Reflection.BindingFlags.CreateInstance,
                  null,
                new object[] { se.Binding, se.Address },
                 CultureInfo.CurrentCulture, null);

            // Get the operation's method, invoke it, and get the return value
            object retVal = instance.GetType().GetMethod(operationName).Invoke(instance, null);
            return (bool)retVal;
        }
    }
}
