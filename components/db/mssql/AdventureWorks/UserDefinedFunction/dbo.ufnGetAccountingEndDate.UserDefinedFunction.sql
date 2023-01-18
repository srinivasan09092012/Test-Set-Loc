/****** Object:  UserDefinedFunction [dbo].[ufnGetAccountingEndDate]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ufnGetAccountingEndDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[ufnGetAccountingEndDate]()
RETURNS [datetime] 
AS 
BEGIN
    RETURN DATEADD(millisecond, -2, CONVERT(datetime, ''20040701'', 112));
END;
' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'FUNCTION',N'ufnGetAccountingEndDate', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Scalar function used in the uSalesOrderHeader trigger to set the starting account date.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'ufnGetAccountingEndDate'
GO
