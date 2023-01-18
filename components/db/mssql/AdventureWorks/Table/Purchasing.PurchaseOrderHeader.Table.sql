/****** Object:  Table [Purchasing].[PurchaseOrderHeader]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]') AND type in (N'U'))
BEGIN
CREATE TABLE [Purchasing].[PurchaseOrderHeader](
	[PurchaseOrderID] [int] IDENTITY(1,1) NOT NULL,
	[RevisionNumber] [tinyint] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[VendorID] [int] NOT NULL,
	[ShipMethodID] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[ShipDate] [datetime] NULL,
	[SubTotal] [money] NOT NULL,
	[TaxAmt] [money] NOT NULL,
	[Freight] [money] NOT NULL,
	[TotalDue]  AS (isnull(([SubTotal]+[TaxAmt])+[Freight],(0))) PERSISTED NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PurchaseOrderHeader_PurchaseOrderID] PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [IX_PurchaseOrderHeader_EmployeeID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]') AND name = N'IX_PurchaseOrderHeader_EmployeeID')
CREATE NONCLUSTERED INDEX [IX_PurchaseOrderHeader_EmployeeID] ON [Purchasing].[PurchaseOrderHeader]
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseOrderHeader_VendorID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]') AND name = N'IX_PurchaseOrderHeader_VendorID')
CREATE NONCLUSTERED INDEX [IX_PurchaseOrderHeader_VendorID] ON [Purchasing].[PurchaseOrderHeader]
(
	[VendorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_PurchaseOrderHeader_RevisionNumber]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[PurchaseOrderHeader] ADD  CONSTRAINT [DF_PurchaseOrderHeader_RevisionNumber]  DEFAULT ((0)) FOR [RevisionNumber]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_PurchaseOrderHeader_Status]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[PurchaseOrderHeader] ADD  CONSTRAINT [DF_PurchaseOrderHeader_Status]  DEFAULT ((1)) FOR [Status]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_PurchaseOrderHeader_OrderDate]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[PurchaseOrderHeader] ADD  CONSTRAINT [DF_PurchaseOrderHeader_OrderDate]  DEFAULT (getdate()) FOR [OrderDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_PurchaseOrderHeader_SubTotal]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[PurchaseOrderHeader] ADD  CONSTRAINT [DF_PurchaseOrderHeader_SubTotal]  DEFAULT ((0.00)) FOR [SubTotal]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_PurchaseOrderHeader_TaxAmt]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[PurchaseOrderHeader] ADD  CONSTRAINT [DF_PurchaseOrderHeader_TaxAmt]  DEFAULT ((0.00)) FOR [TaxAmt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_PurchaseOrderHeader_Freight]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[PurchaseOrderHeader] ADD  CONSTRAINT [DF_PurchaseOrderHeader_Freight]  DEFAULT ((0.00)) FOR [Freight]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_PurchaseOrderHeader_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[PurchaseOrderHeader] ADD  CONSTRAINT [DF_PurchaseOrderHeader_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_PurchaseOrderHeader_Employee_EmployeeID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderHeader_Employee_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [HumanResources].[Employee] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_PurchaseOrderHeader_Employee_EmployeeID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader] CHECK CONSTRAINT [FK_PurchaseOrderHeader_Employee_EmployeeID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_PurchaseOrderHeader_ShipMethod_ShipMethodID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderHeader_ShipMethod_ShipMethodID] FOREIGN KEY([ShipMethodID])
REFERENCES [Purchasing].[ShipMethod] ([ShipMethodID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_PurchaseOrderHeader_ShipMethod_ShipMethodID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader] CHECK CONSTRAINT [FK_PurchaseOrderHeader_ShipMethod_ShipMethodID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_PurchaseOrderHeader_Vendor_VendorID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderHeader_Vendor_VendorID] FOREIGN KEY([VendorID])
REFERENCES [Purchasing].[Vendor] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_PurchaseOrderHeader_Vendor_VendorID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader] CHECK CONSTRAINT [FK_PurchaseOrderHeader_Vendor_VendorID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderHeader_Freight]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader]  WITH CHECK ADD  CONSTRAINT [CK_PurchaseOrderHeader_Freight] CHECK  (([Freight]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderHeader_Freight]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader] CHECK CONSTRAINT [CK_PurchaseOrderHeader_Freight]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderHeader_ShipDate]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader]  WITH CHECK ADD  CONSTRAINT [CK_PurchaseOrderHeader_ShipDate] CHECK  (([ShipDate]>=[OrderDate] OR [ShipDate] IS NULL))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderHeader_ShipDate]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader] CHECK CONSTRAINT [CK_PurchaseOrderHeader_ShipDate]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderHeader_Status]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader]  WITH CHECK ADD  CONSTRAINT [CK_PurchaseOrderHeader_Status] CHECK  (([Status]>=(1) AND [Status]<=(4)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderHeader_Status]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader] CHECK CONSTRAINT [CK_PurchaseOrderHeader_Status]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderHeader_SubTotal]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader]  WITH CHECK ADD  CONSTRAINT [CK_PurchaseOrderHeader_SubTotal] CHECK  (([SubTotal]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderHeader_SubTotal]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader] CHECK CONSTRAINT [CK_PurchaseOrderHeader_SubTotal]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderHeader_TaxAmt]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader]  WITH CHECK ADD  CONSTRAINT [CK_PurchaseOrderHeader_TaxAmt] CHECK  (([TaxAmt]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_PurchaseOrderHeader_TaxAmt]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[PurchaseOrderHeader]'))
ALTER TABLE [Purchasing].[PurchaseOrderHeader] CHECK CONSTRAINT [CK_PurchaseOrderHeader_TaxAmt]
GO
/****** Object:  Trigger [Purchasing].[uPurchaseOrderHeader]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[Purchasing].[uPurchaseOrderHeader]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [Purchasing].[uPurchaseOrderHeader] ON [Purchasing].[PurchaseOrderHeader] 
AFTER UPDATE AS 
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
            UPDATE [Purchasing].[PurchaseOrderHeader]
            SET [Purchasing].[PurchaseOrderHeader].[RevisionNumber] = 
                [Purchasing].[PurchaseOrderHeader].[RevisionNumber] + 1
            WHERE [Purchasing].[PurchaseOrderHeader].[PurchaseOrderID] IN 
                (SELECT inserted.[PurchaseOrderID] FROM inserted);
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
ALTER TABLE [Purchasing].[PurchaseOrderHeader] ENABLE TRIGGER [uPurchaseOrderHeader]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'PurchaseOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'PurchaseOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'RevisionNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Incremental number to track changes to the purchase order over time.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'RevisionNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'DF_PurchaseOrderHeader_RevisionNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_PurchaseOrderHeader_RevisionNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'Status'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Order current status. 1 = Pending; 2 = Approved; 3 = Rejected; 4 = Complete' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'Status'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'DF_PurchaseOrderHeader_Status'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 1' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_PurchaseOrderHeader_Status'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'EmployeeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee who created the purchase order. Foreign key to Employee.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'VendorID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vendor with whom the purchase order is placed. Foreign key to Vendor.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'VendorID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'ShipMethodID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Shipping method. Foreign key to ShipMethod.ShipMethodID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'ShipMethodID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'OrderDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Purchase order creation date.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'OrderDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'DF_PurchaseOrderHeader_OrderDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_PurchaseOrderHeader_OrderDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'ShipDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estimated shipment date from the vendor.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'ShipDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'SubTotal'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Purchase order subtotal. Computed as SUM(PurchaseOrderDetail.LineTotal)for the appropriate PurchaseOrderID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'SubTotal'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'DF_PurchaseOrderHeader_SubTotal'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_PurchaseOrderHeader_SubTotal'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'TaxAmt'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tax amount.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'TaxAmt'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'DF_PurchaseOrderHeader_TaxAmt'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_PurchaseOrderHeader_TaxAmt'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'Freight'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Shipping cost.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'Freight'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'DF_PurchaseOrderHeader_Freight'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_PurchaseOrderHeader_Freight'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'TotalDue'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Total due to vendor. Computed as Subtotal + TaxAmt + Freight.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'TotalDue'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'DF_PurchaseOrderHeader_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'DF_PurchaseOrderHeader_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'PK_PurchaseOrderHeader_PurchaseOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'PK_PurchaseOrderHeader_PurchaseOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'INDEX',N'IX_PurchaseOrderHeader_EmployeeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'INDEX',@level2name=N'IX_PurchaseOrderHeader_EmployeeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'INDEX',N'IX_PurchaseOrderHeader_VendorID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'INDEX',@level2name=N'IX_PurchaseOrderHeader_VendorID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'General purchase order information. See PurchaseOrderDetail.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'TRIGGER',N'uPurchaseOrderHeader'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AFTER UPDATE trigger that updates the RevisionNumber and ModifiedDate columns in the PurchaseOrderHeader table.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'TRIGGER',@level2name=N'uPurchaseOrderHeader'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'FK_PurchaseOrderHeader_Employee_EmployeeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Employee.EmployeeID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'FK_PurchaseOrderHeader_Employee_EmployeeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'FK_PurchaseOrderHeader_ShipMethod_ShipMethodID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing ShipMethod.ShipMethodID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'FK_PurchaseOrderHeader_ShipMethod_ShipMethodID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'FK_PurchaseOrderHeader_Vendor_VendorID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Vendor.VendorID.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'FK_PurchaseOrderHeader_Vendor_VendorID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'CK_PurchaseOrderHeader_Freight'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Freight] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'CK_PurchaseOrderHeader_Freight'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'CK_PurchaseOrderHeader_ShipDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ShipDate] >= [OrderDate] OR [ShipDate] IS NULL' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'CK_PurchaseOrderHeader_ShipDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'CK_PurchaseOrderHeader_Status'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Status] BETWEEN (1) AND (4)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'CK_PurchaseOrderHeader_Status'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'CK_PurchaseOrderHeader_SubTotal'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SubTotal] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'CK_PurchaseOrderHeader_SubTotal'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'PurchaseOrderHeader', N'CONSTRAINT',N'CK_PurchaseOrderHeader_TaxAmt'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [TaxAmt] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'PurchaseOrderHeader', @level2type=N'CONSTRAINT',@level2name=N'CK_PurchaseOrderHeader_TaxAmt'
GO
