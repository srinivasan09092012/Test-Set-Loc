/****** Object:  Table [Sales].[SpecialOfferProduct]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[SpecialOfferProduct]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[SpecialOfferProduct](
	[SpecialOfferID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SpecialOfferProduct_SpecialOfferID_ProductID] PRIMARY KEY CLUSTERED 
(
	[SpecialOfferID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [AK_SpecialOfferProduct_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SpecialOfferProduct]') AND name = N'AK_SpecialOfferProduct_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_SpecialOfferProduct_rowguid] ON [Sales].[SpecialOfferProduct]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SpecialOfferProduct_ProductID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SpecialOfferProduct]') AND name = N'IX_SpecialOfferProduct_ProductID')
CREATE NONCLUSTERED INDEX [IX_SpecialOfferProduct_ProductID] ON [Sales].[SpecialOfferProduct]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SpecialOfferProduct_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SpecialOfferProduct] ADD  CONSTRAINT [DF_SpecialOfferProduct_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SpecialOfferProduct_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SpecialOfferProduct] ADD  CONSTRAINT [DF_SpecialOfferProduct_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SpecialOfferProduct_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOfferProduct]'))
ALTER TABLE [Sales].[SpecialOfferProduct]  WITH CHECK ADD  CONSTRAINT [FK_SpecialOfferProduct_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [Production].[Product] ([ProductID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SpecialOfferProduct_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOfferProduct]'))
ALTER TABLE [Sales].[SpecialOfferProduct] CHECK CONSTRAINT [FK_SpecialOfferProduct_Product_ProductID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SpecialOfferProduct_SpecialOffer_SpecialOfferID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOfferProduct]'))
ALTER TABLE [Sales].[SpecialOfferProduct]  WITH CHECK ADD  CONSTRAINT [FK_SpecialOfferProduct_SpecialOffer_SpecialOfferID] FOREIGN KEY([SpecialOfferID])
REFERENCES [Sales].[SpecialOffer] ([SpecialOfferID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SpecialOfferProduct_SpecialOffer_SpecialOfferID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SpecialOfferProduct]'))
ALTER TABLE [Sales].[SpecialOfferProduct] CHECK CONSTRAINT [FK_SpecialOfferProduct_SpecialOffer_SpecialOfferID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', N'COLUMN',N'SpecialOfferID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for SpecialOfferProduct records.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct', @level2type=N'COLUMN',@level2name=N'SpecialOfferID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', N'COLUMN',N'ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product identification number. Foreign key to Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', N'CONSTRAINT',N'DF_SpecialOfferProduct_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct', @level2type=N'CONSTRAINT',@level2name=N'DF_SpecialOfferProduct_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', N'CONSTRAINT',N'DF_SpecialOfferProduct_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct', @level2type=N'CONSTRAINT',@level2name=N'DF_SpecialOfferProduct_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', N'CONSTRAINT',N'PK_SpecialOfferProduct_SpecialOfferID_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct', @level2type=N'CONSTRAINT',@level2name=N'PK_SpecialOfferProduct_SpecialOfferID_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', N'INDEX',N'AK_SpecialOfferProduct_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct', @level2type=N'INDEX',@level2name=N'AK_SpecialOfferProduct_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', N'INDEX',N'IX_SpecialOfferProduct_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct', @level2type=N'INDEX',@level2name=N'IX_SpecialOfferProduct_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cross-reference table mapping products to special offer discounts.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', N'CONSTRAINT',N'FK_SpecialOfferProduct_Product_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct', @level2type=N'CONSTRAINT',@level2name=N'FK_SpecialOfferProduct_Product_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SpecialOfferProduct', N'CONSTRAINT',N'FK_SpecialOfferProduct_SpecialOffer_SpecialOfferID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SpecialOffer.SpecialOfferID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SpecialOfferProduct', @level2type=N'CONSTRAINT',@level2name=N'FK_SpecialOfferProduct_SpecialOffer_SpecialOfferID'
GO
