@echo off
setlocal

set PS_SCRIPT="%~dp0\..\Tools\AutomatedBuildTest\CheckSolutionList.ps1"
set USAGE="CheckSolutionList.cmd <BranchName>"

IF "%1" == "" (
	echo .
	echo Incorrect Branch Parameter:
	echo     %USAGE%
	echo .
	goto End
) ELSE (
	set BRANCH=%1
)

powershell -executionPolicy bypass -nologo -file %PS_SCRIPT% -branch %BRANCH%


