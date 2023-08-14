# define the output file path
$Branch = $Args
$outputFile = "C:\UA3\source\mms-cms-util\components\SQATools\Coverity\MES\$Branch\$Branch.CoverityScan.SolutionList.txt"

# make sure the output file is empty before writing to it
if (Test-Path $outputFile) { Remove-Item $outputFile }

# create empty lists to store normal and excluded lines
$normalLines = @()
$excludedLines = @()

# list of modules
$modules = "mms-cms-adm", "mms-cms-core", "mms-cms-cfg", "mms-cms-dr", "mms-cms-edi", "mms-cms-fxfer",
            "mms-cms-im", "mms-cms-inx", "mms-cms-mc", "mms-hp-mco", "mms-cms-note", "mms-cms-pl",
            "mms-cms-pict", "mms-cms-pc", "mms-cms-pe", "mms-cms-pm", "mms-hp-prov", "mms-cms-ss",
            "mms-cms-tm", "mms-cms-tplct", "mms-cms-tplp", "mms-hp-mbr", "mms-hp-dr", "mms-cms-pi",
            "mms-cms-fm", "mms-cms-authdeter"

# iterate over each module
foreach ($module in $modules) {

    # get all SolutionList.txt files in the current module directory
    $files = Get-ChildItem "C:\UA3\source\$module\architecture\component-lists" -Filter "*.SolutionList.txt"

    foreach ($file in $files) {
        # read file content
        $content = Get-Content $file.FullName

        # split lines into normal and excluded ones
        foreach ($line in $content) 
        {
            if ($line -ne "#Exclude from scanning") {

                if ($line.StartsWith("#")) {
                    $excludedLines += $line
                } else {
                    # add "/src/main-" before the line content
                    $normalLines += "/src/$Branch-" + $line
                }
            }
        }
    }
}

# sort lines
$normalLines = $normalLines | Sort-Object
$excludedLines = $excludedLines | Sort-Object

# write normal lines to the output file
Add-Content -Path $outputFile -Value $normalLines

# write the line "#Exclude from scanning"
Add-Content -Path $outputFile -Value "#Exclude from scanning"

# write excluded lines to the output file
Add-Content -Path $outputFile -Value $excludedLines

Write-Output "Written to: $outputFile"