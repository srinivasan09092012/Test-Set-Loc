//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NISTMongoAtlasExtractPOC.Main.Tests
{
    [TestClass]
    public class BatchProcessProgramTests
    {
        /// <summary>
        /// To Initialize
        /// </summary>
        private string localeCode;
        private string tenantId;
        private string module;
        private TestContext testContextInstance;
        private StringWriter sw;
        private string consoleOutput;
        private string[] args;

        public TestContext TestContext
        {
            get { return this.testContextInstance; }
            set { this.testContextInstance = value; }
        }

        [AssemblyInitialize]
        public static void Configure(TestContext tc)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [TestInitialize]
        public void Initialize()
        {
            this.tenantId = Guid.NewGuid().ToString();
            this.localeCode = "en-US";
            this.module = "Core";

            ApplicationConfigurationManager.LoadApplicationConfiguration(string.Empty, new CacheManager());

            this.sw = new StringWriter();
            System.Console.SetError(this.sw);
            System.Console.SetOut(this.sw);
        }

        /// <summary>
        /// To Cleanup class
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            this.tenantId = null;
            this.localeCode = null;
            this.module = null;
            this.consoleOutput = null;
            this.args = null;
            StringBuilder sb = this.sw.GetStringBuilder();
            sb.Remove(0, sb.Length);
            this.sw = null;
            ApplicationConfigurationManager.LoadApplicationConfiguration(string.Empty, new CacheManager());
        }

        [TestMethod]
        public void BatchProcessProgram_Main_Method_Should_Handle_Exceptions()
        {
            this.sw = new StringWriter();
            System.Console.SetOut(this.sw);
            System.Console.SetError(this.sw);
            this.args = new string[]
                            {
                    "--TenantId",  this.tenantId,
                    "--Module", this.module,
                    "--StartDate",  "12/01/2000",
                    "--EndDate",  "12/31/2000",
                    "--UserId",  "test"
                            };
            BatchProcessProgram.UnhandledExceptionHandler(new object(), new UnhandledExceptionEventArgs(new ArgumentException("This is a test exception."), true));
            this.consoleOutput = this.sw.ToString();
            Trace.WriteLine(this.consoleOutput);
            Assert.IsTrue(this.consoleOutput.Contains("Exception"));
            StringBuilder sb = this.sw.GetStringBuilder();
            sb.Remove(0, sb.Length);
            System.Console.Clear();
        }

        [TestMethod]
        public void BatchProcessProgram_Main_Method_Should_Display_Error_When_LocaleCode_Is_Invalid()
        {
            this.sw = new StringWriter();
            System.Console.SetOut(this.sw);
            System.Console.SetError(this.sw);
            this.args = new string[]
            {
                "--TenantId",  this.tenantId,
                "--Locale", "invalidlocaleCode"
            };
            BatchProcessProgram.Main(this.args);
            this.consoleOutput = this.sw.ToString();
            Trace.WriteLine(this.consoleOutput);
            Assert.IsTrue(this.consoleOutput.Contains("LocaleCode: en-US supported"));
            StringBuilder sb = this.sw.GetStringBuilder();
            sb.Remove(0, sb.Length);
            System.Console.Clear();
        }

        [TestMethod]
        public void BatchProcessProgram_Main_Method_Should_Run()
        {
            this.sw = new StringWriter();
            System.Console.SetOut(this.sw);
            System.Console.SetError(this.sw);
            this.args = new string[]
            {
                    "--TenantId",  this.tenantId,
                    "--Locale", this.localeCode
            };
            BatchProcessProgram.Main(this.args);
            this.consoleOutput = this.sw.ToString();
            Trace.WriteLine(this.consoleOutput);
            StringBuilder sb = this.sw.GetStringBuilder();
            sb.Remove(0, sb.Length);
            System.Console.Clear();
        }
    }
}