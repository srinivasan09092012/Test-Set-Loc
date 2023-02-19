#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     perform a "get latest" on any number of Git 
#     repositories.  
#
# How to use
#     1) In Visual Studio
#     2) Select View -> Terminal
#     3) Specify a full path to this script.
#     4) Press Enter
#*******************************************************************

# Common parent folder where all repos will reside
$mRootFolder = 'C:\UA3\Source'

$constPromptNonePullOrClone = 1
$constPromptNonePullOnly = 2
$constPromptIfNotCloned = 3
$constPromptAll = 4
$constPromptExit = 9
$mPromptOption = $constPromptNonePullOrClone;

#-------------------------------------------------------------------
# Primary method that processes all repos.
# This is called from in-line code at the bottom of this script.
# Comment out any repos/lines you don't want
#-------------------------------------------------------------------
Function Main()
{
	#
	#  Syntax for following instructions:
	#      // The following uses the inRepoName for the localFolderName
	#      ProcessRepo "inRepoName"
	#      OR
	#      // The following maps the repo to a specified localFolderName
	#      ProcessRepo "inRepoName" "localFolderName"
	#

	# Common repos
	ProcessRepo "mms-cms-pkgs" "Packages"
	ProcessRepo "mms-cms-util"       #Utilities
	ProcessRepo "mms-cms-core"       #Core

	# Module repos
	ProcessRepo "mms-cms-adm"        #Administration
	ProcessRepo "mms-cms-authdeter"  #AuthDetermination
	ProcessRepo "mms-cms-clm"        #ClaimsManagement
	ProcessRepo "mms-cms-cfg"        #ConfigurationManagement
	ProcessRepo "mms-cms-cm"         #CorrespondenceMgmt
	ProcessRepo "mms-cms-dr"         #DrugRebate
	ProcessRepo "mms-hp-dr"          #DrugRebatePortal
	ProcessRepo "mms-cms-edi"        #EDI
	ProcessRepo "mms-cms-empmgmt"    #EmployeeMgmt
	ProcessRepo "mms-cms-fxfer"      #FileTransfer
	ProcessRepo "mms-cms-fm"         #FinancialManagement
	ProcessRepo "mms-cms-im"         #IdentityManagement
	ProcessRepo "mms-cms-inx"        #Integration
	ProcessRepo "mms-cms-mc"         #ManagedCare
	ProcessRepo "mms-hp-mco"         #ManagedCarePortal
	ProcessRepo "mms-cms-mbr"        #MemberManagement
	ProcessRepo "mms-hp-mbr"         #MemberPortal
	ProcessRepo "mms-cms-note"       #Notifications
	ProcessRepo "mms-cms-oc"         #OneClickDeploy
	ProcessRepo "mms-cms-pc"         #ProviderCredentialing
	ProcessRepo "mms-cms-pe"         #ProviderEnrollment
	ProcessRepo "mms-cms-pl"         #PlanManagement
	ProcessRepo "mms-cms-pi"         #ProgramIntegrity
	ProcessRepo "mms-cms-pict"       #ProgramIntegrityCT
	ProcessRepo "mms-cms-pm"         #ProviderManagement
	ProcessRepo "mms-hp-prov"        #ProviderPortal
	ProcessRepo "mms-cms-ss"         #Screening
	ProcessRepo "mms-cms-tm"         #TaskManagement
	ProcessRepo "mms-cms-tplct"      #TPLCaseTracking
	ProcessRepo "mms-cms-tplp"       #TPLPolicy
	ProcessRepo "mms-cms-tps"        #ThirdPartySource
	ProcessRepo "mms-cms-tst"        #Test	
}

#-------------------------------------------------------------------
# This function will either "clone" or "pull" the specified 
# repo, while optionally mapping the repo to a different local 
# folder name.
#
# Parameters    
#    inRepoName
#        Name of the repository in GitHub
#    inRepoFolder (Optional)
#        Optional name of the local folder where the repo will be
#        downloaded to.  Usually, but not always, it is recommended
#        to use the same name as the inRepoName.
#-------------------------------------------------------------------
Function ProcessRepo() 
{
	$inRepoName = $args[0]
	$inRepoFolder = $args[1]

	# Use the repoName as the repoFolder if a folder name was not provided. 
    if (($null -eq $inRepoFolder) -or ($inRepoFolder -eq ""))
	{
		$inRepoFolder = $inRepoName;
	}
	
	$fullRepoFolderName = -join($mRootFolder, "\", $inRepoFolder)
	
	# Separate output for each repo with a blank line 
	Write-Output ""
	Write-Output "-------------------------------------------------------------------"
		
	 Set-Location -Path $mRootFolder -ErrorAction Stop
	if (Test-Path -Path $inRepoFolder) 
    {	
		Set-Location $inRepoFolder

		if (git rev-parse --is-inside-work-tree) 
        {
			$currentBranch = &git rev-parse --abbrev-ref HEAD
			
			PullRepo $inRepoName $fullRepoFolderName $currentBranch
		}

		Set-Location $mRootFolder
	}
	else
    {
		CloneRepo $inRepoName $inRepoFolder $fullRepoFolderName
	}
}

#-------------------------------------------------------------------
# Pull the specified Git repo
#
# Parameters    
#    inRepoName
#        Name of the repository in GitHub
#    inRepoFolder
#        Name of the local folder where the repo will be
#        downloaded to.
#    inFullFolderName
#        Fully qualified name of the local folder where the repo 
#        will be downloaded to.
#-------------------------------------------------------------------
Function PullRepo()
{
	$inRepoName = $args[0]
	$inFullFolderName = $args[1]
	$inCurrentBranch = $args[2]

	Write-Output "REPO NAME:      $inRepoName"
	Write-Output "FOLDER:         $inFullFolderName"
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
	
	Write-Output "COMMAND:        git pull --all --rebase"
	git pull --all --rebase	
}

#-------------------------------------------------------------------
# Clone the specific Git repo.
#
# Parameters:    
#    inRepoName
#        Name of the repository in GitHub
#    inRepoFolder
#        Name of the local folder where the repo will be
#        downloaded to.
#    inFullFolderName
#        Fully qualified name of the local folder where the repo 
#        will be downloaded to.
#-------------------------------------------------------------------
Function CloneRepo() 
{
	$inRepoName = $args[0]
	$inRepoFolder = $args[1]
	$inFullFolderName = $args[2]

	Write-Output "REPO NAME:      $inRepoName"
	Write-Output "FOLDER:         $inFullFolderName"
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
		Set-Location $mRootFolder
	 	git clone https://github.com/mygainwell/$inRepoName $inRepoFolder
	}
	else
	{
		Write-Output "NOT YET CLONED.  SKIPPED."
	}
}

#*******************************************************************
# In-line code
#*******************************************************************

# Ask about clearing the nuget packages folder
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
	Main
	Write-Output ""
	Write-Output "-------------------------------------------------------------------"
	Write-Output "COMPLETED"
	Write-Output ""
}