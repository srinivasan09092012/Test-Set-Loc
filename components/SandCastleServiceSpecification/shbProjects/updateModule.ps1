param (
    [string]
    $pathFileLibrary,
	[string]
	$pathFileConfXML
    )

[xml]$xmlElm = Get-Content -Path $pathFileConfXML
$lastModifiedDate = (Get-Item $pathFileLibrary).LastWriteTime.ToShortDateString()

$updateModule = (Get-Date $lastModifiedDate) -gt (Get-Date $xmlElm.DocModuleSettingModel.BuildingDate)
if($updateModule)
{
    exit 1
}
else 
{
    exit 0
}