//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using UA3.CodeFactory.Core.PluginModel;

namespace UA3.CodeFactory.Core
{
    public class CodeFactoryPluginProvider : ICodeFactoryPluginProvider
    {
        private AggregateCatalog catalog = null;
        private CompositionContainer container = null;

        public CodeFactoryPluginProvider(params Assembly[] assemblies)
        {
            List<AssemblyCatalog> catalogs = new List<AssemblyCatalog>();

            if (assemblies != null)
            {
                foreach (Assembly asm in assemblies)
                {
                    catalogs.Add(new AssemblyCatalog(asm));
                }
            }

            this.catalog = new AggregateCatalog(catalogs);
            this.container = new CompositionContainer(this.catalog);
        }

        public ICodeFactoryPlugin GetPlugin(ICodeFactoryPluginMetadata metadata)
        {
            return this.container.GetExports<ICodeFactoryPlugin, ICodeFactoryPluginMetadata>()
                .Where(p => p.Metadata.Description == metadata.Description &&
                p.Metadata.Name == metadata.Name &&
                p.Metadata.Version == metadata.Version).Select(p => p.Value).FirstOrDefault();
        }

        public List<ICodeFactoryPluginMetadata> GetPluginMetadata()
        {
            return this.container.GetExports<ICodeFactoryPlugin, ICodeFactoryPluginMetadata>().Select(p => p.Metadata).ToList();
        }
    }
}