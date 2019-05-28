using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace APISvcSpec.Helpers
{
    public class dlHelper
    {
        private HtmlDocument _htmlDoc;
        private int _htmlNodeIndex = 0;
        private HtmlNode newLineNode;

        public dlHelper(HtmlDocument doc )
        {
            this._htmlDoc = doc;
        }
        private HtmlNode CreateNode()
        {
            _htmlNodeIndex++;
            return new HtmlNode(HtmlNodeType.Element, _htmlDoc, _htmlNodeIndex);
        }

        public List<HtmlNode> formatDescriptionList(string containerTag)
        {
            List<HtmlNode> result = new List<HtmlNode>();

            var divNodes = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + containerTag + "']").FirstOrDefault();
            var DivChildrenNode = divNodes.ChildNodes.Where(x => x.Name == "dl");

            foreach (var Node in DivChildrenNode)
            {
                if (Node.HasChildNodes)
                {
                    foreach (var child in Node.ChildNodes)
                    {
                        if (child.Name == "dt")
                        {
                            newLineNode = CreateNode();
                            newLineNode.Name = "h4";
                            newLineNode.AddClass("subHeading");
                            newLineNode.InnerHtml = "Input " + child.FirstChild.InnerHtml;
                            result.Add(newLineNode);
                        }

                        if (child.Name == "dd")
                        {
                            foreach (var n in child.ChildNodes)
                            {
                                if (n.Name == "a")
                                {
                                    n.InnerHtml = n.Attributes.FirstOrDefault().Value.Split('_').Last().Split('.').FirstOrDefault();
                                    result.Add(n);
                                    newLineNode = CreateNode();
                                    newLineNode.Name = "br";
                                    result.Add(newLineNode);
                                     
                                }
                            }

                            result.Add(child.ChildNodes.Last());
                            newLineNode = CreateNode();
                            newLineNode.Name = "br";
                            result.Add(newLineNode);
                            result.Add(newLineNode);

                        }
                    }
                }

                DivChildrenNode.FirstOrDefault().Remove();
                break;
            }

            return result;
        }
    }
}
