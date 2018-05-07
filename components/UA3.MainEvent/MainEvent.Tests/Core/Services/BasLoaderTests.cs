//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using MainEvent.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MainEvent.Tests.Core.Services
{
    [TestClass]
    public class BasLoaderTests
    {
        public const string Path = @"C:\inetpub\wwwroot\HP.HSP.UA3.EmployeeServices\R6.0";

        [TestMethod]
        [TestCategory("IgnoreOnBuild")]
        public void BasLoader_should_load_bas_from_path()
        {
            BasLoader loader = new BasLoader(new TenantBasConfigurationProvider(), new DbContextFactory());
            var result = loader.Load(Path, new StatusTracker());

            Assert.IsNotNull(result);
            Assert.AreEqual("Employee Management Services", result.ApplicationName);
            Assert.AreEqual("Employee Mgmt", result.ModuleName);
            Assert.IsNotNull(result.Configuration);
        }
    }
}
