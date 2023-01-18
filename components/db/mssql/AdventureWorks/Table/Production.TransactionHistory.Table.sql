/****** Object:  Table [Production].[TransactionHistory]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[TransactionHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [Production].[TransactionHistory](
	[TransactionID] [int] IDENTITY(100000,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[ReferenceOrderID] [int] NOT NULL,
	[ReferenceOrderLineID] [int] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionType] [nchar](1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[ActualCost] [money] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TransactionHistory_TransactionID] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [IX_TransactionHistory_ProductID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[TransactionHistory]') AND name = N'IX_TransactionHistory_ProductID')
CREATE NONCLUSTERED INDEX [IX_TransactionHistory_ProductID] ON [Production].[TransactionHistory]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TransactionHistory_ReferenceOrderID_ReferenceOrderLineID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[TransactionHistory]') AND name = N'IX_TransactionHistory_ReferenceOrderID_ReferenceOrderLineID')
CREATE NONCLUSTERED INDEX [IX_TransactionHistory_ReferenceOrderID_ReferenceOrderLineID] ON [Production].[TransactionHistory]
(
	[ReferenceOrderID] ASC,
	[ReferenceOrderLineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_TransactionHistory_ReferenceOrderLineID]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[TransactionHistory] ADD  CONSTRAINT [DF_TransactionHistory_ReferenceOrderLineID]  DEFAULT ((0)) FOR [ReferenceOrderLineID]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_TransactionHistory_TransactionDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[TransactionHistory] ADD  CONSTRAINT [DF_TransactionHistory_TransactionDate]  DEFAULT (getdate()) FOR [TransactionDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_TransactionHistory_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[TransactionHistory] ADD  CONSTRAINT [DF_TransactionHistory_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_TransactionHistory_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Production].[TransactionHistory]'))
ALTER TABLE [Production].[TransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_TransactionHistory_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [Production].[Product] ([ProductID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_TransactionHistory_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Production].[TransactionHistory]'))
ALTER TABLE [Production].[TransactionHistory] CHECK CONSTRAINT [FK_TransactionHistory_Product_ProductID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_TransactionHistory_TransactionType]') AND parent_object_id = OBJECT_ID(N'[Production].[TransactionHistory]'))
ALTER TABLE [Production].[TransactionHistory]  WITH CHECK ADD  CONSTRAINT [CK_TransactionHistory_TransactionType] CHECK  ((upper([TransactionType])='P' OR upper([TransactionType])='S' OR upper([TransactionType])='W'))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_TransactionHistory_TransactionType]') AND parent_object_id = OBJECT_ID(N'[Production].[TransactionHistory]'))
ALTER TABLE [Production].[TransactionHistory] CHECK CONSTRAINT [CK_TransactionHistory_TransactionType]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'COLUMN',N'TransactionID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for TransactionHistory records.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'COLUMN',@level2name=N'TransactionID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'COLUMN',N'ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product identification number. Foreign key to Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'COLUMN',N'ReferenceOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Purchase order, sales order, or work order identification number.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'COLUMN',@level2name=N'ReferenceOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'COLUMN',N'ReferenceOrderLineID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Line number associated with the purchase order, sales order, or work order.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'COLUMN',@level2name=N'ReferenceOrderLineID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'CONSTRAINT',N'DF_TransactionHistory_ReferenceOrderLineID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'CONSTRAINT',@level2name=N'DF_TransactionHistory_ReferenceOrderLineID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'COLUMN',N'TransactionDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time of the transaction.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'COLUMN',@level2name=N'TransactionDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'CONSTRAINT',N'DF_TransactionHistory_TransactionDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'CONSTRAINT',@level2name=N'DF_TransactionHistory_TransactionDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'COLUMN',N'TransactionType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'W = WorkOrder, S = SalesOrder, P = PurchaseOrder' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'COLUMN',@level2name=N'TransactionType'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'COLUMN',N'Quantity'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product quantity.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'COLUMN',N'ActualCost'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product cost.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'COLUMN',@level2name=N'ActualCost'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'CONSTRAINT',N'DF_TransactionHistory_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'CONSTRAINT',@level2name=N'DF_TransactionHistory_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'CONSTRAINT',N'PK_TransactionHistory_TransactionID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'CONSTRAINT',@level2name=N'PK_TransactionHistory_TransactionID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'INDEX',N'IX_TransactionHistory_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'INDEX',@level2name=N'IX_TransactionHistory_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'INDEX',N'IX_TransactionHistory_ReferenceOrderID_ReferenceOrderLineID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'INDEX',@level2name=N'IX_TransactionHistory_ReferenceOrderID_ReferenceOrderLineID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Record of each purchase order, sales order, or work order transaction year to date.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'CONSTRAINT',N'FK_TransactionHistory_Product_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'CONSTRAINT',@level2name=N'FK_TransactionHistory_Product_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'TransactionHistory', N'CONSTRAINT',N'CK_TransactionHistory_TransactionType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [TransactionType]=''p'' OR [TransactionType]=''s'' OR [TransactionType]=''w'' OR [TransactionType]=''P'' OR [TransactionType]=''S'' OR [TransactionType]=''W'')' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'TransactionHistory', @level2type=N'CONSTRAINT',@level2name=N'CK_TransactionHistory_TransactionType'
GO
