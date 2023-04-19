#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     perform a "get latest" on any number of Git 
#     repositories.  
#
# How to use
#     1) In Visual Studio
#     2) Select View -> Terminal
#     3) Specify a full path to this script along with the 
#        name of the script that controls which repos to
#        process.
#        Example Powershell command line:
#        The following assumes both scripts are in the same folder.
#            GitGetLatest.ps1 MMS.ps1
#        OR you can fully qualify the path.
#            GitGetLatest.ps1 c:\somefolder\MMS.ps1
#     4) Press Enter and follow prompts.
#*******************************************************************

# Constants for user prompt options
$constPromptNonePullOrClone = 1
$constPromptNonePullOnly = 2
$constPromptIfNotCloned = 3
$constPromptAll = 4
$constPromptExit = 9
$mPromptOption = $constPromptNonePullOrClone;

#-------------------------------------------------------------------
# This function will either "clone" or "pull" the specified 
# repo, while optionally mapping the repo to a different local 
# folder name.
#
# Parameters    
#    inRepoRootFolder
#        Fully qualified root folder where the local repository 
#        will reside.
#    inRepoName
#        Name of the repository in GitHub
#    inRepoFolder (Optional)
#        Optional name of the local folder where the repo will be
#        downloaded to.  This will be a subfolder of inRepoRootFolder.
#        Usually, but not always, it is recommended to use the same 
#        name as the inRepoName.
#-------------------------------------------------------------------
Function ProcessRepo() 
{
	$inRepoRootFolder = $args[0]
	$inRepoName = $args[1]
	$inRepoFolder = $args[2]

	# Use the repoName as the repoFolder if a folder name was not provided. 
	if (($null -eq $inRepoFolder) -or ($inRepoFolder -eq ""))
	{
		$inRepoFolder = $inRepoName;
	}
	
	$fullRepoFolderName = -join($inRepoRootFolder, "\", $inRepoFolder)
	
	# Separate output for each repo with a blank line 
	Write-Output ""
	Write-Output "-------------------------------------------------------------------"
	Write-Output "REPO NAME:      $inRepoName"
	Write-Output "FOLDER:         $fullRepoFolderName"

	Set-Location -Path $inRepoRootFolder -ErrorAction Stop
	if (Test-Path -Path $inRepoFolder) 
	{	
		Set-Location $inRepoFolder

		if (git rev-parse --is-inside-work-tree) 
		{
			$currentBranch = &git rev-parse --abbrev-ref HEAD
			
			PullRepo $currentBranch
		}

		Set-Location $inRepoRootFolder
	}
	else
	{
		CloneRepo $inRepoName $inRepoFolder
	}
}

#-------------------------------------------------------------------
# Pull the specified Git repo
#
# Parameters    
#    inCurrentBranch
#        Name of the current branch of the repo being processed.
#-------------------------------------------------------------------
Function PullRepo()
{
	$inCurrentBranch = $args[0]

	Write-Output "CURRENT BRANCH: $inCurrentBranch"

	# Constants
	$constPullRepo = 1
	$constSkipRepo = 8
	$constExit = 9

	if ($mPromptOption -eq $constPromptAll)
	{
		$askUser = $true
		for (;$askUser;)	
		{
			Write-Output "SELECT OPTION:"
			Write-Output "    $constPullRepo = Pull this repo"
			Write-Output "    $constSkipRepo = Skip this repo"
			Write-Output "    $constExit = Cancel and exit processing"
			
			$pullOption = Read-Host
			
			switch ($pullOption)
			{
				$constPullRepo 
				{
					# exit the loop
					$askUser = $false
				}
				
				$constSkipRepo 
				{
					Write-Output "SKIPPING REPO"
					return 
				}
				
				$constExit 
				{ 
					Write-Output ""
					Write-Output "-------------------------------------------------------------------"
					Write-Output "EXIT PROCESSING"
					Write-Output ""
					Exit 
				}
			}		
		}	
	}
	
	Write-Output "COMMAND:        git pull --all"
	git pull --all
}

#-------------------------------------------------------------------
# Clone the specific Git repo.
#
# Parameters:    
#    inRepoRootFolder
#        Fully qualified root folder where the local repository 
#        will reside.
#    inRepoName
#        Name of the repository in GitHub
#    inRepoFolder
#        Name of the local folder where the repo will be
#        downloaded to.  This will be a subfolder of 
#        inRepoRootFolder.
#-------------------------------------------------------------------
Function CloneRepo() 
{
	$inRepoRootFolder = $args[0]
	$inRepoName = $args[1]      
	$inRepoFolder = $args[2]

	Write-Output "COMMAND:        git clone https://github.com/mygainwell/$inRepoName $inRepoFolder"

	# Determine if this repo should be cloned
	$cloneThisRepo = $true
	
	if ($mPromptOption -eq $constPromptNonePullOnly)
	{
		$cloneThisRepo = $false
	}
	else
	{
		if (($mPromptOption -eq $constPromptAll) -or ($mPromptOption -eq $constPromptIfNotCloned))
		{
			Write-Output "CLONE this repo?"
			Write-Output "    1 = Yes"
			Write-Output "    2 = No"
			$cloneOption = Read-Host
			
			if ($cloneOption -ne 1)
			{
				$cloneThisRepo = $false
			}
		}
	}
	
	if ($cloneThisRepo -eq $true)
	{
		Set-Location $inRepoRootFolder
		git clone https://github.com/mygainwell/$inRepoName $inRepoFolder
	}
	else
	{
		Write-Output "NOT YET CLONED.  SKIPPED."
	}
}

#-------------------------------------------------------------------
# Display error for the bad parameter
#
# Parameters:    
#   None
#-------------------------------------------------------------------
Function ShowBadParameterError()
{
		Write-Output ""
		Write-Output "Parameter invalid.  Must specify a script file containing the Git repositories to process."
		Write-Output "Example if the script file is in the same folder with this script:"
		Write-Output "    GitGetLatest.ps1 MMS.ps1"
		Write-Output "Example with fully qualified script name:"
		Write-Output "    GitGetLatest.ps1 c:\ua3\source\mms-cms-util\components\git\MMS.ps1"
		Write-Output ""

		Exit
}

#*******************************************************************
# In-line code
#*******************************************************************

#
# Get name of script containing all repos to process.
# Should be passed in as a parameter.
#
$m_inRepoListScript = $args[0]
$mRepoListScriptFullPath = $m_inRepoListScript

if ([string]::IsNullOrEmpty($mRepoListScriptFullPath))
{
	ShowBadParameterError
}

if ((Test-Path -Path $m_inRepoListScript -PathType leaf) -eq $false)
{
	# This is the path where this script is running
	$myPath = $PSScriptRoot
	$m_inRepoListScript = "$myPath\$m_inRepoListScript"
	if ((Test-Path -Path $m_inRepoListScript -PathType leaf) -eq $false)
	{
		ShowBadParameterError
	}
}

$mRepoListScriptFullPath = Resolve-Path -LiteralPath $m_inRepoListScript

#
# Ask about clearing the nuget packages folder
#
Write-Output ""
Write-Output "CLEAR your local user Nuget cache?"
Write-Output "    LOCATION: $env:USERPROFILE\.nuget\packages\"
Write-Output "    1 = Yes    Force latest HPP packages to be picked up."
Write-Output "               NOTE: The folder will repopulate as you recompile projects."
Write-Output "    2 = No     Select if you are currently testing packages not yet uploaded."
$clearUserNugetFolder = Read-Host

if ($clearUserNugetFolder -eq 1)
{
	Set-Location $env:USERPROFILE\.nuget\packages\
	Remove-Item -Recurse -Force -Path "hpp.*"
	Write-Output "PACKAGES CLEARED"
    Set-Location $PSScriptRoot
}

# Get option for when to prompt the user
$askUser = $true
for (;$askUser;)	
{
	Write-Output ""
	Write-Output "SELECT OPTION:"
	Write-Output "    $constPromptNonePullOrClone = Process ALL repos (Pull or Clone. No additional prompts)."
	Write-Output "    $constPromptNonePullOnly = Process ALL LOCAL repos (NO CLONING. No additional prompts)."
	Write-Output "    $constPromptIfNotCloned = Process ALL LOCAL repos (Prompt if NOT CLONED)."
	Write-Output "    $constPromptAll = Prompt for EACH repo."
	Write-Output "    $constPromptExit = Cancel and exit."
	
	$mPromptOption = Read-Host
	
	switch ($mPromptOption)
	{
		$constPromptNonePullOrClone {$askUser = $false}
		$constPromptNonePullOnly {$askUser = $false}
		$constPromptIfNotCloned {$askUser = $false}
		$constPromptAll {$askUser = $false}
		$constPromptExit {$askUser = $false}
	}		
}	

# Process the selected user option
if ($mPromptOption -eq $constPromptExit)
{
	Write-Output "EXIT PROCESSING"
	Write-Output ""
	Exit
}
else
{
	# Run the script that contains all the repos to process.	
	Invoke-Expression "$mRepoListScriptFullPath"

	Write-Output ""
	Write-Output "-------------------------------------------------------------------"
	Write-Output "COMPLETED"
	Write-Output ""
}