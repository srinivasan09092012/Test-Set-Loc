﻿using Controller.Helpers.HTML;
using Controller.Helpers.Scan;
using Common.ModuleSettings;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Interfaces;
using System;

namespace Controller
{
    public class HtmlFactory
    {
        int nodeIndex = 0;
        public ModuleSettingModel ModuleSettings = new ModuleSettingModel();
        private readonly ILogger LoggerEngine;

        public HtmlFactory(ILogger loggerEngineInstance)
        {
            LoggerEngine = loggerEngineInstance;
        }
        
        public void prepareIndexFile(string indexPage, string MainPageContent)
        {
            string fullSourcePath = ModuleSettings.WebTargetPath + indexPage;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            var headTag = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//head").FirstOrDefault().ChildNodes.Where(x => x.Name == "script").FirstOrDefault();
            headTag.InnerHtml = @"window.location.replace('html/" + MainPageContent + "')";
            htmlDocument.Save();

            //opening R_ page to remove namespace table
            htmlDocument._documentPath = ModuleSettings.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + MainPageContent;
            htmlDocument.Load();
            TableHelper tbl = new TableHelper(htmlDocument._loadedDocument, "namespaceList");
            tbl.Remove();

            DivHelper divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "collapsibleAreaRegion");
            divHelper.Remove();

            tbl = new TableHelper(htmlDocument._loadedDocument, string.Empty, "titleTable");
            tbl.SetCellDisplayValue("titleColumn", "BAS For Third Party Integrators");
            htmlDocument.Save();
        }

        public void updateCmdAndEventServiceList(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            updateServiceList(htmlDocument._loadedDocument, webFolderTarget);
            htmlDocument.Save();
        }

        public void updateCmdAndEventServiceListForQuery(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            updateServiceListForQuery(htmlDocument._loadedDocument, webFolderTarget);
            htmlDocument.Save();
        }

        public void preparePropertiesTable(string originalDocument)
        {
            string fullSourcePath = originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            addDataTypeColumn(htmlDocument._loadedDocument);
            htmlDocument.Save();
            htmlDocument.Load();
            fillDataTypeColumn(htmlDocument._loadedDocument);
            htmlDocument.Save();
            htmlDocument.Load();
            removeHiperLinkNameColumn(htmlDocument._loadedDocument);
            htmlDocument.Save();
            htmlDocument.Load();
            removeIconColumn(htmlDocument._loadedDocument);
            htmlDocument.Save();
        }

        /// <summary>
        /// getServiceListFromPageContent
        /// create directory folder and handle exceptions
        /// </summary>
        /// <param name="contentPage">Page content that hold a list of links of services</param>
        /// <returns>A list of services in the given content page, each item in the list are still html links, format should be applied to read.</returns>
        public List<string> getServiceListFromPageContent(string contentPage)
        {
            string fullSourcePath = ModuleSettings.WebTargetPath + contentPage;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, "classList", contentPage);
            return tblHelper.readColumnValues(1);
        }

        /// <summary>
        /// extractServiceUrl
        /// Each item in input, is a <a href> tag. This method extract the "href" target
        /// </summary>
        /// <param name="serviceList">A list of <a href> tags, where each <href> links to a Service</param>
        /// <returns>A list of service Url's</returns>
        public List<string> extractServiceUrl(List<string> serviceList)
        {
            List<string> ServiceUrls = new List<string>();

            foreach (string service in serviceList)
            {
                ServiceUrls.Add((ModuleSettings.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension));
            }

            return ServiceUrls;
        }

        public void prepareServiceOperationList(string originalDocument)
        {
            string fullSourcePath = ModuleSettings.WebTargetPath + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, "classList", originalDocument);
            tblHelper = new TableHelper(htmlDocument._loadedDocument, "classList");

            List<string> services = tblHelper.readColumnValues(1);

            foreach (string service in services)
            {
                var svcHtmlDocument = DocumentHelper.GetInstance();
                svcHtmlDocument._documentPath = ModuleSettings.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension;
                svcHtmlDocument.Load();
                OperationListPage(htmlDocument);
                svcHtmlDocument.Save();
            }

            tblHelper.removeColumn(0);
            htmlDocument.Save();
        }

        public void UpdateOperationsLists(string originalDocument)
        {
            string fullSourcePath = ModuleSettings.WebTargetPath + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();

            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, "classList", originalDocument);
            List<string> services = tblHelper.readColumnValues(1);

            foreach (string service in services)
            {
                if (service.Contains("Query"))
                {
                    this.UpdateQueryMethodsTableList(ModuleSettings.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                }
                else
                {
                    this.UpdateMethodsTableList(ModuleSettings.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                }
            }
        }

        public void UpdateInputOutput(string originalDocument)
        {
            string fullSourcePath = ModuleSettings.WebTargetPath + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();

            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, "classList", originalDocument);
            List<string> services = tblHelper.readColumnValues(1);

            foreach (string service in services)
            {
                if (service.Contains("Query"))
                {
                    this.updateCmdAndEventServiceListForQuery(ModuleSettings.WebTargetPath, Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                }
                else
                {
                    this.updateCmdAndEventServiceList(ModuleSettings.WebTargetPath, Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
                }
            }
        }

        public void removeHiperLinks(string pageContent)
        {
            string fullSourcePath = ModuleSettings.WebTargetPath + pageContent;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, "classList", pageContent);
            List<string> pagesExposingServices = tblHelper.readColumnValues(1);
            
            foreach (string servicePage in pagesExposingServices)
            {
                removeTableCellHiperLink(ModuleSettings.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + servicePage.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension);
            }
        }

        public void AddPaginationControl(string originalDocument)
        {
            string fullSourcePath = ModuleSettings.WebTargetPath + originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();

            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, "classList", originalDocument);
            List<string> services = tblHelper.readColumnValues(1);

            foreach (string service in services)
            {
                setPaginationControl(ModuleSettings.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, "methodList", "ID3RBSection");
            }
        }

        public void RemoveEmptyParams(string originalDocument)
        {
            string fullSourcePath = ModuleSettings.WebTargetPath + originalDocument;

            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();

            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, "classList", originalDocument);
            List<string> services = tblHelper.readColumnValues(1);

            foreach (string service in services)
            {
                removeEmptyTags(ModuleSettings.WebTargetPath, Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension, "p");
            }
        }

        public void UpdateLeftNavigator(string urlPage, List<string> ServiceList, string lvl0HomePage)
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = urlPage;
            htmlDocument.Load();
            UpdatePageLeftMenu(htmlDocument._loadedDocument, ServiceList, lvl0HomePage);
            htmlDocument.Save();
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
            UpdateBodyPage(htmlDocument._loadedDocument, "div", htmlPage, ModuleSettings.ModuleName, ModuleSettings.MainPageContent);
            htmlDocument.Save();
        }

        private void addDataTypeColumn(HtmlDocument doc)
        {
            TableHelper tableHelper = new TableHelper(doc, "propertyList");
            tableHelper.addTableColumn("Data Type");
        }

        private void removeIconColumn(HtmlDocument doc)
        {
            TableHelper tableHelper = new TableHelper(doc, "propertyList");
            tableHelper.removeColumn(0);
        }

        public void fillDataTypeColumn(HtmlDocument htmlDoc)
        {
            //update with data type value
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            TableHelper tableHelper = new TableHelper(htmlDoc, "propertyList");
            List<string> files = tableHelper.readColumnValues(1);
            int j;
            int x = 1;

            foreach (var file in files)
            {
                if (file.StartsWith("<a href="))
                {
                    j = (ModuleSettings.WebTargetPath + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                    innerHtmlDoc.Load((ModuleSettings.WebTargetPath + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                    DivHelper divHelper = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID0EACA_code_Div1");
                    var nodes = divHelper._ContextDiv.ChildNodes.FirstOrDefault().ChildNodes.Where(y => y.HasClass("identifier")).ToList<HtmlNode>();

                    if (nodes.Count() == 2)
                    {
                        DivHelper dv2 = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID1RBSection");
                        var result = dv2._ContextDiv.ChildNodes.Where(n => n.Name.Equals("a"));

                        if (result != null && result.Count() > 0)
                        {
                            tableHelper.SetCellDisplayValue(3, x, result.FirstOrDefault().OuterHtml);
                        }
                        else
                        {
                            tableHelper.SetCellDisplayValue(3, x, nodes[0].InnerText);
                        }
                    }
                    else if (nodes.Count() == 3)
                    {
                        if (nodes[0].InnerText == "Nullable")
                        {
                            tableHelper.SetCellDisplayValue(3, x, nodes[1].InnerText);
                        }
                        else
                        {

                            DivHelper dv2 = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID1RBSection");
                            var result = dv2._ContextDiv.ChildNodes.Where(n => n.Name.Equals("a"));

                            if (result.Count() > 0)
                            {
                                tableHelper.SetCellDisplayValue(3, x, nodes[0].InnerText + "&lt;" + result.FirstOrDefault().OuterHtml + "&gt;");
                            }
                            else
                            {
                                tableHelper.SetCellDisplayValue(3, x, nodes[0].InnerText + "&lt;" + nodes[1].InnerText + "&gt;");
                            }
                        }
                    }
                    else if (nodes.Count() == 4)
                    {
                        DivHelper dv2 = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID1RBSection");
                        var result = dv2._ContextDiv.ChildNodes.Where(n => n.Name.Equals("a"));

                        if (result.Count() > 0)
                        {
                            tableHelper.SetCellDisplayValue(3, x, nodes[1].InnerText + "&lt;" + result.FirstOrDefault().OuterHtml + "&gt;");
                        }
                        else
                        {
                            tableHelper.SetCellDisplayValue(3, x, nodes[1].InnerText + "&lt;" + nodes[1].InnerText + "&gt;");
                        }
                        
                    }
                    
                    x++;
                }
            }
        }

        public void removeHiperLinkNameColumn(HtmlDocument htmlDoc)
        {
            //update with data type value
            nodeIndex++;
            HtmlNode newNode = new HtmlNode(HtmlNodeType.Element, htmlDoc, nodeIndex);
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            TableHelper tableHelper = new TableHelper(htmlDoc, "propertyList");
            List<string> files = tableHelper.readColumnValues(1);
            int x = 1;
            foreach (var file in files)
            {
                newNode.InnerHtml = file;
                tableHelper.SetCellDisplayValue(1, x, newNode.InnerText);
                x++;
            }
        }

        public void formatHtmlDataList(string path)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(path);
            DataListHelper diHelper = new DataListHelper(doc);
            List<HtmlNode> r = diHelper.formatDescriptionList("ID1RBSection");

            string collapsibleAreaRegion = "collapsibleAreaRegion";
            var divNodes = doc.DocumentNode.SelectNodes("//div[@class='" + collapsibleAreaRegion + "']");

            if (divNodes != null && divNodes.Count > 0)
            {

                foreach (var n in r)
                {
                    divNodes.FirstOrDefault().ChildNodes.Add(n);
                }

                divNodes.FirstOrDefault().Id = "ID0RBSection";
                divNodes.FirstOrDefault().RemoveClass("collapsibleAreaRegion");
                divNodes.FirstOrDefault().AddClass("collapsibleSection");
            }

            doc.Save(path, Encoding.UTF8);
        }

        public void updateServiceList(HtmlDocument htmlMainDoc, string webFolderTarget)
        {
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            TableHelper tableHelper = new TableHelper(htmlMainDoc, "methodList");
            List<string> r = tableHelper.readColumnValues(0);
            int x = 1;
            int j;
            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                formatHtmlDataList((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID0RBSection");
                tableHelper.SetCellDisplayValue(2, x, divHelper.GetChildValueByTag("a"));
                x++;
            }

            x = 1;
            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID1RBSection");
                tableHelper.SetCellDisplayValue(3, x, divHelper.GetChildValueByTag("a"));
                x++;
            }
        }

        public string CreateOnclickAttribute(DocumentHelper htmlDocument)
        {
            TableHelper tbl = null;
            string response = string.Empty;

            tbl = new TableHelper(htmlDocument._loadedDocument, "propertyList");
            if (tbl._ContextTable != null)
            {
                List<string> dataTypeColumn = tbl.readColumnValues(2);
                int x = 1;
                nodeIndex++;
                var tmpNode = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
                tmpNode.Name = "div";
                string NewCellValue = string.Empty;
                string hrefAttri = string.Empty;
                bool isList = false;

                foreach (var file in dataTypeColumn)//.Where(y => y.Contains("<a href=")))
                {
                    if (file.StartsWith("List"))
                    {
                        isList = true;
                        tmpNode.InnerHtml = file.Replace("List", string.Empty).Replace("&lt;", string.Empty).Replace("&gt;", string.Empty);
                    }
                    else
                    {
                        tmpNode.InnerHtml = file;
                    }

                    if (tmpNode.ChildNodes[0].HasAttributes)
                    {
                        hrefAttri = "HtmlBlock_" + tmpNode.ChildNodes[0].Attributes[0].Value;
                        tmpNode.ChildNodes[0].Attributes[0].Value = "#!";
                        tmpNode.ChildNodes[0].Attributes.Add("onClick", "try{updateBreadCrumb({DisplayName:'" + tmpNode.ChildNodes[0].InnerText + "',TargetUrl:'"
                                                                            + ModuleSettings.WebHost
                                                                            + @"\" + ModuleSettings.WebTargetPath.Replace(ModuleSettings.WebHostPhysicalPath, string.Empty)
                                                                            + @"\" + Common.Constants.WebSolutionStructure.Folders.Html
                                                                            + @"\" + hrefAttri + "',IsActive:true});}catch(err){alert(err.message);}");
                        if (isList)
                        {
                            NewCellValue = "List &lt;" + tmpNode.ChildNodes[0].OuterHtml + "&gt;";
                        }
                        else
                        {
                            NewCellValue = tmpNode.ChildNodes[0].OuterHtml;
                        }

                        tbl.SetCellDisplayValue(2, x, NewCellValue);
                    }
                    
                    x++;
                    isList = false;
                }

                response = "<table>" + tbl._ContextTable.OuterHtml + "</table>";
                htmlDocument.Save();
            }

            return response;
        }

        public void CreateHtmlBlocks(string page)
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = page;
            htmlDocument.Load();

            var tableToHtmlBlock = CreateOnclickAttribute(htmlDocument);

            //rewriting this file because save fails when i set the .Text property
            using (StreamWriter sw = File.CreateText(Path.GetDirectoryName(htmlDocument._documentPath) + @"\" + "HtmlBlock_" + Path.GetFileName(htmlDocument._documentPath)))
            {
                sw.WriteLine(tableToHtmlBlock);
                sw.Flush(); 
            }

            //update links to use the entry point function in custom jquery code
            htmlDocument.Load();
        }

        public void updateServiceListForQuery(HtmlDocument doc, string webFolderTarget)
        {
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            HtmlDocument innerHtmlDocLvl2 = new HtmlDocument();
            HtmlDocument innerHtmlDocLvl3 = new HtmlDocument();
            TableHelper tableHelper = new TableHelper(doc, "methodList");
            TableHelper tblHelperlevl2;
            HtmlNode myNode;
            List<HtmlNode> myNodeList = new List<HtmlNode>();
            List<string> r = tableHelper.readColumnValues(0);
            int x = 1;
            int j;
            nodeIndex++;

            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID1RBSection");
                string linkfilelevel2 = divHelper.GetChildNodeOuterHtml("a");

                if (linkfilelevel2 != string.Empty)
                {
                    j = (webFolderTarget + @"html\" + linkfilelevel2.Remove(0, 9).Split('>')[0]).Length;
                    innerHtmlDocLvl2.Load((webFolderTarget + @"\html\" + linkfilelevel2.Remove(0, 9).Split('>')[0]).Substring(0, j));
                    tblHelperlevl2 = new TableHelper(innerHtmlDocLvl2, "propertyList");

                    if (tblHelperlevl2._ts != null)
                    {
                        List<string> linkColumns = tblHelperlevl2.readColumnValues(1);

                        string tempstr = string.Empty;
                        foreach (string col in linkColumns)
                        {
                            nodeIndex++;
                            myNode = new HtmlNode(HtmlNodeType.Element, doc, nodeIndex);
                            myNode.Name = "p";
                            myNode.InnerHtml = col;
                            if (myNode.InnerText == "Where")
                            {
                                tempstr = myNode.InnerHtml;
                                break;
                            }
                            nodeIndex++;
                            myNode = null;
                        }

                        string linkfilelvl3 = tempstr;

                        if (linkfilelvl3 != string.Empty)
                        {
                            j = (webFolderTarget + @"html\" + linkfilelvl3.Remove(0, 9).Split('>')[0]).Length;
                            innerHtmlDocLvl3.Load((webFolderTarget + @"\html\" + linkfilelvl3.Remove(0, 9).Split('>')[0]).Substring(0, j));
                            divHelper = new DivHelper(innerHtmlDocLvl3, DivHelper.SearchFilter.Id, "ID1RBSection");
                            string linkfilelvl4 = divHelper.GetChildValueByTag("a");
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
                x++;
            }

            x = 1;
            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, DivHelper.SearchFilter.Id, "ID1RBSection");
                string linkfilelevel2 = divHelper.GetChildNodeOuterHtml("a");
                if (linkfilelevel2 != string.Empty)
                {
                    j = (webFolderTarget + @"html\" + linkfilelevel2.Remove(0, 9).Split('>')[0]).Length;
                    innerHtmlDocLvl2.Load((webFolderTarget + @"\html\" + linkfilelevel2.Remove(0, 9).Split('>')[0]).Substring(0, j));
                    divHelper = new DivHelper(innerHtmlDocLvl2, DivHelper.SearchFilter.Id, "ID0RBSection");
                    tableHelper.SetCellDisplayValue(3, x, divHelper.GetChildValueByTag("a"));
                }
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

        private void OperationListPage(DocumentHelper doc)
        {
            TableHelper tableHelper = new TableHelper(doc._loadedDocument, string.Empty, "titleTable");
            TableHelper MathodListTableHelper = new TableHelper(doc._loadedDocument, "methodList");
            List<string> list = MathodListTableHelper.readColumnValues(0);
            tableHelper.SetCellDisplayValue("titleColumn", tableHelper.GetCellDisplayValue("titleColumn").Replace("Class", string.Empty));
            SpanHelper spanHelper = new SpanHelper(doc._loadedDocument, "collapsibleRegionTitle", 1);

            spanHelper.RemoveNodeByClass();
            doc.Save();

            doc.Load();
            spanHelper.RemoveNodeByClass();
            doc.Save();

            doc.Load();
            spanHelper.RenameChildNode("Service Operations");
            doc.Save();

            doc.Load();
            spanHelper = new SpanHelper(doc._loadedDocument, "collapsibleRegionTitle_2");
            spanHelper.RemoveNodeById();
            doc.Save();

            doc.Load();
            spanHelper = new SpanHelper(doc._loadedDocument, "collapsibleRegionTitle_3");
            spanHelper.RemoveNodeById();
            doc.Save();

            spanHelper = new SpanHelper(doc._loadedDocument, "introStyle", 1);
            spanHelper.RemoveNode();

            spanHelper = new SpanHelper(doc._loadedDocument, "nolink", 1);
            spanHelper.RemoveNode("all");

            spanHelper = new SpanHelper(doc._loadedDocument, "selflink", 1);
            spanHelper.RemoveNode("all");

            DivHelper divHelper = new DivHelper(doc._loadedDocument, DivHelper.SearchFilter.Id, "ID0RBSection");
            divHelper.Remove();

            divHelper = new DivHelper(doc._loadedDocument, DivHelper.SearchFilter.Class, "seeAlsoSection");
            divHelper.Remove();

            divHelper = new DivHelper(doc._loadedDocument, DivHelper.SearchFilter.Id, "ID4RBSection");
            divHelper.Remove();

            divHelper = new DivHelper(doc._loadedDocument, DivHelper.SearchFilter.Id, "ID5RBSection");
            divHelper.Remove();

            divHelper = new DivHelper(doc._loadedDocument, DivHelper.SearchFilter.Class, "codeSnippetContainer");
            divHelper.Remove();

            divHelper = new DivHelper(doc._loadedDocument, DivHelper.SearchFilter.Id, "ID2RBSection");
            divHelper.Remove();

            TextHelper txtHelper = new TextHelper(doc._loadedDocument, "Namespace:");
            txtHelper.removeTagByText("TopicContent");

            txtHelper = new TextHelper(doc._loadedDocument, "Assembly:");
            txtHelper.removeTagByText("TopicContent");

            if (ModuleSettings.ModuleName != string.Empty)
            {
                txtHelper = new TextHelper(doc._loadedDocument, "HP.HSP.UA3.");
                txtHelper.removeTagByTextStartWith("TopicContent");

                txtHelper = new TextHelper(doc._loadedDocument, "HP.HSP.UA3.");
                txtHelper.removeTagByTextStartWith("TopicContent");
            }
        }

        private void UpdatePageLeftMenu(HtmlDocument htmlDocument, List<string> ServiceList, string lvl0HomePage)
        {
            HtmlNode menuEntry;
            DivHelper divHelperTocNav = new DivHelper(htmlDocument, DivHelper.SearchFilter.Id, "tocNav");
            divHelperTocNav.SetInnerHtml(string.Empty);

            nodeIndex++;
            menuEntry = new HtmlNode(HtmlNodeType.Element, htmlDocument, nodeIndex);
            menuEntry.Name = "div";
            menuEntry.AddClass("toclevel0");
            menuEntry.Attributes.Add("data-toclevel", "0");
            menuEntry.InnerHtml = "<a class='tocCollapsed' onclick='javascript: Toggle(this); ' href='#!'></a> " + "<a data-tochassubtree = 'true' href='" + lvl0HomePage + "'>Payer Platform - Service and Event Specification</a>";
            divHelperTocNav.addChildrenNode(menuEntry);
            menuEntry = null;

            nodeIndex++;
            menuEntry = new HtmlNode(HtmlNodeType.Element, htmlDocument, nodeIndex);
            menuEntry.Name = "div";
            menuEntry.AddClass("toclevel1");
            menuEntry.Attributes.Add("data-toclevel", "1");
            menuEntry.InnerHtml = "<a class='tocCollapsed' onclick='javascript: Toggle(this); ' href='#!'></a> " + "<a data-tochassubtree = 'true' href='#!'>"+ ModuleSettings.ModuleNameDisplay  +" API Web Services</a>";
            divHelperTocNav.addChildrenNode(menuEntry);
            menuEntry = null;

            foreach (string service in ServiceList)
            {
                nodeIndex++;
                menuEntry = new HtmlNode(HtmlNodeType.Element, htmlDocument, nodeIndex);
                menuEntry.Name = "div";
                menuEntry.AddClass("toclevel2");
                menuEntry.Attributes.Add("data-toclevel", "2");
                menuEntry.InnerHtml = "<a class='tocCollapsed' onclick='javascript: Toggle(this);''href='#!' /><a data-tochassubtree='true' href='.." + Common.Constants.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constants.WebSolutionStructure.Files.Extensions.htmlExtension + "' title='Service' tocid='Methods_T_HP_HSP_UA3_" + ModuleSettings.ModuleName + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + "'>" + service.Split('>')[1].Split('<')[0] + "</a>";
                divHelperTocNav.addChildrenNode(menuEntry);
                menuEntry = null;
            }

            nodeIndex++;
            menuEntry = new HtmlNode(HtmlNodeType.Element, htmlDocument, nodeIndex);
            menuEntry.Name = "div";
            menuEntry.AddClass("toclevel1");
            menuEntry.Attributes.Add("data-toclevel", "1");
            menuEntry.Name = "div";
            menuEntry.InnerHtml = "<a class='tocCollapsed' onclick='javascript: Toggle(this); href='#'/><a data-tochassubtree='true' href='#!'>Events For Outbound Communications</a>";
            divHelperTocNav.addChildrenNode(menuEntry);
            menuEntry = null;

            nodeIndex++;
            menuEntry = new HtmlNode(HtmlNodeType.Element, htmlDocument, nodeIndex);
            menuEntry.Name = "div";
            menuEntry.AddClass("toclevel2");
            menuEntry.Attributes.Add("data-toclevel", "2");
            menuEntry.Name = "div";
            menuEntry.InnerHtml = "<a class='tocCollapsed' onclick='javascript: Toggle(this); href='#'/><a data-tochassubtree='true' href='.." + Common.Constants.WebSolutionStructure.Folders.Html + ModuleSettings.MainContractContent + "'>Full Events List</a>";
            divHelperTocNav.addChildrenNode(menuEntry);
            menuEntry = null;

            //TODO: THIS IS NOT PART OF LEFT NAV, RE LOCATE
            this.UpdatePageHeader(htmlDocument);
            this.UpdatePageFooter(htmlDocument);
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

        /// <summary>
        /// UpdateBodyPage
        /// Remove and clean unwanted html tags for events, commands, queryParams, Models and DTO
        /// </summary>
        /// <returns>void</returns>
        public void UpdateBodyPage(HtmlDocument doc, string mainNode, string docPath, string moduleName, string mainPage)
        {
            TableHelper tableHelper;

            //refactored method
            //***********************
            // common section starts
            //***********************

            // calling div helper to remove copy - Syntax section
            DivHelper divHelper = new DivHelper(doc, DivHelper.SearchFilter.Class, Common.Constants.HtmlInventory.PageSyntaxisClassDiv);
            divHelper.Remove();

            // calling span helper to remove Syntax header
            SpanHelper spanHelper = new SpanHelper(doc, Common.Constants.HtmlInventory.PageSyntaxiHeadersClassSpan, 1);
            spanHelper.RemoveNode("all");

            //calling div hhelper to remove reference on the botton section
            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID6RBSection");
            divHelper.Remove();

            // calling div helper to remove methods section 
            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID5RBSection");
            divHelper.Remove();

            //remarks at the bottom
            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID4RBSection");
            divHelper.Remove();

            // calling div helper to remove empty section 
            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID2RBSection");
            divHelper.Remove();

            //removing 
            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Class, "collapsibleAreaRegion");
            divHelper.Remove();

            // calling div helper to remove see also header
            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "seeAlsoSection");
            divHelper.Remove();

            // calling div helper to remove see also header
            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID3RBSection");

            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID2RBSection");
            divHelper.Remove();

            divHelper = new DivHelper(doc, DivHelper.SearchFilter.Id, "ID0RBSection");
            divHelper.Remove();

            spanHelper = new SpanHelper(doc, "introStyle", 1);
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

            tableHelper = new TableHelper(doc, "", "titleTable");
            tableHelper.removeRow(0);

            txtHelper = new TextHelper(doc, "Top");
            txtHelper.removeTagByText("ID3RBSection");

            if (ModuleSettings.ModuleName != string.Empty)
            {
                txtHelper = new TextHelper(doc, "HP.HSP.UA3.");
                txtHelper.removeTagByTextStartWith("TopicContent");

                txtHelper = new TextHelper(doc, "HP.HSP.UA3.");
                txtHelper.removeTagByTextStartWith("TopicContent");
            }

            if (Path.GetFileName(docPath).Contains("_Contracts_Events_") || Path.GetFileName(docPath).Contains("_Contracts_ViewDto_"))
            {
                tableHelper = new TableHelper(doc, string.Empty, "titleTable");
                tableHelper.SetCellDisplayValue("titleColumn", tableHelper.readTdDisplayValueByClass("titleColumn").Replace("Class", "Event"));

                txtHelper = new TextHelper(doc, "event exposes the following attributes.");
                txtHelper.renameTagByText("TopicContent", "type exposes the following members.");
            }

            tableHelper = new TableHelper(doc, string.Empty, "titleTable");
            tableHelper.SetCellDisplayValue("titleColumn", tableHelper.readTdDisplayValueByClass("titleColumn").Replace("Class", string.Empty));

            //end by page type
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

            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, "classList");
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
            nodeIndex++;
            HtmlNode tmpColumnHolderNode = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, "methodList");

            int x = 1;
            foreach (var serviceNameColumn in tableHelper.readColumnValues(0))
            {
                tmpColumnHolderNode.InnerHtml = serviceNameColumn;
                tableHelper.SetCellDisplayValue(0, x, tmpColumnHolderNode.InnerText);
                x++;
            }

            htmlDocument.Save();

            /*getting the services and descriptions*/
            htmlDocument.Load();
            tableHelper = new TableHelper(htmlDocument._loadedDocument, "methodList");
            TableHelper tableTitleHelper = new TableHelper(htmlDocument._loadedDocument, "", "titleTable");

            MissingTagsHelper scanHelper = new MissingTagsHelper(LoggerEngine);
            scanHelper.GetServicesSource(tableHelper.ReadAllColumnsValues(), ModuleSettings.ModuleName, tableTitleHelper.GetCellDisplayValue("titleColumn"), ModuleSettings.StorageDrivePath);
            scanHelper = null;
        }

        public void UpdateMethodsTableList(string doc)
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = doc;
            htmlDocument.Load();
            TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, "methodList");
            tableHelper.addTableColumn("Input Command");
            tableHelper.addTableColumn("Event returned");
            tableHelper.renameColumnHeader(1, "Operation Name");
            tableHelper.removeColumn(0);
            htmlDocument.Save();
        }

        public void setPaginationControl(string contentPage, string targetTable, string targetDivContainer)
        {
            // get rown num in list
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = contentPage;
            htmlDocument.Load();
            nodeIndex++;
            TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, targetTable);
            StringBuilder sb = new StringBuilder();
            HtmlNode tableNode = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            tableNode.Name = "table";
            tableNode.AddClass("table table-striped table-bordered table-sm");
            tableNode.Id = "methodList";

            //creating boostrap table

            // produce <thead></thead>
            sb.Append("<thead><tr>");
            foreach (var col in tableHelper._ts._tableHeaderColumns)
            {
                sb.Append("<th>");
                sb.Append(col.Value);
                sb.Append("</th>");
            }
            sb.Append("</tr></thead>");

            // produce <tbody>
            string bodyTable = tableHelper.InnerHtmltableCollection.Replace(sb.ToString().Replace("<thead>", string.Empty).Replace("</thead>", string.Empty), string.Empty);

            sb.Append("<tbody>");
            sb.Append(bodyTable);
            sb.Append("<tbody>");

            // produce <tfoot>
            sb.Append("<tfoot><tr>");
            foreach (var col in tableHelper._ts._tableHeaderColumns)
            {
                sb.Append("<th>");
                sb.Append(col.Value);
                sb.Append("</th>");
            }
            sb.Append("</tr></tfoot>");

            tableNode.InnerHtml = sb.ToString();

            //removing original table
            tableHelper.Remove();
            htmlDocument.Save();
            htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = contentPage;
            htmlDocument.Load();
            //collapsibleRegionTitle
            DivHelper divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, targetDivContainer);
            divHelper.addChildrenNode(tableNode);
            htmlDocument.Save();

            #region add links to head tag
            var divNodes = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//head").FirstOrDefault();
            nodeIndex++;
            HtmlNode node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "link";
            node.Attributes.Add("href", "../css/bootstrap.min.css");
            node.Attributes.Add("rel", "stylesheet");
            divNodes.ChildNodes.Add(node);
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "link";
            node.Attributes.Add("href", "../css/mdb.min.cs");
            node.Attributes.Add("rel", "stylesheet");
            divNodes.ChildNodes.Add(node);
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "link";
            node.Attributes.Add("href", "../css/style.css");
            node.Attributes.Add("rel", "stylesheet");
            divNodes.ChildNodes.Add(node);
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "link";
            node.Attributes.Add("href", "../css/addons/datatables.min.css");
            node.Attributes.Add("rel", "stylesheet");
            divNodes.ChildNodes.Add(node);
            #endregion


            //adding script blocks to body tag
            var headNode = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//head").FirstOrDefault();
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/jquery-3.4.0.min.js");
            headNode.ChildNodes.Add(node);
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/popper.min.js");
            headNode.ChildNodes.Add(node);
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/bootstrap.min.js");
            headNode.ChildNodes.Add(node);
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/addons/datatables.min.js");
            headNode.ChildNodes.Add(node);

            //adding script blocks to html tag
            var htmlNode = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//html").FirstOrDefault();
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "script";
            node.InnerHtml = "$(document).ready(function(){$('#methodList').DataTable();$('.dataTables_length').addClass('bs-select'); });";
            htmlNode.ChildNodes.Add(node);

            htmlDocument.Save();
        }

        public void UpdateQueryMethodsTableList(string doc)
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = doc;
            htmlDocument.Load();
            TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, "methodList");
            tableHelper.addTableColumn("Input Query Parameter");
            tableHelper.addTableColumn("Query Collection Result");
            tableHelper.renameColumnHeader(1, "Operation Name");
            tableHelper.removeColumn(0);
            htmlDocument.Save();
        }

        public void prepareEventPage()
        {
            var htmlDocument = DocumentHelper.GetInstance();
            DivHelper EvtView;
            TableHelper tblHelper;
            DivHelper divHelper;
            divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "leftNav");
            string innerDiv = divHelper.GetInnerHtml();
            htmlDocument._documentPath = ModuleSettings.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + ModuleSettings.MainContractContent;
            htmlDocument.Load();

            EvtView = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "PageHeader");
            EvtView.removeAllChildNodes();
            EvtView.SetInnerHtml("Healthcare Payer Platform");
            tblHelper = new TableHelper(htmlDocument._loadedDocument, "namespaceList");
            tblHelper.Remove();
            divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "leftNav");
            divHelper.SetInnerHtml(innerDiv);
            htmlDocument.Save();

            StringBuilder strTableEvents = new StringBuilder();
            var innerHtmlDocument = DocumentHelper.GetInstance();
            foreach (var page in ModuleSettings.ContractListPages.ListPage)
            {
                innerHtmlDocument._documentPath = ModuleSettings.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + page;
                innerHtmlDocument.Load();
                divHelper = new DivHelper(innerHtmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "leftNav");
                divHelper.SetInnerHtml(innerDiv);
                tblHelper = new TableHelper(innerHtmlDocument._loadedDocument, "classList");
                strTableEvents.Append(tblHelper.GetInnerHtml());
                innerHtmlDocument.Save();
            }

            //printing table
            htmlDocument._documentPath = ModuleSettings.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + ModuleSettings.MainContractContent;
            htmlDocument.Load();
            divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "ID0RBSection");

            nodeIndex++;
            HtmlNode tableevntNode = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            tableevntNode.AddClass("members");
            tableevntNode.Name = "table";
            tableevntNode.Id = "classList";
            tableevntNode.InnerHtml = strTableEvents.ToString();
            divHelper.addChildrenNode(tableevntNode);
            htmlDocument.Save();

            htmlDocument.Load();
            SpanHelper spanHelper = new SpanHelper(htmlDocument._loadedDocument, "collapsibleRegionTitle", 1);
            spanHelper.RenameChildNode("#text", "Event List");

            tblHelper = new TableHelper(htmlDocument._loadedDocument, "classList");
            tblHelper.removeColumn(0);
            tblHelper.renameColumnHeader(0, "Event");
            htmlDocument.Save();

            htmlDocument.Load();
            tblHelper = new TableHelper(htmlDocument._loadedDocument, "classList");
            List<string> col = tblHelper.readColumnValues(0);
            int x = 1;
            HtmlNode tmpNode = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            foreach (string val in col)
            {
                tmpNode.Name = "span";
                tmpNode.InnerHtml = val;
                tmpNode.ChildNodes[0].Attributes.Add("onClick", "window.open('" + tmpNode.ChildNodes[0].Attributes["href"].Value + "', 'MyWindow','width=800,height=850,toolbar=no,menubar=no,status=no,resizable=yes,scrollbars=yes'); return false;");
                tmpNode.ChildNodes[0].Attributes["href"].Value = "#";
                tblHelper.SetCellDisplayValue(0, x, tmpNode.InnerHtml);
                x++;
            }

            tblHelper = new TableHelper(htmlDocument._loadedDocument, string.Empty, "titleTable");
            tblHelper.SetCellDisplayValue("titleColumn", "Events For Third Party Integrators");

            divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "summary");
            divHelper.SetInnerHtml(string.Empty);
            divHelper.addLines(3);
            htmlDocument.Save();
        }

        public void addBreadCrumbsControl(string htmlPage)
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = htmlPage;
            htmlDocument.Load();

            DivHelper divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "collapsibleAreaRegion");
            divHelper.removeStyleClass("collapsibleAreaRegion");
            htmlDocument.Save();
            htmlDocument.Load();
            divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "collapsibleAreaRegion");
            nodeIndex++;
            HtmlNode breadCrumbsList = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            breadCrumbsList.Name = "ol";
            breadCrumbsList.Id = "BreadCrumbsEventPage";
            breadCrumbsList.AddClass("breadcrumb");

            nodeIndex++;
            HtmlNode breadCrumbsListDefaultItem = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            breadCrumbsListDefaultItem.Name = "li";
            breadCrumbsListDefaultItem.AddClass("breadcrumb-item");
            breadCrumbsListDefaultItem.AddClass("active");
            breadCrumbsListDefaultItem.InnerHtml = "Main Event Attributes";
            breadCrumbsList.ChildNodes.Add(breadCrumbsListDefaultItem);
   
            var headNode = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//head").FirstOrDefault();
            
            nodeIndex++;
            HtmlNode node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/jquery-3.4.0.min.js");
            headNode.ChildNodes.Add(node);
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/popper.min.js");
            headNode.ChildNodes.Add(node);
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/bootstrap.min.js");
            headNode.ChildNodes.Add(node);
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/addons/datatables.min.js");
            headNode.ChildNodes.Add(node);
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "link";
            node.Attributes.Add("rel", "stylesheet");
            node.Attributes.Add("href", "../css/bootstrap.min.css");
            headNode.ChildNodes.Add(node);
            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "link";
            node.Attributes.Add("rel", "stylesheet");
            node.Attributes.Add("href", "https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css");
            headNode.ChildNodes.Add(node);

            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "script";
            node.InnerHtml = "$(document).ready(function(){try{InitializeBreadCrumbs({DisplayName:'Main Event Attributes',TargetUrl:'" + ModuleSettings.WebHost + @"\" + ModuleSettings.WebTargetPath.Replace(ModuleSettings.WebHostPhysicalPath, string.Empty) + @"\" + Common.Constants.WebSolutionStructure.Folders.Html +  @"\" + htmlDocument._documentTitle + "',IsActive: false});}catch(err){alert(err.message);}});";  //"$(document).ready(function(){try{InitializeBreadCrumbs({DisplayName:'Main Event Attributes',TargetUrl:'http://localhost:8080/ProviderManagement/APISeviceSpecification/html/T_HP_HSP_UA3_ProviderManagement_BAS_Providers_Contracts_Events_ServiceLocationAdded.htm',IsActive: false});}catch(err){alert(err.message);}});";
            headNode.ChildNodes.Add(node);

            nodeIndex++;
            node = new HtmlNode(HtmlNodeType.Element, htmlDocument._loadedDocument, nodeIndex);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "ComplexEventNavigator.js");
            headNode.ChildNodes.Add(node);

            divHelper.addChildrenNode(breadCrumbsList);

            htmlDocument.Save();
        }
    }
}
