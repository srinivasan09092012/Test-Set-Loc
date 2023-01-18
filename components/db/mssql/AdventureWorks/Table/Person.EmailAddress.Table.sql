/****** Object:  Table [Person].[EmailAddress]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[EmailAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[EmailAddress](
	[BusinessEntityID] [int] NOT NULL,
	[EmailAddressID] [int] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_EmailAddress_BusinessEntityID_EmailAddressID] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC,
	[EmailAddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_EmailAddress_EmailAddress]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[EmailAddress]') AND name = N'IX_EmailAddress_EmailAddress')
CREATE NONCLUSTERED INDEX [IX_EmailAddress_EmailAddress] ON [Person].[EmailAddress]
(
	[EmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_EmailAddress_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[EmailAddress] ADD  CONSTRAINT [DF_EmailAddress_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_EmailAddress_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[EmailAddress] ADD  CONSTRAINT [DF_EmailAddress_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_EmailAddress_Person_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Person].[EmailAddress]'))
ALTER TABLE [Person].[EmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_EmailAddress_Person_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Person].[Person] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_EmailAddress_Person_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Person].[EmailAddress]'))
ALTER TABLE [Person].[EmailAddress] CHECK CONSTRAINT [FK_EmailAddress_Person_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'EmailAddress', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Person associated with this email address.  Foreign key to Person.BusinessEntityID' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'EmailAddress', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'EmailAddress', N'COLUMN',N'EmailAddressID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. ID of this email address.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'EmailAddress', @level2type=N'COLUMN',@level2name=N'EmailAddressID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'EmailAddress', N'COLUMN',N'EmailAddress'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'E-mail address for the person.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'EmailAddress', @level2type=N'COLUMN',@level2name=N'EmailAddress'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'EmailAddress', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'EmailAddress', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'EmailAddress', N'CONSTRAINT',N'DF_EmailAddress_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'EmailAddress', @level2type=N'CONSTRAINT',@level2name=N'DF_EmailAddress_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'EmailAddress', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'EmailAddress', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'EmailAddress', N'CONSTRAINT',N'DF_EmailAddress_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'EmailAddress', @level2type=N'CONSTRAINT',@level2name=N'DF_EmailAddress_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'EmailAddress', N'CONSTRAINT',N'PK_EmailAddress_BusinessEntityID_EmailAddressID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'EmailAddress', @level2type=N'CONSTRAINT',@level2name=N'PK_EmailAddress_BusinessEntityID_EmailAddressID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'EmailAddress', N'INDEX',N'IX_EmailAddress_EmailAddress'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'EmailAddress', @level2type=N'INDEX',@level2name=N'IX_EmailAddress_EmailAddress'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'EmailAddress', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Where to send a person email.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'EmailAddress'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'EmailAddress', N'CONSTRAINT',N'FK_EmailAddress_Person_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Person.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'EmailAddress', @level2type=N'CONSTRAINT',@level2name=N'FK_EmailAddress_Person_BusinessEntityID'
GO
