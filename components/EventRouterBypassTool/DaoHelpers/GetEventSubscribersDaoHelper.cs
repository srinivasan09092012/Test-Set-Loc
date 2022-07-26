//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using EventRouterByPassTool.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace EventRouterByPassTool.DaoHelpers
{
    public class GetEventSubscribersDaoHelper
    {
        #region Constructor
        public GetEventSubscribersDaoHelper(IntegrationDbContext context)
        {
            this.Context = context;
        }
        #endregion

        #region Properties
        public IntegrationDbContext Context { get; set; }
        #endregion

        #region Public Methods
        public List<EventSubscriptionModel> ExecuteProcedure()
        {
            string useDomain = null;
            IQueryable<EventSubscriptionFilterSet> eventSubscriptionFilters = this.BuildEventSubFilter();

            var result = from m in this.Context.Modules
                         join me in this.Context.ModuleEvents on m.ModuleID equals me.ModuleID
                         join mes in this.Context.ModuleEventSubscription on me.ModuleEventID equals mes.ModuleEventID
                         where mes.IsActive.Equals(true) && mes.SubscriberDomain == useDomain
                         select new EventSubscriptionModel()
                         {
                             EventSubscriptionID = mes.EventSubscriptionID,
                             ModuleEventID = me.ModuleEventID,
                             ModuleID = m.ModuleID,
                             EventTypeName = me.EventTypeName,
                             ModuleName = m.ModuleName,
                             SubscriberName = mes.SubscriberName,
                             DeliveryUrl = mes.DeliveryURL,
                             BindingConfig = mes.BindingConfig,
                             BindingName = mes.BindingName,
                             IsQueue = mes.IsQueue,
                             SubscriberDomain = mes.SubscriberDomain,
                             EventSubscriptionFilterSets = eventSubscriptionFilters.Where(f => f.EventSubscriptionID == mes.EventSubscriptionID).ToList()
                         };

            List<EventSubscriptionModel> resultList = new List<EventSubscriptionModel>();

            if (result.Any())
            {
                resultList = result.ToList<EventSubscriptionModel>();

                List<EventSubscriptionModel> eventsWithFilters = resultList.Where(x => x.EventSubscriptionFilterSets != null && x.EventSubscriptionFilterSets.Count > 0).ToList();
                foreach (EventSubscriptionModel eventSubscription in eventsWithFilters)
                {
                    this.JsonDeserializeFilterCriteria(eventSubscription);
                }
            }
            
            return resultList;
        }
        #endregion

        #region Private Methods
        private IQueryable<EventSubscriptionFilterSet> BuildEventSubFilter()
        {
            return from mesf in this.Context.Set<Entities.ModuleEventSubscriptionFilter>()
                   where mesf.IsValid == true
                   select new EventSubscriptionFilterSet()
                   {
                       EventSubscriptionID = mesf.EventSubscriptionID,
                       FilterSetName = mesf.FilterSetName,
                       FilterCriteriaString = mesf.FilterSetCriteria
                   };
        }

        private void JsonDeserializeFilterCriteria(EventSubscriptionModel eventSubscription)
        {
            foreach (EventSubscriptionFilterSet filterSet in eventSubscription.EventSubscriptionFilterSets)
            {
                if (!string.IsNullOrEmpty(filterSet.FilterCriteriaString))
                {
                    List<EventSubscriptionFilterCriteria> filterCriteria = JsonConvert.DeserializeObject<List<EventSubscriptionFilterCriteria>>(filterSet.FilterCriteriaString, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Serialize, PreserveReferencesHandling = PreserveReferencesHandling.All });

                    filterSet.FilterCriteria = filterCriteria;
                }
            }
        }
        #endregion
    }
}
