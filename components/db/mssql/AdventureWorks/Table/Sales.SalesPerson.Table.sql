/****** Object:  Table [Sales].[SalesPerson]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[SalesPerson]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[SalesPerson](
	[BusinessEntityID] [int] NOT NULL,
	[TerritoryID] [int] NULL,
	[SalesQuota] [money] NULL,
	[Bonus] [money] NOT NULL,
	[CommissionPct] [smallmoney] NOT NULL,
	[SalesYTD] [money] NOT NULL,
	[SalesLastYear] [money] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesPerson_BusinessEntityID] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [AK_SalesPerson_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Sales].[SalesPerson]') AND name = N'AK_SalesPerson_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_SalesPerson_rowguid] ON [Sales].[SalesPerson]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesPerson_Bonus]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesPerson] ADD  CONSTRAINT [DF_SalesPerson_Bonus]  DEFAULT ((0.00)) FOR [Bonus]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesPerson_CommissionPct]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesPerson] ADD  CONSTRAINT [DF_SalesPerson_CommissionPct]  DEFAULT ((0.00)) FOR [CommissionPct]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesPerson_SalesYTD]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesPerson] ADD  CONSTRAINT [DF_SalesPerson_SalesYTD]  DEFAULT ((0.00)) FOR [SalesYTD]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesPerson_SalesLastYear]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesPerson] ADD  CONSTRAINT [DF_SalesPerson_SalesLastYear]  DEFAULT ((0.00)) FOR [SalesLastYear]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesPerson_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesPerson] ADD  CONSTRAINT [DF_SalesPerson_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_SalesPerson_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[SalesPerson] ADD  CONSTRAINT [DF_SalesPerson_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesPerson_Employee_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson]  WITH CHECK ADD  CONSTRAINT [FK_SalesPerson_Employee_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [HumanResources].[Employee] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesPerson_Employee_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson] CHECK CONSTRAINT [FK_SalesPerson_Employee_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesPerson_SalesTerritory_TerritoryID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson]  WITH CHECK ADD  CONSTRAINT [FK_SalesPerson_SalesTerritory_TerritoryID] FOREIGN KEY([TerritoryID])
REFERENCES [Sales].[SalesTerritory] ([TerritoryID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_SalesPerson_SalesTerritory_TerritoryID]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson] CHECK CONSTRAINT [FK_SalesPerson_SalesTerritory_TerritoryID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPerson_Bonus]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson]  WITH CHECK ADD  CONSTRAINT [CK_SalesPerson_Bonus] CHECK  (([Bonus]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPerson_Bonus]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson] CHECK CONSTRAINT [CK_SalesPerson_Bonus]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPerson_CommissionPct]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson]  WITH CHECK ADD  CONSTRAINT [CK_SalesPerson_CommissionPct] CHECK  (([CommissionPct]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPerson_CommissionPct]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson] CHECK CONSTRAINT [CK_SalesPerson_CommissionPct]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPerson_SalesLastYear]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson]  WITH CHECK ADD  CONSTRAINT [CK_SalesPerson_SalesLastYear] CHECK  (([SalesLastYear]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPerson_SalesLastYear]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson] CHECK CONSTRAINT [CK_SalesPerson_SalesLastYear]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPerson_SalesQuota]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson]  WITH CHECK ADD  CONSTRAINT [CK_SalesPerson_SalesQuota] CHECK  (([SalesQuota]>(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPerson_SalesQuota]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson] CHECK CONSTRAINT [CK_SalesPerson_SalesQuota]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPerson_SalesYTD]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson]  WITH CHECK ADD  CONSTRAINT [CK_SalesPerson_SalesYTD] CHECK  (([SalesYTD]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Sales].[CK_SalesPerson_SalesYTD]') AND parent_object_id = OBJECT_ID(N'[Sales].[SalesPerson]'))
ALTER TABLE [Sales].[SalesPerson] CHECK CONSTRAINT [CK_SalesPerson_SalesYTD]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for SalesPerson records. Foreign key to Employee.BusinessEntityID' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'COLUMN',N'TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Territory currently assigned to. Foreign key to SalesTerritory.SalesTerritoryID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'COLUMN',@level2name=N'TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'COLUMN',N'SalesQuota'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Projected yearly sales.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'COLUMN',@level2name=N'SalesQuota'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'COLUMN',N'Bonus'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bonus due if quota is met.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'COLUMN',@level2name=N'Bonus'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'DF_SalesPerson_Bonus'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesPerson_Bonus'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'COLUMN',N'CommissionPct'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Commision percent received per sale.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'COLUMN',@level2name=N'CommissionPct'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'DF_SalesPerson_CommissionPct'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesPerson_CommissionPct'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'COLUMN',N'SalesYTD'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales total year to date.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'COLUMN',@level2name=N'SalesYTD'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'DF_SalesPerson_SalesYTD'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesPerson_SalesYTD'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'COLUMN',N'SalesLastYear'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales total of previous year.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'COLUMN',@level2name=N'SalesLastYear'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'DF_SalesPerson_SalesLastYear'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesPerson_SalesLastYear'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'DF_SalesPerson_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesPerson_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'DF_SalesPerson_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'DF_SalesPerson_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'PK_SalesPerson_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'PK_SalesPerson_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'INDEX',N'AK_SalesPerson_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'INDEX',@level2name=N'AK_SalesPerson_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales representative current information.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'FK_SalesPerson_Employee_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Employee.EmployeeID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesPerson_Employee_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'FK_SalesPerson_SalesTerritory_TerritoryID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing SalesTerritory.TerritoryID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'FK_SalesPerson_SalesTerritory_TerritoryID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'CK_SalesPerson_Bonus'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Bonus] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesPerson_Bonus'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'CK_SalesPerson_CommissionPct'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [CommissionPct] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesPerson_CommissionPct'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'CK_SalesPerson_SalesLastYear'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SalesLastYear] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesPerson_SalesLastYear'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'CK_SalesPerson_SalesQuota'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SalesQuota] > (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesPerson_SalesQuota'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'SalesPerson', N'CONSTRAINT',N'CK_SalesPerson_SalesYTD'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SalesYTD] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesPerson', @level2type=N'CONSTRAINT',@level2name=N'CK_SalesPerson_SalesYTD'
GO
