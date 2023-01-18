/****** Object:  UserDefinedFunction [dbo].[ufnGetStock]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ufnGetStock]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[ufnGetStock](@ProductID [int])
RETURNS [int] 
AS 
-- Returns the stock level for the product. This function is used internally only
BEGIN
    DECLARE @ret int;
    
    SELECT @ret = SUM(p.[Quantity]) 
    FROM [Production].[ProductInventory] p 
    WHERE p.[ProductID] = @ProductID 
        AND p.[LocationID] = ''6''; -- Only look at inventory in the misc storage
    
    IF (@ret IS NULL) 
        SET @ret = 0
    
    RETURN @ret
END;
' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'FUNCTION',N'ufnGetStock', N'PARAMETER',N'@ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the scalar function ufnGetStock. Enter a valid ProductID from the Production.ProductInventory table.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'ufnGetStock', @level2type=N'PARAMETER',@level2name=N'@ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'FUNCTION',N'ufnGetStock', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Scalar function returning the quantity of inventory in LocationID 6 (Miscellaneous Storage)for a specified ProductID.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'ufnGetStock'
GO
