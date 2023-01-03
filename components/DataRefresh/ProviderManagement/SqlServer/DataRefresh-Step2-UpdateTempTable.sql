---- NO CHANGES OR MODIFICATIONS NEEDED FOR THIS SCRIPT JUST EXECUTE IT AS IT ONLY UPDATES A TEMP TABLE.

set nocount on;
GO

begin
    declare @overallStartDate datetime, 
            @startDate datetime, 
            @endDate datetime,
            @rowcount int,
            @errors int,
            @time time(0),  
            @start int,
            @end int,
            @eventname varchar(200),
            @split varchar(200),
            @payload_clob varchar(max),
            @pattern varchar(200), 
            @replacement varchar(200),
            @message varchar(2000),
            @isJSON bit; 

    select @startDate = sysdatetime();

    RAISERROR('Starting update of temp table: [COMMITS_DEID_AGGREGATE].',0,1) WITH NOWAIT;

    select @startDate = sysdatetime();
    set @rowcount = 0;
    set @errors = 0;

    declare c_commits_reval cursor for
        select [PAYLOAD_CLOB]
        from [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE]
        where [RECORD_STATUS] = 0
        order by [CHECKPOINTNUMBER] asc
    for update of [PAYLOAD_CLOB], [RECORD_STATUS];

    open c_commits_reval;

    fetch next from c_commits_reval into @payload_clob;
    while @@FETCH_STATUS = 0  
    begin   
        declare c_pattern cursor for
            select [PATTERN],	
                   [REPLACEMENT_VALUE] 
            from [UA3_PROVIDER].[COMMITS_DEID_PATTERN]
        for read only;

        open c_pattern;

        fetch next from c_pattern into @pattern, @replacement;
        while @@FETCH_STATUS = 0  
        begin 
            set @start = charindex(@pattern, @payload_clob, 0);

            while @start > 0
            begin
                -- THIS CODE WILL ONLY WORK IF THE TARGET VALUE IS A STRING VALUE STARTING AND ENDING WITH DOUBLE QUOTES "
                set @end = charindex('"', @payload_clob, @start + len(@pattern));

                set @payload_clob = stuff(@payload_clob, @start, @end - @start, @pattern + @replacement);

                set @start = charindex(@pattern, @payload_clob, @end + 1);
            end;

            fetch next from c_pattern into @pattern, @replacement;
        end;

        close c_pattern;
        deallocate c_pattern;

        -- CHECK IF THE RESULTANT VALUE IS VALID JSON. WE HAVE TO REMOVE THE BINARY TAG AND $ CHARACTERS TO PERFORM THE JSON CHECK.
        set @isJSON = isjson(replace(replace(@payload_clob, '$', ''), 'ï»¿', ''));
        if (@isJSON = 1)
        begin
            update [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE]
               set [PAYLOAD_CLOB] = @payload_clob,
                   [RECORD_STATUS] = 1
            where current of c_commits_reval;

            set @rowcount = @rowcount + 1;
        end
        else
        begin
            update [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE]
               set [PAYLOAD_CLOB] = @payload_clob,
                   [RECORD_STATUS] = 4
            where current of c_commits_reval;
            
            set @errors = @errors + 1;
        end;

        fetch next from c_commits_reval into @payload_clob;
    end;
    
    close c_commits_reval  ;
    deallocate c_commits_reval  ;

    select @endDate = sysdatetime();
    set @time = cast((@endDate-@startDate) as time(0)) ;
    set @message = 'Finished update of temp table [COMMITS_DEID_AGGREGATE]. Duration: ' + cast(@time as varchar(50)) + ' Records: ' + cast(@rowcount as varchar(50)) + ' Errors: ' + cast(@errors as varchar(50));
    RAISERROR(@message,0,1) WITH NOWAIT;
end;
GO