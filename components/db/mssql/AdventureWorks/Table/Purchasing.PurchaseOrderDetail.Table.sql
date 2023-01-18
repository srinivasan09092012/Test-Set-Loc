/****** Object:  Table [Purchasing].[PurchaseOrderDetail]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [Purchasing].[PurchaseOrderDetail](
	[PurchaseOrderID] [int] NOT NULL,
	[PurchaseOrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[OrderQty] [smallint] NOT NULL,
	[ProductID] [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[LineTotal]  AS (isnull([OrderQty]*[UnitPrice],(0.00))),
	[ReceivedQty] [decimal](8, 2) NOT NULL,
	[RejectedQty] [decimal](8, 2) NOT NULL,
	[StockedQty]  AS (isnull([ReceivedQty]-[RejectedQty],(0.00))),
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PurchaseOrderDetail_PurchaseOrderID_PurchaseOrderDetailID] PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderID] ASC,
	[PurchaseOrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [IX_PurchaseOrderDetail_ProductID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]') AND name = N'IX_PurchaseOrderDetail_ProductID')
CREATE NONCLUSTERED INDEX [IX_PurchaseOrderDetail_ProductID] ON [Purchasing].[PurchaseOrderDetail]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_PurchaseOrderDetail_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[PurchaseOrderDetail] ADD  CONSTRAINT [DF_PurchaseOrderDetail_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_PurchaseOrderDetail_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderDetail_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [Production].[Product] ([ProductID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_PurchaseOrderDetail_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail] CHECK CONSTRAINT [FK_PurchaseOrderDetail_Product_ProductID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_PurchaseOrderDetail_PurchaseOrderHeader_PurchaseOrderID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderDetail_PurchaseOrderHeader_PurchaseOrderID] FOREIGN KEY([PurchaseOrderID])
REFERENCES [Purchasing].[PurchaseOrderHeader] ([PurchaseOrderID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_PurchaseOrderDetail_PurchaseOrderHeader_PurchaseOrderID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail] CHECK CONSTRAINT [FK_PurchaseOrderDetail_PurchaseOrderHeader_PurchaseOrderID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderDetail_OrderQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail]  WITH CHECK ADD  CONSTRAINT [CK_PurchaseOrderDetail_OrderQty] CHECK  (([OrderQty]>(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderDetail_OrderQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail] CHECK CONSTRAINT [CK_PurchaseOrderDetail_OrderQty]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderDetail_ReceivedQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail]  WITH CHECK ADD  CONSTRAINT [CK_PurchaseOrderDetail_ReceivedQty] CHECK  (([ReceivedQty]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderDetail_ReceivedQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail] CHECK CONSTRAINT [CK_PurchaseOrderDetail_ReceivedQty]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderDetail_RejectedQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail]  WITH CHECK ADD  CONSTRAINT [CK_PurchaseOrderDetail_RejectedQty] CHECK  (([RejectedQty]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderDetail_RejectedQty]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail] CHECK CONSTRAINT [CK_PurchaseOrderDetail_RejectedQty]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderDetail_UnitPrice]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail]  WITH CHECK ADD  CONSTRAINT [CK_PurchaseOrderDetail_UnitPrice] CHECK  (([UnitPrice]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderDetail_UnitPrice]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderDetail]'))
ALTER TABLE [Purchasing].[PurchaseOrderDetail] CHECK CONSTRAINT [CK_PurchaseOrderDetail_UnitPrice]
GO
/****** Object:  Trigger [Purchasing].[iPurchaseOrderDetail]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[Purchasing].[iPurchaseOrderDetail]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [Purchasing].[iPurchaseOrderDetail] ON [Purchasing].[PurchaseOrderDetail] 
AFTER INSERT AS
BEGIN
    DECLARE @Count int;

    SET @Count = @@ROWCOUNT;
    IF @Count = 0 
        RETURN;

    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO [Production].[TransactionHistory]
            ([ProductID]
            ,[ReferenceOrderID]
            ,[ReferenceOrderLineID]
            ,[TransactionType]
            ,[TransactionDate]
            ,[Quantity]
            ,[ActualCost])
        SELECT 
            inserted.[ProductID]
            ,inserted.[PurchaseOrderID]
            ,inserted.[PurchaseOrderDetailID]
            ,''P''
            ,GETDATE()
            ,inserted.[OrderQty]
            ,inserted.[UnitPrice]
        FROM inserted 
            INNER JOIN [Purchasing].[PurchaseOrderHeader] 
            ON inserted.[PurchaseOrderID] = [Purchasing].[PurchaseOrderHeader].[PurchaseOrderID];

        -- Update SubTotal in PurchaseOrderHeader record. Note that this causes the 
        -- PurchaseOrderHeader trigger to fire which will update the RevisionNumber.
        UPDATE [Purchasing].[PurchaseOrderHeader]
        SET [Purchasing].[PurchaseOrderHeader].[SubTotal] = 
            (SELECT SUM([Purchasing].[PurchaseOrderDetail].[LineTotal])
                FROM [Purchasing].[PurchaseOrderDetail]
                WHERE [Purchasing].[PurchaseOrderHeader].[PurchaseOrderID] = [Purchasing].[PurchaseOrderDetail].[PurchaseOrderID])
        WHERE [Purchasing].[PurchaseOrderHeader].[PurchaseOrderID] IN (SELECT inserted.[PurchaseOrderID] FROM inserted);
    END TRY
    BEGIN CATCH
        EXECUTE [dbo].[uspPrintError];

        -- Rollback any active or uncommittable transactions before
        -- inserting information in the ErrorLog
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        EXECUTE [dbo].[uspLogError];
    END CATCH;
END;
' 
GO
ALTER TABLE [Purchasing].[PurchaseOrderDetail] ENABLE TRIGGER [iPurchaseOrderDetail]
GO
/****** Object:  Trigger [Purchasing].[uPurchaseOrderDetail]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[Purchasing].[uPurchaseOrderDetail]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [Purchasing].[uPurchaseOrderDetail] ON [Purchasing].[PurchaseOrderDetail] 
AFTER UPDATE AS 
BEGIN
    DECLARE @Count int;

    SET @Count = @@ROWCOUNT;
    IF @Count = 0 
        RETURN;

    SET NOCOUNT ON;

    BEGIN TRY
        IF UPDATE([ProductID]) OR UPDATE([OrderQty]) OR UPDATE([UnitPrice])
        -- Insert record into TransactionHistory 
        BEGIN
            INSERT INTO [Production].[TransactionHistory]
                ([ProductID]
                ,[ReferenceOrderID]
                ,[ReferenceOrderLineID]
                ,[TransactionType]
                ,[TransactionDate]
                ,[Quantity]
                ,[ActualCost])
            SELECT 
                inserted.[ProductID]
                ,inserted.[PurchaseOrderID]
                ,inserted.[PurchaseOrderDetailID]
                ,''P''
                ,GETDATE()
                ,inserted.[OrderQty]
                ,inserted.[UnitPrice]
            FROM inserted 
                INNER JOIN [Purchasing].[PurchaseOrderDetail] 
                ON inserted.[PurchaseOrderID] = [Purchasing].[PurchaseOrderDetail].[PurchaseOrderID];

            -- Update SubTotal in PurchaseOrderHeader record. Note that this causes the 
            -- PurchaseOrderHeader trigger to fire which will update the RevisionNumber.
            UPDATE [Purchasing].[PurchaseOrderHeader]
            SET [Purchasing].[PurchaseOrderHeader].[SubTotal] = 
                (SELECT SUM([Purchasing].[PurchaseOrderDetail].[LineTotal])
                    FROM [Purchasing].[PurchaseOrderDetail]
                    WHERE [Purchasing].[PurchaseOrderHeader].[PurchaseOrderID] 
                        = [Purchasing].[PurchaseOrderDetail].[PurchaseOrderID])
            WHERE [Purchasing].[PurchaseOrderHeader].[PurchaseOrderID] 
                IN (SELECT inserted.[PurchaseOrderID] FROM inserted);

            UPDATE [Purchasing].[PurchaseOrderDetail]
            SET [Purchasing].[PurchaseOrderDetail].[ModifiedDate] = GETDATE()
            FROM inserted
            WHERE inserted.[PurchaseOrderID] = [Purchasing].[PurchaseOrderDetail].[PurchaseOrderID]
                AND inserted.[PurchaseOrderDetailID] = [Purchasing].[PurchaseOrderDetail].[PurchaseOrderDetailID];
        END;
    END TRY
    BEGIN CATCH
        EXECUTE [dbo].[uspPrintError];

        -- Rollback any active or uncommittable transactions before
        -- inserting information in the ErrorLog
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        EXECUTE [dbo].[uspLogError];
    END CATCH;
END;
' 
GO
ALTER TABLE [Purchasing].[PurchaseOrderDetail] ENABLE TRIGGER [uPurchaseOrderDetail]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'COLUMN',N'PurchaseOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to PurchaseOrderHeader.PurchaseOrderID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'COLUMN',@level2name=N'PurchaseOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'COLUMN',N'PurchaseOrderDetailID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. One line number per purchased product.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'COLUMN',@level2name=N'PurchaseOrderDetailID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'COLUMN',N'DueDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date the product is expected to be received.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'COLUMN',@level2name=N'DueDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'COLUMN',N'OrderQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Quantity ordered.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'COLUMN',@level2name=N'OrderQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'COLUMN',N'ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product identification number. Foreign key to Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'COLUMN',N'UnitPrice'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vendor''s selling price of a single product.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'COLUMN',@level2name=N'UnitPrice'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'COLUMN',N'LineTotal'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Per product subtotal. Computed as OrderQty * UnitPrice.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'COLUMN',@level2name=N'LineTotal'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'COLUMN',N'ReceivedQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Quantity actually received from the vendor.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'COLUMN',@level2name=N'ReceivedQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'COLUMN',N'RejectedQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Quantity rejected during inspection.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'COLUMN',@level2name=N'RejectedQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'COLUMN',N'StockedQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Quantity accepted into inventory. Computed as ReceivedQty - RejectedQty.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'COLUMN',@level2name=N'StockedQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'CONSTRAINT',N'DF_PurchaseOrderDetail_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'CONSTRAINT',@level2name=N'DF_PurchaseOrderDetail_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'CONSTRAINT',N'PK_PurchaseOrderDetail_PurchaseOrderID_PurchaseOrderDetailID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'CONSTRAINT',@level2name=N'PK_PurchaseOrderDetail_PurchaseOrderID_PurchaseOrderDetailID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'INDEX',N'IX_PurchaseOrderDetail_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'INDEX',@level2name=N'IX_PurchaseOrderDetail_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Individual products associated with a specific purchase order. See PurchaseOrderHeader.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'TRIGGER',N'iPurchaseOrderDetail'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AFTER INSERT trigger that inserts a row in the TransactionHistory table and updates the PurchaseOrderHeader.SubTotal column.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'TRIGGER',@level2name=N'iPurchaseOrderDetail'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'TRIGGER',N'uPurchaseOrderDetail'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AFTER UPDATE trigger that inserts a row in the TransactionHistory table, updates ModifiedDate in PurchaseOrderDetail and updates the PurchaseOrderHeader.SubTotal column.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'TRIGGER',@level2name=N'uPurchaseOrderDetail'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'CONSTRAINT',N'FK_PurchaseOrderDetail_Product_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'CONSTRAINT',@level2name=N'FK_PurchaseOrderDetail_Product_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'CONSTRAINT',N'FK_PurchaseOrderDetail_PurchaseOrderHeader_PurchaseOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing PurchaseOrderHeader.PurchaseOrderID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'CONSTRAINT',@level2name=N'FK_PurchaseOrderDetail_PurchaseOrderHeader_PurchaseOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'CONSTRAINT',N'CK_PurchaseOrderDetail_OrderQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [OrderQty] > (0)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'CONSTRAINT',@level2name=N'CK_PurchaseOrderDetail_OrderQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'CONSTRAINT',N'CK_PurchaseOrderDetail_ReceivedQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ReceivedQty] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'CONSTRAINT',@level2name=N'CK_PurchaseOrderDetail_ReceivedQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'CONSTRAINT',N'CK_PurchaseOrderDetail_RejectedQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [RejectedQty] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'CONSTRAINT',@level2name=N'CK_PurchaseOrderDetail_RejectedQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderDetail', N'CONSTRAINT',N'CK_PurchaseOrderDetail_UnitPrice'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [UnitPrice] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderDetail', @level2type=N'CONSTRAINT',@level2name=N'CK_PurchaseOrderDetail_UnitPrice'
GO
