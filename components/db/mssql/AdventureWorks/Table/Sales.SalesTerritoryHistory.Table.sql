/****** Object:  Table [Sales].[SalesTerritoryHistory]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[SalesTerritoryHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[SalesTerritoryHistory](
	[BusinessEntityID] [int] NOT NULL,
	[TerritoryID] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesTerritoryHistory_BusinessEntityID_StartDate_TerritoryID] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC,
	[StartDate] ASC,
	[TerritoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [AK_SalesTerritoryHistory_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SalesTerritoryHistory]') AND name = N'AK_SalesTerritoryHistory_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_SalesTerritoryHistory_rowguid] ON [Sales].[SalesTerritoryHistory]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesTerritoryHistory_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesTerritoryHistory] ADD  CONSTRAINT [DF_SalesTerritoryHistory_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesTerritoryHistory_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesTerritoryHistory] ADD  CONSTRAINT [DF_SalesTerritoryHistory_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritoryHistory]'))
ALTER TABLE [Sales].[SalesTerritoryHistory]  WITH CHECK ADD  CONSTRAINT [FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Sales].[SalesPerson] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritoryHistory]'))
ALTER TABLE [Sales].[SalesTerritoryHistory] CHECK CONSTRAINT [FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesTerritoryHistory_SalesTerritory_TerritoryID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritoryHistory]'))
ALTER TABLE [Sales].[SalesTerritoryHistory]  WITH CHECK ADD  CONSTRAINT [FK_SalesTerritoryHistory_SalesTerritory_TerritoryID] FOREIGN KEY([TerritoryID])
REFERENCES [Sales].[SalesTerritory] ([TerritoryID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesTerritoryHistory_SalesTerritory_TerritoryID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritoryHistory]'))
ALTER TABLE [Sales].[SalesTerritoryHistory] CHECK CONSTRAINT [FK_SalesTerritoryHistory_SalesTerritory_TerritoryID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTerritoryHistory_EndDate]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritoryHistory]'))
ALTER TABLE [Sales].[SalesTerritoryHistory]  WITH CHECK ADD  CONSTRAINT [CK_SalesTerritoryHistory_EndDate] CHECK  (([EndDate]>=[StartDate] OR [EndDate] IS NULL))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTerritoryHistory_EndDate]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritoryHistory]'))
ALTER TABLE [Sales].[SalesTerritoryHistory] CHECK CONSTRAINT [CK_SalesTerritoryHistory_EndDate]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. The sales rep.  Foreign key to SalesPerson.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'COLUMN',N'TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Territory identification number. Foreign key to SalesTerritory.SalesTerritoryID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'COLUMN',@level2name=N'TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'COLUMN',N'StartDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Date the sales representive started work in the territory.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'COLUMN',@level2name=N'StartDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'COLUMN',N'EndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date the sales representative left work in the territory.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'COLUMN',@level2name=N'EndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'CONSTRAINT',N'DF_SalesTerritoryHistory_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesTerritoryHistory_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'CONSTRAINT',N'DF_SalesTerritoryHistory_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesTerritoryHistory_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'CONSTRAINT',N'PK_SalesTerritoryHistory_BusinessEntityID_StartDate_TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'CONSTRAINT',@level2name=N'PK_SalesTerritoryHistory_BusinessEntityID_StartDate_TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'INDEX',N'AK_SalesTerritoryHistory_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'INDEX',@level2name=N'AK_SalesTerritoryHistory_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales representative transfers to other sales territories.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'CONSTRAINT',N'FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SalesPerson.SalesPersonID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'CONSTRAINT',N'FK_SalesTerritoryHistory_SalesTerritory_TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SalesTerritory.TerritoryID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesTerritoryHistory_SalesTerritory_TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritoryHistory', N'CONSTRAINT',N'CK_SalesTerritoryHistory_EndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [EndDate] >= [StartDate] OR [EndDate] IS NULL' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritoryHistory', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesTerritoryHistory_EndDate'
GO
