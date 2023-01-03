---- REPLACE ALL VALUES IN INSERT SCRIPTS BELOW FOR THE PATTERN TABLE TO WHAT THE CLIENT WANTS TO BE INSERTED INTO THOSE FIELDS.
---- THE EMAIL ADDRESS USED MUST BE REVIEWED BY THE SECURITY OFFICER OF THE CLIENT
---- THE ADDRESS FIELDS COULD BE LEFT BLANK WITH '' BUT NOT NULL AT THIS TIME. 
---- ADDING NEW FIELDS IS NOT RECOMMENDED AT THIS TIME AS THIS CODE ONLY WORKS FOR STRING VALUES WRAPPED IN DOUBLE QUOTES AND ALSO REQUIRES 
---- CHANGES TO THE VIEW PROJECTION SCRIPT LATER IN THE PROCESS. 
---- FIELDS CAN BE REMOVED IF THE CLIENTS NO LONGER WANT TO MAKE THE CHANGE BUT THE SAME CHANGE MUST ALSO BE APPLIED TO THE VIEW PROJECTION SCRIPT.

---- THE STEPS FOR THIS PROCESS WERE SPLIT UP TO ALLOW FOR A VALIDATION TO OCCUR PRIOR TO RUNNING THE COMMITS AND VIEW PROJECTION STEPS.
---- IF ANY EVENTS ARE FOUND TO BE IN ERROR AFTER RUNNING STEP 2 WHICH WILL HAVE A RECORD_STATUS = 4 THEY WILL NOT BE MODIFIED.
---- SO IF YOU CONTINUE AND THERE WERE RECORDS THEN THOSE RECORDS WOULD NOT BE DEIDENTIFIED AND WOULD NEED TO BE MANUALLY CHANGED FROM THE UX.

if exists (select * from sysobjects where name='COMMITS_DEID_AGGREGATE' and xtype='U')
begin
    drop table [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE];
    RAISERROR('Temp Table [COMMITS_DEID_AGGREGATE] Dropped.',0,1) WITH NOWAIT;
end
create table [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE]
    (
        [ID]                    int identity(1,1) not null,
        [CHECKPOINTNUMBER]      bigint            not null,
        [STREAMIDORIGINAL]      uniqueidentifier  not null,
        [PAYLOAD_CLOB]          varchar(max)      null,
        [RECORD_STATUS]         tinyint           not null
    )
    with(DATA_COMPRESSION = NONE);

    alter table [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] add constraint [COMMITS_DEID_AGGREGATE_PK] primary key clustered ([ID] asc) on [PRIMARY];

    RAISERROR('Temp Table [COMMITS_DEID_AGGREGATE] created.',0,1) WITH NOWAIT;
GO

if exists (select * from sysobjects where name='COMMITS_DEID_PATTERN' and xtype='U')
begin
    drop table [UA3_PROVIDER].[COMMITS_DEID_PATTERN];
    RAISERROR('Temp Table [COMMITS_DEID_PATTERN] Dropped.',0,1) WITH NOWAIT;
end
create table [UA3_PROVIDER].[COMMITS_DEID_PATTERN]
    (
        [PATTERN]               varchar(200)      not null,
        [REPLACEMENT_VALUE]     varchar(200)      null
    )
    with(DATA_COMPRESSION = NONE);

    RAISERROR('Temp Table [COMMITS_DEID_PATTERN] created.',0,1) WITH NOWAIT;
GO

if exists (select * from sysindexes where name='COMMITS_DEID_AGGREGATE_IX0')
begin
    drop index [COMMITS_DEID_AGGREGATE_IX0] on [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE];

    RAISERROR('Index [COMMITS_DEID_AGGREGATE_IX0] dropped for temp table [COMMITS_DEID_AGGREGATE].',0,1) WITH NOWAIT;
end;
GO 

set nocount on;
GO

begin
    insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('EmailAddress":"', 'example@gainwelltechnologies.com');
    insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('Email":"', 'example@gainwelltechnologies.com');
    --insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('"FinancialInstitutionRoutingNumber":"', '999999999');
    --insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('"FinancialInstitutionAccountNumber":"', '999999999');
    --insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('"FinancialInstitutionName":"', 'Bank of Unknown');
    --insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('"FinancialInstitutionAddressLine1":"', '312 Hurricane Lane');
    --insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('"FinancialInstitutionAddressLine2":"', 'Suite 100');
    --insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('"FinancialInstitutionCity":"', 'Williston');
    --insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('"FinancialInstitutionState":"', 'VT');
    --insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('"FinancialInstitutionPostalCode":"', '054950000');
    --insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('"FinancialInstitutionCountry":"', 'US');
    --insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('"FinancialInstitutionPhoneNumber":"', '5555555555');
    --insert into [UA3_PROVIDER].[COMMITS_DEID_PATTERN] ([PATTERN], [REPLACEMENT_VALUE]) values ('"FinancialInstitutionPhoneNumberExtension":"', '1234');

    RAISERROR('Finished initial load of temp table [COMMITS_DEID_PATTERN].',0,1) WITH NOWAIT;
end;

begin
    declare @overallStartDate datetime, 
            @startDate datetime, 
            @endDate datetime,
            @rowcount int,
            @time time(0),
            @patterns varchar(2000) = '',
            @delimiter char(1) = '|', 
            @message varchar(2000); 

    select @patterns = coalesce(@patterns + @delimiter, '') + [PATTERN] from [UA3_PROVIDER].[COMMITS_DEID_PATTERN];

    select @overallStartDate = sysdatetime(), @startDate = sysdatetime();

    RAISERROR('Starting load of temp table: [COMMITS_DEID_AGGREGATE].',0,1) WITH NOWAIT;

    WITH commits_CTE 
    AS   
    (  
        select a.[CHECKPOINTNUMBER],
               a.[STREAMIDORIGINAL],
               cast(a.[PAYLOAD] as varchar(max)) as PAYLOAD_CLOB
        from [UA3_PROVIDER].[COMMITS] a
        where a.[BUCKETID] in ('ProviderAggregate','ServiceLocationAggregate')
          and not exists (select null
                          from [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] b
                          where a.[CHECKPOINTNUMBER] = b.[CHECKPOINTNUMBER]
                            and b.[RECORD_STATUS] = 1)
          --and (a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('8DF5213C-014A-4812-B5B4-795F983EA6E1') as varchar(100))),2)
          -- or  a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('2391FDA6-8FDA-4DB2-9A1E-A89599F61D43') as varchar(100))),2)
          -- or  a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('8C55D68E-DD94-45E1-BC22-F2987D118586') as varchar(100))),2)
          -- or  a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('ABF996ED-27F6-454C-AB64-AD2193FB52FA') as varchar(100))),2)
          -- or  a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('79D58182-95DE-48A4-85F2-2179460321E2') as varchar(100))),2)
          -- or  a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('B6C86E81-FD30-4FDD-A648-192D2FAF22DD') as varchar(100))),2)
          -- or  a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('94C1DA36-09DA-47FA-8CCB-1FFC7EE89DCE') as varchar(100))),2)
          -- or  a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('5D18ED61-8BA0-4D84-938C-5C653A5B62D8') as varchar(100))),2)
          -- or  a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('5A2DD1FC-E984-47ED-8414-5C1FAFE619C4') as varchar(100))),2)
          -- or  a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('74AD2495-98BD-4F7E-BCE0-FBB9147B1496') as varchar(100))),2)
          -- or  a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('8A2DE27B-1302-4DC3-963B-0B41FDB482AF') as varchar(100))),2)
          -- or  a.[STREAMID] = CONVERT(nvarchar(40),HASHBYTES('SHA1', cast(lower('92A52A20-E699-48A6-82D3-9CB143AE92B0') as varchar(100))),2))
    )  
    insert into [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] 
    (
        [CHECKPOINTNUMBER],
        [STREAMIDORIGINAL],
        [PAYLOAD_CLOB],
        [RECORD_STATUS]
    )
    select distinct [CHECKPOINTNUMBER],
           [STREAMIDORIGINAL],
           [PAYLOAD_CLOB],
           0
    from commits_CTE c
         join string_split(@patterns, @delimiter) a on charindex(a.value, c.[PAYLOAD_CLOB]) > 0;

    select @rowcount = @@ROWCOUNT;
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));

    set @message = 'Finished initial load of temp table [COMMITS_DEID_AGGREGATE]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;
end;
GO

if not exists (select * from sysindexes where name='COMMITS_DEID_AGGREGATE_IX0')
begin
    create nonclustered index [COMMITS_DEID_AGGREGATE_IX0] on [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] ([STREAMIDORIGINAL])
    with(SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = ON) on [PRIMARY];

    RAISERROR('Index [COMMITS_DEID_AGGREGATE_IX0] created for temp table [COMMITS_DEID_AGGREGATE].',0,1) WITH NOWAIT;
end;
GO