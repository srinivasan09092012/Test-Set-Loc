#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     1. Switch to a target branch in Cloned MES repositories.
#
# How to use
#     1. git-switch-to-branch "<Target branch>" "<Folder name within C:\UA3\Source>"
#*******************************************************************

Function git-switch-to-branch () {
    $rootFolder = "C:\UA3\Source"
	$branch = $args[0]
	$repoFolder = $args[1]
    Set-Location -Path $rootFolder -ErrorAction Stop
    if (Test-Path -Path $repoFolder) {
        cd $repoFolder
        if (git rev-parse --is-inside-work-tree) {
             Write-Output "********************************* $repoFolder ************************************"
             git switch $branch
        }
    }
    else{
       Write-Output "Folder not found $repoFolder"
    }
}
       
$branch = "main"
#$branch = "r20"

git-switch-to-branch $branch "Administration"
git-switch-to-branch $branch "AuthDetermination"
git-switch-to-branch $branch "ConfigurationManagement"
git-switch-to-branch $branch "ClaimsManagement"
git-switch-to-branch $branch "CorrespondenceMgmt"
git-switch-to-branch $branch "Core"
git-switch-to-branch $branch "DrugRebate"
git-switch-to-branch $branch "EDI"
git-switch-to-branch $branch "EmployeeMgmt"
git-switch-to-branch $branch "FinancialManagement"
git-switch-to-branch $branch "FileTransfer"
git-switch-to-branch $branch "IdentityManagement"
git-switch-to-branch $branch "Integration"
git-switch-to-branch $branch "MemberManagement"
git-switch-to-branch $branch "ManagedCare"
git-switch-to-branch $branch "Notifications"
git-switch-to-branch $branch "ProviderCredentialing"
git-switch-to-branch $branch "ProviderEnrollment"
git-switch-to-branch $branch "ProgramIntegrity"
git-switch-to-branch $branch "ProgramIntegrityCT"
git-switch-to-branch $branch "PlanManagement"
git-switch-to-branch $branch "ProviderManagement"
git-switch-to-branch $branch "Screening"
git-switch-to-branch $branch "TaskManagement"
git-switch-to-branch $branch "TPLCaseTracking"
git-switch-to-branch $branch "TPLPolicy"
git-switch-to-branch $branch "DrugRebatePortal"
git-switch-to-branch $branch "MemberPortal"
git-switch-to-branch $branch "ManagedCarePortal"
git-switch-to-branch $branch "ProviderPortal"



#git-switch-to-branch $branch "OneClickDeploy"
#git-switch-to-branch $branch "Packages"
#git-switch-to-branch $branch "Test"
#git-switch-to-branch $branch "ThirdPartySource"
#git-switch-to-branch $branch "Utilities"