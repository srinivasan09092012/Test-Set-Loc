//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Base.Replay;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using MainEvent.Core;
using MainEvent.Core.Services;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NEventStore;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;

namespace MainEvent.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        //// WHY DOES THIS FAIL????
        [TestMethod]
        public void Integration_Test()
        {
            BasLoader loader = new BasLoader();
            var loadResult = loader.Load(@"C:\inetpub\wwwroot\HP.HSP.UA3.EmployeeServices\R1.0", new StatusTracker());

            var configuratorType = loadResult.CreateReflector().GetConfiguratorType();
            IModuleConfigurator configurator = null;

            if (configuratorType != null)
            {
                object configuratorObj = configuratorType.Assembly.CreateInstance(configuratorType.FullName, true);
                configurator = configuratorObj as IModuleConfigurator;
            }

            ModuleConfiguratorExtender wrapper = new ModuleConfiguratorExtender(configurator);
            wrapper.AfterConfigureDependencies = container =>
            {
                container.RegisterType(typeof(NEventStore.Serialization.ISerialize), typeof(CustomJsonSerializer), EventContext.Local, new TransientLifetimeManager());
            };

            string tenantId = LocalSettings.TenantId;
            EventReplayContext context = EventReplayContext.ForTenant(loadResult.Configuration).Configure(cfg =>
            {
                cfg.SpecifyAggregates(p => p.All());
                cfg.SpecifyEvents(p => p.All());
                cfg.SpecifyExternalDistribution(p => p.Disabled());
                cfg.SpecifyHandlers(p => p.All());
                if (configurator != null)
                {
                    cfg.UseConfigurator(wrapper);
                }
            });

            new EventReplayer().Replay(context);
        }

        [TestMethod]
        public void NEventstore_serialization_bug_fixed_by_using_latest_greatest_jsondotnet_binder()
        {
            var binder = new DefaultSerializationBinder();

            Type result = this.TrySerializationBinderWithDynamicallyLoadedType(binder);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NEventstore_serialization_bug_replicated()
        {
            dynamic binder = typeof(Bucket).Assembly.CreateInstance("Newtonsoft.Json.Serialization.DefaultSerializationBinder");

            Type result = this.TrySerializationBinderWithDynamicallyLoadedType(binder);
            Assert.IsNotNull(result);
        }

        private Type TrySerializationBinderWithDynamicallyLoadedType(dynamic serializationBinder)
        {
            this.AssertEmployeeNotLoaded();

            BasLoader loader = new BasLoader();
            var loadResult = loader.Load(@"C:\inetpub\wwwroot\HP.HSP.UA3.EmployeeServices\R1.0", new StatusTracker());

            this.AssertEmployeeLoaded();

            string typeName = "HP.HSP.UA3.EmployeeMgmt.BAS.Employee.Contracts.Events.EmployeeAdded";
            string asmName = "HP.HSP.UA3.EmployeeMgmt.BAS.Employee.Contracts";

            return serializationBinder.BindToType(asmName, typeName);
        }

        private void AssertEmployeeNotLoaded()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Select(p => p.FullName).ToArray();
            Assert.IsFalse(assemblies.Any(p => p.Contains("Employee")));
        }

        private void AssertEmployeeLoaded()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Select(p => p.FullName).ToArray();
            Assert.IsTrue(assemblies.Any(p => p.Contains("Employee")));
        }
    }
}