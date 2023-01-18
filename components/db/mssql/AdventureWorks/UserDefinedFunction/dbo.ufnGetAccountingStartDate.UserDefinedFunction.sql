/****** Object:  UserDefinedFunction [dbo].[ufnGetAccountingStartDate]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ufnGetAccountingStartDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[ufnGetAccountingStartDate]()
RETURNS [datetime] 
AS 
BEGIN
    RETURN CONVERT(datetime, ''20030701'', 112);
END;
' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'FUNCTION',N'ufnGetAccountingStartDate', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Scalar function used in the uSalesOrderHeader trigger to set the ending account date.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'ufnGetAccountingStartDate'
GO
