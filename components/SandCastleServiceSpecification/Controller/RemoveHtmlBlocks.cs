using Common.Information;
using Common.Interfaces;
using Common.ModuleSettings;
using Controller.Helpers.HTML;
using Controller.Helpers.Scan;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class RemoveHtmlBlocks
    {
        private readonly IExecutionContext ExecutionContext;
        private readonly ILogger LoggerEngine;
        public ModuleSettingModel ModuleSettings { get; set; }

        public RemoveHtmlBlocks()
        {

        }
        public RemoveHtmlBlocks(ILogger loggerEngineInstance, IExecutionContext exeContext)
        {
            LoggerEngine = loggerEngineInstance;
            ExecutionContext = exeContext;
        }

        public void RemoveHiperLinks(string pageContent)
        {
            string fullSourcePath = ExecutionContext.getTargetPath() + pageContent;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
            List<string> pagesExposingServices = tblHelper.readColumnValues(1);

            foreach (string servicePage in pagesExposingServices)
            {
                removeTableCellHiperLink(ExecutionContext.getTargetPath() + Common.Constants.WebSolutionStructure.Folders.Html +
                                                                            servicePage.Remove(0, 9).Split('>')[0].Split('.')[0] +
                                                                            Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
            }
        }

        public void RemoveEmptyParams(string originalDocument)
        {
            string fullSourcePath = ModuleSettings.WebTargetPath + originalDocument;

            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();

            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
            List<string> services = tblHelper.readColumnValues(1);

            foreach (string service in services)
            {
                removeEmptyTags(ModuleSettings.WebTargetPath, Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, "p");
                removeEmptyTags(ModuleSettings.WebTargetPath, Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, "br");
                removeEmptyTags(ModuleSettings.WebTargetPath, Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, "div");
            }
        }

        public void removeIconColumn(HtmlDocument doc)
        {
            TableHelper tableHelper = new TableHelper(doc, TableHelper.SearchFilter.Id, "propertyList");
            tableHelper.removeColumn(0);
            TableHelper tableHelper2 = new TableHelper(doc, TableHelper.SearchFilter.Id, "operatorList");
            tableHelper2.removeColumn(0);
        }


        public void removeHiperLinkNameColumn(HtmlDocument htmlDoc)
        {
            //update with data type value
            HtmlNode newNode = NodeHelper.GetNode(htmlDoc);
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            TableHelper tableHelper = new TableHelper(htmlDoc, TableHelper.SearchFilter.Id, "propertyList");
            List<string> files = tableHelper.readColumnValues(1);
            int x = 1;
            foreach (var file in files)
            {
                newNode.InnerHtml = file;
                tableHelper.SetCellDisplayValue(1, x, newNode.InnerText);
                x++;
            }
        }


        public void removeEmptyTags(string webFolderTarget, string originalDocument, string tagName)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();

            var bodyTag = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//body").FirstOrDefault();

            if (bodyTag.HasChildNodes)
            {
                removeTag(bodyTag, tagName);
            }

            htmlDocument.Save();
        }

        public bool removeTag(HtmlNode node, string tagName)
        {
            if (node.Name == tagName && node.InnerHtml.Trim() == string.Empty)
            {
                node.Remove();
                return true;
            }
            else if (node.HasChildNodes)
            {
                foreach (var child in node.ChildNodes)
                {
                    if (removeTag(child, tagName))
                        break;
                }
            }

            return false;
        }

        public void removeText(string originalDocument)
        {
            string fullSourcePath = ModuleSettings.WebTargetPath + originalDocument;
            TextHelper txtHelper;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();

            // remove top link
            txtHelper = new TextHelper(htmlDocument._loadedDocument, "exposes the following members.");
            txtHelper.removeTagByTextClass("topicContent");
            txtHelper = new TextHelper(htmlDocument._loadedDocument, "Top");
            txtHelper.removeTagByTextClass("collapsibleSection");

            // remove search form
            DivHelper dhelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "PageHeader");
            dhelper.removeChildByName("form");

            htmlDocument.Save();
        }

        public void removeTextFromServices(string originalDocument)
        {
            string fullSourcePath = ModuleSettings.WebTargetPath + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();

            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
            List<string> services = tblHelper.readColumnValues(1);

            foreach (string service in services)
            {
                removeText(Common.Constants.WebSolutionStructure.Folders.Html + "/" + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
            }
        }

        /// <summary>
        /// removeTableCellHiperLink
        /// Remove hiperlink from the service name column, keeping the name only as a display value
        /// </summary>
        /// <param name="ServicePage">Receive a URL to a peg that hold services operations</param>
        /// <returns>void</returns>
        public void removeTableCellHiperLink(string ServicePage) //TODO: This may be a table helper method, refactoring this to remove link from any given column.
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = ServicePage;
            htmlDocument.Load();
            HtmlNode tmpColumnHolderNode = NodeHelper.GetNode(htmlDocument._loadedDocument);
            TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "methodList");

            int x = 1;
            foreach (var serviceNameColumn in tableHelper.readColumnValues(0))
            {
                tmpColumnHolderNode.InnerHtml = serviceNameColumn;
                tableHelper.SetCellDisplayValue(0, x, tmpColumnHolderNode.InnerText);
                x++;
            }

            htmlDocument.Save();

            htmlDocument.Load();
            tableHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "methodList");
            TableHelper tableTitleHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Class, "titleTable");

            MissingTagsHelper scanHelper = new MissingTagsHelper(LoggerEngine);
            scanHelper.GetServicesSource(tableHelper.ReadAllColumnsValues(), ModuleSettings.ModuleName, tableTitleHelper.GetInnerText(), ModuleSettings.StorageDrivePath);
            scanHelper = null;
        }

        /// <summary>
        /// UpdateInputOutputPages
        /// Remove and clean unwanted html tags for events, commands, queryParams, Models and DTO
        /// </summary>
        /// <returns>void</returns>
        public void cleanInputOutputPages(string htmlPage)
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = htmlPage;
            htmlDocument.Load();
            UpdateHtmlBlocks UpdateHtmlBlocks = new UpdateHtmlBlocks();
            UpdateHtmlBlocks.ModuleSettings = ModuleSettings;
            UpdateHtmlBlocks.UpdateBodyPage(htmlDocument._loadedDocument, "div", htmlPage, ModuleSettings.ModuleName, ModuleSettings.MainPageContent);
            htmlDocument.Save();

            removeEmptyTags(ModuleSettings.WebTargetPath, Common.Constants.WebSolutionStructure.Folders.Html + htmlDocument._documentTitle, "div");
            removeEmptyTags(ModuleSettings.WebTargetPath, Common.Constants.WebSolutionStructure.Folders.Html + htmlDocument._documentTitle, "p");
            removeEmptyTags(ModuleSettings.WebTargetPath, Common.Constants.WebSolutionStructure.Folders.Html + htmlDocument._documentTitle, "br");
        }
    }
}
