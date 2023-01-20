@ECHO OFF
SET ThisScriptsDirectory=%~dp0..\..\
SET PowerShellScriptPath=%ThisScriptsDirectory%updateModule.ps1
PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& '%PowerShellScriptPath%' -pathFileLibrary 'J:\www\UA3\SandCastleCustomizationTool\ShbDoc\ShbDoc_Dev\HP.HSP.UA3.CodeInformation\HP.HSP.UA3.PlanManagement.BAS.CodeInformation.xml' -pathFileConfXML 'J:\UA3\SandCastleCustomizationTool\ModulesSettings\Processed - Active Modules\PlanManagement_CodeInformation2.xml'"

if %ERRORLEVEL% EQU 1 (
	GOTO ExecuteSC
)
GOTO:eof

:ExecuteSC
c:
cd C:\Program Files (x86)\MSBuild\14.0\Bin
start MSBuild.exe /p:CleanIntermediates=True /p:Configuration=Release J:\UA3\SandCastleCustomizationTool\ShbDoc\PlanManagement\CodeInformation\PlanM_CodeInformation.shfbproj