using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using Common;
using APISvcSpec.Helpers;
using APISvcSpec.IO;


namespace SandCastleSvcSpec
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***********************************");
            Console.WriteLine("SandCastle Service Enhancement Tool");
            Console.WriteLine("***********************************");
            Console.WriteLine("Loading settings");
            DocModuleSettingModel moduleSetting = validateInputArgs(args);
            DoBackup(moduleSetting);
            ExecuteSandCastleEnhancement(moduleSetting);
        }

        static Common.DocModuleSettingModel validateInputArgs(string[] args)
        {
            string storageDrive = ConfigurationManager.AppSettings["storageDrive"];
            DirectoryInfo sDrive = new DirectoryInfo(storageDrive);
            XmlSerializer xs;
            Common.DocModuleSettingModel moduleSetting = new Common.DocModuleSettingModel();

            if (args.Length <= 0)
                throw new Exception("Missing Module name parameter, process will stop");

            if(string.IsNullOrEmpty(args[0]))
                throw new Exception("Module parameter cant be null or empty, process will stop");

            string ModuleName = args[0];

            if (File.Exists(sDrive + @"\" + (ModuleName + ".xml")))
            {
                xs = new XmlSerializer(typeof(DocModuleSettingModel));
                using (var sr = new StreamReader(sDrive + @"\" + ModuleName + ".xml"))
                {
                    moduleSetting = (DocModuleSettingModel)xs.Deserialize(sr);
                }

                return moduleSetting;
            }
            else
            {
                throw new Exception("information to produce documentation not found for the module " + ModuleName);
            }
        }

        static void DoBackup(DocModuleSettingModel moduleSetting)
        {
            #region backupsite
            Backup bkp = new Backup(moduleSetting.webSourcePath, moduleSetting.webTargetPath);
            bkp.BackUpWebSite();
            bkp.copyFile(Environment.CurrentDirectory + Common.Constant.WebSolutionStructure.Folders.img + Common.Constant.WebSolutionStructure.Files.UnderConstructionIcon, moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Icons + Common.Constant.WebSolutionStructure.Files.UnderConstructionIcon);
            #endregion
        }

        static void ExecuteSandCastleEnhancement(DocModuleSettingModel moduleSetting)
        {
            List<string> PagesToUpdateLeftNavigator = new List<string>();

            #region prepare Landing Page
                APISvcSpec.HtmlFactory.updateIndexFile(moduleSetting.webTargetPath, Common.Constant.WebSolutionStructure.Files.IndexPage, "/" + moduleSetting.MainPageContent);
                APISvcSpec.HtmlFactory.UpdateLandingPage(moduleSetting.webTargetPath, Common.Constant.WebSolutionStructure.Folders.Html + "/" + moduleSetting.MainPageContent);
            #endregion

            #region update T page
            APISvcSpec.HtmlFactory.UpdateALLOperationListPage(moduleSetting.webTargetPath, Common.Constant.WebSolutionStructure.Folders.Html + "/" + moduleSetting.MainPageContent, moduleSetting.ModuleName);
            APISvcSpec.HtmlFactory.UpdateALLTableList(moduleSetting.webTargetPath, Common.Constant.WebSolutionStructure.Folders.Html + "/" + moduleSetting.MainPageContent);
            #endregion

            #region UpdateLeftNavigator
                List<string> PagesFormated = new List<string>();
                List<string> Pages = new List<string>();
                Pages = APISvcSpec.HtmlFactory.getServices(moduleSetting.webTargetPath, Common.Constant.WebSolutionStructure.Folders.Html + "/" + moduleSetting.MainPageContent, moduleSetting.ModuleName);
                PagesFormated = APISvcSpec.HtmlFactory.FormatServices(moduleSetting.webTargetPath, Pages);
                PagesToUpdateLeftNavigator.AddRange(PagesFormated);
                PagesToUpdateLeftNavigator.Add(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html + "/" + moduleSetting.MainPageContent);
                foreach (string page in PagesToUpdateLeftNavigator)
                {
                    APISvcSpec.HtmlFactory.UpdateLeftNavigator(page, moduleSetting.ModuleName, moduleSetting.MainPageContent, Pages);
                }
            #endregion

            Pages.Clear();
            //Pages.AddRange(Directory.EnumerateFiles(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, "T_*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Pages.AddRange(Directory.EnumerateFiles(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, "*_Contracts_Commands*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Pages.AddRange(Directory.EnumerateFiles(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, "*_Contracts_Events*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Pages.AddRange(Directory.EnumerateFiles(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, "*_Contracts_Queries_Parameters_*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
            Pages.AddRange(Directory.EnumerateFiles(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, "*_Contracts_Domain_*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));

            foreach (string page in Pages)
            {
               
               APISvcSpec.HtmlFactory.UpdateCMDEventsPages(page, moduleSetting.ModuleName, moduleSetting.MainPageContent, Pages);
                
            }


            APISvcSpec.HtmlFactory.UpdateInputOutput(moduleSetting.webTargetPath, Common.Constant.WebSolutionStructure.Folders.Html + "/" + moduleSetting.MainPageContent);

            APISvcSpec.HtmlFactory.RemoveALLHiperlinks(moduleSetting.webTargetPath, Common.Constant.WebSolutionStructure.Folders.Html + "/" + moduleSetting.MainPageContent);

            APISvcSpec.HtmlFactory.AddPaginationControl(moduleSetting.webTargetPath, Common.Constant.WebSolutionStructure.Folders.Html + "/" + moduleSetting.MainPageContent);

            List<string> allFiles = new List<string>();

            //allFiles.AddRange(Directory.EnumerateFiles(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, Common.Constant.WebSolutionStructure.Files.StartWithTokenCommandClassesIOServices + "*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));

            allFiles.AddRange(Directory.EnumerateFiles(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, Path.GetFileNameWithoutExtension(moduleSetting.MainPageContent.Replace("N_","T_")) + Common.Constant.WebSolutionStructure.Files.StartWithTokenCommandClassesIOServices +"*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));

            //allFiles.AddRange(Directory.EnumerateFiles(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, Common.Constant.WebSolutionStructure.Files.StartWithTokenEventsIOServices + "*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));

            allFiles.AddRange(Directory.EnumerateFiles(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, Path.GetFileNameWithoutExtension(moduleSetting.MainPageContent.Replace("N_", "T_")) + Common.Constant.WebSolutionStructure.Files.StartWithTokenEventsIOServices + "*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));

            allFiles.AddRange(Directory.EnumerateFiles(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, Path.GetFileNameWithoutExtension(moduleSetting.MainPageContent.Replace("N_", "T_")) + Common.Constant.WebSolutionStructure.Files.StartWithTokenQueryParams + "*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));

            allFiles.AddRange(Directory.EnumerateFiles(moduleSetting.webTargetPath + Common.Constant.WebSolutionStructure.Folders.Html, Path.GetFileNameWithoutExtension(moduleSetting.MainPageContent.Replace("N_", "T_")) + Common.Constant.WebSolutionStructure.Files.StartWithTokenQueryDomain + "*" + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));

            foreach (string file in allFiles)
            {
                
                APISvcSpec.HtmlFactory.addDataTypeColumnToCommandPages(moduleSetting.webTargetPath, file);
            }

            APISvcSpec.HtmlFactory.removeText(moduleSetting.webTargetPath, Common.Constant.WebSolutionStructure.Folders.Html + "/" + moduleSetting.MainPageContent);

            APISvcSpec.HtmlFactory.removeTextFromServices(moduleSetting.webTargetPath, Common.Constant.WebSolutionStructure.Folders.Html + "/" + moduleSetting.MainPageContent);

            APISvcSpec.HtmlFactory.RemoveEmptyParams(moduleSetting.webTargetPath, Common.Constant.WebSolutionStructure.Folders.Html + "/" + moduleSetting.MainPageContent);
            

        }
    }
}
