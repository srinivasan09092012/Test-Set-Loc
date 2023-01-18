/****** Object:  Table [Sales].[CurrencyRate]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[CurrencyRate]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[CurrencyRate](
	[CurrencyRateID] [int] IDENTITY(1,1) NOT NULL,
	[CurrencyRateDate] [datetime] NOT NULL,
	[FromCurrencyCode] [nchar](3) NOT NULL,
	[ToCurrencyCode] [nchar](3) NOT NULL,
	[AverageRate] [money] NOT NULL,
	[EndOfDayRate] [money] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CurrencyRate_CurrencyRateID] PRIMARY KEY CLUSTERED 
(
	[CurrencyRateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[CurrencyRate]') AND name = N'AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode')
CREATE UNIQUE NONCLUSTERED INDEX [AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode] ON [Sales].[CurrencyRate]
(
	[CurrencyRateDate] ASC,
	[FromCurrencyCode] ASC,
	[ToCurrencyCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_CurrencyRate_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[CurrencyRate] ADD  CONSTRAINT [DF_CurrencyRate_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_CurrencyRate_Currency_FromCurrencyCode]') AND parent_object_id = OBJECT_ID(N'[Sales].[CurrencyRate]'))
ALTER TABLE [Sales].[CurrencyRate]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyRate_Currency_FromCurrencyCode] FOREIGN KEY([FromCurrencyCode])
REFERENCES [Sales].[Currency] ([CurrencyCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_CurrencyRate_Currency_FromCurrencyCode]') AND parent_object_id = OBJECT_ID(N'[Sales].[CurrencyRate]'))
ALTER TABLE [Sales].[CurrencyRate] CHECK CONSTRAINT [FK_CurrencyRate_Currency_FromCurrencyCode]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_CurrencyRate_Currency_ToCurrencyCode]') AND parent_object_id = OBJECT_ID(N'[Sales].[CurrencyRate]'))
ALTER TABLE [Sales].[CurrencyRate]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyRate_Currency_ToCurrencyCode] FOREIGN KEY([ToCurrencyCode])
REFERENCES [Sales].[Currency] ([CurrencyCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_CurrencyRate_Currency_ToCurrencyCode]') AND parent_object_id = OBJECT_ID(N'[Sales].[CurrencyRate]'))
ALTER TABLE [Sales].[CurrencyRate] CHECK CONSTRAINT [FK_CurrencyRate_Currency_ToCurrencyCode]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'COLUMN',N'CurrencyRateID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for CurrencyRate records.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'COLUMN',@level2name=N'CurrencyRateID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'COLUMN',N'CurrencyRateDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the exchange rate was obtained.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'COLUMN',@level2name=N'CurrencyRateDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'COLUMN',N'FromCurrencyCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Exchange rate was converted from this currency code.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'COLUMN',@level2name=N'FromCurrencyCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'COLUMN',N'ToCurrencyCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Exchange rate was converted to this currency code.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'COLUMN',@level2name=N'ToCurrencyCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'COLUMN',N'AverageRate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Average exchange rate for the day.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'COLUMN',@level2name=N'AverageRate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'COLUMN',N'EndOfDayRate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Final exchange rate for the day.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'COLUMN',@level2name=N'EndOfDayRate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'CONSTRAINT',N'DF_CurrencyRate_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'CONSTRAINT',@level2name=N'DF_CurrencyRate_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'CONSTRAINT',N'PK_CurrencyRate_CurrencyRateID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'CONSTRAINT',@level2name=N'PK_CurrencyRate_CurrencyRateID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'INDEX',N'AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'INDEX',@level2name=N'AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Currency exchange rates.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'CONSTRAINT',N'FK_CurrencyRate_Currency_FromCurrencyCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Currency.FromCurrencyCode.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'CONSTRAINT',@level2name=N'FK_CurrencyRate_Currency_FromCurrencyCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CurrencyRate', N'CONSTRAINT',N'FK_CurrencyRate_Currency_ToCurrencyCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Currency.ToCurrencyCode.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CurrencyRate', @level2type=N'CONSTRAINT',@level2name=N'FK_CurrencyRate_Currency_ToCurrencyCode'
GO
