/****** Object:  Table [Sales].[CreditCard]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[CreditCard]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[CreditCard](
	[CreditCardID] [int] IDENTITY(1,1) NOT NULL,
	[CardType] [nvarchar](50) NOT NULL,
	[CardNumber] [nvarchar](25) NOT NULL,
	[ExpMonth] [tinyint] NOT NULL,
	[ExpYear] [smallint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CreditCard_CreditCardID] PRIMARY KEY CLUSTERED 
(
	[CreditCardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_CreditCard_CardNumber]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[CreditCard]') AND name = N'AK_CreditCard_CardNumber')
CREATE UNIQUE NONCLUSTERED INDEX [AK_CreditCard_CardNumber] ON [Sales].[CreditCard]
(
	[CardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_CreditCard_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[CreditCard] ADD  CONSTRAINT [DF_CreditCard_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CreditCard', N'COLUMN',N'CreditCardID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for CreditCard records.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CreditCard', @level2type=N'COLUMN',@level2name=N'CreditCardID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CreditCard', N'COLUMN',N'CardType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Credit card name.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CreditCard', @level2type=N'COLUMN',@level2name=N'CardType'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CreditCard', N'COLUMN',N'CardNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Credit card number.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CreditCard', @level2type=N'COLUMN',@level2name=N'CardNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CreditCard', N'COLUMN',N'ExpMonth'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Credit card expiration month.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CreditCard', @level2type=N'COLUMN',@level2name=N'ExpMonth'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CreditCard', N'COLUMN',N'ExpYear'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Credit card expiration year.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CreditCard', @level2type=N'COLUMN',@level2name=N'ExpYear'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CreditCard', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CreditCard', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CreditCard', N'CONSTRAINT',N'DF_CreditCard_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CreditCard', @level2type=N'CONSTRAINT',@level2name=N'DF_CreditCard_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CreditCard', N'CONSTRAINT',N'PK_CreditCard_CreditCardID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CreditCard', @level2type=N'CONSTRAINT',@level2name=N'PK_CreditCard_CreditCardID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CreditCard', N'INDEX',N'AK_CreditCard_CardNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CreditCard', @level2type=N'INDEX',@level2name=N'AK_CreditCard_CardNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'CreditCard', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer credit card information.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'CreditCard'
GO
