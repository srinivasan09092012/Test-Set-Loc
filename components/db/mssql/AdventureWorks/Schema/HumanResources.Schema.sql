/****** Object:  Schema [HumanResources]    ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'HumanResources')
EXEC sys.sp_executesql N'CREATE SCHEMA [HumanResources]'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', NULL,NULL, NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contains objects related to employees and departments.' , @level0type=N'SCHEMA',@level0name=N'HumanResources'
GO
