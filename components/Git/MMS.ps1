#*******************************************************************
# Purpose
#     This script is not run directly, but is desgined to be 
#     called from the script:  GitGetLatest.ps1
#
#     This file is intended to be a master list of all MMS
#     Git repositories, and provides an easy way to perform
#     a "get latest" on multiple repositories all at once.
#
#     Example Powershell command line:
#     The following assumes both scripts are in the same folder.
#         GitGetLatest.ps1 MMS.ps1
#     OR you can fully qualify the path.
#         GitGetLatest.ps1 c:\somefolder\MMS.ps1
#
# How to use
#     See notes in:  GitGetLatest.ps1
#*******************************************************************

#  NOTE:  "ProcessRepo" is a function in GitGetLatest.ps1.
#
#  Syntax for following instructions:
#      // The following uses the inRepoName for the localFolderName
#      ProcessRepo "C:\UA3\Source" "inRepoName"
#      OR
#      // The following maps the repo to a specified localFolderName
#      ProcessRepo "C:\UA3\Source" "inRepoName" "localFolderName"
#

# Common repos
ProcessRepo "C:\UA3\Source" "mms-cms-pkgs" "Packages" -deleteExisting
ProcessRepo "C:\UA3\Source" "mms-cms-util"       #Utilities
ProcessRepo "C:\UA3\Source" "mms-cms-core"       #Core

# Module repos
ProcessRepo "C:\UA3\Source" "mms-cms-adm"        #Administration
ProcessRepo "C:\UA3\Source" "mms-cms-authdeter"  #AuthDetermination
ProcessRepo "C:\UA3\Source" "mms-cms-clm"        #ClaimsManagement
ProcessRepo "C:\UA3\Source" "mms-cms-cfg"        #ConfigurationManagement
ProcessRepo "C:\UA3\Source" "mms-cms-cm"         #CorrespondenceMgmt
ProcessRepo "C:\UA3\Source" "mms-cms-dr"         #DrugRebate
ProcessRepo "C:\UA3\Source" "mms-hp-dr"          #DrugRebatePortal
ProcessRepo "C:\UA3\Source" "mms-cms-edi"        #EDI
ProcessRepo "C:\UA3\Source" "mms-cms-empmgmt"    #EmployeeMgmt
ProcessRepo "C:\UA3\Source" "mms-cms-fxfer"      #FileTransfer
ProcessRepo "C:\UA3\Source" "mms-cms-fm"         #FinancialManagement
ProcessRepo "C:\UA3\Source" "mms-cms-im"         #IdentityManagement
ProcessRepo "C:\UA3\Source" "mms-cms-inx"        #Integration
ProcessRepo "C:\UA3\Source" "mms-cms-mc"         #ManagedCare
ProcessRepo "C:\UA3\Source" "mms-hp-mco"         #ManagedCarePortal
ProcessRepo "C:\UA3\Source" "mms-cms-mbr"        #MemberManagement
ProcessRepo "C:\UA3\Source" "mms-hp-mbr"         #MemberPortal
ProcessRepo "C:\UA3\Source" "mms-cms-note"       #Notifications
ProcessRepo "C:\UA3\Source" "mms-cms-oc"         #OneClickDeploy
ProcessRepo "C:\UA3\Source" "mms-cms-paae"       #Prior Auth Automation
ProcessRepo "C:\UA3\Source" "mms-cms-pc"         #ProviderCredentialing
ProcessRepo "C:\UA3\Source" "mms-cms-pe"         #ProviderEnrollment
ProcessRepo "C:\UA3\Source" "mms-cms-pl"         #PlanManagement
ProcessRepo "C:\UA3\Source" "mms-cms-pi"         #ProgramIntegrity
ProcessRepo "C:\UA3\Source" "mms-cms-pict"       #ProgramIntegrityCT
ProcessRepo "C:\UA3\Source" "mms-cms-pm"         #ProviderManagement
ProcessRepo "C:\UA3\Source" "mms-hp-prov"        #ProviderPortal
ProcessRepo "C:\UA3\Source" "mms-cms-ss"         #Screening
ProcessRepo "C:\UA3\Source" "mms-cms-tm"         #TaskManagement
ProcessRepo "C:\UA3\Source" "mms-cms-tplct"      #TPLCaseTracking
ProcessRepo "C:\UA3\Source" "mms-cms-tplp"       #TPLPolicy
ProcessRepo "C:\UA3\Source" "mms-cms-tps"        #ThirdPartySource
ProcessRepo "C:\UA3\Source" "mms-cms-tst"        #Test	