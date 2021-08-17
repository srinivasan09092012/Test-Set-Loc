using Common.Information;
using Common.Interfaces;
using Common.ModuleSettings;
using Controller.Helpers.HTML;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Controller
{
    public class AddHtmlBlocks
    {
        private readonly IExecutionContext ExecutionContext;
        private readonly ILogger LoggerEngine;
        public ModuleSettingModel ModuleSettings { get; set; }

        public AddHtmlBlocks()
        {

        }
        public AddHtmlBlocks(ILogger loggerEngineInstance, IExecutionContext exeContext)
        {
            LoggerEngine = loggerEngineInstance;
            ExecutionContext = exeContext;
        }

        public void AddPaginationControl(string originalDocument)
        {
            HtmlFactory HtmlFactory = new HtmlFactory();
            string fullSourcePath = ExecutionContext.getTargetPath() + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();

            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
            List<string> services = tblHelper.readColumnValues(1);

            foreach (string service in services)
            {
                HtmlFactory.setPaginationControl(ExecutionContext.getTargetPath() + Common.Constants.WebSolutionStructure.Folders.Html +
                                                                    @service.Remove(0, 9).Split('>')[0].Split('.')[0] +
                                                                    Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, "methodList", "ID4RBSection");
            }
        }


        public void addDataTypeColumn(HtmlDocument doc)
        {
            TableHelper tableHelper = new TableHelper(doc, TableHelper.SearchFilter.Id, "propertyList");
            tableHelper.addColumn("Data Type");
        }

        public void AddStickyLayout(string htmlPage)
        {
            string comments = string.Empty;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = htmlPage;
            htmlDocument.Load();

            //body
            var bodyNode = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//body").FirstOrDefault();

            //take title from table
            TableHelper tblTitle = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Class, "titleTable");
            var title = tblTitle.readTdDisplayValueByClass("titleColumn");
            tblTitle.Remove();

            //header for stcky content 
            HtmlNode headerDiv = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, 0);
            headerDiv.Name = "div";
            headerDiv.Id = "topHeader";
            headerDiv.Attributes.Add("class", "top-Header");

            //mid-header for stcky content 
            HtmlNode midHeaderDiv = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, 1);
            midHeaderDiv.Name = "div";
            midHeaderDiv.Id = "midHeader";
            midHeaderDiv.Attributes.Add("class", "mid-Header");

            //adding header
            DivHelper topicContentDiv = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "TopicContent");
            topicContentDiv._ContextDiv.RemoveClass("pageBody");

            headerDiv.InnerHtml = "<h1>" + title + "</h1>";
            topicContentDiv._ContextDiv.ChildNodes.Insert(0, headerDiv);

            //adding mid-header
            topicContentDiv._ContextDiv.ChildNodes.Insert(1, midHeaderDiv);

            DivHelper PropTableContentDiv = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "ID4RBSection");
            PropTableContentDiv.AddAttribute("class", "Content");

            //adding references to css and js
            var headNode = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//head").FirstOrDefault();

            HtmlNode scriptNode = NodeHelper.GetNode(htmlDocument._loadedDocument);
            scriptNode.Name = "script";
            scriptNode.Attributes.Add("type", "text/javascript");
            scriptNode.Attributes.Add("src", "stickyLayout.js");
            headNode.ChildNodes.Add(scriptNode);

            var StyleRefNode = NodeHelper.GetNode(htmlDocument._loadedDocument);
            StyleRefNode.Name = "link";
            StyleRefNode.Attributes.Add("rel", "stylesheet");
            StyleRefNode.Attributes.Add("href", "stickyLayout.css");

            var MetaNode = NodeHelper.GetNode(htmlDocument._loadedDocument);
            MetaNode.Name = "meta";
            MetaNode.Attributes.Add("content", "with=device-with, initial-scale=1");

            headNode.ChildNodes.Add(scriptNode);
            headNode.ChildNodes.Add(StyleRefNode);
            headNode.ChildNodes.Add(MetaNode);

            //removing body attrib
            bodyNode.Attributes.Remove("onload");
            var inputNodes = bodyNode.ChildNodes.Where(x => x.Name == "input");
            if (inputNodes != null)
            {
                inputNodes.FirstOrDefault().Remove();
            }

            DivHelper pageBodyDiv = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "pageBody");
            pageBodyDiv._ContextDiv.RemoveClass("pageBody");

            htmlDocument.Save();
        }

        public void addBreadCrumbsControl(string htmlPage, string breadcrumbText)
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = htmlPage;
            htmlDocument.Load();

            DivHelper divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "summary");
            HtmlNode breadCrumbsList = NodeHelper.GetNode(htmlDocument._loadedDocument);
            breadCrumbsList.Name = "ol";
            breadCrumbsList.Id = "BreadCrumbsEventPage";
            breadCrumbsList.AddClass("breadcrumb");

            HtmlNode breadCrumbsListDefaultItem = NodeHelper.GetNode(htmlDocument._loadedDocument);
            breadCrumbsListDefaultItem.Name = "li";
            breadCrumbsListDefaultItem.AddClass("breadcrumb-item");
            breadCrumbsListDefaultItem.AddClass("active");
            breadCrumbsListDefaultItem.InnerHtml = breadcrumbText;
            breadCrumbsList.ChildNodes.Add(breadCrumbsListDefaultItem);

            var headNode = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//head").FirstOrDefault();

            HtmlNode node = NodeHelper.GetNode(htmlDocument._loadedDocument);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/jquery-3.4.0.min.js");
            headNode.ChildNodes.Add(node);
            node = NodeHelper.GetNode(htmlDocument._loadedDocument);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/popper.min.js");
            headNode.ChildNodes.Add(node);
            node = NodeHelper.GetNode(htmlDocument._loadedDocument);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/bootstrap.min.js");
            headNode.ChildNodes.Add(node);
            node = NodeHelper.GetNode(htmlDocument._loadedDocument);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/addons/datatables.min.js");
            headNode.ChildNodes.Add(node);
            node = NodeHelper.GetNode(htmlDocument._loadedDocument);
            node.Name = "link";
            node.Attributes.Add("rel", "stylesheet");
            node.Attributes.Add("href", "../css/bootstrap.min.css");
            headNode.ChildNodes.Add(node);
            node = NodeHelper.GetNode(htmlDocument._loadedDocument);
            node.Name = "link";
            node.Attributes.Add("rel", "stylesheet");
            node.Attributes.Add("href", "https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css");
            headNode.ChildNodes.Add(node);

            node = NodeHelper.GetNode(htmlDocument._loadedDocument);
            node.Name = "script";
            node.InnerHtml = "$(document).ready(function(){try{InitializeBreadCrumbs({DisplayName:'" + breadcrumbText + "',TargetUrl:'" + ModuleSettings.WebHost + @"\" + ModuleSettings.WebRoutingTargetPath.Replace(ModuleSettings.WebHostPhysicalPath, string.Empty) + @"\" + Common.Constants.WebSolutionStructure.Folders.Html + @"\" + htmlDocument._documentTitle + "',IsActive: false});}catch(err){alert(err.message);}});";  //"$(document).ready(function(){try{InitializeBreadCrumbs({DisplayName:'Main Event Attributes',TargetUrl:'http://localhost:8080/ProviderManagement/APISeviceSpecification/html/T_HP_HSP_UA3_ProviderManagement_BAS_Providers_Contracts_Events_ServiceLocationAdded.htm',IsActive: false});}catch(err){alert(err.message);}});";
            headNode.ChildNodes.Add(node);

            node = NodeHelper.GetNode(htmlDocument._loadedDocument);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "ComplexEventNavigator.js");
            headNode.ChildNodes.Add(node);
            divHelper.InsertChildrenNode(breadCrumbsList);

            htmlDocument.Save();
        }
    }
}
