/****** Object:  Table [Person].[StateProvince]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[StateProvince]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[StateProvince](
	[StateProvinceID] [int] IDENTITY(1,1) NOT NULL,
	[StateProvinceCode] [nchar](3) NOT NULL,
	[CountryRegionCode] [nvarchar](3) NOT NULL,
	[IsOnlyStateProvinceFlag] [dbo].[Flag] NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[TerritoryID] [int] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StateProvince_StateProvinceID] PRIMARY KEY CLUSTERED 
(
	[StateProvinceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_StateProvince_Name]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[StateProvince]') AND name = N'AK_StateProvince_Name')
CREATE UNIQUE NONCLUSTERED INDEX [AK_StateProvince_Name] ON [Person].[StateProvince]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AK_StateProvince_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[StateProvince]') AND name = N'AK_StateProvince_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_StateProvince_rowguid] ON [Person].[StateProvince]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_StateProvince_StateProvinceCode_CountryRegionCode]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[StateProvince]') AND name = N'AK_StateProvince_StateProvinceCode_CountryRegionCode')
CREATE UNIQUE NONCLUSTERED INDEX [AK_StateProvince_StateProvinceCode_CountryRegionCode] ON [Person].[StateProvince]
(
	[StateProvinceCode] ASC,
	[CountryRegionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_StateProvince_IsOnlyStateProvinceFlag]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[StateProvince] ADD  CONSTRAINT [DF_StateProvince_IsOnlyStateProvinceFlag]  DEFAULT ((1)) FOR [IsOnlyStateProvinceFlag]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_StateProvince_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[StateProvince] ADD  CONSTRAINT [DF_StateProvince_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_StateProvince_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[StateProvince] ADD  CONSTRAINT [DF_StateProvince_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_StateProvince_CountryRegion_CountryRegionCode]') AND parent_object_id = OBJECT_ID(N'[Person].[StateProvince]'))
ALTER TABLE [Person].[StateProvince]  WITH CHECK ADD  CONSTRAINT [FK_StateProvince_CountryRegion_CountryRegionCode] FOREIGN KEY([CountryRegionCode])
REFERENCES [Person].[CountryRegion] ([CountryRegionCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_StateProvince_CountryRegion_CountryRegionCode]') AND parent_object_id = OBJECT_ID(N'[Person].[StateProvince]'))
ALTER TABLE [Person].[StateProvince] CHECK CONSTRAINT [FK_StateProvince_CountryRegion_CountryRegionCode]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_StateProvince_SalesTerritory_TerritoryID]') AND parent_object_id = OBJECT_ID(N'[Person].[StateProvince]'))
ALTER TABLE [Person].[StateProvince]  WITH CHECK ADD  CONSTRAINT [FK_StateProvince_SalesTerritory_TerritoryID] FOREIGN KEY([TerritoryID])
REFERENCES [Sales].[SalesTerritory] ([TerritoryID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_StateProvince_SalesTerritory_TerritoryID]') AND parent_object_id = OBJECT_ID(N'[Person].[StateProvince]'))
ALTER TABLE [Person].[StateProvince] CHECK CONSTRAINT [FK_StateProvince_SalesTerritory_TerritoryID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'COLUMN',N'StateProvinceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for StateProvince records.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'COLUMN',@level2name=N'StateProvinceID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'COLUMN',N'StateProvinceCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ISO standard state or province code.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'COLUMN',@level2name=N'StateProvinceCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'COLUMN',N'CountryRegionCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. ' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'COLUMN',@level2name=N'CountryRegionCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'COLUMN',N'IsOnlyStateProvinceFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = StateProvinceCode exists. 1 = StateProvinceCode unavailable, using CountryRegionCode.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'COLUMN',@level2name=N'IsOnlyStateProvinceFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'CONSTRAINT',N'DF_StateProvince_IsOnlyStateProvinceFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 1 (TRUE)' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'CONSTRAINT',@level2name=N'DF_StateProvince_IsOnlyStateProvinceFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'COLUMN',N'Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'State or province description.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'COLUMN',@level2name=N'Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'COLUMN',N'TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID of the territory in which the state or province is located. Foreign key to SalesTerritory.SalesTerritoryID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'COLUMN',@level2name=N'TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'CONSTRAINT',N'DF_StateProvince_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'CONSTRAINT',@level2name=N'DF_StateProvince_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'CONSTRAINT',N'DF_StateProvince_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'CONSTRAINT',@level2name=N'DF_StateProvince_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'CONSTRAINT',N'PK_StateProvince_StateProvinceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'CONSTRAINT',@level2name=N'PK_StateProvince_StateProvinceID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'INDEX',N'AK_StateProvince_Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'INDEX',@level2name=N'AK_StateProvince_Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'INDEX',N'AK_StateProvince_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'INDEX',@level2name=N'AK_StateProvince_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'INDEX',N'AK_StateProvince_StateProvinceCode_CountryRegionCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'INDEX',@level2name=N'AK_StateProvince_StateProvinceCode_CountryRegionCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'State and province lookup table.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'CONSTRAINT',N'FK_StateProvince_CountryRegion_CountryRegionCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing CountryRegion.CountryRegionCode.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'CONSTRAINT',@level2name=N'FK_StateProvince_CountryRegion_CountryRegionCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'StateProvince', N'CONSTRAINT',N'FK_StateProvince_SalesTerritory_TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SalesTerritory.TerritoryID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'StateProvince', @level2type=N'CONSTRAINT',@level2name=N'FK_StateProvince_SalesTerritory_TerritoryID'
GO
