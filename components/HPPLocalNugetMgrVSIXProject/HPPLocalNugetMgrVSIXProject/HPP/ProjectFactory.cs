using Microsoft.VisualStudio.Shell;
using System.Diagnostics;

namespace HPPLocalNugetMgrVSIXProject.HPP
{
    /// <summary>
    ///     Used to create our custom Project objects from provided VS Project objects.
    /// </summary>
    internal static class ProjectFactory
    {
        /// <summary>
        ///     Creates our Project object from the provided VS Project object.
        /// </summary>
        /// <param name="dteProject">
        ///     Visual studio object that represents the project.
        /// </param>
        public static Project CreateProject(EnvDTE.Project dteProject)
        {
            //TODO: How to read this (IsPackable) from csproj file?  As well as DebugType and GenerateDocumentationFile settings.

            ThreadHelper.ThrowIfNotOnUIThread();

            var project = new Project();

            MapProjectProperties(project, dteProject);

            if (dteProject.ConfigurationManager != null && dteProject.ConfigurationManager.ActiveConfiguration != null)
            {
                MapActiveConfigurationProperties(project, dteProject.ConfigurationManager.ActiveConfiguration);
            }

            var globalVariables = dteProject.Globals.VariableNames as System.Array;
            foreach (string globalVariable in globalVariables)
            {
                Debug.WriteLine("Variable: " + globalVariable);
            }

            return project;
        }

        /// <summary>
        ///     Grabs specific values from the VS Project object and populates
        ///     our corresponding Project class properties.
        /// </summary>
        /// <param name="dteProject">
        ///     Visual studio object that represents the project.
        /// </param>
        private static void MapProjectProperties(Project project, EnvDTE.Project dteProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            foreach (var propertyObject in dteProject.Properties)
            {
                var property = propertyObject as EnvDTE.Property;
                Debug.WriteLine("Project property: " + property.Name + ": " + property.Value);

                switch (property.Name)
                {
                    case "FileName":
                        project.FileName = property.Value.ToString();
                        break;

                    case "FullPath":
                        project.FullPath = property.Value.ToString();
                        break;

                    case "FullProjectFileName":
                        project.FullProjectFileName = property.Value.ToString();
                        break;

                    case "FileVersion":
                        project.FileVersion = property.Value.ToString();
                        break;

                    case "PackageId":
                        project.PackageId = property.Value.ToString();
                        break;

                    case "PackageTags":
                        project.PackageTags = property.Value.ToString();
                        break;

                    case "Version":
                        project.Version = property.Value.ToString();
                        break;

                    case "GeneratePackageOnBuild":
                        project.GeneratePackageOnBuild = property.Value.ToString();
                        break;

                    default:
                        // no action
                        break;
                }
            }
        }

        /// <summary>
        ///     Grabs specific values from the VS Project's ActiveConfiguration object and populates
        ///     our corresponding Project class properties.
        /// </summary>
        /// <param name="activeConfiguration">
        ///     Visual studio object that represents the Active Configuration 
        ///     for the project.
        /// </param>
        private static void MapActiveConfigurationProperties(Project project, EnvDTE.Configuration activeConfiguration)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            project.ConfigurationName = activeConfiguration.ConfigurationName;

            foreach (var propertyObject in activeConfiguration.Properties)
            {
                var property = propertyObject as EnvDTE.Property;
                Debug.WriteLine("Configuration property: " + property.Name);

                switch (property.Name)
                {
                    ////case "DebugSymbols":
                    ////    this.DebugSymbols = property.Value.ToString();
                    ////    break;

                    case "LanguageVersion":
                        project.LanguageVersion = property.Value.ToString();
                        break;

                    ////case "DocumentationFile":
                    ////    this.DocumentationFile = property.Value.ToString();
                    ////    break;

                    case "OutputPath":
                        project.OutputPath = property.Value.ToString();
                        break;

                    default:
                        // no action
                        break;
                }
            }
        }
    }
}
