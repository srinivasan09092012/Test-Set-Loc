/****** Object:  Table [dbo].[AWBuildVersion]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AWBuildVersion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AWBuildVersion](
	[SystemInformationID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Database Version] [nvarchar](25) NOT NULL,
	[VersionDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AWBuildVersion_SystemInformationID] PRIMARY KEY CLUSTERED 
(
	[SystemInformationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_AWBuildVersion_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AWBuildVersion] ADD  CONSTRAINT [DF_AWBuildVersion_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'AWBuildVersion', N'COLUMN',N'SystemInformationID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for AWBuildVersion records.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AWBuildVersion', @level2type=N'COLUMN',@level2name=N'SystemInformationID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'AWBuildVersion', N'COLUMN',N'Database Version'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Version number of the database in 9.yy.mm.dd.00 format.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AWBuildVersion', @level2type=N'COLUMN',@level2name=N'Database Version'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'AWBuildVersion', N'COLUMN',N'VersionDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AWBuildVersion', @level2type=N'COLUMN',@level2name=N'VersionDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'AWBuildVersion', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AWBuildVersion', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'AWBuildVersion', N'CONSTRAINT',N'DF_AWBuildVersion_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AWBuildVersion', @level2type=N'CONSTRAINT',@level2name=N'DF_AWBuildVersion_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'AWBuildVersion', N'CONSTRAINT',N'PK_AWBuildVersion_SystemInformationID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AWBuildVersion', @level2type=N'CONSTRAINT',@level2name=N'PK_AWBuildVersion_SystemInformationID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'AWBuildVersion', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Current version number of the AdventureWorks 2016 sample database. ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AWBuildVersion'
GO
