//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Configuration;

namespace MainEvent.Core.Services
{
    public class BasLoader : IBasLoader
    {
        private ITenantBASConfigurationProvider configProvider;
        private IDbContextFactory contextFactory;

        public BasLoader(ITenantBASConfigurationProvider configProvider, IDbContextFactory contextFactory)
        {
            this.configProvider = configProvider;
            this.contextFactory = contextFactory;
        }

        [InjectionConstructor]
        public BasLoader()
            : this(new TenantBasConfigurationProvider(), new DbContextFactory())
        {
        }

        public BasLoaderResult Load(string path, IStatusTracker statusTracker)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            statusTracker.Set("Loading web.config");
            Configuration config = this.LoadBasWebConfig(path);

            BasLoaderResult result = new BasLoaderResult(path);
            result.ApplicationName = config.AppSettings.Settings["ApplicationName"].Value;
            result.ModuleName = config.AppSettings.Settings["ModuleName"].Value;

            statusTracker.Set("Loading BAS tenant configuration");
            using (var ctx = this.contextFactory.Create<DataListsDbContext>(LocalSettings.TenantConnectionString.ConnectionString, LocalSettings.TenantConnectionString.ProviderName, LocalSettings.TenantSchemaName))
            {
                result.Configuration = this.configProvider.GetConfiguration(LocalSettings.TenantId, result.ModuleName, result.ApplicationName, ctx);
            }

            statusTracker.Set("Loading assemblies...");
            this.LoadAssemblies(result);
            statusTracker.Clear();

            return result;
        }

        private void LoadAssemblies(BasLoaderResult loaderResult)
        {
            string binPath = Path.Combine(loaderResult.Path, "bin");
            string[] paths = Directory.GetFiles(binPath, "*.dll", SearchOption.TopDirectoryOnly);

            foreach (string p in paths)
            {
                AssemblyName asmName = AssemblyName.GetAssemblyName(p);
                Assembly asm = Assembly.Load(asmName);
                loaderResult.Assemblies.Add(asm);
            }
        }

        private Configuration LoadBasWebConfig(string basPath)
        {
            VirtualDirectoryMapping vdm = new VirtualDirectoryMapping(basPath, true);
            WebConfigurationFileMap wcfm = new WebConfigurationFileMap();
            wcfm.VirtualDirectories.Add("/", vdm);

            return WebConfigurationManager.OpenMappedWebConfiguration(wcfm, "/");
        }
    }
}