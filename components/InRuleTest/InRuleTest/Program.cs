using HPE.HSP.UA3.Core.API.BusinessRules.Providers.Tests.Domain;
using InRuleRestLibrary;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InRuleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                ProcessRESTRequest();
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

        private async static void ProcessRESTRequest()
        {
            // Create rule application
            RuleApp ruleApp = new RuleApp()
            {
                RepositoryServiceUri = "http://bre1.dev.ua3.elabs.svcs.hpe.com/InRuleCatalogService/Service.svc",
                RepositoryRuleAppRevisionSpec = new RepositoryRuleAppRevisionSpec()
                {
                    RuleApplicationName = "BREApiTestApplication",
                    Label = string.Empty
                },
                UserName = "BASConsumer",
                Password = "Password2015"
            };

            // Create new request
            ExecuteIndependentRuleSetRequest request = new ExecuteIndependentRuleSetRequest()
            {
                RequestId = Guid.NewGuid().ToString("n"),
                RuleApp = ruleApp,
                RuleEngineServiceOutputTypes = new RuleEngineServiceOutputTypes()
                {
                    ActiveNotifications = true,
                    ActiveValidations = true,
                    EntityState = true,
                    RuleExecutionLog = true,
                    Overrides = true
                },
                RuleSetName = "IndependentValidation",
                EntityStateFieldName = "ParmTestModel",
                EntityState = JsonConvert.SerializeObject(new TestModel("Informational", 1))
            };

            Console.WriteLine("");
            Console.WriteLine("Entity State:");
            Console.WriteLine(request.EntityState);

            string serializedRequest = JsonConvert.SerializeObject(request);
            Console.WriteLine("");
            Console.WriteLine("Request:");
            Console.WriteLine(serializedRequest);

            // Execute REST call
            Stopwatch timer = new Stopwatch();
            timer.Start();
            HttpResponseMessage webResponse = await ProcessRESTCall(serializedRequest);
            string response = await webResponse.Content.ReadAsStringAsync();
            timer.Stop();
            Console.WriteLine("");
            Console.WriteLine("Response:");
            Console.WriteLine(response);
            webResponse.EnsureSuccessStatusCode();

            // Analyze output
            ExecuteIndependentRuleSetResponse rulesetResponse = JsonConvert.DeserializeObject<ExecuteIndependentRuleSetResponse>(response);
            TestModel responseModel = JsonConvert.DeserializeObject<TestModel>(rulesetResponse.EntityState);

            Console.WriteLine("");
            Console.WriteLine("Status: SUCCESS");

            Console.WriteLine("");
            Console.WriteLine("Active Notifications:");
            foreach (ActiveNotification notification in rulesetResponse.ActiveNotifications)
            {
                Console.WriteLine(notification.NotificationType + ": " + notification.Message);
            }

            Console.WriteLine("");
            Console.WriteLine("Active Validations:");
            foreach (ActiveValidation validation in rulesetResponse.ActiveValidations)
            {
                Console.WriteLine(validation.InvalidMessageText);
            }

            Console.WriteLine("");
            Console.WriteLine("Execution: " + timer.ElapsedMilliseconds + "ms");
        }

        private static async Task<HttpResponseMessage> ProcessRESTCall(string request)
        {
            HttpResponseMessage response = null;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.PostAsync("http://bre1.dev.ua3.elabs.svcs.hpe.com/InRuleRuleEngineService/HttpService.svc/ExecuteIndependentRuleSet",
                      new StringContent(request, Encoding.UTF8, "application/json"));
            }

            return response;
        }
    }
}
