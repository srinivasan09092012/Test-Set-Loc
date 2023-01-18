@echo on
echo "XSDs Generation started"
SET SVCUTIL=C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\Bin\NETFX 4.6.1 Tools\x64\SvcUtil.exe
SET ProjectPath="%1"
SET DestinationPath="%2"

IF NOT EXIST "%SVCUTIL%" GOTO NOMSB

"%SVCUTIL%"  "%ProjectPath%" /dconly "%DestinationPath%"    

GOTO :EOF
:NOMSB
echo. 
echo SVCUTIL not found 
echo. 
GOTO :EOF 


