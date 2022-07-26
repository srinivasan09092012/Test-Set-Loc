//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;

namespace EventRouterByPassTool.Entities
{
    #region Enum
    public enum FilterFieldOperators
    {
        Contains = 0,
        NotContains = 1
    }
    #endregion

    [System.Serializable]
    public class EventSubscriptionFilterCriteria
    {
        #region Properties
        public string FilterFieldName { get; set; }

        public FilterFieldOperators FilterFieldOperator { get; set; }

        public List<string> FilterFieldValues { get; set; }
        #endregion
    }
}
