/****** Object:  Table [Sales].[SpecialOffer]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[SpecialOffer]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[SpecialOffer](
	[SpecialOfferID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[DiscountPct] [smallmoney] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[MinQty] [int] NOT NULL,
	[MaxQty] [int] NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SpecialOffer_SpecialOfferID] PRIMARY KEY CLUSTERED 
(
	[SpecialOfferID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [AK_SpecialOffer_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SpecialOffer]') AND name = N'AK_SpecialOffer_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_SpecialOffer_rowguid] ON [Sales].[SpecialOffer]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SpecialOffer_DiscountPct]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SpecialOffer] ADD  CONSTRAINT [DF_SpecialOffer_DiscountPct]  DEFAULT ((0.00)) FOR [DiscountPct]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SpecialOffer_MinQty]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SpecialOffer] ADD  CONSTRAINT [DF_SpecialOffer_MinQty]  DEFAULT ((0)) FOR [MinQty]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SpecialOffer_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SpecialOffer] ADD  CONSTRAINT [DF_SpecialOffer_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SpecialOffer_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SpecialOffer] ADD  CONSTRAINT [DF_SpecialOffer_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SpecialOffer_DiscountPct]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOffer]'))
ALTER TABLE [Sales].[SpecialOffer]  WITH CHECK ADD  CONSTRAINT [CK_SpecialOffer_DiscountPct] CHECK  (([DiscountPct]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SpecialOffer_DiscountPct]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOffer]'))
ALTER TABLE [Sales].[SpecialOffer] CHECK CONSTRAINT [CK_SpecialOffer_DiscountPct]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SpecialOffer_EndDate]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOffer]'))
ALTER TABLE [Sales].[SpecialOffer]  WITH CHECK ADD  CONSTRAINT [CK_SpecialOffer_EndDate] CHECK  (([EndDate]>=[StartDate]))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SpecialOffer_EndDate]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOffer]'))
ALTER TABLE [Sales].[SpecialOffer] CHECK CONSTRAINT [CK_SpecialOffer_EndDate]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SpecialOffer_MaxQty]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOffer]'))
ALTER TABLE [Sales].[SpecialOffer]  WITH CHECK ADD  CONSTRAINT [CK_SpecialOffer_MaxQty] CHECK  (([MaxQty]>=(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SpecialOffer_MaxQty]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOffer]'))
ALTER TABLE [Sales].[SpecialOffer] CHECK CONSTRAINT [CK_SpecialOffer_MaxQty]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SpecialOffer_MinQty]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOffer]'))
ALTER TABLE [Sales].[SpecialOffer]  WITH CHECK ADD  CONSTRAINT [CK_SpecialOffer_MinQty] CHECK  (([MinQty]>=(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SpecialOffer_MinQty]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOffer]'))
ALTER TABLE [Sales].[SpecialOffer] CHECK CONSTRAINT [CK_SpecialOffer_MinQty]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'COLUMN',N'SpecialOfferID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for SpecialOffer records.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'COLUMN',@level2name=N'SpecialOfferID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'COLUMN',N'Description'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Discount description.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'COLUMN',@level2name=N'Description'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'COLUMN',N'DiscountPct'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Discount precentage.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'COLUMN',@level2name=N'DiscountPct'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'CONSTRAINT',N'DF_SpecialOffer_DiscountPct'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'CONSTRAINT',@level2name=N'DF_SpecialOffer_DiscountPct'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'COLUMN',N'Type'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Discount type category.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'COLUMN',@level2name=N'Type'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'COLUMN',N'Category'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Group the discount applies to such as Reseller or Customer.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'COLUMN',@level2name=N'Category'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'COLUMN',N'StartDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Discount start date.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'COLUMN',@level2name=N'StartDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'COLUMN',N'EndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Discount end date.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'COLUMN',@level2name=N'EndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'COLUMN',N'MinQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Minimum discount percent allowed.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'COLUMN',@level2name=N'MinQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'CONSTRAINT',N'DF_SpecialOffer_MinQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'CONSTRAINT',@level2name=N'DF_SpecialOffer_MinQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'COLUMN',N'MaxQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Maximum discount percent allowed.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'COLUMN',@level2name=N'MaxQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'CONSTRAINT',N'DF_SpecialOffer_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'CONSTRAINT',@level2name=N'DF_SpecialOffer_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'CONSTRAINT',N'DF_SpecialOffer_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'CONSTRAINT',@level2name=N'DF_SpecialOffer_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'CONSTRAINT',N'PK_SpecialOffer_SpecialOfferID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'CONSTRAINT',@level2name=N'PK_SpecialOffer_SpecialOfferID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'INDEX',N'AK_SpecialOffer_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'INDEX',@level2name=N'AK_SpecialOffer_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sale discounts lookup table.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'CONSTRAINT',N'CK_SpecialOffer_DiscountPct'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [DiscountPct] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'CONSTRAINT',@level2name=N'CK_SpecialOffer_DiscountPct'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'CONSTRAINT',N'CK_SpecialOffer_EndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [EndDate] >= [StartDate]' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'CONSTRAINT',@level2name=N'CK_SpecialOffer_EndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'CONSTRAINT',N'CK_SpecialOffer_MaxQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [MaxQty] >= (0)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'CONSTRAINT',@level2name=N'CK_SpecialOffer_MaxQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOffer', N'CONSTRAINT',N'CK_SpecialOffer_MinQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [MinQty] >= (0)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOffer', @level2type=N'CONSTRAINT',@level2name=N'CK_SpecialOffer_MinQty'
GO
