#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     Build and zip a module to be scanned by veracode.
#
# How to use
#     Example Powershell command line:
#            .\BuildForVeracode.ps1 -moduleName mms-cms-adm
#     OR you can fully qualify the path.
#            C:\UA3\Source\mms-cms-util\components\SQATools\Veracode\BuildForVeracode.ps1 -moduleName mms-cms-adm
#     You can clean the module as well with the -cleanInstead switch.
#     Use -cleanDuring switch in order to delete files as soon as they are no longer being used
#     (recommended if you are low on storage)
#            C:\UA3\Source\mms-cms-util\components\SQATools\Veracode\BuildForVeracode.ps1 -cleanDuring -moduleName mms-cms-adm
#*******************************************************************
param(
    [string] $rootFolder = "C:\UA3", 
    [string] $sourceFolder = "$rootFolder\Source", 
    [string] $buildType = "Release",
    [Parameter(Mandatory)]
    [string] $moduleName,
    [string] $solutionList = "$sourceFolder\$moduleName\architecture\component-lists\$moduleName.SolutionList.txt",
    [switch] $cleanInstead = $false,
    [switch] $cleanDuring = $false,
    [string] $MsBuildExe = "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
)

#-------------------------------------------------------------------
# Remove the extension from a file path
#
# Parameters    
#    Path
#        Path that you want to remove extension from
#        Must be a path that exists
#-------------------------------------------------------------------
function removeExtension([string] $path) {
    $item = Get-Item $path
    return $item.DirectoryName + "\" + $item.BaseName
}

#-------------------------------------------------------------------
# Build all of the solutions in a module and put them in a zip.
# The solutions for each module are found in their out solution list.
#-------------------------------------------------------------------
function BuildSolutions {
    $moduleErrorLog = "$sourceFolder\$moduleName\$moduleName-errors$(get-date -f yyyy.MM.dd.HHmm).log"
    if(!$cleanInstead) {
        if(Test-Path $moduleErrorLog) {
            Clear-Content $moduleErrorLog
        } else {
            New-Item $moduleErrorLog
        }
    }
    if(!(Test-Path $solutionList -PathType Leaf)) {
        $msg = "Solution list '$solutionList' does not exist"
        Write-Error $msg
        if(!$cleanInstead) {
            Add-Content -Path $moduleErrorLog -Value "BuildForVeracode: $msg"
        }
        return
    }
    $outputFolder = "$sourceFolder\$moduleName\$moduleName$(get-date -f yyyy.MM.dd.HHmm)"
    if(!(Test-Path $outputFolder) -and !$cleanInstead) {
        mkdir $outputFolder
    }
    foreach($line in Get-Content $solutionList) {
        $line = $line.Trim()
        if($line -eq "" -or $line.StartsWith("#")) {
            continue
        }
        $solution = Join-Path $sourceFolder $line
        if(!(Test-Path $solution -PathType Leaf)) {
            $msg = "Solution '$solution' does not exist"
            Write-Error $msg
            if(!$cleanInstead) {
                Add-Content -Path $moduleErrorLog -Value "------------------------- $solution -------------------------"
                Add-Content -Path $moduleErrorLog -Value "BuildForVeracode: $msg"
            }
            continue
        }
        if($cleanInstead) {
            Write-Output "Cleaning $solution"
            & $MsBuildExe $solution /t:Clean /property:Configuration=Release
            & $MsBuildExe $solution /t:Clean /property:Configuration=Debug
            continue
        }
        $solutionFolder = removeExtension $solution
        $solutionLog = "$solutionFolder.log"
        Write-Output "Building $solution"
        & $MsBuildExe $solution /property:Configuration=$buildType /restore /p:RestorePackagesConfig=$true `
            /fileLogger `
            /fileLoggerParameters:verbosity=quiet `
            /fileLoggerParameters:logfile=$solutionLog `
            /fileLoggerParameters:errorsOnly
        $content = Get-Content $solutionLog
        if(![string]::IsNullOrWhiteSpace($content)) {
            Add-Content -Path $moduleErrorLog -Value "------------------------- $solution -------------------------"
            Add-Content -Path $moduleErrorLog -Value $content
            Remove-Item $solutionLog
        }
        ForEach($folder in ls "$solutionFolder*") {
            $folder = $folder.ToString()
            if((Test-Path $folder -PathType Leaf) -or $folder.EndsWith("Tests")) {
                continue
            }
            Write-Output "Checking $folder"
            $binFolder = "$folder\bin"
            if(!(Test-Path $binFolder -PathType Container)) {
                continue
            }
            if(Test-Path "$binFolder\$buildType" -PathType Container) {
                $binFolder += "\$buildType"
            }
            Write-Output "Checking $binFolder"
            ForEach($file in (@(ls "$binFolder\*.dll") + @(ls "$binFolder\*.pdb") + @(ls "$binFolder\*.exe"))) {
                Write-Output "Moving $file"
                Move-Item -Path $file.toString() -Destination $outputFolder -Force
            }
        }
        if($cleanDuring) {
            Write-Output "Cleaning $solution"
            & $MsBuildExe $solution /t:Clean /property:Configuration=$buildType
        }
    }
    if($cleanInstead) {
        Write-Output "All cleaned"
    } else {
        if((ls $outputFolder).Count -ne 0) {
            $zipPath = "$outputFolder.zip"
            Compress-Archive $outputFolder $zipPath
            Write-Output "Zip created at $zipPath"
        } else {
            Write-Output "Did not create zip because of lack of files"
        }
        Remove-Item $outputFolder -Force -Recurse
    }
}

BuildSolutions