/****** Object:  Table [Purchasing].[ProductVendor]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]') AND type in (N'U'))
BEGIN
CREATE TABLE [Purchasing].[ProductVendor](
	[ProductID] [int] NOT NULL,
	[BusinessEntityID] [int] NOT NULL,
	[AverageLeadTime] [int] NOT NULL,
	[StandardPrice] [money] NOT NULL,
	[LastReceiptCost] [money] NULL,
	[LastReceiptDate] [datetime] NULL,
	[MinOrderQty] [int] NOT NULL,
	[MaxOrderQty] [int] NOT NULL,
	[OnOrderQty] [int] NULL,
	[UnitMeasureCode] [nchar](3) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductVendor_ProductID_BusinessEntityID] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[BusinessEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [IX_ProductVendor_BusinessEntityID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]') AND name = N'IX_ProductVendor_BusinessEntityID')
CREATE NONCLUSTERED INDEX [IX_ProductVendor_BusinessEntityID] ON [Purchasing].[ProductVendor]
(
	[BusinessEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductVendor_UnitMeasureCode]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]') AND name = N'IX_ProductVendor_UnitMeasureCode')
CREATE NONCLUSTERED INDEX [IX_ProductVendor_UnitMeasureCode] ON [Purchasing].[ProductVendor]
(
	[UnitMeasureCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_ProductVendor_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[ProductVendor] ADD  CONSTRAINT [DF_ProductVendor_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_ProductVendor_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor]  WITH CHECK ADD  CONSTRAINT [FK_ProductVendor_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [Production].[Product] ([ProductID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_ProductVendor_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor] CHECK CONSTRAINT [FK_ProductVendor_Product_ProductID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_ProductVendor_UnitMeasure_UnitMeasureCode]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor]  WITH CHECK ADD  CONSTRAINT [FK_ProductVendor_UnitMeasure_UnitMeasureCode] FOREIGN KEY([UnitMeasureCode])
REFERENCES [Production].[UnitMeasure] ([UnitMeasureCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_ProductVendor_UnitMeasure_UnitMeasureCode]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor] CHECK CONSTRAINT [FK_ProductVendor_UnitMeasure_UnitMeasureCode]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_ProductVendor_Vendor_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor]  WITH CHECK ADD  CONSTRAINT [FK_ProductVendor_Vendor_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Purchasing].[Vendor] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_ProductVendor_Vendor_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor] CHECK CONSTRAINT [FK_ProductVendor_Vendor_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_AverageLeadTime]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor]  WITH CHECK ADD  CONSTRAINT [CK_ProductVendor_AverageLeadTime] CHECK  (([AverageLeadTime]>=(1)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_AverageLeadTime]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor] CHECK CONSTRAINT [CK_ProductVendor_AverageLeadTime]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_LastReceiptCost]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor]  WITH CHECK ADD  CONSTRAINT [CK_ProductVendor_LastReceiptCost] CHECK  (([LastReceiptCost]>(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_LastReceiptCost]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor] CHECK CONSTRAINT [CK_ProductVendor_LastReceiptCost]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_MaxOrderQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor]  WITH CHECK ADD  CONSTRAINT [CK_ProductVendor_MaxOrderQty] CHECK  (([MaxOrderQty]>=(1)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_MaxOrderQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor] CHECK CONSTRAINT [CK_ProductVendor_MaxOrderQty]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_MinOrderQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor]  WITH CHECK ADD  CONSTRAINT [CK_ProductVendor_MinOrderQty] CHECK  (([MinOrderQty]>=(1)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_MinOrderQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor] CHECK CONSTRAINT [CK_ProductVendor_MinOrderQty]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_OnOrderQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor]  WITH CHECK ADD  CONSTRAINT [CK_ProductVendor_OnOrderQty] CHECK  (([OnOrderQty]>=(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_OnOrderQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor] CHECK CONSTRAINT [CK_ProductVendor_OnOrderQty]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_StandardPrice]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor]  WITH CHECK ADD  CONSTRAINT [CK_ProductVendor_StandardPrice] CHECK  (([StandardPrice]>(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ProductVendor_StandardPrice]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ProductVendor]'))
ALTER TABLE [Purchasing].[ProductVendor] CHECK CONSTRAINT [CK_ProductVendor_StandardPrice]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'COLUMN',N'ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to Vendor.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'COLUMN',N'AverageLeadTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The average span of time (in days) between placing an order with the vendor and receiving the purchased product.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'COLUMN',@level2name=N'AverageLeadTime'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'COLUMN',N'StandardPrice'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The vendor''s usual selling price.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'COLUMN',@level2name=N'StandardPrice'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'COLUMN',N'LastReceiptCost'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The selling price when last purchased.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'COLUMN',@level2name=N'LastReceiptCost'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'COLUMN',N'LastReceiptDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date the product was last received by the vendor.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'COLUMN',@level2name=N'LastReceiptDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'COLUMN',N'MinOrderQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The maximum quantity that should be ordered.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'COLUMN',@level2name=N'MinOrderQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'COLUMN',N'MaxOrderQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The minimum quantity that should be ordered.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'COLUMN',@level2name=N'MaxOrderQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'COLUMN',N'OnOrderQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The quantity currently on order.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'COLUMN',@level2name=N'OnOrderQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'COLUMN',N'UnitMeasureCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The product''s unit of measure.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'COLUMN',@level2name=N'UnitMeasureCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'CONSTRAINT',N'DF_ProductVendor_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'CONSTRAINT',@level2name=N'DF_ProductVendor_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'CONSTRAINT',N'PK_ProductVendor_ProductID_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'CONSTRAINT',@level2name=N'PK_ProductVendor_ProductID_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'INDEX',N'IX_ProductVendor_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'INDEX',@level2name=N'IX_ProductVendor_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'INDEX',N'IX_ProductVendor_UnitMeasureCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'INDEX',@level2name=N'IX_ProductVendor_UnitMeasureCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cross-reference table mapping vendors with the products they supply.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'CONSTRAINT',N'FK_ProductVendor_Product_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductVendor_Product_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'CONSTRAINT',N'FK_ProductVendor_UnitMeasure_UnitMeasureCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing UnitMeasure.UnitMeasureCode.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductVendor_UnitMeasure_UnitMeasureCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'CONSTRAINT',N'FK_ProductVendor_Vendor_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Vendor.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductVendor_Vendor_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'CONSTRAINT',N'CK_ProductVendor_AverageLeadTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [AverageLeadTime] >= (1)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'CONSTRAINT',@level2name=N'CK_ProductVendor_AverageLeadTime'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'CONSTRAINT',N'CK_ProductVendor_LastReceiptCost'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [LastReceiptCost] > (0.00)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'CONSTRAINT',@level2name=N'CK_ProductVendor_LastReceiptCost'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'CONSTRAINT',N'CK_ProductVendor_MaxOrderQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [MaxOrderQty] >= (1)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'CONSTRAINT',@level2name=N'CK_ProductVendor_MaxOrderQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'CONSTRAINT',N'CK_ProductVendor_MinOrderQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [MinOrderQty] >= (1)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'CONSTRAINT',@level2name=N'CK_ProductVendor_MinOrderQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'CONSTRAINT',N'CK_ProductVendor_OnOrderQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [OnOrderQty] >= (0)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'CONSTRAINT',@level2name=N'CK_ProductVendor_OnOrderQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ProductVendor', N'CONSTRAINT',N'CK_ProductVendor_StandardPrice'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [StandardPrice] > (0.00)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ProductVendor', @level2type=N'CONSTRAINT',@level2name=N'CK_ProductVendor_StandardPrice'
GO
