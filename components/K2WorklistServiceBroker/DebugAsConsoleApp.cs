using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.WorklistService;

namespace SourceCode.SmartObjects.Services.WorklistService
{
    class DebugAsConsoleApp
    {
        /// <summary>
        /// Included within the project for debugging purposes.  To debug (outside of the K2 Server); change the project properties 'Output Type'to a 
        /// console application, then run through debug.  
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string connectionString = "Integrated=True;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=True;Host=localhost;Port=5252;WindowsDomain=denallix;UserID=K2Service;Password=K2pass!";

            //ServicePackage rResultTable = new ServicePackage();
            DataTable result1 = new DataTable("Result1");
            DataTable result2 = new DataTable("Result2");
            ExecutionSettings settings = null;

            // build the properties collection.
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("ProcessName", "TPL Lead");
            //properties.Add("UserName", "%");

            // build the method parameters collection
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            settings = new ExecutionSettings(connectionString, "denallix\\K2Service");
            BasicWorklistItem worklist = new BasicWorklistItem(settings);

            result1 = worklist.GetWorklistItems(properties, parameters);
            Console.WriteLine(String.Format("Found {0} results", result1.Rows.Count));
            Console.ReadLine();

            // Modify to suit your needs
//            result2 = worklist.GetManagedUserWorklistItems(properties, parameters, "K2:DENALLIX\\Erica");

//            WorklistServiceBroker worklist3 = new WorklistServiceBroker();
            //worklist3.Execute();
        }
    }
}
