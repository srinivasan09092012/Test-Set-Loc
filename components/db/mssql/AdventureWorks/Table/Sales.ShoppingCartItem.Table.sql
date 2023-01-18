/****** Object:  Table [Sales].[ShoppingCartItem]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[ShoppingCartItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[ShoppingCartItem](
	[ShoppingCartItemID] [int] IDENTITY(1,1) NOT NULL,
	[ShoppingCartID] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ShoppingCartItem_ShoppingCartItemID] PRIMARY KEY CLUSTERED 
(
	[ShoppingCartItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ShoppingCartItem_ShoppingCartID_ProductID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[ShoppingCartItem]') AND name = N'IX_ShoppingCartItem_ShoppingCartID_ProductID')
CREATE NONCLUSTERED INDEX [IX_ShoppingCartItem_ShoppingCartID_ProductID] ON [Sales].[ShoppingCartItem]
(
	[ShoppingCartID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_ShoppingCartItem_Quantity]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[ShoppingCartItem] ADD  CONSTRAINT [DF_ShoppingCartItem_Quantity]  DEFAULT ((1)) FOR [Quantity]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_ShoppingCartItem_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[ShoppingCartItem] ADD  CONSTRAINT [DF_ShoppingCartItem_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_ShoppingCartItem_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[ShoppingCartItem] ADD  CONSTRAINT [DF_ShoppingCartItem_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_ShoppingCartItem_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Sales].[ShoppingCartItem]'))
ALTER TABLE [Sales].[ShoppingCartItem]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCartItem_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [Production].[Product] ([ProductID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_ShoppingCartItem_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Sales].[ShoppingCartItem]'))
ALTER TABLE [Sales].[ShoppingCartItem] CHECK CONSTRAINT [FK_ShoppingCartItem_Product_ProductID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_ShoppingCartItem_Quantity]') AND parent_object_id = OBJECT_ID(N'[Sales].[ShoppingCartItem]'))
ALTER TABLE [Sales].[ShoppingCartItem]  WITH CHECK ADD  CONSTRAINT [CK_ShoppingCartItem_Quantity] CHECK  (([Quantity]>=(1)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_ShoppingCartItem_Quantity]') AND parent_object_id = OBJECT_ID(N'[Sales].[ShoppingCartItem]'))
ALTER TABLE [Sales].[ShoppingCartItem] CHECK CONSTRAINT [CK_ShoppingCartItem_Quantity]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'COLUMN',N'ShoppingCartItemID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for ShoppingCartItem records.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'COLUMN',@level2name=N'ShoppingCartItemID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'COLUMN',N'ShoppingCartID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Shopping cart identification number.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'COLUMN',@level2name=N'ShoppingCartID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'COLUMN',N'Quantity'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product quantity ordered.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'CONSTRAINT',N'DF_ShoppingCartItem_Quantity'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 1' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'CONSTRAINT',@level2name=N'DF_ShoppingCartItem_Quantity'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'COLUMN',N'ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product ordered. Foreign key to Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'COLUMN',N'DateCreated'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date the time the record was created.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'COLUMN',@level2name=N'DateCreated'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'CONSTRAINT',N'DF_ShoppingCartItem_DateCreated'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'CONSTRAINT',@level2name=N'DF_ShoppingCartItem_DateCreated'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'CONSTRAINT',N'DF_ShoppingCartItem_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'CONSTRAINT',@level2name=N'DF_ShoppingCartItem_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'CONSTRAINT',N'PK_ShoppingCartItem_ShoppingCartItemID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'CONSTRAINT',@level2name=N'PK_ShoppingCartItem_ShoppingCartItemID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'INDEX',N'IX_ShoppingCartItem_ShoppingCartID_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'INDEX',@level2name=N'IX_ShoppingCartItem_ShoppingCartID_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contains online customer orders until the order is submitted or cancelled.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'CONSTRAINT',N'FK_ShoppingCartItem_Product_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'CONSTRAINT',@level2name=N'FK_ShoppingCartItem_Product_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'ShoppingCartItem', N'CONSTRAINT',N'CK_ShoppingCartItem_Quantity'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Quantity] >= (1)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'ShoppingCartItem', @level2type=N'CONSTRAINT',@level2name=N'CK_ShoppingCartItem_Quantity'
GO
