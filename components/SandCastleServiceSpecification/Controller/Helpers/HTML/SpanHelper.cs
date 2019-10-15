﻿using HtmlAgilityPack;
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

        public bool RemoveNodeById()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//span[@id='" + this._SpanId + "']");

            if (n != null)
            {
                n.FirstOrDefault().Remove();
            }
            
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

        public void RenameChildNode(string ChildNodeName, string Label)
        {
            var spanTags = _htmlDoc.DocumentNode.SelectNodes("//span[@class='" + this._SpanStyleClass + "']");
            if (spanTags.Count >= 1)
            {
                spanTags.FirstOrDefault().ChildNodes.Where(x => x.Name == "#text").FirstOrDefault().InnerHtml = Label;
            }
        }

        public void removeClass()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//span[@class='" + this._SpanStyleClass + "']").FirstOrDefault();
            n.RemoveClass();
        }

        public void addClass(string styleClass)
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//span[@class='" + this._SpanStyleClass + "']").FirstOrDefault();
            n.AddClass(styleClass);
        }

        public bool RemoveNodeByClass()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//span[@class='" + this._SpanStyleClass + "']").FirstOrDefault();
            n.Remove();
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
