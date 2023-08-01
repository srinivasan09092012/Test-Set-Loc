param(
    [string] $rootFolder     = "C:\UA3", 
    [string] $sourceFolder   = "$rootFolder\Source",
    [string] $outputFolder   = "$sourceFolder\mms-cms-util\components\FrameworkTeamTools\SBOM\output",
    [string] $inputFolder    = "$sourceFolder\mms-cms-util\components\FrameworkTeamTools\SBOM\input",
    [string] $moduleName     = "",
    [string] $moduleListPath = "$inputFolder\modules.txt",
    [switch] $noSolutionList
)

$allReferencesFile = "$outputFolder\AllReferences-$(get-date -f yyyy.MM.dd.HHmm).csv"
$packageEntriesFile = "$outputFolder\PackageEntries-$(get-date -f yyyy.MM.dd.HHmm).csv"

function FindAllReferences() {
    if($moduleName -ne "") {
        FindReferences $moduleName
    } elseif($moduleListPath -ne "") {
        if(!(Test-Path $moduleListPath -PathType Leaf)) {
            Write-Error "$moduleListPath is not a file"
            return
        }
        Get-Content $moduleListPath | ForEach-Object {
            FindReferences $_
        }
    }
}

function FindReferences([string] $moduleName) {
    Write-Host "Checking $moduleName"
    if($noSolutionList) {
        FindReferencesWithoutSolutionList $moduleName
    } else {
        $solutionList = "$sourceFolder\$moduleName\architecture\component-lists\$moduleName.SolutionList.txt"
        FindReferencesWithSolutionList $solutionList
    }
}

function FindReferencesWithoutSolutionList([string] $moduleName) {
    $modulePath = Join-Path $sourceFolder "$moduleName\components"
    $projList = ls "$modulePath\*" -Include "*.csproj" -Recurse |
        ForEach-Object { $_.FullName }
    ProcessProjectList $projList
}

function FindReferencesWithSolutionList([string] $solutionList) {
    if(!(Test-Path $solutionList -PathType Leaf)) {
        Write-Error "$solutionList does not exist"
        return
    }

    foreach ($line in Get-Content $solutionList) {
        $line = $line.Trim(' #')
        if($line -eq "" -or $line -eq "Exclude from scanning") {
            continue
        }
        $solution = Join-Path $sourceFolder $line
        if(!(Test-Path $solutionList -PathType Leaf)) {
            Write-Error "$solutionList does not exist"
            continue
        }
        Write-Host "`tChecking $solution"
        $projList = GetProjectList -solution $solution
        ProcessProjectList $projList
    }
}

function ProcessProjectList($projList) {
    $references = ProcessSolutionProjects -projects $projList
    if ($references -ne $null) {
        foreach ($reference in $references) {
            if ($reference.project -ne $null) {
                $projectName = $reference.project
                $ref = $reference.reference.Replace("<HintPath>", "")
                $ref = $ref.Replace("</HintPath>", "")
                $moduleName = $projectName.Split("\\")[3]
                Add-Content -Path  $allReferencesFile -Value "$moduleName,$projectName,$ref"
            }
        }
    }
}

function ProcessSolutionProjects($projects) {
    $AllReferenceList = New-Object System.Collections.ArrayList
    foreach ($project in $projects) {
        $ref = Get-Content $project | Where-Object { $_.Contains("<HintPath>") }
        if ($ref -ne $null) {
            foreach ($line in $ref) {
                $AllReferenceList.Add(@([pscustomobject]@{project = $project; reference = $line }))
            }
        }
   
        $packagesConfigFile = $project.Substring(0, $project.LastIndexOf("\") + 1) + "packages.config"
        if (Test-Path $packagesConfigFile) {
            foreach ($line in Get-Content $packagesConfigFile) {
                if ($line.Contains("targetFramework")) {
                    $moduleName = $packagesConfigFile.Split("\\")[3]
                    Add-Content -Path  $packageEntriesFile -Value "$moduleName,$packagesConfigFile,$line"
                }
            }
        }
    }
    return $AllReferenceList
}

function GetProjectList {
    param($solution)
    $solutionFolder = $solution.Substring(0, $solution.LastIndexOf("\") + 1)
    $projList = New-Object System.Collections.ArrayList
    Get-Content $solution |
        Select-String 'Project\(' |
        ForEach-Object {
            $projectParts = $_ -Split '[,=]' | ForEach-Object { $_.Trim('[ "{}]') };
            if ($projectParts[2].EndsWith(".csproj")) {
                $projList.Add($solutionFolder + $projectParts[2]) > $null
            }
        }
    return $projList
}

FindAllReferences
