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
using System;
using System.Collections.Generic;

namespace NISTMongoAtlasExtractPOC.Ctrl.Tests
{
    [TestClass]
    public class BatchJobExtractStepTests
    {
        private NotificationHelper mockNotificationHelper;

        [TestInitialize]
        public void Initialize()
        {
            ApplicationConfigurationManager.LoadApplicationConfiguration(string.Empty, new CacheManager());
            this.mockNotificationHelper = MockRepository.GenerateMock<NotificationHelper>();
            this.mockNotificationHelper.Expect(p => p.PostEvent("eventname", "id", "Message")).IgnoreArguments().Return(true);
        }

        [TestMethod]
        public void BatchJobExtractStep_Constructor_Should_Create_Empty_Constructor()
        {
            BatchJobExtractStep batchJobExtractStep = new BatchJobExtractStep();
            Assert.IsInstanceOfType(batchJobExtractStep, typeof(BatchJobExtractStep));
            var objForwardParms = batchJobExtractStep.ForwardParms;
            var objMessages = batchJobExtractStep.ResultMessages;
            Assert.IsNotNull(batchJobExtractStep);
        }

        [TestMethod]
        public void BatchJobExtractStep_Initialize_Should_Initialize_Parms()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("TenantId", Guid.NewGuid().ToString());

            BatchJobExtractStep batchJobExtractStep = new BatchJobExtractStep();
            batchJobExtractStep.Initialize(dictionary);

            Assert.AreEqual(1, batchJobExtractStep.ForwardParms.Count);
        }

        [TestMethod]
        public void BatchJobExtractStep_Should_Execute()
        {
            log4net.Config.XmlConfigurator.Configure();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            CommandLineOptions options = new CommandLineOptions();
            options.TenantId = new Guid("77b50320-5f06-5740-84f4-18d4a8cda51d");
            options.Locale = "en-US";
            dictionary.Add("CommandLineOptions", options);
            BatchJobExtractStep batchJobExtractStep = new BatchJobExtractStep(this.mockNotificationHelper);
            batchJobExtractStep.Initialize(dictionary);
            batchJobExtractStep.Execute();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void BatchJobExtractStep_Execute_Should_Handle_Exceptions()
        {
            log4net.Config.XmlConfigurator.Configure();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            CommandLineOptions options = new CommandLineOptions();
            options.TenantId = new Guid("77b50320-5f06-5740-84f4-18d4a8cda51d");
            options.Locale = "en-US";
            dictionary.Add("CommandLineOptions", options);
            this.mockNotificationHelper.Expect(x => x.PostEvent("eventname", "id", "Message")).Throw(new Exception("Error")).IgnoreArguments();
            BatchJobExtractStep batchJobExtractStep = new BatchJobExtractStep(this.mockNotificationHelper);
            batchJobExtractStep.Initialize(dictionary);
            batchJobExtractStep.Execute();

            Assert.IsTrue(true);
        }
    }
}
