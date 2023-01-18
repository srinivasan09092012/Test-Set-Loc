rem -- This script will be used to install event router windows service 
rem -- [Note: Standard folder structure to be used, otherwise below path should be modified accordingly]
@echo off
set warmUpProviderLocation=C:\WamUp\BackUp\UXWarmUpService\UXWarmUpService\bin\x64\Release\UXWarmUpService.exe
set installUtilDir=c:\windows\microsoft.net\framework64\v4.0.30319\
set destinationFolder = C:\Windows\System32
set serviceName=UxWarmUpService
CD %installUtilDir%
echo *********Stopping UxWarmUpService Windows Service**************
timeout 5
SC stop %serviceName%
echo *********Un-installing UxWarmUpService Windows Service**************
timeout 5
installutil.exe -u %warmUpProviderLocation%
rem --echo *********Installing UxWarmUpService Windows Service**************
rem --timeout 5
rem --installutil.exe -i %warmUpProviderLocation%
rem --echo *********Starting UxWarmUpService Windows Service**************
rem --timeout 3 
rem -- SC start %serviceName%
pause