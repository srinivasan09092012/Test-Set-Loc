/****** Object:  Table [HumanResources].[Employee]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[Employee]') AND type in (N'U'))
BEGIN
CREATE TABLE [HumanResources].[Employee](
	[BusinessEntityID] [int] NOT NULL,
	[NationalIDNumber] [nvarchar](15) NOT NULL,
	[LoginID] [nvarchar](256) NOT NULL,
	[OrganizationNode] [hierarchyid] NULL,
	[OrganizationLevel]  AS ([OrganizationNode].[GetLevel]()),
	[JobTitle] [nvarchar](50) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[MaritalStatus] [nchar](1) NOT NULL,
	[Gender] [nchar](1) NOT NULL,
	[HireDate] [date] NOT NULL,
	[SalariedFlag] [dbo].[Flag] NOT NULL,
	[VacationHours] [smallint] NOT NULL,
	[SickLeaveHours] [smallint] NOT NULL,
	[CurrentFlag] [dbo].[Flag] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Employee_BusinessEntityID] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Employee_LoginID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HumanResources].[Employee]') AND name = N'AK_Employee_LoginID')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Employee_LoginID] ON [HumanResources].[Employee]
(
	[LoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Employee_NationalIDNumber]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HumanResources].[Employee]') AND name = N'AK_Employee_NationalIDNumber')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Employee_NationalIDNumber] ON [HumanResources].[Employee]
(
	[NationalIDNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AK_Employee_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HumanResources].[Employee]') AND name = N'AK_Employee_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Employee_rowguid] ON [HumanResources].[Employee]
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
/****** Object:  Index [IX_Employee_OrganizationLevel_OrganizationNode]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HumanResources].[Employee]') AND name = N'IX_Employee_OrganizationLevel_OrganizationNode')
CREATE NONCLUSTERED INDEX [IX_Employee_OrganizationLevel_OrganizationNode] ON [HumanResources].[Employee]
(
	[OrganizationLevel] ASC,
	[OrganizationNode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employee_OrganizationNode]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HumanResources].[Employee]') AND name = N'IX_Employee_OrganizationNode')
CREATE NONCLUSTERED INDEX [IX_Employee_OrganizationNode] ON [HumanResources].[Employee]
(
	[OrganizationNode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[DF_Employee_SalariedFlag]') AND type = 'D')
BEGIN
ALTER TABLE [HumanResources].[Employee] ADD  CONSTRAINT [DF_Employee_SalariedFlag]  DEFAULT ((1)) FOR [SalariedFlag]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[DF_Employee_VacationHours]') AND type = 'D')
BEGIN
ALTER TABLE [HumanResources].[Employee] ADD  CONSTRAINT [DF_Employee_VacationHours]  DEFAULT ((0)) FOR [VacationHours]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[DF_Employee_SickLeaveHours]') AND type = 'D')
BEGIN
ALTER TABLE [HumanResources].[Employee] ADD  CONSTRAINT [DF_Employee_SickLeaveHours]  DEFAULT ((0)) FOR [SickLeaveHours]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[DF_Employee_CurrentFlag]') AND type = 'D')
BEGIN
ALTER TABLE [HumanResources].[Employee] ADD  CONSTRAINT [DF_Employee_CurrentFlag]  DEFAULT ((1)) FOR [CurrentFlag]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[DF_Employee_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [HumanResources].[Employee] ADD  CONSTRAINT [DF_Employee_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[DF_Employee_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [HumanResources].[Employee] ADD  CONSTRAINT [DF_Employee_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Employee_Person_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Person_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Person].[Person] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_Employee_Person_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee] CHECK CONSTRAINT [FK_Employee_Person_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_BirthDate]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee]  WITH CHECK ADD  CONSTRAINT [CK_Employee_BirthDate] CHECK  (([BirthDate]>='1930-01-01' AND [BirthDate]<=dateadd(year,(-18),getdate())))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_BirthDate]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee] CHECK CONSTRAINT [CK_Employee_BirthDate]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_Gender]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee]  WITH CHECK ADD  CONSTRAINT [CK_Employee_Gender] CHECK  ((upper([Gender])='F' OR upper([Gender])='M'))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_Gender]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee] CHECK CONSTRAINT [CK_Employee_Gender]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_HireDate]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee]  WITH CHECK ADD  CONSTRAINT [CK_Employee_HireDate] CHECK  (([HireDate]>='1996-07-01' AND [HireDate]<=dateadd(day,(1),getdate())))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_HireDate]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee] CHECK CONSTRAINT [CK_Employee_HireDate]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_MaritalStatus]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee]  WITH CHECK ADD  CONSTRAINT [CK_Employee_MaritalStatus] CHECK  ((upper([MaritalStatus])='S' OR upper([MaritalStatus])='M'))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_MaritalStatus]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee] CHECK CONSTRAINT [CK_Employee_MaritalStatus]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_SickLeaveHours]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee]  WITH CHECK ADD  CONSTRAINT [CK_Employee_SickLeaveHours] CHECK  (([SickLeaveHours]>=(0) AND [SickLeaveHours]<=(120)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_SickLeaveHours]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee] CHECK CONSTRAINT [CK_Employee_SickLeaveHours]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_VacationHours]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee]  WITH CHECK ADD  CONSTRAINT [CK_Employee_VacationHours] CHECK  (([VacationHours]>=(-40) AND [VacationHours]<=(240)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[HumanResources].[CK_Employee_VacationHours]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[Employee]'))
ALTER TABLE [HumanResources].[Employee] CHECK CONSTRAINT [CK_Employee_VacationHours]
GO
/****** Object:  Trigger [HumanResources].[dEmployee]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[HumanResources].[dEmployee]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [HumanResources].[dEmployee] ON [HumanResources].[Employee] 
INSTEAD OF DELETE NOT FOR REPLICATION AS 
BEGIN
    DECLARE @Count int;

    SET @Count = @@ROWCOUNT;
    IF @Count = 0 
        RETURN;

    SET NOCOUNT ON;

    BEGIN
        RAISERROR
            (N''Employees cannot be deleted. They can only be marked as not current.'', -- Message
            10, -- Severity.
            1); -- State.

        -- Rollback any active or uncommittable transactions
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
    END;
END;
' 
GO
ALTER TABLE [HumanResources].[Employee] ENABLE TRIGGER [dEmployee]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for Employee records.  Foreign key to BusinessEntity.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'NationalIDNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique national identification number such as a social security number.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'NationalIDNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'LoginID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Network login.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'LoginID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'OrganizationNode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Where the employee is located in corporate hierarchy.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'OrganizationNode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'OrganizationLevel'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The depth of the employee in the corporate hierarchy.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'OrganizationLevel'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'JobTitle'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Work title such as Buyer or Sales Representative.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'JobTitle'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'BirthDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of birth.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'BirthDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'MaritalStatus'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'M = Married, S = Single' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'MaritalStatus'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'Gender'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'M = Male, F = Female' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Gender'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'HireDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee hired on this date.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'HireDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'SalariedFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective bargaining.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'SalariedFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'DF_Employee_SalariedFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 1 (TRUE)' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'DF_Employee_SalariedFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'VacationHours'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Number of available vacation hours.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'VacationHours'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'DF_Employee_VacationHours'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'DF_Employee_VacationHours'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'SickLeaveHours'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Number of available sick leave hours.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'SickLeaveHours'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'DF_Employee_SickLeaveHours'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'DF_Employee_SickLeaveHours'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'CurrentFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = Inactive, 1 = Active' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'CurrentFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'DF_Employee_CurrentFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 1' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'DF_Employee_CurrentFlag'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'DF_Employee_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'DF_Employee_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'DF_Employee_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'DF_Employee_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'PK_Employee_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'PK_Employee_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'INDEX',N'AK_Employee_LoginID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'INDEX',@level2name=N'AK_Employee_LoginID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'INDEX',N'AK_Employee_NationalIDNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'INDEX',@level2name=N'AK_Employee_NationalIDNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'INDEX',N'AK_Employee_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'INDEX',@level2name=N'AK_Employee_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'INDEX',N'IX_Employee_OrganizationLevel_OrganizationNode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'INDEX',@level2name=N'IX_Employee_OrganizationLevel_OrganizationNode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'INDEX',N'IX_Employee_OrganizationNode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'INDEX',@level2name=N'IX_Employee_OrganizationNode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee information such as salary, department, and title.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'TRIGGER',N'dEmployee'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'INSTEAD OF DELETE trigger which keeps Employees from being deleted.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'TRIGGER',@level2name=N'dEmployee'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'FK_Employee_Person_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Person.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'FK_Employee_Person_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'CK_Employee_BirthDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [BirthDate] >= ''1930-01-01'' AND [BirthDate] <= dateadd(year,(-18),GETDATE())' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'CK_Employee_BirthDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'CK_Employee_Gender'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Gender]=''f'' OR [Gender]=''m'' OR [Gender]=''F'' OR [Gender]=''M''' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'CK_Employee_Gender'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'CK_Employee_HireDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [HireDate] >= ''1996-07-01'' AND [HireDate] <= dateadd(day,(1),GETDATE())' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'CK_Employee_HireDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'CK_Employee_MaritalStatus'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [MaritalStatus]=''s'' OR [MaritalStatus]=''m'' OR [MaritalStatus]=''S'' OR [MaritalStatus]=''M''' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'CK_Employee_MaritalStatus'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'CK_Employee_SickLeaveHours'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [SickLeaveHours] >= (0) AND [SickLeaveHours] <= (120)' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'CK_Employee_SickLeaveHours'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'Employee', N'CONSTRAINT',N'CK_Employee_VacationHours'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [VacationHours] >= (-40) AND [VacationHours] <= (240)' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'CONSTRAINT',@level2name=N'CK_Employee_VacationHours'
GO
