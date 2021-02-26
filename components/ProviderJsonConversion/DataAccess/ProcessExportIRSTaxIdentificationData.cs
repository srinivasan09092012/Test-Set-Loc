//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
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
    public class ProcessExportIRSTaxIdentificationData
    {
        private int startNumber = 0;
        private int totalIRSTaxIds = 0;
        private IDbContextBase context = null;

        public ProcessExportIRSTaxIdentificationData()
        {
            this.context = ExportDataHelper.Initialize();
            this.totalIRSTaxIds = this.GetTotalIRSTaxIds();
        }

        public void CompleteCall(string outputFilePath)
        {
            int totalIRSTaxId = this.GetTotalIRSTaxIds();
            int currentPage = 1;

            DRIRSTaxIdentificationQuery result = this.ExtractData(currentPage, totalIRSTaxId);
            ExportDataHelper.GenerateJsonFile(result, this.startNumber, this.totalIRSTaxIds, "DRIRSTaxIdentification", outputFilePath);
        }

        public void PartialCalls(int takeNumber, string outputFilePath)
        {
            int skipNumber = 0;
            int partialTimes = 0;
            int finalIRSTaxIds = 0;
            string fileName = "DRIRSTaxIdentification-";

            if (takeNumber > this.totalIRSTaxIds)
            {
                takeNumber = this.totalIRSTaxIds;
            }

            partialTimes = this.totalIRSTaxIds / takeNumber;
            finalIRSTaxIds = this.totalIRSTaxIds % takeNumber;

            for (int i = 1; i <= partialTimes; i++)
            {
                DRIRSTaxIdentificationQuery result = this.ExtractData(i, takeNumber);
                ExportDataHelper.GenerateJsonFile(result, this.startNumber, this.totalIRSTaxIds, fileName + i, outputFilePath);
                skipNumber = takeNumber * i;
                this.startNumber = skipNumber;

                if (i == partialTimes && finalIRSTaxIds > 0)
                {
                    DRIRSTaxIdentificationQuery resultExtra = this.ExtractData(i + 1, takeNumber);
                    ExportDataHelper.GenerateJsonFile(resultExtra, this.startNumber, this.totalIRSTaxIds, fileName + (i + 1), outputFilePath);
                }
            }
        }

        public DRIRSTaxIdentificationQuery ExtractData(int currentPage, int takeNumber)
        {
            DRIRSTaxIdentificationQuery query = new DRIRSTaxIdentificationQuery()
            {
                Where = new DRIRSTaxIdentificationQueryParms()
                {
                },
                CurrentPage = currentPage,
                PageSize = takeNumber
            };

            DRIRSTaxIdentQueryExecutor queryExec = new DRIRSTaxIdentQueryExecutor();
            LoggerManager.Logger.LogInformational("Getting data from IRSTaxIdentification");
            DRIRSTaxIdentificationQuery result = queryExec.Execute(this.context, query);

            return result;
        }

        public int GetTotalIRSTaxIds()
        {
            DRIRSTaxIdentificationQuery query = new DRIRSTaxIdentificationQuery()
            {
                Where = new DRIRSTaxIdentificationQueryParms()
                {
                }
            };

            DRTotalIRSTaxIdentQueryExecutor totalIRSTaxId = new DRTotalIRSTaxIdentQueryExecutor();
            DRIRSTaxIdentificationQuery result = totalIRSTaxId.Execute(this.context, query);

            return result.RowCount;
        }
    }
}