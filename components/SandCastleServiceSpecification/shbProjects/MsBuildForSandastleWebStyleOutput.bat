@echo off
c:
cd C:\Program Files (x86)\MSBuild\14.0\Bin
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\Administration\Administration_BroadCastMessage.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\DrugRebate\DrugRebate.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\Finantial\Finantial.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\ManagedCare\ManagedCare.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\MemberManagement\MemberMngt.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\PlanManagement\CodeInformation\CodeInformation.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\PlanManagement\Grouping\PL_Grouping.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\PlanManagement\RateSettings\PL_RateSettings.shfbproj.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\PlanManagement\RelatedInformation\RelatedInformation.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\ProgramIntegrity\ProgramIntegrity.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\ProviderEnrollment\ProviderEnrollment.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\ProviderManagement\PManagement.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\TaskM\TaskM.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\TPLC\TPLCaseTracking.shfbproj
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Debug J:\UA3\SandCastleCustomizationTool\ShbDoc\TPLPolicy\TPLPolicy.shfbproj
exit