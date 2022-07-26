//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Data.Entity;

namespace EventRouterByPassTool.Entities
{
    public interface IIntegrationDbContext
    {
        /// <summary>
        /// The Module table holds information related to integration module
        /// such as the Module Name and if its an active module.
        /// <uol>
        /// <li><b>Table:</b>MODULE</li> 
        /// <li><b>References:</b>None</li> 
        /// <li><b>Referenced By:</b>MODULE_EVENTS</li> 
        /// </uol>
        /// </summary>
        IDbSet<IntegrationModule> Modules { get; set; }

        /// <summary>
        /// The Module Events table holds information related to integration module events
        /// such as the Module and Event Name
        /// <uol>
        /// <li><b>Table:</b>MODULE_EVENTS</li> 
        /// <li><b>References:</b>None</li> 
        /// <li><b>Referenced By:</b>MODULE_EVENT_SUBSCRIPTION</li> 
        /// </uol>
        /// </summary>
        IDbSet<ModuleEvent> ModuleEvents { get; set; }

        /// <summary>
        /// The Module Event Subscription table holds information related to module event subscribers
        /// such as the Module and Event Name
        /// <uol>
        /// <li><b>Table:</b>MODULE_EVENT_SUBSCRIPTION</li> 
        /// <li><b>References:</b>None</li> 
        /// <li><b>Referenced By:</b>None</li> 
        /// </uol>
        /// </summary>
        IDbSet<ModuleEventSubscription> ModuleEventSubscription { get; set; }

        /// <summary>
        /// The Event Subscription table holds information related to module event subscribers
        /// <uol>
        /// <li><b>Table:</b>EVENT_SUBSCRIPTIONS_ERR</li> 
        /// <li><b>References:</b>None</li> 
        /// <li><b>Referenced By:</b>None</li> 
        /// </uol>
        /// </summary>
        IDbSet<EventSubscriptionError> EventSubscriptionErrors { get; set; }
    }
}
    