//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using EventRouterByPassTool.Entities.ConsumedEvents;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Base.BulkTransactions;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System.Collections.Generic;

namespace EventRouterByPassTool.ProjectionWritters
{
    public class ResProjectionWritter : ProjectionWriterBase<ExternalRes>
    {
        #region Private Fields
        private IDbContextBase contextBase;
        #endregion

        #region Constructor
        public ResProjectionWritter(IDbContextBase context) : base(context)
        {
            this.contextBase = context;
        }

        public ResProjectionWritter(IBulkTransaction bulkTransaction, IDbContextBase context) : base(context)
        {
            this.contextBase = context;
            this.BulkTransaction = bulkTransaction;
        }
        #endregion

        #region Public Methods
        public void BulkInsert(List<ExternalRes> eventMessages, string tableName, string connectionString)
        {
            this.InitializeBulkInsertHelper();
            this.BulkTransaction.OverrideTableName(tableName);
            this.BulkTransaction.OverrideConnectionString(connectionString);
            this.Add(eventMessages);
        }
        #endregion
    }
}
