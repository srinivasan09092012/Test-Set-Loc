using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleDBUtil.Business
{
    public class DatabaseInstallServices
    {
        private DataAccess.DatabaseMetaRepository repos;
        private string sourceRoot;
        private string extrationExt;

        public DatabaseInstallServices()
        {
            repos = new DataAccess.DatabaseMetaRepository();
            sourceRoot = ConfigurationManager.AppSettings["SourceRoot"];
            extrationExt = ConfigurationManager.AppSettings["ExtractionExt"];
        }

        public void ExecuteInstall()
        {
            if (!Directory.Exists(sourceRoot))
                throw new Exception("Source Root directory does not exist (" + sourceRoot + ") , configure source root correctly in App.config file");

            foreach (ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
            {
                if (connection.ProviderName == "System.Data.OracleClient" && connection.Name != "InstallConnection")
                {
                    CreateSchema(connection.ConnectionString);

                    string workingDirectory = sourceRoot + connection.Name + extrationExt ;

                    Console.WriteLine("Installing Tables...");
                    InstallObjects(workingDirectory + @"\Tables");

                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Installing Sequences...");
                    InstallObjects(workingDirectory + @"\Seq");

                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Installing Procedures...");
                    InstallObjects(workingDirectory + @"\Procs");

                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Installing Packages...");
                    InstallObjects(workingDirectory + @"\Packages\Spec");
                    InstallObjects(workingDirectory + @"\Packages\Body");
                }
            }
        }

        public void InstallObjects(string fileLocation)
        {
            if(Directory.Exists(fileLocation))
            {
                var tablescripts = Directory.GetFiles(fileLocation);
                foreach(string script in tablescripts)
                {
                    Console.WriteLine("Installing " + script);
                    string scriptText = File.ReadAllText(script);

                    if(scriptText.Contains("USING INDEX PCTFREE"))
                    {
                        scriptText = scriptText.Substring(0, scriptText.IndexOf("USING INDEX PCTFREE")).TrimEnd() + ")";
                    }

                    if (scriptText.Contains("SEGMENT CREATION"))
                    {
                        scriptText = scriptText.Substring(0, scriptText.IndexOf("SEGMENT CREATION")).TrimEnd();
                    }

                    repos.CreateDBObject(scriptText);
                }
            }
        }

        public void CreateSchema(string ConnectionString)
        {
            string user = ConnectionString.Substring(ConnectionString.IndexOf(@"User ID=") + 8);
            user = user.Substring(0, user.IndexOf(@";Password"));
            string password = ConnectionString.Substring(ConnectionString.IndexOf(@"Password=") + 9);
            password = password.Substring(0, password.IndexOf(@";Self Tuning"));

            repos.CreateSchema(user, password);
        }
    }
}
