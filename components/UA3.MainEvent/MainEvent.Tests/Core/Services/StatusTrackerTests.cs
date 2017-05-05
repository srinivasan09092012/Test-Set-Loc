//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using MainEvent.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MainEvent.Tests.Core.Services
{
    [TestClass]
    public class StatusTrackerTests
    {
        public string LastStatus { get; private set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.LastStatus = string.Empty;
        }

        [TestMethod]
        public void StatusTracker_should_track_status_via_events()
        {
            StatusTracker inst = new StatusTracker();

            try
            {
                inst.StatusUpdated += this.OnStatusUpdated;
                inst.Set("Foo bar status {0} {1}", 1, 2);
                Assert.AreEqual("Foo bar status 1 2", this.LastStatus);
                Assert.AreEqual(this.LastStatus, inst.Status);

                inst.Clear();
                Assert.AreEqual(this.LastStatus, string.Empty);
                Assert.AreEqual(this.LastStatus, inst.Status);
            }
            finally
            {
                inst.StatusUpdated -= this.OnStatusUpdated;
            }
        }

        private void OnStatusUpdated(string status)
        {
            this.LastStatus = status;
        }
    }
}