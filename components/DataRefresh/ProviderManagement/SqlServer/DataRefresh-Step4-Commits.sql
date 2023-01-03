----THIS IS THE NO TURNING BACK POINT. AFTER THIS RUNS ALL COMMIT RECORD DATA WILL BE CHANGED FOREVER EXCEPT FROM A DATABASE BACKUP.

----UNCOMMENT THE UPDATE STATEMENTS BELOW ONCE YOU HAVE VERIFIED THIS IS THE CORRECT DATABASE SCHEMA.

set nocount on;
GO

begin
    declare @startDate datetime, 
            @endDate datetime,
            @rowcount int,
            @time time(0),
            @message varchar(2000); 

    RAISERROR('Starting update of commits table: [COMMITS].',0,1) WITH NOWAIT;

    select @startDate = sysdatetime();
    set @rowcount = 0;

    ----UNCOMMENT THE UPDATE STATEMENT BELOW WHEN YOU ARE READY TO RUN THE CHANGES. THERE IS NO GOING BACK AT THIS POINT OTHER THAN A DB BACKUP.

    --update [UA3_PROVIDER].[COMMITS]
    --    set [PAYLOAD] = cast(b.[PAYLOAD_CLOB] as varbinary(max))
    --from [UA3_PROVIDER].[COMMITS] a
    --    join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] b on a.[CHECKPOINTNUMBER] = b.[CHECKPOINTNUMBER]
    --where b.[RECORD_STATUS] = 1;

    select @rowcount = @@ROWCOUNT
    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0));
    set @message = 'Finished updating [COMMITS] table. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;
    
    select @startDate = sysdatetime();
    set @rowcount = 0;

    ---- UPDATE ALL RECORDS TO BE RECORD STATUS 2 TO ALLOW FOR THE LAST STEP TO FILTER ON THOSE RECORDS.

    --update [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE]
    --    set [RECORD_STATUS] = 2
    --from [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] a
    --    join [UA3_PROVIDER].[COMMITS] b on a.[CHECKPOINTNUMBER] = b.[CHECKPOINTNUMBER]
    --where a.[RECORD_STATUS] = 1;

    select @rowcount = @@ROWCOUNT
    select @endDate = sysdatetime();
    set @time = cast((@enddate-@startDate) as time(0));    
    set @message = 'Finished updating [COMMITS_DEID_AGGREGATE] table from status 1 to 2. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;
end;
GO

----VALIDATE THE DATA CHANGED ON RADOM RECORDS. CHECKING THE MORE COMPLEX RECORDS.
----THIS SHOULD NOT FIND ANYTHING DIFFERENT THAN THE PRE RUN SCRIPT BUT NOW BOTH COLUMNS ARE THE SAME SO YOU ARE JUST LOOKING AT THE PAYLOAD COLUMN TO VERIFY THE SCRIPT IS OK.
----SIMPLY RUN THE COMMENTED OUT QUERY AND RANDOMLY SELECT SOME ROWS TO VERIFY THE JSON LOOKS VALID EVERYWHERE.

--select top 10 a.[CHECKPOINTNUMBER], cast(a.payload as varchar(max)) as payload, b.payload_clob, (DATALENGTH(cast(a.payload as varchar(max)))-DATALENGTH(REPLACE(cast(a.payload as varchar(max)),'@','')))/NULLIF(DATALENGTH('@'), 0) as emailCount
--from [UA3_PROVIDER].[COMMITS] a
--     join [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE] b on a.[CHECKPOINTNUMBER] = b.[CHECKPOINTNUMBER]
--where (DATALENGTH(cast(a.payload as varchar(max)))-DATALENGTH(REPLACE(cast(a.payload as varchar(max)),'Email','')))/NULLIF(DATALENGTH('Email'), 0) > 3;