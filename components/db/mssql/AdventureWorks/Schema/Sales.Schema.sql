/****** Object:  Schema [Sales]    ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Sales')
EXEC sys.sp_executesql N'CREATE SCHEMA [Sales]'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', NULL,NULL, NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contains objects related to customers, sales orders, and sales territories.' , @level0type=N'SCHEMA',@level0name=N'Sales'
GO
