#=================================================================================================
# This code is the property of Gainwell Technologies
# Copyright (c) 2022. All rights reserved. 
#
# Any unauthorized use in whole or in part without written consent is strictly prohibited.
# Violators may be punished to the full extent of the law.
#
# Description:
#   This copies the generated DB scripts from the generated location (Backup\yyyymmdd)
#   to the target location (..\mssql).  
#
#   The contents of the target OBJECT TYPE subdirectories (e.g. Table, StoredProcedure, Role) 
#   will first be fully deleted, and re-populated with the generated object scripts.
#   This should handle the scenario where an object is DELETED and the associated object SCRIPT
#   is also DELETED from the folder and reflected in subsequent commit and pull request merge.
#
#   Any non OBJECT TYPE subdirectories (e.g. _Changes-Data, _Changes-Table) will NOT be impacted
#   when running this script.
#=================================================================================================
$date_ = (date -f yyyyMMdd)
$path = ".\Backup\"+"$date_"
$tgtpath = "..\mssql"    # TODO: use parameter to identify target parent directory (relative path)

foreach ($dbpath in Get-ChildItem $path)
{
  $db = $dbpath.Name
  Write-Host "Contains DB: $db"

  $tgtdbpath = $tgtpath + "\" + $db

  if ( !(Test-Path $tgtdbpath))
    { $null = New-Item -type directory -name $db -path $tgtpath }

  foreach ($typepath in Get-ChildItem $dbpath)
  {
    $type = $typepath.Name
    $tgttypepath = $tgtdbpath + "\" + $type
    Write-Host "Contains Type: $type : $tgttypepath"

    if ( (Test-Path $tgttypepath))
      { $null = Remove-Item $tgttypepath -Recurse }
    $null = New-Item -type directory -name $type -path $tgtdbpath
    
    Copy-Item "$typepath\*" -Destination $tgttypepath -Recurse
  }
}