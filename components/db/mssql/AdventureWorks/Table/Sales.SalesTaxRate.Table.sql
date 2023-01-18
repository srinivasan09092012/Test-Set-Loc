/****** Object:  Table [Sales].[SalesTaxRate]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[SalesTaxRate]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[SalesTaxRate](
	[SalesTaxRateID] [int] IDENTITY(1,1) NOT NULL,
	[StateProvinceID] [int] NOT NULL,
	[TaxType] [tinyint] NOT NULL,
	[TaxRate] [smallmoney] NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesTaxRate_SalesTaxRateID] PRIMARY KEY CLUSTERED 
(
	[SalesTaxRateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [AK_SalesTaxRate_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SalesTaxRate]') AND name = N'AK_SalesTaxRate_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_SalesTaxRate_rowguid] ON [Sales].[SalesTaxRate]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AK_SalesTaxRate_StateProvinceID_TaxType]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SalesTaxRate]') AND name = N'AK_SalesTaxRate_StateProvinceID_TaxType')
CREATE UNIQUE NONCLUSTERED INDEX [AK_SalesTaxRate_StateProvinceID_TaxType] ON [Sales].[SalesTaxRate]
(
	[StateProvinceID] ASC,
	[TaxType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesTaxRate_TaxRate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesTaxRate] ADD  CONSTRAINT [DF_SalesTaxRate_TaxRate]  DEFAULT ((0.00)) FOR [TaxRate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesTaxRate_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesTaxRate] ADD  CONSTRAINT [DF_SalesTaxRate_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesTaxRate_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesTaxRate] ADD  CONSTRAINT [DF_SalesTaxRate_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesTaxRate_StateProvince_StateProvinceID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTaxRate]'))
ALTER TABLE [Sales].[SalesTaxRate]  WITH CHECK ADD  CONSTRAINT [FK_SalesTaxRate_StateProvince_StateProvinceID] FOREIGN KEY([StateProvinceID])
REFERENCES [Person].[StateProvince] ([StateProvinceID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesTaxRate_StateProvince_StateProvinceID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTaxRate]'))
ALTER TABLE [Sales].[SalesTaxRate] CHECK CONSTRAINT [FK_SalesTaxRate_StateProvince_StateProvinceID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTaxRate_TaxType]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTaxRate]'))
ALTER TABLE [Sales].[SalesTaxRate]  WITH CHECK ADD  CONSTRAINT [CK_SalesTaxRate_TaxType] CHECK  (([TaxType]>=(1) AND [TaxType]<=(3)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesTaxRate_TaxType]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesTaxRate]'))
ALTER TABLE [Sales].[SalesTaxRate] CHECK CONSTRAINT [CK_SalesTaxRate_TaxType]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'COLUMN',N'SalesTaxRateID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for SalesTaxRate records.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'COLUMN',@level2name=N'SalesTaxRateID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'COLUMN',N'StateProvinceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'State, province, or country/region the sales tax applies to.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'COLUMN',@level2name=N'StateProvinceID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'COLUMN',N'TaxType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Tax applied to retail transactions, 2 = Tax applied to wholesale transactions, 3 = Tax applied to all sales (retail and wholesale) transactions.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'COLUMN',@level2name=N'TaxType'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'COLUMN',N'TaxRate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tax rate amount.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'COLUMN',@level2name=N'TaxRate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'CONSTRAINT',N'DF_SalesTaxRate_TaxRate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesTaxRate_TaxRate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'COLUMN',N'Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tax rate description.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'COLUMN',@level2name=N'Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'CONSTRAINT',N'DF_SalesTaxRate_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesTaxRate_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'CONSTRAINT',N'DF_SalesTaxRate_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesTaxRate_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'CONSTRAINT',N'PK_SalesTaxRate_SalesTaxRateID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'CONSTRAINT',@level2name=N'PK_SalesTaxRate_SalesTaxRateID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'INDEX',N'AK_SalesTaxRate_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'INDEX',@level2name=N'AK_SalesTaxRate_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'INDEX',N'AK_SalesTaxRate_StateProvinceID_TaxType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'INDEX',@level2name=N'AK_SalesTaxRate_StateProvinceID_TaxType'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tax rate lookup table.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'CONSTRAINT',N'FK_SalesTaxRate_StateProvince_StateProvinceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing StateProvince.StateProvinceID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesTaxRate_StateProvince_StateProvinceID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesTaxRate', N'CONSTRAINT',N'CK_SalesTaxRate_TaxType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [TaxType] BETWEEN (1) AND (3)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesTaxRate', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesTaxRate_TaxType'
GO
