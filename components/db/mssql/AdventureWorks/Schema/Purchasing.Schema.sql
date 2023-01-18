/****** Object:  Schema [Purchasing]    ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Purchasing')
EXEC sys.sp_executesql N'CREATE SCHEMA [Purchasing]'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Purchasing', NULL,NULL, NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contains objects related to vendors and purchase orders.' , @level0type=N'SCHEMA',@level0name=N'Purchasing'
GO
