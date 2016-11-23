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
        private Directories directories;

        public DatabaseInstallServices()
        {
            repos = new DataAccess.DatabaseMetaRepository();
        }

        public void ExecuteInstall()
        {
            foreach (ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
            {
                if (connection.ProviderName == "System.Data.OracleClient" && connection.Name != "InstallConnection")
                {
                    directories = new Directories(connection.Name);
                    CreateSchema(connection.ConnectionString);

                    Console.WriteLine("Installing Tables...");
                    InstallObjects(directories.TableDir);

                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Installing Sequences...");
                    InstallObjects(directories.SequenceDir);

                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Installing Procedures...");
                    InstallObjects(directories.ProcDir);

                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Installing Packages...");
                    InstallObjects(directories.PackageSpecDir);
                    InstallObjects(directories.PackageBodyDir);
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

                    if(scriptText.Contains("NOPARTITION"))
                    {
                        scriptText = scriptText.Replace("NOPARTITION", string.Empty);
                    }

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
