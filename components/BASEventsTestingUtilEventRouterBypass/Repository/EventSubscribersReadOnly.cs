//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using EventRouterByPassTool.DaoHelpers;
using EventRouterByPassTool.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EventRouterByPassTool.Repository
{
    public class EventSubscribersReadOnly : IEventSubscribersReadOnly
    {
        #region Private Objects
        private string connectionString = string.Empty;
        private string connectionProviderName = string.Empty;
        private string connectionSchemaName = string.Empty;
        #endregion

        #region Constructors
        public EventSubscribersReadOnly()
            : this(string.Empty, string.Empty, string.Empty)
        {
        }

        public EventSubscribersReadOnly(string connectionString, string connectionProviderName, string connectionSchemaName)
        {
            this.connectionString = connectionString;
            this.connectionProviderName = connectionProviderName;
            this.connectionSchemaName = connectionSchemaName;
        }
        #endregion

        #region Public Methods
        public List<EventSubscriptionModel> GetSubscribers(string eventTypeName = null, string moduleName = null)
        {
            List<EventSubscriptionModel> result = null;

            using (IDbSession session = new DbSession(this.connectionProviderName, this.connectionString, this.connectionSchemaName))
            {
                 using (var dataBaseContext = new IntegrationDbContext(session, true))
                 {
                    result = new GetEventSubscribersDaoHelper(dataBaseContext).ExecuteProcedure();
                 }
            }

            result = result.Where(w => (string.IsNullOrEmpty(eventTypeName) || w.EventTypeName == eventTypeName) &&
                       (string.IsNullOrEmpty(moduleName) || w.ModuleName.ToLower() == moduleName.ToLower())).ToList();

            return result;
        }
        #endregion
    }
}
