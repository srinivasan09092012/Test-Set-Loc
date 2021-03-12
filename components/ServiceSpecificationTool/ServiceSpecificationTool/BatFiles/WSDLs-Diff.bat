@echo on
echo "Started Generating WSDL-Diff started"
SET WSDLDIFF=C:\Program Files (x86)\EWSoftware\soa-model-distribution-1.4.1\bin\wsdldiff.bat
SET oldWSDLPath=%1
SET LatestWSDLPath=%2
SET DestinationPath=%3

IF NOT EXIST "%WSDLDIFF%" GOTO NOMSB

"%WSDLDIFF%" "%oldWSDLPath%" "%LatestWSDLPath%" "%DestinationPath%"

GOTO :EOF
:NOMSB
echo. 
echo wsdldiff.bat not found 
echo. 
GOTO :EOF          
