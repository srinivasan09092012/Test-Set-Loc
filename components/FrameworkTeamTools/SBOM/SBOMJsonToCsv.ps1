#*******************************************************************
# Purpose
#     This powershell script can be run on demand to 
#     Download the Dependency Graph SBOM from any or all MMS-CMS repos and convert it to a usable
#     CSV file that can be used as an import to LeanIX.  
#     Output Columns of the csv:
#     ID|Type|Name|Subtype|Description|Lifecycle: End of life|Applications|Products|Providers|Tech Categories|User Groups|Latest Version|Vulnerability max severity
#
# How to use
#     1) Create a new github personal access token with the 
#        read:packages and repo scopes.
#     2) Run in the terminal:
#            .\SBOMJsonToCsv.ps1 -token "<Your Personal Access Token>"
#*******************************************************************
param(
    [string] $inputFolder = 'input',
    [string] $packageList   = $inputFolder + "\LeanIXPackageList.txt",
    [string] $outFile1 = "output/mms-cms-$(get-date -f yyyy.MM.dd.HHmm).csv",
    [string] $outFile2 = "output/mms-cms-internal-$(get-date -f yyyy.MM.dd.HHmm).csv",
    [string] $productType = "ITComponent",
    [string] $productSubType = "Software",
    [string] $productEOL = "",
    [string] $packageFilter = "",
    [string[]] $excludeFilters = @('^System', '^Microsoft'),
    [string[]] $internalIncludeFilters = @(
        '^HP\.HSP\.UA3\.',
        '^HPE\.HSP\.UA3\.',
        '^HPP\.'
    ),
    [string[]] $ignoreCheckPackageFilters = @(
        '^HP\.HSP\.UA3\.',
        '^HPE\.HSP\.UA3\.',
        '^HPP\.',
        '^Telerik',
        '^Vertica',
        '^Molina',
        '^Kendo'
    ),
    [parameter(Mandatory)]
    [string] $token = ""
)

$packageModuleTable = @{};
$repoModuleTable = @{
    "mms-cms-adm"       = "MMS Administration Module";
    "mms-cms-core"      = "MMS Core Module";
    "mms-cms-cfg"       = "MMS CorrespondenceMgmt Module";
    "mms-cms-dr"        = "MMS DrugRebate Module";
    "mms-cms-edi"       = "MMS EDI Module";
    "mms-cms-fxfer"     = "MMS FileTransfer Module";
    "mms-cms-im"        = "MMS IdentityManagement Module";
    "mms-cms-inx"       = "MMS Integration Module";
    "mms-cms-mc"        = "MMS ManagedCare Module";
    "mms-hp-mco"        = "MMS ManagedCarePortal Module";
    "mms-cms-note"      = "MMS Notifications Module";
    "mms-cms-pl"        = "MMS PlanManagement Module";
    "mms-cms-pict"      = "MMS ProgramIntegrityCT Module";
    "mms-cms-pc"        = "MMS ProviderCredentialing Module";
    "mms-cms-pe"        = "MMS ProviderEnrollment Module";
    "mms-cms-pm"        = "MMS ProviderManagement Module";
    "mms-hp-prov"       = "MMS ProviderPortal Module";
    "mms-cms-ss"        = "MMS Screening Module";
    "mms-cms-tm"        = "MMS TaskManagement Module";
    "mms-cms-tplct"     = "MMS TPLCaseTracking Module";
    "mms-cms-tplp"      = "MMS TPLPolicy Module";
    "mms-hp-mbr"        = "MMS MemberPortal Module";
    "mms-hp-dr"         = "MMS DrugRebatePortal Module";
    "mms-cms-pi"        = "MMS ProgramIntegrity Module";
    # TODO: Talk to john about this
    # "mms-cms-fm"        = "MMS Flexi Financial Module";
    "mms-cms-fm"        = "MMS FinancialManagement Module";
    "mms-cms-authdeter" = "MMS AuthorizationDetermination Module";
    "mms-cms-cef"       = "MMS Claims Administrator Module";
    "mms-cms-meddent"   = "MMS Medical Dental Portal";
    "mms-cms-paae"      = "MMS EDI Gateway";
    "mms-cms-v360"           = "MMS Vue360 Module";
};

#-------------------------------------------------------------------
# This function will check the package for vulnerabilities
# https://stackoverflow.com/questions/72184723/getting-nuget-package-vulnerability-information-from-an-api
#
# Parameters    
#    id
#        The identifier of the package on NuGet.org
#    version
#        The version of the package to check
#    returns
#        An array of length two where the first element is:
#              the latest version number of the package
#        and the second element is:
#              the list of vulnerabilites
#        null is returned if no vulnerabilites found
#-------------------------------------------------------------------
function checkPackage([string] $id, [string] $version)
{
    $packages = Invoke-RestMethod "https://azuresearch-usnc.nuget.org/query?q=$id"
    $package = $packages.data |
        Where-Object { $_.id -eq $id }
    $latestVersion = $package.version
    $packageDetailsUrl = $package |
        ForEach-Object { $_.versions } |
        Where-Object { $_.version -eq $version } | 
        ForEach-Object { $_."@id" }
    if($packageDetailsUrl)
    {
        $packageDetails = Invoke-RestMethod $packageDetailsUrl
        $packageSuperDetails = Invoke-RestMethod $packageDetails.catalogEntry
        return $latestVersion, $packageSuperDetails.vulnerabilities
    }
    echo "$id not found" >> error\log.txt
    return $null;
}

#-------------------------------------------------------------------
# This function will download the dependency-graph sbom of a given 
# repo
#
# Parameters    
#    repo
#        the name of the mygainwell repository that is being used
#        e.g. mms-cms-core or mms-hp-dr
#        The repository must have it dependecy graph set up on github
#-------------------------------------------------------------------
function retrieveDependencies([string] $repo) {
    $uri = "https://api.github.com/repos/mygainwell/$repo/dependency-graph/sbom";
    $headers = @{
        Accept = "application/vnd.github+json";
        Authorization = "Bearer $token";
    }
    $response = Invoke-RestMethod -Uri $uri -Headers $headers
    return $response.sbom;
}

#-------------------------------------------------------------------
# This function will convert a repo name to the full name of
# the module.
#
# Parameters    
#    repo
#        the name of the mygainwell repository that is being used
#        e.g. mms-cms-core or mms-hp-dr
#        The repository must be in the $repoModuleTable
#-------------------------------------------------------------------
function repoToModule([string] $repo) {
    if(!$repoModuleTable.contains($repo)) {
        "Unreconized repo $repo" >> error\log.txt;
        return;
    }
    return $repoModuleTable[$repo];
}

#-------------------------------------------------------------------
# This function will read an SBOM JSON and add the modules name
# to the list of each package that uses it. After running this
# with all the SBOM JSON for each module, there will be a table
# of packages that contain a list modules that use that package
#
# Parameters    
#    data
#        The SBOM JSON data downloaded from github
#-------------------------------------------------------------------
function addToTable($data) {
    if($null -eq $data.name) {
        return;
    }
    $repo = ($data.name).Split('/')[1];
    $module = repoToModule $repo;
    if(!$module) {
        return;
    }
    $data.packages | ForEach-Object {
        if($_.versionInfo) {
            $name = $_.name.Split(':')[1];
            $fullName = "$name $($_.versionInfo)";
            if($packageModuleTable.ContainsKey($fullName)) {
                $packageModuleTable[$fullName].modules += ";$module";
            } else {
                $packageModuleTable[$fullName] = @{
                    id = "";
                    description = "";
                    modules = $module;
                };
            }
        }
    }
}

#-------------------------------------------------------------------
# This function will check whether or not a string matches any
# of the provided filters.
#
# Parameters    
#    name
#        The name of the package
#    filters
#        The filters the name should be filtered by
#-------------------------------------------------------------------
function isInFilters([string] $name, [string[]] $filters) {
    ForEach($filter in $filters) {
        if($name -match $filter) {
            return $true;
        }
    }
    return $false;
}

function convertSeverity($severity) {
    switch($severity) {
        $null { $null }
        0 { "$severity-Low" }
        1 { "$severity-Moderate" }
        2 { "$severity-High" }
        3 { "$severity-Critical" }
    }
}

#-------------------------------------------------------------------
# This function will download files from github, then parse them
# and add their data to a table. Then, it will read the solution
# list and add it's data to the partially filled table.
# then the table is sorted and filtered based on module name
# and output into two separate files.
#-------------------------------------------------------------------
function main {
    if(!(Test-Path -Path $packageList)) {
        Write-Output "Cannot find packageList at '$packageList'";
        return;
    }

    $repoModuleTable.keys | Sort-Object | ForEach-Object {
        Write-Output "reading repo: $_";
        $data = retrieveDependencies $_;
        if($null -eq $data) {
            Write-Output "Could not read repo: $_";
        } else {
            Write-Output "Handling json file";
            addToTable $data;
        }
    }

    ForEach($line in Get-Content $packageList -Encoding UTF8) {
        if ($line.ToUpper().Contains($packageFilter.ToUpper())) {
            $packageListItems = $line.split("|");
            $packageName = $packageListItems[0];
            $productDesc = $packageListItems[1];
            $productID = $packageListItems[2];
            if($packageName){
                if(!$packageModuleTable.containsKey($packageName)) {
                    $packageModuleTable[$packageName] = @{
                        id = $productID;
                        description = $productDesc;
                        modules = "";
                    };
                } else {
                    $packageModuleTable[$packageName].id = $productID;
                    $packageModuleTable[$packageName].description = $productDesc;
                }
            } 
        }
    };

    ForEach($key in ($packageModuleTable.keys | Sort-Object)) {
        if(isInFilters $key $excludeFilters) {
            continue;
        }
        $id, $version = $key.Split(' ');
        if(!(isInFilters $id $ignoreCheckPackageFilters)) {
            Write-Output "Checking Package $key"
            $latestVersion, $vulnerabilities = checkPackage $id $version
            $maxSeverity = convertSeverity ($vulnerabilities | Measure-Object -Maximum -Property severity).Maximum
        }
        if($null -eq $latestVersion) {
            $latestVersion = ""
        }
        if($null -eq $maxSeverity) {
            $maxSeverity = ""
        }
        $val = $packageModuleTable[$key];
        $row = "$($val.id)|$productType|$key|$productSubType|$($val.description)|$productEOL|$($val.modules)|||||$latestVersion|$maxSeverity";
        if(isInFilters $key $internalIncludeFilters) {
            $row >> $outfile2;
        } else {
            $row >> $outfile1;
        }
    };
}

main
