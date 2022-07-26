//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace EventRouterByPassTool.Entities
{
    public class EventSubscriptionModel
    {
        #region Constructor
        public EventSubscriptionModel()
        {
            this.EventSubscriptionFilterSets = new List<EventSubscriptionFilterSet>();
        }
        #endregion

        #region Properties
        public Guid EventSubscriptionID { get; set; }

        public Guid ModuleID { get; set; }

        public Guid ModuleEventID { get; set; }

        public string ModuleName { get; set; }

        public string EventTypeName { get; set; }

        public string SubscriberName { get; set; }

        public string DeliveryUrl { get; set; }

        public string BindingName { get; set; }

        public string BindingConfig { get; set; }

        public bool IsActive { get; set; }

        public bool IsQueue { get; set; }

        public string SubscriberDomain { get; set; }

        public List<EventSubscriptionFilterSet> EventSubscriptionFilterSets { get; set; }
        #endregion
    }
}
