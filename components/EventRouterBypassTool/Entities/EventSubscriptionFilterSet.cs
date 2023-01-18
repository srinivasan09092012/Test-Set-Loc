//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;

namespace EventRouterByPassTool.Entities
{
    public class EventSubscriptionFilterSet
    {
        #region Constructor
        public EventSubscriptionFilterSet()
        {
            this.FilterCriteria = new List<EventSubscriptionFilterCriteria>();
        }
        #endregion

        #region Properties
        public string FilterSetID { get; set; }

        public System.Guid EventSubscriptionID { get; set; }

        public string FilterSetName { get; set; }

        public string FilterCriteriaString { get; set; }

        public List<EventSubscriptionFilterCriteria> FilterCriteria { get; set; }

        public bool IsValid { get; set; }
        #endregion
    }
}
