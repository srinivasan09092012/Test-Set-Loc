/****** Object:  Schema [Production]    ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Production')
EXEC sys.sp_executesql N'CREATE SCHEMA [Production]'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', NULL,NULL, NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contains objects related to products, inventory, and manufacturing.' , @level0type=N'SCHEMA',@level0name=N'Production'
GO
