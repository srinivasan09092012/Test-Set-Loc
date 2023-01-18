//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using HP.HSP.UA3.ManagedCare.BAS.ManagedCare;
using HP.HSP.UA3.ManagedCare.BAS.ManagedCare.DataAccess.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SQLToTaskConverter
{
    public class SQLProcessor : CommandServiceBase
    {        
        private string tenantId;

        public SQLProcessor(string tenantId)
        {           
            this.tenantId = tenantId;
        }

        public bool ProcessManagedCareQuery(string sqlFile, string parameterFile, string workCsvPath)
        {
            string csvFileName = Path.GetFileNameWithoutExtension(sqlFile);

            string sqlText = this.RetrieveSqlText(sqlFile, parameterFile);
            List<QueryOutput> output = this.ExecuteManagedCareQuery(sqlText);

            string outputCsv = string.Format("{0}\\{1}.csv", workCsvPath, csvFileName);

            if (output != null && output.Any())
            {
               File.WriteAllLines(outputCsv, output.Select(x => x.Task));
            }

            return output == null ? false : output.Any();
        }
        

        private string RetrieveSqlText(string sqlFile, string parameterFile)
        {
            string result = null;

            List<Parameters> parms = new List<Parameters>();

            if (!string.IsNullOrEmpty(parameterFile) && File.Exists(parameterFile))
            {
                var parmFile = File.ReadAllText(parameterFile);
                parms = JsonConvert.DeserializeObject<List<Parameters>>(parmFile);
            }

            if (!File.Exists(sqlFile))
            {
                throw new FileNotFoundException(string.Format("{0} File not found.", sqlFile));
            }

            result = File.ReadAllText(sqlFile);

            if (parms != null && parms.Any())
            {
                foreach (var item in parms)
                {
                    result = result.Replace(item.ParameterName, item.ParameterValue);
                }
            }

            return result;
        }

        private List<QueryOutput> ExecuteManagedCareQuery(string inputQuery)
        {
            List<QueryOutput> result = new List<QueryOutput>();

            if (!ValidateQuery(inputQuery))
            {
                throw new ApplicationException("Query not allowed.  Contains reserved word(s) that are not allowed");
            }

            using (var tenantContext = this.InitializeTenantRequest(tenantId))
            {
                using (IDbSession session = this.CreateDbSession())
                {
                    using (ManagedCareEntityDbContext context = this.CreateDataListsContext(session))
                    {
                        var tasks = context.Database.SqlQuery<QueryOutput>(inputQuery);
                        result = tasks.Distinct().ToList();
                    }
                }
            }           

            return result;
        }

        public static bool ValidateQuery(string query)
        {
            return !ValidateRegex("delete", query) && !ValidateRegex("exec", query) && !ValidateRegex("insert", query) && !ValidateRegex("alter", query) &&
                   !ValidateRegex("create", query) && !ValidateRegex("drop", query) && !ValidateRegex("truncate", query) && !ValidateRegex("update", query)
                   && !ValidateRegex("rename", query) && !ValidateRegex("lock table", query) && !ValidateRegex("merge", query);
        }
        public static bool ValidateRegex(string term, string query)
        {
            // this regex finds all keywords {0} that are not leading or trailing by alphanumeric 
            return new Regex(string.Format("([^0-9a-z]{0}[^0-9a-z])|(^{0}[^0-9a-z])", term), RegexOptions.IgnoreCase).IsMatch(query);
        }

        private ManagedCareEntityDbContext CreateDataListsContext(IDbSession session)
        {
            return new DbContextFactory().Create<ManagedCareEntityDbContext>(session);
        }

        private IDbSession CreateDbSession()
        {
            return new DbSessionFactory().Create(ApplicationConfigurationManager.AppSettings["DefaultConnectionString"], ApplicationConfigurationManager.AppSettings["DefaultConnectionProvider"], ApplicationConfigurationManager.AppSettings["DefaultConnectionSchema"]);
        }

        protected override IModuleConfigurator GetConfigurator()
        {
            return new ManagedCareModuleConfigurator();
        }

    }
}
