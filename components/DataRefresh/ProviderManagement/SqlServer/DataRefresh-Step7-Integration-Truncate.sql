----THIS IS THE NO TURNING BACK POINT. AFTER THIS RUNS ALL THE VIEW PROJECTION RECORDS DATA WILL BE CHANGED FOREVER EXCEPT FROM A DATABASE BACKUP.

----UNCOMMENT THE TRUNCATE AND UPDATE STATEMENTS BELOW ONCE YOU HAVE VERIFIED THIS IS THE CORRECT DATABASE SCHEMA. THE SCRIPTS ARE COMMENTED OUT TO PREVENT ACCIDENTAL RUNNING IN THE WRONG SCHEMA.

set nocount on;
GO

begin
    declare @overallStartDate datetime, 
            @startDate datetime, 
            @endDate datetime,
            @rowcount int,
            @time time(0),
            @RC int,  
            @message varchar(2000); 
            
    select @overallStartDate = sysdatetime(), @startDate = sysdatetime();

    select @startDate = sysdatetime();
    set @rowcount = 0;
    RAISERROR('Starting truncate of non event sourced integration batch extract tables that need to be cleared out for UAT.',0,1) WITH NOWAIT;

    --truncate table [UA3_INTEGRATION].[PRN_EXTRACT];
    set @message = '    Finished truncating table [PRN_EXTRACT]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    --truncate table [UA3_INTEGRATION].[PRN_SEGMENT];
    set @message = '    Finished truncating table [PRN_SEGMENT]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    --truncate table [UA3_INTEGRATION].[AGGREGATOR_EXTRACT_DATA];
    set @message = '    Finished truncating table [AGGREGATOR_EXTRACT_DATA]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    -- Update all records to be not extracted.
    --update [UA3_INTEGRATION].[AGGREGATOR_EXTRACT]
    --    set [LAST_EXTRACT_TS] = null;
    --set @message = '    Finished updating table [AGGREGATOR_EXTRACT]. Records: ' + cast(@rowcount as varchar(50));
    --RAISERROR(@message,0,1) WITH NOWAIT;

    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0)) ;

    set @message = 'Finished truncating tables ' + cast(@time as varchar(50)) + ' duration.';
    RAISERROR(@message,0,1) WITH NOWAIT;

    set @time = cast((@endDate-@overallStartDate) as time(0)) ;
    set @message = 'Overall Time: ' + cast(@time as varchar(50)) + ' duration. ';  
    RAISERROR(@message,0,1) WITH NOWAIT;
end;
GO