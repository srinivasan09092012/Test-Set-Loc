$tf='C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\TF.exe'
[xml]$XmlDocument = Get-Content -Path ReportMap.xml
$sourcePath = $XmlDocument.ReportModules.SourcePath
$releaseBranch = $XmlDocument.ReportModules.Branch
$targetPath = $XmlDocument.ReportModules.TargetPath + $releaseBranch
$reportFolderName = '\Reports'
$dataSourcePath = $targetPath + "\Data Sources"
$dataSetPath = $targetPath + "\Datasets"

if (!(Test-Path $targetPath -PathType Container)) {
    New-Item -ItemType Directory -Force -Path $targetPath
}

if (!(Test-Path $dataSourcePath -PathType Container)) {
    New-Item -ItemType Directory -Force -Path $dataSourcePath
}

if (!(Test-Path $dataSetPath -PathType Container)) {
    New-Item -ItemType Directory -Force -Path $dataSetPath
}

foreach($c in $XmlDocument.ReportModules.Module)
{
    foreach($d in $c.ReportProject)
    {
        $reportPath = $sourcePath + $c.Name + "\" + $releaseBranch + $reportFolderName + $d.ReportPath

        "`nDownloading " + $c.Name + "." + $d.Name
        & $tf GET ITEMSPEC /FORCE /ALL /OVERWRITE /RECURSIVE $reportPath

        $destPath = $targetPath + $d.ServerReportPath
        $destPath
        if (!(Test-Path $destPath -PathType Container)) {
            New-Item -ItemType Directory -Force -Path $destPath
        }

        Get-ChildItem $reportPath -Filter "*.rdl" -Recurse | Copy-Item -Destination $destPath -Force
        Get-ChildItem $reportPath -Filter "*.rds" -Recurse | Copy-Item -Destination $dataSourcePath -Force
        Get-ChildItem $reportPath -Filter "*.rsd" -Recurse | Copy-Item -Destination $dataSetPath -Force
    }
}
