/****** Object:  Table [Sales].[SalesOrderHeader]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[SalesOrderHeader](
	[SalesOrderID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[RevisionNumber] [tinyint] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[ShipDate] [datetime] NULL,
	[Status] [tinyint] NOT NULL,
	[OnlineOrderFlag] [dbo].[Flag] NOT NULL,
	[SalesOrderNumber]  AS (isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***')),
	[PurchaseOrderNumber] [dbo].[OrderNumber] NULL,
	[AccountNumber] [dbo].[AccountNumber] NULL,
	[CustomerID] [int] NOT NULL,
	[SalesPersonID] [int] NULL,
	[TerritoryID] [int] NULL,
	[BillToAddressID] [int] NOT NULL,
	[ShipToAddressID] [int] NOT NULL,
	[ShipMethodID] [int] NOT NULL,
	[CreditCardID] [int] NULL,
	[CreditCardApprovalCode] [varchar](15) NULL,
	[CurrencyRateID] [int] NULL,
	[SubTotal] [money] NOT NULL,
	[TaxAmt] [money] NOT NULL,
	[Freight] [money] NOT NULL,
	[TotalDue]  AS (isnull(([SubTotal]+[TaxAmt])+[Freight],(0))),
	[Comment] [nvarchar](128) NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesOrderHeader_SalesOrderID] PRIMARY KEY CLUSTERED 
(
	[SalesOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [AK_SalesOrderHeader_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]') AND name = N'AK_SalesOrderHeader_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_SalesOrderHeader_rowguid] ON [Sales].[SalesOrderHeader]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [AK_SalesOrderHeader_SalesOrderNumber]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]') AND name = N'AK_SalesOrderHeader_SalesOrderNumber')
CREATE UNIQUE NONCLUSTERED INDEX [AK_SalesOrderHeader_SalesOrderNumber] ON [Sales].[SalesOrderHeader]
(
	[SalesOrderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SalesOrderHeader_CustomerID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_CustomerID')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_CustomerID] ON [Sales].[SalesOrderHeader]
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SalesOrderHeader_SalesPersonID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_SalesPersonID')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_SalesPersonID] ON [Sales].[SalesOrderHeader]
(
	[SalesPersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesOrderHeader_RevisionNumber]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesOrderHeader] ADD  CONSTRAINT [DF_SalesOrderHeader_RevisionNumber]  DEFAULT ((0)) FOR [RevisionNumber]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesOrderHeader_OrderDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesOrderHeader] ADD  CONSTRAINT [DF_SalesOrderHeader_OrderDate]  DEFAULT (getdate()) FOR [OrderDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesOrderHeader_Status]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesOrderHeader] ADD  CONSTRAINT [DF_SalesOrderHeader_Status]  DEFAULT ((1)) FOR [Status]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesOrderHeader_OnlineOrderFlag]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesOrderHeader] ADD  CONSTRAINT [DF_SalesOrderHeader_OnlineOrderFlag]  DEFAULT ((1)) FOR [OnlineOrderFlag]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesOrderHeader_SubTotal]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesOrderHeader] ADD  CONSTRAINT [DF_SalesOrderHeader_SubTotal]  DEFAULT ((0.00)) FOR [SubTotal]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesOrderHeader_TaxAmt]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesOrderHeader] ADD  CONSTRAINT [DF_SalesOrderHeader_TaxAmt]  DEFAULT ((0.00)) FOR [TaxAmt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesOrderHeader_Freight]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesOrderHeader] ADD  CONSTRAINT [DF_SalesOrderHeader_Freight]  DEFAULT ((0.00)) FOR [Freight]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesOrderHeader_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesOrderHeader] ADD  CONSTRAINT [DF_SalesOrderHeader_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesOrderHeader_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesOrderHeader] ADD  CONSTRAINT [DF_SalesOrderHeader_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_Address_BillToAddressID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeader_Address_BillToAddressID] FOREIGN KEY([BillToAddressID])
REFERENCES [Person].[Address] ([AddressID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_Address_BillToAddressID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [FK_SalesOrderHeader_Address_BillToAddressID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_Address_ShipToAddressID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeader_Address_ShipToAddressID] FOREIGN KEY([ShipToAddressID])
REFERENCES [Person].[Address] ([AddressID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_Address_ShipToAddressID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [FK_SalesOrderHeader_Address_ShipToAddressID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_CreditCard_CreditCardID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeader_CreditCard_CreditCardID] FOREIGN KEY([CreditCardID])
REFERENCES [Sales].[CreditCard] ([CreditCardID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_CreditCard_CreditCardID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [FK_SalesOrderHeader_CreditCard_CreditCardID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_CurrencyRate_CurrencyRateID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeader_CurrencyRate_CurrencyRateID] FOREIGN KEY([CurrencyRateID])
REFERENCES [Sales].[CurrencyRate] ([CurrencyRateID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_CurrencyRate_CurrencyRateID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [FK_SalesOrderHeader_CurrencyRate_CurrencyRateID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_Customer_CustomerID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeader_Customer_CustomerID] FOREIGN KEY([CustomerID])
REFERENCES [Sales].[Customer] ([CustomerID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_Customer_CustomerID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [FK_SalesOrderHeader_Customer_CustomerID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_SalesPerson_SalesPersonID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeader_SalesPerson_SalesPersonID] FOREIGN KEY([SalesPersonID])
REFERENCES [Sales].[SalesPerson] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_SalesPerson_SalesPersonID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [FK_SalesOrderHeader_SalesPerson_SalesPersonID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_SalesTerritory_TerritoryID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeader_SalesTerritory_TerritoryID] FOREIGN KEY([TerritoryID])
REFERENCES [Sales].[SalesTerritory] ([TerritoryID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_SalesTerritory_TerritoryID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [FK_SalesOrderHeader_SalesTerritory_TerritoryID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_ShipMethod_ShipMethodID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeader_ShipMethod_ShipMethodID] FOREIGN KEY([ShipMethodID])
REFERENCES [Purchasing].[ShipMethod] ([ShipMethodID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeader_ShipMethod_ShipMethodID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [FK_SalesOrderHeader_ShipMethod_ShipMethodID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_DueDate]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [CK_SalesOrderHeader_DueDate] CHECK  (([DueDate]>=[OrderDate]))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_DueDate]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [CK_SalesOrderHeader_DueDate]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_Freight]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [CK_SalesOrderHeader_Freight] CHECK  (([Freight]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_Freight]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [CK_SalesOrderHeader_Freight]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_ShipDate]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [CK_SalesOrderHeader_ShipDate] CHECK  (([ShipDate]>=[OrderDate] OR [ShipDate] IS NULL))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_ShipDate]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [CK_SalesOrderHeader_ShipDate]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_Status]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [CK_SalesOrderHeader_Status] CHECK  (([Status]>=(0) AND [Status]<=(8)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_Status]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [CK_SalesOrderHeader_Status]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_SubTotal]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [CK_SalesOrderHeader_SubTotal] CHECK  (([SubTotal]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_SubTotal]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [CK_SalesOrderHeader_SubTotal]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_TaxAmt]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [CK_SalesOrderHeader_TaxAmt] CHECK  (([TaxAmt]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesOrderHeader_TaxAmt]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeader]'))
ALTER TABLE [Sales].[SalesOrderHeader] CHECK CONSTRAINT [CK_SalesOrderHeader_TaxAmt]
GO
/****** Object:  Trigger [Sales].[uSalesOrderHeader]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[Sales].[uSalesOrderHeader]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [Sales].[uSalesOrderHeader] ON [Sales].[SalesOrderHeader] 
AFTER UPDATE NOT FOR REPLICATION AS 
BEGIN
    DECLARE @Count int;

    SET @Count = @@ROWCOUNT;
    IF @Count = 0 
        RETURN;

    SET NOCOUNT ON;

    BEGIN TRY
        -- Update RevisionNumber for modification of any field EXCEPT the Status.
        IF NOT UPDATE([Status])
        BEGIN
            UPDATE [Sales].[SalesOrderHeader]
            SET [Sales].[SalesOrderHeader].[RevisionNumber] = 
                [Sales].[SalesOrderHeader].[RevisionNumber] + 1
            WHERE [Sales].[SalesOrderHeader].[SalesOrderID] IN 
                (SELECT inserted.[SalesOrderID] FROM inserted);
        END;

        -- Update the SalesPerson SalesYTD when SubTotal is updated
        IF UPDATE([SubTotal])
        BEGIN
            DECLARE @StartDate datetime,
                    @EndDate datetime

            SET @StartDate = [dbo].[ufnGetAccountingStartDate]();
            SET @EndDate = [dbo].[ufnGetAccountingEndDate]();

            UPDATE [Sales].[SalesPerson]
            SET [Sales].[SalesPerson].[SalesYTD] = 
                (SELECT SUM([Sales].[SalesOrderHeader].[SubTotal])
                FROM [Sales].[SalesOrderHeader] 
                WHERE [Sales].[SalesPerson].[BusinessEntityID] = [Sales].[SalesOrderHeader].[SalesPersonID]
                    AND ([Sales].[SalesOrderHeader].[Status] = 5) -- Shipped
                    AND [Sales].[SalesOrderHeader].[OrderDate] BETWEEN @StartDate AND @EndDate)
            WHERE [Sales].[SalesPerson].[BusinessEntityID] 
                IN (SELECT DISTINCT inserted.[SalesPersonID] FROM inserted 
                    WHERE inserted.[OrderDate] BETWEEN @StartDate AND @EndDate);

            -- Update the SalesTerritory SalesYTD when SubTotal is updated
            UPDATE [Sales].[SalesTerritory]
            SET [Sales].[SalesTerritory].[SalesYTD] = 
                (SELECT SUM([Sales].[SalesOrderHeader].[SubTotal])
                FROM [Sales].[SalesOrderHeader] 
                WHERE [Sales].[SalesTerritory].[TerritoryID] = [Sales].[SalesOrderHeader].[TerritoryID]
                    AND ([Sales].[SalesOrderHeader].[Status] = 5) -- Shipped
                    AND [Sales].[SalesOrderHeader].[OrderDate] BETWEEN @StartDate AND @EndDate)
            WHERE [Sales].[SalesTerritory].[TerritoryID] 
                IN (SELECT DISTINCT inserted.[TerritoryID] FROM inserted 
                    WHERE inserted.[OrderDate] BETWEEN @StartDate AND @EndDate);
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
ALTER TABLE [Sales].[SalesOrderHeader] ENABLE TRIGGER [uSalesOrderHeader]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'SalesOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'SalesOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'RevisionNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Incremental number to track changes to the sales order over time.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'RevisionNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'DF_SalesOrderHeader_RevisionNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesOrderHeader_RevisionNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'OrderDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dates the sales order was created.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'OrderDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'DF_SalesOrderHeader_OrderDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesOrderHeader_OrderDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'DueDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date the order is due to the customer.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'DueDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'ShipDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date the order was shipped to the customer.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'ShipDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'Status'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'Status'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'DF_SalesOrderHeader_Status'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 1' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesOrderHeader_Status'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'OnlineOrderFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = Order placed by sales person. 1 = Order placed online by customer.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'OnlineOrderFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'DF_SalesOrderHeader_OnlineOrderFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 1 (TRUE)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesOrderHeader_OnlineOrderFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'SalesOrderNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique sales order identification number.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'SalesOrderNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'PurchaseOrderNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer purchase order number reference. ' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'PurchaseOrderNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'AccountNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Financial accounting number reference.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'AccountNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'CustomerID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer identification number. Foreign key to Customer.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'SalesPersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales person who created the sales order. Foreign key to SalesPerson.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'SalesPersonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Territory in which the sale was made. Foreign key to SalesTerritory.SalesTerritoryID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'BillToAddressID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer billing address. Foreign key to Address.AddressID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'BillToAddressID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'ShipToAddressID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer shipping address. Foreign key to Address.AddressID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'ShipToAddressID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'ShipMethodID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Shipping method. Foreign key to ShipMethod.ShipMethodID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'ShipMethodID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'CreditCardID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Credit card identification number. Foreign key to CreditCard.CreditCardID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'CreditCardID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'CreditCardApprovalCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Approval code provided by the credit card company.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'CreditCardApprovalCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'CurrencyRateID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Currency exchange rate used. Foreign key to CurrencyRate.CurrencyRateID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'CurrencyRateID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'SubTotal'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'SubTotal'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'DF_SalesOrderHeader_SubTotal'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesOrderHeader_SubTotal'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'TaxAmt'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tax amount.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'TaxAmt'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'DF_SalesOrderHeader_TaxAmt'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesOrderHeader_TaxAmt'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'Freight'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Shipping cost.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'Freight'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'DF_SalesOrderHeader_Freight'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesOrderHeader_Freight'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'TotalDue'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Total due from customer. Computed as Subtotal + TaxAmt + Freight.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'TotalDue'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'Comment'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales representative comments.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'Comment'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'DF_SalesOrderHeader_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesOrderHeader_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'DF_SalesOrderHeader_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesOrderHeader_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'PK_SalesOrderHeader_SalesOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'PK_SalesOrderHeader_SalesOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'INDEX',N'AK_SalesOrderHeader_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'INDEX',@level2name=N'AK_SalesOrderHeader_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'INDEX',N'AK_SalesOrderHeader_SalesOrderNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'INDEX',@level2name=N'AK_SalesOrderHeader_SalesOrderNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'INDEX',N'IX_SalesOrderHeader_CustomerID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'INDEX',@level2name=N'IX_SalesOrderHeader_CustomerID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'INDEX',N'IX_SalesOrderHeader_SalesPersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'INDEX',@level2name=N'IX_SalesOrderHeader_SalesPersonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'General sales order information.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'TRIGGER',N'uSalesOrderHeader'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AFTER UPDATE trigger that updates the RevisionNumber and ModifiedDate columns in the SalesOrderHeader table.Updates the SalesYTD column in the SalesPerson and SalesTerritory tables.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'TRIGGER',@level2name=N'uSalesOrderHeader'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'FK_SalesOrderHeader_Address_BillToAddressID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Address.AddressID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesOrderHeader_Address_BillToAddressID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'FK_SalesOrderHeader_Address_ShipToAddressID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Address.AddressID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesOrderHeader_Address_ShipToAddressID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'FK_SalesOrderHeader_CreditCard_CreditCardID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing CreditCard.CreditCardID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesOrderHeader_CreditCard_CreditCardID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'FK_SalesOrderHeader_CurrencyRate_CurrencyRateID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing CurrencyRate.CurrencyRateID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesOrderHeader_CurrencyRate_CurrencyRateID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'FK_SalesOrderHeader_Customer_CustomerID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Customer.CustomerID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesOrderHeader_Customer_CustomerID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'FK_SalesOrderHeader_SalesPerson_SalesPersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SalesPerson.SalesPersonID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesOrderHeader_SalesPerson_SalesPersonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'FK_SalesOrderHeader_SalesTerritory_TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SalesTerritory.TerritoryID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesOrderHeader_SalesTerritory_TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'FK_SalesOrderHeader_ShipMethod_ShipMethodID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing ShipMethod.ShipMethodID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesOrderHeader_ShipMethod_ShipMethodID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'CK_SalesOrderHeader_DueDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [DueDate] >= [OrderDate]' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesOrderHeader_DueDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'CK_SalesOrderHeader_Freight'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Freight] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesOrderHeader_Freight'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'CK_SalesOrderHeader_ShipDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ShipDate] >= [OrderDate] OR [ShipDate] IS NULL' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesOrderHeader_ShipDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'CK_SalesOrderHeader_Status'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Status] BETWEEN (0) AND (8)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesOrderHeader_Status'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'CK_SalesOrderHeader_SubTotal'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SubTotal] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesOrderHeader_SubTotal'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeader', N'CONSTRAINT',N'CK_SalesOrderHeader_TaxAmt'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [TaxAmt] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesOrderHeader_TaxAmt'
GO
