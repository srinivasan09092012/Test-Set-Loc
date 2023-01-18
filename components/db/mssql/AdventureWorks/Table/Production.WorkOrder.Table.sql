/****** Object:  Table [Production].[WorkOrder]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[WorkOrder]') AND type in (N'U'))
BEGIN
CREATE TABLE [Production].[WorkOrder](
	[WorkOrderID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[OrderQty] [int] NOT NULL,
	[StockedQty]  AS (isnull([OrderQty]-[ScrappedQty],(0))),
	[ScrappedQty] [smallint] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[DueDate] [datetime] NOT NULL,
	[ScrapReasonID] [smallint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_WorkOrder_WorkOrderID] PRIMARY KEY CLUSTERED 
(
	[WorkOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [IX_WorkOrder_ProductID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[WorkOrder]') AND name = N'IX_WorkOrder_ProductID')
CREATE NONCLUSTERED INDEX [IX_WorkOrder_ProductID] ON [Production].[WorkOrder]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkOrder_ScrapReasonID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[WorkOrder]') AND name = N'IX_WorkOrder_ScrapReasonID')
CREATE NONCLUSTERED INDEX [IX_WorkOrder_ScrapReasonID] ON [Production].[WorkOrder]
(
	[ScrapReasonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_WorkOrder_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[WorkOrder] ADD  CONSTRAINT [DF_WorkOrder_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_WorkOrder_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrder]'))
ALTER TABLE [Production].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrder_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [Production].[Product] ([ProductID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_WorkOrder_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrder]'))
ALTER TABLE [Production].[WorkOrder] CHECK CONSTRAINT [FK_WorkOrder_Product_ProductID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_WorkOrder_ScrapReason_ScrapReasonID]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrder]'))
ALTER TABLE [Production].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrder_ScrapReason_ScrapReasonID] FOREIGN KEY([ScrapReasonID])
REFERENCES [Production].[ScrapReason] ([ScrapReasonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_WorkOrder_ScrapReason_ScrapReasonID]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrder]'))
ALTER TABLE [Production].[WorkOrder] CHECK CONSTRAINT [FK_WorkOrder_ScrapReason_ScrapReasonID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrder_EndDate]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrder]'))
ALTER TABLE [Production].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [CK_WorkOrder_EndDate] CHECK  (([EndDate]>=[StartDate] OR [EndDate] IS NULL))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrder_EndDate]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrder]'))
ALTER TABLE [Production].[WorkOrder] CHECK CONSTRAINT [CK_WorkOrder_EndDate]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrder_OrderQty]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrder]'))
ALTER TABLE [Production].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [CK_WorkOrder_OrderQty] CHECK  (([OrderQty]>(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrder_OrderQty]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrder]'))
ALTER TABLE [Production].[WorkOrder] CHECK CONSTRAINT [CK_WorkOrder_OrderQty]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrder_ScrappedQty]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrder]'))
ALTER TABLE [Production].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [CK_WorkOrder_ScrappedQty] CHECK  (([ScrappedQty]>=(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrder_ScrappedQty]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrder]'))
ALTER TABLE [Production].[WorkOrder] CHECK CONSTRAINT [CK_WorkOrder_ScrappedQty]
GO
/****** Object:  Trigger [Production].[iWorkOrder]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[Production].[iWorkOrder]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [Production].[iWorkOrder] ON [Production].[WorkOrder] 
AFTER INSERT AS 
BEGIN
    DECLARE @Count int;

    SET @Count = @@ROWCOUNT;
    IF @Count = 0 
        RETURN;

    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO [Production].[TransactionHistory](
            [ProductID]
            ,[ReferenceOrderID]
            ,[TransactionType]
            ,[TransactionDate]
            ,[Quantity]
            ,[ActualCost])
        SELECT 
            inserted.[ProductID]
            ,inserted.[WorkOrderID]
            ,''W''
            ,GETDATE()
            ,inserted.[OrderQty]
            ,0
        FROM inserted;
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
ALTER TABLE [Production].[WorkOrder] ENABLE TRIGGER [iWorkOrder]
GO
/****** Object:  Trigger [Production].[uWorkOrder]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[Production].[uWorkOrder]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [Production].[uWorkOrder] ON [Production].[WorkOrder] 
AFTER UPDATE AS 
BEGIN
    DECLARE @Count int;

    SET @Count = @@ROWCOUNT;
    IF @Count = 0 
        RETURN;

    SET NOCOUNT ON;

    BEGIN TRY
        IF UPDATE([ProductID]) OR UPDATE([OrderQty])
        BEGIN
            INSERT INTO [Production].[TransactionHistory](
                [ProductID]
                ,[ReferenceOrderID]
                ,[TransactionType]
                ,[TransactionDate]
                ,[Quantity])
            SELECT 
                inserted.[ProductID]
                ,inserted.[WorkOrderID]
                ,''W''
                ,GETDATE()
                ,inserted.[OrderQty]
            FROM inserted;
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
ALTER TABLE [Production].[WorkOrder] ENABLE TRIGGER [uWorkOrder]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'COLUMN',N'WorkOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for WorkOrder records.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'COLUMN',@level2name=N'WorkOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'COLUMN',N'ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product identification number. Foreign key to Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'COLUMN',N'OrderQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product quantity to build.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'COLUMN',@level2name=N'OrderQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'COLUMN',N'StockedQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Quantity built and put in inventory.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'COLUMN',@level2name=N'StockedQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'COLUMN',N'ScrappedQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Quantity that failed inspection.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'COLUMN',@level2name=N'ScrappedQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'COLUMN',N'StartDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Work order start date.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'COLUMN',@level2name=N'StartDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'COLUMN',N'EndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Work order end date.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'COLUMN',@level2name=N'EndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'COLUMN',N'DueDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Work order due date.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'COLUMN',@level2name=N'DueDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'COLUMN',N'ScrapReasonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Reason for inspection failure.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'COLUMN',@level2name=N'ScrapReasonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'CONSTRAINT',N'DF_WorkOrder_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'CONSTRAINT',@level2name=N'DF_WorkOrder_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'CONSTRAINT',N'PK_WorkOrder_WorkOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'CONSTRAINT',@level2name=N'PK_WorkOrder_WorkOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'INDEX',N'IX_WorkOrder_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'INDEX',@level2name=N'IX_WorkOrder_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'INDEX',N'IX_WorkOrder_ScrapReasonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'INDEX',@level2name=N'IX_WorkOrder_ScrapReasonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Manufacturing work orders.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'TRIGGER',N'iWorkOrder'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AFTER INSERT trigger that inserts a row in the TransactionHistory table.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'TRIGGER',@level2name=N'iWorkOrder'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'TRIGGER',N'uWorkOrder'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AFTER UPDATE trigger that inserts a row in the TransactionHistory table, updates ModifiedDate in the WorkOrder table.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'TRIGGER',@level2name=N'uWorkOrder'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'CONSTRAINT',N'FK_WorkOrder_Product_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'CONSTRAINT',@level2name=N'FK_WorkOrder_Product_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'CONSTRAINT',N'FK_WorkOrder_ScrapReason_ScrapReasonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing ScrapReason.ScrapReasonID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'CONSTRAINT',@level2name=N'FK_WorkOrder_ScrapReason_ScrapReasonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'CONSTRAINT',N'CK_WorkOrder_EndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [EndDate] >= [StartDate] OR [EndDate] IS NULL' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'CONSTRAINT',@level2name=N'CK_WorkOrder_EndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'CONSTRAINT',N'CK_WorkOrder_OrderQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [OrderQty] > (0)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'CONSTRAINT',@level2name=N'CK_WorkOrder_OrderQty'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrder', N'CONSTRAINT',N'CK_WorkOrder_ScrappedQty'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ScrappedQty] >= (0)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrder', @level2type=N'CONSTRAINT',@level2name=N'CK_WorkOrder_ScrappedQty'
GO
