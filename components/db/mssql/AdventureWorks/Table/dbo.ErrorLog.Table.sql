/****** Object:  Table [dbo].[ErrorLog]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ErrorLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ErrorLog](
	[ErrorLogID] [int] IDENTITY(1,1) NOT NULL,
	[ErrorTime] [datetime] NOT NULL,
	[UserName] [sysname] NOT NULL,
	[ErrorNumber] [int] NOT NULL,
	[ErrorSeverity] [int] NULL,
	[ErrorState] [int] NULL,
	[ErrorProcedure] [nvarchar](126) NULL,
	[ErrorLine] [int] NULL,
	[ErrorMessage] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_ErrorLog_ErrorLogID] PRIMARY KEY CLUSTERED 
(
	[ErrorLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_ErrorLog_ErrorTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ErrorLog] ADD  CONSTRAINT [DF_ErrorLog_ErrorTime]  DEFAULT (getdate()) FOR [ErrorTime]
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', N'COLUMN',N'ErrorLogID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for ErrorLog records.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog', @level2type=N'COLUMN',@level2name=N'ErrorLogID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', N'COLUMN',N'ErrorTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The date and time at which the error occurred.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog', @level2type=N'COLUMN',@level2name=N'ErrorTime'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', N'CONSTRAINT',N'DF_ErrorLog_ErrorTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog', @level2type=N'CONSTRAINT',@level2name=N'DF_ErrorLog_ErrorTime'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', N'COLUMN',N'UserName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The user who executed the batch in which the error occurred.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog', @level2type=N'COLUMN',@level2name=N'UserName'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', N'COLUMN',N'ErrorNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The error number of the error that occurred.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog', @level2type=N'COLUMN',@level2name=N'ErrorNumber'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', N'COLUMN',N'ErrorSeverity'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The severity of the error that occurred.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog', @level2type=N'COLUMN',@level2name=N'ErrorSeverity'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', N'COLUMN',N'ErrorState'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The state number of the error that occurred.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog', @level2type=N'COLUMN',@level2name=N'ErrorState'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', N'COLUMN',N'ErrorProcedure'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The name of the stored procedure or trigger where the error occurred.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog', @level2type=N'COLUMN',@level2name=N'ErrorProcedure'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', N'COLUMN',N'ErrorLine'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The line number at which the error occurred.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog', @level2type=N'COLUMN',@level2name=N'ErrorLine'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', N'COLUMN',N'ErrorMessage'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The message text of the error that occurred.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog', @level2type=N'COLUMN',@level2name=N'ErrorMessage'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', N'CONSTRAINT',N'PK_ErrorLog_ErrorLogID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog', @level2type=N'CONSTRAINT',@level2name=N'PK_ErrorLog_ErrorLogID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ErrorLog', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Audit table tracking errors in the the AdventureWorks database that are caught by the CATCH block of a TRY...CATCH construct. Data is inserted by stored procedure dbo.uspLogError when it is executed from inside the CATCH block of a TRY...CATCH construct.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorLog'
GO
