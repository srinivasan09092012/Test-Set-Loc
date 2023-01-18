/****** Object:  Table [Sales].[SalesOrderHeaderSalesReason]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[SalesOrderHeaderSalesReason]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[SalesOrderHeaderSalesReason](
	[SalesOrderID] [int] NOT NULL,
	[SalesReasonID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesOrderHeaderSalesReason_SalesOrderID_SalesReasonID] PRIMARY KEY CLUSTERED 
(
	[SalesOrderID] ASC,
	[SalesReasonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesOrderHeaderSalesReason_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesOrderHeaderSalesReason] ADD  CONSTRAINT [DF_SalesOrderHeaderSalesReason_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeaderSalesReason_SalesOrderHeader_SalesOrderID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeaderSalesReason]'))
ALTER TABLE [Sales].[SalesOrderHeaderSalesReason]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeaderSalesReason_SalesOrderHeader_SalesOrderID] FOREIGN KEY([SalesOrderID])
REFERENCES [Sales].[SalesOrderHeader] ([SalesOrderID])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeaderSalesReason_SalesOrderHeader_SalesOrderID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeaderSalesReason]'))
ALTER TABLE [Sales].[SalesOrderHeaderSalesReason] CHECK CONSTRAINT [FK_SalesOrderHeaderSalesReason_SalesOrderHeader_SalesOrderID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeaderSalesReason_SalesReason_SalesReasonID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeaderSalesReason]'))
ALTER TABLE [Sales].[SalesOrderHeaderSalesReason]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeaderSalesReason_SalesReason_SalesReasonID] FOREIGN KEY([SalesReasonID])
REFERENCES [Sales].[SalesReason] ([SalesReasonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesOrderHeaderSalesReason_SalesReason_SalesReasonID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesOrderHeaderSalesReason]'))
ALTER TABLE [Sales].[SalesOrderHeaderSalesReason] CHECK CONSTRAINT [FK_SalesOrderHeaderSalesReason_SalesReason_SalesReasonID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeaderSalesReason', N'COLUMN',N'SalesOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to SalesOrderHeader.SalesOrderID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeaderSalesReason', @level2type=N'COLUMN',@level2name=N'SalesOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeaderSalesReason', N'COLUMN',N'SalesReasonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to SalesReason.SalesReasonID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeaderSalesReason', @level2type=N'COLUMN',@level2name=N'SalesReasonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeaderSalesReason', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeaderSalesReason', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeaderSalesReason', N'CONSTRAINT',N'DF_SalesOrderHeaderSalesReason_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeaderSalesReason', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesOrderHeaderSalesReason_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeaderSalesReason', N'CONSTRAINT',N'PK_SalesOrderHeaderSalesReason_SalesOrderID_SalesReasonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeaderSalesReason', @level2type=N'CONSTRAINT',@level2name=N'PK_SalesOrderHeaderSalesReason_SalesOrderID_SalesReasonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeaderSalesReason', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cross-reference table mapping sales orders to sales reason codes.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeaderSalesReason'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeaderSalesReason', N'CONSTRAINT',N'FK_SalesOrderHeaderSalesReason_SalesOrderHeader_SalesOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SalesOrderHeader.SalesOrderID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeaderSalesReason', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesOrderHeaderSalesReason_SalesOrderHeader_SalesOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesOrderHeaderSalesReason', N'CONSTRAINT',N'FK_SalesOrderHeaderSalesReason_SalesReason_SalesReasonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SalesReason.SalesReasonID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrderHeaderSalesReason', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesOrderHeaderSalesReason_SalesReason_SalesReasonID'
GO
