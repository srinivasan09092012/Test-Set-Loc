using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DeployReports
{
    class Program
    {
        static void Main(string[] args)
        {
            //XElement xelement = XElement.Load(@"C:\UA3\Source\ProviderEnrollment\Dev\Reports\HP.HSP.UA3.ProviderEnrollment.Reports\HP.HSP.UA3.ProviderEnrollment.UIReports\ReturnToProvider-en-us.rdl");
            //XDocument doc = XDocument.Load(@"C:\UA3\Source\ProviderEnrollment\Dev\Reports\HP.HSP.UA3.ProviderEnrollment.Reports\HP.HSP.UA3.ProviderEnrollment.UIReports\ReturnToProvider-en-us.rdl");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\UA3\Source\ProviderEnrollment\Dev\Reports\HP.HSP.UA3.ProviderEnrollment.Reports\HP.HSP.UA3.ProviderEnrollment.UIReports\ReturnToProvider-en-us.rdl");
            XmlNodeList elemList = xmlDoc.GetElementsByTagName("SharedDataSet");
            XDocument doc = XDocument.Load(@"C:\UA3\Source\ProviderEnrollment\Dev\Reports\HP.HSP.UA3.ProviderEnrollment.Reports\HP.HSP.UA3.ProviderEnrollment.UIReports\ReturnToProvider-en-us.rdl");
            XElement xelement = XElement.Parse(doc.ToString());
            var module = from nm in xelement.Elements("DataSource")
                         select nm;
            foreach (XElement xEle in module)
                Console.WriteLine(xEle);

            Console.Read();

            //XElement xelement = XElement.Load(@"..\..\ReportMap.xml");
            //XElement xelement = GetFromResources(@"ReportMap.xml");

            //var module = from nm in xelement.Elements("Module")
            //           where (string)nm.Attribute("Name") == "ProviderManagement"
            //           select nm;
            //foreach (XElement xEle in module)
            //    Console.WriteLine(xEle);

            //Console.Read();
        }

        private static XElement GetFromResources(string resourceName)
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            using (Stream stream = assem.GetManifestResourceStream(assem.GetName().Name + '.' + resourceName))
            {
                return XElement.Load(stream);
            }
        }
    }
}
