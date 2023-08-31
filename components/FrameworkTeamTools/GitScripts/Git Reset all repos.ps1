#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     1. Reset and clean the cloned MES repositories.
#     2. Discard uncommitted changes
#     3. Remove / delete build artifacts (like bin / obj folders)
#
# How to use
#     1. git-reset "<Repository name>" "<Folder name within C:\UA3\Source>"
#*******************************************************************

Function git-reset () {
    $rootFolder = "C:\UA3\Source"
	$repository = $args[0]
	$repoFolder = $args[1]
    Set-Location -Path $rootFolder -ErrorAction Stop
    if (Test-Path -Path $repoFolder) {
        Write-Output "****************************************Resetting Repository $repository ($repoFolder)**********************************************"
        cd $repoFolder
        if (git rev-parse --is-inside-work-tree) {
             git reset --hard
             git clean -fxd
        }
    }
}

git-reset "mms-cms-adm" "Administration"
git-reset "mms-cms-authdeter" "AuthDetermination"
git-reset "mms-cms-cfg" "ConfigurationManagement"
git-reset "mms-cms-clm" "ClaimsManagement"
git-reset "mms-cms-cm" "CorrespondenceMgmt"
git-reset "mms-cms-core" "Core"
git-reset "mms-cms-dr" "DrugRebate"
git-reset "mms-cms-edi" "EDI"
git-reset "mms-cms-empmgmt" "EmployeeMgmt"
git-reset "mms-cms-fm" "FinancialManagement"
git-reset "mms-cms-fxfer" "FileTransfer"
git-reset "mms-cms-im" "IdentityManagement"
git-reset "mms-cms-inx" "Integration"
git-reset "mms-cms-mbr" "MemberManagement"
git-reset "mms-cms-mc" "ManagedCare"
git-reset "mms-cms-note" "Notifications"
git-reset "mms-cms-pc" "ProviderCredentialing"
git-reset "mms-cms-pe" "ProviderEnrollment"
git-reset "mms-cms-pi" "ProgramIntegrity"
git-reset "mms-cms-pict" "ProgramIntegrityCT"
git-reset "mms-cms-pl" "PlanManagement"
git-reset "mms-cms-pm" "ProviderManagement"
git-reset "mms-cms-ss" "Screening"
git-reset "mms-cms-tm" "TaskManagement"
git-reset "mms-cms-tplct" "TPLCaseTracking"
git-reset "mms-cms-tplp" "TPLPolicy"
git-reset "mms-hp-dr" "DrugRebatePortal"
git-reset "mms-hp-mbr" "MemberPortal"
git-reset "mms-hp-mco" "ManagedCarePortal"
git-reset "mms-hp-prov" "ProviderPortal"





#git-reset "mms-cms-oc" "OneClickDeploy"
#git-reset "mms-cms-pkgs" "Packages"
#git-reset "mms-cms-tst" "Test"
#git-reset "mms-cms-tps" "ThirdPartySource"
#git-reset "mms-cms-util" "Utilities"