//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using ViewProjectionLoader.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ViewProjectionLoader.Tests.ViewModel
{
    [TestClass]
    public class ViewModelLocatorTests
    {
        [TestMethod]
        public void ViewModelLocator_should_initialize_viewmodels()
        {
            ViewModelLocator inst = new ViewModelLocator();

            Assert.IsNotNull(inst.BasLoad);
            Assert.IsNotNull(inst.Commits);
            Assert.IsNotNull(inst.Main);
            Assert.IsNotNull(inst.Replay);
            Assert.IsNotNull(inst.ViewProjections);
        }
    }
}
