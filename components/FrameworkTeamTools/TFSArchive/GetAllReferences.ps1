param(
    [string] $rootFolder     = "C:\UA3", 
    [string] $sourceFolder   = $rootFolder + "\Source",
    [string] $branch         = "Dev",
    [string] $dllList   = $sourceFolder + "\" + $branch +".dll.List.txt",
    [string] $solutionList = $sourceFolder + "\" + $branch +".Solution.List.txt",
    [string] $dllFilter = "")

    function FindReferences {
        $allReferencesFile = "$sourceFolder\PackageReferencesReporting\AllReferences-$(get-date -f yyyy.MM.dd.HHmm).csv"
        $packageEntriesFile = "$sourceFolder\PackageReferencesReporting\PackageEntries-$(get-date -f yyyy.MM.dd.HHmm).csv"

        foreach($line in Get-Content $solutionList) {
           if($line.Trim() -ne "") {
                $solution = $line.Replace("/","\")
                $solution = $rootFolder+$solution
                $projList = GetProjectList -solution $solution
                $references= ProcessSolutionProjects -projects $projList
                if($references -ne $null){
                    foreach($reference in $references){
                        if($reference.project -ne $null){
                            $projectName = $reference.project
                            $ref = $reference.reference.Replace("<HintPath>", "")
                            $ref = $ref.Replace("</HintPath>", "")
                            Add-Content -Path  $allReferencesFile -Value "$projectName,$ref"
                        }
                    }
                }
            }
        }
    }

    function ProcessSolutionProjects($projects){
        $AllReferenceList = New-Object System.Collections.ArrayList
        foreach($project in $projects){
            $ref = Get-Content $project | Where-Object { $_.Contains("<HintPath>")}
            if($ref -ne $null){
                foreach($line in $ref){
                    $AllReferenceList.Add(@([pscustomobject]@{project=$project;reference=$line}))
                }
            }
   
            $packagesConfigFile = $project.Substring(0, $project.LastIndexOf("\")+1) + "packages.config"
            if (Test-Path $packagesConfigFile){
               foreach($line in Get-Content $packagesConfigFile) {
                   if ($line.Contains("targetFramework")) {
                      Add-Content -Path  $packageEntriesFile -Value "$packagesConfigFile,$line"
                   }
               }
            }
        }
        return $AllReferenceList
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

    FindReferences
