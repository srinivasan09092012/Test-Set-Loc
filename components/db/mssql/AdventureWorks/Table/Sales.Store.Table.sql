/****** Object:  Table [Sales].[Store]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[Store]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[Store](
	[BusinessEntityID] [int] NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[SalesPersonID] [int] NULL,
	[Demographics] [xml](CONTENT [Sales].[StoreSurveySchemaCollection]) NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Store_BusinessEntityID] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Index [AK_Store_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[Store]') AND name = N'AK_Store_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Store_rowguid] ON [Sales].[Store]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Store_SalesPersonID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[Store]') AND name = N'IX_Store_SalesPersonID')
CREATE NONCLUSTERED INDEX [IX_Store_SalesPersonID] ON [Sales].[Store]
(
	[SalesPersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PXML_Store_Demographics]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[Store]') AND name = N'PXML_Store_Demographics')
CREATE PRIMARY XML INDEX [PXML_Store_Demographics] ON [Sales].[Store]
(
	[Demographics]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_Store_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[Store] ADD  CONSTRAINT [DF_Store_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_Store_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[Store] ADD  CONSTRAINT [DF_Store_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_Store_BusinessEntity_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Sales].[Store]'))
ALTER TABLE [Sales].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_BusinessEntity_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Person].[BusinessEntity] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_Store_BusinessEntity_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Sales].[Store]'))
ALTER TABLE [Sales].[Store] CHECK CONSTRAINT [FK_Store_BusinessEntity_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_Store_SalesPerson_SalesPersonID]') AND parent_object_id = OBJECT_ID(N'[Sales].[Store]'))
ALTER TABLE [Sales].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_SalesPerson_SalesPersonID] FOREIGN KEY([SalesPersonID])
REFERENCES [Sales].[SalesPerson] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_Store_SalesPerson_SalesPersonID]') AND parent_object_id = OBJECT_ID(N'[Sales].[Store]'))
ALTER TABLE [Sales].[Store] CHECK CONSTRAINT [FK_Store_SalesPerson_SalesPersonID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to Customer.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'COLUMN',N'Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name of the store.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'COLUMN',N'SalesPersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID of the sales person assigned to the customer. Foreign key to SalesPerson.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'SalesPersonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'COLUMN',N'Demographics'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Demographic informationg about the store such as the number of employees, annual sales and store type.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Demographics'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'CONSTRAINT',N'DF_Store_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'CONSTRAINT',@level2name=N'DF_Store_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'CONSTRAINT',N'DF_Store_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'CONSTRAINT',@level2name=N'DF_Store_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'CONSTRAINT',N'PK_Store_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'CONSTRAINT',@level2name=N'PK_Store_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'INDEX',N'AK_Store_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'INDEX',@level2name=N'AK_Store_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'INDEX',N'IX_Store_SalesPersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'INDEX',@level2name=N'IX_Store_SalesPersonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'INDEX',N'PXML_Store_Demographics'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary XML index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'INDEX',@level2name=N'PXML_Store_Demographics'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customers (resellers) of Adventure Works products.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'CONSTRAINT',N'FK_Store_BusinessEntity_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing BusinessEntity.BusinessEntityID' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'CONSTRAINT',@level2name=N'FK_Store_BusinessEntity_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Store', N'CONSTRAINT',N'FK_Store_SalesPerson_SalesPersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SalesPerson.SalesPersonID' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'CONSTRAINT',@level2name=N'FK_Store_SalesPerson_SalesPersonID'
GO
