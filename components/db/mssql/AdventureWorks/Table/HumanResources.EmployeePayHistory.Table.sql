/****** Object:  Table [HumanResources].[EmployeePayHistory]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[EmployeePayHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [HumanResources].[EmployeePayHistory](
	[BusinessEntityID] [int] NOT NULL,
	[RateChangeDate] [datetime] NOT NULL,
	[Rate] [money] NOT NULL,
	[PayFrequency] [tinyint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_EmployeePayHistory_BusinessEntityID_RateChangeDate] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC,
	[RateChangeDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[DF_EmployeePayHistory_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [HumanResources].[EmployeePayHistory] ADD  CONSTRAINT [DF_EmployeePayHistory_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_EmployeePayHistory_Employee_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[EmployeePayHistory]'))
ALTER TABLE [HumanResources].[EmployeePayHistory]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePayHistory_Employee_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [HumanResources].[Employee] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_EmployeePayHistory_Employee_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[EmployeePayHistory]'))
ALTER TABLE [HumanResources].[EmployeePayHistory] CHECK CONSTRAINT [FK_EmployeePayHistory_Employee_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_EmployeePayHistory_PayFrequency]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[EmployeePayHistory]'))
ALTER TABLE [HumanResources].[EmployeePayHistory]  WITH CHECK ADD  CONSTRAINT [CK_EmployeePayHistory_PayFrequency] CHECK  (([PayFrequency]=(2) OR [PayFrequency]=(1)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_EmployeePayHistory_PayFrequency]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[EmployeePayHistory]'))
ALTER TABLE [HumanResources].[EmployeePayHistory] CHECK CONSTRAINT [CK_EmployeePayHistory_PayFrequency]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_EmployeePayHistory_Rate]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[EmployeePayHistory]'))
ALTER TABLE [HumanResources].[EmployeePayHistory]  WITH CHECK ADD  CONSTRAINT [CK_EmployeePayHistory_Rate] CHECK  (([Rate]>=(6.50) AND [Rate]<=(200.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_EmployeePayHistory_Rate]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[EmployeePayHistory]'))
ALTER TABLE [HumanResources].[EmployeePayHistory] CHECK CONSTRAINT [CK_EmployeePayHistory_Rate]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'EmployeePayHistory', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee identification number. Foreign key to Employee.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'EmployeePayHistory', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'EmployeePayHistory', N'COLUMN',N'RateChangeDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date the change in pay is effective' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'EmployeePayHistory', @level2type=N'COLUMN',@level2name=N'RateChangeDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'EmployeePayHistory', N'COLUMN',N'Rate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Salary hourly rate.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'EmployeePayHistory', @level2type=N'COLUMN',@level2name=N'Rate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'EmployeePayHistory', N'COLUMN',N'PayFrequency'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Salary received monthly, 2 = Salary received biweekly' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'EmployeePayHistory', @level2type=N'COLUMN',@level2name=N'PayFrequency'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'EmployeePayHistory', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'EmployeePayHistory', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'EmployeePayHistory', N'CONSTRAINT',N'DF_EmployeePayHistory_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'EmployeePayHistory', @level2type=N'CONSTRAINT',@level2name=N'DF_EmployeePayHistory_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'EmployeePayHistory', N'CONSTRAINT',N'PK_EmployeePayHistory_BusinessEntityID_RateChangeDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'EmployeePayHistory', @level2type=N'CONSTRAINT',@level2name=N'PK_EmployeePayHistory_BusinessEntityID_RateChangeDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'EmployeePayHistory', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee pay history.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'EmployeePayHistory'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'EmployeePayHistory', N'CONSTRAINT',N'FK_EmployeePayHistory_Employee_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Employee.EmployeeID.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'EmployeePayHistory', @level2type=N'CONSTRAINT',@level2name=N'FK_EmployeePayHistory_Employee_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'EmployeePayHistory', N'CONSTRAINT',N'CK_EmployeePayHistory_PayFrequency'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [PayFrequency]=(3) OR [PayFrequency]=(2) OR [PayFrequency]=(1)' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'EmployeePayHistory', @level2type=N'CONSTRAINT',@level2name=N'CK_EmployeePayHistory_PayFrequency'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'EmployeePayHistory', N'CONSTRAINT',N'CK_EmployeePayHistory_Rate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Rate] >= (6.50) AND [Rate] <= (200.00)' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'EmployeePayHistory', @level2type=N'CONSTRAINT',@level2name=N'CK_EmployeePayHistory_Rate'
GO
