//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using EventRouterByPassTool.Common;
using EventRouterByPassTool.Entities;
using EventRouterByPassTool.Entities.ConsumedEvents;
using EventRouterByPassTool.Entities.PublishedEvents;
using EventRouterByPassTool.ProjectionWritters;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EventRouterByPassTool
{
    public class BulkTransactionHelper
    {
        #region Constructor
        public BulkTransactionHelper(IntegrationDbContext context)
        {
            this.Context = context;
        }
        #endregion

        #region Properties
        public IntegrationDbContext Context { get; set; }
        #endregion

        #region Public Methods
        public string ExecuteBulkReqProcedure(string moduleName, List<ExternalReq> values)
        {
            string returnStatus = EventRouterBypassConstants.Messages.SuccessStatus;
            try
            {
                ReqProjectionWritter reqProjectionWritter = new ReqProjectionWritter(this.Context);
                string module = Regex.Replace(moduleName, @"\s+", "_");
                string reqTable = module + "_" + EventRouterBypassConstants.DatabaseRelated.ReqTable;
                reqProjectionWritter.BulkInsert(values, reqTable, this.Context.DbSession.ConnectionString);
            }
            catch (Exception ex)
            {
                returnStatus = string.Format(EventRouterBypassConstants.Messages.FailStatus, ex.Message);
            }

            return returnStatus;
        }

        public string ExecuteBulkResProcedure(string moduleName, List<ExternalRes> values)
        {
            string returnStatus = EventRouterBypassConstants.Messages.SuccessStatus;
            try
            {
                ResProjectionWritter resProjectionWritter = new ResProjectionWritter(this.Context);
                string module = Regex.Replace(moduleName, @"\s+", "_");
                string resTable = module + "_" + EventRouterBypassConstants.DatabaseRelated.ResTable;
                resProjectionWritter.BulkInsert(values, resTable, this.Context.DbSession.ConnectionString);
            }
            catch (Exception ex)
            {
                returnStatus = string.Format(EventRouterBypassConstants.Messages.FailStatus, ex.Message);
            }

            return returnStatus;
        }

        public string ExecuteBulkEventSubscriptionErrProcedure(string moduleName, List<EventSubscriptionError> values)
        {
            string returnStatus = EventRouterBypassConstants.Messages.SuccessStatus;
            try
            {
                EventSubscriptionErrorProjectionWritter eventSubscriptionErrorProjectionWritter = new EventSubscriptionErrorProjectionWritter(this.Context);
                string module = Regex.Replace(moduleName, @"\s+", "_");
                string tableName = EventRouterBypassConstants.DatabaseRelated.EventSubscriptionsErrorTable;
                eventSubscriptionErrorProjectionWritter.BulkInsert(values, tableName, this.Context.DbSession.ConnectionString);
            }
            catch (Exception ex)
            {
                returnStatus = string.Format(EventRouterBypassConstants.Messages.FailStatus, ex.Message);
            }

            return returnStatus;
        }

        public string ExecuteBulkDeleteProcedure(Guid groupId, string moduleName, string tableType)
        {
            string returnStatus = EventRouterBypassConstants.Messages.SuccessStatus;
            try
            {
                ReqProjectionWritter reqProjectionWritter = new ReqProjectionWritter(this.Context);
                string module = Regex.Replace(moduleName, @"\s+", "_");
                string tableName = tableType == EventRouterBypassConstants.DatabaseRelated.EventSubscriptionsErrorTable ? tableType : module + "_" + tableType;
                reqProjectionWritter.DeleteByGroupId(groupId, tableName);
            }
            catch (Exception ex)
            {
                returnStatus = string.Format(EventRouterBypassConstants.Messages.FailStatus, ex.Message);
            }

            return returnStatus;
        }
        #endregion
    }
}
