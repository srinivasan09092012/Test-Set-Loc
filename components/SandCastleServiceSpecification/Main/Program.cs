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
            var moduleSetting = validateInputArgs(args);
            Console.WriteLine("Loading settings... Done");
            Console.WriteLine("");
            Console.WriteLine("");

            foreach (var module in moduleSetting)
            {
                Console.WriteLine("Runnig Settings for Module: " + module.ModuleName);
                Console.WriteLine("");
                DoBackup(module);
                ExecuteSandCastleEnhancement(module);// improve for parallel programing
            }
        }

        static List<Common.DocModuleSettingModel> validateInputArgs(string[] args)
        {
            string storageDrive = ConfigurationManager.AppSettings["storageDrive"];
            var sDrive = new DirectoryInfo(storageDrive);
            var xs = new XmlSerializer(typeof(DocModuleSettingModel));
            var moduleSetting = new DocModuleSettingModel();
            var moduleSeetingList = new List<DocModuleSettingModel>();

            if (sDrive.Exists)
            {
                var ModuleSettingList = sDrive.EnumerateFiles("*.xml", SearchOption.TopDirectoryOnly);

                foreach (FileInfo file in ModuleSettingList)
                {
                    using (var sr = new StreamReader(file.FullName))
                    {
                        moduleSetting = (DocModuleSettingModel)xs.Deserialize(sr);
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

        static void DoBackup(DocModuleSettingModel moduleSetting)
        {
            #region backupsite
            Backup bkp = new Backup(moduleSetting.WebSourcePath, moduleSetting.WebTargetPath);
            bkp.BackUpWebSite();
            bkp.copyFile(Environment.CurrentDirectory + Common.Constant.WebSolutionStructure.Folders.img + Common.Constant.WebSolutionStructure.Files.UnderConstructionIcon, moduleSetting.WebTargetPath + Common.Constant.WebSolutionStructure.Folders.Icons + Common.Constant.WebSolutionStructure.Files.UnderConstructionIcon);
            #endregion
        }

        static void ExecuteSandCastleEnhancement(DocModuleSettingModel moduleSetting)
        {
            Console.WriteLine("Executing SandCastle Customization Process");
            Console.WriteLine("");

            #region variable declarations and initializations
            HtmlFactory factoryHtml = new HtmlFactory();
            factoryHtml.ModuleSttings = moduleSetting;
            List<string> serviceUrls = new List<string>();
            List<string> serviceList = new List<string>();
            ScanMissingTagsHelper missingScanHelper = new ScanMissingTagsHelper();
            Dictionary<string, string> commandsToPrint = new Dictionary<string, string>();
            Dictionary<string, string> EventsToPrint = new Dictionary<string, string>();
            Dictionary<string, string> ModelsToPrint = new Dictionary<string, string>();
            Dictionary<string, string> QueryToPrint = new Dictionary<string, string>();
            Dictionary<string, string> DtosToPrint = new Dictionary<string, string>();
            #endregion

            Console.WriteLine("Preparing Landing Page");
            Console.WriteLine(""); //TODO: para modulos como managed care, el landing page no esta funcionando, por que los servicios estan en varias contentPages, se necesita arreglar para indicar una como home page
            #region prepare Landing Page
            factoryHtml.updateIndexFile(Common.Constant.WebSolutionStructure.Files.IndexPage, moduleSetting.MainPageContent);

            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.PrepareServicePage(Common.Constant.WebSolutionStructure.Folders.Html + ServicePage);
            }

            #endregion

            Console.WriteLine("Preparing Service Operation List Pages");
            Console.WriteLine("");
            #region Preparing Service Operation List Pages
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.prepareServiceOperationList(Common.Constant.WebSolutionStructure.Folders.Html + ServicePage);
            }

            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.UpdateALLTableList(Common.Constant.WebSolutionStructure.Folders.Html + ServicePage);
            }
            #endregion

            Console.WriteLine("Updating Left Navigator");
            Console.WriteLine("");
            #region Updating Left Navigator 
            //TODO:  All the pages have the left navigator by default, there is a good chance to have the navigator in the main page only and setup a cointainer to present each page, to achive this, i will have to remove the left navigator from each single page.
            //this is not affecting on MainPageContent, tags not founds - serviceList.AddRange(factoryHtml.getServiceListFromPageContent(Common.Constant.WebSolutionStructure.Folders.Html + moduleSetting.MainPageContent));

            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                serviceList.AddRange(factoryHtml.getServiceListFromPageContent(Common.Constant.WebSolutionStructure.Folders.Html + ServicePage));
            }

            serviceUrls = factoryHtml.extractServiceUrl(serviceList);

            foreach (string url in serviceUrls)
            {
                factoryHtml.UpdateLeftNavigator(url, serviceList); //TODO: refactor to the call and usage of Pages param, each left navigator in page is being updated with the links to all service urls, need refactor 
            }

            factoryHtml.UpdateLeftNavigator(moduleSetting.WebTargetPath + Constant.WebSolutionStructure.Folders.Html + moduleSetting.MainPageContent, serviceList);
            #endregion

            Console.WriteLine("Preparing Documentation Pages:");
            Console.WriteLine("");
            # region Preparing Command and Event Pages

            //TODO: NECESITAMOS UN ENUMARE FILE CUSTOM POR QUE ESTE TOMA MUICHO TIEMPO LEVANTAR LOS ARCHIVOS DE DISCO.
            //Commands
            serviceList.Clear();
            serviceList.AddRange(Directory.EnumerateFiles(moduleSetting.WebTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, "*_Contracts_Commands_*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Console.WriteLine("-- Commands Pages");
            
            foreach (string page in serviceList)
            {
                if (Path.GetFileName(page).StartsWith("T_")) // solo las paginas que inician con T_nos interesan para el procesarmientio
                {
                    //TODO: IN PROGRESS, refactor, muchas paginas modificadas que no son necesarias
                    //Si el DIV summary existe en doc, quiere decir que falta xml comments
                    var htmlDocument = DocumentHelper.GetInstance();
                    htmlDocument._documentPath = page;
                    htmlDocument.Load();
                    DivHelper CommandDivHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "summary");
                    TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, string.Empty, "titleTable");

                    if (CommandDivHelper.Exists())
                    {
                        commandsToPrint.Add(tableHelper.GetCellDisplayValue("titleColumn"), string.Empty);
                    }

                    factoryHtml.UpdateInputOutputPages(page);
                    factoryHtml.addDataTypeColumnToCommandPages(page);
                }
            }
            Console.WriteLine("-- Done....Exporting to Excel");
            Console.WriteLine("");
            missingScanHelper.GetCommandsSource(commandsToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrive);
            //end

            //Events
            serviceList.Clear();
            serviceList.AddRange(Directory.EnumerateFiles(moduleSetting.WebTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, "*_Contracts_Events_*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Console.WriteLine("-- Events Pages");
            
            foreach (string page in serviceList)
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
                        EventsToPrint.Add(tableHelper.GetCellDisplayValue("titleColumn"), string.Empty);
                    }

                    factoryHtml.UpdateInputOutputPages(page);
                    factoryHtml.addDataTypeColumnToCommandPages(page);
                }
            }

            Console.WriteLine("-- Done....Exporting to Excel");
            Console.WriteLine("");
            missingScanHelper.GetEventsSource(EventsToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrive);
            //end

            //query parameters
            serviceList.Clear();
            serviceList.AddRange(Directory.EnumerateFiles(moduleSetting.WebTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, "*_Contracts_Queries_Parameters_*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Console.WriteLine("-- Query Pages");
            
            foreach (string page in serviceList)
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
                        QueryToPrint.Add(tableHelper.GetCellDisplayValue("titleColumn"), string.Empty);
                    }

                    factoryHtml.UpdateInputOutputPages(page);
                    factoryHtml.addDataTypeColumnToCommandPages(page);
                }
            }
            Console.WriteLine("-- Done....Exporting to Excel");
            Console.WriteLine("");
            missingScanHelper.GetQuerySource(QueryToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrive);
            //end

            //domain
            serviceList.Clear();
            serviceList.AddRange(Directory.EnumerateFiles(moduleSetting.WebTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, "*_Contracts_Domain_*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Console.WriteLine("-- Data Models Pages");
            
            foreach (string page in serviceList)
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
                        ModelsToPrint.Add(tableHelper.GetCellDisplayValue("titleColumn"), string.Empty);
                    }

                    factoryHtml.UpdateInputOutputPages(page);
                    factoryHtml.addDataTypeColumnToCommandPages(page);
                }
            }

            Console.WriteLine("-- Done....Exporting to Excel");
            Console.WriteLine("");
            missingScanHelper.GetModelsSource(ModelsToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrive);

            //end

            //dto
            serviceList.Clear();
            serviceList.AddRange(Directory.EnumerateFiles(moduleSetting.WebTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, "*_Contracts_ViewDto_*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Console.WriteLine("-- View DTO Pages");

            foreach (string page in serviceList)
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
                        DtosToPrint.Add(tableHelper.GetCellDisplayValue("titleColumn"), string.Empty);
                    }

                    factoryHtml.UpdateInputOutputPages(page);
                    factoryHtml.addDataTypeColumnToCommandPages(page);
                }
            }
            Console.WriteLine("-- Done Exporting to Excel");
            Console.WriteLine("");
            missingScanHelper.GetDTOSource(DtosToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrive);

            //cleanup
            DtosToPrint.Clear();
            serviceList.Clear();
            EventsToPrint.Clear();
            ModelsToPrint.Clear();
            QueryToPrint.Clear();
            missingScanHelper = null;

            #endregion

            Console.WriteLine("Adding input and output to the service operations table");
            
            #region Adding input and output to the service operations table
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.UpdateInputOutput(Common.Constant.WebSolutionStructure.Folders.Html + ServicePage);
            }
            Console.WriteLine(".... Done");
            Console.WriteLine("");
            #endregion

            Console.WriteLine("Preparing and cleaning service pages");
            
            #region Preparing and cleaning service pages
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.removeHiperLinks(Common.Constant.WebSolutionStructure.Folders.Html + ServicePage);
            }

            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.removeText(Common.Constant.WebSolutionStructure.Folders.Html + ServicePage);
            }
            Console.WriteLine(".... Done");
            Console.WriteLine("");
            #endregion

            Console.WriteLine("Adding Pagination control to service pages");
            
            #region Adding Pagination control to service pages
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.AddPaginationControl(Common.Constant.WebSolutionStructure.Folders.Html + ServicePage);
            }
            Console.WriteLine(".... Done");
            Console.WriteLine("");
            #endregion

            Console.WriteLine("Preparing and cleaning service list pages");
           
            #region Preparing and cleaning service list pages
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.removeTextFromServices(Common.Constant.WebSolutionStructure.Folders.Html + ServicePage);
            }
            Console.WriteLine(".... Done");
            Console.WriteLine("");
            #endregion

            Console.WriteLine("Removing empty elements and tags");
            
            #region removing empty elements and tags
            foreach (var ServicePage in moduleSetting.ServiceListPages.ServiceListPage)
            {
                factoryHtml.RemoveEmptyParams(Common.Constant.WebSolutionStructure.Folders.Html + ServicePage);
            }
            Console.WriteLine(".... Done");
            Console.WriteLine("");
            #endregion

            Console.WriteLine("Executing SandCastle Enhancement Process done");
            
        }
    }
}
