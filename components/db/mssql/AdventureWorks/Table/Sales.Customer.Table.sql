/****** Object:  Table [Sales].[Customer]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[PersonID] [int] NULL,
	[StoreID] [int] NULL,
	[TerritoryID] [int] NULL,
	[AccountNumber]  AS (isnull('AW'+[dbo].[ufnLeadingZeros]([CustomerID]),'')),
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Customer_CustomerID] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [AK_Customer_AccountNumber]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[Customer]') AND name = N'AK_Customer_AccountNumber')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Customer_AccountNumber] ON [Sales].[Customer]
(
	[AccountNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AK_Customer_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[Customer]') AND name = N'AK_Customer_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Customer_rowguid] ON [Sales].[Customer]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Customer_TerritoryID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[Customer]') AND name = N'IX_Customer_TerritoryID')
CREATE NONCLUSTERED INDEX [IX_Customer_TerritoryID] ON [Sales].[Customer]
(
	[TerritoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_Customer_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[Customer] ADD  CONSTRAINT [DF_Customer_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_Customer_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[Customer] ADD  CONSTRAINT [DF_Customer_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_Customer_Person_PersonID]') AND parent_object_id = OBJECT_ID(N'[Sales].[Customer]'))
ALTER TABLE [Sales].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [Person].[Person] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_Customer_Person_PersonID]') AND parent_object_id = OBJECT_ID(N'[Sales].[Customer]'))
ALTER TABLE [Sales].[Customer] CHECK CONSTRAINT [FK_Customer_Person_PersonID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_Customer_SalesTerritory_TerritoryID]') AND parent_object_id = OBJECT_ID(N'[Sales].[Customer]'))
ALTER TABLE [Sales].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_SalesTerritory_TerritoryID] FOREIGN KEY([TerritoryID])
REFERENCES [Sales].[SalesTerritory] ([TerritoryID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_Customer_SalesTerritory_TerritoryID]') AND parent_object_id = OBJECT_ID(N'[Sales].[Customer]'))
ALTER TABLE [Sales].[Customer] CHECK CONSTRAINT [FK_Customer_SalesTerritory_TerritoryID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_Customer_Store_StoreID]') AND parent_object_id = OBJECT_ID(N'[Sales].[Customer]'))
ALTER TABLE [Sales].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Store_StoreID] FOREIGN KEY([StoreID])
REFERENCES [Sales].[Store] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_Customer_Store_StoreID]') AND parent_object_id = OBJECT_ID(N'[Sales].[Customer]'))
ALTER TABLE [Sales].[Customer] CHECK CONSTRAINT [FK_Customer_Store_StoreID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'COLUMN',N'CustomerID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'COLUMN',N'PersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key to Person.BusinessEntityID' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'PersonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'COLUMN',N'StoreID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key to Store.BusinessEntityID' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'StoreID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'COLUMN',N'TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'COLUMN',N'AccountNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique number identifying the customer assigned by the accounting system.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'AccountNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'CONSTRAINT',N'DF_Customer_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'CONSTRAINT',@level2name=N'DF_Customer_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'CONSTRAINT',N'DF_Customer_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'CONSTRAINT',@level2name=N'DF_Customer_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'CONSTRAINT',N'PK_Customer_CustomerID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'CONSTRAINT',@level2name=N'PK_Customer_CustomerID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'INDEX',N'AK_Customer_AccountNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'INDEX',@level2name=N'AK_Customer_AccountNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'INDEX',N'AK_Customer_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'INDEX',@level2name=N'AK_Customer_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'INDEX',N'IX_Customer_TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'INDEX',@level2name=N'IX_Customer_TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Current customer information. Also see the Person and Store tables.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'CONSTRAINT',N'FK_Customer_Person_PersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Person.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'CONSTRAINT',@level2name=N'FK_Customer_Person_PersonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'CONSTRAINT',N'FK_Customer_SalesTerritory_TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SalesTerritory.TerritoryID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'CONSTRAINT',@level2name=N'FK_Customer_SalesTerritory_TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'Customer', N'CONSTRAINT',N'FK_Customer_Store_StoreID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Store.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'CONSTRAINT',@level2name=N'FK_Customer_Store_StoreID'
GO
