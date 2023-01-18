// See https://aka.ms/new-console-template for more information
using ClosedXML.Excel;
using System.Xml.Linq;

SortedSet<string> sSet = new SortedSet<string>();
var filePaths = Directory.GetFiles(@"C:\UA3\Source\Core\Dev\", "*.csproj",
                                         SearchOption.AllDirectories).ToList();


void WriteToExcel(List<string> data, string sheetName)
{
    if (!File.Exists("packages.xlsx"))
    {
        using var wbook = new XLWorkbook();
        var worksheet = wbook.Worksheets.Add(sheetName);
        wbook.SaveAs("packages.xlsx");
    }
    int i = 1;
    using (var workbook = new XLWorkbook("packages.xlsx"))
    {
        IXLWorksheet worksheet;
        var lst = workbook.Worksheets.Where(x => x.Name == sheetName).ToList();
        if (lst.Count == 0)
        {
            worksheet = workbook.Worksheets.Add(sheetName);
        }
        else
        {
            worksheet = lst.First();
            worksheet.Clear();
        }

        foreach (var hs in data)
        {
            string[] arrpackage = hs.Split(':');
            if (arrpackage.Count() > 1)
            {
                worksheet.Cell("A" + i).Value = arrpackage[0];
                worksheet.Cell("B" + i).SetValue<string>(arrpackage[1].ToString());
            }
            i++;
        }
        worksheet.SetAutoFilter(false);

        workbook.Save();
    }
}

List<string> lstPackagesInfo = new List<string>();

foreach (var file in filePaths)
{
    string fileContent = File.ReadAllText(file);
    XDocument doc = XDocument.Load(file);

    List<XElement> lstXE = doc.Root.DescendantNodes().OfType<XElement>().Where(x => x.Name == "PackageReference").Select(x => x).ToList();
    if (lstXE.Count > 0)
    {
        Console.WriteLine("Project - " + file.Substring(file.LastIndexOf('\\') + 1));
        Console.WriteLine("Package : Reference");

        lstPackagesInfo.Add("Project - " + file.Substring(file.LastIndexOf('\\') + 1) + ":");
        lstPackagesInfo.Add("Package : Reference");

        foreach (var name in lstXE)
        {
            string package = "";
            foreach (XAttribute xe in name.Attributes())
            {
                if (xe.Value.Contains("HP.") || xe.Value.Contains("HPP."))
                {
                    break;
                }
                package += xe.Value + ":";
            }
            if (!string.IsNullOrEmpty(package))
            {
                Console.WriteLine(package);
                lstPackagesInfo.Add(package);
                sSet.Add(package);
            }
        }

        lstPackagesInfo.Add("");
        lstPackagesInfo.Add("");
        Console.WriteLine();
    }

}

WriteToExcel(lstPackagesInfo, "ProjectWise");
WriteToExcel(sSet.ToList(), "Consolidated");



