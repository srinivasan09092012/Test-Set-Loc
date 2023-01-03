----THIS IS THE NO TURNING BACK POINT. AFTER THIS RUNS ALL THE VIEW PROJECTION RECORDS DATA WILL BE CHANGED FOREVER EXCEPT FROM A DATABASE BACKUP.

----UNCOMMENT THE UPDATE STATEMENTS BELOW ONCE YOU HAVE VERIFIED THIS IS THE CORRECT DATABASE SCHEMA. THE SCRIPTS ARE COMMENTED OUT TO PREVENT ACCIDENTAL RUNNING IN THE WRONG SCHEMA.

set nocount on;
GO

begin
    declare @overallStartDate datetime, 
            @startDate datetime, 
            @endDate datetime,
            @rowcount int,
            @time time(0),
            @RC int,  
            @replacementEmail varchar(200) = '',
            @replacementAccountNumber varchar(200) = '',
            @replacementRoutingNumber varchar(200) = '',
            @replacementName varchar(200) = '',
            @replacementAdressLine1 varchar(200) = '',
            @replacementAddressLine2 varchar(200) = '',
            @replacementCity varchar(200) = '',
            @replacementState varchar(200) = '',
            @replacementPostalCode varchar(200) = '',
            @replacementCountry varchar(200) = '',
            @replacementPhoneNumber varchar(200) = '',
            @replacementPhoneNumberExt varchar(200) = '',
            @message varchar(2000); 
            
    select @replacementEmail = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = 'EmailAddress":"';
    select @replacementAccountNumber = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = '"FinancialInstitutionAccountNumber":"';
    select @replacementRoutingNumber = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = '"FinancialInstitutionRoutingNumber":"';
    select @replacementName = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = '"FinancialInstitutionName":"';
    select @replacementAdressLine1 = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = '"FinancialInstitutionAddressLine1":"';
    select @replacementAddressLine2 = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = '"FinancialInstitutionAddressLine2":"';
    select @replacementCity = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = '"FinancialInstitutionCity":"';
    select @replacementState = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = '"FinancialInstitutionState":"';
    select @replacementPostalCode = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = '"FinancialInstitutionPostalCode":"';
    select @replacementCountry = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = '"FinancialInstitutionCountry":"';
    select @replacementPhoneNumber = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = '"FinancialInstitutionPhoneNumber":"';
    select @replacementPhoneNumberExt = [REPLACEMENT_VALUE] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN] where [PATTERN] = '"FinancialInstitutionPhoneNumberExtension":"';

    select @overallStartDate = sysdatetime(), @startDate = sysdatetime();
    RAISERROR('Starting update of view projections.',0,1) WITH NOWAIT;

    --update [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION]
    --    set [EMAIL_ADDRESS] = @replacementEmail
    --from [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] a
    --    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] b on a.[PROVIDER_SERVICE_LOCATION_ID] = b.[STREAMIDORIGINAL]
    --where a.[EMAIL_ADDRESS] is not null
    --    and b.[RECORD_STATUS] = 2;

    select @rowcount = @@ROWCOUNT;
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));

    set @message = '    Finished update of view projection [PROVIDER_SERVICE_LOCATION]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;
    
        select @startDate = sysdatetime();
    set @rowcount = 0;

    ---- If the financial columns should not be updated then adjust accordingly the update statement

    --update [UA3_PROVIDER].[BASE_EFT_ENROLLMENT]
    --    set [PRVDR_AGENT_CNTCT_EMAIL_ADR] = case when [PRVDR_AGENT_CNTCT_EMAIL_ADR] is not null then @replacementEmail else null end
    --        , [PRVDR_CNTCT_EMAIL_ADR] = case when [PRVDR_CNTCT_EMAIL_ADR] is not null then @replacementEmail else null end
    --        --, [FINCL_INSTN_ACNT_NUM] = case when [FINCL_INSTN_ACNT_NUM] is not null then @replacementAccountNumber else null end
    --        --, [FINCL_INSTN_ROUTG_NUM] = case when [FINCL_INSTN_ROUTG_NUM] is not null then @replacementRoutingNumber else null end
    --        --, [FINCL_INSTN_NAME] = case when [FINCL_INSTN_NAME] is not null then @replacementName else null end
    --        --, [FINCL_INSTN_ADR_LINE_1] = case when [FINCL_INSTN_ADR_LINE_1] is not null then @replacementAdressLine1 else null end
    --        --, [FINCL_INSTN_ADR_LINE_2] = case when [FINCL_INSTN_ADR_LINE_2] is not null then @replacementAddressLine2 else null end
    --        --, [FINCL_INSTN_CITY] = case when [FINCL_INSTN_CITY] is not null then @replacementCity else null end
    --        --, [FINCL_INSTN_STATE_CD] = case when [FINCL_INSTN_STATE_CD] is not null then @replacementState else null end
    --        --, [FINCL_INSTN_POSTAL_CD] = case when [FINCL_INSTN_POSTAL_CD] is not null then @replacementPostalCode else null end
    --        --, [FINCL_INSTN_COUNTRY_CD] = case when [FINCL_INSTN_COUNTRY_CD] is not null then @replacementCountry else null end
    --        --, [FINCL_INSTN_PHONE_NUM] = case when [FINCL_INSTN_PHONE_NUM] is not null then @replacementPhoneNumber else null end
    --        --, [FINCL_INSTN_PHONE_EXT] = case when [FINCL_INSTN_PHONE_EXT] is not null then @replacementPhoneNumberExt else null end
    --from [UA3_PROVIDER].[BASE_EFT_ENROLLMENT] e
    --    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on e.[PROVIDER_ID] = a.[STREAMIDORIGINAL]
    --where a.[RECORD_STATUS] = 2;

    select @rowcount = @@ROWCOUNT;
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));

    set @message = '    Finished update of view projection [BASE_EFT_ENROLLMENT]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;
    
    select @startDate = sysdatetime();
    set @rowcount = 0;
    
    ---- If the financial columns should not be updated then adjust accordingly the update statement

    --update [UA3_PROVIDER].[EFT_ENROLLMENT]
    --    set [PRVDR_AGENT_CNTCT_EMAIL_ADR] = case when [PRVDR_AGENT_CNTCT_EMAIL_ADR] is not null then @replacementEmail else null end
    --        , [PRVDR_CNTCT_EMAIL_ADR] = case when [PRVDR_CNTCT_EMAIL_ADR] is not null then @replacementEmail else null end
    --        --, [FINCL_INSTN_ACNT_NUM] = case when [FINCL_INSTN_ACNT_NUM] is not null then @replacementAccountNumber else null end
    --        --, [FINCL_INSTN_ROUTG_NUM] = case when [FINCL_INSTN_ROUTG_NUM] is not null then @replacementRoutingNumber else null end
    --        --, [FINCL_INSTN_NAME] = case when [FINCL_INSTN_NAME] is not null then @replacementName else null end
    --        --, [FINCL_INSTN_ADR_LINE_1] = case when [FINCL_INSTN_ADR_LINE_1] is not null then @replacementAdressLine1 else null end
    --        --, [FINCL_INSTN_ADR_LINE_2] = case when [FINCL_INSTN_ADR_LINE_2] is not null then @replacementAddressLine2 else null end
    --        --, [FINCL_INSTN_CITY] = case when [FINCL_INSTN_CITY] is not null then @replacementCity else null end
    --        --, [FINCL_INSTN_STATE_CD] = case when [FINCL_INSTN_STATE_CD] is not null then @replacementState else null end
    --        --, [FINCL_INSTN_POSTAL_CD] = case when [FINCL_INSTN_POSTAL_CD] is not null then @replacementPostalCode else null end
    --        --, [FINCL_INSTN_COUNTRY_CD] = case when [FINCL_INSTN_COUNTRY_CD] is not null then @replacementCountry else null end
    --        --, [FINCL_INSTN_PHONE_NUM] = case when [FINCL_INSTN_PHONE_NUM] is not null then @replacementPhoneNumber else null end
    --        --, [FINCL_INSTN_PHONE_EXT] = case when [FINCL_INSTN_PHONE_EXT] is not null then @replacementPhoneNumberExt else null end
    --from [UA3_PROVIDER].[EFT_ENROLLMENT] e
    --    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PROVIDER_SERVICE_LOCATION_ID] = e.[PROVIDER_SERVICE_LOCATION_ID]
    --    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
    --where a.[RECORD_STATUS] = 2;

    select @rowcount = @@ROWCOUNT;
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));

    set @message = '    Finished update of view projection [EFT_ENROLLMENT]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;
    
    select @startDate = sysdatetime();
    set @rowcount = 0;

    --update [UA3_PROVIDER].[ERA_ENROLLMENT]
    --    set [CLRGHSE_CNTCT_EMAIL] = case when [CLRGHSE_CNTCT_EMAIL] is not null then @replacementEmail else null end,
    --        [PRVDR_AGENT_CNTCT_EMAIL_ADR] = case when [PRVDR_AGENT_CNTCT_EMAIL_ADR] is not null then @replacementEmail else null end,
    --        [PRVDR_CNTCT_EMAIL_ADR] = case when [PRVDR_CNTCT_EMAIL_ADR] is not null then @replacementEmail else null end,
    --        [VNDR_CNTCT_EMAIL] = case when [VNDR_CNTCT_EMAIL] is not null then @replacementEmail else null end
    --from [UA3_PROVIDER].[ERA_ENROLLMENT] e
    --    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PROVIDER_SERVICE_LOCATION_ID] = e.[PROVIDER_SERVICE_LOCATION_ID]
    --    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
    --where a.[RECORD_STATUS] = 2;

    select @rowcount = @@ROWCOUNT;
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));

    set @message = '    Finished update of view projection [ERA_ENROLLMENT]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    select @startDate = sysdatetime();
    set @rowcount = 0;

    --update [UA3_PROVIDER].[PRACTICE_LCTN_ADR_ASSCN]
    --    set [EMAIL_ADR] = @replacementEmail
    --from [UA3_PROVIDER].[PRACTICE_LCTN_ADR_ASSCN] e
    --    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PRACTICE_LOCATION_ID] = e.[PRACTICE_LOCATION_ID]
    --    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
    --where e.[EMAIL_ADR] is not null
    --    and a.[RECORD_STATUS] = 2;

    select @rowcount = @@ROWCOUNT;
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));

    set @message = '    Finished update of view projection [PRACTICE_LCTN_ADR_ASSCN]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    select @startDate = sysdatetime();
    set @rowcount = 0;

    --update [UA3_PROVIDER].[PROVIDER_OWNER_ASSCN]
    --    set [EMAIL_ADR] = @replacementEmail
    --from [UA3_PROVIDER].[PROVIDER_OWNER_ASSCN] e
    --    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PROVIDER_SERVICE_LOCATION_ID] = e.[PROVIDER_SERVICE_LOCATION_ID]
    --    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
    --where e.[EMAIL_ADR] is not null
    --    and a.[RECORD_STATUS] = 2;

    select @rowcount = @@ROWCOUNT;
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));

    set @message = '    Finished update of view projection [PROVIDER_OWNER_ASSCN]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    set @time = cast((@endDate-@overallStartDate) as time(0));

    select @startDate = sysdatetime();
    set @rowcount = 0;

    --update [UA3_PROVIDER].[PROVIDER_NOTIFY_REPORT]
    --  set [NOTIFY_EMAIL_ADR] = @replacementEmail
    --from [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s 
    --  join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
    --where [NOTIFY_EMAIL_ADR] is not null
    --  and a.[RECORD_STATUS] = 2;


    select @rowcount = @@ROWCOUNT;
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));

    set @message = '    Finished update of view projection [PROVIDER_NOTIFY_REPORT]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    set @time = cast((@endDate-@overallStartDate) as time(0));

    select @startDate = sysdatetime();
    set @rowcount = 0;

   --update [UA3_PROVIDER].[PROVIDER_NOTIFY_HISTORY] 
   --  set [INFO_CHANGE_DETAIL] = '<?xml version="1.0" encoding="utf-16"?><ProviderDataChanges xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"><DataChanges><DataChange><ColumnName></ColumnName><BeforeValue></BeforeValue><AfterValue></AfterValue><IsDifferent>false</IsDifferent></DataChange></DataChanges></ProviderDataChanges>'
   --from [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s 
   --  join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
   --where a.[RECORD_STATUS] = 2
   --  and [UA3_PROVIDER].[PROVIDER_NOTIFY_HISTORY].[NOTIFICATION_CATEGORY] = 'ProvChangeNotif';

    select @rowcount = @@ROWCOUNT;
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));

    set @message = '    Finished update of view projection [PROVIDER_NOTIFY_HISTORY]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    set @message = 'Finished update of view projections. Duration: ' + cast(@time as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    select @startDate = sysdatetime();
    set @rowcount = 0;

    ---- Update all records to be record status 3 to allow for the last step to filter on those records.

    --update [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE]
    --    set [RECORD_STATUS] = 3
    --from [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a
    --    join [UA3_PROVIDER].[COMMITS] b on a.[CHECKPOINTNUMBER] = b.[CHECKPOINTNUMBER]
    --where a.[RECORD_STATUS] = 2;

    select @rowcount = @@ROWCOUNT
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));    
    set @message = 'Finished updating [COMMITS_DEID_AGGREGATE] table from status 2 to 3. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;
end;
GO