using APISvcSpec.Helpers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace APISvcSpec
{
    public static class HtmlFactory
    {
        private static HtmlDocument htmlDoc = new HtmlDocument();
        private static List<TableStructure> TablesInHtmlDoc = new List<TableStructure>();
        private static int _htmlNodeIndex = 0;
        private static HtmlNode newNode;
        static int nodeIndex = 0;

        public static void SetUnderConstruction(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            htmlDoc.LoadHtml(File.ReadAllText(fullSourcePath));

            //remove content
            DivHelper divHelper = new DivHelper(htmlDoc, "TopicContent");
            divHelper.removeChildNodes();
            divHelper.addTableChildrenNode("<table class='titleTable'><tr><td class='logoColumn'><img alt='DXC.Technology' width='120' height='120' src='../icons/dxc_logo_vt_blk_rgb_300.png'></td><td class='titleColumn'><h1>Provider Query Service</h1></td></tr></table>");

            //place under construction label/icon
            divHelper.addNewLine();
            divHelper.addNewLine();
            divHelper.addNewLine();
            divHelper.addNewLine();
            divHelper.addNewLine();
            divHelper.addNewLine();
            divHelper.addImgChildrenNode("<img alt='dxc.technology'  width='520' src='../icons/underConstruction.jpg'>");

            //save doc
            htmlDoc.Save(fullSourcePath, Encoding.UTF8);
        }

        public static void updateIndexFile(string webFolderTarget, string originalDocument, string newUrl)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            htmlDoc.LoadHtml(File.ReadAllText(fullSourcePath));
            updateLinkToHtmlFolder(htmlDoc, newUrl);
            htmlDoc.Save(fullSourcePath, Encoding.UTF8);
        }

        public static void updateCmdAndEventServiceList(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            htmlDoc.LoadHtml(File.ReadAllText(fullSourcePath));
            updateServiceList(htmlDoc, webFolderTarget);
            htmlDoc.Save(fullSourcePath, Encoding.UTF8);
        }

        public static void updateCmdAndEventServiceListForQuery(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            htmlDoc.LoadHtml(File.ReadAllText(fullSourcePath));
            updateServiceListForQuery(htmlDoc, webFolderTarget);
            htmlDoc.Save(fullSourcePath, Encoding.UTF8);
        }

        public static void addDataTypeColumnToCommandPages(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            htmlDoc.LoadHtml(File.ReadAllText(originalDocument));
            addDataTypeColumn(htmlDoc);
            htmlDoc.Save(originalDocument, Encoding.UTF8);

            htmlDoc.LoadHtml(File.ReadAllText(originalDocument));
            updatePropertyList(htmlDoc, webFolderTarget);
            htmlDoc.Save(originalDocument, Encoding.UTF8);

            htmlDoc.LoadHtml(File.ReadAllText(originalDocument));
            updatePropertyListRemoveHiperlink(htmlDoc, webFolderTarget);
            htmlDoc.Save(originalDocument, Encoding.UTF8);

            htmlDoc.LoadHtml(File.ReadAllText(originalDocument));
            removeIxonColumn(htmlDoc);
            htmlDoc.Save(originalDocument, Encoding.UTF8);

            htmlDoc.LoadHtml(File.ReadAllText(originalDocument));
            preparePropertyBodyCommandsandEvents(htmlDoc);
            htmlDoc.Save(originalDocument, Encoding.UTF8);
        }

        public static void preparePropertyBodyCommandsandEvents(HtmlDocument doc)
        {
            DivHelper divHelper = new DivHelper(doc,"PageHeader");
            divHelper.RemoveNodeById();
            divHelper = new DivHelper(doc, "leftNav");
            divHelper.RemoveNodeById();
            divHelper = new DivHelper(doc, "TopicContent");
            divHelper.removeClass("topicContent");
            divHelper = new DivHelper(doc, "pageFooter");
            divHelper.RemoveNodeById();
            TableHelper tableHelper = new TableHelper(doc, "", "titleTable");
            tableHelper.removeRow(0);
            TextHelper txtHelper = new TextHelper(doc, "Top");
            txtHelper.removeTagByText("ID4RBSection");
        }

        public static List<string> getServices(string webFolderTarget, string originalDocument, string moduleName)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            HtmlDocument htmlDocMainPage = new HtmlDocument();

            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDocMainPage.LoadHtml(cleanHTMLDoc.ToString());

            TableHelper tblHelper = new TableHelper(htmlDocMainPage, "classList");
      
            return tblHelper.readColumnValues(0);
        }

        public static List<string> FormatServices(string webFolderTarget, List<string> serviceList)
        { 
            List<string> paths = new List<string>();

            foreach (string service in serviceList)
            {
                paths.Add((webFolderTarget + Common.Constant.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension));
            }

            return paths;
        }

        public static void UpdateALLOperationListPage(string webFolderTarget, string originalDocument, string ModuleName)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            HtmlDocument htmlDocMainPage = new HtmlDocument();

            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDocMainPage.LoadHtml(cleanHTMLDoc.ToString());

            TableHelper tblHelper = new TableHelper(htmlDocMainPage, "classList");
            List<string> services = tblHelper.readColumnValues(0);


            foreach (string service in services)
            {
                ////left navigator in T
                UpdateOperationListPage(webFolderTarget, Common.Constant.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, ModuleName);
            }
        }

        public static void UpdateALLTableList(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            HtmlDocument htmlDocMainPage = new HtmlDocument();

            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDocMainPage.LoadHtml(cleanHTMLDoc.ToString());

            TableHelper tblHelper = new TableHelper(htmlDocMainPage, "classList");
            List<string> services = tblHelper.readColumnValues(0);

            foreach (string service in services)
            {
                if (service.Contains("Query"))
                {
                    APISvcSpec.HtmlFactory.UpdateQueryMethodsTableList(webFolderTarget + Common.Constant.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension);
                }
                else
                {
                    APISvcSpec.HtmlFactory.UpdateMethodsTableList(webFolderTarget + Common.Constant.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension);
                }
            }
        }

        public static void UpdateInputOutput(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            HtmlDocument htmlDocMainPage = new HtmlDocument();

            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDocMainPage.LoadHtml(cleanHTMLDoc.ToString());

            TableHelper tblHelper = new TableHelper(htmlDocMainPage, "classList");
            List<string> services = tblHelper.readColumnValues(0);

            foreach (string service in services)
            {
                if (service.Contains("Query"))
                {
                    APISvcSpec.HtmlFactory.updateCmdAndEventServiceListForQuery(webFolderTarget, Common.Constant.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension);
                }
                else
                {
                    APISvcSpec.HtmlFactory.updateCmdAndEventServiceList(webFolderTarget, Common.Constant.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension);
                }
            }
        }

        public static void RemoveALLHiperlinks(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            HtmlDocument htmlDocMainPage = new HtmlDocument();

            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDocMainPage.LoadHtml(cleanHTMLDoc.ToString());

            TableHelper tblHelper = new TableHelper(htmlDocMainPage, "classList");
            List<string> services = tblHelper.readColumnValues(0);

            foreach (string service in services)
            {
                APISvcSpec.HtmlFactory.removeHIperlink(webFolderTarget + Common.Constant.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension);
            }
        }

        public static void AddPaginationControl(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            HtmlDocument htmlDocMainPage = new HtmlDocument();

            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDocMainPage.LoadHtml(cleanHTMLDoc.ToString());

            TableHelper tblHelper = new TableHelper(htmlDocMainPage, "classList");
            List<string> services = tblHelper.readColumnValues(0);

            foreach (string service in services)
            {
                paginateIOServiceList(webFolderTarget + Common.Constant.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension);
            }
        }

        public static void RemoveEmptyParams(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            HtmlDocument htmlDocMainPage = new HtmlDocument();

            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDocMainPage.LoadHtml(cleanHTMLDoc.ToString());

            TableHelper tblHelper = new TableHelper(htmlDocMainPage, "classList");
            List<string> services = tblHelper.readColumnValues(0);

            foreach (string service in services)
            {
                removeEmptyTags(webFolderTarget, Common.Constant.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension, "p");
            }
        }

        public static void UpdateLeftNavigator(string originalDocument, string mooduleName, string mainPage, List<string> ServiceList)
        {
            htmlDoc.LoadHtml(File.ReadAllText(originalDocument));
            UpdatePageLeftMenu(htmlDoc, "div", originalDocument, mooduleName, mainPage, ServiceList);
            htmlDoc.Save(originalDocument, Encoding.UTF8);
        }

        public static void UpdateCMDEventsPages(string originalDocument, string mooduleName, string mainPage, List<string> ServiceList)
        {
            htmlDoc.LoadHtml(File.ReadAllText(originalDocument));
            UpdateBodyPage(htmlDoc, "div", originalDocument, mooduleName, mainPage);
            htmlDoc.Save(originalDocument, Encoding.UTF8);
        }

        private static void addDataTypeColumn(HtmlDocument doc)
        {
            //adding column
            TableHelper tableHelper = new TableHelper(doc, "propertyList");
            tableHelper.addTableColumn("Data Type");
        }

        private static void removeIxonColumn(HtmlDocument doc)
        {
            //adding column
            TableHelper tableHelper = new TableHelper(doc, "propertyList");
            tableHelper.removeColumn(0);
        }

        public static void updatePropertyList(HtmlDocument doc, string webFolderTarget)
        {   //update with data type value
            HtmlNode newNode = new HtmlNode(HtmlNodeType.Element, doc, 0);
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            TableHelper tableHelper = new TableHelper(doc, "propertyList");
            List<string> r = tableHelper.readColumnValues(1);
            int j;
            int x = 1;

            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, "ID1RBSection");
                newNode.InnerHtml = divHelper.GetChildValueByTag("span");
                tableHelper.SetCellDisplayValue(3, x, newNode.InnerText);
                x++;
            }
        }

        public static void updatePropertyListRemoveHiperlink(HtmlDocument doc, string webFolderTarget)
        {
            //update with data type value
            HtmlNode newNode = new HtmlNode(HtmlNodeType.Element, doc, 0);
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            TableHelper tableHelper = new TableHelper(doc, "propertyList");
            List<string> r = tableHelper.readColumnValues(1);
            int x = 1;
            foreach (var file in r)
            {
                newNode.InnerHtml = file;
                tableHelper.SetCellDisplayValue(1, x, newNode.InnerText);
                x++;
            }
        }

        private static void updateLinkToHtmlFolder(HtmlDocument doc, string newUrl)
        {
            var headTag = doc.DocumentNode.SelectNodes("//head").FirstOrDefault().ChildNodes.Where(x => x.Name == "script").FirstOrDefault();
            headTag.InnerHtml = "window.location.replace('html" + newUrl +"')";
        }

        public static void formatDL(string path)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(path);
            Helpers.dlHelper diHelper = new Helpers.dlHelper(doc);
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

        public static void updateServiceList(HtmlDocument doc, string webFolderTarget)
        {
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            TableHelper tableHelper = new TableHelper(doc, "methodList");
            List<string> r = tableHelper.readColumnValues(0);
            int x = 1;
            int j;
            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                formatDL((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, "ID0RBSection");
                tableHelper.SetCellDisplayValue(2, x, divHelper.GetChildValueByTag("a"));
                x++;
            }

            x = 1;
            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, "ID1RBSection");
                tableHelper.SetCellDisplayValue(3, x, divHelper.GetChildValueByTag("a"));
                x++;
            }
        }
        
        public static void updateServiceListForQuery(HtmlDocument doc, string webFolderTarget)
        {
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            HtmlDocument innerHtmlDocLvl2 = new HtmlDocument();
            HtmlDocument innerHtmlDocLvl3 = new HtmlDocument();

            TableHelper tableHelper = new TableHelper(doc, "methodList");
            TableHelper tblHelperlevl2;

           
            HtmlNode myNode ;
            List<HtmlNode> myNodeList = new List<HtmlNode>();

            List<string> r = tableHelper.readColumnValues(0);
            int x = 1;
            int j;
            Random rand = new Random(0);
            foreach (var file in r)
            {
                j = (webFolderTarget + @"html\" + @file.Remove(0, 9).Split('>')[0]).Length;
                innerHtmlDoc.Load((webFolderTarget + @"\html\" + @file.Remove(0, 9).Split('>')[0]).Substring(0, j));
                DivHelper divHelper = new DivHelper(innerHtmlDoc, "ID1RBSection");
                string linkfilelevel2 = divHelper.GetChildValueByTagForQuery("a");

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
                            myNode = new HtmlNode(HtmlNodeType.Element, doc, rand.Next());
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
                            divHelper = new DivHelper(innerHtmlDocLvl3, "ID1RBSection");
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
                DivHelper divHelper = new DivHelper(innerHtmlDoc, "ID1RBSection");
                string linkfilelevel2 = divHelper.GetChildValueByTagForQuery("a");
                if (linkfilelevel2 != string.Empty)
                {
                    j = (webFolderTarget + @"html\" + linkfilelevel2.Remove(0, 9).Split('>')[0]).Length;
                    innerHtmlDocLvl2.Load((webFolderTarget + @"\html\" + linkfilelevel2.Remove(0, 9).Split('>')[0]).Substring(0, j));
                    divHelper = new DivHelper(innerHtmlDocLvl2, "ID0RBSection");
                    tableHelper.SetCellDisplayValue(3, x, divHelper.GetChildValueByTag("a"));
                }
                x++;
            }
        }

        public static void UpdateLandingPage(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            htmlDoc.LoadHtml(File.ReadAllText(fullSourcePath));
            UpdateLadingPage(htmlDoc);
            htmlDoc.Save(fullSourcePath, Encoding.UTF8);
        }

        public static void UpdateOperationListPage(string webFolderTarget, string originalDocument, string ModuleName)
        {
            string fullSourcePath = webFolderTarget + originalDocument;

            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }


            htmlDoc.LoadHtml(cleanHTMLDoc.ToString());
            OperationListPage(htmlDoc, ModuleName);
            htmlDoc.Save(fullSourcePath, Encoding.UTF8);
        }

        public static void removeEmptyTags(string webFolderTarget, string originalDocument,string tagName)
        {
            string fullSourcePath = webFolderTarget + originalDocument;

            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDoc.LoadHtml(cleanHTMLDoc.ToString());

            var bodyTag = htmlDoc.DocumentNode.SelectNodes("//body").FirstOrDefault();

            if (bodyTag.HasChildNodes)
            {
                removeTag(bodyTag, tagName);
            }

            htmlDoc.Save(fullSourcePath, Encoding.UTF8);
        }

        public static bool removeTag(HtmlNode node, string tagName)
        {
            if (node.Name == tagName && node.InnerHtml.Trim() == string.Empty) //&& node.InnerHtml.Trim() == string.Empty)
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

        private static void OperationListPage(HtmlDocument doc, string ModuleName)
        {
            TableHelper tableHelper = new TableHelper(doc, string.Empty, "titleTable");
            tableHelper.SetCellDisplayValue("titleColumn", tableHelper.GetCellDisplayValue("titleColumn").Replace("Class", string.Empty));

            SpanHelper spanHelper = new SpanHelper(doc, "collapsibleRegionTitle", 1);
            spanHelper.RenameChildNode("Service Operations");
            spanHelper = new SpanHelper(doc, "introStyle", 1);
            spanHelper.RemoveNode();

            spanHelper = new SpanHelper(doc, "collapsibleRegionTitle_1");
            spanHelper.RemoveNodeById();

            spanHelper = new SpanHelper(doc, "collapsibleRegionTitle_2");
            spanHelper.RemoveNodeById();

            spanHelper = new SpanHelper(doc, "nolink", 1);
            spanHelper.RemoveNode("all");

            spanHelper = new SpanHelper(doc, "selflink", 1);
            spanHelper.RemoveNode("all");

            DivHelper divHelper = new DivHelper(doc, "ID0RBSection");
            divHelper.RemoveNodeById();

            divHelper = new DivHelper(doc, "seeAlsoSection");
            divHelper.RemoveNodeById();

            divHelper = new DivHelper(doc, "ID4RBSection");
            divHelper.RemoveNodeById();

            divHelper = new DivHelper(doc, "codeSnippetContainer", 1);
            divHelper.RemoveNode();

            divHelper = new DivHelper(doc, "ID2RBSection");
            divHelper.RemoveNodeById();

            TextHelper txtHelper = new TextHelper(doc, "Namespace:");
            txtHelper.removeTagByText("TopicContent");

            txtHelper = new TextHelper(doc, "HP.HSP.UA3."+ ModuleName + ".BAS.");
            txtHelper.removeTagByTextStartWith("TopicContent");

            txtHelper = new TextHelper(doc, "Assembly:");
            txtHelper.removeTagByText("TopicContent");

            //txtHelper = new TextHelper(doc, "HP.HSP.UA3.ProviderManagement.BAS.ProviderSvc (in HP.HSP.UA3.ProviderManagement.BAS.ProviderSvc.dll) Version: 19.1.64.0 (19.1.64.0)");
            txtHelper = new TextHelper(doc, "HP.HSP.UA3.DrugRebate.BAS.DrugRebate (in HP.HSP.UA3.DrugRebate.BAS.DrugRebate.dll) Version: 19.1.64.0 (19.1.64.0)");
            txtHelper.removeTagByText("TopicContent");

            //txtHelper = new TextHelper(doc, "HP.HSP.UA3." + ModuleName + ".BAS.");
            //txtHelper.removeTagByTextStartWith("TopicContent");
        }

        private static void UpdateLadingPage(HtmlDocument doc)
        {
            // calling table helper
            Helpers.TableHelper tableHelper = new TableHelper(doc, string.Empty, "titleTable");
            tableHelper.SetCellDisplayValue("titleColumn", "BAS and Events For Third Party Integrators");

            // calling span helper
            SpanHelper spanHelper = new SpanHelper(doc, "collapsibleRegionTitle", 1);
            spanHelper.RenameChildNode("Service List");

            //calling div helper
            Helpers.DivHelper divHelper = new DivHelper(doc, Common.Constant.HtmlInventory.PageFooterDiv);
            divHelper.removeChildNodes();
            divHelper.RenameDivInnerHtml(Common.Constant.Labels.CopyRight);

            // calling table helper
            tableHelper = new TableHelper(doc, "classList");
            tableHelper.removeColumn(0);
            tableHelper.renameColumnHeader(0, "Service Name");
        }
        
        private static void UpdatePageLeftMenu(HtmlDocument doc, string mainNode, string docPath, string moduleName, string mainPage, List<string> ServiceList)
        {
            foreach (var node in doc.DocumentNode.SelectNodes("//" + mainNode))
            {
                if (node.HasClass("toclevel2"))
                {
                    node.Remove();
                }

                if (node.HasClass("toclevel0"))
                {
                    if (node.HasChildNodes)
                    {
                        foreach (var childNode in node.ChildNodes.Where(x => x.Name == "a"))
                        {
                            foreach (var childNodeAttribute in childNode.Attributes.Where(x => x.Name == "tocid"))
                            {
                                if (childNodeAttribute.Value != "roottoc")
                                {
                                    node.Remove();
                                    break;
                                }
                                else if (childNodeAttribute.Value == "roottoc")
                                {
                                    childNode.Attributes["href"].Value = "#!";
                                    break;
                                }
                            }
                        }
                    }
                }
                
                if (node.HasClass("toclevel1"))
                {
                    if (node.HasChildNodes)
                    {
                        foreach (var childNode in node.ChildNodes.Where(x => x.Name == "a"))
                        {
                            foreach (var childNodeAttribute in childNode.Attributes.Where(x => x.Name == "tocid"))
                            {
                                //if (childNodeAttribute.Value.Contains("_HP_HSP_UA3_"+ moduleName +"_BAS_Providers"))
                                if (childNodeAttribute.Value.Contains(Path.GetFileNameWithoutExtension(mainPage.Replace("N_",string.Empty))))
                                {
                                    childNode.InnerHtml = moduleName + " API";
                                    childNode.Attributes["href"].Value = "../html/" + mainPage;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //remove Class key word from itle
            var titleNode = doc.DocumentNode.SelectNodes("//title").FirstOrDefault();
            titleNode.InnerHtml = titleNode.InnerText.Replace("Class", string.Empty);

            // add items to menu
            DivHelper divHelper = new DivHelper(doc, "tocNav");

            foreach (string service in ServiceList)
            {
                divHelper.addChildrenNode("<a class='tocCollapsed' onclick='javascript: Toggle(this);''href='#!' /><a data-tochassubtree='true' href='.." + Common.Constant.WebSolutionStructure.Folders.Html + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension + "' title='Provider Service' tocid='Methods_T_HP_HSP_UA3_" + moduleName + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + "'>" + service.Split('>')[1].Split('<')[0] + "</a>");
            }

            UpdatePageHeader(doc);

            UpdatePageFooter(doc);
        }

        public static void UpdatePageHeader(HtmlDocument doc)
        {
            DivHelper divHelper = new DivHelper(doc, Common.Constant.HtmlInventory.PageHeaderDiv);
            divHelper.RenameDivInnerHtml(Common.Constant.Labels.ProductName);
        }

        public static void UpdatePageFooter(HtmlDocument doc)
        {
            DivHelper divHelper = new DivHelper(doc, Common.Constant.HtmlInventory.PageFooterDiv);
            divHelper.removeChildNodes();
            divHelper.RenameDivInnerHtml(Common.Constant.Labels.CopyRight);
        }

        public static void UpdateBodyPage(HtmlDocument doc, string mainNode, string docPath, string moduleName, string mainPage)
        {
            TableHelper tableHelper;

            //refactored method
            // common section
            // calling div helper to remove copy - Syntax section
            DivHelper divHelper = new DivHelper(doc, Common.Constant.HtmlInventory.PageSyntaxisClassDiv, 1);
            divHelper.RemoveNode();

            // calling span helper to remove Syntax header
            SpanHelper spanHelper = new SpanHelper(doc, Common.Constant.HtmlInventory.PageSyntaxiHeadersClassSpan, 1);
            spanHelper.RemoveNode();

            //calling div hhelper to remove reference on the botton section
            divHelper = new DivHelper(doc, "ID6RBSection");
            divHelper.RemoveNodeById();

            // calling div helper to remove methods section 
            divHelper = new DivHelper(doc, "ID5RBSection");
            divHelper.RemoveNodeById();

            // calling div helper to remove empty section 
            divHelper = new DivHelper(doc, "ID2RBSection");
            divHelper.RemoveNodeById();

            //removing 
            divHelper = new DivHelper(doc, "collapsibleAreaRegion", 1);
            divHelper.RemoveAllNodes();

            // calling div helper to remove see also header
            divHelper = new DivHelper(doc, "seeAlsoSection");
            divHelper.RemoveNodeById();

            // calling div helper to remove see also header
            divHelper = new DivHelper(doc, "ID3RBSection");
            if (!divHelper.RemoveNodeById())
            {
                divHelper = new DivHelper(doc, "ID2RBSection");
                divHelper.RemoveNodeById();
            }

            divHelper = new DivHelper(doc, "ID0RBSection");
            divHelper.RemoveNodeById();

            spanHelper = new SpanHelper(doc, "introStyle", 1);
            spanHelper.RemoveNode();

            TextHelper txtHelper = new TextHelper(doc, "Namespace:");
            txtHelper.removeTagByText("TopicContent");

            txtHelper = new TextHelper(doc, "Assembly:");
            txtHelper.removeTagByText("TopicContent");

            //txtHelper = new TextHelper(doc, "HP.HSP.UA3.ProviderManagement.BAS.ProviderSvc.Contracts (in HP.HSP.UA3.ProviderManagement.BAS.ProviderSvc.Contracts.dll) Version: 19.1.64.0 (19.1.64.0)");
            txtHelper = new TextHelper(doc, "HP.HSP.UA3.DrugRebate.BAS.DrugRebate.Contracts (in HP.HSP.UA3.DrugRebate.BAS.DrugRebate.Contracts.dll) Version: 19.1.64.0 (19.1.64.0)");
            txtHelper.removeTagByText("TopicContent");

            //////////////////txtHelper = new TextHelper(doc, "HP.HSP.UA3." + moduleName + ".BAS.");
            //////////////////txtHelper.removeTagByTextStartWith("TopicContent");
            //end common section

            if (Path.GetFileName(docPath).Contains("_Contracts_Commands_"))
            {
                tableHelper = new TableHelper(doc, string.Empty, "titleTable");
                tableHelper.SetCellDisplayValue("titleColumn", tableHelper.readTdDisplayValueByClass("titleColumn").Replace("Class", string.Empty));

                txtHelper = new TextHelper(doc, "HP.HSP.UA3." + moduleName + ".BAS.");
                txtHelper.removeTagByTextStartWith("TopicContent");
            }

            if (Path.GetFileName(docPath).Contains("_Contracts_Events_"))
            {
                tableHelper = new TableHelper(doc, string.Empty, "titleTable");
                tableHelper.SetCellDisplayValue("titleColumn", tableHelper.readTdDisplayValueByClass("titleColumn").Replace("Class", "Event"));

                txtHelper = new TextHelper(doc, "HP.HSP.UA3." + moduleName + ".BAS.");
                txtHelper.removeTagByTextStartWith("TopicContent");

                txtHelper = new TextHelper(doc, "event exposes the following attributes.");
                txtHelper.renameTagByText("TopicContent", "type exposes the following members.");
            }

            if (Path.GetFileName(docPath).Contains("_Contracts_Queries_Parameters_"))
            {
                tableHelper = new TableHelper(doc, string.Empty, "titleTable");
                tableHelper.SetCellDisplayValue("titleColumn", tableHelper.readTdDisplayValueByClass("titleColumn").Replace("Class", string.Empty));

               txtHelper = new TextHelper(doc, "HP.HSP.UA3." + moduleName + ".BAS.");
               txtHelper.removeTagByTextStartWith("TopicContent");
            }

            if (Path.GetFileName(docPath).Contains("_Contracts_Domain_"))
            {
                tableHelper = new TableHelper(doc, string.Empty, "titleTable");
                tableHelper.SetCellDisplayValue("titleColumn", tableHelper.readTdDisplayValueByClass("titleColumn").Replace("Class", string.Empty));

                txtHelper = new TextHelper(doc, "HP.HSP.UA3." + moduleName + ".BAS.");
                txtHelper.removeTagByTextStartWith("TopicContent");
            }
            //end by page type
        }

        public static void removeText(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            HtmlDocument htmlDocMainPage = new HtmlDocument();
            TextHelper txtHelper;
            StringBuilder cleanHTMLDoc = new StringBuilder();

            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDocMainPage.LoadHtml(cleanHTMLDoc.ToString());

            // remove top link
            txtHelper = new TextHelper(htmlDocMainPage, "exposes the following members.");
            txtHelper.removeTagByTextClass("topicContent");
            txtHelper = new TextHelper(htmlDocMainPage, "Top");
            txtHelper.removeTagByTextClass("collapsibleSection");

            // remove search form
            DivHelper dhelper = new DivHelper(htmlDocMainPage, "PageHeader");
            dhelper.removeChildByName("form");

            htmlDocMainPage.Save(fullSourcePath, Encoding.UTF8);
        }

        public static void removeTextFromServices(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            HtmlDocument htmlDocMainPage = new HtmlDocument();

            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDocMainPage.LoadHtml(cleanHTMLDoc.ToString());

            TableHelper tblHelper = new TableHelper(htmlDocMainPage, "classList");
            List<string> services = tblHelper.readColumnValues(0);

            foreach (string service in services)
            {
                removeText(webFolderTarget, Common.Constant.WebSolutionStructure.Folders.Html + "/" + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension);
            }
        }

        public static void addLinksToLeftNavFromServices(string webFolderTarget, string originalDocument)
        {
            string fullSourcePath = webFolderTarget + originalDocument;
            HtmlDocument htmlDocMainPage = new HtmlDocument();

            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(fullSourcePath))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDocMainPage.LoadHtml(cleanHTMLDoc.ToString());

            TableHelper tblHelper = new TableHelper(htmlDocMainPage, "classList");
            List<string> services = tblHelper.readColumnValues(0);

            DivHelper divHelper = new DivHelper(htmlDocMainPage, "tocNav");

            foreach (string service in services)
            {
                divHelper.addChildrenNode("<a class='tocCollapsed' onclick='javascript: Toggle(this);''href='#!' /><a data-tochassubtree='true' href='.." + Common.Constant.WebSolutionStructure.Folders.Html + "/" + @service.Remove(0, 9).Split('>')[0].Split('.')[0] + Common.Constant.WebSolutionStructure.Files.Extensions.htmlExtension + " title='Provider Service' tocid='Methods_T_HP_HSP_UA3_ProviderManagement_BAS_Providers_'"+ @service.Remove(0, 9).Split('>')[0].Split('.')[0] + ">"+ @service.Remove(0, 9).Split('>')[0].Split('.')[0] + "</a>");
            }

            htmlDocMainPage.Save(fullSourcePath, Encoding.UTF8);
        }

        public static void removeHIperlink(string doc)
        {
            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(doc))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDoc.LoadHtml(cleanHTMLDoc.ToString());

            // removed hiperlink in column
            //update with data type value
            HtmlNode newNode = new HtmlNode(HtmlNodeType.Element, htmlDoc, 0);
            HtmlDocument innerHtmlDoc = new HtmlDocument();
            TableHelper tableHelper = new TableHelper(htmlDoc, "methodList");
            List<string> r = tableHelper.readColumnValues(0);
            int x = 1;
            foreach (var file in r)
            {
                newNode.InnerHtml = file;
                tableHelper.SetCellDisplayValue(0, x, newNode.InnerText);
                x++;
            }

            htmlDoc.Save(doc, Encoding.UTF8);
        }

        public static void UpdateMethodsTableList(string doc)
        {
            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(doc))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDoc.LoadHtml(cleanHTMLDoc.ToString());
            TableHelper tableHelper = new TableHelper(htmlDoc, "methodList");
            tableHelper.addTableColumn("Input Command");
            tableHelper.addTableColumn("Event returned");
            tableHelper.renameColumnHeader(1, "Operation Name");
            tableHelper.removeColumn(0);
            htmlDoc.Save(doc, Encoding.UTF8);
        }

        public static void paginateIOServiceList(string doc)
        {
            // get rown num in list
            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(doc))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDoc.LoadHtml(cleanHTMLDoc.ToString());

            TableHelper tableHelper = new TableHelper(htmlDoc, "methodList");
            StringBuilder sb = new StringBuilder();
            HtmlNode tableNode = new HtmlNode(HtmlNodeType.Element, htmlDoc, 4);
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
            tableHelper.RemoveTable();
            htmlDoc.Save(doc, Encoding.UTF8);


            cleanHTMLDoc.Clear();
            foreach (var line in File.ReadLines(doc))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDoc.LoadHtml(cleanHTMLDoc.ToString());
            DivHelper divHelper = new DivHelper(htmlDoc, "ID3RBSection");
            divHelper.addChildrenNode(tableNode);
            htmlDoc.Save(doc, Encoding.UTF8);

            #region add links to head tag
            var divNodes = htmlDoc.DocumentNode.SelectNodes("//head").FirstOrDefault();
            HtmlNode node = new HtmlNode(HtmlNodeType.Element, htmlDoc, 0);
            node.Name = "link";
            node.Attributes.Add("href", "../css/bootstrap.min.css");
            node.Attributes.Add("rel", "stylesheet");
            divNodes.ChildNodes.Add(node);

            node = new HtmlNode(HtmlNodeType.Element, htmlDoc, 1);
            node.Name = "link";
            node.Attributes.Add("href", "../css/mdb.min.cs");
            node.Attributes.Add("rel", "stylesheet");
            divNodes.ChildNodes.Add(node);

            node = new HtmlNode(HtmlNodeType.Element, htmlDoc, 2);
            node.Name = "link";
            node.Attributes.Add("href", "../css/style.css");
            node.Attributes.Add("rel", "stylesheet");
            divNodes.ChildNodes.Add(node);

            node = new HtmlNode(HtmlNodeType.Element, htmlDoc, 3);
            node.Name = "link";
            node.Attributes.Add("href", "../css/addons/datatables.min.css");
            node.Attributes.Add("rel", "stylesheet");
            divNodes.ChildNodes.Add(node);
            #endregion


            //adding script blocks to body tag
            var headNode = htmlDoc.DocumentNode.SelectNodes("//head").FirstOrDefault();
            node = new HtmlNode(HtmlNodeType.Element, htmlDoc, 5);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/jquery-3.4.0.min.js");
            headNode.ChildNodes.Add(node);

            node = new HtmlNode(HtmlNodeType.Element, htmlDoc, 6);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/popper.min.js");
            headNode.ChildNodes.Add(node);

            node = new HtmlNode(HtmlNodeType.Element, htmlDoc, 7);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/bootstrap.min.js");
            headNode.ChildNodes.Add(node);

            node = new HtmlNode(HtmlNodeType.Element, htmlDoc, 8);
            node.Name = "script";
            node.Attributes.Add("type", "text/javascript");
            node.Attributes.Add("src", "../js/addons/datatables.min.js");
            headNode.ChildNodes.Add(node);

            //adding script blocks to html tag
            var htmlNode = htmlDoc.DocumentNode.SelectNodes("//html").FirstOrDefault();
            node = new HtmlNode(HtmlNodeType.Element, htmlDoc, 9);
            node.Name = "script";
            node.InnerHtml = "$(document).ready(function(){$('#methodList').DataTable();$('.dataTables_length').addClass('bs-select'); });";
            htmlNode.ChildNodes.Add(node);

            htmlDoc.Save(doc, Encoding.UTF8);
        }

        public static void UpdateQueryMethodsTableList(string doc)
        {
            StringBuilder cleanHTMLDoc = new StringBuilder();
            foreach (var line in File.ReadLines(doc))
            {
                if (line.Trim() != string.Empty)
                {
                    cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                    ////TODO: CLEAN THE HTML IN THIS WAY, MAKE THE SINGLE READ METHOD FOR ALL THE EDITIONS.
                }
            }

            htmlDoc.LoadHtml(cleanHTMLDoc.ToString());
            TableHelper tableHelper = new TableHelper(htmlDoc, "methodList");
            tableHelper.addTableColumn("Input Query Parameter");
            tableHelper.addTableColumn("Query Collection Result");
            tableHelper.renameColumnHeader(1, "Operation Name");
            tableHelper.removeColumn(0);

            htmlDoc.Save(doc, Encoding.UTF8);
        }
    }
    } 
