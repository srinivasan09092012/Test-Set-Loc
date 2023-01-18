/****** Object:  Table [Purchasing].[Vendor]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[Vendor]') AND type in (N'U'))
BEGIN
CREATE TABLE [Purchasing].[Vendor](
	[BusinessEntityID] [int] NOT NULL,
	[AccountNumber] [dbo].[AccountNumber] NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[CreditRating] [tinyint] NOT NULL,
	[PreferredVendorStatus] [dbo].[Flag] NOT NULL,
	[ActiveFlag] [dbo].[Flag] NOT NULL,
	[PurchasingWebServiceURL] [nvarchar](1024) NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Vendor_BusinessEntityID] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Vendor_AccountNumber]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Purchasing].[Vendor]') AND name = N'AK_Vendor_AccountNumber')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Vendor_AccountNumber] ON [Purchasing].[Vendor]
(
	[AccountNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_Vendor_PreferredVendorStatus]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[Vendor] ADD  CONSTRAINT [DF_Vendor_PreferredVendorStatus]  DEFAULT ((1)) FOR [PreferredVendorStatus]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_Vendor_ActiveFlag]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[Vendor] ADD  CONSTRAINT [DF_Vendor_ActiveFlag]  DEFAULT ((1)) FOR [ActiveFlag]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_Vendor_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[Vendor] ADD  CONSTRAINT [DF_Vendor_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_Vendor_BusinessEntity_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[Vendor]'))
ALTER TABLE [Purchasing].[Vendor]  WITH CHECK ADD  CONSTRAINT [FK_Vendor_BusinessEntity_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Person].[BusinessEntity] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Purchasing].[FK_Vendor_BusinessEntity_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[Vendor]'))
ALTER TABLE [Purchasing].[Vendor] CHECK CONSTRAINT [FK_Vendor_BusinessEntity_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_Vendor_CreditRating]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[Vendor]'))
ALTER TABLE [Purchasing].[Vendor]  WITH CHECK ADD  CONSTRAINT [CK_Vendor_CreditRating] CHECK  (([CreditRating]>=(1) AND [CreditRating]<=(5)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_Vendor_CreditRating]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[Vendor]'))
ALTER TABLE [Purchasing].[Vendor] CHECK CONSTRAINT [CK_Vendor_CreditRating]
GO
/****** Object:  Trigger [Purchasing].[dVendor]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[Purchasing].[dVendor]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [Purchasing].[dVendor] ON [Purchasing].[Vendor] 
INSTEAD OF DELETE NOT FOR REPLICATION AS 
BEGIN
    DECLARE @Count int;

    SET @Count = @@ROWCOUNT;
    IF @Count = 0 
        RETURN;

    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @DeleteCount int;

        SELECT @DeleteCount = COUNT(*) FROM deleted;
        IF @DeleteCount > 0 
        BEGIN
            RAISERROR
                (N''Vendors cannot be deleted. They can only be marked as not active.'', -- Message
                10, -- Severity.
                1); -- State.

        -- Rollback any active or uncommittable transactions
            IF @@TRANCOUNT > 0
            BEGIN
                ROLLBACK TRANSACTION;
            END
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
ALTER TABLE [Purchasing].[Vendor] ENABLE TRIGGER [dVendor]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for Vendor records.  Foreign key to BusinessEntity.BusinessEntityID' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'COLUMN',N'AccountNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vendor account (identification) number.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'COLUMN',@level2name=N'AccountNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'COLUMN',N'Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Company name.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'COLUMN',@level2name=N'Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'COLUMN',N'CreditRating'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Superior, 2 = Excellent, 3 = Above average, 4 = Average, 5 = Below average' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'COLUMN',@level2name=N'CreditRating'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'COLUMN',N'PreferredVendorStatus'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = Do not use if another vendor is available. 1 = Preferred over other vendors supplying the same product.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'COLUMN',@level2name=N'PreferredVendorStatus'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'CONSTRAINT',N'DF_Vendor_PreferredVendorStatus'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 1 (TRUE)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'CONSTRAINT',@level2name=N'DF_Vendor_PreferredVendorStatus'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'COLUMN',N'ActiveFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = Vendor no longer used. 1 = Vendor is actively used.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'COLUMN',@level2name=N'ActiveFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'CONSTRAINT',N'DF_Vendor_ActiveFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 1 (TRUE)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'CONSTRAINT',@level2name=N'DF_Vendor_ActiveFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'COLUMN',N'PurchasingWebServiceURL'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vendor URL.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'COLUMN',@level2name=N'PurchasingWebServiceURL'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'CONSTRAINT',N'DF_Vendor_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'CONSTRAINT',@level2name=N'DF_Vendor_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'CONSTRAINT',N'PK_Vendor_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'CONSTRAINT',@level2name=N'PK_Vendor_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'INDEX',N'AK_Vendor_AccountNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'INDEX',@level2name=N'AK_Vendor_AccountNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Companies from whom Adventure Works Cycles purchases parts or other goods.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'TRIGGER',N'dVendor'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'INSTEAD OF DELETE trigger which keeps Vendors from being deleted.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'TRIGGER',@level2name=N'dVendor'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'CONSTRAINT',N'FK_Vendor_BusinessEntity_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing BusinessEntity.BusinessEntityID' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'CONSTRAINT',@level2name=N'FK_Vendor_BusinessEntity_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'Vendor', N'CONSTRAINT',N'CK_Vendor_CreditRating'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [CreditRating] BETWEEN (1) AND (5)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'Vendor', @level2type=N'CONSTRAINT',@level2name=N'CK_Vendor_CreditRating'
GO
