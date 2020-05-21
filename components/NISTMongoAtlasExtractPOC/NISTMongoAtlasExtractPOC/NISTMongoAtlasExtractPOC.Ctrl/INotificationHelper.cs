//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace NISTMongoAtlasExtractPOC.Ctrl
{
    public interface INotificationHelper
    {
        bool PostEvent(string eventName, string tenantId, string message);
    }
}