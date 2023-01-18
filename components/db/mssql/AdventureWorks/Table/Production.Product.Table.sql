/****** Object:  Table [Production].[Product]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[Product]') AND type in (N'U'))
BEGIN
CREATE TABLE [Production].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[ProductNumber] [nvarchar](25) NOT NULL,
	[MakeFlag] [dbo].[Flag] NOT NULL,
	[FinishedGoodsFlag] [dbo].[Flag] NOT NULL,
	[Color] [nvarchar](15) NULL,
	[SafetyStockLevel] [smallint] NOT NULL,
	[ReorderPoint] [smallint] NOT NULL,
	[StandardCost] [money] NOT NULL,
	[ListPrice] [money] NOT NULL,
	[Size] [nvarchar](5) NULL,
	[SizeUnitMeasureCode] [nchar](3) NULL,
	[WeightUnitMeasureCode] [nchar](3) NULL,
	[Weight] [decimal](8, 2) NULL,
	[DaysToManufacture] [int] NOT NULL,
	[ProductLine] [nchar](2) NULL,
	[Class] [nchar](2) NULL,
	[Style] [nchar](2) NULL,
	[ProductSubcategoryID] [int] NULL,
	[ProductModelID] [int] NULL,
	[SellStartDate] [datetime] NOT NULL,
	[SellEndDate] [datetime] NULL,
	[DiscontinuedDate] [datetime] NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_ProductID] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Product_Name]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[Product]') AND name = N'AK_Product_Name')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Product_Name] ON [Production].[Product]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Product_ProductNumber]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[Product]') AND name = N'AK_Product_ProductNumber')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Product_ProductNumber] ON [Production].[Product]
(
	[ProductNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AK_Product_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[Product]') AND name = N'AK_Product_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Product_rowguid] ON [Production].[Product]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_Product_MakeFlag]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_MakeFlag]  DEFAULT ((1)) FOR [MakeFlag]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_Product_FinishedGoodsFlag]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_FinishedGoodsFlag]  DEFAULT ((1)) FOR [FinishedGoodsFlag]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_Product_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_Product_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_Product_ProductModel_ProductModelID]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductModel_ProductModelID] FOREIGN KEY([ProductModelID])
REFERENCES [Production].[ProductModel] ([ProductModelID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_Product_ProductModel_ProductModelID]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [FK_Product_ProductModel_ProductModelID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_Product_ProductSubcategory_ProductSubcategoryID]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductSubcategory_ProductSubcategoryID] FOREIGN KEY([ProductSubcategoryID])
REFERENCES [Production].[ProductSubcategory] ([ProductSubcategoryID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_Product_ProductSubcategory_ProductSubcategoryID]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [FK_Product_ProductSubcategory_ProductSubcategoryID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_Product_UnitMeasure_SizeUnitMeasureCode]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_UnitMeasure_SizeUnitMeasureCode] FOREIGN KEY([SizeUnitMeasureCode])
REFERENCES [Production].[UnitMeasure] ([UnitMeasureCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_Product_UnitMeasure_SizeUnitMeasureCode]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [FK_Product_UnitMeasure_SizeUnitMeasureCode]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_Product_UnitMeasure_WeightUnitMeasureCode]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_UnitMeasure_WeightUnitMeasureCode] FOREIGN KEY([WeightUnitMeasureCode])
REFERENCES [Production].[UnitMeasure] ([UnitMeasureCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_Product_UnitMeasure_WeightUnitMeasureCode]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [FK_Product_UnitMeasure_WeightUnitMeasureCode]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_Class]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [CK_Product_Class] CHECK  ((upper([Class])='H' OR upper([Class])='M' OR upper([Class])='L' OR [Class] IS NULL))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_Class]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [CK_Product_Class]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_DaysToManufacture]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [CK_Product_DaysToManufacture] CHECK  (([DaysToManufacture]>=(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_DaysToManufacture]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [CK_Product_DaysToManufacture]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_ListPrice]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [CK_Product_ListPrice] CHECK  (([ListPrice]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_ListPrice]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [CK_Product_ListPrice]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_ProductLine]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [CK_Product_ProductLine] CHECK  ((upper([ProductLine])='R' OR upper([ProductLine])='M' OR upper([ProductLine])='T' OR upper([ProductLine])='S' OR [ProductLine] IS NULL))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_ProductLine]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [CK_Product_ProductLine]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_ReorderPoint]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [CK_Product_ReorderPoint] CHECK  (([ReorderPoint]>(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_ReorderPoint]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [CK_Product_ReorderPoint]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_SafetyStockLevel]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [CK_Product_SafetyStockLevel] CHECK  (([SafetyStockLevel]>(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_SafetyStockLevel]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [CK_Product_SafetyStockLevel]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_SellEndDate]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [CK_Product_SellEndDate] CHECK  (([SellEndDate]>=[SellStartDate] OR [SellEndDate] IS NULL))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_SellEndDate]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [CK_Product_SellEndDate]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_StandardCost]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [CK_Product_StandardCost] CHECK  (([StandardCost]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_StandardCost]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [CK_Product_StandardCost]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_Style]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [CK_Product_Style] CHECK  ((upper([Style])='U' OR upper([Style])='M' OR upper([Style])='W' OR [Style] IS NULL))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_Style]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [CK_Product_Style]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_Weight]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [CK_Product_Weight] CHECK  (([Weight]>(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Product_Weight]') AND parent_object_id = OBJECT_ID(N'[Production].[Product]'))
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [CK_Product_Weight]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for Product records.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name of the product.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'ProductNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique product identification number.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ProductNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'MakeFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = Product is purchased, 1 = Product is manufactured in-house.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'MakeFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'DF_Product_MakeFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of  1' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'DF_Product_MakeFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'FinishedGoodsFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = Product is not a salable item. 1 = Product is salable.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'FinishedGoodsFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'DF_Product_FinishedGoodsFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of  1' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'DF_Product_FinishedGoodsFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'Color'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product color.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Color'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'SafetyStockLevel'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Minimum inventory quantity. ' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'SafetyStockLevel'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'ReorderPoint'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Inventory level that triggers a purchase order or work order. ' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ReorderPoint'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'StandardCost'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Standard cost of the product.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'StandardCost'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'ListPrice'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Selling price.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ListPrice'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'Size'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product size.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Size'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'SizeUnitMeasureCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unit of measure for Size column.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'SizeUnitMeasureCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'WeightUnitMeasureCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unit of measure for Weight column.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'WeightUnitMeasureCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'Weight'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product weight.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Weight'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'DaysToManufacture'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Number of days required to manufacture the product.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'DaysToManufacture'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'ProductLine'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'R = Road, M = Mountain, T = Touring, S = Standard' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ProductLine'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'Class'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'H = High, M = Medium, L = Low' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Class'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'Style'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'W = Womens, M = Mens, U = Universal' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Style'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'ProductSubcategoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product is a member of this product subcategory. Foreign key to ProductSubCategory.ProductSubCategoryID. ' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ProductSubcategoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'ProductModelID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product is a member of this product model. Foreign key to ProductModel.ProductModelID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ProductModelID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'SellStartDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date the product was available for sale.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'SellStartDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'SellEndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date the product was no longer available for sale.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'SellEndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'DiscontinuedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date the product was discontinued.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'DiscontinuedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'DF_Product_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'DF_Product_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'DF_Product_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'DF_Product_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'PK_Product_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'PK_Product_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'INDEX',N'AK_Product_Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'INDEX',@level2name=N'AK_Product_Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'INDEX',N'AK_Product_ProductNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'INDEX',@level2name=N'AK_Product_ProductNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'INDEX',N'AK_Product_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'INDEX',@level2name=N'AK_Product_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Products sold or used in the manfacturing of sold products.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'FK_Product_ProductModel_ProductModelID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing ProductModel.ProductModelID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'FK_Product_ProductModel_ProductModelID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'FK_Product_ProductSubcategory_ProductSubcategoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing ProductSubcategory.ProductSubcategoryID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'FK_Product_ProductSubcategory_ProductSubcategoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'FK_Product_UnitMeasure_SizeUnitMeasureCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing UnitMeasure.UnitMeasureCode.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'FK_Product_UnitMeasure_SizeUnitMeasureCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'FK_Product_UnitMeasure_WeightUnitMeasureCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing UnitMeasure.UnitMeasureCode.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'FK_Product_UnitMeasure_WeightUnitMeasureCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'CK_Product_Class'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Class]=''h'' OR [Class]=''m'' OR [Class]=''l'' OR [Class]=''H'' OR [Class]=''M'' OR [Class]=''L'' OR [Class] IS NULL' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'CK_Product_Class'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'CK_Product_DaysToManufacture'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [DaysToManufacture] >= (0)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'CK_Product_DaysToManufacture'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'CK_Product_ListPrice'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ListPrice] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'CK_Product_ListPrice'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'CK_Product_ProductLine'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ProductLine]=''r'' OR [ProductLine]=''m'' OR [ProductLine]=''t'' OR [ProductLine]=''s'' OR [ProductLine]=''R'' OR [ProductLine]=''M'' OR [ProductLine]=''T'' OR [ProductLine]=''S'' OR [ProductLine] IS NULL' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'CK_Product_ProductLine'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'CK_Product_ReorderPoint'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ReorderPoint] > (0)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'CK_Product_ReorderPoint'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'CK_Product_SafetyStockLevel'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SafetyStockLevel] > (0)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'CK_Product_SafetyStockLevel'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'CK_Product_SellEndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SellEndDate] >= [SellStartDate] OR [SellEndDate] IS NULL' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'CK_Product_SellEndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'CK_Product_StandardCost'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SafetyStockLevel] > (0)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'CK_Product_StandardCost'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'CK_Product_Style'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Style]=''u'' OR [Style]=''m'' OR [Style]=''w'' OR [Style]=''U'' OR [Style]=''M'' OR [Style]=''W'' OR [Style] IS NULL' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'CK_Product_Style'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Product', N'CONSTRAINT',N'CK_Product_Weight'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Weight] > (0.00)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'CK_Product_Weight'
GO
