//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NISTMongoAtlasExtractPOC.Ctrl.Tests.Ctrl
{
    [TestClass]
    public class CommandLineOptionsTests
    {
        [TestMethod]
        public void CommandLineOptions_should_GetExamples()
        {
            var examples = CommandLineOptions.Examples;

            Assert.IsNotNull(examples);
        }
    }
}
