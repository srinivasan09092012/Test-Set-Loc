//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using MainEvent.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace MainEvent.Tests.Core.Services
{
    [TestClass]
    public class OracleDbServiceTests
    {
        [TestMethod]
        public void OracleDbService_should_get_aggregates_from_range()
        {
            var cs = ConfigurationManager.ConnectionStrings["Employee"];
            using (var svc = new OracleDbService(cs.ConnectionString, cs.ProviderName))
            {
                DateTime start = DateTime.Parse("01/01/2017");
                DateTime end = DateTime.Parse("01/01/2018");
                var result = svc.GetAggregatesWithCommitsBetween(start, end);
            }
        }
    }
}