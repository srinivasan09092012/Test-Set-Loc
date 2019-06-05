//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using HPE.HSP.UA3.Core.API.Logger;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Configuration;

namespace DataAccess
{
    public static class ExportDataHelper
    {
        public static IDbContextBase Initialize()
        {
            IDbContextBase context;
            BASUnityContainer.Initialize();
            BASUnityContainer.Container.RegisterType<IDbContextBase, ProviderManagementDbContext>();
            var cs = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            string schema = ApplicationConfigurationManager.AppSettings["ProviderManagementDbContext.SchemaName"];
            string provider = ApplicationConfigurationManager.AppSettings["#DBProvider.Oracle"];

            BASUnityContainer.Container.RegisterType<IDbSession, DbSession>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new InjectionParameter<string>(provider),
                    new InjectionParameter<string>(cs.ConnectionString),
                    new InjectionParameter<string>(schema)));
            context = BASUnityContainer.Container.Resolve<IDbContextBase>();

            return context;
        }

        public static void GenerateJsonFile(dynamic result, int startNumber, int totalRecords, string fileName)
        {
            LoggerManager.Logger.LogInformational("Generating json object");

            ////The format of the dates must be ISODate("2019-05-01T00:00:00.000Z") and since there was no way to generate this as a valid format date
            ////we need to add two replacement text to get the output in this format "@@@2019-05-01T00:00:00.000Z@@"
            string json = JsonConvert.SerializeObject(
                result.Results.ToArray(), 
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "@@@yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffZ@@"
                });

            ////Then replace the replacement text by ISODate(" and ") to have in this way ISODate("2019-05-01T00:00:00.000Z")
            json = json.Replace("\"@@@", "ISODate(\"").Replace("@@\"", "\")");

            LoggerManager.Logger.LogInformational("Writting json file");
            System.IO.File.WriteAllText(@"c:\" + fileName + ".json", json);
            TimeSpan t = TimeSpan.FromMilliseconds(result.TotalMiliseconds);
            string totalTime = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            LoggerManager.Logger.LogInformational(string.Format("Time to process {0} to {1} from {2} records took: {3}", startNumber + 1, startNumber + result.Results.Count, totalRecords, totalTime));
        }
    }
}