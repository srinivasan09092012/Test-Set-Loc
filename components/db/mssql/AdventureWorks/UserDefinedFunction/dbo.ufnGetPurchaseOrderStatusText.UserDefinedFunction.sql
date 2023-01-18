/****** Object:  UserDefinedFunction [dbo].[ufnGetPurchaseOrderStatusText]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ufnGetPurchaseOrderStatusText]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[ufnGetPurchaseOrderStatusText](@Status [tinyint])
RETURNS [nvarchar](15) 
AS 
-- Returns the sales order status text representation for the status value.
BEGIN
    DECLARE @ret [nvarchar](15);

    SET @ret = 
        CASE @Status
            WHEN 1 THEN ''Pending''
            WHEN 2 THEN ''Approved''
            WHEN 3 THEN ''Rejected''
            WHEN 4 THEN ''Complete''
            ELSE ''** Invalid **''
        END;
    
    RETURN @ret
END;
' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'FUNCTION',N'ufnGetPurchaseOrderStatusText', N'PARAMETER',N'@Status'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the scalar function ufnGetPurchaseOrdertStatusText. Enter a valid integer.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'ufnGetPurchaseOrderStatusText', @level2type=N'PARAMETER',@level2name=N'@Status'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'FUNCTION',N'ufnGetPurchaseOrderStatusText', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Scalar function returning the text representation of the Status column in the PurchaseOrderHeader table.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'ufnGetPurchaseOrderStatusText'
GO
