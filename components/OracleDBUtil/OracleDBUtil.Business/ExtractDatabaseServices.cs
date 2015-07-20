using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleDBUtil.Contracts;
using System.Configuration;
using System.IO;

namespace OracleDBUtil.Business
{
    public class ExtractDatabaseServices
    {
        private DataAccess.DatabaseMetaRepository repos;
        private string sourceRoot;
        private string extrationExt;

        public ExtractDatabaseServices()
        {
            repos = new DataAccess.DatabaseMetaRepository();
            sourceRoot = ConfigurationManager.AppSettings["SourceRoot"];
            extrationExt = ConfigurationManager.AppSettings["ExtractionExt"];
        }

        public void ExtractDatabaseObjects()
        {
            if (!Directory.Exists(sourceRoot))
                throw new Exception("Source Root directory does not exist (" + sourceRoot + ") , configure source root correctly in App.config file");

            foreach (ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
            {
                if (connection.ProviderName == "System.Data.OracleClient" && connection.Name != "InstallConnection")
                {
                    string workingDirectory = sourceRoot + connection.Name + extrationExt;

                    Console.WriteLine("Extracting tables from " + connection.Name);
                    List<DatabaseObject> tables = repos.GetTableMetadata(connection.Name);

                    SaveMetadata(workingDirectory + @"\Tables", tables);

                    Console.WriteLine("Extracting procedures from " + connection.Name);
                    List<DatabaseObject> procedures = repos.GetProcedureMetadata(connection.Name);

                    SaveMetadata(workingDirectory + @"\Procs", procedures);
                }
            }

            Console.WriteLine("Operation Complete");
        }

        public void SaveMetadata(string saveDirectory, List<DatabaseObject> toSave)
        {
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);

            foreach(DatabaseObject item in toSave)
            {
                string filename = item.ObjectName + ".sql";
                Console.WriteLine("Saving " + filename);
                File.WriteAllText(Path.Combine(saveDirectory, filename), item.ObjectCreateScript);
            }
        }
    }
}
