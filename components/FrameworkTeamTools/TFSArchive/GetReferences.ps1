param(
    [string] $rootFolder     = "C:\UA3", 
    [string] $sourceFolder   = $rootFolder + "\Source",
    [string] $branch         = "Dev",
    [string] $dllList   = $sourceFolder + "\" + $branch +".dll.List.txt",
    [string] $solutionList = $sourceFolder + "\" + $branch +".Solution.List.txt",
    [string] $latestVersionLabel = "LatestVersion:",
    [string] $moduleName = "",
    [string] $dllFilter = "")
    
    $packageFolder = ""
    $latestVersion = ""
    $nugetPath = "C:\UA3\Tools\NuGet\"
    $exe = $nugetPath + "nuget.exe"

    function FindReferences {
        param($dll)
        $referencesFile = "$sourceFolder\PackageReferencesReporting\References-$dll-$(get-date -f yyyy.MM.dd.HHmm).csv"
        $incorrectReferencesFile = "$sourceFolder\PackageReferencesReporting\Incorrect-References-$dll-$(get-date -f yyyy.MM.dd.HHmm).csv"
        $packageEntriesFile = "$sourceFolder\PackageReferencesReporting\PackageEntries-$dll-$(get-date -f yyyy.MM.dd.HHmm).csv"

        foreach($line in Get-Content $solutionList) {
        if($line.Trim() -ne "") {
                $solution = $line.Replace("/","\")
                $solution = $rootFolder+$solution
                $projList = GetProjectList -solution $solution
                $references= ProcessSolutionProjects -projects $projList -dll $dll
                if($references -ne $null){
                
                    foreach($reference in $references){
                        if($reference.project -ne $null){
                            
                            $projectName = $reference.project
                            $moduleName =$projectName.Split("\")[3]
                            $ref = $reference.reference.Replace("<HintPath>", "")
                            $ref = $ref.Replace("</HintPath>", "")
                            Add-Content -Path  $referencesFile -Value "$moduleName,$projectName,$ref"
                        
                            if(-not $ref.Contains($packageFolder)){
                                Add-Content -Path  $incorrectReferencesFile -Value "$projectName,$ref"
                            }
                        }
                    }
                }
                foreach($project in $projList){
                    $packagesConfigFile = $project.Substring(0, $project.LastIndexOf("\")+1) + "packages.config"
                    if (Test-Path $packagesConfigFile){
                        foreach($line in Get-Content $packagesConfigFile) {
                            if ($line.ToUpper().Contains("`"" + $dll.Substring(0,$dll.length-4).ToUpper() + "`"")) {
                                Add-Content -Path  $packageEntriesFile -Value "$packagesConfigFile,$line"
                            }
                        }
                    }
                }
            }
        }
    }

    function ProcessSolutionProjects($projects, $dll){
        $referenceList = New-Object System.Collections.ArrayList
        foreach($project in $projects){
            $ref = Get-Content $project | Where-Object { $_.Contains($dll)}
            if($ref -ne $null){
                foreach($line in $ref){
                    $referenceList.Add(@([pscustomobject]@{project=$project;reference=$line}))
                }
            }
        }
        return $referenceList
    }

    function GetProjectList{
        param($solution)
        $solutionFolder =$solution.Substring(0, $solution.LastIndexOf("\")+1)
        $projList = New-Object System.Collections.ArrayList
        Get-Content $solution |
            Select-String 'Project\(' |
                ForEach-Object {
                    $projectParts = $_ -Split '[,=]' | ForEach-Object { $_.Trim('[ "{}]') };
                    if($projectParts[2].EndsWith(".csproj")){
                        $projList.Add($solutionFolder + $projectParts[2]) > $null
                    }
                    
        }

    return $projList

    }

    foreach($line in Get-Content $dllList) {
        if ($line.ToUpper().Contains($dllFilter.ToUpper())) {
            $packageFolder = $line.Substring($line.IndexOf('|')+1)
            FindReferences -dll $line.Substring(0, $line.IndexOf('|'))
        }
    }