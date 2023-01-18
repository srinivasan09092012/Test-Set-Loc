/****** Object:  Table [Sales].[SalesTerritory]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[SalesTerritory]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[SalesTerritory](
	[TerritoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[CountryRegionCode] [nvarchar](3) NOT NULL,
	[Group] [nvarchar](50) NOT NULL,
	[SalesYTD] [money] NOT NULL,
	[SalesLastYear] [money] NOT NULL,
	[CostYTD] [money] NOT NULL,
	[CostLastYear] [money] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesTerritory_TerritoryID] PRIMARY KEY CLUSTERED 
(
	[TerritoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_SalesTerritory_Name]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SalesTerritory]') AND name = N'AK_SalesTerritory_Name')
CREATE UNIQUE NONCLUSTERED INDEX [AK_SalesTerritory_Name] ON [Sales].[SalesTerritory]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AK_SalesTerritory_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SalesTerritory]') AND name = N'AK_SalesTerritory_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_SalesTerritory_rowguid] ON [Sales].[SalesTerritory]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesTerritory_SalesYTD]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesTerritory] ADD  CONSTRAINT [DF_SalesTerritory_SalesYTD]  DEFAULT ((0.00)) FOR [SalesYTD]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesTerritory_SalesLastYear]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesTerritory] ADD  CONSTRAINT [DF_SalesTerritory_SalesLastYear]  DEFAULT ((0.00)) FOR [SalesLastYear]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesTerritory_CostYTD]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesTerritory] ADD  CONSTRAINT [DF_SalesTerritory_CostYTD]  DEFAULT ((0.00)) FOR [CostYTD]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesTerritory_CostLastYear]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesTerritory] ADD  CONSTRAINT [DF_SalesTerritory_CostLastYear]  DEFAULT ((0.00)) FOR [CostLastYear]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesTerritory_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesTerritory] ADD  CONSTRAINT [DF_SalesTerritory_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesTerritory_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesTerritory] ADD  CONSTRAINT [DF_SalesTerritory_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesTerritory_CountryRegion_CountryRegionCode]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritory]'))
ALTER TABLE [Sales].[SalesTerritory]  WITH CHECK ADD  CONSTRAINT [FK_SalesTerritory_CountryRegion_CountryRegionCode] FOREIGN KEY([CountryRegionCode])
REFERENCES [Person].[CountryRegion] ([CountryRegionCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesTerritory_CountryRegion_CountryRegionCode]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritory]'))
ALTER TABLE [Sales].[SalesTerritory] CHECK CONSTRAINT [FK_SalesTerritory_CountryRegion_CountryRegionCode]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTerritory_CostLastYear]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritory]'))
ALTER TABLE [Sales].[SalesTerritory]  WITH CHECK ADD  CONSTRAINT [CK_SalesTerritory_CostLastYear] CHECK  (([CostLastYear]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTerritory_CostLastYear]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritory]'))
ALTER TABLE [Sales].[SalesTerritory] CHECK CONSTRAINT [CK_SalesTerritory_CostLastYear]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTerritory_CostYTD]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritory]'))
ALTER TABLE [Sales].[SalesTerritory]  WITH CHECK ADD  CONSTRAINT [CK_SalesTerritory_CostYTD] CHECK  (([CostYTD]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTerritory_CostYTD]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritory]'))
ALTER TABLE [Sales].[SalesTerritory] CHECK CONSTRAINT [CK_SalesTerritory_CostYTD]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTerritory_SalesLastYear]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritory]'))
ALTER TABLE [Sales].[SalesTerritory]  WITH CHECK ADD  CONSTRAINT [CK_SalesTerritory_SalesLastYear] CHECK  (([SalesLastYear]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTerritory_SalesLastYear]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritory]'))
ALTER TABLE [Sales].[SalesTerritory] CHECK CONSTRAINT [CK_SalesTerritory_SalesLastYear]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTerritory_SalesYTD]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritory]'))
ALTER TABLE [Sales].[SalesTerritory]  WITH CHECK ADD  CONSTRAINT [CK_SalesTerritory_SalesYTD] CHECK  (([SalesYTD]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTerritory_SalesYTD]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTerritory]'))
ALTER TABLE [Sales].[SalesTerritory] CHECK CONSTRAINT [CK_SalesTerritory_SalesYTD]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'COLUMN',N'TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for SalesTerritory records.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'COLUMN',@level2name=N'TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'COLUMN',N'Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales territory description' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'COLUMN',@level2name=N'Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'COLUMN',N'CountryRegionCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. ' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'COLUMN',@level2name=N'CountryRegionCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'COLUMN',N'Group'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Geographic area to which the sales territory belong.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'COLUMN',@level2name=N'Group'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'COLUMN',N'SalesYTD'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales in the territory year to date.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'COLUMN',@level2name=N'SalesYTD'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'DF_SalesTerritory_SalesYTD'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesTerritory_SalesYTD'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'COLUMN',N'SalesLastYear'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales in the territory the previous year.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'COLUMN',@level2name=N'SalesLastYear'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'DF_SalesTerritory_SalesLastYear'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesTerritory_SalesLastYear'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'COLUMN',N'CostYTD'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Business costs in the territory year to date.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'COLUMN',@level2name=N'CostYTD'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'DF_SalesTerritory_CostYTD'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesTerritory_CostYTD'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'COLUMN',N'CostLastYear'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Business costs in the territory the previous year.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'COLUMN',@level2name=N'CostLastYear'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'DF_SalesTerritory_CostLastYear'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesTerritory_CostLastYear'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'DF_SalesTerritory_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesTerritory_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'DF_SalesTerritory_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesTerritory_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'PK_SalesTerritory_TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'PK_SalesTerritory_TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'INDEX',N'AK_SalesTerritory_Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'INDEX',@level2name=N'AK_SalesTerritory_Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'INDEX',N'AK_SalesTerritory_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'INDEX',@level2name=N'AK_SalesTerritory_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales territory lookup table.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'FK_SalesTerritory_CountryRegion_CountryRegionCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing CountryRegion.CountryRegionCode.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesTerritory_CountryRegion_CountryRegionCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'CK_SalesTerritory_CostLastYear'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [CostLastYear] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesTerritory_CostLastYear'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'CK_SalesTerritory_CostYTD'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [CostYTD] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesTerritory_CostYTD'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'CK_SalesTerritory_SalesLastYear'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SalesLastYear] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesTerritory_SalesLastYear'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTerritory', N'CONSTRAINT',N'CK_SalesTerritory_SalesYTD'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SalesYTD] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTerritory', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesTerritory_SalesYTD'
GO
