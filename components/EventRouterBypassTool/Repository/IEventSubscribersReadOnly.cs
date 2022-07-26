//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using EventRouterByPassTool.Entities;
using System.Collections.Generic;

namespace EventRouterByPassTool.Repository
{
    public interface IEventSubscribersReadOnly
    {
        List<EventSubscriptionModel> GetSubscribers(string eventTypeName = null, string moduleName = null);
    }
}
