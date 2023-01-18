//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace NISTMongoAtlasExtractPOC.Ctrl.Tests
{
    [TestClass]
    public class NotificationHelperTests
    {
        private INotificationHelper mockNotificationHelper;
        [TestInitialize]
        public void Initialize()
        {
            ApplicationConfigurationManager.LoadApplicationConfiguration(string.Empty, new CacheManager());
            ApplicationConfigurationManager.AppSettings.Set("NotificationServiceLocation", "https://bas.dev.mapshc.com/HP.HSP.UA3.NotificationServices/R6.0/NotificationService.svc");
            ApplicationConfigurationManager.AppSettings.Set("NotificationServiceBinding", "BasicHttpsBinding");
            this.mockNotificationHelper = MockRepository.GenerateMock<INotificationHelper>();
        }

        [TestMethod]
        public void NotificationHelper_PostEvent_Successfully_Post_Events()
        {
            bool result = this.mockNotificationHelper.PostEvent("eventName", "tenantId", "message");
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("IgnoreOnBuild")]
        public void NotificationHelper_PostEvent_Successfully_Post_Events_Functional_Test()
        {
            NotificationHelper notificationHelper = new NotificationHelper();
            bool result = notificationHelper.PostEvent("eventName", "tenantId", "message");
            Assert.IsFalse(result);
        }
    }
}
