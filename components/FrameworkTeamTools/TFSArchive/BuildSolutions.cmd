@echo off
setlocal

set PS_SCRIPT="%~dp0\..\Tools\AutomatedBuildTest\BuildSolutions.ps1"
set USAGE="BuildSolutions.cmd <BranchName> <Target [List|Clean|"Clean,Build"|"Restore,Clean,Build"|...]> <Debug|Release> [Filter]"

IF "%1" == "" (
	echo .
	echo Incorrect Branch Parameter:
	echo     %USAGE%
	echo .
	goto End
) ELSE (
	set BRANCH=%1
)


IF "%~2" == "" (
	echo .
	echo Incorrect Target Parameter:
	echo     %USAGE%
	echo .
	goto End
) ELSE (
	set TARGETS=%~2
)

IF /I "%3" == "Debug" (
	set CONFIGURATION=Debug
) ELSE IF /I "%3" == "Release" (
	set CONFIGURATION=Release
) ELSE (
	echo .
	echo Incorrect Configuration Parameter:
	echo     %USAGE%
	echo .
	goto End
)

set FILTER="%4"

powershell -executionPolicy bypass -nologo -file %PS_SCRIPT% -branch %BRANCH% -configuration %CONFIGURATION% -target %TARGETS% -filter %FILTER%

goto End


:End


