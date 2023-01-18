/****** Object:  UserDefinedFunction [dbo].[ufnGetProductListPrice]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ufnGetProductListPrice]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[ufnGetProductListPrice](@ProductID [int], @OrderDate [datetime])
RETURNS [money] 
AS 
BEGIN
    DECLARE @ListPrice money;

    SELECT @ListPrice = plph.[ListPrice] 
    FROM [Production].[Product] p 
        INNER JOIN [Production].[ProductListPriceHistory] plph 
        ON p.[ProductID] = plph.[ProductID] 
            AND p.[ProductID] = @ProductID 
            AND @OrderDate BETWEEN plph.[StartDate] AND COALESCE(plph.[EndDate], CONVERT(datetime, ''99991231'', 112)); -- Make sure we get all the prices!

    RETURN @ListPrice;
END;
' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'FUNCTION',N'ufnGetProductListPrice', N'PARAMETER',N'@ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the scalar function ufnGetProductListPrice. Enter a valid ProductID from the Production.Product table.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'ufnGetProductListPrice', @level2type=N'PARAMETER',@level2name=N'@ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'FUNCTION',N'ufnGetProductListPrice', N'PARAMETER',N'@OrderDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the scalar function ufnGetProductListPrice. Enter a valid order date.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'ufnGetProductListPrice', @level2type=N'PARAMETER',@level2name=N'@OrderDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'FUNCTION',N'ufnGetProductListPrice', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Scalar function returning the list price for a given product on a particular order date.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'ufnGetProductListPrice'
GO
