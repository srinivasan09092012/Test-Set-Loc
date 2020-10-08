@echo off
c:
cd C:\Program Files (x86)\MSBuild\14.0\Bin
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\Administration\BroadCastMessage\Administration_BroadCastMessage.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\DrugRebate\DrugRebateFull.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\Integration\Integration.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\ManagedCare\ManagedCare.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\PlanManagement\Grouping\PL_Grouping.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\PlanManagement\RateSettings\PL_RateSettings.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\PlanManagement\RelatedInformation\PL_RelatedInformation.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\PlanManagement\CodeInformation\PlanM_CodeInformation.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\ProviderEnrollment\ProviderEnrollment.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\ProgramIntegrity\ProgramIntegrity.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\ProviderManagement\PManagement.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\TaskM\TaskM.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\TPLC\TPLCaseTracking.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\TPLPolicy\TPLPolicy.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\ProviderCredentialing\Audit\ProviderCredentialingAudit.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\ProviderCredentialing\FileManagement\ProviderCredentialing-FileAdmin.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\ProviderCredentialing\ValidCredentials\ProviderCredentialing-ValidCredentials.shfbproj
