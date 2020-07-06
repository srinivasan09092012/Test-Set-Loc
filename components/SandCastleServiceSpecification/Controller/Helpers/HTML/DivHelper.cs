using HtmlAgilityPack;
using System.Linq;

namespace Controller.Helpers.HTML
{
    public class DivHelper
    {
        private HtmlDocument _htmlDoc;   
        public HtmlNode _ContextDiv; 
        public HtmlNodeCollection _ContextDivCollection;
        private bool _ContextDivLoaded = false;
        private int _htmlNodeIndex;
        private short _selectedFilter;
        public enum SearchFilter : short
        {
            Name = 1,
            Id = 2,
            Class = 4,
        };

        //<summary>
        //DivHelper Constructor
        //<param name = "htmlDoc">HTMLAgilityPack.HtmlDocument object, pre loaded, helper will target the search to it  </param>
        //<param name = "filter">Enum intented to support more than one search filter, By Name, Id and Class</param>
        //<param name = "criteria">Search criteria for the selected search filter</param>
        //</summary>
        public DivHelper(HtmlDocument htmlDoc, SearchFilter filter, string criteria)
        {
            this._htmlDoc = htmlDoc;
            this._htmlNodeIndex = 1000;
            this._selectedFilter = (short)filter;

            switch (_selectedFilter)
            {
                case 1:
                    this.Load("//div[@Name='" + criteria + "']");
                    break;
                case 2:
                    this.Load("//div[@id='" + criteria + "']");
                    break;
                default:
                    this.Load("//div[@class='" + criteria + "']");
                    break;
            }
        }

        //<summary>
        //Load
        //Search for the given DIV in the way selected in constructor
        //<param name = "xpath">spath string to search in the html document for the DIV requested.</param>
        //</summary>
        private void Load(string xpath)
        {
            var DivNodes = _htmlDoc.DocumentNode.SelectNodes(xpath);

            if (DivNodes != null && DivNodes.Count() > 0)
            {
                this._ContextDiv = DivNodes.FirstOrDefault();
                this._ContextDivCollection = DivNodes;
                this._ContextDivLoaded = true;
            }
            else
            {
                _ContextDivLoaded = false;
            }
        }

        //<summary>
        //Load
        //Search for the given DIV in the way selected in constructor
        //<param name = "xpath">spath string to search in the html document for the DIV requested.</param>
        //</summary>
        ///TODO: THIS CODE NOT SURE WHERE SHOULD BE, BUT NOT HERE, DOUBLE CHECK.
        public string GetChildValueByTag(string childNodeTag, string WebHost, string WebHostPhysicalPath, string WebTargetPath)
        {
            if (this._ContextDivLoaded)
            {
                var ContextDivChild = this._ContextDiv.ChildNodes.Where(x => x.Name == childNodeTag && x.Attributes.Contains("href"));

                if (ContextDivChild != null & ContextDivChild.Count() > 0)
                {
                    ContextDivChild.FirstOrDefault().Attributes.Add("onClick", "window.open('" + WebHost + @"\" + WebTargetPath.Replace(WebHostPhysicalPath, string.Empty) + @"\" + Common.Constants.WebSolutionStructure.Folders.Html  + @"\"+  ContextDivChild.FirstOrDefault().Attributes["href"].Value + "', 'MyWindow','width=800,height=450,toolbar=no,menubar=no,status=no,resizable=yes,scrollbars=yes'); return false;");
                    ContextDivChild.FirstOrDefault().Attributes["href"].Value = "#";
                    return ContextDivChild.FirstOrDefault().OuterHtml;
                }
            }

            return string.Empty;
        }

        //<summary>
        // GetChildNodeOuterHtml
        // Get the outerHtml of a child in the context DIV
        // Search by Node Name
        //<param name = "childNodeName">  </param>
        ///<returns>String</returns>
        //</summary>
        public string GetChildNodeOuterHtml(string childNodeName)
        {
            if (this._ContextDivLoaded)
            {
                var ContextDivChildNodes = this._ContextDiv.ChildNodes.Where(x => x.Name == childNodeName).Select(y => y.OuterHtml);

                if (ContextDivChildNodes != null & ContextDivChildNodes.Count() > 0)
                {
                    return ContextDivChildNodes.FirstOrDefault();
                }
            }

            return string.Empty;
        }

        //<summary>
        //removeChildByName
        //Remove a Child Node from the DIV Parent Node in Context
        //<param name = "divName"> Name of the Child Div Node to delelete</param>
        ///<returns>Void</returns>
        //</summary>
        public void removeChildByName(string NodeName)
        {
            if (this._ContextDivLoaded)
            {
                if (this._ContextDiv.HasChildNodes)
                {
                    var ContextDivChildNodes = this._ContextDiv.ChildNodes.Where(x => x.Name == NodeName);

                    if (ContextDivChildNodes != null & ContextDivChildNodes.Count() > 0)
                    {
                        ContextDivChildNodes.FirstOrDefault().Remove(); 
                    }
                }
            }
        }

        //<summary>
        //ReplaceInnerHtml
        //Replace the current InnerHTML of the conext DIV
        //<param name = "html">string variable that holds the new html code to set </param>
        ///<returns>Void</returns>
        //</summary>
        public void SetInnerHtml(string html)
        {
            if (this._ContextDivLoaded)
            {
                this._ContextDiv.InnerHtml = html;
            }
        }

        //<summary>
        //GetInnerHtml
        //Get the current InnerHTML of the conext DIV
        ///<returns>Void</returns>
        //</summary>
        public string GetInnerHtml()
        {
            if (this._ContextDivLoaded)
            {
                return this._ContextDiv.InnerHtml;
            }

            return string.Empty;
        }

        //<summary>
        //Remove
        //Remove the Context DIV and all of it content in from given HTML doc
        ///<returns>Void</returns>
        //</summary>
        public void Remove()
        {
            if (this._ContextDivLoaded)
            {
                this._ContextDiv.Remove();
            }
        }
 
        public void RemoveCollectionMatch()
        {
            if (this._ContextDivLoaded)
            {
                foreach (var div in this._ContextDivCollection)
                {
                   div.Remove();
                }
                
            }
        }

        //<summary>
        //removeAllChildNodes
        //Remove all the Child Nodes from the DIV Parent Node in Context
        ///<returns>Void</returns>
        //</summary>
        public void removeAllChildNodes()
        {
            if (this._ContextDivLoaded)
            {
                this._ContextDiv.ChildNodes.Clear();
            }
        }

        //<summary>
        //addChildrenNode
        //Add a children node to the context DIV on the given HTML doc
        //<param name = "htmlNode"> HTMLAgilityPack.HtmlNode to add as child note to the context DIV</param>
        ///<returns>Void</returns>
        //</summary>
        public void addChildrenNode(HtmlNode htmlNode)
        {
            if (this._ContextDivLoaded)
            {
                this._ContextDiv.ChildNodes.Add(htmlNode);
            }
        }

        public void InsertChildrenNode(HtmlNode htmlNode)
        {
            if (this._ContextDivLoaded)
            {
                if (this._ContextDiv.HasChildNodes)
                {
                    this._ContextDiv.ChildNodes.Insert(this._ContextDiv.ChildNodes.Count, htmlNode);
                }
            }
        }

        //<summary>
        //removeClass
        //Remove the class style from the context DIV
        ///<returns>Void</returns>
        //</summary>
        public void removeStyleClass(string StyleClass)
        {
            if (this._ContextDivLoaded)
            {
                this._ContextDiv.RemoveClass(StyleClass);
            }
        }

        public bool Exists()
        {
            return this._ContextDivLoaded;
        }
    }
}
