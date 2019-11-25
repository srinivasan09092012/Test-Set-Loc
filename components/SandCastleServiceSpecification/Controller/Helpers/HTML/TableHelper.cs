using HtmlAgilityPack;
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
        private string _tableId = string.Empty;
        private string _tableStyleClass = string.Empty;
        private int _htmlNodeIndex = 0;
        public List<List<string>> rows = new List<List<string>>();
        public string InnerHtmltableCollection;

        public TableHelper(HtmlDocument htmlDoc, string tableId) 
        {
            _htmlDoc = htmlDoc;
            this._tableId = tableId;
            _ts = new TableStructure(this._tableId);
            this.GetTableStructure(this._tableId);
        }

        public TableHelper(HtmlDocument htmlDoc, string tableId, string tableStyleClass) 
        {
            _htmlDoc = htmlDoc;
            this._tableId = tableId;
            this._tableStyleClass = tableStyleClass;
        }

        public void Remove()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//table[@id='" + this._tableId + "']");

            if (n != null)
            {
                n.FirstOrDefault().Remove();
            }
        }

        public void Delete()
        {
            var n = _htmlDoc.DocumentNode.SelectNodes("//table[@class='" + this._tableStyleClass + "']");

            if (n != null)
            {
                n.FirstOrDefault().Remove();
            }
        }

        private HtmlNode CreateNode()
        {
            _htmlNodeIndex++;
            return new HtmlNode(HtmlNodeType.Element, _htmlDoc, _htmlNodeIndex);
        }
        private void addTableColumnHeader(string headerLabel)
        {
             if (_ts is null)
             {
                 _ts = new TableStructure(this._tableId);
                 this.GetTableStructure(this._tableId);
             }
             HtmlNode newHeader = CreateNode();
             newHeader.Name = "th";
             newHeader.InnerHtml = headerLabel;

             if (_ts._tableHeaderColumns.Select(x => x.Value == headerLabel).Count() > 0)
             {
                // column name al ready exist con table error
             }

             if (_ts._columnCount == 0)
                 return;


            #region addColumnHeader

            foreach (var table in _htmlDoc.DocumentNode.SelectNodes("//table[@id='" + this._tableId + "']"))
            {
                var rows = table.ChildNodes;
                var firstRow = rows.FirstOrDefault();
                firstRow.ChildNodes.Add(newHeader);
            }

            newHeader = null;
            #endregion
            
        }

        /// <summary>
        /// Add new column at the right
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool addTableColumn(string columnName)
        {
            this.addTableColumnHeader(columnName);
            HtmlNode newColumn = CreateNode();
            newColumn.Name = "td";
            newColumn.InnerHtml = string.Empty;

            if (_ts._columnCount == 0)
                return false;

            foreach (var table in _htmlDoc.DocumentNode.SelectNodes("//table[@id='" + this._tableId + "']"))
            {
                var rows = table.ChildNodes;
                foreach (var row in rows.Skip(1))
                {
                    row.ChildNodes.Add(newColumn);
                }
            }

            newColumn = null;

            return true;
        }

        public bool removeRow(int rowIndex)
        {
            try
            {
                foreach (var node in _htmlDoc.DocumentNode.SelectNodes("//table[@class='" + this._tableStyleClass + "']"))
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
            return true;
        }
        public bool removeColumnByClass(int columnIndex)
        {
            try
            {
                foreach (var node in _htmlDoc.DocumentNode.SelectNodes("//table[@class='" + this._tableStyleClass + "']"))
                {
                    if (node.HasChildNodes)
                    {
                        foreach (var row in node.ChildNodes)
                        {
                            if (row.HasChildNodes)
                            {
                                row.ChildNodes[columnIndex].Remove();
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool removeColumn(int columnIndex)
        {
            try
            {
                foreach (var node in _htmlDoc.DocumentNode.SelectNodes("//table[@id='" + this._tableId + "']"))
                {
                    if (node.HasChildNodes)
                    {
                        foreach (var row in node.ChildNodes)
                        {
                            if (row.HasChildNodes)
                            {
                                row.ChildNodes[columnIndex].Remove();
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool renameColumnHeader(int columnIndex, string newColumnHeaderLabel)
        {
            var selectedNodes = _htmlDoc.DocumentNode.SelectNodes("//table[@id='" + this._tableId + "']");

            if (selectedNodes == null)
                return true;
            
            foreach (var node in _htmlDoc.DocumentNode.SelectNodes("//table[@id='" + this._tableId + "']"))
            {
                if (node.HasChildNodes)
                {
                    foreach (var row in node.ChildNodes.Where(x => x.FirstChild.Name == "th"))
                    {
                        if (row.HasChildNodes)
                        {
                            row.ChildNodes[columnIndex].InnerHtml = newColumnHeaderLabel;
                            break;
                        }
                    }
                }
            }

            return true;

        }
   
        public void SetCellDisplayValue(int columnIndex, int rowIndex, string label)
        {
            var node = _htmlDoc.DocumentNode.SelectNodes("//table[@id='" + this._tableId + "']").FirstOrDefault().ChildNodes;

            if (rowIndex <= node.Count()-1)
            {
                var row = node[rowIndex];
                row.ChildNodes[columnIndex].InnerHtml = label;
            }
        }

        public void SetCellDisplayValue(string tdClassId, string label)
        {
            try
            {
                foreach (var node in _htmlDoc.DocumentNode.SelectNodes("//table[@class='" + this._tableStyleClass + "']"))
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

        public string GetCellDisplayValue(string tdClassId)
        {
            foreach (var node in _htmlDoc.DocumentNode.SelectNodes("//table[@class='" + this._tableStyleClass + "']"))
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

            return string.Empty;
        }

        private void GetTableStructure(string id, string styleclass = "styleclass")
        {
            int columnCount = 0;
            int rowCount = 0;
            Dictionary<int, string> tableHeaderColumns = new Dictionary<int, string>();
            DataTable dt = new DataTable(id);
            List<string> tmpRow = new List<string>();

            var t = _htmlDoc.DocumentNode.SelectNodes("//table[@id='" + id + "']");
            
            if (t != null)
            {
                InnerHtmltableCollection = ((HtmlNodeCollection)t).FirstOrDefault().InnerHtml;
                this._ContextTable = t.FirstOrDefault();
                foreach (var table in t)
                {
                    if (table.HasChildNodes)
                    {
                        foreach (var row in table.ChildNodes)
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
                }
            }

            _ts._tableClass = styleclass;
            _ts._tableId = id;
            _ts._rowCount = rowCount;
            _ts._columnCount = columnCount;
            _ts._tableHeaderColumns = tableHeaderColumns;
            _ts._isValid = true;
        }

        public List<string> readColumnValues(int indexColumn)
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

        public string readTdDisplayValueByClass(string tdClass)
        {
            foreach (var node in _htmlDoc.DocumentNode.SelectNodes("//table[@class='" + this._tableStyleClass + "']"))
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

            return string.Empty;
        }

        public List<List<string>> ReadAllColumnsValues()
        {
            return this.rows;
        }

        //<summary>
        //GetInnerHtml
        //Get the current InnerHTML of the conext DIV
        ///<returns>string content inner html</returns>
        //</summary>
        public string GetInnerHtml()
        {
            return this._ContextTable.InnerHtml;
        }
    }
}
