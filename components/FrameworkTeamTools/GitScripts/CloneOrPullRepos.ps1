#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     
#
# How to use
#     1)
#*******************************************************************

Function git-clone-or-pull () {
    $rootFolder = "C:\UA3\Source"
	$repository = $args[0]
	$repoFolder = $args[1]
    Set-Location -Path $rootFolder -ErrorAction Stop
    if (Test-Path -Path $repoFolder) {
        Write-Output "*******************************Getting Latest from $repository***********************************"
        cd $repoFolder
        if (git rev-parse --is-inside-work-tree) {
             git pull
        }
    }
    else{
       Write-Output "Cloning Repository $repository"
       git clone https://github.com/mygainwell/$repository $repoFolder
    }
}
       


git-clone-or-pull "mms-cms-adm" "Administration"
git-clone-or-pull "mms-cms-authdeter" "AuthDetermination"
git-clone-or-pull "mms-cms-cfg" "ConfigurationManagement"
git-clone-or-pull "mms-cms-clm" "ClaimsManagement"
git-clone-or-pull "mms-cms-cm" "CorrespondenceMgmt"
git-clone-or-pull "mms-cms-core" "Core"
git-clone-or-pull "mms-cms-dr" "DrugRebate"
git-clone-or-pull "mms-cms-edi" "EDI"
git-clone-or-pull "mms-cms-empmgmt" "EmployeeMgmt"
git-clone-or-pull "mms-cms-fm" "FinancialManagement"
git-clone-or-pull "mms-cms-fxfer" "FileTransfer"
git-clone-or-pull "mms-cms-im" "IdentityManagement"
git-clone-or-pull "mms-cms-inx" "Integration"
git-clone-or-pull "mms-cms-mbr" "MemberManagement"
git-clone-or-pull "mms-cms-mc" "ManagedCare"
git-clone-or-pull "mms-cms-note" "Notifications"
git-clone-or-pull "mms-cms-pc" "ProviderCredentialing"
git-clone-or-pull "mms-cms-pe" "ProviderEnrollment"
git-clone-or-pull "mms-cms-pi" "ProgramIntegrity"
git-clone-or-pull "mms-cms-pict" "ProgramIntegrityCT"
git-clone-or-pull "mms-cms-pl" "PlanManagement"
git-clone-or-pull "mms-cms-pm" "ProviderManagement"
git-clone-or-pull "mms-cms-ss" "Screening"
git-clone-or-pull "mms-cms-tm" "TaskManagement"
git-clone-or-pull "mms-cms-tplct" "TPLCaseTracking"
git-clone-or-pull "mms-cms-tplp" "TPLPolicy"
git-clone-or-pull "mms-hp-dr" "DrugRebatePortal"
git-clone-or-pull "mms-hp-mbr" "MemberPortal"
git-clone-or-pull "mms-hp-mco" "ManagedCarePortal"
git-clone-or-pull "mms-hp-prov" "ProviderPortal"
      

       
    
    
#git-clone-or-pull "mms-cms-oc" "OneClickDeploy"
#git-clone-or-pull "mms-cms-pkgs" "Packages"
#git-clone-or-pull "mms-cms-tst" "Test"
#git-clone-or-pull "mms-cms-tps" "ThirdPartySource"
#git-clone-or-pull "mms-cms-util" "Utilities"
