//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using UA3.CodeFactory.Core.CodeAnalysis;

namespace UA3.CodeFactory.Core.PluginModel
{
    public abstract class SolutionTransformBase<T> : ISolutionTransform where T : SettingsModel
    {
        public SolutionTransformBase(T settings)
            : this(settings, new BasCodeGenerator())
        {
        }

        public SolutionTransformBase(T settings, IBasCodeGenerator codeGenerator)
            : base()
        {
            this.Settings = settings;
            this.CodeGenerator = codeGenerator;
        }

        protected T Settings { get; private set; }

        protected IBasCodeGenerator CodeGenerator { get; private set; }

        protected BasConventions Conventions { get; private set; }

        public void Execute(SolutionContext context)
        {
            this.Conventions = BasConventions.CreateFrom(context.CurrentSolution);

            this.ExecuteTransform(context);
        }

        protected abstract void ExecuteTransform(SolutionContext context);
    }
}