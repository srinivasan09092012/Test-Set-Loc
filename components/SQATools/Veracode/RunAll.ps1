#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     Build and zip all module to be scanned by veracode.
#
# How to use
#     Example Powershell command line:
#            .\RunAll.ps1
#     OR you can fully qualify the path.
#            C:\UA3\Source\mms-cms-util\components\SQATools\Veracode\RunAll.ps1 
#*******************************************************************

$sourceFolder = "C:\UA3\Source"
$veracodeFolder = "$sourceFolder\mms-cms-util\components\SQATools\Veracode"
$buildForVeracodePath = "$veracodeFolder\BuildForVeracode.ps1"
$outputFolder = "$veracodeFolder\output$(get-date -f yyyy.MM.dd.HHmm)"

mkdir $outputFolder

ForEach($repo in @(
	'mms-cms-adm',
	'mms-cms-authdeter',
	'mms-cms-cfg',
	'mms-cms-clm',
	'mms-cms-cm',
	'mms-cms-core',
	'mms-cms-dr',
	'mms-cms-edi',
	'mms-cms-fm',
	'mms-cms-fxfer',
	'mms-cms-im',
	'mms-cms-inx',
	'mms-cms-mbr',
	'mms-cms-mc',
	'mms-cms-note',
	'mms-cms-pc',
	'mms-cms-pe',
	'mms-cms-pi',
	'mms-cms-pict',
	'mms-cms-pl',
	'mms-cms-pm',
	'mms-cms-ss',
	'mms-cms-tm',
	'mms-cms-tplct',
	'mms-cms-tplp',
	'mms-hp-dr',
	'mms-hp-mbr',
	'mms-hp-mco',
	'mms-hp-prov'
)) {
	$date = get-date -f yyyy.MM.dd.HHmm
    & $buildForVeracodePath -cleanDuring -moduleName $repo
	$files = "$sourceFolder/$repo/$repo$date.zip","$sourceFolder/$repo/$repo-errors$date.log"
	Move-Item $files $outputFolder
}

$aggregateLog = "$outputFolder/everyError.log"
New-Item $aggregateLog
ForEach($file in (ls "$outputFolder/mms-*.log")) {
	$content = Get-Content $file
	if(![string]::IsNullOrWhiteSpace($content)) {
		Add-Content -Path $aggregateLog -Value "##################################################`n$($file.Name)`n##################################################"
		Add-Content -Path $aggregateLog -Value $content
	}
}
