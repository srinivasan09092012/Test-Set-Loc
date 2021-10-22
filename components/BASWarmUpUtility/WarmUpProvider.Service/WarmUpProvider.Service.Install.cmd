rem -- This script will be used to install event router windows service 
rem -- [Note: Standard folder structure to be used, otherwise below path should be modified accordingly]
@echo off
set warmUpProviderLocation=C:\UA3\Source\Utilities\Dev\BASWarmUpUtility\WarmUpProvider.Service\bin\x64\Release\WarmUpProvider.Service.exe
set installUtilDir=c:\windows\microsoft.net\framework64\v4.0.30319\
set destinationFolder = C:\Windows\System32
set serviceName=BASWarmUpUtility
CD %installUtilDir%
echo *********Stopping BASWarmUpUtility Windows Service**************
timeout 3
SC stop %serviceName%
echo *********Un-installing BASWarmUpUtility Windows Service**************
timeout 3
installutil.exe -u %warmUpProviderLocation%
echo *********Installing BASWarmUpUtility Windows Service**************
timeout 5
installutil.exe -i %warmUpProviderLocation%
echo *********Starting BASWarmUpUtility Windows Service**************
timeout 3
SC start %serviceName%
pause