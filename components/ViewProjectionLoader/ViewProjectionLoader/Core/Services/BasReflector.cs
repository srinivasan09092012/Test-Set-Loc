//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewProjectionLoader.Core.Services
{
    public class BasReflector
    {
        private BasLoaderResult loadResult;

        public BasReflector(BasLoaderResult result)
        {
            this.loadResult = result;
        }

        public Type GetConfiguratorType()
        {
            return (from t in this.AllTypes()
                    where t.IsClass && t.IsPublic && !t.IsAbstract &&
                    t.GetConstructor(Type.EmptyTypes) != null &&
                    t.GetInterfaces().Any(p => p.FullName == typeof(IModuleConfigurator).FullName)
                    select t).FirstOrDefault();
        }

        public IList<Type> GetEventTypes()
        {
            return (from t in this.AllTypes()
                    where t.IsClass && !t.IsAbstract && t.IsPublic &&
                    t.GetInterfaces().Any(p => p.FullName == typeof(IAggregateEvent).FullName)
                    select t).ToList();
        }

        public IList<Type> GetEventHandlerTypes()
        {
            return (from t in this.AllTypes()
                    where t.IsClass && !t.IsAbstract && t.IsPublic &&
                    t.GetInterfaces().Any(p => p.FullName == typeof(IHandleEvent).FullName)
                    select t).ToList();
        }

        private IEnumerable<Type> AllTypes()
        {
            return (from asm in this.loadResult.Assemblies
                    from t in asm.GetExportedTypes()
                    select t);
        }
    }
}
