#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     1. See the status of Cloned MES repositories
#
# How to use
#     1. git-status "Administration" "<Folder name within C:\UA3\Source>"
#*******************************************************************

Function git-status () {
    $rootFolder = "C:\UA3\Source"
	#$branch = $args[0]
	$repoFolder = $args[0]
    Set-Location -Path $rootFolder -ErrorAction Stop
    if (Test-Path -Path $repoFolder) {
        
        cd $repoFolder
        if (git rev-parse --is-inside-work-tree) {
             Write-Output "************************* Status for $repoFolder ****************************"
             git status
        }
    }
    else{
       Write-Output "Folder not found $repoFolder"
    }
}
       


git-status "Administration"
git-status "AuthDetermination"
git-status "ClaimsManagement"
git-status "ConfigurationManagement"
git-status "Core"
git-status "CorrespondenceMgmt"
git-status "DrugRebate"
git-status "EDI"
git-status "EmployeeMgmt"
git-status "FinancialManagement"
git-status "FileTransfer"
git-status "IdentityManagement"
git-status "Integration"
git-status "MemberManagement"
git-status "ManagedCare"
git-status "Notifications"
git-status "ProviderCredentialing"
git-status "ProviderEnrollment"
git-status "ProgramIntegrity"
git-status "ProgramIntegrityCT"
git-status "PlanManagement"
git-status "ProviderManagement"
git-status "Screening"
git-status "TaskManagement"
git-status "TPLCaseTracking"
git-status "TPLPolicy"
git-status "DrugRebatePortal"
git-status "MemberPortal"
git-status "ManagedCarePortal"
git-status "ProviderPortal"


#git-status "Packages"
#git-status "OneClickDeploy"
#git-status "Test"
#git-status "ThirdPartySource"
#git-status "Utilities"