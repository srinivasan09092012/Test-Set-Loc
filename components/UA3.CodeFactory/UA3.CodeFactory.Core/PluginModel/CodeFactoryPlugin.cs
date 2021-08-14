//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace UA3.CodeFactory.Core.PluginModel
{
    public abstract class CodeFactoryPlugin<T> : ICodeFactoryPlugin where T : SettingsModel
    {
        public ISolutionTransform GetTransform(SettingsModel settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            if (!(settings is T))
            {
                throw new InvalidOperationException($"Expected {typeof(T).Name} as settings, received {settings.GetType().Name} instead");
            }

            return this.CreateTransform(settings as T);
        }

        protected abstract ISolutionTransform CreateTransform(T settings);

        protected abstract T CreateDefault();

        public SettingsModel GetDefaultSettings()
        {
            return this.CreateDefault();
        }
    }
}
