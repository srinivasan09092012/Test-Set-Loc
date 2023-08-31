#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     1. Delete a branch on Cloned MES repositories with an option to delete remote branch in server
#     
#
# How to use
#     1. git-delete-branch "<branch name to delete>" <Delete remote branch> "<Folder name within C:\UA3\Source>"
#     2. Options for <Delete remote branch> are $true or $false
#*******************************************************************

Function git-delete-branch () {
    $rootFolder = "C:\UA3\Source"
	$branch = $args[0]
    $deleteRemoteBranch = $args[1]
	$repoFolder = $args[2]
    Set-Location -Path $rootFolder -ErrorAction Stop
    if (Test-Path -Path $repoFolder) {
        
        cd $repoFolder
        if (git rev-parse --is-inside-work-tree) {
            Write-Output "****************************************Deleting local branch $branch from $repoFolder**********************************************"
            git branch -D $branch
            if($deleteRemoteBranch -eq $true) {
            Write-Output "****************************************Deleting remote branch $branch from $repoFolder**********************************************"
               git push origin --delete $branch
            }
        }
    }
}



$branch = "r20-US95064-Mongo-Driver-Upgrade"
$deleteRemoteBranch = $true

git-delete-branch $branch $deleteRemoteBranch "Administration"
git-delete-branch $branch $deleteRemoteBranch "AuthDetermination"
git-delete-branch $branch $deleteRemoteBranch "ConfigurationManagement"
git-delete-branch $branch $deleteRemoteBranch "ClaimsManagement"
git-delete-branch $branch $deleteRemoteBranch "CorrespondenceMgmt"
git-delete-branch $branch $deleteRemoteBranch "Core"
git-delete-branch $branch $deleteRemoteBranch "DrugRebate"
git-delete-branch $branch $deleteRemoteBranch "EDI"
git-delete-branch $branch $deleteRemoteBranch "EmployeeMgmt"
git-delete-branch $branch $deleteRemoteBranch "FinancialManagement"
git-delete-branch $branch $deleteRemoteBranch "FileTransfer"
git-delete-branch $branch $deleteRemoteBranch "IdentityManagement"
git-delete-branch $branch $deleteRemoteBranch "Integration"
git-delete-branch $branch $deleteRemoteBranch "MemberManagement"
git-delete-branch $branch $deleteRemoteBranch "ManagedCare"
git-delete-branch $branch $deleteRemoteBranch "Notifications"
git-delete-branch $branch $deleteRemoteBranch "ProviderCredentialing"
git-delete-branch $branch $deleteRemoteBranch "ProviderEnrollment"
git-delete-branch $branch $deleteRemoteBranch "ProgramIntegrity"
git-delete-branch $branch $deleteRemoteBranch "ProgramIntegrityCT"
git-delete-branch $branch $deleteRemoteBranch "PlanManagement"
git-delete-branch $branch $deleteRemoteBranch "ProviderManagement"
git-delete-branch $branch $deleteRemoteBranch "Screening"
git-delete-branch $branch $deleteRemoteBranch "TaskManagement"
git-delete-branch $branch $deleteRemoteBranch "TPLCaseTracking"
git-delete-branch $branch $deleteRemoteBranch "TPLPolicy"
git-delete-branch $branch $deleteRemoteBranch "DrugRebatePortal"
git-delete-branch $branch $deleteRemoteBranch "MemberPortal"
git-delete-branch $branch $deleteRemoteBranch "ManagedCarePortal"
git-delete-branch $branch $deleteRemoteBranch "ProviderPortal"



#git-delete-branch $branch $deleteRemoteBranch "OneClickDeploy"
#git-delete-branch $branch $deleteRemoteBranch "Packages"
#git-delete-branch $branch $deleteRemoteBranch "Test"
#git-delete-branch $branch $deleteRemoteBranch "ThirdPartySource"
#git-delete-branch $branch $deleteRemoteBranch "Utilities"