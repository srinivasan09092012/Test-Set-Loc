#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     1. Fetch, Pull and Push operations on Cloned MES repositories.
#
# How to use
#     1. git-fetch-pull-push <Fetch> <Pull> <Push> "<Folder name within C:\UA3\Source>"
#     2. Options for <Fetch> <Pull> and <Push> are $true or $false
#*******************************************************************

Function git-fetch-pull-push () {
    $rootFolder = "C:\UA3\Source"
	$fetch = $args[0]
	$pull = $args[1]
    $push = $args[2]
    $repoFolder = $args[3]
    Set-Location -Path $rootFolder -ErrorAction Stop
    if (Test-Path -Path $repoFolder) {
        cd $repoFolder
        if (git rev-parse --is-inside-work-tree) {
            if($fetch -eq $true){
                Write-Output "************************* Fetching $repoFolder ****************************"
                git fetch
             }

             if($pull -eq $true){
                Write-Output "************************* Pulling $repoFolder ****************************"
                git pull
             }

             if($push -eq $true){
                Write-Output "************************* Pushing $repoFolder ****************************"
                git push
             }
        }
    }
    else{
       Write-Output "Folder not found $repoFolder"
    }
}
  
$fetch = $true
$pull = $true
$push = $false


git-fetch-pull-push $fetch $pull $push "Administration"
git-fetch-pull-push $fetch $pull $push "AuthDetermination"
git-fetch-pull-push $fetch $pull $push "ConfigurationManagement"
git-fetch-pull-push $fetch $pull $push "ClaimsManagement"
git-fetch-pull-push $fetch $pull $push "CorrespondenceMgmt"
git-fetch-pull-push $fetch $pull $push "Core"
git-fetch-pull-push $fetch $pull $push "DrugRebate"
git-fetch-pull-push $fetch $pull $push "EDI"
git-fetch-pull-push $fetch $pull $push "EmployeeMgmt"
git-fetch-pull-push $fetch $pull $push "FinancialManagement"
git-fetch-pull-push $fetch $pull $push "FileTransfer"
git-fetch-pull-push $fetch $pull $push "IdentityManagement"
git-fetch-pull-push $fetch $pull $push "Integration"
git-fetch-pull-push $fetch $pull $push "MemberManagement"
git-fetch-pull-push $fetch $pull $push "ManagedCare"
git-fetch-pull-push $fetch $pull $push "Notifications"
git-fetch-pull-push $fetch $pull $push "ProviderCredentialing"
git-fetch-pull-push $fetch $pull $push "ProviderEnrollment"
git-fetch-pull-push $fetch $pull $push "ProgramIntegrity"
git-fetch-pull-push $fetch $pull $push "ProgramIntegrityCT"
git-fetch-pull-push $fetch $pull $push "PlanManagement"
git-fetch-pull-push $fetch $pull $push "ProviderManagement"
git-fetch-pull-push $fetch $pull $push "Screening"
git-fetch-pull-push $fetch $pull $push "TaskManagement"
git-fetch-pull-push $fetch $pull $push "TPLCaseTracking"
git-fetch-pull-push $fetch $pull $push "TPLPolicy"
git-fetch-pull-push $fetch $pull $push "DrugRebatePortal"
git-fetch-pull-push $fetch $pull $push "MemberPortal"
git-fetch-pull-push $fetch $pull $push "ManagedCarePortal"
git-fetch-pull-push $fetch $pull $push "ProviderPortal"




#git-fetch-pull-push $fetch $pull $push "OneClickDeploy"
#git-fetch-pull-push $fetch $pull $push "Packages"
#git-fetch-pull-push $fetch $pull $push "Test"
#git-fetch-pull-push $fetch $pull $push "ThirdPartySource"
#git-fetch-pull-push $fetch $pull $push "Utilities"