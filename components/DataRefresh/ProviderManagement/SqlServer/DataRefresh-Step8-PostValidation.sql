---- THIS STEP IS OPTIONAL. AS WRITTEN IT WILL LOOK THROUGH ALL RECORDS IN THE COMMITS TABLE TABLE AND SCAN FOR ANY CHARACTERS OF @.
---- AS THIS IS THE REQUIRED VALUE IN AN EMAIL FOR IT TO WORK AT ALL WE WILL RENDER ALL NAME AND VALUE PAIRS ASSOCIATED TO THE JSON ELEMENT THAT CONTAINST AN @ IN THE VALUE.
---- BY LOOKING AT THE RESULTING RECORDS YOU CAN VERIFY WHAT WOULD HAPPEN AND MAKE SURE NO MISSING EMAIL ADDRESSES EXIST.
---- DURING TESTING OF A SMALL NUMBER OF RECORDS THIS SHOULD BE RUN EVERY TIME. 
---- BUT DURING THE LIVE RUN OF THE PROCESS IT IS OPTIONAL AS IT WILL TAKE SOME TIME TO PARSE AND CALCULATE ALL RECORDS VALUES.
---- IF YOU ONLY WANT A SMALL SUBSET THEN UNCOMMENT OUT THE JOIN IN THE CURSOR TO ONLY GET THOSE RECORDS THAT WERE PART OF THE DEID AGGREGATE PROCESS.

set nocount on;
GO

begin
    declare @overallStartDate datetime, 
            @startDate datetime, 
            @endDate datetime,
            @rowcount int,
            @errors int,
            @time time(0),  
            @tagstart int, 
            @tagend int,
            @start int,
            @end int,
            @eventname varchar(200),
            @split varchar(200),
            @payload_clob varchar(max),
            @pattern varchar(200), 
            @replacement varchar(200),
            @message varchar(2000),
            @isJSON bit,
            @i int,
            @email varchar(2000),
            @tag varchar(2000);

    select @startDate = sysdatetime();

    
    if exists (select * from sysobjects where name='COMMITS_EMAIL' and xtype='U')
    begin
        drop table [UA3_PROVIDER].[COMMITS_EMAIL];
        RAISERROR('Temp Table [COMMITS_EMAIL] Dropped.',0,1) WITH NOWAIT;
    end
    create table [UA3_PROVIDER].[COMMITS_EMAIL]
        (
            [TAG]                   varchar(200)      not null,
            [EMAIL_ADDR]            varchar(200)      not null
        )
    with(DATA_COMPRESSION = NONE)

    RAISERROR('Temp Table [COMMITS_EMAIL] created.',0,1) WITH NOWAIT;

    RAISERROR('Starting population of temp table: [COMMITS_EMAIL].',0,1) WITH NOWAIT;

    select @startDate = sysdatetime();
    set @rowcount = 0;
    set @errors = 0;

    -- If you are running a small test of data then uncomment out the join to this query.
    -- Otherwise if you want to see all potential email address in the commits table leave alone and execute.
    declare c_commits_reval cursor for
        select cast([PAYLOAD] as varchar(max))
        from [UA3_PROVIDER].[COMMITS] a
           --join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] b on a.[CHECKPOINTNUMBER] = b.[CHECKPOINTNUMBER]
        where [BUCKETID] in ('ProviderAggregate','ServiceLocationAggregate')
        order by a.[CHECKPOINTNUMBER] asc
    for read only;

    open c_commits_reval;

    fetch next from c_commits_reval into @payload_clob;
    while @@FETCH_STATUS = 0  
    begin   
        set @start = charindex('@', @payload_clob, 0);

        while @start > 0
        begin
            -- THIS CODE WILL ONLY WORK IF THE TARGET VALUE IS A STRING VALUE STARTING AND ENDING WITH DOUBLE QUOTES "
            set @end = charindex('"', @payload_clob, @start + 1);
            set @i = 1;
            while @end - @i > 0
            begin
                if substring(@payload_clob, @end - @i, 1) = '"'
                begin
                    set @start = @end - @i;
                    break;
                end;

                set @i = @i + 1;
            end;

            set @email = substring(@payload_clob, @start + 1, @end - @start - 1);

            set @tagend = @start - 2;

            set @i = 1;
            while @tagend - @i > 0
            begin
                if substring(@payload_clob, @tagend - @i, 1) = '"'
                begin
                    set @tagstart = @tagend - @i;
                    break;
                end;

                set @i = @i + 1;
            end;

            set @tag = substring(@payload_clob, @tagstart + 1, @tagend - @tagstart - 1);

            if (len(rtrim(ltrim(@email))) > 0)
            begin
                insert into [UA3_PROVIDER].[COMMITS_EMAIL] (TAG, EMAIL_ADDR) VALUES (@tag, @email);
                
                set @rowcount = @rowcount + @@ROWCOUNT;
            end;

            set @start = charindex('@', @payload_clob, @end + 1);
        end;

        fetch next from c_commits_reval into @payload_clob;
    end;
    
    close c_commits_reval  ;
    deallocate c_commits_reval  ;

    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0)) ;
    set @message = 'Finished update of temp table [COMMITS_EMAIL]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;
    
    create nonclustered index [COMMITS_EMAIL_IX0] on [UA3_PROVIDER].[COMMITS_EMAIL] ([EMAIL_ADDR], [TAG])
    with(SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = ON) on [PRIMARY];

    RAISERROR('Index [COMMITS_EMAIL_IX0] created for temp table [COMMITS_EMAIL].',0,1) WITH NOWAIT;
end;
GO

select distinct [TAG], [EMAIL_ADDR] from [UA3_PROVIDER].[COMMITS_EMAIL]
union
select distinct 'BASE_EFT_ENROLLMENT.PRVDR_AGENT_CNTCT_EMAIL_ADR', [PRVDR_AGENT_CNTCT_EMAIL_ADR] 
from [UA3_PROVIDER].[BASE_EFT_ENROLLMENT] e
    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on e.[PROVIDER_ID] = a.[STREAMIDORIGINAL]
union
select distinct 'BASE_EFT_ENROLLMENT.PRVDR_CNTCT_EMAIL_ADR', [PRVDR_CNTCT_EMAIL_ADR] 
from [UA3_PROVIDER].[BASE_EFT_ENROLLMENT] e
    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on e.[PROVIDER_ID] = a.[STREAMIDORIGINAL]
union
select distinct 'EFT_ENROLLMENT.PRVDR_AGENT_CNTCT_EMAIL_ADR', [PRVDR_AGENT_CNTCT_EMAIL_ADR]  
from [UA3_PROVIDER].[EFT_ENROLLMENT] e
    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PROVIDER_SERVICE_LOCATION_ID] = e.[PROVIDER_SERVICE_LOCATION_ID]
    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
union
select distinct 'EFT_ENROLLMENT.PRVDR_CNTCT_EMAIL_ADR', [PRVDR_CNTCT_EMAIL_ADR] 
from [UA3_PROVIDER].[EFT_ENROLLMENT] e
    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PROVIDER_SERVICE_LOCATION_ID] = e.[PROVIDER_SERVICE_LOCATION_ID]
    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
union
select distinct 'ERA_ENROLLMENT.CLRGHSE_CNTCT_EMAIL', [CLRGHSE_CNTCT_EMAIL] 
from [UA3_PROVIDER].[ERA_ENROLLMENT] e
    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PROVIDER_SERVICE_LOCATION_ID] = e.[PROVIDER_SERVICE_LOCATION_ID]
    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
union
select distinct 'ERA_ENROLLMENT.PRVDR_AGENT_CNTCT_EMAIL_ADR', [PRVDR_AGENT_CNTCT_EMAIL_ADR] 
from [UA3_PROVIDER].[ERA_ENROLLMENT] e
    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PROVIDER_SERVICE_LOCATION_ID] = e.[PROVIDER_SERVICE_LOCATION_ID]
    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
union
select distinct 'ERA_ENROLLMENT.PRVDR_CNTCT_EMAIL_ADR', [PRVDR_CNTCT_EMAIL_ADR] 
from [UA3_PROVIDER].[ERA_ENROLLMENT] e
    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PROVIDER_SERVICE_LOCATION_ID] = e.[PROVIDER_SERVICE_LOCATION_ID]
    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
union
select distinct 'ERA_ENROLLMENT.VNDR_CNTCT_EMAIL', [VNDR_CNTCT_EMAIL] 
from [UA3_PROVIDER].[ERA_ENROLLMENT] e
    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PROVIDER_SERVICE_LOCATION_ID] = e.[PROVIDER_SERVICE_LOCATION_ID]
    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
union
select distinct 'PRACTICE_LCTN_ADR_ASSCN.EMAIL_ADR', [EMAIL_ADR] 
from [UA3_PROVIDER].[PRACTICE_LCTN_ADR_ASSCN] e
    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PRACTICE_LOCATION_ID] = e.[PRACTICE_LOCATION_ID]
    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
union
select distinct 'PROVIDER_OWNER_ASSCN.EMAIL_ADR', [EMAIL_ADR] 
from [UA3_PROVIDER].[PROVIDER_OWNER_ASSCN] e
    join [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] s on s.[PROVIDER_SERVICE_LOCATION_ID] = e.[PROVIDER_SERVICE_LOCATION_ID]
    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on s.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL]
union
select distinct 'PROVIDER_SERVICE_LOCATION.EMAIL_ADR', [EMAIL_ADDRESS] 
from [UA3_PROVIDER].[PROVIDER_SERVICE_LOCATION] e
    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a on e.[PROVIDER_SERVICE_LOCATION_ID] = a.[STREAMIDORIGINAL];
GO