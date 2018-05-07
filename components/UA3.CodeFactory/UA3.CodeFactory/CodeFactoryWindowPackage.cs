//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight.Ioc;
using Microsoft.CodeAnalysis;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using UA3.CodeFactory.Core;
using UA3.CodeFactory.Core.Services;
using UA3.CodeFactory.Plugins.AddServiceCommand;
using UA3.CodeFactory.Services;

namespace UA3.CodeFactory
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(CodeFactoryWindow))]
    [Guid(CodeFactoryWindowPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ExcludeFromCodeCoverage]
    public sealed class CodeFactoryWindowPackage : Package, ISolutionProvider
    {
        /// <summary>
        /// CodeFactoryWindowPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "714f7c38-0426-4454-a5c7-cfd5228758c6";

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeFactoryWindow"/> class.
        /// </summary>
        public CodeFactoryWindowPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        public Solution GetSolution()
        {
            var componentModel = (IComponentModel)this.GetService(typeof(SComponentModel));
            var workspace = componentModel.GetService<VisualStudioWorkspace>();

            return workspace?.CurrentSolution;
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            CodeFactoryWindowCommand.Initialize(this);

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ISolutionProvider>(() => this, true);
            SimpleIoc.Default.Register<IWindowService>(() => new WindowService(), true);

            Assembly[] pluginAssemblies = new Assembly[]
            {
                this.GetType().Assembly,
                typeof(AddServiceCommandPlugin).Assembly
            };

            SimpleIoc.Default.Register<ICodeFactoryPluginProvider>(() => new CodeFactoryPluginProvider(pluginAssemblies), true);

            base.Initialize();
        }

        #endregion Package Members
    }
}
