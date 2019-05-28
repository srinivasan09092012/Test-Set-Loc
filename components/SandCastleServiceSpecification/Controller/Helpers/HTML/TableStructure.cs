using System.Collections.Generic;

namespace APISvcSpec.Helpers
{
    public class TableStructure
    {
        public TableStructure(string tableid)
        {
            this._tableId = tableid;
            this._isValid = true;
        }

        public string _tableId = string.Empty;
        public string _tableClass = string.Empty;
        public int _columnCount = 0;
        public int _rowCount = 0;
        public Dictionary<int, string> _tableHeaderColumns = new Dictionary<int, string>();
        public bool _isValid = true;
    }
}
