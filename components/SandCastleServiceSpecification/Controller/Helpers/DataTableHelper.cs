using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISvcSpec.Helpers
{
    public static class DataTableHelper
    {
        public static string _tableId = string.Empty;
        public static DataTable table = new DataTable();
        public static DataSet ds = new DataSet("TablesInDocumentation");

        public static void addDataTable(List<List<string>> rows, string tableId)
        {
            ds.Tables.Add(tableId, "TablesInDocumentation.ModuleName." + tableId);
            ds.Tables[tableId].Columns.Add("Bullet");
            ds.Tables[tableId].Columns.Add("Operation Name");
            ds.Tables[tableId].Columns.Add("Description");
            //ds.Tables[tableId].Columns.Add("Input Command");
            //ds.Tables[tableId].Columns.Add("Event Produced");

            foreach (var row in rows)
            {
                ds.Tables[tableId].Rows.Add(new object[] { row[0], row[1], row[2] });
            }
        }
    }
}
