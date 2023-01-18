/****** Object:  Table [Purchasing].[ShipMethod]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[ShipMethod]') AND type in (N'U'))
BEGIN
CREATE TABLE [Purchasing].[ShipMethod](
	[ShipMethodID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[ShipBase] [money] NOT NULL,
	[ShipRate] [money] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ShipMethod_ShipMethodID] PRIMARY KEY CLUSTERED 
(
	[ShipMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_ShipMethod_Name]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Purchasing].[ShipMethod]') AND name = N'AK_ShipMethod_Name')
CREATE UNIQUE NONCLUSTERED INDEX [AK_ShipMethod_Name] ON [Purchasing].[ShipMethod]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AK_ShipMethod_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Purchasing].[ShipMethod]') AND name = N'AK_ShipMethod_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_ShipMethod_rowguid] ON [Purchasing].[ShipMethod]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_ShipMethod_ShipBase]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[ShipMethod] ADD  CONSTRAINT [DF_ShipMethod_ShipBase]  DEFAULT ((0.00)) FOR [ShipBase]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_ShipMethod_ShipRate]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[ShipMethod] ADD  CONSTRAINT [DF_ShipMethod_ShipRate]  DEFAULT ((0.00)) FOR [ShipRate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_ShipMethod_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[ShipMethod] ADD  CONSTRAINT [DF_ShipMethod_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Purchasing].[DF_ShipMethod_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Purchasing].[ShipMethod] ADD  CONSTRAINT [DF_ShipMethod_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ShipMethod_ShipBase]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ShipMethod]'))
ALTER TABLE [Purchasing].[ShipMethod]  WITH CHECK ADD  CONSTRAINT [CK_ShipMethod_ShipBase] CHECK  (([ShipBase]>(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ShipMethod_ShipBase]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ShipMethod]'))
ALTER TABLE [Purchasing].[ShipMethod] CHECK CONSTRAINT [CK_ShipMethod_ShipBase]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ShipMethod_ShipRate]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ShipMethod]'))
ALTER TABLE [Purchasing].[ShipMethod]  WITH CHECK ADD  CONSTRAINT [CK_ShipMethod_ShipRate] CHECK  (([ShipRate]>(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Purchasing].[CK_ShipMethod_ShipRate]') AND parent_object_id = OBJECT_ID(N'[Purchasing].[ShipMethod]'))
ALTER TABLE [Purchasing].[ShipMethod] CHECK CONSTRAINT [CK_ShipMethod_ShipRate]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'COLUMN',N'ShipMethodID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for ShipMethod records.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'COLUMN',@level2name=N'ShipMethodID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'COLUMN',N'Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Shipping company name.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'COLUMN',@level2name=N'Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'COLUMN',N'ShipBase'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Minimum shipping charge.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'COLUMN',@level2name=N'ShipBase'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'CONSTRAINT',N'DF_ShipMethod_ShipBase'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'CONSTRAINT',@level2name=N'DF_ShipMethod_ShipBase'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'COLUMN',N'ShipRate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Shipping charge per pound.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'COLUMN',@level2name=N'ShipRate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'CONSTRAINT',N'DF_ShipMethod_ShipRate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'CONSTRAINT',@level2name=N'DF_ShipMethod_ShipRate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'CONSTRAINT',N'DF_ShipMethod_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'CONSTRAINT',@level2name=N'DF_ShipMethod_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'CONSTRAINT',N'DF_ShipMethod_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'CONSTRAINT',@level2name=N'DF_ShipMethod_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'CONSTRAINT',N'PK_ShipMethod_ShipMethodID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'CONSTRAINT',@level2name=N'PK_ShipMethod_ShipMethodID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'INDEX',N'AK_ShipMethod_Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'INDEX',@level2name=N'AK_ShipMethod_Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'INDEX',N'AK_ShipMethod_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'INDEX',@level2name=N'AK_ShipMethod_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Shipping company lookup table.' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'CONSTRAINT',N'CK_ShipMethod_ShipBase'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ShipBase] > (0.00)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'CONSTRAINT',@level2name=N'CK_ShipMethod_ShipBase'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', N'TABLE',N'ShipMethod', N'CONSTRAINT',N'CK_ShipMethod_ShipRate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ShipRate] > (0.00)' , @level0type=N'SCHEMA',@level0name=N'Purchasing', @level1type=N'TABLE',@level1name=N'ShipMethod', @level2type=N'CONSTRAINT',@level2name=N'CK_ShipMethod_ShipRate'
GO
