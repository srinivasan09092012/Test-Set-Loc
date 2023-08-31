#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     1. Rebase the checked out branch of, Cloned MES repositories to the head of its base branch
#     
#
# How to use
#     1. git-rebase "<Base branch>" "<Checked-out branch>" "<Folder name within C:\UA3\Source>"
#     2. Rebase might result in conflicts which needs to be resolved after running this script.
#*******************************************************************

Function git-rebase () {
    $rootFolder = "C:\UA3\Source"
	$baseBranch = $args[0]
    $branchBeingRebased = $args[1]
	$repoFolder = $args[2]
    Set-Location -Path $rootFolder -ErrorAction Stop
    if (Test-Path -Path $repoFolder) {
        
        cd $repoFolder
        if ((git rev-parse --is-inside-work-tree) -and (git rev-parse --abbrev-ref HEAD -eq $branchBeingRebased)) {
             Write-Output "****************************************Rebasing checked out branch $branchBeingRebased of $repoFolder with base branch $baseBranch**********************************************"
             git fetch
             git rebase $baseBranch
             
             #git rebase --abort
        }
    }
}
$branchBeingRebased = "main-US58749-MigratetoPR" 
#$branchBeingRebased = "r20-US95064-Mongo-Driver-Upgrade"     
$baseBranch = "main"

git-rebase $baseBranch $branchBeingRebased "Administration"
git-rebase $baseBranch $branchBeingRebased "AuthDetermination"
git-rebase $baseBranch $branchBeingRebased "ConfigurationManagement"
git-rebase $baseBranch $branchBeingRebased "ClaimsManagement"
git-rebase $baseBranch $branchBeingRebased "CorrespondenceMgmt"
git-rebase $baseBranch $branchBeingRebased "Core"
git-rebase $baseBranch $branchBeingRebased "DrugRebate"
git-rebase $baseBranch $branchBeingRebased "EDI"
git-rebase $baseBranch $branchBeingRebased "EmployeeMgmt"
git-rebase $baseBranch $branchBeingRebased "FinancialManagement"
git-rebase $baseBranch $branchBeingRebased "FileTransfer"
git-rebase $baseBranch $branchBeingRebased "IdentityManagement"
git-rebase $baseBranch $branchBeingRebased "Integration"
git-rebase $baseBranch $branchBeingRebased "MemberManagement"
git-rebase $baseBranch $branchBeingRebased "ManagedCare"
git-rebase $baseBranch $branchBeingRebased "Notifications"
git-rebase $baseBranch $branchBeingRebased "ProviderCredentialing"
git-rebase $baseBranch $branchBeingRebased "ProviderEnrollment"
git-rebase $baseBranch $branchBeingRebased "ProgramIntegrity"
git-rebase $baseBranch $branchBeingRebased "ProgramIntegrityCT"
git-rebase $baseBranch $branchBeingRebased "PlanManagement"
git-rebase $baseBranch $branchBeingRebased "ProviderManagement"
git-rebase $baseBranch $branchBeingRebased "Screening"
git-rebase $baseBranch $branchBeingRebased "TaskManagement"
git-rebase $baseBranch $branchBeingRebased "TPLCaseTracking"
git-rebase $baseBranch $branchBeingRebased "TPLPolicy"
git-rebase $baseBranch $branchBeingRebased "DrugRebatePortal"
git-rebase $baseBranch $branchBeingRebased "MemberPortal"
git-rebase $baseBranch $branchBeingRebased "ManagedCarePortal"
git-rebase $baseBranch $branchBeingRebased "ProviderPortal"



#git-rebase $baseBranch $branchBeingRebased "Packages"
#git-rebase $baseBranch $branchBeingRebased "OneClickDeploy"
#git-rebase $baseBranch $branchBeingRebased "Test"
#git-rebase $baseBranch $branchBeingRebased "ThirdPartySource"
#git-rebase $baseBranch $branchBeingRebased "Utilities"