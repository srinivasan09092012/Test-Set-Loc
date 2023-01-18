@echo on
echo "Build Solution started"
SET MSBUILD=C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\amd64\MSBuild.exe
SET SolutionPath=%1 

For %%A in ("%ProjectPath%") do (
    set FileName=%%~nA.xml
)

"%MSBUILD%" %SolutionPath% /t:Clean;Rebuild

GOTO :EOF
:NOMSB
echo. 
echo MSBUILD not found 
echo. 
GOTO :EOF          
