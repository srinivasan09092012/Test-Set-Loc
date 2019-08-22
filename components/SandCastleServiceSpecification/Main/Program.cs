using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using APISvcSpec.IO;
using APISvcSpec.Helpers.Scan;
using Common;
using APISvcSpec;
using APISvcSpec.Helpers.HTML;
using Common.ModuleSettings;

namespace SandCastleSvcSpec
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***********************************");
            Console.WriteLine("SandCastle Service Enhancement Tool");
            Console.WriteLine("***********************************");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Loading settings");
            var moduleSetting = validateInputArgs();
            Console.WriteLine("Loading settings... Done");
            Console.WriteLine("");
            Console.WriteLine("");

            foreach (var module in moduleSetting)
            {
                Console.WriteLine("Runnig Settings for Module: " + module.ModuleNameDisplay);
                Console.WriteLine("");
                DoBackup(module);
                ExecuteSandCastleEnhancement(module);// improve for parallel programing
            }
        }

        static List<ModuleSettingModel> validateInputArgs()
        {
            string storageDrive = ConfigurationManager.AppSettings["storageDrive"];
            var sDrive = new DirectoryInfo(storageDrive);
            var xs = new XmlSerializer(typeof(ModuleSettingModel));
            var moduleSetting = new ModuleSettingModel();
            var moduleSeetingList = new List<ModuleSettingModel>();

            if (sDrive.Exists)
            {
                var ModuleSettingList = sDrive.EnumerateFiles("*.xml", SearchOption.TopDirectoryOnly);

                foreach (FileInfo file in ModuleSettingList)
                {
                    using (var sr = new StreamReader(file.FullName))
                    {
                        moduleSetting = (ModuleSettingModel)xs.Deserialize(sr);
                        moduleSeetingList.Add(moduleSetting);
                    }
                }

                return moduleSeetingList; // this will be removed when parallel programing is in placee
                                            /*Once you start running processes in parallel, 
                                             * you start to have to track other considerations like how many processors / cores you have, 
                                             * how many tasks you want to try to run in parallel, how to make your variables thread safe, 
                                             * and how to track the progress and results of your processes.*/
            }
            else
            {
                throw new Exception("Information to produce documentation not found.");
            }
        }

        static void DoBackup(ModuleSettingModel moduleSetting)
        {
            #region backupsite
            Backup bkp = new Backup(moduleSetting.WebSourcePath, moduleSetting.WebTargetPath);
            bkp.BackUpWebSite();
            bkp.copyFile(Environment.CurrentDirectory + Common.Constants.WebSolutionStructure.Folders.img + Common.Constants.WebSolutionStructure.Files.UnderConstructionIcon, moduleSetting.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Icons + Common.Constants.WebSolutionStructure.Files.UnderConstructionIcon);
            #endregion
        }

        static void ExecuteSandCastleEnhancement(ModuleSettingModel moduleSetting)
        {
            Console.WriteLine("Executing SandCastle Customization Process");
            Console.WriteLine("");

            #region variable declarations and initializations
            HtmlFactory factoryHtml = new HtmlFactory();
            factoryHtml.ModuleSettings = moduleSetting;
            List<string> serviceUrls = new List<string>();
            List<string> CommandsPage = new List<string>();
            ScanMissingTagsHelper missingScanHelper = new ScanMissingTagsHelper();
            Dictionary<string, string> commandsToPrint = new Dictionary<string, string>();
            Dictionary<string, string> EventsToPrint = new Dictionary<string, string>();
            Dictionary<string, string> ModelsToPrint = new Dictionary<string, string>();
            Dictionary<string, string> QueryToPrint = new Dictionary<string, string>();
            Dictionary<string, string> DtosToPrint = new Dictionary<string, string>();
            #endregion

           
            Console.WriteLine("");
            Console.WriteLine(".... Done");

            Console.WriteLine("Preparing Services Landing Page");
            Console.WriteLine("");
            #region prepare Landing Page
            factoryHtml.updateIndexFile(Common.Constants.WebSolutionStructure.Files.IndexPage, moduleSetting.MainPageContent);
            #endregion

            Console.WriteLine("Preparing Service Operation List Pages");
            Console.WriteLine("");
            #region Preparing Service Operation List Pages
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.prepareServiceOperationList(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
                factoryHtml.UpdateALLTableList(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
            }
            #endregion

            Console.WriteLine("Updating Left Navigator");
            Console.WriteLine("");
            #region Updating Left Navigator 
            //TODO:  All the pages have the left navigator by default, there is a good chance to have the navigator in the main page only and setup a cointainer to present each page, to achive this, i will have to remove the left navigator from each single page.

            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                CommandsPage.AddRange(factoryHtml.getServiceListFromPageContent(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage));
            }

            serviceUrls = factoryHtml.extractServiceUrl(CommandsPage);

            foreach (string url in serviceUrls)
            {
                factoryHtml.UpdateLeftNavigator(url, CommandsPage);
            }

            factoryHtml.UpdateLeftNavigator(moduleSetting.WebTargetPath + Constants.WebSolutionStructure.Folders.Html + moduleSetting.MainPageContent, CommandsPage);
            #endregion

            Console.WriteLine("Preparing Documentation Pages:");
            Console.WriteLine("");
            # region Preparing Command and Event Pages

            //Commands
            CommandsPage.Clear();
            CommandsPage.AddRange(Directory.EnumerateFiles(moduleSetting.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html, "*_Contracts_Commands_*" + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Console.WriteLine("-- Commands Pages");
            
            foreach (string CommandPage in CommandsPage)
            {
                if (Path.GetFileName(CommandPage).StartsWith("T_")) // solo las paginas que inician con T_nos interesan para el procesarmientio
                {
                    //TODO: IN PROGRESS, refactor, muchas paginas modificadas que no son necesarias
                    //Si el DIV summary existe en doc, quiere decir que falta xml comments
                    var htmlDocument = DocumentHelper.GetInstance();
                    htmlDocument._documentPath = CommandPage;
                    htmlDocument.Load();
                    DivHelper CommandDivHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "summary");
                    TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, string.Empty, "titleTable");

                    if (CommandDivHelper.Exists())
                    {
                        commandsToPrint.Add(tableHelper.GetCellDisplayValue("titleColumn"), CommandDivHelper.GetInnerHtml());
                    }

                    factoryHtml.cleanInputOutputPages(CommandPage);
                    factoryHtml.preparePropertiesTable(CommandPage);
                }
            }
            Console.WriteLine("-- Done....Exporting to Excel");
            Console.WriteLine("");
            missingScanHelper.GetCommandsSource(commandsToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrive);
            //end

            //Events
            CommandsPage.Clear();
            CommandsPage.AddRange(Directory.EnumerateFiles(moduleSetting.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html, "*_Contracts_Events_*" + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Console.WriteLine("-- Events Pages");
            
            foreach (string page in CommandsPage)
            {
                if (Path.GetFileName(page).StartsWith("T_")) // solo las paginas que inician con T_nos interesan para el procesarmientio
                {
                    //TODO: IN PROGRESS, refactor, muchas paginas modificadas que no son necesarias
                    //Si el DIV summary existe en doc, quiere decir que falta xml comments
                    var htmlDocument = DocumentHelper.GetInstance();
                    htmlDocument._documentPath = page;
                    htmlDocument.Load();
                    DivHelper EventDivHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "summary");
                    TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, string.Empty, "titleTable");

                    if (EventDivHelper.Exists())
                    {
                        EventsToPrint.Add(tableHelper.GetCellDisplayValue("titleColumn"), EventDivHelper.GetInnerHtml());
                    }

                    factoryHtml.cleanInputOutputPages(page);
                    factoryHtml.preparePropertiesTable(page);
                }
            }

            Console.WriteLine("-- Done....Exporting to Excel");
            Console.WriteLine("");
            missingScanHelper.GetEventsSource(EventsToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrive);
            //end

            //query parameters
            CommandsPage.Clear();
            CommandsPage.AddRange(Directory.EnumerateFiles(moduleSetting.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html, "*_Contracts_Queries_Parameters_*" + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Console.WriteLine("-- Query Pages");
            
            foreach (string page in CommandsPage)
            {
                if (Path.GetFileName(page).StartsWith("T_")) // solo las paginas que inician con T_nos interesan para el procesarmientio
                {
                    //TODO: IN PROGRESS, refactor, muchas paginas modificadas que no son necesarias
                    //Si el DIV summary existe en doc, quiere decir que falta xml comments
                    var htmlDocument = DocumentHelper.GetInstance();
                    htmlDocument._documentPath = page;
                    htmlDocument.Load();
                    DivHelper QueryDivHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "summary");
                    TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, string.Empty, "titleTable");

                    if (QueryDivHelper.Exists())
                    {
                        QueryToPrint.Add(tableHelper.GetCellDisplayValue("titleColumn"), QueryDivHelper.GetInnerHtml());
                    }

                    factoryHtml.cleanInputOutputPages(page);
                    factoryHtml.preparePropertiesTable(page);
                }
            }
            Console.WriteLine("-- Done....Exporting to Excel");
            Console.WriteLine("");
            missingScanHelper.GetQuerySource(QueryToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrive);
            //end

            //domain
            CommandsPage.Clear();
            CommandsPage.AddRange(Directory.EnumerateFiles(moduleSetting.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html, "*_Contracts_Domain_*" + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Console.WriteLine("-- Data Models Pages");
            
            foreach (string page in CommandsPage)
            {
                if (Path.GetFileName(page).StartsWith("T_")) // solo las paginas que inician con T_nos interesan para el procesarmientio
                {
                    //TODO: IN PROGRESS, refactor, muchas paginas modificadas que no son necesarias
                    //Si el DIV summary existe en doc, quiere decir que falta xml comments
                    var htmlDocument = DocumentHelper.GetInstance();
                    htmlDocument._documentPath = page;
                    htmlDocument.Load();
                    DivHelper ModelDivHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "summary");
                    TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, string.Empty, "titleTable");

                    if (ModelDivHelper.Exists())
                    {
                        ModelsToPrint.Add(tableHelper.GetCellDisplayValue("titleColumn"), ModelDivHelper.GetInnerHtml());
                    }

                    factoryHtml.cleanInputOutputPages(page);
                    factoryHtml.preparePropertiesTable(page);
                }
            }

            Console.WriteLine("-- Done....Exporting to Excel");
            Console.WriteLine("");
            missingScanHelper.GetModelsSource(ModelsToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrive);
            //end

            //dto
            CommandsPage.Clear();
            CommandsPage.AddRange(Directory.EnumerateFiles(moduleSetting.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html, "*_Contracts_ViewDto_*" + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Console.WriteLine("-- View DTO Pages");

            foreach (string page in CommandsPage)
            {
                if (Path.GetFileName(page).StartsWith("T_")) // solo las paginas que inician con T_nos interesan para el procesarmientio
                {
                    //TODO: IN PROGRESS, refactor, muchas paginas modificadas que no son necesarias
                    //Si el DIV summary existe en doc, quiere decir que falta xml comments
                    var htmlDocument = DocumentHelper.GetInstance();
                    htmlDocument._documentPath = page;
                    htmlDocument.Load();
                    DivHelper DTODivHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "summary");
                    TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, string.Empty, "titleTable");

                    if (DTODivHelper.Exists())
                    {
                        DtosToPrint.Add(tableHelper.GetCellDisplayValue("titleColumn"), DTODivHelper.GetInnerHtml());
                    }

                    factoryHtml.cleanInputOutputPages(page);
                    factoryHtml.preparePropertiesTable(page);
                }
            }
            Console.WriteLine("-- Done Exporting to Excel");
            Console.WriteLine("");
            missingScanHelper.GetDTOSource(DtosToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrive);

            //cleanup
            DtosToPrint.Clear();
            CommandsPage.Clear();
            EventsToPrint.Clear();
            ModelsToPrint.Clear();
            QueryToPrint.Clear();
            missingScanHelper = null;

            #endregion

            Console.WriteLine("Adding input and output to the service operations table");
            
            #region Adding input and output to the service operations table
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.UpdateInputOutput(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
            }
            Console.WriteLine(".... Done");
            Console.WriteLine("");
            #endregion

            Console.WriteLine("Preparing and cleaning service pages");
            
            #region Preparing and cleaning service pages
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.removeHiperLinks(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
            }

            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.removeText(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
            }
            Console.WriteLine(".... Done");
            Console.WriteLine("");
            #endregion

            Console.WriteLine("Adding Pagination control to service pages");
            
            #region Adding Pagination control to service pages
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.AddPaginationControl(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
            }
            Console.WriteLine(".... Done");
            Console.WriteLine("");
            #endregion

            Console.WriteLine("Preparing and cleaning service list pages");
           
            #region Preparing and cleaning service list pages
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.removeTextFromServices(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
            }
            Console.WriteLine(".... Done");
            Console.WriteLine("");
            #endregion

            Console.WriteLine("Removing empty elements and tags");
            
            #region removing empty elements and tags
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.RemoveEmptyParams(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
            }
            Console.WriteLine(".... Done");
            Console.WriteLine("");
            #endregion

            Console.WriteLine("");
            Console.WriteLine("Preparing Event View Landing Page");
            #region Adding Event View
            factoryHtml.AddEventView();
            #endregion

            Console.WriteLine("");
            Console.WriteLine("Adding Pagination control to event page");
            #region Adding Pagination control to service pages
            factoryHtml.setPaginationControl(moduleSetting.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + moduleSetting.MainContractContent, "classList", "namespacesSection");
            Console.WriteLine(".... Done");
            Console.WriteLine("");
            #endregion

            Console.WriteLine("Executing SandCastle Enhancement Process done");

        }
    }
}
