//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using DataAccess;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HPE.HSP.UA3.Core.API.Logger;

namespace ProviderConversion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                ProcessCompleteCall(args);
            }
            else if (args.Length == 3)
            {
                ProcessPartialCalls(args);
            }
            else
            {
                LoggerManager.Logger.LogError("Only two or three arguments are allowed to run the process, first one for TenantId and second one for collection name and third one (optional) for number of records to process");
            }
            
            LoggerManager.Logger.LogInformational("****Process ended****");
        }

        private static void ProcessCompleteCall(string[] args)
        {
            string tenantId = args[0];
            string collectionName = args[1];
            LoggerManager.Logger.LogInformational("Process started - TenantId:" + tenantId);
            ApplicationConfigurationManager.LoadApplicationConfiguration(tenantId, new RedisCacheManager());

            if (collectionName == "Provider")
            {
                ProcessExportProviderData processData = new ProcessExportProviderData();
                processData.CompleteCall();
            }
            else if (collectionName == "IRSTaxIdentification")
            {
                ProcessExportIRSTaxIdentificationData processData = new ProcessExportIRSTaxIdentificationData();
                processData.CompleteCall();
            }
            else
            {
                LoggerManager.Logger.LogInformational("Invalid collection name provided, valid names are either Provider or IRSTaxIdentification");
            }
        }

        private static void ProcessPartialCalls(string[] args)
        {
            string tenantId = args[0];
            string collectionName = args[1];
            int takeNumber = int.Parse(args[2]);
            LoggerManager.Logger.LogInformational("Process started - TenantId:" + tenantId);
            ApplicationConfigurationManager.LoadApplicationConfiguration(tenantId, new RedisCacheManager());

            if (collectionName == "Provider")
            {
                ProcessExportProviderData processPartialData = new ProcessExportProviderData();
                processPartialData.PartialCalls(takeNumber);
            }
            else if (collectionName == "IRSTaxIdentification")
            {
                ProcessExportIRSTaxIdentificationData processPartialData = new ProcessExportIRSTaxIdentificationData();
                processPartialData.PartialCalls(takeNumber);
            }
            else
            {
                LoggerManager.Logger.LogInformational("Invalid collection name provided, valid names are either Provider or IRSTaxIdentification");
            }
        }
    }
}