//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using ViewProjectionLoader.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ViewProjectionLoader.Tests.Core.Services
{
    [TestClass]
    public class BusyTrackerTests
    {
        public bool IsBusy { get; set; }

        public bool IsIdle { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.IsBusy = false;
            this.IsIdle = true;
        }

        [TestMethod]
        public void BusyTracker_should_track_busy_state_via_methods_and_events()
        {
            BusyTracker inst = new BusyTracker();

            try
            {
                inst.OnBusy += this.OnBusy;
                inst.OnIdle += this.OnIdle;

                inst.SetBusy();
                Assert.IsFalse(inst.IsIdle);
                Assert.IsFalse(this.IsIdle);
                Assert.IsTrue(this.IsBusy);

                inst.SetIdle();
                Assert.IsTrue(inst.IsIdle);
                Assert.IsTrue(this.IsIdle);
                Assert.IsFalse(this.IsBusy);
            }
            finally
            {
                inst.OnBusy -= this.OnBusy;
                inst.OnIdle -= this.OnIdle;
            }
        }

        private void OnBusy()
        {
            this.IsIdle = false;
            this.IsBusy = true;
        }

        private void OnIdle()
        {
            this.IsIdle = true;
            this.IsBusy = false;
        }
    }
}
