//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Config;
using System.Collections.Generic;
using System.Reflection;

namespace MainEvent.Core.Services
{
    public class BasLoaderResult
    {
        public BasLoaderResult(string path)
        {
            this.Path = path;
            this.Assemblies = new List<Assembly>();
        }

        public string Path { get; private set; }

        public string ApplicationName { get; set; }

        public string ModuleName { get; set; }

        public TenantBasConfiguration Configuration { get; set; }

        public List<Assembly> Assemblies { get; private set; }

        public BasReflector CreateReflector()
        {
            return new BasReflector(this);
        }
    }
}
