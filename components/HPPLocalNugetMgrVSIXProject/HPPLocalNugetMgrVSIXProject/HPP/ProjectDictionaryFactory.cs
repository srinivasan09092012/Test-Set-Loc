using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace HPPLocalNugetMgrVSIXProject.HPP
{
    internal static class ProjectDictionaryFactory
    {
        /// <summary>
        ///     Spins through the list of Visual Studio projects for the
        ///     solution and loads any that are "packable" (that can generate
        ///     a nuget package).
        /// </summary>
        /// <param name="dteProjects"></param>
        public static ProjectDictionary CreateProjectDictionary(EnvDTE.Projects dteProjects)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var projectDictionary = new ProjectDictionary();

            foreach (var dteProjectObject in dteProjects)
            {
                EnvDTE.Project dteProject = dteProjectObject as EnvDTE.Project;

                // In the past this collection contained an entry for each project in the solution.
                // Something changed in VS2022 and now there is an extra project type in the collection 
                // of the type: Microsoft.VisualStudio.CommonIDE.Solutions.Dte.DteMiscProject
                // Not sure what this is for but we need to ignore it.
                // Instead of explicitly checking for a Type, putting a catch block around the 
                // properties we need to grab, and if that throws an error we skip that entry in the 
                // collection.
                if (dteProject != null)
                {
                    EnvDTE.Properties dteProjectProperties = null;
                    EnvDTE.ConfigurationManager dteProjectConfigurationManager = null;

                    try
                    {
                        dteProjectProperties = dteProject.Properties;
                        dteProjectConfigurationManager = dteProject.ConfigurationManager;
                    }
                    catch
                    {
                        // Squash on purpose for now.  See explanation above.
                        // Could change this to load warning messages in a collection and display
                        // if ever helpful.
                    }

                    if (dteProjectProperties != null && dteProjectConfigurationManager != null && dteProjectConfigurationManager.ActiveConfiguration != null)
                    {
                        var project = ProjectFactory.CreateProject(dteProject);
                        if (project.IsPackable)
                        {
                            projectDictionary.TryAdd(project.PackageId, project);
                        }
                    }
                }
            }

            return projectDictionary;
        }
    }
}
