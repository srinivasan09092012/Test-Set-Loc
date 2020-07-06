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
        private string _id;
        private string _class;
        private short _selectedFilter;
        public HtmlNode _ContextSpan;
        public bool _ContextSpanLoaded;

        public enum SearchFilter : short
        {
            Name = 1,
            Id = 2,
            Class = 4,
        };

        public SpanHelper(HtmlDocument htmlDoc, SearchFilter filter, string criteria)
        {
            this._htmlDoc = htmlDoc;
            this._htmlNodeIndex = 1000;
            this._selectedFilter = (short)filter;

            switch (_selectedFilter)
            {
                case 1:
                    this.Load("//span[@Name='" + criteria + "']");
                    break;
                case 2:
                    this.Load("//span[@id='" + criteria + "']");
                    break;
                default:
                    this.Load("//span[@class='" + criteria + "']");
                    break;
            }

            if (!this._ContextSpanLoaded)
            {
                Console.WriteLine("Requested Span Not found. Criteria: " + criteria + " Filter: " + filter.ToString());
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
                this._ContextSpan = DivNodes.FirstOrDefault();
                this._ContextSpanLoaded = true;
            }
            else
            {
                _ContextSpanLoaded = false;
            }
        }

        public void RemoveNode()
        {
            if (_ContextSpanLoaded)
            {
                _ContextSpan.Remove();
            }
        }

        //<summary>
        //RemoveAllOccurences
        //Remove all spans with the specified style
        //</summary>
        public bool RemoveAllOccurences()
        {
            var selectedNode = _htmlDoc.DocumentNode.SelectNodes("//span[@class='" + this._class + "']");

            if (selectedNode != null && selectedNode.Count > 0)
            {
                foreach (var spanTag in selectedNode)
                {
                    spanTag.Remove();
                }
            }

            return true;
        }

        public void InnerHtml(string text)
        {
            if (_ContextSpanLoaded)
            {
                _ContextSpan.InnerHtml = text;
            }
        }
    }
}
