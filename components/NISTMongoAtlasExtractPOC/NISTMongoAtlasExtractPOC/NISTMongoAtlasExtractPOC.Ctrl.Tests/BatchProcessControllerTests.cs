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

namespace NISTMongoAtlasExtractPOC.Ctrl.Tests.Ctrl
{
    [TestClass]
    public class BatchProcessControllerTests
    {
        private BatchProcessController controller;

        [TestInitialize]
        public void Initialize()
        {
            ApplicationConfigurationManager.LoadApplicationConfiguration(string.Empty, new CacheManager());
            ApplicationConfigurationManager.AppSettings.Set("DefaultConnectionString", "Data Source=AWRK_PDBB;User ID=UA3_INTEGRATION;Password=UA3_INTEGRATION;Self Tuning=false");
            ApplicationConfigurationManager.AppSettings.Set("DefaultConnectionProvider", "Oracle.DataAccess.Client");
            ApplicationConfigurationManager.AppSettings.Set("DefaultConnectionSchema", "UA3_TENANT_TXN");
            this.controller = MockRepository.GenerateMock<BatchProcessController>();
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.controller = null;
        }

        [TestMethod]
        [TestCategory("IgnoreOnBuild")]
        public void BatchProcessController_Constructor_Should_Create_Object_StartStep_Null()
        {
            BatchProcessController controller = new BatchProcessController();
            Assert.IsInstanceOfType(new BatchProcessController(), typeof(BatchProcessController));
            Assert.AreEqual(1, this.controller.Steps.Count);
        }
    }
}
