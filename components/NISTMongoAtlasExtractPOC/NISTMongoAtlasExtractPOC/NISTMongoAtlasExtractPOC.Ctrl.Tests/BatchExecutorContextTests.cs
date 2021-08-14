//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NISTMongoAtlasExtractPOC.Ctrl.Tests
{
    [TestClass]
    public class BatchExecutorContextTests
    {
        private BatchExecutorContext target = null;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new BatchExecutorContext("HPP.Core.Batch.NISTAppEventAuditByModule");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.target = null;
        }

        [TestMethod]
        public void BatchExecutorContext_Contructor_Should_Be_Successful()
        {
            Assert.IsNotNull(this.target);
            Assert.IsInstanceOfType(this.target, typeof(BatchExecutorContext));
        }

        [TestMethod]
        public void BatchExecutorContext_GetResultMessages_Should_Be_Successful()
        {
            BatchExecutorContext target = new BatchExecutorContext("HPP.Core.Batch.NISTAppEventAuditByModule");
            List<string> resultMessage = target.GetResultMessages();
            Assert.IsNotNull(resultMessage);
            Assert.IsInstanceOfType(resultMessage, typeof(List<string>));
        }
    }
}
