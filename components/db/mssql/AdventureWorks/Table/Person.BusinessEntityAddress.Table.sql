/****** Object:  Table [Person].[BusinessEntityAddress]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[BusinessEntityAddress](
	[BusinessEntityID] [int] NOT NULL,
	[AddressID] [int] NOT NULL,
	[AddressTypeID] [int] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BusinessEntityAddress_BusinessEntityID_AddressID_AddressTypeID] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC,
	[AddressID] ASC,
	[AddressTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [AK_BusinessEntityAddress_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]') AND name = N'AK_BusinessEntityAddress_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_BusinessEntityAddress_rowguid] ON [Person].[BusinessEntityAddress]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusinessEntityAddress_AddressID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]') AND name = N'IX_BusinessEntityAddress_AddressID')
CREATE NONCLUSTERED INDEX [IX_BusinessEntityAddress_AddressID] ON [Person].[BusinessEntityAddress]
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusinessEntityAddress_AddressTypeID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]') AND name = N'IX_BusinessEntityAddress_AddressTypeID')
CREATE NONCLUSTERED INDEX [IX_BusinessEntityAddress_AddressTypeID] ON [Person].[BusinessEntityAddress]
(
	[AddressTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_BusinessEntityAddress_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[BusinessEntityAddress] ADD  CONSTRAINT [DF_BusinessEntityAddress_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_BusinessEntityAddress_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[BusinessEntityAddress] ADD  CONSTRAINT [DF_BusinessEntityAddress_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_Address_AddressID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityAddress_Address_AddressID] FOREIGN KEY([AddressID])
REFERENCES [Person].[Address] ([AddressID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_Address_AddressID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress] CHECK CONSTRAINT [FK_BusinessEntityAddress_Address_AddressID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_AddressType_AddressTypeID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityAddress_AddressType_AddressTypeID] FOREIGN KEY([AddressTypeID])
REFERENCES [Person].[AddressType] ([AddressTypeID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_AddressType_AddressTypeID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress] CHECK CONSTRAINT [FK_BusinessEntityAddress_AddressType_AddressTypeID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Person].[BusinessEntity] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Person].[BusinessEntityAddress]'))
ALTER TABLE [Person].[BusinessEntityAddress] CHECK CONSTRAINT [FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to BusinessEntity.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'COLUMN',N'AddressID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to Address.AddressID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'COLUMN',@level2name=N'AddressID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'COLUMN',N'AddressTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to AddressType.AddressTypeID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'COLUMN',@level2name=N'AddressTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'CONSTRAINT',N'DF_BusinessEntityAddress_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'CONSTRAINT',@level2name=N'DF_BusinessEntityAddress_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'CONSTRAINT',N'DF_BusinessEntityAddress_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'CONSTRAINT',@level2name=N'DF_BusinessEntityAddress_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'CONSTRAINT',N'PK_BusinessEntityAddress_BusinessEntityID_AddressID_AddressTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'CONSTRAINT',@level2name=N'PK_BusinessEntityAddress_BusinessEntityID_AddressID_AddressTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'INDEX',N'AK_BusinessEntityAddress_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'INDEX',@level2name=N'AK_BusinessEntityAddress_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'INDEX',N'IX_BusinessEntityAddress_AddressID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'INDEX',@level2name=N'IX_BusinessEntityAddress_AddressID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'INDEX',N'IX_BusinessEntityAddress_AddressTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'INDEX',@level2name=N'IX_BusinessEntityAddress_AddressTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cross-reference table mapping customers, vendors, and employees to their addresses.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'CONSTRAINT',N'FK_BusinessEntityAddress_Address_AddressID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Address.AddressID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'CONSTRAINT',@level2name=N'FK_BusinessEntityAddress_Address_AddressID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'CONSTRAINT',N'FK_BusinessEntityAddress_AddressType_AddressTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing AddressType.AddressTypeID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'CONSTRAINT',@level2name=N'FK_BusinessEntityAddress_AddressType_AddressTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'BusinessEntityAddress', N'CONSTRAINT',N'FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing BusinessEntity.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'BusinessEntityAddress', @level2type=N'CONSTRAINT',@level2name=N'FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID'
GO
