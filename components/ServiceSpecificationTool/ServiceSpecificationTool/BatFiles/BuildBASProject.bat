@echo on
echo "Build Project started"
SET MSBUILD=C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\amd64\MSBuild.exe
SET ProjectPath=%1%
SET TempDllsOutPutDir=%2%

set _path=%ProjectPath%
for %%a in (%_path%) do (
set p_dir=%%~dpa
)

for %%a in (%p_dir:~0,-1%) do (
set OutPutDir=%p_dir%
 )

For %%A in (%_path%) do (
    set FileName=%%~nA.xml
) 

"%MSBUILD%" %ProjectPath% /t:Clean;Rebuild /p:OutDir=%TempDllsOutPutDir% /p:platform=x64 /p:Disable_CopyWebApplication=True,Configuration="Release",DocumentationFile=%FileName%

echo "Copying files from Temporary folder to Actual Directory"

set ActualDir=%OutPutDir%\bin\Release
IF not exist %ActualDir% (mkdir %ActualDir%)

xcopy "%TempDllsOutPutDir%" "%ActualDir%" /h /i /c /k /e /r /y

echo "Cleaning the temporary folder"
rd /s /q "%TempDllsOutPutDir%\"

GOTO :EOF
:NOMSB
echo. 
echo MSBUILD not found 
echo. 
GOTO :EOF          
         
