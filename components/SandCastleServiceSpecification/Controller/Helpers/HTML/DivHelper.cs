using HtmlAgilityPack;
using System.Linq;

namespace APISvcSpec.Helpers
{
    public class DivHelper
    {
        private HtmlDocument _htmlDoc;
        private int _htmlNodeIndex;
        private string _divId;
        private string _divStyleClass;

        public DivHelper(HtmlDocument htmlDoc, string divId)
        {
            this._htmlDoc = htmlDoc;
            this._divId = divId;
            this._htmlNodeIndex = 1000;
        }

        public DivHelper(HtmlDocument htmlDoc, string divStyleClass, int byClass)
        {
            this._htmlDoc = htmlDoc;
            this._divStyleClass = divStyleClass;
            this._htmlNodeIndex = 1000;
        }

        public DivHelper(HtmlDocument htmlDoc, string divId, string divStyleClass)
        {
            this._htmlDoc = htmlDoc;
            this._divId = divId;
            this._divStyleClass = divStyleClass;
            this._htmlNodeIndex = 1000;
        }

        public string GetChildValueByTag(string childNodeTag)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']");

            if (n != null)
            {
                if (n.FirstOrDefault().HasChildNodes)
                {
                    foreach (var child in n.FirstOrDefault().ChildNodes)
                    {
                        if (child.Name == childNodeTag)
                        {
                            if (child.Attributes.Contains("href"))
                            {
                                child.Attributes.Add("onClick", "window.open('" + child.Attributes["href"].Value + "', 'MyWindow','width=800,height=850,toolbar=no,menubar=no,status=no,resizable=no'); return false;");
                                child.Attributes["href"].Value = "#";
                            }
                            return child.OuterHtml;
                        }
                    }
                }
            }

            return string.Empty;
        }

        public string GetChildValueByTagForQuery(string childNodeTag)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();

            if (n.HasChildNodes)
            {
                foreach (var child in n.ChildNodes)
                {
                    if (child.Name == childNodeTag)
                    {
                        return child.OuterHtml;
                    }
                }
            }

            return string.Empty;
        }

        private HtmlNode CreateNode()
        {
            _htmlNodeIndex++;
            return new HtmlNode(HtmlNodeType.Element, _htmlDoc, _htmlNodeIndex);
        }

        public bool removeChildByName(string tagName)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();

            if (n.HasChildNodes)
            {
               foreach(var node in n.ChildNodes)
               {
                    if (node.Name == tagName)
                    {
                        node.Remove();
                        break;
                    }

               }
            }

            return true;
        }

        public bool removeChildNodes()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();

            if (n.HasChildNodes)
            {
                int x = 0;
                while (x <= n.ChildNodes.Count - 1)
                {
                    n.ChildNodes.RemoveAt(x);
                }
            }

            return true;
        }

        public bool RenameDivInnerHtml(string label)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();
            n.InnerHtml = label;
            return true;
        }

        public bool RemoveNode()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@class='" + this._divStyleClass + "']");

            if (n != null && n.Count > 0)
            {
                n.FirstOrDefault().Remove();
            }
            
            return true;
        }

        public bool RemoveAllNodes()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@class='" + this._divStyleClass + "']");

            if (n != null)
            {
                foreach (var node in n)
                {
                    node.Remove();
                }
            }
            
            return true;
        }

        public bool addCleanChildrenNode(string htmlString)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();
            HtmlNode newNode = CreateNode();
            newNode.Name = "p";
            newNode.InnerHtml = htmlString;
            n.ChildNodes.Add(newNode);
            newNode = null;
            return true;
        }

        public bool addChildrenNode(HtmlNode newNode)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();
            n.ChildNodes.Add(newNode);
            return true;
        }

        public bool addChildrenNode(string htmlString)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();
            HtmlNode newNode = CreateNode();
            newNode.Name = "div";
            newNode.AddClass("toclevel2");
            newNode.Attributes.Add("data-toclevel", "2");
            newNode.InnerHtml = htmlString;
            n.ChildNodes.Add(newNode);
            newNode = null;
            return true;
        }

        public bool addImgChildrenNode(string htmlString)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();
            HtmlNode newNode = CreateNode();
            newNode.Name = "p";
            newNode.Attributes.Add("align", "middle");
            newNode.InnerHtml = htmlString;
            n.ChildNodes.Add(newNode);
              newNode = null;
            return true;
        }

        public bool addNewLine()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();
            HtmlNode newNode = CreateNode();
            newNode.Name = "br";
            n.ChildNodes.Add(newNode);
            newNode = null;
            return true;
        }

        public bool addTableChildrenNode(string htmlString)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();
            HtmlNode newNode = CreateNode();
            newNode.Name = "table";
            newNode.InnerHtml = htmlString;
            n.ChildNodes.Add(newNode);
            newNode = null;
            return true;
        }

        public bool RemoveNodeById()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']");

            if (n != null)
                n.FirstOrDefault().Remove();
            else
            {
                return false;
            }

            return true;
        }


        public bool removeClass(string NodeClass)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();

            if (n.HasClass(NodeClass))
            {
                n.RemoveClass(NodeClass);
            }
            return true;
        }

        //TODO: how to know when to delete using id or class


        public bool updateAreChildNode()
        {
            try
            {
                var n = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + this._divId + "']").FirstOrDefault();

                foreach (var child in n.ChildNodes)
                {
                    if (child.Name == "a")
                    {
                        child.Attributes.Add("onClick", "window.open('" + child.Attributes["href"].Value + "', 'MyWindow','width=800,height=850,toolbar=no,menubar=no,status=no,resizable=no'); return false;");
                        child.Attributes["href"].Value = "#";
                    }
                }
            }
            catch
            { return false; }
            return true;
        }
    }
}
