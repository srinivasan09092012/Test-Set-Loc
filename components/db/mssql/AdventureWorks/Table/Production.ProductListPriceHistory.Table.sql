/****** Object:  Table [Production].[ProductListPriceHistory]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[ProductListPriceHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [Production].[ProductListPriceHistory](
	[ProductID] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[ListPrice] [money] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductListPriceHistory_ProductID_StartDate] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_ProductListPriceHistory_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[ProductListPriceHistory] ADD  CONSTRAINT [DF_ProductListPriceHistory_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductListPriceHistory_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductListPriceHistory]'))
ALTER TABLE [Production].[ProductListPriceHistory]  WITH CHECK ADD  CONSTRAINT [FK_ProductListPriceHistory_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [Production].[Product] ([ProductID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductListPriceHistory_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductListPriceHistory]'))
ALTER TABLE [Production].[ProductListPriceHistory] CHECK CONSTRAINT [FK_ProductListPriceHistory_Product_ProductID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_ProductListPriceHistory_EndDate]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductListPriceHistory]'))
ALTER TABLE [Production].[ProductListPriceHistory]  WITH CHECK ADD  CONSTRAINT [CK_ProductListPriceHistory_EndDate] CHECK  (([EndDate]>=[StartDate] OR [EndDate] IS NULL))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_ProductListPriceHistory_EndDate]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductListPriceHistory]'))
ALTER TABLE [Production].[ProductListPriceHistory] CHECK CONSTRAINT [CK_ProductListPriceHistory_EndDate]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_ProductListPriceHistory_ListPrice]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductListPriceHistory]'))
ALTER TABLE [Production].[ProductListPriceHistory]  WITH CHECK ADD  CONSTRAINT [CK_ProductListPriceHistory_ListPrice] CHECK  (([ListPrice]>(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_ProductListPriceHistory_ListPrice]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductListPriceHistory]'))
ALTER TABLE [Production].[ProductListPriceHistory] CHECK CONSTRAINT [CK_ProductListPriceHistory_ListPrice]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductListPriceHistory', N'COLUMN',N'ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product identification number. Foreign key to Product.ProductID' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductListPriceHistory', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductListPriceHistory', N'COLUMN',N'StartDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'List price start date.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductListPriceHistory', @level2type=N'COLUMN',@level2name=N'StartDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductListPriceHistory', N'COLUMN',N'EndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'List price end date' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductListPriceHistory', @level2type=N'COLUMN',@level2name=N'EndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductListPriceHistory', N'COLUMN',N'ListPrice'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product list price.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductListPriceHistory', @level2type=N'COLUMN',@level2name=N'ListPrice'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductListPriceHistory', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductListPriceHistory', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductListPriceHistory', N'CONSTRAINT',N'DF_ProductListPriceHistory_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductListPriceHistory', @level2type=N'CONSTRAINT',@level2name=N'DF_ProductListPriceHistory_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductListPriceHistory', N'CONSTRAINT',N'PK_ProductListPriceHistory_ProductID_StartDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductListPriceHistory', @level2type=N'CONSTRAINT',@level2name=N'PK_ProductListPriceHistory_ProductID_StartDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductListPriceHistory', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Changes in the list price of a product over time.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductListPriceHistory'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductListPriceHistory', N'CONSTRAINT',N'FK_ProductListPriceHistory_Product_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductListPriceHistory', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductListPriceHistory_Product_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductListPriceHistory', N'CONSTRAINT',N'CK_ProductListPriceHistory_EndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [EndDate] >= [StartDate] OR [EndDate] IS NULL' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductListPriceHistory', @level2type=N'CONSTRAINT',@level2name=N'CK_ProductListPriceHistory_EndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductListPriceHistory', N'CONSTRAINT',N'CK_ProductListPriceHistory_ListPrice'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ListPrice] > (0.00)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductListPriceHistory', @level2type=N'CONSTRAINT',@level2name=N'CK_ProductListPriceHistory_ListPrice'
GO
