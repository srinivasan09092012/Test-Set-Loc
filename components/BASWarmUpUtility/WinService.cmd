rem -- This script will be used to install event router windows service 
rem -- [Note: Standard folder structure to be used, otherwise below path should be modified accordingly]
@echo off
set eventRouterLocation=C:\UA3\Source\Utilities\Dev\BASWarmUpUtility\WarmUpProvider.Service\bin\x64\Debug\WarmUpProvider.Service.exe
set installUtilDir=c:\windows\microsoft.net\framework64\v4.0.30319\
set JsonFileLocation=C:\UA3\Source\Utilities\Dev\WarmUpProviderUtilityTool\WarmUpProvider.Service\bin\x64\Debug\
set destinationFolder = C:\Windows\System32
set serviceName=BASWarmUpUtility
CD %installUtilDir%
echo *********Stopping BASWarmUpUtility Windows Service**************
timeout 3
SC stop %serviceName%
echo *********Un-installing BASWarmUpUtility Windows Service**************
timeout 3
installutil.exe -u %eventRouterLocation%
echo *********Installing BASWarmUpUtility Windows Service**************
timeout 5
installutil.exe -i %eventRouterLocation%
echo *********Starting BASWarmUpUtility Windows Service**************
timeout 3
SC start %serviceName%
rem echo *********Copying JSON file from bin to destination**************
rem CD %JsonFileLocation%
rem xcopy /f /r /y  "EndpointInformation.json" "C:\Windows\System32"
rem echo *********BASWarmUpUtility installation completed**************
pause