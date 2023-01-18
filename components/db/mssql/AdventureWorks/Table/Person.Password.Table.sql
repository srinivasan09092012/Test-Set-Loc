/****** Object:  Table [Person].[Password]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[Password]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[Password](
	[BusinessEntityID] [int] NOT NULL,
	[PasswordHash] [varchar](128) NOT NULL,
	[PasswordSalt] [varchar](10) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Password_BusinessEntityID] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_Password_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[Password] ADD  CONSTRAINT [DF_Password_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_Password_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[Password] ADD  CONSTRAINT [DF_Password_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Password_Person_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Person].[Password]'))
ALTER TABLE [Person].[Password]  WITH CHECK ADD  CONSTRAINT [FK_Password_Person_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Person].[Person] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Password_Person_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Person].[Password]'))
ALTER TABLE [Person].[Password] CHECK CONSTRAINT [FK_Password_Person_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Password', N'COLUMN',N'PasswordHash'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Password for the e-mail account.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Password', @level2type=N'COLUMN',@level2name=N'PasswordHash'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Password', N'COLUMN',N'PasswordSalt'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Random value concatenated with the password string before the password is hashed.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Password', @level2type=N'COLUMN',@level2name=N'PasswordSalt'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Password', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Password', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Password', N'CONSTRAINT',N'DF_Password_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Password', @level2type=N'CONSTRAINT',@level2name=N'DF_Password_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Password', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Password', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Password', N'CONSTRAINT',N'DF_Password_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Password', @level2type=N'CONSTRAINT',@level2name=N'DF_Password_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Password', N'CONSTRAINT',N'PK_Password_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Password', @level2type=N'CONSTRAINT',@level2name=N'PK_Password_BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Password', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'One way hashed authentication information' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Password'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Password', N'CONSTRAINT',N'FK_Password_Person_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Person.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Password', @level2type=N'CONSTRAINT',@level2name=N'FK_Password_Person_BusinessEntityID'
GO
