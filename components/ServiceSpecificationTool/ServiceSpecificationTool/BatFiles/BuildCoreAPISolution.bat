@echo on
echo "Build Core API solution started"
Call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\VsMSBuildCmd.bat"
SET ProjectPath=%1           

set _path=%ProjectPath%
for %%a in (%_path%) do (
set p_dir=%%~dpa 
)

for %%a in (%p_dir:~0,-1%) do (
set OutPutDir=%%~dpa
 )

cd "%ProjectPath%"   

MsBuild.exe /p:OutDir=%OutPutDir%bin\Release /p:Configuration="Release" /p:Platform="x64"

GOTO :EOF
:NOMSB
GOTO :EOF          
