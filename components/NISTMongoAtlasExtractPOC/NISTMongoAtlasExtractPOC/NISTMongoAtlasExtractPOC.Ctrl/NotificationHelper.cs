//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Helpers;
using HPE.HSP.UA3.Core.API.Logger;
using NISTMongoAtlasExtractPOC.Ctrl.NotificationService;
using System;

namespace NISTMongoAtlasExtractPOC.Ctrl
{
    public class NotificationHelper : INotificationHelper
    {
        public virtual bool PostEvent(string eventName, string tenantId, string message)
        {
            try
            {
                var binding = ApplicationConfigurationManager.AppSettings["NotificationServiceBinding"];
                var endpoint = ApplicationConfigurationManager.AppSettings["NotificationServiceLocation"];
                PostEventRequest postEventRequest = new PostEventRequest()
                {
                    eventName = eventName,
                    subsystem = ApplicationConfigurationManager.AppSettings["ModuleName"],
                    isArchivedEvent = true,
                    messageBody = message,
                    operatorId = ApplicationConfigurationManager.AppSettings["ApplicationName"],
                    processRunTime = DateTime.UtcNow,
                    tenantId = tenantId
                };
                ServiceChannelFactory channelFactory = new ServiceChannelFactory(binding, endpoint);
                channelFactory.Invoke<INotificationService>(proxy =>
                {
                    proxy.PostEvent(postEventRequest);
                });
                LoggerManager.Logger.LogInformational(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
