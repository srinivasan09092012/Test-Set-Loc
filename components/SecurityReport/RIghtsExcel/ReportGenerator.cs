//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Linq;

namespace RightsExcel
{
    public class ReportGenerator
    {
        const int firstDataRow = 5;
        private string _dbName;
        private string _tenantName;

        public event Action<string> OnStatusChanged;
        public List<ReportFunction> Functions { get; set; }
        public List<ReportRight> Rights { get; set; }
        public List<ReportRole> Roles { get; set; }

        public string ReportStatus { get; set; }

        public ReportGenerator()
        {
            Functions = new List<ReportFunction>();
            Rights = new List<ReportRight>();
            Roles = new List<ReportRole>();
        }

        public bool CreateReport(string fileNameAndPath, string dbName, string tenantName)
        {
            if (this.Roles == null || this.Roles.Count == 0)
            {
                ChangeStatus("No role data available");
                return false;
            }

            if (this.Functions == null || this.Functions.Count == 0)
            {
                ChangeStatus("No function data available");
                return false;
            }

            if (this.Rights == null || this.Rights.Count == 0)
            {
                ChangeStatus("No rights data available");
                return false;
            }

            this._dbName = dbName;
            this._tenantName = tenantName;

            Application excelApp = new Application();
            Workbook wb = excelApp.Workbooks.Add();

            // Rights
            ChangeStatus("Creating Rights worksheet");
            Worksheet rightsSheet = CreateSheet(wb, "Right", "Role", "Function");

            int itemCount = 0;
            foreach (ReportRight right in this.Rights)
            {
                AddRowToSheet(rightsSheet, itemCount, right.Name, 
                    right.RoleIds.Distinct().Count(), GetItemNames(right.RoleIds, Roles.ConvertAll(x => (ReportItemBase)x)),
                    right.FunctionIds.Distinct().Count(), GetItemNames(right.FunctionIds, Functions.ConvertAll(x => (ReportItemBase)x)));
                itemCount++;
            }
            FormatDataCells(rightsSheet, itemCount);

            // Functions
            ChangeStatus("Creating Functions worksheet");
            Worksheet functionsSheet = CreateSheet(wb, "Function", "Role", "Right");

            itemCount = 0;
            foreach (ReportFunction function in this.Functions)
            {
                AddRowToSheet(functionsSheet, itemCount, function.Name, 
                    function.RoleIds.Distinct().Count(), GetItemNames(function.RoleIds, Roles.ConvertAll(x => (ReportItemBase)x)),
                    function.RightIds.Distinct().Count(), GetItemNames(function.RightIds, Rights.ConvertAll(x => (ReportItemBase)x)));
                itemCount++;
            }
            FormatDataCells(functionsSheet, itemCount);


            // Roles
            ChangeStatus("Creating Roles worksheet");
            Worksheet rolesSheet = CreateSheet(wb, "Role", "Function", "Right");
            
            itemCount = 0;
            foreach(ReportRole role in this.Roles)
            {
                AddRowToSheet(rolesSheet, itemCount, role.Name, 
                    role.FunctionIds.Distinct().Count(), GetItemNames(role.FunctionIds, Functions.ConvertAll(x => (ReportItemBase)x)),
                    role.RightIds.Distinct().Count(), GetItemNames(role.RightIds, Rights.ConvertAll(x => (ReportItemBase)x)));
                itemCount++;
            }
            FormatDataCells(rolesSheet, itemCount);


            ChangeStatus("Saving Spreadsheet");

            excelApp.DisplayAlerts = false;
            wb.SaveAs(Filename: fileNameAndPath);
            wb.Close(SaveChanges: true);

            return true;
        }

        private void AddRowToSheet(Worksheet sheet, int rowOffset, string name, int count1, string relatedItems1, int count2, string relatedItems2)
        {
            sheet.Cells[firstDataRow + rowOffset, 1] = name;
            sheet.Cells[firstDataRow + rowOffset, 2] = count1;
            sheet.Cells[firstDataRow + rowOffset, 3] = count2;
            sheet.Cells[firstDataRow + rowOffset, 4] = relatedItems1;
            sheet.Cells[firstDataRow + rowOffset, 5] = relatedItems2;
        }

        private void FormatDataCells(Worksheet sheet, int itemCount)
        {
            sheet.Columns["A:E"].AutoFit();

            for (int i = 0; i < itemCount; i++)
            {
                if (sheet.Rows[i + firstDataRow].Height > 44)
                {
                    sheet.Rows[i + firstDataRow].RowHeight = 44;
                }
            }
        }

        private Worksheet CreateSheet(Workbook wb, string DataTypeName, string FirstChildName, string SecondChildName)
        {
            Worksheet sheet = wb.Sheets.Add();
            sheet.Name = $"{DataTypeName}s"; 
            sheet.Cells[1, 1] = $"Generated for tenant {this._tenantName} from database {this._dbName} at {DateTime.Now}";

            sheet.Cells[2, 1] = $"{DataTypeName}s";

            sheet.Cells[4, 1] = $"{DataTypeName} name";
            sheet.Cells[4, 2] = $"{FirstChildName} count";
            sheet.Cells[4, 3] = $"{SecondChildName} count";
            sheet.Cells[4, 4] = $"{FirstChildName}s";
            sheet.Cells[4, 5] = $"{SecondChildName}s";

            Range titleRange = sheet.Range["A2", "C2"];
            titleRange.Merge();
            titleRange.Font.Bold = true;
            titleRange.Font.Size = sheet.Cells[1, 2].Font.Size * 1.5;
            titleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            Range headerRange = sheet.Range["A4", "E4"];
            headerRange.Font.Bold = true;
            headerRange.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            headerRange.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
            headerRange.Borders[XlBordersIndex.xlEdgeBottom].Color = Color.Black.ToArgb();

            return sheet;
        }

        private void ChangeStatus(string status)
        {
            OnStatusChanged?.Invoke(status);
        }

        private string GetItemNames(List<string> ItemIds, IEnumerable<ReportItemBase> ItemList)
        {
            string[] items = ItemList.Where(x => ItemIds.Contains(x.Id)).OrderBy(x => x.Name).Select(x => x.Name).ToArray();

            return string.Join(",\n", items);
        }
    }
}
