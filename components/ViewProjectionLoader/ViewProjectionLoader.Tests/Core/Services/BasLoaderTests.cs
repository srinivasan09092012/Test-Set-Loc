//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using ViewProjectionLoader.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ViewProjectionLoader.Tests.Core.Services
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
