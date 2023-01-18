/****** Object:  Table [Sales].[SalesPersonQuotaHistory]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[SalesPersonQuotaHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[SalesPersonQuotaHistory](
	[BusinessEntityID] [int] NOT NULL,
	[QuotaDate] [datetime] NOT NULL,
	[SalesQuota] [money] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesPersonQuotaHistory_BusinessEntityID_QuotaDate] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC,
	[QuotaDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [AK_SalesPersonQuotaHistory_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SalesPersonQuotaHistory]') AND name = N'AK_SalesPersonQuotaHistory_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_SalesPersonQuotaHistory_rowguid] ON [Sales].[SalesPersonQuotaHistory]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesPersonQuotaHistory_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesPersonQuotaHistory] ADD  CONSTRAINT [DF_SalesPersonQuotaHistory_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesPersonQuotaHistory_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesPersonQuotaHistory] ADD  CONSTRAINT [DF_SalesPersonQuotaHistory_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesPersonQuotaHistory_SalesPerson_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPersonQuotaHistory]'))
ALTER TABLE [Sales].[SalesPersonQuotaHistory]  WITH CHECK ADD  CONSTRAINT [FK_SalesPersonQuotaHistory_SalesPerson_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Sales].[SalesPerson] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesPersonQuotaHistory_SalesPerson_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPersonQuotaHistory]'))
ALTER TABLE [Sales].[SalesPersonQuotaHistory] CHECK CONSTRAINT [FK_SalesPersonQuotaHistory_SalesPerson_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPersonQuotaHistory_SalesQuota]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPersonQuotaHistory]'))
ALTER TABLE [Sales].[SalesPersonQuotaHistory]  WITH CHECK ADD  CONSTRAINT [CK_SalesPersonQuotaHistory_SalesQuota] CHECK  (([SalesQuota]>(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPersonQuotaHistory_SalesQuota]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPersonQuotaHistory]'))
ALTER TABLE [Sales].[SalesPersonQuotaHistory] CHECK CONSTRAINT [CK_SalesPersonQuotaHistory_SalesQuota]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales person identification number. Foreign key to SalesPerson.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', N'COLUMN',N'QuotaDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales quota date.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory', @level2type=N'COLUMN',@level2name=N'QuotaDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', N'COLUMN',N'SalesQuota'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales quota amount.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory', @level2type=N'COLUMN',@level2name=N'SalesQuota'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', N'CONSTRAINT',N'DF_SalesPersonQuotaHistory_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesPersonQuotaHistory_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', N'CONSTRAINT',N'DF_SalesPersonQuotaHistory_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesPersonQuotaHistory_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', N'CONSTRAINT',N'PK_SalesPersonQuotaHistory_BusinessEntityID_QuotaDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory', @level2type=N'CONSTRAINT',@level2name=N'PK_SalesPersonQuotaHistory_BusinessEntityID_QuotaDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', N'INDEX',N'AK_SalesPersonQuotaHistory_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory', @level2type=N'INDEX',@level2name=N'AK_SalesPersonQuotaHistory_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales performance tracking.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', N'CONSTRAINT',N'FK_SalesPersonQuotaHistory_SalesPerson_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SalesPerson.SalesPersonID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesPersonQuotaHistory_SalesPerson_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPersonQuotaHistory', N'CONSTRAINT',N'CK_SalesPersonQuotaHistory_SalesQuota'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SalesQuota] > (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPersonQuotaHistory', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesPersonQuotaHistory_SalesQuota'
GO
