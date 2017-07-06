using HPE.HSP.UA3.Core.API.BusinessRules.Providers.Tests.Domain;
using HP.HSP.UA3.Core.UX.Common.Utilities;
using InRuleTest.InRuleRuleSoapService;
using System;
using System.ServiceModel;
using System.Net.Http;
using System.Net.Http.Headers;

namespace InRuleTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            //ProcessRestRequest();
            ProcessSoapRequest();
        }

        private static void ProcessRestRequest()
        {
            try
            {
                // Get RuleApp as defined in the config (RepositoryRuleApp or FileSystemRuleApp)
                RepositoryRuleApp rules = new RepositoryRuleApp()
                {
                    RepositoryServiceUri = "http://bre1.dev.ua3.elabs.svcs.hpe.com/InRuleCatalogService/Service.svc",
                    RepositoryRuleAppRevisionSpec = new RepositoryRuleAppRevisionSpec()
                    {
                        RuleApplicationName = "BREApiTestApplication",
                        Label = null
                    },
                    UserName = "BASConsumer",
                    Password = "Password2015"
                };

                // Create new request
                ExecuteIndependentRuleSetRequest request = new ExecuteIndependentRuleSetRequest()
                {
                    RuleApp = rules,
                    RuleEngineServiceOutputTypes = new RuleEngineServiceOutputTypes()
                    {
                        ActiveNotifications = true,
                        ActiveValidations = true,
                        EntityState = true,
                        RuleExecutionLog = true
                    },
                    RuleSetName = "IndependentValidation",
                    EntityStateFieldName = "TestModel"
                };
                request.EntityState = BuildEntityState();
                Console.WriteLine("");
                Console.WriteLine("Request data:");
                Console.WriteLine(request.EntityState);

                // Execute REST call
                HttpResponseMessage webResponse = ProcessRESTCall(request);

                // Analyze output
                //Console.WriteLine("");
                //Console.WriteLine("Active Notifications:");
                //foreach (Notification notification in response.ActiveNotifications)
                //{
                //    Console.WriteLine(notification.NotificationType + ": " + notification.Message);
                //}

                //Console.WriteLine("");
                //Console.WriteLine("Active Validations:");
                //foreach (Validation validation in response.ActiveValidations)
                //{
                //    Console.WriteLine(validation.InvalidMessageText);
                //}

                //Console.WriteLine("");
                //Console.WriteLine("Output State:");
                //Console.WriteLine(response.EntityState);
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("Unknown exception occurred during RuleEngineService REST request: " + ex.ToString());
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static HttpResponseMessage ProcessRESTCall(ExecuteIndependentRuleSetRequest request)
        {
            HttpResponseMessage response = null;
            using (HttpClient client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://bre1.dev.ua3.elabs.svcs.hpe.com/InRuleRuleEngineService/HttpService.svc");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PostAsJsonAsync("http://bre1.dev.ua3.elabs.svcs.hpe.com/InRuleRuleEngineService/HttpService.svc/ExecuteIndependentRuleSet", request).Result;
                response.EnsureSuccessStatusCode();
            }

            return response;
        }

        private static void ProcessSoapRequest()
        {
            using (RuleEngineServiceClient proxy = new RuleEngineServiceClient())
            {
                try
                {
                    // Set endpoint and binding
                    BasicHttpBinding binding = new BasicHttpBinding();
                    EndpointAddress endpoint = new EndpointAddress("http://bre1.dev.ua3.elabs.svcs.hpe.com/InRuleRuleEngineService/Service.svc");
                    ChannelFactory<IRuleEngineServiceChannel> channelFactory = new ChannelFactory<IRuleEngineServiceChannel>(binding, endpoint);

                    // Get RuleApp as defined in the config (RepositoryRuleApp or FileSystemRuleApp)
                    RepositoryRuleApp rules = new RepositoryRuleApp()
                    {
                        RepositoryServiceUri = "http://bre1.dev.ua3.elabs.svcs.hpe.com/InRuleCatalogService/Service.svc",
                        RepositoryRuleAppRevisionSpec = new RepositoryRuleAppRevisionSpec()
                        {
                            RuleApplicationName = "BREApiTestApplication",
                            Label = null
                        },
                        UserName = "BASConsumer",
                        Password = "Password2015"
                    };

                    // Create new request
                    ExecuteIndependentRuleSetRequest request = new ExecuteIndependentRuleSetRequest()
                    {
                        RuleApp = rules,
                        RuleEngineServiceOutputTypes = new RuleEngineServiceOutputTypes()
                        {
                            ActiveNotifications = true,
                            ActiveValidations = true,
                            EntityState = true,
                            RuleExecutionLog = true
                        },
                        RuleSetName = "IndependentValidation",
                        EntityStateFieldName = "TestModel"
                    };
                    request.EntityState = BuildEntityState();
                    Console.WriteLine("");
                    Console.WriteLine("Request data:");
                    Console.WriteLine(request.EntityState);

                    // Submit Request
                    Console.WriteLine("");
                    Console.WriteLine("- Calling ApplyRules() from RuleEngineService...");
                    RuleEngineServiceResponse response = proxy.ExecuteRuleEngineRequest(request);

                    // Analyze output
                    Console.WriteLine("");
                    Console.WriteLine("Active Notifications:");
                    foreach (Notification notification in response.ActiveNotifications)
                    {
                        Console.WriteLine(notification.NotificationType + ": " + notification.Message);
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Active Validations:");
                    foreach (Validation validation in response.ActiveValidations)
                    {
                        Console.WriteLine(validation.InvalidMessageText);
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Output State:");
                    Console.WriteLine(response.EntityState);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Unknown exception occurred during RuleEngineService SOAP request: " + ex.ToString());
                }
                finally
                {
                    Console.ReadKey();
                }
            }
        }

        private static string BuildEntityState()
        {
            return Serializer.XmlSerialize<TestModel>(new TestModel("Informational", 1));
            //return "<TestModel><Name>Name</Name><Priority>1</Priority><Validation>0</Validation><Rules><Validation><AuditMessages /><FieldMessages /><Messages /></Validation></Rules></TestModel>";
        }
    }
}
