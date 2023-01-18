using Common.Information;
using Common.Interfaces;
using Common.ModuleSettings;
using Controller.Helpers.HTML;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Controller
{
    public class UpdateHtmlBlocks
    {
        private readonly IExecutionContext ExecutionContext;
        private readonly ILogger LoggerEngine;
        public ModuleSettingModel ModuleSettings { get; set; }
        public InformationModel InformationModel { get; set; }

        public UpdateHtmlBlocks()
        {

        }
        public UpdateHtmlBlocks(ILogger loggerEngineInstance, IExecutionContext exeContext)
        {
            LoggerEngine = loggerEngineInstance;
            ExecutionContext = exeContext;
        }

        public void updateServiceList(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            HtmlFactory.FormatServiceDocument(htmlDocument);
            updateServiceList(htmlDocument._loadedDocument, webFolderTarget);
            htmlDocument.Save();
        }

        public void updateServiceList(HtmlDocument htmlMainDoc, string webFolderTarget)
        {
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            HtmlFactory HtmlFactory = new HtmlFactory();
            TableHelper tableHelper = new TableHelper(htmlMainDoc, TableHelper.SearchFilter.Id, "methodList");
            List<string> r = tableHelper.readColumnValues(0);
            int x = 1;
            int j;

            r = r.Where(row => row.Contains("href")).ToList();

            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                HtmlFactory.formatHtmlDataList((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID0RBSection");
                tableHelper.SetCellDisplayValue(2, x, divHelper.GetChildValueByTag("a", ModuleSettings.WebHost, ModuleSettings.WebHostPhysicalPath, ModuleSettings.WebRoutingTargetPath));
                x++;
            }

            x = 1;
            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID1RBSection");
                tableHelper.SetCellDisplayValue(3, x, divHelper.GetChildValueByTag("a", ModuleSettings.WebHost, ModuleSettings.WebHostPhysicalPath, ModuleSettings.WebRoutingTargetPath));
                x++;
            }
        }


        public void updateServiceListForQuery(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            HtmlFactory.FormatServiceDocument(htmlDocument);
            updateServiceListForQuery(htmlDocument._loadedDocument, webFolderTarget);
            htmlDocument.Save();
        }

        public void updateServiceListForQuery(HtmlDocument doc, string webFolderTarget)
        {
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            HtmlDocument innerHtmlDocLvl2 = new HtmlDocument();
            HtmlDocument innerHtmlDocLvl3 = new HtmlDocument();
            TableHelper tableHelper = new TableHelper(doc, TableHelper.SearchFilter.Id, "methodList");
            TableHelper tblHelperlevl2;
            HtmlNode myNode;
            List<HtmlNode> myNodeList = new List<HtmlNode>();
            List<string> r = tableHelper.readColumnValues(0);
            int x = 1;
            int j;

            r = r.Where(row => row.Contains("href")).ToList();

            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID1RBSection");
                DataListHelper dlhlp = new DataListHelper(innerHtmlDoc);
                var formatedList = dlhlp.formatDescriptionList("ID1RBSection");
                string linkfilelevel2 = string.Empty;
                HtmlNode hrefNewNode = NodeHelper.GetNode(doc);

                if (formatedList != null)
                {
                    var arefTag = formatedList.Where(e => e.Name == "a");
                    if (arefTag != null && arefTag.Count() > 0)
                    {
                        var attributesHrefColl = arefTag.FirstOrDefault().Attributes.Where(i => i.Name == "href");
                        if (attributesHrefColl != null && attributesHrefColl.Count() > 0)
                        {
                            linkfilelevel2 = attributesHrefColl.FirstOrDefault().Value;
                            hrefNewNode.InnerHtml = arefTag.FirstOrDefault().InnerHtml;
                        }
                    }
                }

                if (linkfilelevel2.Contains("Query"))
                {
                    if (linkfilelevel2 != string.Empty)
                    {
                        innerHtmlDocLvl2.Load((webFolderTarget + @"\html\" + linkfilelevel2));
                        tblHelperlevl2 = new TableHelper(innerHtmlDocLvl2, TableHelper.SearchFilter.Id, "propertyList");

                        if (tblHelperlevl2._ts != null)
                        {
                            string tempstr = string.Empty;
                            string linkfilelvl3 = string.Empty;
                            linkfilelevel2 = divHelper.GetChildNodeOuterHtml("a");
                            List<string> linkColumns = tblHelperlevl2.readColumnValues(1);
                            foreach (string col in linkColumns)
                            {
                                myNode = NodeHelper.GetNode(doc);
                                myNode.Name = "p";
                                myNode.InnerHtml = col;
                                if (myNode.InnerText == "Where")
                                {
                                    tempstr = myNode.InnerHtml;
                                    break;
                                }
                            }
                            myNode = null;

                            linkfilelvl3 = tempstr;

                            if (linkfilelvl3 != string.Empty)
                            {
                                j = (webFolderTarget + @"html\" + linkfilelvl3.Remove(0, 9).Split('>')[0]).Length;
                                innerHtmlDocLvl3.Load((webFolderTarget + @"\html\" + linkfilelvl3.Remove(0, 9).Split('>')[0]).Substring(0, j));
                                divHelper = new DivHelper(innerHtmlDocLvl3, DivHelper.SearchFilter.Id, "ID1RBSection");
                                string linkfilelvl4 = divHelper.GetChildValueByTag("a", ModuleSettings.WebHost, ModuleSettings.WebHostPhysicalPath, ModuleSettings.WebRoutingTargetPath);
                                tableHelper.SetCellDisplayValue(2, x, linkfilelvl4);
                            }
                            else
                            {
                                tableHelper.SetCellDisplayValue(2, x, "This query receives no parameters");
                            }


                        }
                    }
                    else
                    {
                        tableHelper.SetCellDisplayValue(2, x, "This query receives no parameters");
                    }
                }
                else
                {
                    //request object
                    hrefNewNode.Name = "a";
                    hrefNewNode.Attributes.Add("href", "#");
                    hrefNewNode.Attributes.Add("onClick", "window.open('" + ModuleSettings.WebHost + @"\" + ModuleSettings.WebRoutingTargetPath.Replace(ModuleSettings.WebHostPhysicalPath, string.Empty) + @"\" + Common.Constants.WebSolutionStructure.Folders.Html + @"\" + linkfilelevel2 + "', 'MyWindow','width=800,height=450,toolbar=no,menubar=no,status=no,resizable=yes,scrollbars=yes'); return false;");
                    tableHelper.SetCellDisplayValue(2, x, hrefNewNode.OuterHtml);
                }

                x++;
            }

            x = 1;
            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID1RBSection");
                string linkfilelevel2 = divHelper.GetChildNodeOuterHtml("a");
                var link = divHelper.GetChildValueByTag("a", ModuleSettings.WebHost, ModuleSettings.WebHostPhysicalPath, ExecutionContext.getRoutingTargetPath());

                if (link.Contains("Query"))
                {
                    if (linkfilelevel2 != string.Empty)
                    {
                        j = (webFolderTarget + @"html\" + linkfilelevel2.Remove(0, 9).Split('>')[0]).Length;
                        innerHtmlDocLvl2.Load((webFolderTarget + @"\html\" + linkfilelevel2.Remove(0, 9).Split('>')[0]).Substring(0, j));
                        divHelper = new DivHelper(innerHtmlDocLvl2, DivHelper.SearchFilter.Id, "ID0RBSection");
                        tableHelper.SetCellDisplayValue(3, x, divHelper.GetChildValueByTag("a", ModuleSettings.WebHost, ModuleSettings.WebHostPhysicalPath, ExecutionContext.getRoutingTargetPath()));
                    }
                    else
                    {
                        tableHelper.SetCellDisplayValue(3, x, "Return type unknow");
                    }
                }
                else
                {
                    tableHelper.SetCellDisplayValue(3, x, "List<" + link + ">");
                }

                x++;
            }
        }

        public void UpdateOperationsLists(string originalDocument)
        {
            string fullSourcePath = ExecutionContext.getTargetPath() + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();

            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
            List<string> services = tblHelper.readColumnValues(1);
            foreach (string service in services)
            {
                if (ExecutionContext.getExecutionStage() == 2)
                {
                    this.UpdateQueryMethodsTableList(ExecutionContext.getTargetPath() + Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                }
                else
                {
                    if (service.Contains("Query") || service.Contains("Report"))
                    {
                        this.UpdateQueryMethodsTableList(ExecutionContext.getTargetPath() + Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                    }
                    else
                    {
                        this.UpdateMethodsTableList(ExecutionContext.getTargetPath() + Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                    }
                }
            }
        }

        public void UpdateInputOutput(string originalDocument)
        {
            string fullSourcePath = ExecutionContext.getTargetPath() + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();

            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
            List<string> services = tblHelper.readColumnValues(1);

            foreach (string service in services)
            {
                if (ExecutionContext.getExecutionStage() == 2)
                {
                    this.updateServiceListForQuery(ExecutionContext.getTargetPath(), Common.Constants.WebSolutionStructure.Folders.Html +
                                                                                                    @service.Remove(0, 9).Split('>')[0].Split('.')[0] +
                                                                                                    Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                }
                else
                {
                    if (service.Contains("Query"))
                    {
                        this.updateServiceListForQuery(ExecutionContext.getTargetPath(), Common.Constants.WebSolutionStructure.Folders.Html +
                                                                                                    @service.Remove(0, 9).Split('>')[0].Split('.')[0] +
                                                                                                    Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                    }
                    else
                    {
                        this.updateServiceList(ExecutionContext.getTargetPath(), Common.Constants.WebSolutionStructure.Folders.Html +
                                                                                            @service.Remove(0, 9).Split('>')[0].Split('.')[0] +
                                                                                            Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                    }
                }
            }
        }

        public void UpdateContentLeftNavigator(string urlPage, List<string> ServiceList, string lvl0HomePage, List<string> apiContentList)
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = urlPage;
            htmlDocument.Load();

            #region declarations
            Dictionary<string, string> ServicesMenuEntry = new Dictionary<string, string>();
            Dictionary<string, string> apiMenuEntry = new Dictionary<string, string>();

            HtmlNode ModuleHomeDivForMenuEntryTocLevel0;
            HtmlNode ModuleHomeDivForMenuEntryTocLevel1;

            HtmlNode ModuleHomeLinkedMenuEntry;
            HtmlNode ModuleHomeRootMenuEntry;

            HtmlNode EventsDivForMenuEntryTocLevel0;
            HtmlNode EventsDivForMenuEntryTocLevel1;

            HtmlNode EventsLinkedMenuEntry;
            HtmlNode EventsRootMenuEntry;

            HtmlNode OtherResourcesRootMenuEntry;
            HtmlNode OtherResourcesLinkedMenuEntry;
            HtmlNode OtherResourcesAPILinkedMenuEntry;
            HtmlNode OtherResourcesPortalLinkedMenuEntry;

            HtmlNode ServicesDivForMenuEntryTocLevel0;
            HtmlNode ServicesDivForMenuEntryTocLevel1;

            HtmlNode OtherResourcesDivForMenuEntryTocLevel0;
            HtmlNode OtherResourcesDivForMenuEntryTocLevel1;
            HtmlNode OtherResourcesDivForMenuEntryTocLevel2;

            HtmlNode apiDivForMenuEntryTocLevel0;
            HtmlNode apiDivForMenuEntryTocLevel1;

            HtmlNode apiLinkedMenuEntry;
            HtmlNode apiRootMenuEntry;

            HtmlNode OtherResourcesDivForApiMenuEntryTocLevel1;

            HtmlNode ServicesLinkedMenuEntry;
            HtmlNode ServicesRootMenuEntry;

            //Linked items in menu, located below of a not linked root item
            ModuleHomeLinkedMenuEntry = NodeHelper.GetNode(htmlDocument._loadedDocument);
            //Root items in menu, used to group linked items such: Events and Web Services.
            ModuleHomeRootMenuEntry = NodeHelper.GetNode(htmlDocument._loadedDocument);

            //Linked items in menu, located below of a not linked root item
            EventsLinkedMenuEntry = NodeHelper.GetNode(htmlDocument._loadedDocument);
            //Root items in menu, used to group linked items such: Events and Web Services.
            EventsRootMenuEntry = NodeHelper.GetNode(htmlDocument._loadedDocument);

            OtherResourcesRootMenuEntry = NodeHelper.GetNode(htmlDocument._loadedDocument);

            //Used to organize the menu identation levels, hold all divs
            DivHelper divHelperTocNav = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "tocNav");

            ServicesLinkedMenuEntry = NodeHelper.GetNode(htmlDocument._loadedDocument);

            OtherResourcesLinkedMenuEntry = NodeHelper.GetNode(htmlDocument._loadedDocument);

            OtherResourcesPortalLinkedMenuEntry = NodeHelper.GetNode(htmlDocument._loadedDocument);


            OtherResourcesAPILinkedMenuEntry = NodeHelper.GetNode(htmlDocument._loadedDocument);

            ServicesRootMenuEntry = NodeHelper.GetNode(htmlDocument._loadedDocument);

            apiRootMenuEntry = NodeHelper.GetNode(htmlDocument._loadedDocument);

            EventsDivForMenuEntryTocLevel0 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            EventsDivForMenuEntryTocLevel0.Name = "div";
            EventsDivForMenuEntryTocLevel0.AddClass("toclevel1");
            EventsDivForMenuEntryTocLevel0.Attributes.Add("data-toclevel", "0");

            EventsDivForMenuEntryTocLevel1 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            EventsDivForMenuEntryTocLevel1.Name = "div";
            EventsDivForMenuEntryTocLevel1.AddClass("toclevel2");
            EventsDivForMenuEntryTocLevel1.Attributes.Add("data-toclevel", "1");

            ServicesDivForMenuEntryTocLevel0 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            ServicesDivForMenuEntryTocLevel0.Name = "div";
            ServicesDivForMenuEntryTocLevel0.AddClass("toclevel2");
            ServicesDivForMenuEntryTocLevel0.Attributes.Add("data-toclevel", "0");

            ServicesDivForMenuEntryTocLevel1 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            ServicesDivForMenuEntryTocLevel1.Name = "div";
            ServicesDivForMenuEntryTocLevel1.AddClass("toclevel3");
            ServicesDivForMenuEntryTocLevel1.Attributes.Add("data-toclevel", "1");


            apiDivForMenuEntryTocLevel1 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            apiDivForMenuEntryTocLevel1.Name = "div";
            apiDivForMenuEntryTocLevel1.AddClass("toclevel3");
            apiDivForMenuEntryTocLevel1.Attributes.Add("data-toclevel", "1");

            OtherResourcesDivForMenuEntryTocLevel0 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            OtherResourcesDivForMenuEntryTocLevel0.Name = "div";
            OtherResourcesDivForMenuEntryTocLevel0.AddClass("toclevel2");
            OtherResourcesDivForMenuEntryTocLevel0.Attributes.Add("data-toclevel", "0");

            OtherResourcesDivForMenuEntryTocLevel1 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            OtherResourcesDivForMenuEntryTocLevel1.Name = "div";
            OtherResourcesDivForMenuEntryTocLevel1.AddClass("toclevel3");
            OtherResourcesDivForMenuEntryTocLevel1.Attributes.Add("data-toclevel", "1");

            OtherResourcesDivForMenuEntryTocLevel2 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            OtherResourcesDivForMenuEntryTocLevel2.Name = "div";
            OtherResourcesDivForMenuEntryTocLevel2.AddClass("toclevel3");
            OtherResourcesDivForMenuEntryTocLevel2.Attributes.Add("data-toclevel", "1");

            OtherResourcesDivForApiMenuEntryTocLevel1 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            OtherResourcesDivForApiMenuEntryTocLevel1.Name = "div";
            OtherResourcesDivForApiMenuEntryTocLevel1.AddClass("toclevel3");
            OtherResourcesDivForApiMenuEntryTocLevel1.Attributes.Add("data-toclevel", "1");

            ModuleHomeDivForMenuEntryTocLevel0 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            ModuleHomeDivForMenuEntryTocLevel0.Name = "div";
            ModuleHomeDivForMenuEntryTocLevel0.AddClass("toclevel1");
            ModuleHomeDivForMenuEntryTocLevel0.Attributes.Add("data-toclevel", "0");

            ModuleHomeDivForMenuEntryTocLevel1 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            ModuleHomeDivForMenuEntryTocLevel1.Name = "div";
            ModuleHomeDivForMenuEntryTocLevel1.AddClass("toclevel1");
            ModuleHomeDivForMenuEntryTocLevel1.Attributes.Add("data-toclevel", "1");

            apiDivForMenuEntryTocLevel0 = NodeHelper.GetNode(htmlDocument._loadedDocument);
            apiDivForMenuEntryTocLevel0.Name = "div";
            apiDivForMenuEntryTocLevel0.AddClass("toclevel2");
            apiDivForMenuEntryTocLevel0.Attributes.Add("data-toclevel", "0");

            //Clear the default menu provided by default sandcastle engine.
            divHelperTocNav.SetInnerHtml(string.Empty);
            #endregion

            #region page setup
            if (Path.GetFileName(urlPage) != lvl0HomePage)
            {
                DivHelper divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "TopicContent");
                divHelper.removeStyleClass("topicContent");
            }

            if (Path.GetFileName(urlPage) == lvl0HomePage)
            {
                #region add links to head tag
                var divNodes = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//head").FirstOrDefault();
                HtmlNode node = NodeHelper.GetNode(htmlDocument._loadedDocument);
                node.Name = "link";
                node.Attributes.Add("href", "../css/bootstrap.min.css");
                node.Attributes.Add("rel", "stylesheet");
                divNodes.ChildNodes.Add(node);
                node = NodeHelper.GetNode(htmlDocument._loadedDocument);
                node.Name = "link";
                node.Attributes.Add("href", "../css/mdb.min.cs");
                node.Attributes.Add("rel", "stylesheet");
                divNodes.ChildNodes.Add(node);
                node = NodeHelper.GetNode(htmlDocument._loadedDocument);
                node.Name = "link";
                node.Attributes.Add("href", "../css/style.css");
                node.Attributes.Add("rel", "stylesheet");
                divNodes.ChildNodes.Add(node);
                node = NodeHelper.GetNode(htmlDocument._loadedDocument);
                node.Name = "link";
                node.Attributes.Add("href", "../css/addons/datatables.min.css");
                node.Attributes.Add("rel", "stylesheet");
                divNodes.ChildNodes.Add(node);
                #endregion

                //adding script blocks to body tag
                var headNode = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//head").FirstOrDefault();
                node = NodeHelper.GetNode(htmlDocument._loadedDocument);
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
                node.Name = "script";
                node.Attributes.Add("type", "text/javascript");
                node.Attributes.Add("src", "LeftNavMenu.js");
                headNode.ChildNodes.Add(node);
            }
            #endregion

            #region module entry
            ModuleHomeLinkedMenuEntry.Name = "a";
            ModuleHomeLinkedMenuEntry.Attributes.Add("style", "color: black;");
            ModuleHomeLinkedMenuEntry.Attributes.Add("onmouseover", "OnHoverHandle('#ModuleOverview');");
            ModuleHomeLinkedMenuEntry.Attributes.Add("onmouseout", "OnLeaveHandler('#ModuleOverview');");

            ModuleHomeLinkedMenuEntry.Attributes.Add("id", "ModuleOverview");
            ModuleHomeLinkedMenuEntry.Attributes.Add("href", "#!");
            ModuleHomeLinkedMenuEntry.Attributes.Add("parent", "DXCHome");
            ModuleHomeLinkedMenuEntry.Attributes.Add("IsParentActive", "true");
            ModuleHomeLinkedMenuEntry.Attributes.Add("BreadscrumbDisplayName", ModuleSettings.ModuleNameDisplay);
            ModuleHomeLinkedMenuEntry.Attributes.Add("TopicContentHtml", "" + ModuleSettings.WebHost
                                                                            + @"\" + ModuleSettings.WebRoutingTargetPath.Replace(ModuleSettings.WebHostPhysicalPath, string.Empty)
                                                                            + @"\" + Common.Constants.WebSolutionStructure.Folders.Html
                                                                            + @"\ModuleOverview.html");

            ModuleHomeLinkedMenuEntry.InnerHtml = ModuleSettings.ModuleNameDisplay;
            ModuleHomeLinkedMenuEntry.Attributes.Add("onclick", "OnClickHandler(this)");
            ModuleHomeDivForMenuEntryTocLevel1.InnerHtml = ModuleHomeLinkedMenuEntry.OuterHtml;
            divHelperTocNav.addChildrenNode(ModuleHomeDivForMenuEntryTocLevel1);
            #endregion

            if (ModuleSettings.ModuleHelpContentAvailable)
            {
                EventsRootMenuEntry.Name = "a";
                EventsRootMenuEntry.Attributes.Add("style", "color: black;");
                EventsRootMenuEntry.InnerHtml = "Events Produced";
                EventsDivForMenuEntryTocLevel0.InnerHtml = EventsRootMenuEntry.OuterHtml;
                EventsLinkedMenuEntry.Name = "a";
                EventsLinkedMenuEntry.Attributes.Add("style", "color: black;");
                EventsLinkedMenuEntry.Attributes.Add("onmouseover", "OnHoverHandle('#EventsFullList');");
                EventsLinkedMenuEntry.Attributes.Add("onmouseout", "OnLeaveHandler('#EventsFullList');");
                EventsLinkedMenuEntry.Attributes.Add("id", "EventsFullList");
                EventsLinkedMenuEntry.Attributes.Add("href", "#!");
                EventsLinkedMenuEntry.Attributes.Add("parent", "Integration");
                EventsLinkedMenuEntry.Attributes.Add("IsParentActive", "true");

                if (ModuleSettings.ContractListPages.ListPage.Any())
                {
                    EventsLinkedMenuEntry.Attributes.Add("BreadscrumbDisplayName", "Events Produced");
                } 
                else
                {
                    EventsLinkedMenuEntry.Attributes.Add("BreadscrumbDisplayName", "Events Produced: No events are produced for this sub-module.");
                }

                EventsLinkedMenuEntry.Attributes.Add("TopicContentHtml", "" + ModuleSettings.WebHost
                                                                                + @"\" + ModuleSettings.WebRoutingTargetPath.Replace(ModuleSettings.WebHostPhysicalPath, string.Empty)
                                                                                + @"\" + Common.Constants.WebSolutionStructure.Folders.Html
                                                                                + @"\" + ModuleSettings.MainContractContent + " #namespacesSection");
                EventsLinkedMenuEntry.Attributes.Add("onclick", "OnClickHandler(this)");
                EventsLinkedMenuEntry.InnerHtml = "Events Produced";
                EventsDivForMenuEntryTocLevel1.InnerHtml = EventsLinkedMenuEntry.OuterHtml;
                divHelperTocNav.addChildrenNode(EventsDivForMenuEntryTocLevel1);

                ServicesRootMenuEntry.InnerHtml = "Web Services";
                ServicesRootMenuEntry.Name = "a";
                ServicesRootMenuEntry.Attributes.Add("style", "color: black;");


                if (ServiceList.Count > 0)
                {
                    ServicesDivForMenuEntryTocLevel0.InnerHtml = ServicesRootMenuEntry.OuterHtml;
                    divHelperTocNav.addChildrenNode(ServicesDivForMenuEntryTocLevel0);

                    foreach (string service in ServiceList)
                    {
                        if (!string.IsNullOrEmpty(service))
                        {
                            ServicesMenuEntry.Add(service.Split('>')[1].Split('<')[0], @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                        }
                    }

                    var ServicesMenuEntryOrdered = ServicesMenuEntry.OrderBy(x => x.Key);

                    ServicesLinkedMenuEntry.Name = "a";
                    ServicesLinkedMenuEntry.Attributes.Add("style", "color: black;");

                    foreach (var menuEntryOrdered in ServicesMenuEntryOrdered)
                    {
                        ServicesLinkedMenuEntry.Attributes.Remove("onmouseover");
                        ServicesLinkedMenuEntry.Attributes.Remove("onmouseout");
                        ServicesLinkedMenuEntry.Attributes.Remove("id");
                        ServicesLinkedMenuEntry.Attributes.Remove("onclick");
                        ServicesLinkedMenuEntry.Attributes.Remove("BreadscrumbDisplayName");
                        ServicesLinkedMenuEntry.Attributes.Remove("TopicContentHtml");
                        ServicesLinkedMenuEntry.Attributes.Add("href", "#!");
                        ServicesLinkedMenuEntry.Attributes.Add("Parent", "Web Services");
                        ServicesLinkedMenuEntry.Attributes.Add("IsParentActive", "false");
                        ServicesLinkedMenuEntry.Attributes.Add("onclick", "OnClickHandler(this)");
                        ServicesLinkedMenuEntry.Attributes.Add("BreadscrumbDisplayName", menuEntryOrdered.Key);
                        ServicesLinkedMenuEntry.Attributes.Add("TopicContentHtml", "" + ModuleSettings.WebHost
                                                                                    + @"\" + ModuleSettings.WebRoutingTargetPath.Replace(ModuleSettings.WebHostPhysicalPath, string.Empty)
                                                                                    + @"\" + Common.Constants.WebSolutionStructure.Folders.Html
                                                                                    + @"\" + menuEntryOrdered.Value + " #ID4RBSection");
                        ServicesLinkedMenuEntry.InnerHtml = menuEntryOrdered.Key;
                        ServicesLinkedMenuEntry.Attributes.Add("onmouseover", "OnHoverHandle('#" + menuEntryOrdered.Key + "');");
                        ServicesLinkedMenuEntry.Attributes.Add("onmouseout", "OnLeaveHandler('#" + menuEntryOrdered.Key + "');");
                        ServicesLinkedMenuEntry.Attributes.Add("id", menuEntryOrdered.Key);
                        ServicesDivForMenuEntryTocLevel1.InnerHtml += ServicesLinkedMenuEntry.OuterHtml;
                    }

                    divHelperTocNav.addChildrenNode(ServicesDivForMenuEntryTocLevel1);
                }
                else
                {
                    var serviceListpage = ModuleSettings.ServiceListPages.ListPage[0];

                    ServicesRootMenuEntry.Attributes.Add("onmouseover", "OnHoverHandle('#WebServiceEmpty');");
                    ServicesRootMenuEntry.Attributes.Add("onmouseout", "OnLeaveHandler('#WebServiceEmpty');");
                    ServicesRootMenuEntry.Attributes.Add("id", "WebServiceEmpty");
                    ServicesRootMenuEntry.Attributes.Add("href", "#!");
                    ServicesRootMenuEntry.Attributes.Add("onclick", "OnClickHandler(this)");
                    ServicesRootMenuEntry.Attributes.Add("parent", "Integration");
                    ServicesRootMenuEntry.Attributes.Add("IsParentActive", "true");
                    ServicesRootMenuEntry.Attributes.Add("BreadscrumbDisplayName", "Web Service Produced: No services are produced for this sub-module.");
                    ServicesRootMenuEntry.Attributes.Add("TopicContentHtml", "" + ModuleSettings.WebHost
                                                                                    + @"\" + ModuleSettings.WebRoutingTargetPath.Replace(ModuleSettings.WebHostPhysicalPath, string.Empty)
                                                                                    + @"\" + Common.Constants.WebSolutionStructure.Folders.Html
                                                                                    + @"\" + serviceListpage + " #ID4RBSection");

                    ServicesDivForMenuEntryTocLevel0.InnerHtml = ServicesRootMenuEntry.OuterHtml;
                    divHelperTocNav.addChildrenNode(ServicesDivForMenuEntryTocLevel0);

                }
            }

            if (ModuleSettings.ModuleAPIAvailable)
            {
                foreach (string service in apiContentList)
                {
                    if (!string.IsNullOrEmpty(service))
                    {
                        apiMenuEntry.Add(service.Split('>')[1].Split('<')[0], @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                    }
                }

                var apiMenuEntryOrdered = apiMenuEntry.OrderBy(x => x.Key);

                apiRootMenuEntry.Name = "a";
                apiRootMenuEntry.Attributes.Add("style", "color: black;");
                apiRootMenuEntry.InnerHtml = "Module Specific API";
                apiDivForMenuEntryTocLevel0.InnerHtml = apiRootMenuEntry.OuterHtml;
                divHelperTocNav.addChildrenNode(apiDivForMenuEntryTocLevel0);

                OtherResourcesAPILinkedMenuEntry.Name = "a";
                OtherResourcesAPILinkedMenuEntry.Attributes.Add("style", "color: black;");
                OtherResourcesAPILinkedMenuEntry.Attributes.Add("onmouseover", "OnHoverHandle('#moduleAPI');");
                OtherResourcesAPILinkedMenuEntry.Attributes.Add("onmouseout", "OnLeaveHandler('#moduleAPI');");
                OtherResourcesAPILinkedMenuEntry.Attributes.Add("id", "moduleAPI");
                OtherResourcesAPILinkedMenuEntry.Attributes.Add("href", "#!");
                OtherResourcesAPILinkedMenuEntry.Attributes.Add("Parent", "Module Specific API");
                OtherResourcesAPILinkedMenuEntry.Attributes.Add("IsParentActive", "false");
                OtherResourcesAPILinkedMenuEntry.Attributes.Add("onclick", "OnClickHandler(this)");
                OtherResourcesAPILinkedMenuEntry.Attributes.Add("BreadscrumbDisplayName", apiMenuEntryOrdered.FirstOrDefault().Key + " API");

                OtherResourcesAPILinkedMenuEntry.Attributes.Add("TopicContentHtml", "" + ModuleSettings.WebHost
                                                                       + @"\" + ModuleSettings.ModuleAPITargetPath.Replace(ModuleSettings.WebHostPhysicalPath, string.Empty)
                                                                       + @"\" + Common.Constants.WebSolutionStructure.Folders.Html
                                                                       + @"\" + apiMenuEntryOrdered.FirstOrDefault().Value + " #ID5RBSection");

                OtherResourcesAPILinkedMenuEntry.InnerHtml = apiMenuEntryOrdered.FirstOrDefault().Key + " API";
                OtherResourcesDivForApiMenuEntryTocLevel1.InnerHtml = OtherResourcesAPILinkedMenuEntry.OuterHtml;
                divHelperTocNav.addChildrenNode(OtherResourcesDivForApiMenuEntryTocLevel1);
            }

            #region otherResources
            OtherResourcesRootMenuEntry.Name = "a";
            OtherResourcesRootMenuEntry.Attributes.Add("style", "color: black;");
            OtherResourcesRootMenuEntry.InnerHtml = "Other Resources";
            OtherResourcesDivForMenuEntryTocLevel0.InnerHtml = OtherResourcesRootMenuEntry.OuterHtml;
            divHelperTocNav.addChildrenNode(OtherResourcesDivForMenuEntryTocLevel0);

            OtherResourcesLinkedMenuEntry.Name = "a";
            OtherResourcesLinkedMenuEntry.Attributes.Add("style", "color: black;");
            OtherResourcesLinkedMenuEntry.Attributes.Add("onmouseover", "OnHoverHandle('#OtherResources');");
            OtherResourcesLinkedMenuEntry.Attributes.Add("onmouseout", "OnLeaveHandler('#OtherResources');");

            OtherResourcesLinkedMenuEntry.Attributes.Add("id", "OtherResources");
            OtherResourcesLinkedMenuEntry.Attributes.Add("href", ModuleSettings.WebHost + @"\" + ModuleSettings.OtherResourcesFolder);
            OtherResourcesLinkedMenuEntry.Attributes.Add("target", "_blank");
            OtherResourcesLinkedMenuEntry.InnerHtml = "Browse to xsd and wsdl folder";
            OtherResourcesDivForMenuEntryTocLevel1.InnerHtml = OtherResourcesLinkedMenuEntry.OuterHtml;
            divHelperTocNav.addChildrenNode(OtherResourcesDivForMenuEntryTocLevel1);

            if (!string.IsNullOrEmpty(ModuleSettings.WebPortalPath))
            {
                //Adding Portal External Links
                OtherResourcesPortalLinkedMenuEntry.Name = "a";
                OtherResourcesPortalLinkedMenuEntry.Attributes.Add("style", "color: black;");
                OtherResourcesPortalLinkedMenuEntry.Attributes.Add("onmouseover", "OnHoverHandle('#ExternalPortal');");
                OtherResourcesPortalLinkedMenuEntry.Attributes.Add("onmouseout", "OnLeaveHandler('#ExternalPortal');");

                OtherResourcesPortalLinkedMenuEntry.Attributes.Add("id", "ExternalPortal");
                OtherResourcesPortalLinkedMenuEntry.Attributes.Add("href", ModuleSettings.WebHost + @"\" + ModuleSettings.WebPortalPath);
                
                OtherResourcesPortalLinkedMenuEntry.Attributes.Add("target", "_blank");
                OtherResourcesPortalLinkedMenuEntry.InnerHtml = "Browse to portal documentation";
                OtherResourcesDivForMenuEntryTocLevel2.InnerHtml = OtherResourcesPortalLinkedMenuEntry.OuterHtml;
                divHelperTocNav.addChildrenNode(OtherResourcesDivForMenuEntryTocLevel2);
            }
                                     
            #endregion

            this.UpdatePageBodyAttrib(htmlDocument._loadedDocument);

            htmlDocument.Save();
        }
       
        public void UpdatePageHeader(HtmlDocument doc)
        {
            DivHelper divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, Common.Constants.HtmlInventory.PageHeaderDiv);
            divHelper.SetInnerHtml(Common.Constants.Labels.ProductName);
        }

        public void UpdatePageFooter(HtmlDocument doc)
        {
            DivHelper divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, Common.Constants.HtmlInventory.PageFooterDiv);
            divHelper.removeAllChildNodes();
            divHelper.SetInnerHtml(Common.Constants.Labels.CopyRight);
        }

        public void UpdateBuildVersionPageFooter()
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = InformationModel.TargetPathHomePage + @"\DocumentationHome.htm";
            htmlDocument.Load();

            DivHelper divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "buildInformation");
            divHelper.SetInnerHtml($"Server: {InformationModel.ServerIp} Build Date: {DateTime.Now.ToString("M/d/yyyy")} v{InformationModel.BuildVersion}");

            htmlDocument.Save();
        }

        public void UpdatePageBodyAttrib(HtmlDocument doc)
        {
            DivHelper divHelper = new DivHelper(doc, DivHelper.SearchFilter.Class, "pageBody");
            divHelper._ContextDiv.Attributes.Add("id", "pageBody");

            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "TocResize");
            divHelper.Remove();

            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "tocResizableEW");
            divHelper.Remove();
        }


        /// <summary>
        /// UpdateBodyPage
        /// Remove and clean unwanted html tags for events, commands, queryParams, Models and DTO
        /// </summary>
        /// <returns>void</returns>
        public void UpdateBodyPage(HtmlDocument doc, string mainNode, string docPath, string moduleName, string mainPage)
        {
            TableHelper tableHelper;

            // calling div helper to remove copy - Syntax section
            DivHelper divHelper = new DivHelper(doc, DivHelper.SearchFilter.Class, Common.Constants.HtmlInventory.PageSyntaxisClassDiv);
            divHelper.Remove();

            // calling span helper to remove Syntax header
            SpanHelper spanHelper = new SpanHelper(doc, SpanHelper.SearchFilter.Class, "collapsibleRegionTitle");
            spanHelper.RemoveAllOccurences();

            //calling div hhelper to remove reference on the botton section
            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID6RBSection");
            divHelper.Remove();

            // calling div helper to remove methods section 
            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID5RBSection");
            divHelper.Remove();

            // calling div helper to remove empty section 
            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID2RBSection");
            divHelper.Remove();

            // calling div helper to remove see also header
            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "seeAlsoSection");
            divHelper.Remove();

            TableHelper tblConstructor = new TableHelper(doc, TableHelper.SearchFilter.Id, "constructorList");
            tblConstructor.Remove();

            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID2RBSection");
            divHelper.Remove();

            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID0RBSection");
            divHelper.Remove();

            spanHelper = new SpanHelper(doc, SpanHelper.SearchFilter.Class, "introStyle");
            spanHelper.RemoveNode();

            TextHelper txtHelper = new TextHelper(doc, "Namespace:");
            txtHelper.removeTagByText("TopicContent");

            txtHelper = new TextHelper(doc, "Assembly:");
            txtHelper.removeTagByText("TopicContent");

            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "PageHeader");
            divHelper.Remove();

            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Class, "leftNav");
            divHelper.Remove();

            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "TopicContent");
            divHelper.removeStyleClass("topicContent");

            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "pageFooter");
            divHelper.Remove();

            tableHelper = new TableHelper(doc, TableHelper.SearchFilter.Class, "titleTable");
            tableHelper.removeRow(0);

            txtHelper = new TextHelper(doc, "Top");
            txtHelper.removeTagByText("ID4RBSection");

            txtHelper = new TextHelper(doc, "Top");
            txtHelper.removeTagByText("ID3RBSection");

            if (ModuleSettings.ModuleName != string.Empty)
            {
                txtHelper = new TextHelper(doc, "HP.HSP.UA3.");
                txtHelper.removeTagByTextStartWith("TopicContent");

                txtHelper = new TextHelper(doc, "HP.HSP.UA3.");
                txtHelper.removeTagByTextStartWith("TopicContent");

                txtHelper = new TextHelper(doc, "HPE.HSP.UA3");
                txtHelper.removeTagByTextStartWith("TopicContent");

                txtHelper = new TextHelper(doc, "HPE.HSP.UA3");
                txtHelper.removeTagByTextStartWith("TopicContent");

                txtHelper = new TextHelper(doc, "HPP.HSP.UA3");
                txtHelper.removeTagByTextStartWith("TopicContent");

                txtHelper = new TextHelper(doc, "HPP.HSP.UA3");
                txtHelper.removeTagByTextStartWith("TopicContent");
            }

            if (Path.GetFileName(docPath).Contains("_Contracts_Events_") || Path.GetFileName(docPath).Contains("_Contracts_ViewDto_"))
            {
                tableHelper = new TableHelper(doc, TableHelper.SearchFilter.Class, "titleTable");
                tableHelper.SetCellDisplayValue("titleColumn", tableHelper.readTdDisplayValueByClass("titleColumn").Replace("Class", "Event"));

                txtHelper = new TextHelper(doc, "event exposes the following attributes.");
                txtHelper.renameTagByText("TopicContent", "type exposes the following members.");
            }

            tableHelper = new TableHelper(doc, TableHelper.SearchFilter.Class, "titleTable");
            tableHelper.SetCellDisplayValue("titleColumn", tableHelper.readTdDisplayValueByClass("titleColumn").Replace("Class", string.Empty));
        }

        /// <summary>
        /// UpdateMethodsTableList
        /// <para>Add columns: Input, Event and Operation Name</para>
        /// </summary>
        /// <param name="originalDocument"></param>
        public void UpdateMethodsTableList(string originalDocument)
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = originalDocument;

            htmlDocument.Load();
            DivHelper seeAlsoDiv = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "seeAlsoStyle");
            seeAlsoDiv.SetInnerHtml(string.Empty);
            htmlDocument.Save();

            htmlDocument.Load();
            TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "methodList");
            tableHelper.addColumn("Input Command");
            tableHelper.addColumn("Event returned");
            tableHelper.renameColumnHeader(1, "Operation Name");
            tableHelper.removeColumn(0);
            htmlDocument.Save();
        }

        /// <summary>
        /// UpdateQueryMethodsTableList
        /// <para>Add columns: Input, Result and Operation </para>
        /// </summary>
        /// <param name="originalDocument"></param>
        public void UpdateQueryMethodsTableList(string originalDocument)
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = originalDocument;

            htmlDocument.Load();
            DivHelper seeAlsoDiv = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "seeAlsoStyle");
            seeAlsoDiv.SetInnerHtml(string.Empty);
            htmlDocument.Save();

            htmlDocument.Load();
            TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "methodList");
            tableHelper.addColumn("Input Query Parameter");
            tableHelper.addColumn("Query Collection Result");
            tableHelper.renameColumnHeader(1, "Operation Name");
            tableHelper.removeColumn(0);
            TextHelper txtHepler = new TextHelper(htmlDocument._loadedDocument, "[Missing");
            txtHepler.removeTagByTextStartWith("ID4RBSection");
            htmlDocument.Save();
        }
    }
}
