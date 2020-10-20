using Controller;
using Controller.Helpers.HTML;
using Controller.Helpers.Scan;
using Controller.IO;
using Common;
using Common.Implementations;
using Common.Interfaces;
using Common.ModuleSettings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Common.ExtensionMethods;

namespace SandCastleSvcSpec
{
  class Program
  {
    //details events can queried in case of and error shown on general error log
    static ILogger loggerDetailEngine = new EventViewerLogger("SandCastle Customization Tool", "SandCastle Services Specification Enhancements");

    private delegate bool validateSettingFile(ModuleSettingModel parsedSettingFile);

    static void Main(string[] args)
    {
      Console.WriteLine("************************************");
      Console.WriteLine("   SandCastle Customization Tool");
      Console.WriteLine("************************************");
      Console.WriteLine("");
      Console.WriteLine("");
      loggerDetailEngine.writeEntry("SandCastle Customization Tool started, loading settings...\n\nThis may take a while depending on the number of modules requested to create documentation.", LogginSeetings.LevelType.InformationApplication, 1041, 1);

      string moduleSettingFilesStoragePath = ConfigurationManager.AppSettings["moduleSettingFilesStoragePath"];
      var moduleSetting = SearchingModuleSettings(moduleSettingFilesStoragePath);

      // maybe with context patter will be easy to introduce this since now there is no way to track from this point all the modules and their process result
      foreach (var module in moduleSetting)
      {
        Console.WriteLine("");
        Console.WriteLine("**********************************************************");
        loggerDetailEngine.writeEntry(string.Format("Running Settings for Module: {0} \n\nCopying new target files, waiting for two checkpoints before process create documentation. ", module.ModuleNameDisplay), LogginSeetings.LevelType.InformationApplication, 9000, 9);
        Console.WriteLine("**********************************************************");

        try
        {
          ExecuteSandCastleEnhancement(module);// improve for parallel programing
          loggerDetailEngine.writeEntry(string.Format("Web style documentation created successfuly for \n\nModule: {0} \n\nIn the local path: {1} \n\nDocumentation now available online. ", module.ModuleNameDisplay, module.WebTargetPath), LogginSeetings.LevelType.InformationApplication, 2181, 2);
        }
        catch (Exception exe)
        {
          loggerDetailEngine.writeEntry(string.Format("Unexpected exception ocurred for \n\nModule: {0} \n\nIn the local path: {1} \n\nError Message: {2} ", module.ModuleNameDisplay, module.WebTargetPath, exe.Message), LogginSeetings.LevelType.ErrorApplication, 2014, 2);
        }
      }

      Console.WriteLine("");
      loggerDetailEngine.writeEntry("SandCastle Customization Tool execution completed", LogginSeetings.LevelType.InformationApplication, 1042, 1);
    }

    static List<ModuleSettingModel> SearchingModuleSettings(string moduleSettingFilesStoragePath)
    {
      DirectoryInfo sDrive = null;
      var moduleSetting = new ModuleSettingModel();
      var moduleSeetingList = new List<ModuleSettingModel>();

      string storageDrive = moduleSettingFilesStoragePath;

      try
      {
        if (string.IsNullOrEmpty(storageDrive))
        {
          throw new Exception("\nSetting key: [moduleSettingFilesStoragePath] not found in Application Setting File, create and set this key to the xml settings folder");
        }

        sDrive = new DirectoryInfo(storageDrive);

        if (!sDrive.Exists)
        {
          throw new Exception("\nPath not found in local system " + sDrive.FullName);
        }
      }
      catch (Exception exe)
      {
        Console.WriteLine("");
        loggerDetailEngine.writeEntry("Error to locate module settings folder: " + exe.Message, LogginSeetings.LevelType.ErrorApplication, 1013, 1);
        return moduleSeetingList;
      }

      var ModuleSettingList = sDrive.GetFiles("*" + Constants.WebSolutionStructure.Files.Extensions.xmlExtension, SearchOption.TopDirectoryOnly);

      if (ModuleSettingList.Length == 0)
      {
        Console.WriteLine("");
        loggerDetailEngine.writeEntry("Aborting process 0 setting files found in folder " + sDrive.FullName, LogginSeetings.LevelType.WarningApplication, 2021, 2);
        Console.WriteLine();
        return moduleSeetingList;
      }

      Console.WriteLine("");
      loggerDetailEngine.writeEntry(string.Format("Search completed, {0} files found in folder {1} \nValidating each one", ModuleSettingList.Length, sDrive.FullName), LogginSeetings.LevelType.InformationApplication, 9000, 9);

      byte validSettingFiles = 0;

      foreach (FileInfo file in ModuleSettingList)
      {
        moduleSetting = readSettingFile(file.FullName);

        if (IsValidSettingFile(moduleSetting, file.FullName))
        {
          moduleSeetingList.Add(moduleSetting);
          validSettingFiles++;
        }
      }

      Console.WriteLine("");

      if (validSettingFiles != 0)
      {
        Console.WriteLine("");
        loggerDetailEngine.writeEntry(string.Format("Validation completed, running process for {0} file(s) out of {1} \n\nProcess will generate web style documentation for the modules:\n{2}", validSettingFiles, ModuleSettingList.Length, moduleSeetingList.PrintComaSeparated()), LogginSeetings.LevelType.InformationApplication, 9000, 9);
      }
      else
      {
        Console.WriteLine("");
        loggerDetailEngine.writeEntry(string.Format("No valid files were parsed to generate documentation \nProcess will stop."), LogginSeetings.LevelType.WarningApplication, 2022, 2);
      }

      return moduleSeetingList; // this will be removed when parallel programing is in placee
                                /*Once you start running processes in parallel, 
                                 * you start to have to track other considerations like how many processors / cores you have, 
                                 * how many tasks you want to try to run in parallel, how to make your variables thread safe, 
                                 * and how to track the progress and results of your processes.*/
    }

    private

    static ModuleSettingModel readSettingFile(string settingFilePath)
    {
      var xs = new XmlSerializer(typeof(ModuleSettingModel));

      using (var sr = new StreamReader(settingFilePath))
      {
        try
        {
          return (ModuleSettingModel)xs.Deserialize(sr);
        }
        catch (Exception exe)
        {
          loggerDetailEngine.writeEntry(string.Format("Skipping the setting file {0}, XML structure error found: {1} \n", Path.GetFileName(settingFilePath), exe.Message), LogginSeetings.LevelType.FailureAudit, 2163, 2);
          return null;
        }
      }
    }

    static bool IsValidSettingFile(ModuleSettingModel parsedSettingFile, string parsedSettingFilePath)
    {
      //use this delegate when validations are on
      validateSettingFile fullValidation = delegate (ModuleSettingModel settingFile)
      {
        Type objType = typeof(ModuleSettingModel);
              //validation stage 1: No nulls or empty on string type fields
              var membersInfo = objType.GetFields().Where(x => x.FieldType.FullName.Equals("System.String"));

        foreach (var member in membersInfo)
        {
          var propertyInfo = objType.GetField(member.Name);

          if (string.IsNullOrEmpty((string)propertyInfo.GetValue(settingFile)))
          {
            loggerDetailEngine.writeEntry(string.Format("Skipping the setting file {0} \nEmpty node '{1}' ", Path.GetFileName(parsedSettingFilePath), member.Name), LogginSeetings.LevelType.FailureAudit, 2163, 2);
            return false;
          }

          var propertyValue = (string)propertyInfo.GetValue(settingFile);

                //stage 1: all paths must exist
                if (propertyInfo.Name.EndsWith("Path") ? (propertyValue.IsPathValid() ? false : true) : false)
          {
            loggerDetailEngine.writeEntry(string.Format("Skipping the setting file {0} \nPath not found '{1}' ", Path.GetFileName(parsedSettingFilePath), propertyValue), LogginSeetings.LevelType.FailureAudit, 2163, 2);
            return false;
          }
        }

              //validation stage 2: No nulls or empty on inner fields
              var innersMembersInfo = objType.GetFields().Where(x => x.FieldType.FullName.StartsWith("Common.ModuleSettings"));

        foreach (var member in innersMembersInfo)
        {
          var propertyInfo = objType.GetField(member.Name);

          if (((ContentPage)propertyInfo.GetValue(settingFile))?.ListPage.Count == null ? true : ((ContentPage)propertyInfo.GetValue(settingFile)).ListPage.Count == 0)
          {
            loggerDetailEngine.writeEntry(string.Format("Skipping the setting file {0} \nEmpty node '{1}' ", Path.GetFileName(parsedSettingFilePath), propertyInfo.FieldType.Name), LogginSeetings.LevelType.FailureAudit, 2163, 2);
            return false;
          }

          foreach (string item in ((ContentPage)propertyInfo.GetValue(settingFile)).ListPage)
          {
            if (string.IsNullOrEmpty(item))
            {
              loggerDetailEngine.writeEntry(string.Format("Skipping the setting file {0} \nEmpty list provided in the closing node </ListPage>.", Path.GetFileName(parsedSettingFilePath)), LogginSeetings.LevelType.FailureAudit, 2163, 2);
              return false;
            }
          }
        }
        return true;
      };

      //use this delegate when validations are off
      validateSettingFile noValidation = delegate (ModuleSettingModel settingFile) { return true; };

      return parsedSettingFile == null ? false : ((Convert.ToBoolean(ConfigurationManager.AppSettings["fullValidationOnSettingFile"]) ? fullValidation(parsedSettingFile) : noValidation(parsedSettingFile)));
    }

    static void DoBackup(string WebSourcePath, string WebTargetPath)
    {
      #region backupsite
      BackupHelper bkp = new BackupHelper(WebSourcePath, WebTargetPath, loggerDetailEngine);
      bkp.CopyTargetSite();
      #endregion
    }

    public static Dictionary<string, string> PreparePages(string searchFilePattern, ModuleSettingModel moduleSetting)
    {
      HtmlFactory factoryHtml = new HtmlFactory(loggerDetailEngine);
      factoryHtml.ModuleSettings = moduleSetting;
      List<string> serviceUrls = new List<string>();
      List<string> Pages = new List<string>();
      Pages.AddRange(Directory.EnumerateFiles(moduleSetting.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html, searchFilePattern + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, SearchOption.TopDirectoryOnly));
      Dictionary<string, string> artifactsToPrint = new Dictionary<string, string>();
      foreach (string page in Pages)
      {
        var htmlDocument = DocumentHelper.GetInstance();
        htmlDocument._documentPath = page;
        htmlDocument.Load();

        DivHelper multiusedRegion = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "collapsibleAreaRegion");
        DivHelper SummaryDivHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "summary");
        TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Class, "titleTable");

        multiusedRegion.RemoveCollectionMatch();

        if (SummaryDivHelper.GetInnerHtml().Contains("[Missing "))
        {
          SummaryDivHelper.Remove();
          if (!artifactsToPrint.ContainsKey(tableHelper.GetInnerText()))
          {
            artifactsToPrint.Add(tableHelper.GetInnerText(), SummaryDivHelper.GetInnerHtml());
            SummaryDivHelper.Remove();
          }
        }

        htmlDocument.Save();

        factoryHtml.cleanInputOutputPages(page);
        factoryHtml.preparePropertiesTable(page);

        if (page.Contains("_Commands_"))
        {
          DivHelper seeAlso = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "ID7RBSection");
          seeAlso.Remove();
          htmlDocument.Save();
        }

        if (page.Contains("_Events_"))
        {
          factoryHtml.addBreadCrumbsControl(page);
          factoryHtml.removeEmptyTags(moduleSetting.WebTargetPath, Common.Constants.WebSolutionStructure.Folders.Html + Path.GetFileName(page), "div");
          factoryHtml.AddStickyLayout(page);
          factoryHtml.CreateOnclickAttribute(htmlDocument);
          factoryHtml.CreateHtmlBlocks(page);
        }

        if (page.Contains("_Contracts_ValueObjects_") || page.Contains("_Contracts_Shared_"))
        {
          factoryHtml.CreateHtmlBlocks(page);
        }

      }

      return artifactsToPrint;
    }

    static void ExecuteSandCastleEnhancement(ModuleSettingModel moduleSetting)
    {
      #region variable declarations and initializations
      HtmlFactory factoryHtml;
      ExecutionContext ctx;
      List<string> ContentPages = new List<string>();
      List<string> apiContentPages = new List<string>();
      MissingTagsHelper missingScanHelper = new MissingTagsHelper(loggerDetailEngine);
      Dictionary<string, string> commandsToPrint = new Dictionary<string, string>();
      Dictionary<string, string> EventsToPrint = new Dictionary<string, string>();
      Dictionary<string, string> ModelsToPrint = new Dictionary<string, string>();
      Dictionary<string, string> QueryToPrint = new Dictionary<string, string>();
      Dictionary<string, string> DtosToPrint = new Dictionary<string, string>();
      #endregion

      #region ModuleHelpContentAvailable
      if (moduleSetting.ModuleHelpContentAvailable)
      {

        SetHtmlPageLanguage(moduleSetting);

        loggerDetailEngine.writeEntry("Generating help content documentation", LogginSeetings.LevelType.InformationApplication, 9000, 9);

        ctx = new ExecutionContext(ExecutionContext.ExecutionStages.HelpContent, moduleSetting);
        factoryHtml = new HtmlFactory(loggerDetailEngine, ctx);
        factoryHtml.ModuleSettings = moduleSetting;

        loggerDetailEngine.writeEntry("Preparing Services Landing Page");
        #region prepare Landing Page
        factoryHtml.prepareIndexFile(Common.Constants.WebSolutionStructure.Files.IndexPage, moduleSetting.MainPageContent);
        #endregion

        loggerDetailEngine.writeEntry("Preparing operations tables and adding pagination controls");

        #region Preparing Service Operation List Pages
        foreach (var ServicePage in moduleSetting.ServiceListPages.ListPage)
        {
          factoryHtml.UpdateOperationsLists(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
          factoryHtml.UpdateInputOutput(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
          factoryHtml.removeHiperLinks(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
          factoryHtml.AddPaginationControl(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
        }
        #endregion

        loggerDetailEngine.writeEntry("Preparing Command and Event Pages:");

        #region Preparing Command and Event Pages
        loggerDetailEngine.writeEntry("-- Commands Pages");
        commandsToPrint = PreparePages("T_*_Contracts_Commands_*", moduleSetting);
        missingScanHelper.GetCommandsSource(commandsToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrivePath);

        loggerDetailEngine.writeEntry("-- Events Pages");
        EventsToPrint = PreparePages("T_*_Contracts_Events_*", moduleSetting);
        missingScanHelper.GetEventsSource(EventsToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrivePath);

        loggerDetailEngine.writeEntry("-- Query Pages");
        QueryToPrint = PreparePages("T_*_Contracts_Queries_Parameters_*", moduleSetting);
        missingScanHelper.GetQuerySource(QueryToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrivePath);

        loggerDetailEngine.writeEntry("-- Data Models Pages");
        ModelsToPrint = PreparePages("T_*_Contracts_Domain_*", moduleSetting);
        missingScanHelper.GetModelsSource(ModelsToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrivePath);

        loggerDetailEngine.writeEntry("-- View DTO Pages");
        DtosToPrint = PreparePages("T_*_Contracts_ViewDto_*", moduleSetting);
        missingScanHelper.GetDTOSource(DtosToPrint, moduleSetting.ModuleName, moduleSetting.StorageDrivePath);

        loggerDetailEngine.writeEntry("-- Value Objects");
        PreparePages("T_*_Contracts_ValueObjects_*", moduleSetting);

        loggerDetailEngine.writeEntry("-- Shared Objects");
        PreparePages("T_*_Contracts_Shared_*", moduleSetting);

        loggerDetailEngine.writeEntry(".... Done");

        #endregion

        loggerDetailEngine.writeEntry("Removing empty elements and tags");
        #region removing empty elements and tags
        foreach (var ServicePage in moduleSetting.ServiceListPages.ListPage)
        {
          factoryHtml.removeTextFromServices(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
          factoryHtml.removeText(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
          factoryHtml.RemoveEmptyParams(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
        }

        loggerDetailEngine.writeEntry(".... Done");
        #endregion

        loggerDetailEngine.writeEntry("Preparing Event View Landing Page");
        #region Adding Event View
        factoryHtml.prepareEventPage();
        #endregion

        loggerDetailEngine.writeEntry("Adding Pagination control to event page");
        #region Adding Pagination control to service pages
        factoryHtml.setPaginationControl(moduleSetting.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + moduleSetting.MainContractContent, "classList", "namespacesSection");

        Console.WriteLine(".... Done");
        Console.WriteLine("");
        #endregion
      }
      #endregion

      #region routine for API
      // claims management
      // PaymentCapitalization
      ctx = new ExecutionContext(ExecutionContext.ExecutionStages.APIContent, moduleSetting);
      factoryHtml = new HtmlFactory(loggerDetailEngine, ctx);
      factoryHtml.ModuleSettings = moduleSetting;

      if (ctx.ModuleSetting.ModuleAPIAvailable)
      {
        DoBackup(ctx.getSourcePath(), ctx.getTargetPath());

        Console.WriteLine("");
        Console.WriteLine("Preparing operations tables and adding pagination controls");
        #region Preparing Service Operation List Pages
        foreach (var ServicePage in moduleSetting.ModuleAPIPages.ListPage)
        {
          factoryHtml.UpdateOperationsLists(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
          factoryHtml.UpdateInputOutput(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
          factoryHtml.removeHiperLinks(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage);
        }


        factoryHtml.setPaginationControl(ctx.getTargetPath() + Common.Constants.WebSolutionStructure.Folders.Html + moduleSetting.MainAPIContent, "methodList", "ID5RBSection");
        #endregion

        var strTmp = moduleSetting.WebTargetPath;
        moduleSetting.WebTargetPath = moduleSetting.ModuleAPITargetPath;
        PreparePages("T_HPE_HSP_UA3_*Request", moduleSetting);
        PreparePages("T_HPE_HSP_UA3_*Response", moduleSetting);

        PreparePages("T_HPP_HSP_UA3_*Request", moduleSetting);
        PreparePages("T_HPP_HSP_UA3_*Response", moduleSetting);

        Console.WriteLine("");
        Console.WriteLine("Preparing Command and Event Pages vARIANT 2:");

        Console.WriteLine("");
        Console.WriteLine("-- Commands Pages");
        commandsToPrint = PreparePages("T_*_Commands_*", moduleSetting);

        Console.WriteLine("-- Events Pages");
        EventsToPrint = PreparePages("T_*_Events_*", moduleSetting);

        Console.WriteLine("-- Queries");
        EventsToPrint = PreparePages("T_*_Queries_*", moduleSetting);

        Console.WriteLine("-- Queries");
        EventsToPrint = PreparePages("T_*_Domain_*", moduleSetting);

        moduleSetting.WebTargetPath = strTmp;
      }
      #endregion

      #region Updating Left Navigator 
      Console.WriteLine("");
      Console.WriteLine("Updating Left Navigator for all the content");
      foreach (var ServicePage in moduleSetting.ServiceListPages.ListPage)
      {
        ContentPages.AddRange(factoryHtml.getServiceListFromPageContent(Common.Constants.WebSolutionStructure.Folders.Html + ServicePage, moduleSetting.WebTargetPath));
      }

      foreach (var apiServicePage in moduleSetting.ModuleAPIPages.ListPage)
      {
        apiContentPages.AddRange(factoryHtml.getServiceListFromPageContent(Common.Constants.WebSolutionStructure.Folders.Html + apiServicePage, moduleSetting.ModuleAPITargetPath));
      }

      var targetPath = string.IsNullOrEmpty(moduleSetting.WebTargetPath) ? moduleSetting.ModuleAPITargetPath : moduleSetting.WebTargetPath;
      var mainContent = string.IsNullOrEmpty(moduleSetting.MainPageContent) ? moduleSetting.MainAPIContent : moduleSetting.MainPageContent;


      factoryHtml.UpdateContentLeftNavigator(targetPath + Constants.WebSolutionStructure.Folders.Html + mainContent, ContentPages, mainContent, apiContentPages);
      #endregion


    }

    static void SetHtmlPageLanguage(ModuleSettingModel model)
    {
      loggerDetailEngine.writeEntry("Adding language atribute to all html pages:");

      var files = Directory.GetFiles(model.WebSourcePath, "*.htm").ToList();
      var folders = Directory.GetDirectories(model.WebSourcePath).ToList();

      foreach (var folder in folders)
      {
        files.AddRange(Directory.GetFiles(folder, "*.htm").Distinct().ToList());
      }

      var totFiles = files.Count();
      int cnt = 0;

      loggerDetailEngine.writeEntry($"{totFiles} html files to modify.");

      foreach (var file in files)
      {
        var fname = new FileInfo(file).Name;
        cnt++;
        var html = File.ReadAllText(file).Replace("<html>", "<html lang=\"en-US\">"); 
        File.WriteAllText(file, html);
        loggerDetailEngine.writeEntry($"{fname} modified");
      }
    }
  }
}
