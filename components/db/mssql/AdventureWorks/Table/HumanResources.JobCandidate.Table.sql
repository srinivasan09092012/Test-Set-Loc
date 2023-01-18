/****** Object:  Table [HumanResources].[JobCandidate]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[JobCandidate]') AND type in (N'U'))
BEGIN
CREATE TABLE [HumanResources].[JobCandidate](
	[JobCandidateID] [int] IDENTITY(1,1) NOT NULL,
	[BusinessEntityID] [int] NULL,
	[Resume] [xml](CONTENT [HumanResources].[HRResumeSchemaCollection]) NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_JobCandidate_JobCandidateID] PRIMARY KEY CLUSTERED 
(
	[JobCandidateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Index [IX_JobCandidate_BusinessEntityID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HumanResources].[JobCandidate]') AND name = N'IX_JobCandidate_BusinessEntityID')
CREATE NONCLUSTERED INDEX [IX_JobCandidate_BusinessEntityID] ON [HumanResources].[JobCandidate]
(
	[BusinessEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  FullTextIndex     ******/
IF not EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[HumanResources].[JobCandidate]'))
CREATE FULLTEXT INDEX ON [HumanResources].[JobCandidate]
KEY INDEX [PK_JobCandidate_JobCandidateID]ON ([AW2016FullTextCatalog], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[DF_JobCandidate_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [HumanResources].[JobCandidate] ADD  CONSTRAINT [DF_JobCandidate_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_JobCandidate_Employee_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[JobCandidate]'))
ALTER TABLE [HumanResources].[JobCandidate]  WITH CHECK ADD  CONSTRAINT [FK_JobCandidate_Employee_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [HumanResources].[Employee] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[HumanResources].[FK_JobCandidate_Employee_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[HumanResources].[JobCandidate]'))
ALTER TABLE [HumanResources].[JobCandidate] CHECK CONSTRAINT [FK_JobCandidate_Employee_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'JobCandidate', N'COLUMN',N'JobCandidateID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for JobCandidate records.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'JobCandidate', @level2type=N'COLUMN',@level2name=N'JobCandidateID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'JobCandidate', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee identification number if applicant was hired. Foreign key to Employee.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'JobCandidate', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'JobCandidate', N'COLUMN',N'Resume'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Résumé in XML format.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'JobCandidate', @level2type=N'COLUMN',@level2name=N'Resume'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'JobCandidate', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'JobCandidate', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'JobCandidate', N'CONSTRAINT',N'DF_JobCandidate_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'JobCandidate', @level2type=N'CONSTRAINT',@level2name=N'DF_JobCandidate_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'JobCandidate', N'CONSTRAINT',N'PK_JobCandidate_JobCandidateID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'JobCandidate', @level2type=N'CONSTRAINT',@level2name=N'PK_JobCandidate_JobCandidateID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'JobCandidate', N'INDEX',N'IX_JobCandidate_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'JobCandidate', @level2type=N'INDEX',@level2name=N'IX_JobCandidate_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'JobCandidate', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Résumés submitted to Human Resources by job applicants.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'JobCandidate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'TABLE',N'JobCandidate', N'CONSTRAINT',N'FK_JobCandidate_Employee_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Employee.EmployeeID.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'TABLE',@level1name=N'JobCandidate', @level2type=N'CONSTRAINT',@level2name=N'FK_JobCandidate_Employee_BusinessEntityID'
GO
