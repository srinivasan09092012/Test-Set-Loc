#=================================================================================================
# This code is the property of Gainwell Technologies
# Copyright (c) 2021. All rights reserved. 
#
# Any unauthorized use in whole or in part without written consent is strictly prohibited.
# Violators may be punished to the full extent of the law.
#
# Description:
#   Align build definition mappings with solution assemblies.
#=================================================================================================

# Set global variables.


$sourceFolderRoot = "c:\"
$sourceFolder = "c:\ua3\Source\Coverity\R25_124\"

$fileToModifyInTFS = $sourceFolder + "124.CoverityScan.SolutionList_DONT_EDIT.txt"

$masterCatalogSolutions = "124.Solution.List.txt"
$solutionsModifiedinTFS = "124.ChangedSolutions.txt"
$coreSolutionsAlwaysInclude =  "124.Supporting.SolutionList.txt"
$solutionsToScanResult = "124.CoverityScan.SolutionList_DONT_EDIT.txt"
$solutionsToScanArchived = "124.CoverityScan.SolutionList_DONT_EDIT_Archived.txt"



#
# This function prepares the environment prior to producing new output files
#
function ArchiveOldFiles($sourceFolder, $solutionsToScanResult, $solutionsToScanArchived)
{
    try
    {
        Write-Host "ArchiveOldFiles activities."
		
		#delete the previous archiving if exists
		$fileArchived = $sourceFolder + $solutionsToScanArchived
        if(Test-Path $fileArchived)
        {
            Remove-Item -Path $fileArchived
			Write-Host "Deleted previous archiving."
		}
        
        #copy and rename old output files to be scanned
		$fileToArchive = $sourceFolder + $solutionsToScanResult
        if(Test-Path $fileToArchive)
        {
            Copy-Item -Path $fileToArchive -Destination $fileArchived
			Write-Host "Archived current results."
        }
        
        #delete the existing output file
		$fileToDelete = $sourceFolder + $solutionsToScanResult
        if(Test-Path $fileToDelete)
        {
            Remove-Item -Path $fileToDelete
			Write-Host "Deleted current results to allow the process to create a new fresh output."
        }
    }
    catch
    {
        Write-Host "ArchiveOldFiles raised an exception error: " + $Error[0]
        
    }
}

function DidIntersectionChanged($sourceFolder, $solutionsToScanResult, $solutionsToScanArchived)
{
    try
    {
        Write-Host "DidIntersectionChanged activities."
		
		#delete the previous archiving if exists
		$fileArchived = $sourceFolder + $solutionsToScanArchived
        $fileResultCreated = $sourceFolder + $solutionsToScanResult

        if(Test-Path $fileArchived -And Test-Path $fileArchived)
        {
			Write-Host "Archived and new generated file exist - now compare if anything changed."
            if ( -Not (Compare-Object (Get-Content $fileArchived) (Get-Content $fileResultCreated)) )
            {
                Write-Host "new created file NOT the same as archived file - Need to check in results in TFS."
                
            }
		}
        
    }
    catch
    {
        Write-Host "DidIntersectionChanged raised an exception error: " + $Error[0]
        
    }
}


#hash tables to hold the solution data and to the intersections
$Hash_solutionsAlreadyScanned = @{}
$Hash_solutionsCore = @{}
$Hash_solutions = @{}
$Hash_modified = @{}


#
# This function includes all the solutions that had already been scanned into the list to scan again
# Otherwise coverity will mark any open defects as Fixed when previously scanned files are absent
#

function FindOutSolutionsAlreadyScanned($solutionsToScanResult, $Hash_solutionsAlreadyScanned)
{

    try
    {
        #find out if solutions scanned in previous scans - they must be always re-scanned otherwise Coverity has issues marking false positive 'Fixed'.
        if(Test-Path $solutionsToScanResult)
        {
            Write-Host "Finding previous solutions scanned that are not part of the Core solutions."
    	    foreach($line in Get-Content $solutionsToScanResult)
	        {
		      if(-Not ($Hash_solutionsAlreadyScanned.ContainsKey($line)))
		      {
			     $Hash_solutionsAlreadyScanned.Add($line, '1')
		      }
	       }
        }
        else
        {
            Write-Host "No previous solutions to add."
        }
    }
    catch
    {
	   Write-Host "FindOutSolutionsAlreadyScanned raised an exception error: " + $Error[0]
    }
 }

#
# This function includes all the core solutions as target solution to always be scanned.
# These solutions do bring dependencies with other potential solutions to be scanned.
#
function InitializeCoreSolutions($sourceFolder, $coreSolutionsAlwaysInclude, $solutionsToScanResult, $Hash_solutionsCore)
{
    try
    {
        #copy all items from the support files to the output file to be scanned
        $fileToCopy = $sourceFolder + $coreSolutionsAlwaysInclude
        $fileDestination = $sourceFolder + $solutionsToScanResult
        Copy-Item -Path  $fileToCopy -Destination $fileDestination
        
        #remove read-only to make it writeable
        $fileToRemoveReadOnly = Get-Item -Path $fileDestination
        $fileToRemoveReadOnly.IsReadOnly = $false
        
        # add the core solutions in a hash - we will need that to avoid duplication when merging with the solutions already scanned
        Write-Host "Finding previous solutions scanned that are not part of the Core solutions."
    	$fileToReadFrom = $sourceFolder + $coreSolutionsAlwaysInclude
        foreach($line in Get-Content $fileToReadFrom)
	    {
		  if(-Not ($Hash_solutionsCore.ContainsKey($line)))
		  {
		      $Hash_solutionsCore.Add($line, '1')
		  }
	    }
    }
    catch
    {
		Write-Host "InitializeCoreSolutions raised an exception error: " + $Error[0]
    }

}

#
# This function makes the intersections between the different files to find out the resulting
# list of solutions that were modified and require to be scanned again
#
function FindIntersectionSolutions($sourceFolder, $masterCatalogSolutions, $solutionsModifiedinTFS, $coreSolutionsAlwaysInclude, $solutionsToScanResult, $Hash_solutions, $Hash_modified, $Hash_solutionsCore)
{

try{

		Write-Host "populate hashtable 1 with master solutions file."
		$fileToReadFrom = $sourceFolder + $masterCatalogSolutions
        foreach($line in Get-Content $fileToReadFrom)
		{
			if(-Not ($Hash_solutions.ContainsKey($line)))
			{
				$Hash_solutions.Add($line, '1')
			}
		}
    
		Write-Host "populate hashtable2 with TFS modified solutions."
        $fileToReadFrom = $sourceFolder + $solutionsModifiedinTFS
		foreach($line2 in Get-Content $fileToReadFrom)
		{
			#strip the part of the solution path that does not matter to match with the existing solutions file
			$pathThatMattersString = ""
			$pathThatMattersIndex = $line2.IndexOf("/Source/") 

			if($pathThatMattersIndex)
			{
				$pathThatMattersString = $line2.Substring($pathThatMattersIndex)
			}
			else
			{
				#if expected format not found - skip the line and inform an issue
				Write-Host "path format not expected in modified TFS file - this solution will be ignored " + $line2
				Continue
			}
			
			Write-Host "path changed from modified solutions to " + $line2
			if(-Not ($pathThatMattersString -eq $null -or $pathThatMattersString -eq "") -and -Not ($Hash_modified.ContainsKey($pathThatMattersString)))
			{
				$Hash_modified.Add($pathThatMattersString, '1')
			}
		}
    
		Write-Host "finding intersections between master catalog and modified solutions. Anything else will not matter to be scanned."
        $solutionsCounter = 0

        #if output file does not exist, create it - it should exist since the master solutions are already there
        $fileToOutput = $sourceFolder + $solutionsToScanResult
        if(-Not (Test-Path $fileToOutput))
        {
            $fileoutput = New-Item -Path $fileToOutput -ItemType File
        }
        else
        {
            $fileoutput = $fileToOutput
        }

		foreach($solution in $Hash_modified.Keys)
		{
			#Write-Host test for the intersection - if a modified solution is also in the list of all solution and NOT added yet due to be a Core Solution - then add it"
			if($Hash_solutions.ContainsKey($solution) -And -Not $Hash_solutionsCore.ContainsKey($solution))
			{
				Write-Host "found intersection/solution to be added into the file." + $solution 
				Add-Content $fileoutput -Value $solution
				$solutionsCounter = $solutionsCounter + 1
			}
		}

		Write-Host "number of intersections/already scanned solutions found in master and modified files OR already scanned: " + $solutionsCounter
	}
	catch
	{
		Write-Host "FindIntersectionSolutions raised an exception error: " + $Error[0]
	}
   
}

#Main Code

#1 archive old output files for history purposes - only archiving the last run
ArchiveOldFiles $sourceFolder $solutionsToScanResult $solutionsToScanArchived

#2 initialize the output with the core solutions - always necessary to be scanned
InitializeCoreSolutions $sourceFolder $coreSolutionsAlwaysInclude $solutionsToScanResult $Hash_solutionsCore

#3: find the modified solutions to add on top of the core solutions - this will be the final output of solutions to scan
FindIntersectionSolutions $sourceFolder $masterCatalogSolutions $solutionsModifiedinTFS $coreSolutionsAlwaysInclude $solutionsToScanResult $Hash_solutions $Hash_modified $Hash_solutionsCore
