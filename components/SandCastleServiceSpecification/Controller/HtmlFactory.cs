using Common.Information;
using Common.Interfaces;
using Common.ModuleSettings;
using Controller.Helpers.Extracts;
using Controller.Helpers.HTML;
using Controller.Helpers.Scan;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Controller
{
    public class HtmlFactory
    {
        public ModuleSettingModel ModuleSettings { get; set; }
        private RemoveHtmlBlocks removeHtmlBlocks = new RemoveHtmlBlocks();
        private AddHtmlBlocks addHtmlBlocks = new AddHtmlBlocks();
        private readonly ILogger LoggerEngine;
        private readonly IExecutionContext ExecutionContext;

        public AddHtmlBlocks AddHtmlBlocks
        {
            get
            {
                return addHtmlBlocks;
            }
        }

        public RemoveHtmlBlocks RemoveHtmlBlocks
        {
            get
            {
                return removeHtmlBlocks;
            }
        }

        public HtmlFactory()
        {

        }

        public HtmlFactory(ILogger loggerEngineInstance)
        {
            LoggerEngine = loggerEngineInstance;
        }

        public HtmlFactory(ILogger loggerEngineInstance, IExecutionContext exeContext)
        {
            LoggerEngine = loggerEngineInstance;
            ExecutionContext = exeContext;
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
            TableHelper tbl = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "namespaceList");
            tbl.Remove();

            tbl = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Class, "titleTable");
            tbl.removeColumn(0);
            SpanHelper spIntro = new SpanHelper(htmlDocument._loadedDocument, SpanHelper.SearchFilter.Class, "introStyle");
            spIntro.InnerHtml("Select a service name in the left menu to display the available web service methods.");
            tbl.SetCellDisplayValue("titleColumn", "Web Services");//Web Services for third party integrators
            htmlDocument.Save();
        }        

        public static void FormatServiceDocument(DocumentHelper htmlDocument)
        {
            var summaryText = htmlDocument._loadedDocument.DocumentNode.Descendants("div").FirstOrDefault(x => x.ParentNode.Name == "div" && x.ParentNode.Id == "TopicContent" && x.InnerText.Contains("Service Characteristics"));

            if (summaryText != null)
            {
                DivHelper divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "ID4RBSection");
                
                if (divHelper != null)
                {                    
                    divHelper.addChildrenNode(summaryText);
                    divHelper.removeChildByName("h4");                    
                }
            }
        }

        public void preparePropertiesTable(string originalDocument)
        {
            string fullSourcePath = originalDocument;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            AddHtmlBlocks.addDataTypeColumn(htmlDocument._loadedDocument);
            htmlDocument.Save();
            htmlDocument.Load();
            fillDataTypeColumn(htmlDocument._loadedDocument);
            htmlDocument.Save();
            htmlDocument.Load();
            RemoveHtmlBlocks.removeHiperLinkNameColumn(htmlDocument._loadedDocument);
            htmlDocument.Save();
            htmlDocument.Load();
            RemoveHtmlBlocks.removeIconColumn(htmlDocument._loadedDocument);
            htmlDocument.Save();

            htmlDocument.Load();
            DivHelper divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "ID4RBSection");/////////ID3RBSection
            divHelper.removeStyleClass("collapsibleSection");
            htmlDocument.Save();
        }

        /// <summary>
        /// getServiceListFromPageContent
        /// create directory folder and handle exceptions
        /// </summary>
        /// <param name="contentPage">Page content that hold a list of links of services</param>
        /// <returns>A list of services in the given content page, each item in the list are still html links, format should be applied to read.</returns>
        public List<string> getServiceListFromPageContent(string contentPage, string targetPath)
        {
            string fullSourcePath = targetPath + contentPage;
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = fullSourcePath;
            htmlDocument.Load();
            TableHelper tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
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

        public void fillDataTypeColumn(HtmlDocument htmlDoc)
        {
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            TableHelper tableHelper = new TableHelper(htmlDoc, TableHelper.SearchFilter.Id, "propertyList");
            List<string> files = tableHelper.readColumnValues(1);
            int j;
            int x = 1;

            foreach (var file in files)
            {
                if (!string.IsNullOrEmpty(file))
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

        public string CreateOnclickAttribute(DocumentHelper htmlDocument)
        {
            TableHelper tbl = null;
            string response = string.Empty;

            tbl = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id,"propertyList");
            if (tbl._ContextTable != null)
            {
                List<string> dataTypeColumn = tbl.readColumnValues(2);
                int x = 1;
                var tmpNode = NodeHelper.GetNode(htmlDocument._loadedDocument);
                tmpNode.Name = "div";
                string NewCellValue = string.Empty;
                string hrefAttri = string.Empty;
                bool isList = false;
                bool isEnumerable = false;

                foreach (var file in dataTypeColumn)
                {
                    if (!string.IsNullOrEmpty(file))
                    {
                        if (file.StartsWith("List"))
                        {
                            isList = true;
                            tmpNode.InnerHtml = file.Replace("List", string.Empty).Replace("&lt;", string.Empty).Replace("&gt;", string.Empty);
                        }else if (file.StartsWith("IEnumerable"))
                        {
                            isEnumerable = true;
                            tmpNode.InnerHtml = file.Replace("IEnumerable", string.Empty).Replace("&lt;", string.Empty).Replace("&gt;", string.Empty);
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
                            } else if (isEnumerable)
                            {
                                NewCellValue = "IEnumerable &lt;" + tmpNode.ChildNodes[0].OuterHtml + "&gt;";
                            }
                            else
                            {
                                NewCellValue = tmpNode.ChildNodes[0].OuterHtml;
                            }

                            tbl.SetCellDisplayValue(2, x, NewCellValue);
                        }
                    }
                    x++;
                    isList = false;
                    isEnumerable = false;
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

            using (StreamWriter sw = File.CreateText(Path.GetDirectoryName(htmlDocument._documentPath) + @"\" + "HtmlBlock_" + Path.GetFileName(htmlDocument._documentPath)))
            {
                sw.WriteLine(tableToHtmlBlock);
                sw.Flush(); 
            }

            htmlDocument.Load();
        }

        public string CreateTablePageHtmlBlock(DocumentHelper htmlDocument)
        {
            string response = string.Empty;
            DivHelper divTopicContent = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "TopicContent");
            return divTopicContent.GetInnerHtml();
        }

        public void CreatePageHtmlBlocks(string page)
        {
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = page;
            htmlDocument.Load();
            var tableToHtmlBlock = CreateTablePageHtmlBlock(htmlDocument);

            using (StreamWriter sw = File.CreateText(Path.GetDirectoryName(htmlDocument._documentPath) + @"\" + Path.GetFileName(htmlDocument._documentPath)))
            {
                sw.WriteLine(tableToHtmlBlock);
                sw.Flush();
            }

            htmlDocument.Load();
        }

        public void setPaginationControl(string contentPage, string targetTable, string targetDivContainer)
        {
            // get rown num in list
            var htmlDocument = DocumentHelper.GetInstance();
            htmlDocument._documentPath = contentPage;
            htmlDocument.Load();
            TableHelper tableHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, targetTable);
            StringBuilder sb = new StringBuilder();
            HtmlNode tableNode = NodeHelper.GetNode(htmlDocument._loadedDocument);
            tableNode.Name = "table";
            tableNode.AddClass("table table-striped table-bordered table-sm");
            tableNode.Id = "methodList";
            //creating boostrap table

            // produce <thead></thead>
            sb.Append("<thead><tr>");

            if (!tableHelper._ContextTableLoaded)
            {
                return;
            }

            foreach (var col in tableHelper._ts._tableHeaderColumns)
            {
                sb.Append("<th>");
                sb.Append(col.Value);
                sb.Append("</th>");
            }
            sb.Append("</tr></thead>");

            // produce <tbody>
            string bodyTable = tableHelper._innerHtmltableCollection.Replace(sb.ToString().Replace("<thead>", string.Empty).Replace("</thead>", string.Empty), string.Empty);

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

            if (ModuleSettings == null || ModuleSettings.ContractListPages.ListPage.Any())
            {
                //adding checkbox control to filter published events
                DivHelper divCheckboxHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "ID0RBSection");
                divCheckboxHelper.SetInnerHtml("<div class=\"collapsibleSection\" id=\"ID0RBSection\"><form><div class=\"form-check\"><input id=\"includeUnpublished\" onclick=\"FilterPublished(this);\" type=\"checkbox\"><label for=\"includeUnpublished\">Include Unpublished</label></div></form></div>");
            }

            //collapsibleRegionTitle
            DivHelper divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, targetDivContainer);
            divHelper.addChildrenNode(tableNode);
            htmlDocument.Save();

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

            //adding script blocks to html tag
            var htmlNode = htmlDocument._loadedDocument.DocumentNode.SelectNodes("//html").FirstOrDefault();
            node = NodeHelper.GetNode(htmlDocument._loadedDocument);
            node.Name = "script";
            node.InnerHtml = "$(document).ready(function(){$('#methodList').DataTable();$('.dataTables_length').addClass('bs-select'); });";
            htmlNode.ChildNodes.Add(node);

            divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "collapsibleAreaRegion");
            divHelper.RemoveCollectionMatch();
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
            EvtView.SetInnerHtml("DXC Payer Platform");
            tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "namespaceList");
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
                tblHelper = new TableHelper(innerHtmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
                strTableEvents.Append(tblHelper.GetInnerHtml());
                innerHtmlDocument.Save();
            }

            //printing table
            htmlDocument._documentPath = ModuleSettings.WebTargetPath + Common.Constants.WebSolutionStructure.Folders.Html + ModuleSettings.MainContractContent;
            htmlDocument.Load();
            divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Id, "ID0RBSection");

            HtmlNode tableevntNode = NodeHelper.GetNode(htmlDocument._loadedDocument);
            tableevntNode.AddClass("members");
            tableevntNode.Name = "table";
            tableevntNode.Id = "classList";
            tableevntNode.InnerHtml = strTableEvents.ToString();
            divHelper.addChildrenNode(tableevntNode);
            htmlDocument.Save();

            htmlDocument.Load();
            SpanHelper spanHelper = new SpanHelper(htmlDocument._loadedDocument, SpanHelper.SearchFilter.Class, "collapsibleRegionTitle");
            spanHelper.RemoveNode();// removing 

            tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
            tblHelper.removeColumn(0);
            tblHelper.renameColumnHeader(0, "Event");
            htmlDocument.Save();

            htmlDocument.Load();
            tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
            List<string> col = tblHelper.readColumnValues(0).Where(v => v != "Class").ToList();
            int x = 1;
            HtmlNode tmpNode = NodeHelper.GetNode(htmlDocument._loadedDocument);
            foreach (string val in col)
            {
                tmpNode.Name = "span";
                tmpNode.InnerHtml = val;
                tmpNode.ChildNodes[0].Attributes.Add("onClick", "window.open('" + ModuleSettings.WebHost + @"\" + ModuleSettings.WebTargetPath.Replace(ModuleSettings.WebHostPhysicalPath, string.Empty) + @"\" + Common.Constants.WebSolutionStructure.Folders.Html + @"\" + tmpNode.ChildNodes[0].Attributes["href"].Value + "', 'MyWindow','width=800,height=450,toolbar=no,menubar=no,status=no,resizable=yes,scrollbars=yes'); return false;");
                tmpNode.ChildNodes[0].Attributes["href"].Value = "#";
                tblHelper.SetCellDisplayValue(0, x, tmpNode.InnerHtml);
                x++;
            }

            tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Class, "titleTable");
            tblHelper.Remove();
            tblHelper.SetCellDisplayValue("titleColumn", string.Empty);
            tblHelper.removeColumn(0);
            divHelper = new DivHelper(htmlDocument._loadedDocument, DivHelper.SearchFilter.Class, "summary");
            divHelper.Remove();
            SpanHelper spIntro = new SpanHelper(htmlDocument._loadedDocument,  SpanHelper.SearchFilter.Class, "introStyle");
            spIntro.RemoveNode();
            htmlDocument.Save();

            if (ModuleSettings.ContractListPages.ListPage.Any())
            {
                //Adding a published column
                htmlDocument.Load();
                tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
                tblHelper.addColumn("Published");
                htmlDocument.Save();

                //Load extract
                if (!string.IsNullOrEmpty(ModuleSettings.EventsExtractFile) & File.Exists(ModuleSettings.EventsExtractFile))
                {
                    CSVExtractHelper CSVExtract = new CSVExtractHelper();
                    var eventsExtractFile = CSVExtract.GetExtract(ModuleSettings.EventsExtractFile);

                    if (eventsExtractFile != null & eventsExtractFile.Any())
                    {
                        htmlDocument.Load();
                        tblHelper = new TableHelper(htmlDocument._loadedDocument, TableHelper.SearchFilter.Id, "classList");
                        List<string> events = tblHelper.readColumnValues(0);
                        HtmlNode aRefEventNode = NodeHelper.GetNode(htmlDocument._loadedDocument);

                        int rowIndex = 1;    //Skipping header row (index zero)
                        int columnIndex = 2; //Published column
                        foreach (string eventName in events)
                        {
                            aRefEventNode.Name = "a";
                            aRefEventNode.InnerHtml = eventName;
                            if (eventsExtractFile.Where(o => o.EVENT_TYPE_NAME.Equals(aRefEventNode.InnerText)
                                                           & o.MODULE_NAME.Equals(ModuleSettings.ModuleNameDisplay)).Count() >= 1)
                            {
                                tblHelper.SetCellDisplayValue(columnIndex, rowIndex, "Yes");
                            }
                            else
                            {
                                tblHelper.SetCellDisplayValue(columnIndex, rowIndex, "No");
                            }
                            rowIndex++;
                        }

                        htmlDocument.Save();
                    }
                }
            }  
            else
            {
                Console.WriteLine("This Module hasn't events");
            }
        }        
    }
}
