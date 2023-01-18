#=================================================================================================
# This code is the property of Gainwell Technologies
# Copyright (c) 2022. All rights reserved. 
#
# Any unauthorized use in whole or in part without written consent is strictly prohibited.
# Violators may be punished to the full extent of the law.
#
# Description:
#   This script generates DB scripts.
#=================================================================================================

param
(
  [Parameter(Mandatory)]
  $Server,
  $Port="1433",
  $User="mms_readonly",
  [Parameter(Mandatory)]
  $SqlPwd,
  $IncludeDBs=@("AdventureWorks"),
  $PythonExe="C:\Python310\python.exe",
  $SilentMode=$False
)

Import-Module SqlServer

$date_ = (date -f yyyyMMdd)
$path = ".\Backup\"+"$date_"

# Identify objects to exclude for generating DATA scripts
$AdventureWorksExcludeObj = 'Sales.* Production.* HumanResources.* Person.BusinessEntityContact Purchasing.ProductVendor Purchasing.Vendor'

$serverConnection = New-Object "Microsoft.SqlServer.Management.Common.ServerConnection"("$Server,$Port", $User, $SqlPwd)
$serverInstance = New-Object "Microsoft.SqlServer.Management.SMO.Server" $serverConnection

foreach ($db in $serverInstance.Databases)
{
  $dbname = "$db".replace("[", "").replace("]", "")
  if ($IncludeDBs -contains $dbname)
  {
    if (!$SilentMode)
      { Write-Host $dbname }

    $dbpath = $path + "\" + $dbname

    # The tool, mssqlscripter produces strange results for DATA when there are NO tables in the database (e.g. VUE360_RX)
    $hasTables = 0
    
    # If the directory exists at the DB level, purge and start from empty
    # - to address scnearios where objects are dropped / removed 
    if ((Test-Path $dbpath))
      { $null = Remove-Item $dbpath -Recurse }
    $null = New-Item -type directory -name $dbname -path $path

    # FIRST pass to capture object definitions individually "file-per-object"
    $arglist = "-m mssqlscripter -S '$Server,$Port' -U $User -P $SqlPwd -d $dbname -f $dbpath --file-per-object --check-for-existence --exclude-use-database --target-server-version 2016"
    Start-Process -FilePath $PythonExe -ArgumentList $arglist -Wait -NoNewWindow

    # SECOND pass to capture all object definitions in a SINGLE file
    $filepath = $dbpath + "\All-" + $dbname + ".Database.sql"
    $arglist = "-m mssqlscripter -S '$Server,$Port' -U $User -P $SqlPwd -d $dbname -f $filepath --target-server-version 2016"
    Start-Process -FilePath $PythonExe -ArgumentList $arglist -Wait -NoNewWindow

    # Move files into object type specific subdirectory
    # - string ".sql" from the end of the filename
    # - for each object type, now specified by file extension, create subdirectory and move those files
    # - strip out the DATETIME header info from each file
    Get-ChildItem $dbpath -Recurse | Rename-Item -NewName { $_.name.substring(0, $_.name.length - 4) }
    foreach ($type in (Get-ChildItem $dbpath -Recurse | Select-Object Extension -Unique))
    {
      $typename = $type.Extension.substring(1, $type.Extension.length - 1)
      $typepath = $dbpath + "\" + $typename

      if ($typename -eq 'Table')
        { $hasTables = 1 }

      if (!$SilentMode)
        { Write-Host "$dbname : $typename" }

      if (!(Test-Path $typepath))
        { $null = New-Item -type directory -name $typename -path $dbpath }
      
      Move-Item -Path ".\$dbpath\*.$typename" -Destination $typepath
      Get-ChildItem $typepath -Recurse | Rename-Item -NewName { $_.name + ".sql" }

      foreach($file in Get-ChildItem $typepath)
      {
        ((Get-Content -Path $file) -replace 'Script Date: \d+/\d+/\d+ \d+:\d+:\d+ (A|P)M ', '') | Set-Content -Path $file
      }
    }

    if ($hasTables) {
      # THIRD pass to capture data content individually "file-per-object"
      # - start in the <DB>\Data directory
      # - gather any objects to EXCLUDE
      # - remove any files that have NO content, identified by any filesize less than 5 bytes
      # - rename files that have content to be "Data.sql" rather than "Table.sql"
      if (!$SilentMode)
      { Write-Host "$dbname : Data" }

      $datapath = $dbpath + "\Data"
      if (!(Test-Path $datapath))
        {$null = New-Item -type directory -name "Data" -path $dbpath}

      # https://communary.net/2015/04/24/quick-tip-dynamically-create-and-use-variables/ -- dynamically specify values for $excludeobj
      # https://mcpmag.com/articles/2015/12/14/test-variables-in-powershell.aspx?m=1     -- handle error if source variable does NOT exist
      $excludeobj = "<none>"
      if (Get-Variable -Name "$($dbname)ExcludeObj" -ErrorAction SilentlyContinue) {
        $excludeobj = (Get-Variable -Name "$($dbname)ExcludeObj").Value
      }

      $arglist = "-m mssqlscripter -S localhost -d $dbname -f $datapath --data-only --file-per-object --exclude-objects $excludeobj --exclude-use-database --target-server-version 2016"
      Start-Process -FilePath $PythonExe -ArgumentList $arglist -Wait -NoNewWindow

      foreach($file in Get-ChildItem $datapath)
      {
        if ($file.Length -lt 5 -Or $file.Length -gt 1000000) {
          $file.FullName | Remove-Item -Force
        } else {
          $newFilename = $file.Name -replace '.Table.sql','.Data.sql'
          Rename-Item $file -NewName $newFilename
        }
      }
    }
  }
}
