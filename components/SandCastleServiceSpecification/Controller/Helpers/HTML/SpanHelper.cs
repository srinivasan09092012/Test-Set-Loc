using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISvcSpec.Helpers
{
    public class SpanHelper
    {
        private HtmlDocument _htmlDoc;
        private int _htmlNodeIndex;
        private string _SpanId;
        private string _SpanStyleClass;
        private int _searchBy;
        
        public SpanHelper(HtmlDocument htmlDoc, string spanId)
        {
            this._htmlDoc = htmlDoc;
            this._SpanId = spanId;
            this._htmlNodeIndex = 0;
        }

        public SpanHelper(HtmlDocument htmlDoc, string spanStyleClass, int byClass)
        {
            this._htmlDoc = htmlDoc;
            this._SpanStyleClass = spanStyleClass;
            this._htmlNodeIndex = 0;
        }

        public SpanHelper(HtmlDocument htmlDoc, string spanId, string spanStyleClass)
        {
            this._htmlDoc = htmlDoc;
            this._SpanId = spanId;
            this._SpanStyleClass = spanStyleClass;
            this._htmlNodeIndex = 0;
            this._searchBy = 2;
        }

        public bool RemoveNode()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//span[@class='" + this._SpanStyleClass + "']");

            if (n != null && n.Count > 0)
            {
                n.FirstOrDefault().Remove();
            }
            return true;
        }

        public bool RemoveNode(string all)
        {
            foreach (var spanTag in _htmlDoc.DocumentNode.SelectNodes("//span[@class='" + this._SpanStyleClass + "']"))
            {
                spanTag.Remove();
            }

            return true;
        }

        public bool RemoveNodeById()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//span[@id='" + this._SpanId + "']").FirstOrDefault();
            n.Remove();
            return true;
        }

        public bool RenameChildNode(string label)
        {
            var spanTags = _htmlDoc.DocumentNode.SelectNodes("//span[@class='" + this._SpanStyleClass + "']");
            if (spanTags.Count >= 1)
            {
                int spanTagCounter = 1;
                foreach (var node in spanTags)
                {
                    if (node.HasChildNodes)
                    {
                        node.ChildNodes.Where(x => x.Name == "#text").FirstOrDefault().InnerHtml = label;
                        node.Id = this._SpanStyleClass + "_" + spanTagCounter.ToString();
                        spanTagCounter++;
                    }
                }
            }

            return true;
        }
    }
}
