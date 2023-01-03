----VALIDATE THAT NO RECORDS FAILED TO BE RECOGNIZED AS VALID JSON. IF THIS QUERY RETURNS ANYTHING OTHER THAN ONE ROW WITH ALL RECORD_STATUS = 1 WE NEED TO INVESTIGATE.

--select [RECORD_STATUS], count(*)
--from [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE]
--group by [RECORD_STATUS];

----OPTIONALLY VALIDATE SOME OF THE RECORDS PRIOR BY COMPARING THE TWO COLUMNS ON RANDOM RECORDS. 
----SIMPLY RUN THE COMMENTED OUT QUERY AND RANDOMLY SELECT SOME ROWS TO COMPARE THE BEFORE AND AFTER TEXT.
----IF NO RECORDS ARE FOUND CHANGE THE > 3 TO SOMETHING LOWER. IT IS TRYING TO FIND EVENTS WITH MORE THAN ONE EMAIL RECORD FOR COMPLEX CHANGES.

--select top 10 a.[CHECKPOINTNUMBER], cast(a.payload as varchar(max)) as payload, b.payload_clob, (DATALENGTH(cast(a.payload as varchar(max)))-DATALENGTH(REPLACE(cast(a.payload as varchar(max)),'@','')))/NULLIF(DATALENGTH('@'), 0) as emailCount
--from [UA3_PROVIDER].[COMMITS] a
--     join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] b on a.[CHECKPOINTNUMBER] = b.[CHECKPOINTNUMBER]
--where (DATALENGTH(cast(a.payload as varchar(max)))-DATALENGTH(REPLACE(cast(a.payload as varchar(max)),'Email','')))/NULLIF(DATALENGTH('Email'), 0) > 3;

---- THIS STEP IS OPTIONAL. AS WRITTEN IT WILL LOOK THROUGH ALL RECORDS IN THE TEMPORARY COMMITS_DEID_AGGREGATE TABLE AND SCAN FOR ANY CHARACTERS OF @.
---- AS THIS IS THE REQUIRED VALUE IN AN EMAIL FOR IT TO WORK AT ALL WE WILL RENDER ALL NAME AND VALUE PAIRS ASSOCIATED TO THE JSON ELEMENT THAT CONTAINST AN @ IN THE VALUE.
---- BY LOOKING AT THE RESULTING RECORDS YOU CAN VERIFY WHAT WOULD HAPPEN AND MAKE SURE NO MISSING EMAIL ADDRESSES EXIST.
---- DURING TESTING OF A SMALL NUMBER OF RECORDS THIS SHOULD BE RUN EVERY TIME. 
---- BUT DURING THE LIVE RUN OF THE PROCESS IT IS OPTIONAL AS IT WILL TAKE SOME TIME TO PARSE AND CALCULATE ALL RECORDS VALUES.

set nocount on;
GO

begin
    declare @startDate datetime, 
            @endDate datetime,
            @rowcount int,
            @errors int,
            @time time(0),  
            @tagstart int, 
            @tagend int,
            @start int,
            @end int,
            @payload_clob varchar(max),
            @message varchar(2000),
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

    declare c_commits_reval cursor for
        select [PAYLOAD_CLOB]
        from [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE]
        order by [CHECKPOINTNUMBER] asc
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

    select @rowcount = @@ROWCOUNT;
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));

    set @message = 'Finished finding tags in [COMMITS_DEID_AGGREGATE]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;
end;
GO

select distinct [TAG], [EMAIL_ADDR]
from [UA3_PROVIDER].[COMMITS_EMAIL];

GO