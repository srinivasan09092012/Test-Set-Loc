//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using EventRouterByPassTool.Common;
using EventRouterByPassTool.Entities.PublishedEvents;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Base.BulkTransactions;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System;
using System.Collections.Generic;

namespace EventRouterByPassTool.ProjectionWritters
{
    public class ReqProjectionWritter : ProjectionWriterBase<ExternalReq>
    {
        #region Private Fields
        private IDbContextBase contextBase;
        #endregion

        #region Constructors
        public ReqProjectionWritter(IDbContextBase context) : base(context)
        {
            this.contextBase = context;
        }

        public ReqProjectionWritter(IBulkTransaction bulkTransaction, IDbContextBase context) : base(context)
        {
            this.contextBase = context;
            this.BulkTransaction = bulkTransaction;
        }
        #endregion

        #region Public Methods
        public void BulkInsert(List<ExternalReq> eventMessages, string tableName, string connectionString)
        {
            this.InitializeBulkInsertHelper();
            this.BulkTransaction.OverrideTableName(tableName);
            this.BulkTransaction.OverrideConnectionString(connectionString);
            this.Add(eventMessages);
        }

        public void DeleteByGroupId(Guid group_id, string tableName)
        {
            this.InitializeBulkInsertHelper();
            string query = BuildDeleteExternalReqQuery(this.FormatGuidId(group_id.ToString()), tableName);
            this.BulkTransaction.ExecuteQuery(query);
        }
        #endregion

        #region Private Methods
        private string FormatGuidId(string groupId)
        {
            return this.BulkTransaction is OracleBulkTransaction
                ? this.GuidToRAW(groupId)
                : groupId;
        }

        private string GuidToRAW(string text)
        {
            Guid guid;
            Guid.TryParse(text, out guid);
            return BitConverter.ToString(guid.ToByteArray()).Replace("-", string.Empty);
        }

        private string BuildDeleteExternalReqQuery(string group_Id, string table_name)
        {
            string deleteQueryConstant = this.BulkTransaction is OracleBulkTransaction ? EventRouterBypassConstants.DatabaseRelated.DeleteQueryOracle
                            : EventRouterBypassConstants.DatabaseRelated.DeleteQuerySql;

            return string.Format(
                deleteQueryConstant,
                EventRouterBypassConstants.DatabaseRelated.INXSchema,
                table_name,
                group_Id);
        }
        #endregion
    }
}
