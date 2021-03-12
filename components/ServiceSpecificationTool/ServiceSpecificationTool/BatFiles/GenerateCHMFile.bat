@echo off
echo ".CHM Batch started"
SET MSBUILD=C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\amd64\MSBuild.exe
SET ProjectPath=%1

IF NOT EXIST "%MSBUILD%" GOTO NOMSB

"%MSBUILD%" %ProjectPath% 

GOTO :EOF
:NOMSB
echo. 
echo MSBUILD not found 
echo. 
GOTO :EOF 

