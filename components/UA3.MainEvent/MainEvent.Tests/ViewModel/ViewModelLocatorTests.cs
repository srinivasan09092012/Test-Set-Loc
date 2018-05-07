//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using MainEvent.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MainEvent.Tests.ViewModel
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
