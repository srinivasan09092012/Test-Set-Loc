//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using Contracts.Queries;
using Contracts.Queries.Parameters;
using DataAccess.QueryHandlers;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using HPE.HSP.UA3.Core.API.Logger;

namespace DataAccess
{
    public class ProcessExportProviderData
    {
        private int startNumber = 0;
        private int totalProv;
        private IDbContextBase context = null;

        public ProcessExportProviderData()
        {
            this.context = ExportDataHelper.Initialize();
            this.totalProv = this.GetTotalProviders();
        }

        public void CompleteCall(string outputFlePath)
        {
            int totalProv = this.GetTotalProviders();
            int currentPage = 1;

            DRProviderQuery result = this.ExtractData(currentPage, totalProv);
            ExportDataHelper.GenerateJsonFile(result, this.startNumber, this.totalProv, "DRProvider", outputFlePath);
        }

        public void PartialCalls(int takeNumber, string outputFilePath)
        {
            int skipNumber = 0;
            int partialTimes = 0;
            int finalProviders = 0;
            string fileName = "DRProvider-";

            if (takeNumber > this.totalProv)
            {
                takeNumber = this.totalProv;
            }

            partialTimes = this.totalProv / takeNumber;
            finalProviders = this.totalProv % takeNumber;

            for (int i = 1; i <= partialTimes; i++)
            {
                DRProviderQuery result = this.ExtractData(i, takeNumber);
                ExportDataHelper.GenerateJsonFile(result, this.startNumber, this.totalProv, fileName + i, outputFilePath);
                skipNumber = takeNumber * i;
                this.startNumber = skipNumber;

                if (i == partialTimes && finalProviders > 0)
                {
                    DRProviderQuery resultExtra = this.ExtractData(i + 1, takeNumber);
                    ExportDataHelper.GenerateJsonFile(resultExtra, this.startNumber, this.totalProv, fileName + (i + 1), outputFilePath);
                }
            }
        }

        public DRProviderQuery ExtractData(int currentPage, int takeNumber)
        {
            DRProviderQuery query = new DRProviderQuery()
            {
                Where = new DRProviderQueryParms()
                {
                },
                PageSize = takeNumber, 
                CurrentPage = currentPage
            };

            DRProviderQueryExecutor queryExec = new DRProviderQueryExecutor(this.context);
            LoggerManager.Logger.LogInformational("Getting data from Provider");
            DRProviderQuery result = queryExec.Execute(this.context, query);

            return result;
        }

        public int GetTotalProviders()
        {
            DRProviderQuery query = new DRProviderQuery()
            {
                Where = new DRProviderQueryParms()
                {
                }
            };

            DRTotalNumberProviderQueryExecutor totalProv = new DRTotalNumberProviderQueryExecutor();
            DRProviderQuery result = totalProv.Execute(this.context, query);

            return result.RowCount;
        }
    }
}