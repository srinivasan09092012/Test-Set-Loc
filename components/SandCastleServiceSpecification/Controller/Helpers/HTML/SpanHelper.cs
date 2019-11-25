using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Helpers.HTML
{
    public class SpanHelper
    {
        private HtmlDocument _htmlDoc;
        private int _htmlNodeIndex;
        private string _SpanId;
        private string _SpanStyleClass;
        private int _searchBy;
        
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
            var n = _htmlDoc.DocumentNode.SelectNodes("//span[@class='" + this._SpanStyleClass + "']");

            if (n != null && n.Count > 0)
            {
                foreach (var spanTag in n)
                {
                    spanTag.Remove();
                }
            }

            return true;
        }

        public void InnerHtml(string text)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//span[@class='" + this._SpanStyleClass + "']");
            if (n != null)
            {
                n.FirstOrDefault().InnerHtml = text;
            }
        }
    }
}
