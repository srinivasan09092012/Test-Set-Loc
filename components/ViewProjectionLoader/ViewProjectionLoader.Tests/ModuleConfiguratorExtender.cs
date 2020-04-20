//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using Microsoft.Practices.Unity;
using System;

namespace ViewProjectionLoader.Tests
{
    public class ModuleConfiguratorExtender : IModuleConfigurator
    {
        private IModuleConfigurator source;

        public ModuleConfiguratorExtender(IModuleConfigurator source)
        {
            this.source = source;
        }

        public void ConfigureContractAssemblies(IDataContractRegistry contractRegistry)
        {
            this.BeforeRegisterContractAssemblies?.Invoke(contractRegistry);
            this.source.ConfigureContractAssemblies(contractRegistry);
            this.AfterRegisterContractAssemblies?.Invoke(contractRegistry);
        }

        public void ConfigureDependencies(IUnityContainer container)
        {
            this.BeforeConfigureDependencies?.Invoke(container);
            this.source.ConfigureDependencies(container);
            this.AfterConfigureDependencies?.Invoke(container);
        }

        public void ConfigureEvents(IDistributableEventRegistry registry)
        {
            this.BeforeConfigureEvents?.Invoke(registry);
            this.source.ConfigureEvents(registry);
            this.AfterConfigureEvents?.Invoke(registry);
        }

        public Action<IDataContractRegistry> BeforeRegisterContractAssemblies { get; set; }

        public Action<IDataContractRegistry> AfterRegisterContractAssemblies { get; set; }

        public Action<IUnityContainer> BeforeConfigureDependencies { get; set; }

        public Action<IUnityContainer> AfterConfigureDependencies { get; set; }

        public Action<IDistributableEventRegistry> BeforeConfigureEvents { get; set; }

        public Action<IDistributableEventRegistry> AfterConfigureEvents { get; set; }
    }
}
