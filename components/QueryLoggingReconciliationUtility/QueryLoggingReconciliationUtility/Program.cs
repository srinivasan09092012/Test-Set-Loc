using QueryLoggingReconciliationUtility.Terminal.DataListsQueryService;
using QueryLoggingReconciliationUtility.Terminal.DataListsService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QueryLoggingReconciliationUtility.Terminal
{
    public class Program
    {
        private static string currentBASAssemblyPath = string.Empty;
        private static bool done;

        public static void Main(string[] args)
        {
            SetCurrentBASAssemblyPath();

            Assembly assembly = LoadAssemblyFromPath();

            var assemblyTypes = assembly.GetTypes().ToList();

            List<Type> queryServiceInterfaces = GetQueryServiceInterfaces(assemblyTypes);

            Type selectedInterface = SelectInterfaceToProcess(queryServiceInterfaces);
            ////Console.WriteLine($"You have selected: {selectedInterface.Name}");

            ProcessQueryServiceInterface(assemblyTypes, selectedInterface);

            Console.WriteLine();
            Console.WriteLine($"Press any key to exit.");
            Console.ReadLine();
        }

        public static void SetCurrentBASAssemblyPath()
        {
            Console.WriteLine("Please enter the absolute path to the BAS Assembly (.dll) file: ");
            string assemblyPath = Console.ReadLine();
            if (File.Exists(assemblyPath) && (Path.GetExtension(assemblyPath) == ".dll"))
            {
                Console.WriteLine($"Assembly file was found at the provided path.");
                currentBASAssemblyPath = assemblyPath;
            }
            else
            {
                Console.WriteLine($"A valid Assembly file was not found at the provided path. Please make sure the BAS solution has been built successfully and the .dll file exists at the given path.");
                SetCurrentBASAssemblyPath();
            }

            Console.WriteLine($"Current BAS Assembly Path: {currentBASAssemblyPath}");
            Console.WriteLine();
        }

        public static Assembly LoadAssemblyFromPath()
        {
            ////An unhandled exception of type 'System.Reflection.ReflectionTypeLoadException' occurred in mscorlib.dll
            ////Additional information: Unable to load one or more of the requested types. Retrieve the LoaderExceptions property for more information.
            ////var assembly = Assembly.LoadFile(@"C:\UA3\CodeAnalysisBuildOutput\HP.HSP.UA3.TPLPolicy.BAS.Policy.dll");

            return Assembly.LoadFrom(currentBASAssemblyPath);
        }

        private static List<Type> GetQueryServiceInterfaces(List<Type> assemblyTypes)
        {
            var queryServiceInterfaces =
                assemblyTypes
                .Where(type => type.IsInterface && type.IsDefined(typeof(ServiceContractAttribute)))
                ////.Where(type => type.Name.EndsWith("QueryService"))
                .OrderBy(type => type.Name)
                .ToList();
            
            ////PrintQueryServiceInterfaces(queryServiceInterfaces);
            
            return queryServiceInterfaces;
        }

        private static void PrintQueryServiceInterfaces(List<Type> queryServiceInterfaces)
        {
            Console.WriteLine();
            Console.WriteLine("List of Query Service Interfaces:");
            queryServiceInterfaces.ForEach(si => { Console.WriteLine($"{si.Name}"); });
        }

        private static Type SelectInterfaceToProcess(List<Type> queryServiceInterfaces)
        {
            var interfaceSelectionDict = new Dictionary<string, Type>();
            for (int i = 0; i < queryServiceInterfaces.Count; i++)
            {
                var itemKey = (i + 1).ToString();
                interfaceSelectionDict.Add(itemKey, queryServiceInterfaces[i]);
            }

            Console.WriteLine($"Query Service Interfaces in this Assembly: {queryServiceInterfaces.Count}");
            foreach (var item in interfaceSelectionDict)
            {
                Console.WriteLine($"{item.Key}: {item.Value.Name}");
            }
            Console.WriteLine("Enter the # for interface to process (Ex: 1) :");

            string selectedKey = string.Empty;
            do
            {
                selectedKey = Console.ReadLine();
                if (!interfaceSelectionDict.ContainsKey(selectedKey))
                {
                    Console.WriteLine("Invalid selection. Please enter the # for the interface to process.");
                }
            } while (!interfaceSelectionDict.ContainsKey(selectedKey));

            Type selectedInterface = interfaceSelectionDict[selectedKey];
            Console.WriteLine($"You have selected: {selectedInterface.Name}");
            Console.WriteLine();
            return selectedInterface;
        }

        private static void ProcessQueryServiceInterface(List<Type> assemblyTypes, Type selectedInterface)
        {
            Console.WriteLine();
            Console.WriteLine($"Query Service Interface: {selectedInterface.Name}");

            var implementation =
                assemblyTypes
                .Where(assemblyType => !assemblyType.IsInterface && assemblyType.GetInterfaces().Contains(selectedInterface))
                ////.Where(type => type.Name.EndsWith("QueryService"))
                .OrderBy(type => type.Name)
                .FirstOrDefault();
            Console.WriteLine($"Implementation Name: {implementation.Name}");

            var operations =
                selectedInterface
                .GetMethods()
                .Where(m => m.IsDefined(typeof(OperationContractAttribute)))
                .OrderBy(m => m.Name)
                .ToList();

            DataListItemsQuery queryResponse = new DataListItemsQuery();
            queryResponse = FetchQueryLogSettingDataListItems(implementation);

            var dataListItems = queryResponse.QueryResult;

            operations.ForEach(operation =>
            {
                DisplayServiceOperationDetails(operation, implementation, dataListItems);
            });
        }

        private static DataListItemsQuery FetchQueryLogSettingDataListItems(Type implementation)
        {
            DataListItemsQuery queryResponse;
            try
            {
                string dataListsQueryServiceURL = ConfigurationManager.AppSettings["DataListsQueryServiceURL"].ToString();
                ////DataListsQueryServiceClient client = new DataListsQueryServiceClient("BasicHttpsBinding_IDataListsQueryService", "https://localhost:44311/DataListsQueryService.svc");
                DataListsQueryServiceClient client = new DataListsQueryServiceClient("BasicHttpsBinding_Large_IDataListsQueryService", dataListsQueryServiceURL);

                DataListItemsQuery query = new DataListItemsQuery
                {
                    Where = new DataListItemsQueryParms
                    {
                        ContentID = ConfigurationManager.AppSettings["QueryLogSettingDataListContentId"].ToString(),
                        FilterCode = implementation.Name,
                        FilterCodeSearchMode = WhereOperatorEnumerationsSearchModeType.StartsWith,
                        IncludeIsValid = null ////Observations from debugging using PL UX: This is null when IncludeInvalids is checked in the UX. So, when this is null it should fetch both invalid and valid items. And when true it should fetch only items which are valid.
                    },
                    PageSize = 2000, ////operations.Count,
                    Requestor = new Terminal.DataListsQueryService.RequestorModel
                    {
                        ApplicationName = ConfigurationManager.AppSettings["ApplicationName"].ToString(),
                        TenantId = ConfigurationManager.AppSettings["TenantId"].ToString(),
                        LocaleCode = "en-us",
                        IdentifierId = "testUser"
                    }
                };
                queryResponse = client.GetDataListItems(query);
                client.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("An Exception occurred when called in the DL Query Service.");
                throw;
            }

            return queryResponse;
        }

        private static void DisplayServiceOperationDetails(MethodInfo operation, Type implementation, CodeListModel[] dataListItems)
        {
            var queryLogItem = $"{implementation.Name}.{operation.Name}.QueryLog";
            var queryLogVerboseItem = $"{implementation.Name}.{operation.Name}.QueryLogVerbose";

            if (dataListItems != null)
            {
                if (dataListItems.Any(item => item.Code == queryLogItem))
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine(queryLogItem);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(queryLogItem);

                    if (!done)
                    {
                        AddDataListItem(queryLogItem);
                    }
                }
                Console.ResetColor();

                if (dataListItems.Any(item => item.Code == queryLogVerboseItem))
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine(queryLogVerboseItem);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(queryLogVerboseItem);
                }
                Console.ResetColor();
            }

            Console.WriteLine();
        }

        private static void AddDataListItem(string queryLogItem)
        {
            string dataListsCommandServiceURL = ConfigurationManager.AppSettings["DataListsCommandServiceURL"].ToString();
            DataListsServiceClient client = new DataListsServiceClient("BasicHttpBinding_IDataListsService", dataListsCommandServiceURL);
            AddDataListItemCommand command = new AddDataListItemCommand
            {
                DataListContentId = ConfigurationManager.AppSettings["QueryLogSettingDataListContentId"].ToString(),
                DataListItem = new DataListItem
                {
                    DataListItemLanguages = new List<DataListItemLanguage>
                    {
                        new DataListItemLanguage
                        {
                            DataListItemId = "",
                            Description = "Log request for service operation​",
                            IsActive = true,
                            LocaleId = "en-us",
                            LongDescription = "Allows the client to enable or disable the request logging of data for the service operation."
                        },
                        new DataListItemLanguage
                        {
                            DataListItemId = "",
                            Description = "Solicitud de registro para la operación del servicio",
                            IsActive = true,
                            LocaleId = "es-mx",
                            LongDescription = "Permite al cliente habilitar o deshabilitar el registro de solicitud de datos para la operación del servicio.​"
                        }
                    },
                    DataListItemModuleConfiguration = new DataListItemModuleConfiguration
                    {
                        DataListItemIsEditable = true,
                    },
                    EffectiveDate = DateTime.MinValue,
                    EndDate = DateTime.MaxValue,
                    ItemIsActive = true,
                    ItemIsEditable = true,
                    Key = queryLogItem,
                    OrderIndex = 688,
                    Periods = new List<DataListItemPeriod>
                    {
                        new DataListItemPeriod
                        {
                            DataListItemPeriodId = "",
                            IsValid = true,
                            PeriodEffectiveDate = DateTime.Now.Date,
                            PeriodEndDate = DateTime.MaxValue.Date
                        }
                    }
                },
                Requestor = new Terminal.DataListsService.RequestorModel
                {
                    ApplicationName = ConfigurationManager.AppSettings["ApplicationName"].ToString(),
                    TenantId = ConfigurationManager.AppSettings["TenantId"].ToString(),
                    LocaleCode = "en-us",
                    IdentifierId = "testUser"
                }
            };

            var dataListItemAdded = client.AddDataListItem(command);
            done = true;
            Console.WriteLine("DataListItem Added successfully");
            Console.WriteLine(dataListItemAdded.DataListItemId);
        }
    }
}
