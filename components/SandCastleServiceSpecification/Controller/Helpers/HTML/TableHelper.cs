using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Controller.Helpers.HTML
{
    public class TableHelper
    {
        private HtmlDocument _htmlDoc;
        public HtmlNode _ContextTable;
        public TableStructure _ts;
        public List<List<string>> rows = new List<List<string>>();
        public string _innerHtmltableCollection;
        public bool _tableFound = false;
        public string _tableId = string.Empty;
        public string _tableStyleClass = string.Empty;
        public int _htmlNodeIndex = 0;

        private short _selectedFilter;
        public bool _ContextTableLoaded = false;

        public enum SearchFilter : short
        {
            Name = 1,
            Id = 2,
            Class = 4,
        };

        //<summary>
        //TableHelper Constructor
        //<param name = "htmlDoc">HTMLAgilityPack.HtmlDocument object, pre loaded, helper will target the search to it  </param>
        //<param name = "filter">Enum intented to support more than one search filter, By Name, Id and Class</param>
        //<param name = "criteria">Search criteria for the selected search filter</param>
        //</summary>
        public TableHelper(HtmlDocument htmlDoc, SearchFilter filter, string criteria)
        {
            this._htmlDoc = htmlDoc;
            this._htmlNodeIndex = 1000;
            this._selectedFilter = (short)filter;

            switch (_selectedFilter)
            {
                case 1:
                    this.Load("//table[@Name='" + criteria + "']");
                    break;
                case 2:
                    this.Load("//table[@id='" + criteria + "']");
                    break;
                default:
                    this.Load("//table[@class='" + criteria + "']");
                    break;
            }

            if (!this._ContextTableLoaded)
            {
                Console.WriteLine("Requested Table Not found. Criteria: " + criteria + " Filter: " + filter.ToString());
            }
            else
            {
                Console.WriteLine("Table found. Criteria: " + criteria + " Filter: " + filter.ToString() + " getting structure");
                _ts = new TableStructure(criteria);
                this.GetTableStructure(criteria);
                Console.WriteLine("Getting structure done");
            }
        }

        //<summary>
        //Load
        //Search for the given DIV in the way selected in constructor
        //<param name = "xpath">spath string to search in the html document for the DIV requested.</param>
        //</summary>
        private void Load(string xpath)
        {
            var tableNodes = _htmlDoc.DocumentNode.SelectNodes(xpath);

            if (tableNodes != null)
            {
                this._ContextTable = tableNodes.FirstOrDefault();
                this._ContextTableLoaded = true;
            }
            else
            {
                _ContextTableLoaded = false;
            }
        }

        public void Remove()
        {
            if (this._ContextTableLoaded)
            {
                this._ContextTable.Remove();
            }
        }

        private HtmlNode CreateNode()
        {
            _htmlNodeIndex++;
            return new HtmlNode(HtmlNodeType.Element, _htmlDoc, _htmlNodeIndex);
        }

        private void addTableColumnHeader(string headerLabel)
        {
            if (this._ContextTableLoaded)
            {
                HtmlNode newHeader = CreateNode();
                newHeader.Name = "th";
                newHeader.InnerHtml = headerLabel;
                this._ContextTable.ChildNodes.FirstOrDefault().ChildNodes.Add(newHeader);
            }
        }

        /// <summary>
        /// Add new column at the right
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool addColumn(string columnName)
        {
            if (this._ContextTableLoaded)
            {
                this.addTableColumnHeader(columnName);
                HtmlNode newColumn = CreateNode();
                newColumn.Name = "td";
                newColumn.InnerHtml = string.Empty;

                if (_ts._columnCount == 0)
                    return false;
                
                var rows = this._ContextTable.ChildNodes;

                foreach (var row in rows.Skip(1))
                {
                    row.ChildNodes.Add(newColumn);
                }
                
                newColumn = null;
            }
            return true;
        }

        public bool removeRow(int rowIndex)
        {
            if (this._ContextTableLoaded)
            {
                try
                {
                    foreach (var node in this._ContextTable.ChildNodes)
                    {
                        if (node.HasChildNodes)
                        {
                            foreach (var row in node.ChildNodes)
                            {
                                if (row.HasChildNodes)
                                {
                                    row.ChildNodes[rowIndex].Remove();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        public void removeColumn(int columnIndex)
        {
            if (this._ContextTableLoaded)
            {
                try
                {
                    if (this._ContextTable.HasChildNodes)
                    {
                        foreach (var row in this._ContextTable.ChildNodes)
                        {
                            if (row.HasChildNodes)
                            {
                                row.ChildNodes[columnIndex].Remove();
                            }
                        }
                    }
                    
                }
                catch
                {
                }
            }
        }

        public void renameColumnHeader(int columnIndex, string columnHeader)
        {
            if (this._ContextTableLoaded)
            {
                if (this._ContextTable.HasChildNodes)
                {
                    var rows = this._ContextTable.ChildNodes;
                    var headerRow = rows.FirstOrDefault();
                    try
                    {
                        headerRow.ChildNodes[columnIndex].InnerHtml = columnHeader;
                    }
                    catch (Exception exe)
                    {
                        Console.WriteLine("error when renameColumnHeader " + exe.Message);
                    }
                }
            }
        }
   
        public void SetCellDisplayValue(int columnIndex, int rowIndex, string label)
        {
            if (this._ContextTableLoaded)
            {
                var allRows = this._ContextTable.ChildNodes;
                var row = allRows[rowIndex];
                row.ChildNodes[columnIndex].InnerHtml = label;
            }
        }

        public void SetCellDisplayValue(string tdClassId, string label)
        {
            if (this._ContextTableLoaded)
            {
                try
                {
                    foreach (var node in this._ContextTable.ChildNodes)
                    {
                        if (node.HasChildNodes)
                        {
                            foreach (var child in node.ChildNodes)
                            {
                                foreach (var childlvl in child.ChildNodes)
                                {
                                    if (childlvl.HasClass(tdClassId))
                                    {
                                        if (childlvl.HasChildNodes)
                                        {
                                            childlvl.ChildNodes.FirstOrDefault().InnerHtml = label;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                { }
            }
        }

        public string GetCellDisplayValue(string tdClassId)
        {
            if (this._ContextTableLoaded)
            {
                foreach (var node in this._ContextTable.ChildNodes)
                {
                    if (node.HasChildNodes)
                    {
                        foreach (var child in node.ChildNodes)
                        {
                            foreach (var childlvl in child.ChildNodes)
                            {
                                if (childlvl.HasClass(tdClassId))
                                {
                                    if (childlvl.HasChildNodes)
                                    {
                                        return childlvl.ChildNodes.FirstOrDefault().InnerHtml;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        private void GetTableStructure(string id)
        {
            if (this._ContextTableLoaded)
            {
                int columnCount = 0;
                int rowCount = 0;
                Dictionary<int, string> tableHeaderColumns = new Dictionary<int, string>();
                List<string> tmpRow = new List<string>();
                this._innerHtmltableCollection = this._ContextTable.InnerHtml;
                if (this._ContextTable.HasChildNodes)
                {
                    foreach (var row in this._ContextTable.ChildNodes)
                    {
                        if (row.HasChildNodes)
                        {
                            rowCount++;
                            foreach (var column in row.ChildNodes)
                            {
                                if (rowCount == 1)
                                {
                                    tableHeaderColumns.Add(columnCount, column.InnerHtml);
                                    columnCount++;
                                }
                                else
                                {
                                    tmpRow.Add(column.InnerHtml);
                                }
                            }

                            if (tmpRow.Count > 0)
                            {
                                rows.Add(tmpRow);
                            }

                            tmpRow = null;
                            tmpRow = new List<string>();

                        }
                    }
                }
                
                _ts._tableId = id;
                _ts._rowCount = rowCount;
                _ts._columnCount = columnCount;
                _ts._tableHeaderColumns = tableHeaderColumns;
                _ts._isValid = true;
            }
        }

        public List<string> readColumnValues(int indexColumn)
        {
            if (this._ContextTableLoaded)
            {
                if (_ts is null)
                {
                    _ts = new TableStructure(this._tableId);
                    this.GetTableStructure(this._tableId);
                }

                List<string> response = new List<string>();

                foreach (List<string> rows in rows)
                {
                    response.Add(rows[indexColumn]);
                }

                return response;
            }
            else
            {
                return new List<string>();
            }
        }

        public string readTdDisplayValueByClass(string tdClass)
        {
            if (this._ContextTableLoaded)
            {
                foreach (var node in this._ContextTable.ChildNodes)
                {
                    if (node.HasChildNodes)
                    {
                        foreach (var child in node.ChildNodes)
                        {
                            foreach (var childlvl in child.ChildNodes)
                            {
                                if (childlvl.HasClass(tdClass))
                                {
                                    if (childlvl.HasChildNodes)
                                    {
                                        return childlvl.ChildNodes.FirstOrDefault().InnerHtml;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        //<summary>
        //GetInnerHtml
        //Get the current InnerHTML of the conext DIV
        ///<returns>string content inner html</returns>
        //</summary>
        public string GetInnerHtml()
        {
            if (this._ContextTableLoaded)
            {
                return this._ContextTable.InnerHtml;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
