using QueryLoggingReconciliationUtility.GUI.DataListsQueryService;
using QueryLoggingReconciliationUtility.GUI.DataListsService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueryLoggingReconciliationUtility.GUI.Forms
{
    public partial class MainForm : Form
    {
        private static string currentBASAssemblyPath = string.Empty;
        private static List<Type> queryServiceInterfaces = null;
        private static List<Type> assemblyTypes = null;
        private static string loggedInWindowsUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        public MainForm()
        {
            InitializeComponent();
            labelLoggedInUserValue.Text = loggedInWindowsUser;
        }

        private void buttonBrowseForAssembly_Click(object sender, EventArgs e)
        {
            DialogResult openFileDialog = openFileDialogAssembly.ShowDialog();

            if (openFileDialog == DialogResult.OK)
            {
                string assemblyPath = openFileDialogAssembly.FileName;
                if (File.Exists(assemblyPath) && (Path.GetExtension(assemblyPath) == ".dll"))
                {
                    textBoxAssemblyPath.Text = assemblyPath;
                    currentBASAssemblyPath = assemblyPath;
                }
                else
                {
                    MessageBox.Show("A valid Assembly file was not found at the provided path. Please make sure the BAS solution has been built successfully and the .dll file exists at the given path.");
                }
            }
        }

        public static Assembly LoadAssemblyFromPath()
        {
            return Assembly.LoadFrom(currentBASAssemblyPath);
        }

        private void buttonLoadServiceContracts_Click(object sender, EventArgs e)
        {
            Assembly assembly = LoadAssemblyFromPath();

            comboBoxServiceContracts.Items.Clear();
            assemblyTypes = assembly.GetTypes().ToList();

            queryServiceInterfaces = GetQueryServiceInterfaces(assemblyTypes);

            foreach (var item in queryServiceInterfaces)
            {
                comboBoxServiceContracts.Items.Add(item.Name);
            }
        }

        private List<Type> GetQueryServiceInterfaces(List<Type> assemblyTypes)
        {
            bool loadOnlyTypesWhichEndWithQueryService = checkboxEndsWithQueryService.Checked;

            var queryServiceInterfaces =
                assemblyTypes
                .Where(type => type.IsInterface && type.IsDefined(typeof(ServiceContractAttribute)))
                .Where(type => (!loadOnlyTypesWhichEndWithQueryService) || type.Name.EndsWith("QueryService"))
                .OrderBy(type => type.Name)
                .ToList();

            return queryServiceInterfaces;
        }

        private void comboBoxServiceContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBoxServiceContracts = (ComboBox)sender;
            string selectedServiceContract = comboBoxServiceContracts.SelectedItem.ToString();

            var selectedInterface = assemblyTypes.Where(t => t.Name == selectedServiceContract).FirstOrDefault();
            labelSelectedServiceContractValue.Text = selectedInterface.FullName;
            ProcessQueryServiceInterface(assemblyTypes, selectedInterface);
        }

        private void ProcessQueryServiceInterface(List<Type> assemblyTypes, Type selectedInterface)
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

            dgvServiceOperations.Rows.Clear();
            var totalOperations = 0;
            var itemsHavingEntry = 0;
            var itemsWithoutEntry = 0;

            operations.ForEach(operation =>
            {
                totalOperations = totalOperations + 1;
                DisplayServiceOperationDetails(operation, implementation, dataListItems, ref itemsHavingEntry, ref itemsWithoutEntry);
            });

            labelTotalOperationsValue.Text = totalOperations.ToString();
            labelTotalItemsValue.Text = (totalOperations * 2).ToString();
            labelItemsHavingEntryValue.Text = itemsHavingEntry.ToString();
            labelItemsWithoutEntryValue.Text = itemsWithoutEntry.ToString();
        }

        private DataListItemsQuery FetchQueryLogSettingDataListItems(Type implementation)
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
                    Requestor = new DataListsQueryService.RequestorModel
                    {
                        ApplicationName = ConfigurationManager.AppSettings["ApplicationName"].ToString(),
                        TenantId = ConfigurationManager.AppSettings["TenantId"].ToString(),
                        LocaleCode = "en-us",
                        IdentifierId = loggedInWindowsUser.Split('\\')[1] //Remove the domain part of the Windows User
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

        private void DisplayServiceOperationDetails(MethodInfo operation, Type implementation, CodeListModel[] dataListItems, ref int itemsHavingEntry, ref int itemsWithoutEntry)
        {
            var queryLogItem = $"{implementation.Name}.{operation.Name}.QueryLog";
            var queryLogVerboseItem = $"{implementation.Name}.{operation.Name}.QueryLogVerbose";

            if (dataListItems != null)
            {
                if (dataListItems.Any(item => item.Code == queryLogItem))
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvServiceOperations);
                    row.Cells[0].ReadOnly = true;
                    row.Cells[0].Value = false;
                    row.Cells[1].Value = queryLogItem;
                    row.DefaultCellStyle.BackColor = Color.Green;
                    dgvServiceOperations.Rows.Add(row);
                    itemsHavingEntry = itemsHavingEntry + 1;
                }
                else
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvServiceOperations);
                    row.Cells[0].Value = false;
                    row.Cells[1].Value = queryLogItem;
                    row.DefaultCellStyle.BackColor = Color.Red;
                    dgvServiceOperations.Rows.Add(row);
                    itemsWithoutEntry = itemsWithoutEntry + 1;
                }

                if (dataListItems.Any(item => item.Code == queryLogVerboseItem))
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvServiceOperations);
                    row.Cells[0].ReadOnly = true;
                    row.Cells[0].Value = false;
                    row.Cells[1].Value = queryLogVerboseItem;
                    row.DefaultCellStyle.BackColor = Color.Green;
                    dgvServiceOperations.Rows.Add(row);
                    itemsHavingEntry = itemsHavingEntry + 1;
                }
                else
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvServiceOperations);
                    row.Cells[0].Value = false;
                    row.Cells[1].Value = queryLogVerboseItem;
                    row.DefaultCellStyle.BackColor = Color.Red;
                    dgvServiceOperations.Rows.Add(row);
                    itemsWithoutEntry = itemsWithoutEntry + 1;
                }
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
                    DataListItemModule = new List<DataListItemModule>(),
                    VosTagModule = new List<VosTagModule>(),
                    DataListItemModuleConfiguration = new DataListItemModuleConfiguration
                    {
                        DataListItemIsEditable = true,
                    },
                    EffectiveDate = DateTime.MinValue,
                    EndDate = DateTime.MaxValue,
                    ItemIsActive = true, //TODO: Decide if it can and should default to false during creation 
                    ItemIsEditable = true,
                    Key = queryLogItem,
                    OrderIndex = 688,
                    Periods = new List<DataListItemPeriod>
                    {
                        new DataListItemPeriod
                        {
                            DataListItemPeriodId = "",
                            IsValid = true, //TODO: Decide if it can and should default to false during creation 
                            PeriodEffectiveDate = DateTime.Now.Date,
                            PeriodEndDate = DateTime.MaxValue.Date
                        }
                    }
                },
                Requestor = new DataListsService.RequestorModel
                {
                    ApplicationName = ConfigurationManager.AppSettings["ApplicationName"].ToString(),
                    TenantId = ConfigurationManager.AppSettings["TenantId"].ToString(),
                    LocaleCode = "en-us",
                    IdentifierId = loggedInWindowsUser.Split('\\')[1] //Remove the domain part of the Windows User
                }
            };

                var dataListItemAdded = client.AddDataListItem(command);
                Console.WriteLine("DataListItem Added successfully");
                Console.WriteLine(dataListItemAdded.DataListItemId);
        }

        private void btnCreateQueryLoggingDataListEntry_Click(object sender, EventArgs e)
        {
            var selectedRows = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dgvServiceOperations.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Selected"].Value) == true)
                {
                    selectedRows.Add(row);
                    try
                    {
                        var item = row.Cells[1].Value.ToString();
                        AddDataListItem(item);
                        row.Cells["Status"].Value = "Success.";
                        row.DefaultCellStyle.BackColor = Color.Green;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An Exception occurred when calling the DL Command Service.");
                        row.Cells["Status"].Value = "Failed.";
                    }
                }
            }

            ReloadServiceOperations();
        }

        private void ReloadServiceOperations()
        {
            string selectedServiceContract = comboBoxServiceContracts.SelectedItem.ToString();
            var selectedInterface = assemblyTypes.Where(t => t.Name == selectedServiceContract).FirstOrDefault();

            ProcessQueryServiceInterface(assemblyTypes, selectedInterface);
        }
    }
}
