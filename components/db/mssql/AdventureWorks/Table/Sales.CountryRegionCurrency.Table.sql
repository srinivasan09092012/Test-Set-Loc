/****** Object:  Table [Sales].[CountryRegionCurrency]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[CountryRegionCurrency]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[CountryRegionCurrency](
	[CountryRegionCode] [nvarchar](3) NOT NULL,
	[CurrencyCode] [nchar](3) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CountryRegionCurrency_CountryRegionCode_CurrencyCode] PRIMARY KEY CLUSTERED 
(
	[CountryRegionCode] ASC,
	[CurrencyCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CountryRegionCurrency_CurrencyCode]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[CountryRegionCurrency]') AND name = N'IX_CountryRegionCurrency_CurrencyCode')
CREATE NONCLUSTERED INDEX [IX_CountryRegionCurrency_CurrencyCode] ON [Sales].[CountryRegionCurrency]
(
	[CurrencyCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_CountryRegionCurrency_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[CountryRegionCurrency] ADD  CONSTRAINT [DF_CountryRegionCurrency_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_CountryRegionCurrency_CountryRegion_CountryRegionCode]') AND parent_object_id = OBJECT_ID(N'[Sales].[CountryRegionCurrency]'))
ALTER TABLE [Sales].[CountryRegionCurrency]  WITH CHECK ADD  CONSTRAINT [FK_CountryRegionCurrency_CountryRegion_CountryRegionCode] FOREIGN KEY([CountryRegionCode])
REFERENCES [Person].[CountryRegion] ([CountryRegionCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_CountryRegionCurrency_CountryRegion_CountryRegionCode]') AND parent_object_id = OBJECT_ID(N'[Sales].[CountryRegionCurrency]'))
ALTER TABLE [Sales].[CountryRegionCurrency] CHECK CONSTRAINT [FK_CountryRegionCurrency_CountryRegion_CountryRegionCode]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_CountryRegionCurrency_Currency_CurrencyCode]') AND parent_object_id = OBJECT_ID(N'[Sales].[CountryRegionCurrency]'))
ALTER TABLE [Sales].[CountryRegionCurrency]  WITH CHECK ADD  CONSTRAINT [FK_CountryRegionCurrency_Currency_CurrencyCode] FOREIGN KEY([CurrencyCode])
REFERENCES [Sales].[Currency] ([CurrencyCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_CountryRegionCurrency_Currency_CurrencyCode]') AND parent_object_id = OBJECT_ID(N'[Sales].[CountryRegionCurrency]'))
ALTER TABLE [Sales].[CountryRegionCurrency] CHECK CONSTRAINT [FK_CountryRegionCurrency_Currency_CurrencyCode]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CountryRegionCurrency', N'COLUMN',N'CountryRegionCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ISO code for countries and regions. Foreign key to CountryRegion.CountryRegionCode.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CountryRegionCurrency', @level2type=N'COLUMN',@level2name=N'CountryRegionCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CountryRegionCurrency', N'COLUMN',N'CurrencyCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ISO standard currency code. Foreign key to Currency.CurrencyCode.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CountryRegionCurrency', @level2type=N'COLUMN',@level2name=N'CurrencyCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CountryRegionCurrency', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CountryRegionCurrency', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CountryRegionCurrency', N'CONSTRAINT',N'DF_CountryRegionCurrency_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CountryRegionCurrency', @level2type=N'CONSTRAINT',@level2name=N'DF_CountryRegionCurrency_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CountryRegionCurrency', N'CONSTRAINT',N'PK_CountryRegionCurrency_CountryRegionCode_CurrencyCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CountryRegionCurrency', @level2type=N'CONSTRAINT',@level2name=N'PK_CountryRegionCurrency_CountryRegionCode_CurrencyCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CountryRegionCurrency', N'INDEX',N'IX_CountryRegionCurrency_CurrencyCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CountryRegionCurrency', @level2type=N'INDEX',@level2name=N'IX_CountryRegionCurrency_CurrencyCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CountryRegionCurrency', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cross-reference table mapping ISO currency codes to a country or region.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CountryRegionCurrency'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CountryRegionCurrency', N'CONSTRAINT',N'FK_CountryRegionCurrency_CountryRegion_CountryRegionCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing CountryRegion.CountryRegionCode.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CountryRegionCurrency', @level2type=N'CONSTRAINT',@level2name=N'FK_CountryRegionCurrency_CountryRegion_CountryRegionCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CountryRegionCurrency', N'CONSTRAINT',N'FK_CountryRegionCurrency_Currency_CurrencyCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Currency.CurrencyCode.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CountryRegionCurrency', @level2type=N'CONSTRAINT',@level2name=N'FK_CountryRegionCurrency_Currency_CurrencyCode'
GO
