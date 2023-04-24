using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace HPPLocalNugetMgrVSIXProject.HPP
{
    /// <summary>
    ///     Represents a project from the solution currently opened in Visual Studio.
    ///     This class gets loaded with attributes from the .csproj file, as well as
    ///     other metadata about the project.
    /// </summary>
    internal class Project
    {
        /// <summary>
        ///     Indicates whether this project is setup to generate 
        ///     a NuGet Package.  We are currently using the absense/presence
        ///     of the PackageTags attribute for this.  There is actually 
        ///     a IsPackable attribute in the csproj file, but the VS extension
        ///     run-time does not give us access to it at this time.
        /// </summary>
        public bool IsPackable 
        { 
            get
            {
                if (string.IsNullOrEmpty(this.PackageTags))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        ///     Example: HPP.Core.API.Logger.csproj
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     Example: C:\UA3\Source\Core\Dev\API\Logger\HPP.Core.API.Logger\
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        ///     Example: C:\UA3\Source\Core\Dev\API\Logger\HPP.Core.API.Logger\HPP.Core.API.Logger.csproj
        /// </summary>
        public string FullProjectFileName { get; set; }

        /// <summary>
        ///     Example: 22.2.124
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Example: 22.2.124.0
        /// </summary>
        public string FileVersion { get; set; }

        /// <summary>
        ///     Example: HPP.Core.API.Logger
        /// </summary>
        public string PackageId { get; set; }

        /// <summary>
        ///     Example: Core logger, log4net
        /// </summary>
        public string PackageTags { get; set; }

        public string GeneratePackageOnBuild { get; set; }

        /// <summary>
        ///     Example: Debug, Release
        /// </summary>
        public string ConfigurationName { get; set; }

        /// <summary>
        ///     Example: 10.0
        /// </summary>
        public string LanguageVersion { get; set; }

        /// <summary>
        ///     Example: bin\\Debug\\net6.0\\
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        ///     Example: C:\UA3\Source\Core\Dev\API\Logger\HPP.Core.API.Logger\\bin\\Debug\\net6.0\\
        /// </summary>
        public string OutputPathFull
        {
            get
            { 
                return Path.Combine(this.FullPath, this.OutputPath);
            }
        }

        /// <summary>
        ///     User profile folderPath.
        ///   <para>
        ///     Example: C:\Users\someuser
        ///   </para>
        /// </summary>
        public string UserProfileFolder 
        { 
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            }
        }

        /// <summary>
        ///     Folder where nuget packages are unpacked.
        ///   <para>
        ///     Example: C:\Users\someuser\.nuget\packages
        ///   </para>
        /// </summary>
        public string LocalPackagesFolder 
        { 
            get
            {
                return Path.Combine(this.UserProfileFolder, ".nuget", "packages");
            }
        }

        /// <summary>
        ///     Parent folderPath where the specific package is unpacked.
        ///   <para>
        ///     Example: C:\Users\someuser\.nuget\packages\HPP.Core.API.Logger
        ///   </para>
        /// </summary>
        public string LocalPackageProjectFolder
        {
            get
            {
                return Path.Combine(this.LocalPackagesFolder, this.PackageId);
            }
        }

        /// <summary>
        ///     Folder where the specific version of the package is unpacked.
        ///   <para>
        ///     Example: C:\Users\someuser\.nuget\packages\HPP.Core.API.Logger\22.2.124
        ///   </para>
        /// </summary>
        public string LocalPackageFolder 
        { 
            get
            {
                return Path.Combine(this.LocalPackageProjectFolder, this.Version);
            }
        }

        /// <summary>
        ///     Folder where the dll, pdb and xml files are unpacked.
        ///   <para>
        ///     Example: C:\Users\lcasey2\.nuget\packages\hpp.core.api.logger\22.2.124\lib\net6.0
        ///   </para>
        /// </summary>
        public string LocalPackageLibFolder
        {
            get
            {
                // We are after the part that is specific for target run-time version.
                // IE: net6.0
                var separators = new string[] { "\\" };
                List<string> directoryParts = this.OutputPath.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                var dotNetVersionFolder = directoryParts.Last();

                return Path.Combine(this.LocalPackageFolder, "lib", dotNetVersionFolder);
            }
        }

    }
}
