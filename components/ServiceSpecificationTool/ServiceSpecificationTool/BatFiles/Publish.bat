@echo on
echo "Publish sandcastle documentation started"
SET MSDeploy=C:\Program Files\IIS\Microsoft Web Deploy V3\msdeploy.exe
SET PublishFolder=%1
SET ServerDestPath=%2
SET ServerURL=%3
SET SiteName=%4
SET ServerUserName=%5
SET ServerPassword=%6

"C:\Program Files\IIS\Microsoft Web Deploy V3\msdeploy.exe" -source:package='%PublishFolder%' -dest:contentPath='%ServerDestPath%',computerName='%ServerURL%=%SiteName%',userName='%ServerUserName%',password='%ServerPassword%',authtype='Basic',includeAcls='False' -verb:sync -disableLink:AppPoolExtension -disableLink:ContentExtension -disableLink:CertificateExtension -enableRule:DoNotDeleteRule -allowUntrusted

GOTO :EOF
:NOMSB
echo. 
echo MSDeploy not found 
echo. 
GOTO :EOF          
