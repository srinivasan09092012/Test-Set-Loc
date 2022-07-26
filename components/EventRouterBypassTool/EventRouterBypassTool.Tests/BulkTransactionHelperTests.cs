//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using EventRouterByPassTool;
using EventRouterByPassTool.Common;
using EventRouterByPassTool.Entities;
using EventRouterByPassTool.Entities.ConsumedEvents;
using EventRouterByPassTool.Entities.PublishedEvents;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventRouterBypassTool.Tests
{
    [TestClass]
    public class BulkTransactionHelperTests
    {
        #region Private fields
        private BulkTransactionHelper target;
        private IDbSession session;
        private IntegrationDbContext integrationContext;
        private Guid groupId;
        private Guid commitId;
        #endregion

        #region Initialize
        [TestInitialize]
        public void Initialize()
        {
            string connectionString = EventRouterBypassConstants.DatabaseRelated.ConnectionStringWrk;
            string providerName = EventRouterBypassConstants.DatabaseRelated.OracleProviderName;
            string schema = EventRouterBypassConstants.DatabaseRelated.UA3InxSchema;
            this.session = new DbSession(providerName, connectionString, schema);
            this.integrationContext = new IntegrationDbContext(this.session, false);
            this.target = new BulkTransactionHelper(this.integrationContext);
            this.groupId = Guid.NewGuid();
            this.commitId = Guid.NewGuid();
        }
        #endregion

        #region Test Methods
        [TestMethod]
        public void BulkTranscionHelperShouldInitialize()
        {
            Assert.IsNotNull(this.target);
        }

        [TestMethod]
        public void BulkTransactionHelperShouldInsertIntoReqAndResTables()
        {
            string reqResult = this.target.ExecuteBulkReqProcedure(EventRouterBypassConstants.DatabaseRelated.ExternalModule, this.BuildExternalReqsList(subscriptionSucceded: true));
            string resResult = this.target.ExecuteBulkResProcedure(EventRouterBypassConstants.DatabaseRelated.ExternalModule, this.BuildExternalResList(subscriptionSucceded: true));
            Assert.AreEqual(EventRouterBypassConstants.Messages.SuccessStatus, reqResult);
            Assert.AreEqual(EventRouterBypassConstants.Messages.SuccessStatus, resResult);
        }

        [TestMethod]
        public void BulkTransactionHelperShouldInsertIntoReqAndResAndEventErrTables()
        {
            string reqResult = this.target.ExecuteBulkReqProcedure(EventRouterBypassConstants.DatabaseRelated.ExternalModule, this.BuildExternalReqsList(subscriptionSucceded: false));
            string resResult = this.target.ExecuteBulkResProcedure(EventRouterBypassConstants.DatabaseRelated.ExternalModule, this.BuildExternalResList(subscriptionSucceded: false));
            string eventErrResult = this.target.ExecuteBulkEventSubscriptionErrProcedure(EventRouterBypassConstants.DatabaseRelated.ExternalModule, this.BuildEventSubscriptionErrorsList());
            Assert.AreEqual(EventRouterBypassConstants.Messages.SuccessStatus, reqResult);
            Assert.AreEqual(EventRouterBypassConstants.Messages.SuccessStatus, resResult);
            Assert.AreEqual(EventRouterBypassConstants.Messages.SuccessStatus, eventErrResult);
        }

        [TestMethod]
        public void BulkTransactionHelper_Should_Delete_From_Req_Res_And_EventErr_tables_by_GroupId()
        {
            string eventErrResult = this.target.ExecuteBulkDeleteProcedure(this.groupId, EventRouterBypassConstants.DatabaseRelated.ExternalModule, EventRouterBypassConstants.DatabaseRelated.EventSubscriptionsErrorTable);
            string resResult = this.target.ExecuteBulkDeleteProcedure(this.groupId, EventRouterBypassConstants.DatabaseRelated.ExternalModule, EventRouterBypassConstants.DatabaseRelated.ResTable);
            string reqResult = this.target.ExecuteBulkDeleteProcedure(this.groupId, EventRouterBypassConstants.DatabaseRelated.ExternalModule, EventRouterBypassConstants.DatabaseRelated.ReqTable);
            Assert.AreEqual(EventRouterBypassConstants.Messages.SuccessStatus, reqResult);
            Assert.AreEqual(EventRouterBypassConstants.Messages.SuccessStatus, resResult);
            Assert.AreEqual(EventRouterBypassConstants.Messages.SuccessStatus, eventErrResult);
        }

        [TestMethod]
        public void BulkTransactionHelper_Should_Handle_Error_When_Inserting_Into_Req_Table()
        {
            string result = this.target.ExecuteBulkReqProcedure(EventRouterBypassConstants.DatabaseRelated.InvalidModule, this.BuildExternalReqsList());
            Assert.IsTrue(result.Contains(string.Format(EventRouterBypassConstants.Messages.FailStatus, Arg<string>.Is.Anything)));
        }

        [TestMethod]
        public void BulkTransactionHelper_Should_Handle_Error_When_Inserting_Into_Res_Table()
        {
            string result = this.target.ExecuteBulkResProcedure(EventRouterBypassConstants.DatabaseRelated.InvalidModule, this.BuildExternalResList());
            Assert.IsTrue(result.Contains(string.Format(EventRouterBypassConstants.Messages.FailStatus, Arg<string>.Is.Anything)));
        }

        [TestMethod]
        public void BulkTransactionHelper_Should_Handle_Error_When_Inserting_Into_EventErr_Table()
        {
            string result = this.target.ExecuteBulkEventSubscriptionErrProcedure(EventRouterBypassConstants.DatabaseRelated.InvalidModule, this.BuildEventSubscriptionErrorsList(validRecord: false));
            Assert.IsTrue(result.Contains(string.Format(EventRouterBypassConstants.Messages.FailStatus, Arg<string>.Is.Anything)));
        }
        #endregion

        #region Private methods
        private List<ExternalReq> BuildExternalReqsList(bool subscriptionSucceded = true)
        {
            return new List<ExternalReq>()
            {
                new ExternalReq()
                {
                    CommitId = this.commitId,
                    IsLastFromGroup = true,
                    AggregateRootId = EventRouterBypassConstants.Messages.TestConstant,
                    AggregateRootType = EventRouterBypassConstants.Messages.TestConstant,
                    EventTypeName = EventRouterBypassConstants.Messages.TestConstant,
                    EventNamespace = EventRouterBypassConstants.Messages.TestConstant,
                    EventFilterMetadata = EventRouterBypassConstants.Messages.TestConstant,
                    GroupId = this.groupId,
                    Version = "0",
                    SubscriptionStatus = subscriptionSucceded ? Byte.Parse("1") : Byte.Parse("3"),
                    CreatedTS = DateTime.UtcNow,
                    ModuleName = EventRouterBypassConstants.Messages.TestConstant,
                    LastModifiedTS = DateTime.UtcNow,
                    EventPayload = Encoding.UTF8.GetBytes(EventRouterBypassConstants.Messages.TestConstant)
                }
            };
        }

        private List<ExternalRes> BuildExternalResList(bool subscriptionSucceded = true)
        {
            return new List<ExternalRes>()
            {
                new ExternalRes()
                {
                     CommitId = this.commitId,
                     EventTypeName = EventRouterBypassConstants.Messages.TestConstant,
                     ModuleName = EventRouterBypassConstants.Messages.TestConstant,
                     SubscriberName = EventRouterBypassConstants.Messages.TestConstant,
                     SubscriptionStatus = subscriptionSucceded ? Byte.Parse("1") : Byte.Parse("3"),
                     GroupId = this.groupId,
                     CreatedTS = DateTime.UtcNow,
                     LastModifiedTs = DateTime.UtcNow
                }
            };
        }

        private List<EventSubscriptionError> BuildEventSubscriptionErrorsList(bool validRecord = true)
        {
            return new List<EventSubscriptionError>()
            {
                new EventSubscriptionError()
                {
                        ID = Guid.NewGuid(),
                        EventNamespace = EventRouterBypassConstants.Messages.TestConstant,
                        EventTypeName = EventRouterBypassConstants.Messages.TestConstant,
                        EventPayload = Encoding.UTF8.GetBytes(EventRouterBypassConstants.Messages.TestConstant),
                        CommitID = this.commitId,
                        AggregateRootID = EventRouterBypassConstants.Messages.TestConstant,
                        AggregateRootType = EventRouterBypassConstants.Messages.TestConstant,
                        Version = "0",
                        EventCreated = DateTime.UtcNow,
                        ModuleName = EventRouterBypassConstants.Messages.TestConstant,
                        SubscriberName = EventRouterBypassConstants.Messages.TestConstant,
                        ErrorMessage = EventRouterBypassConstants.Messages.TestConstant,
                        RetryCount = 10,
                        DeadFlag = true,
                        ErrorCode = validRecord ? EventRouterBypassConstants.Messages.ErrorCodeBusinessException : null,
                        SuccessFlag = false,
                        ModifiedDate = DateTime.UtcNow,
                        IsRetryOverride = false,
                        GroupId = this.groupId
                }
            };
        }
        #endregion
    }
}
