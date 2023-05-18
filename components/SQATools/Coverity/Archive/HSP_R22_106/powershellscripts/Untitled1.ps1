net use W: \\bas2-ppsc5.ksaws.stg\MemberTagCorrectionTool
net use X: \\bas3-ppsc5.ksaws.stg\MemberTagCorrectionTool
net use Y: \\batch1-ppsc5.ksaws.stg\MemberTagCorrectionTool
net use Z: \\batch2-ppsc5.ksaws.stg\MemberTagCorrectionTool

del W:\ErroredMembers.txt
del W:\EligibleMembers.txt
del W:\ProcessedMembers.txt
xcopy  /Y EligibleMembers_2.txt W:
ren EligibleMembers_2.txt EligibleMembers.txt
pause
del X:\ErroredMembers.txt
del x:\EligibleMembers.txt
del X:\ProcessedMembers.txt
xcopy  /Y EligibleMembers_3.txt X:
ren EligibleMembers_3.txt EligibleMembers.txt
pause
del Y:\ErroredMembers.txt
del Y:\EligibleMembers.txt
del Y:\ProcessedMembers.txt
xcopy  /Y EligibleMembers_4.txt Y:
ren EligibleMembers_4.txt EligibleMembers.txt
pause
del Z:\ErroredMembers.txt
del Z:\EligibleMembers.txt
del Z:\ProcessedMembers.txt
xcopy  /Y EligibleMembers_5.txt X:
ren EligibleMembers_5.txt EligibleMembers.txt
pause
del ErroredMembers.txt
del EligibleMembers.txt
del ProcessedMembers.txt
ren EligibleMembers_1.txt EligibleMembers.txt
pause


$Bas2, $Bas3, $Batch1, $Batch2 = New-PSSession -ComputerName Server01,Server02,Server03


****  /MIR can DELETE files as well as copy them !

c:\temp>robocopy .\ \\5016VEQA9M\temp xcopytest.txt

-------------------------------------------------------------------------------
   ROBOCOPY     ::     Robust File Copy for Windows

-------------------------------------------------------------------------------

  Started : Monday, February 28, 2022 8:55:26 PM
2022/02/28 20:55:28 ERROR 53 (0x00000035) Getting File System Type of Destinatio
n \\5016VEQA9M\temp\
The network path was not found.

   Source : c:\temp\
     Dest - \\5016VEQA9M\temp\

    Files : xcopytest.txt

  Options : /DCOPY:DA /COPY:DAT /R:1000000 /W:30

------------------------------------------------------------------------------

2022/02/28 20:55:28 ERROR 53 (0x00000035) Accessing Destination Directory \\5016
VEQA9M\temp\
The network path was not found.
Waiting 30 seconds... Retrying...
2022/02/28 20:56:01 ERROR 53 (0x00000035) Accessing Destination Directory \\5016
VEQA9M\temp\
The network path was not found.
Waiting 30 seconds... Retrying...
2022/02/28 20:56:33 ERROR 53 (0x00000035) Accessing Destination Directory \\5016
VEQA9M\temp\
The network path was not found.
Waiting 30 seconds... Retrying...
2022/02/28 20:57:05 ERROR 53 (0x00000035) Accessing Destination Directory \\5016
VEQA9M\temp\
The network path was not found.
Waiting 30 seconds...^C
c:\temp>net use \bas3-ppsc5.ksaws.stg /user:jbriganti2 Stubbleduck1492!
System error 67 has occurred.

The network name cannot be found.


c:\temp>net use Z: \\5016VEQA9M\temp
System error 53 has occurred.

The network path was not found.


c:\temp>net use Z: \\bas3-ppsc5.ksaws.stg\temp
The command completed successfully.


c:\temp>robocopy .\ Z: xcopytest.txt

-------------------------------------------------------------------------------
   ROBOCOPY     ::     Robust File Copy for Windows

-------------------------------------------------------------------------------

  Started : Monday, February 28, 2022 9:19:35 PM
   Source : c:\temp\
     Dest : Z:\

    Files : xcopytest.txt

  Options : /DCOPY:DA /COPY:DAT /R:1000000 /W:30

------------------------------------------------------------------------------

                           1    c:\temp\
100%        Newer                      4        xcopytest.txt

------------------------------------------------------------------------------

               Total    Copied   Skipped  Mismatch    FAILED    Extras
    Dirs :         1         0         0         0         0         0
   Files :         1         1         0         0         0         0
   Bytes :         4         4         0         0         0         0
   Times :   0:00:00   0:00:00                       0:00:00   0:00:00
   Ended : Monday, February 28, 2022 9:19:35 PM


c:\temp>copy xcopytest.txt z:
Overwrite z:xcopytest.txt? (Yes/No/All):
        0 file(s) copied.

c:\temp>copy
The syntax of the command is incorrect.

c:\temp>copy ?
?
The system cannot find the file specified.
        0 file(s) copied.

c:\temp>copy /Y
The syntax of the command is incorrect.

c:\temp>copy /Y xcopytest.txt z:
        1 file(s) copied.

c:\temp>d:

D:\>cd Tools

D:\Tools>cd MemberTagCorrectionTool

D:\Tools\MemberTagCorrectionTool>dir
 Volume in drive D is DATA
 Volume Serial Number is F852-7C0F

 Directory of D:\Tools\MemberTagCorrectionTool

02/27/2022  12:39 AM    <DIR>          .
02/27/2022  12:39 AM    <DIR>          ..
01/26/2022  02:36 AM           166,400 AutoMapper.dll
01/26/2022  02:36 AM            28,672 AutoMapper.Net4.dll
01/26/2022  02:36 AM         1,161,216 AWSSDK.Core.dll
01/26/2022  02:36 AM           269,172 AWSSDK.Core.pdb
01/26/2022  02:36 AM           834,034 AWSSDK.Core.xml
01/26/2022  02:36 AM           926,720 AWSSDK.SimpleSystemsManagement.dll
01/26/2022  02:36 AM           494,352 AWSSDK.SimpleSystemsManagement.pdb
01/26/2022  02:36 AM         3,628,771 AWSSDK.SimpleSystemsManagement.xml
01/26/2022  02:36 AM           150,920 Azure.Core.dll
01/26/2022  02:36 AM           132,760 Azure.Core.xml
01/26/2022  02:36 AM            98,680 Azure.Data.AppConfiguration.dll
01/26/2022  02:36 AM            62,738 Azure.Data.AppConfiguration.xml
02/20/2022  06:02 PM    <DIR>          Configs
01/26/2022  02:36 AM            90,624 DnsClient.dll
02/26/2022  12:37 AM         3,837,924 EligibleMembers - Copy.txt
02/26/2022  11:13 PM         3,837,924 EligibleMembers.txt
01/26/2022  02:36 AM         5,196,496 EntityFramework.dll
01/26/2022  02:36 AM           621,264 EntityFramework.SqlServer.dll
02/20/2022  09:53 PM               267 ErroredMembers - Copy.csv
02/26/2022  02:00 AM                 0 ErroredMembers - Copy.txt
02/27/2022  12:39 AM                99 ErroredMembers.csv
02/27/2022  12:39 AM                38 ErroredMembers.txt
10/22/2021  01:20 AM           208,896 FileHelpers.dll
10/22/2021  01:20 AM           471,122 FileHelpers.xml
01/26/2022  02:36 AM           227,328 HP.HSP.UA3.ClaimsManagement.BAS.ClaimsMan
agement.Contracts.dll
01/26/2022  02:36 AM            17,408 HP.HSP.UA3.Core.API.AddressValidation.Dat
a.dll
01/26/2022  02:36 AM            26,112 HP.HSP.UA3.Core.API.AddressValidation.dll

01/26/2022  02:36 AM             5,120 HP.HSP.UA3.Core.API.AddressValidation.Int
erfaces.dll
01/26/2022  02:36 AM            95,232 HP.HSP.UA3.Core.API.FileManagement.dll
02/01/2022  06:52 PM         1,260,544 HP.HSP.UA3.Core.BAS.CQRS.dll
02/01/2022  06:52 PM             3,771 HP.HSP.UA3.Core.BAS.CQRS.dll.config
02/01/2022  06:52 PM         2,620,928 HP.HSP.UA3.Core.BAS.CQRS.pdb
01/26/2022  02:36 AM            31,744 HP.HSP.UA3.FinancialManagement.BAS.Financ
ialManagementSvc.Contracts.dll
01/26/2022  02:36 AM            43,520 HP.HSP.UA3.Integration.BAS.EventGroupWatc
her.Contracts.dll
02/16/2022  06:07 PM         3,607,552 HP.HSP.UA3.ManagedCare.BAS.ManagedCare.Bu
siness.dll
02/10/2022  09:58 AM         4,793,344 HP.HSP.UA3.ManagedCare.BAS.ManagedCare.Co
ntracts.dll
02/10/2022  09:58 AM         6,569,472 HP.HSP.UA3.ManagedCare.BAS.ManagedCare.Da
taAccess.dll
02/01/2022  09:31 PM           160,768 HP.HSP.UA3.ManagedCare.BAS.ManagedCare.dl
l
01/26/2022  02:36 AM           835,072 HP.HSP.UA3.MemberManagement.BAS.MemberInf
ormation.Contracts.dll
01/26/2022  02:36 AM         1,239,552 HP.HSP.UA3.ProviderEnrollment.BAS.Enrollm
entSvc.Contracts.dll
01/26/2022  02:36 AM         2,997,248 HP.HSP.UA3.ProviderManagement.BAS.Provide
rSvc.Contracts.dll
01/26/2022  02:36 AM           243,712 HP.HSP.UA3.TaskManagement.BAS.TaskManagem
ent.Contracts.dll
01/26/2022  02:36 AM         1,631,744 HP.HSP.UA3.TPLPolicy.BAS.Policy.Contracts
.dll
09/25/2020  10:39 AM             7,168 HPE.HSP.UA3.Core.API.AuthManagement.Inter
face.dll
09/25/2020  10:39 AM            17,408 HPE.HSP.UA3.Core.API.AuthManagement.Provi
ders.dll
02/03/2022  01:31 AM            14,336 HPE.HSP.UA3.Core.API.BusinessRules.Interf
ace.dll
02/03/2022  01:42 AM            40,960 HPE.HSP.UA3.Core.API.BusinessRules.Provid
ers.dll
01/26/2022  02:36 AM            13,312 HPE.HSP.UA3.Core.API.CorrespondenceManage
ment.Interface.dll
01/26/2022  02:36 AM            95,744 HPE.HSP.UA3.Core.API.CorrespondenceManage
ment.Providers.dll
01/26/2022  02:36 AM            25,088 HPE.HSP.UA3.Core.API.Logger.dll
01/26/2022  02:36 AM            12,288 HPP.Core.API.AppConfig.dll
01/26/2022  02:36 AM           101,376 IdentityModel.dll
01/26/2022  02:36 AM           308,224 InRule.Common.dll
01/26/2022  02:36 AM         3,305,472 InRule.Repository.dll
01/26/2022  02:36 AM         4,053,504 InRule.Runtime.dll
01/31/2022  04:44 PM             3,348 InRuleLicense.xml
01/26/2022  02:36 AM           276,480 log4net.dll
01/26/2022  02:36 AM         1,547,797 log4net.xml
02/10/2022  09:23 PM            18,432 MemberEligibilityCorrectionTool.exe
02/20/2022  07:38 PM             6,020 MemberEligibilityCorrectionTool.exe.confi
g
02/20/2022  06:27 PM             6,020 MemberEligibilityCorrectionTool.exe.confi
g.bak
02/02/2022  07:34 PM            34,304 MemberEligibilityCorrectionTool.pdb
01/26/2022  02:36 AM            20,856 Microsoft.Bcl.AsyncInterfaces.dll
01/26/2022  02:36 AM            18,215 Microsoft.Bcl.AsyncInterfaces.xml
01/26/2022  02:36 AM            18,112 Microsoft.Practices.ServiceLocation.dll
01/26/2022  02:36 AM            86,232 Microsoft.Practices.Unity.Configuration.d
ll
01/26/2022  02:36 AM           132,280 Microsoft.Practices.Unity.dll
01/26/2022  02:36 AM            28,400 Microsoft.Practices.Unity.RegistrationByC
onvention.dll
01/26/2022  02:36 AM           449,536 MongoDB.Bson.dll
01/26/2022  02:36 AM           673,792 MongoDB.Driver.Core.dll
01/26/2022  02:36 AM           634,880 MongoDB.Driver.dll
01/26/2022  02:36 AM           313,344 MongoDB.Driver.Legacy.dll
01/26/2022  02:36 AM         1,690,624 NEventStore.dll
01/26/2022  02:36 AM            38,912 NEventStore.Persistence.MongoDB.dll
01/26/2022  02:36 AM           652,288 Newtonsoft.Json.dll
01/26/2022  02:36 AM           674,087 Newtonsoft.Json.xml
01/26/2022  02:36 AM         5,031,936 Oracle.ManagedDataAccess.dll
01/26/2022  02:36 AM           471,040 Oracle.ManagedDataAccess.EntityFramework.
dll
02/26/2022  02:00 AM         3,837,924 ProcessedMembers - Copy.txt
02/27/2022  12:39 AM         3,837,886 ProcessedMembers.txt
01/26/2022  02:36 AM            20,992 QueryInterceptor.Core.dll
01/26/2022  02:36 AM           346,112 StackExchange.Redis.dll
01/26/2022  02:36 AM            28,304 System.Buffers.dll
01/26/2022  02:36 AM             3,481 System.Buffers.xml
01/26/2022  02:36 AM            63,864 System.Diagnostics.DiagnosticSource.dll
01/26/2022  02:36 AM            34,414 System.Diagnostics.DiagnosticSource.xml
01/26/2022  02:36 AM           138,496 System.IdentityModel.Tokens.Jwt.dll
01/26/2022  02:36 AM           188,313 System.IdentityModel.Tokens.Jwt.xml
01/26/2022  02:36 AM           148,760 System.Memory.dll
01/26/2022  02:36 AM            13,950 System.Memory.xml
01/26/2022  02:36 AM           115,856 System.Numerics.Vectors.dll
01/26/2022  02:36 AM           183,484 System.Numerics.Vectors.xml
01/26/2022  02:36 AM            16,760 System.Runtime.CompilerServices.Unsafe.dl
l
01/26/2022  02:36 AM            17,409 System.Runtime.CompilerServices.Unsafe.xm
l
01/26/2022  02:36 AM            57,720 System.Text.Encodings.Web.dll
01/26/2022  02:36 AM            62,933 System.Text.Encodings.Web.xml
01/26/2022  02:36 AM           294,264 System.Text.Json.dll
01/26/2022  02:36 AM           223,732 System.Text.Json.xml
01/26/2022  02:36 AM            33,008 System.Threading.Tasks.Extensions.dll
01/26/2022  02:36 AM            10,147 System.Threading.Tasks.Extensions.xml
01/26/2022  02:36 AM            25,232 System.ValueTuple.dll
01/26/2022  02:36 AM               142 System.ValueTuple.xml
01/26/2022  02:36 AM         1,056,256 Vertica.Data.dll
             102 File(s)     80,210,514 bytes
               3 Dir(s)  279,796,473,856 bytes free

D:\Tools\MemberTagCorrectionTool>rm EligibleMembers.txt
'rm' is not recognized as an internal or external command,
operable program or batch file.

D:\Tools\MemberTagCorrectionTool>del EligibleMembers.txt

D:\Tools\MemberTagCorrectionTool>rename
The syntax of the command is incorrect.

D:\Tools\MemberTagCorrectionTool>rn
'rn' is not recognized as an internal or external command,
operable program or batch file.

D:\Tools\MemberTagCorrectionTool>move
The syntax of the command is incorrect.

D:\Tools\MemberTagCorrectionTool>ren EligibleMembers_2 - Copy,txt E.txt
The syntax of the command is incorrect.

D:\Tools\MemberTagCorrectionTool>ren EligibleMembers_1.txt E.txt

D:\Tools\MemberTagCorrectionTool>net use W: \\bas2-ppsc5.ksaws.stg\MemberTagCorr
ectionTool
The command completed successfully.


D:\Tools\MemberTagCorrectionTool>net use X: \\bas3-ppsc5.ksaws.stg\MemberTagCorr
ectionTool
The command completed successfully.


D:\Tools\MemberTagCorrectionTool>net use Y: \\batch1-ppsc5.ksaws.stg\MemberTagCo
rrectionTool
The command completed successfully.


D:\Tools\MemberTagCorrectionTool>net use Z: \\batch2-ppsc5.ksaws.stg\MemberTagCo
rrectionTool
The command completed successfully.


D:\Tools\MemberTagCorrectionTool>del W:\ErroredMembers.txt

D:\Tools\MemberTagCorrectionTool>del W:\EligibleMembers.txt

D:\Tools\MemberTagCorrectionTool>del W:\ProcessedMembers.txt

D:\Tools\MemberTagCorrectionTool>