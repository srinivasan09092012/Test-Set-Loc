/****** Object:  Table [Person].[BusinessEntityContact]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[BusinessEntityContact](
	[BusinessEntityID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
	[ContactTypeID] [int] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BusinessEntityContact_BusinessEntityID_PersonID_ContactTypeID] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC,
	[PersonID] ASC,
	[ContactTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [AK_BusinessEntityContact_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]') AND name = N'AK_BusinessEntityContact_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_BusinessEntityContact_rowguid] ON [Person].[BusinessEntityContact]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusinessEntityContact_ContactTypeID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]') AND name = N'IX_BusinessEntityContact_ContactTypeID')
CREATE NONCLUSTERED INDEX [IX_BusinessEntityContact_ContactTypeID] ON [Person].[BusinessEntityContact]
(
	[ContactTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusinessEntityContact_PersonID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]') AND name = N'IX_BusinessEntityContact_PersonID')
CREATE NONCLUSTERED INDEX [IX_BusinessEntityContact_PersonID] ON [Person].[BusinessEntityContact]
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_BusinessEntityContact_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[BusinessEntityContact] ADD  CONSTRAINT [DF_BusinessEntityContact_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_BusinessEntityContact_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[BusinessEntityContact] ADD  CONSTRAINT [DF_BusinessEntityContact_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_BusinessEntity_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityContact_BusinessEntity_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Person].[BusinessEntity] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_BusinessEntity_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact] CHECK CONSTRAINT [FK_BusinessEntityContact_BusinessEntity_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_ContactType_ContactTypeID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityContact_ContactType_ContactTypeID] FOREIGN KEY([ContactTypeID])
REFERENCES [Person].[ContactType] ([ContactTypeID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_ContactType_ContactTypeID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact] CHECK CONSTRAINT [FK_BusinessEntityContact_ContactType_ContactTypeID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_Person_PersonID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityContact_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [Person].[Person] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityContact_Person_PersonID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityContact]'))
ALTER TABLE [Person].[BusinessEntityContact] CHECK CONSTRAINT [FK_BusinessEntityContact_Person_PersonID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to BusinessEntity.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'COLUMN',N'PersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to Person.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'COLUMN',@level2name=N'PersonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'COLUMN',N'ContactTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key.  Foreign key to ContactType.ContactTypeID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'COLUMN',@level2name=N'ContactTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'CONSTRAINT',N'DF_BusinessEntityContact_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'CONSTRAINT',@level2name=N'DF_BusinessEntityContact_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'CONSTRAINT',N'DF_BusinessEntityContact_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'CONSTRAINT',@level2name=N'DF_BusinessEntityContact_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'CONSTRAINT',N'PK_BusinessEntityContact_BusinessEntityID_PersonID_ContactTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'CONSTRAINT',@level2name=N'PK_BusinessEntityContact_BusinessEntityID_PersonID_ContactTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'INDEX',N'AK_BusinessEntityContact_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'INDEX',@level2name=N'AK_BusinessEntityContact_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'INDEX',N'IX_BusinessEntityContact_ContactTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'INDEX',@level2name=N'IX_BusinessEntityContact_ContactTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'INDEX',N'IX_BusinessEntityContact_PersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'INDEX',@level2name=N'IX_BusinessEntityContact_PersonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cross-reference table mapping stores, vendors, and employees to people' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'CONSTRAINT',N'FK_BusinessEntityContact_BusinessEntity_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing BusinessEntity.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'CONSTRAINT',@level2name=N'FK_BusinessEntityContact_BusinessEntity_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'CONSTRAINT',N'FK_BusinessEntityContact_ContactType_ContactTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing ContactType.ContactTypeID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'CONSTRAINT',@level2name=N'FK_BusinessEntityContact_ContactType_ContactTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityContact', N'CONSTRAINT',N'FK_BusinessEntityContact_Person_PersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Person.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityContact', @level2type=N'CONSTRAINT',@level2name=N'FK_BusinessEntityContact_Person_PersonID'
GO
