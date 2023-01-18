----THIS IS THE NO TURNING BACK POINT. AFTER THIS RUNS ALL THE VIEW PROJECTION RECORDS DATA WILL BE CHANGED FOREVER EXCEPT FROM A DATABASE BACKUP.

----UNCOMMENT THE TRUNCATE AND DELETE STATEMENTS BELOW ONCE YOU HAVE VERIFIED THIS IS THE CORRECT DATABASE SCHEMA. THE SCRIPTS ARE COMMENTED OUT TO PREVENT ACCIDENTAL RUNNING IN THE WRONG SCHEMA.

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
    RAISERROR('Starting truncate of non event sourced view projection tables not needed for UAT data.',0,1) WITH NOWAIT;

    --truncate table [UA3_PROVIDER].[EVENT_AUDIT];
    set @message = '    Finished truncating table [EVENT_AUDIT]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    --truncate table [UA3_PROVIDER].[EVENT_DISTRIBUTION];
    set @message = '    Finished truncating table [EVENT_DISTRIBUTION]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    --truncate table [UA3_PROVIDER].[PROVIDER_REPORT];
    set @message = '    Finished truncating table [PROVIDER_REPORT]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    --truncate table [UA3_PROVIDER].[SNAPSHOTS];
    set @message = '    Finished truncating table [SNAPSHOTS]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    --truncate table [UA3_PROVIDER].[SCREENING_REPORT];
    set @message = '    Finished truncating table [SCREENING_REPORT]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    --truncate table [UA3_PROVIDER].[WF_TRANSACTION];
    set @message = '    Finished truncating table [WF_TRANSACTION]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    --truncate table [UA3_PROVIDER].[BATCH_ERROR_MSG];
    set @message = '    Finished truncating table [BATCH_ERROR_MSG]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;
    
    --delete from [UA3_PROVIDER].[BATCH_ERROR_RECORD];
    set @message = '    Finished deleting all rows from table [BATCH_ERROR_RECORD]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    --truncate table [UA3_PROVIDER].[EP_TRANSACTION_ATTACHMENT];
    set @message = '    Finished truncating table [EP_TRANSACTION_ATTACHMENT]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    --delete from [UA3_PROVIDER].[EP_TRANSACTION];
    set @message = '    Finished deleting all rows from table [EP_TRANSACTION]. Records: ' + cast(@rowcount as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;

    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0)) ;

    set @message = 'Finished truncating tables ' + cast(@time as varchar(50)) + ' duration.';
    RAISERROR(@message,0,1) WITH NOWAIT;

    set @time = cast((@endDate-@overallStartDate) as time(0)) ;
    set @message = 'Overall Time: ' + cast(@time as varchar(50)) + ' duration. ';  
    RAISERROR(@message,0,1) WITH NOWAIT;
end;
GO