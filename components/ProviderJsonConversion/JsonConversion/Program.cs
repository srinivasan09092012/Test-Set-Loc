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
            LoggerManager.Logger.LogInformational("**** ProviderJSON Batch Process Started ****");

            if (args.Length == 3)
            {
                ProcessCompleteCall(args);
            }
            else if (args.Length == 4)
            {
                ProcessPartialCalls(args);
            }
            else
            {
                LoggerManager.Logger.LogError("**** ERROR PROCESSING JOB ARGUMENTS! ****");
                LoggerManager.Logger.LogError("Only three or four arguments are allowed to run this batch process, as described below.");
                LoggerManager.Logger.LogError("The first argument must be the Tenant ID (i.e. 77b50320-5f06-5740-84f4-18d4a8cda51d).");
                LoggerManager.Logger.LogError("The second argument must be the Output File Path where the JSON extract files will be written to (i.e. C:\\UA3\\LocalStorageAccount\\DxcMedicaid\\DrugRebate\\ProviderJSON\\output)");
                LoggerManager.Logger.LogError("The third argument must be the Mongo database collection name (i.e. Provider or IRSTaxIdentification) that is assocaited with the extract being run.");
                LoggerManager.Logger.LogError("The fourth argument is the Take Number.  It is optional and, if provided, needs to be a numeric value indicating the number of records to write to each JSON file (i.e. 100).");

                LoggerManager.Logger.LogError("*** JOB ARGUMENT FORMAT ***");
                LoggerManager.Logger.LogError("TenantId OutputFilePath CollectionName TakeNumber");

                LoggerManager.Logger.LogError("** See Examples Below ***");
                LoggerManager.Logger.LogError("77b50320-5f06-5740-84f4-18d4a8cda51d C:\\UA3\\LocalStorageAccount\\DxcMedicaid\\DrugRebate\\ProviderJSON\\output IRSTaxIdentification");
                LoggerManager.Logger.LogError("77b50320-5f06-5740-84f4-18d4a8cda51d C:\\UA3\\LocalStorageAccount\\DxcMedicaid\\DrugRebate\\ProviderJSON\\output IRSTaxIdentification 100");
                LoggerManager.Logger.LogError("77b50320-5f06-5740-84f4-18d4a8cda51d C:\\UA3\\LocalStorageAccount\\DxcMedicaid\\DrugRebate\\ProviderJSON\\output Provider");
                LoggerManager.Logger.LogError("77b50320-5f06-5740-84f4-18d4a8cda51d C:\\UA3\\LocalStorageAccount\\DxcMedicaid\\DrugRebate\\ProviderJSON\\output Provider 100");
            }

            LoggerManager.Logger.LogInformational("**** ProviderJSON Batch Process Ended ****");
        }

        private static void ProcessCompleteCall(string[] args)
        {
            string tenantId = args[0];
            string outputFilePath = args[1];
            string collectionName = args[2];

            LoggerManager.Logger.LogInformational("Job argument provided for TenantId: " + args[0]);
            LoggerManager.Logger.LogInformational("Job argument provided for OutputFilePath: " + args[1]);
            LoggerManager.Logger.LogInformational("Job argument provided for CollectionName: " + args[2]);

            LoggerManager.Logger.LogInformational("Starting ProcessCompleteCall for  CollectionName: " + args[2]);

            ApplicationConfigurationManager.LoadApplicationConfiguration(tenantId, new RedisCacheManager());

            if (collectionName == "Provider")
            {
                ProcessExportProviderData processData = new ProcessExportProviderData();
                processData.CompleteCall(outputFilePath);
            }
            else if (collectionName == "IRSTaxIdentification")
            {
                ProcessExportIRSTaxIdentificationData processData = new ProcessExportIRSTaxIdentificationData();
                processData.CompleteCall(outputFilePath);
            }
            else
            {
                LoggerManager.Logger.LogInformational("Invalid collection name provided, valid names are either Provider or IRSTaxIdentification");
            }
        }

        private static void ProcessPartialCalls(string[] args)
        {
            string tenantId = args[0];
            string outputFilePath = args[1];
            string collectionName = args[2];
            int takeNumber = int.Parse(args[3]);

            LoggerManager.Logger.LogInformational("Job argument provided for TenantId: " + args[0]);
            LoggerManager.Logger.LogInformational("Job argument provided for OutputFilePath: " + args[1]);
            LoggerManager.Logger.LogInformational("Job argument provided for CollectionName: " + args[2]);
            LoggerManager.Logger.LogInformational("Job argument provided for TakeNumber : " + args[3]);

            LoggerManager.Logger.LogInformational("Starting ProcessPartialCalls for  CollectionName: " + args[2]);

            ApplicationConfigurationManager.LoadApplicationConfiguration(tenantId, new RedisCacheManager());

            if (collectionName == "Provider")
            {
                ProcessExportProviderData processPartialData = new ProcessExportProviderData();
                processPartialData.PartialCalls(takeNumber, outputFilePath);
            }
            else if (collectionName == "IRSTaxIdentification")
            {
                ProcessExportIRSTaxIdentificationData processPartialData = new ProcessExportIRSTaxIdentificationData();
                processPartialData.PartialCalls(takeNumber, outputFilePath);
            }
            else
            {
                LoggerManager.Logger.LogInformational("Invalid collection name provided, valid names are either Provider or IRSTaxIdentification");
            }
        }
    }
}