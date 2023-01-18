/****** Object:  Table [Person].[ContactType]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[ContactType]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[ContactType](
	[ContactTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ContactType_ContactTypeID] PRIMARY KEY CLUSTERED 
(
	[ContactTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_ContactType_Name]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[ContactType]') AND name = N'AK_ContactType_Name')
CREATE UNIQUE NONCLUSTERED INDEX [AK_ContactType_Name] ON [Person].[ContactType]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_ContactType_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[ContactType] ADD  CONSTRAINT [DF_ContactType_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'ContactType', N'COLUMN',N'ContactTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for ContactType records.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'ContactType', @level2type=N'COLUMN',@level2name=N'ContactTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'ContactType', N'COLUMN',N'Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact type description.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'ContactType', @level2type=N'COLUMN',@level2name=N'Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'ContactType', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'ContactType', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'ContactType', N'CONSTRAINT',N'DF_ContactType_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'ContactType', @level2type=N'CONSTRAINT',@level2name=N'DF_ContactType_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'ContactType', N'CONSTRAINT',N'PK_ContactType_ContactTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'ContactType', @level2type=N'CONSTRAINT',@level2name=N'PK_ContactType_ContactTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'ContactType', N'INDEX',N'AK_ContactType_Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'ContactType', @level2type=N'INDEX',@level2name=N'AK_ContactType_Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'ContactType', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table containing the types of business entity contacts.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'ContactType'
GO
