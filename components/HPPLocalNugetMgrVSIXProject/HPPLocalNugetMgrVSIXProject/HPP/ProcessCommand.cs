using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using System;
using System.Threading.Tasks;

namespace HPPLocalNugetMgrVSIXProject.HPP
{
    /// <summary>
    ///     Instantiated and called by the Command class that executes when the
    ///     user clicks on our menu item from the Tools menu.
    /// </summary>
    internal class ProcessCommand
    {
        public Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider { get; private set; }

        public EnvDTE.Solution Solution { get; private set; }

        public ProjectDictionary ProjectDictionary { get; private set; }

        /// <summary>
        ///     Gets access to the Visual Studio run-time environment, locates
        ///     all projects in the solution, and displays our custom form.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ProcessCommand(Microsoft.VisualStudio.Shell.IAsyncServiceProvider serviceProvider)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.ServiceProvider = serviceProvider;

            var joinableTaskFactory = new JoinableTaskFactory(ThreadHelper.JoinableTaskContext);

            // DTE gives us access to the Visual Studio environment.
            EnvDTE80.DTE2 dte = null;
            joinableTaskFactory.Run(async delegate
            {
                dte = await this.ServiceProvider.GetServiceAsync(typeof(SDTE)) as EnvDTE80.DTE2;
            });

            this.Solution = dte.Solution;

            this.ProjectDictionary = ProjectDictionaryFactory.CreateProjectDictionary(dte.Solution.Projects);

            var formMain = new FormMain();
            formMain.Populate(this.ProjectDictionary);

            //TODO: can we anchor this to a Visual Studio window?
            formMain.TopLevel = true;
            formMain.ShowDialog();
            System.Windows.Forms.Application.DoEvents();
        }
    }
}
