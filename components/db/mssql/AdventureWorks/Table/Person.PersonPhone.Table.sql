/****** Object:  Table [Person].[PersonPhone]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PersonPhone]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PersonPhone](
	[BusinessEntityID] [int] NOT NULL,
	[PhoneNumber] [dbo].[Phone] NOT NULL,
	[PhoneNumberTypeID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PersonPhone_BusinessEntityID_PhoneNumber_PhoneNumberTypeID] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC,
	[PhoneNumber] ASC,
	[PhoneNumberTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PersonPhone_PhoneNumber]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[PersonPhone]') AND name = N'IX_PersonPhone_PhoneNumber')
CREATE NONCLUSTERED INDEX [IX_PersonPhone_PhoneNumber] ON [Person].[PersonPhone]
(
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_PersonPhone_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[PersonPhone] ADD  CONSTRAINT [DF_PersonPhone_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonPhone_Person_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonPhone]'))
ALTER TABLE [Person].[PersonPhone]  WITH CHECK ADD  CONSTRAINT [FK_PersonPhone_Person_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Person].[Person] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonPhone_Person_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonPhone]'))
ALTER TABLE [Person].[PersonPhone] CHECK CONSTRAINT [FK_PersonPhone_Person_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonPhone]'))
ALTER TABLE [Person].[PersonPhone]  WITH CHECK ADD  CONSTRAINT [FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID] FOREIGN KEY([PhoneNumberTypeID])
REFERENCES [Person].[PhoneNumberType] ([PhoneNumberTypeID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID]') AND parent_object_id = OBJECT_ID(N'[Person].[PersonPhone]'))
ALTER TABLE [Person].[PersonPhone] CHECK CONSTRAINT [FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PersonPhone', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Business entity identification number. Foreign key to Person.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PersonPhone', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PersonPhone', N'COLUMN',N'PhoneNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Telephone number identification number.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PersonPhone', @level2type=N'COLUMN',@level2name=N'PhoneNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PersonPhone', N'COLUMN',N'PhoneNumberTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kind of phone number. Foreign key to PhoneNumberType.PhoneNumberTypeID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PersonPhone', @level2type=N'COLUMN',@level2name=N'PhoneNumberTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PersonPhone', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PersonPhone', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PersonPhone', N'CONSTRAINT',N'DF_PersonPhone_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PersonPhone', @level2type=N'CONSTRAINT',@level2name=N'DF_PersonPhone_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PersonPhone', N'CONSTRAINT',N'PK_PersonPhone_BusinessEntityID_PhoneNumber_PhoneNumberTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PersonPhone', @level2type=N'CONSTRAINT',@level2name=N'PK_PersonPhone_BusinessEntityID_PhoneNumber_PhoneNumberTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PersonPhone', N'INDEX',N'IX_PersonPhone_PhoneNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PersonPhone', @level2type=N'INDEX',@level2name=N'IX_PersonPhone_PhoneNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PersonPhone', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Telephone number and type of a person.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PersonPhone'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PersonPhone', N'CONSTRAINT',N'FK_PersonPhone_Person_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Person.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PersonPhone', @level2type=N'CONSTRAINT',@level2name=N'FK_PersonPhone_Person_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PersonPhone', N'CONSTRAINT',N'FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing PhoneNumberType.PhoneNumberTypeID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PersonPhone', @level2type=N'CONSTRAINT',@level2name=N'FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID'
GO
