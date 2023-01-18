if exists (select * from sysobjects where name='COMMITS_DEID_AGGREGATE' and xtype='U')
begin
    drop table [UA3_PROVIDER].[COMMITS_DEID_AGGREGATE];
    print 'Temp Table [COMMITS_DEID_AGGREGATE] Dropped.';
end;
GO

if exists (select * from sysobjects where name='COMMITS_DEID_PATTERN' and xtype='U')
begin
    drop table [UA3_PROVIDER].[COMMITS_DEID_PATTERN];
    print 'Temp Table [COMMITS_DEID_PATTERN] Dropped.';
end;
GO

if exists (select * from sysobjects where name='COMMITS_EMAIL' and xtype='U')
begin
    drop table [UA3_PROVIDER].[COMMITS_EMAIL];
    print 'Temp Table [COMMITS_EMAIL] Dropped.';
end;
GO