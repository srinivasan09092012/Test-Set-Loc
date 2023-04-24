using Microsoft.VisualStudio.Shell;
using System.Collections.Concurrent;

namespace HPPLocalNugetMgrVSIXProject.HPP
{
    /// <summary>
    ///     Dictionary of projects in the current Visual Studio solution 
    ///     that are "packable" (can generate a nuget package).
    /// </summary>
    internal class ProjectDictionary : ConcurrentDictionary<string, Project>
    {
    }
}
