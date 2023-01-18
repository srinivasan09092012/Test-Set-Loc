/****** Object:  Table [Person].[PhoneNumberType]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[PhoneNumberType]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[PhoneNumberType](
	[PhoneNumberTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PhoneNumberType_PhoneNumberTypeID] PRIMARY KEY CLUSTERED 
(
	[PhoneNumberTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_PhoneNumberType_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[PhoneNumberType] ADD  CONSTRAINT [DF_PhoneNumberType_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PhoneNumberType', N'COLUMN',N'PhoneNumberTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for telephone number type records.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PhoneNumberType', @level2type=N'COLUMN',@level2name=N'PhoneNumberTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PhoneNumberType', N'COLUMN',N'Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name of the telephone number type' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PhoneNumberType', @level2type=N'COLUMN',@level2name=N'Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PhoneNumberType', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PhoneNumberType', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PhoneNumberType', N'CONSTRAINT',N'DF_PhoneNumberType_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PhoneNumberType', @level2type=N'CONSTRAINT',@level2name=N'DF_PhoneNumberType_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PhoneNumberType', N'CONSTRAINT',N'PK_PhoneNumberType_PhoneNumberTypeID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PhoneNumberType', @level2type=N'CONSTRAINT',@level2name=N'PK_PhoneNumberType_PhoneNumberTypeID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'PhoneNumberType', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type of phone number of a person.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'PhoneNumberType'
GO
