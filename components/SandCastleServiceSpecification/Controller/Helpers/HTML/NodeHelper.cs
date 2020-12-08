
using HtmlAgilityPack;

namespace Controller.Helpers.HTML
{
    public static class NodeHelper
    {
        static int nodeIndex = 0;
        public static HtmlNode GetNode(HtmlDocument Document)
        {
            nodeIndex++;
            return new HtmlNode(HtmlNodeType.Element, Document, nodeIndex);
        }
    }
}
