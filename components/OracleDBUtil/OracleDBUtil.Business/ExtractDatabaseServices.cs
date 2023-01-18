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
        private Directories directory;

        public ExtractDatabaseServices()
        {
            repos = new DataAccess.DatabaseMetaRepository();
        }

        public void ExtractDatabaseObjects()
        {
            foreach (ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
            {
                if (connection.ProviderName == "System.Data.OracleClient" && connection.Name != "InstallConnection")
                {
                    directory = new Directories(connection.Name);
                    Console.WriteLine("Extracting tables from " + connection.Name);
                    var tables = repos.GetTableMetadata(connection.Name);

                    SaveMetadata(directory.TableDir, tables);

                    Console.WriteLine("Extracting sequences from " + connection.Name);
                    var sequences = repos.GetSequenceMetadata(connection.Name);

                    SaveMetadata(directory.SequenceDir, sequences);

                    Console.WriteLine("Extracting procedures from " + connection.Name);
                    var procedures = repos.GetProcedureMetadata(connection.Name);

                    SaveMetadata(directory.ProcDir, procedures);

                    Console.WriteLine("Extracting packages from " + connection.Name);
                    var packageSpec = repos.GetPackageSpecMetadata(connection.Name);

                    SaveMetadata(directory.PackageSpecDir, packageSpec);

                    var packageBody = repos.GetPackageBodyMetadata(connection.Name);
                    SaveMetadata(directory.PackageBodyDir, packageBody);
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
