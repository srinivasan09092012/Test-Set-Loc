/****** Object:  View [Sales].[vSalesPersonSalesByFiscalYears]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[Sales].[vSalesPersonSalesByFiscalYears]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [Sales].[vSalesPersonSalesByFiscalYears] 
AS 
SELECT 
    pvt.[SalesPersonID]
    ,pvt.[FullName]
    ,pvt.[JobTitle]
    ,pvt.[SalesTerritory]
    ,pvt.[2002]
    ,pvt.[2003]
    ,pvt.[2004] 
FROM (SELECT 
        soh.[SalesPersonID]
        ,p.[FirstName] + '' '' + COALESCE(p.[MiddleName], '''') + '' '' + p.[LastName] AS [FullName]
        ,e.[JobTitle]
        ,st.[Name] AS [SalesTerritory]
        ,soh.[SubTotal]
        ,YEAR(DATEADD(m, 6, soh.[OrderDate])) AS [FiscalYear] 
    FROM [Sales].[SalesPerson] sp 
        INNER JOIN [Sales].[SalesOrderHeader] soh 
        ON sp.[BusinessEntityID] = soh.[SalesPersonID]
        INNER JOIN [Sales].[SalesTerritory] st 
        ON sp.[TerritoryID] = st.[TerritoryID] 
        INNER JOIN [HumanResources].[Employee] e 
        ON soh.[SalesPersonID] = e.[BusinessEntityID] 
		INNER JOIN [Person].[Person] p
		ON p.[BusinessEntityID] = sp.[BusinessEntityID]
	 ) AS soh 
PIVOT 
(
    SUM([SubTotal]) 
    FOR [FiscalYear] 
    IN ([2002], [2003], [2004])
) AS pvt;
' 
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'VIEW',N'vSalesPersonSalesByFiscalYears', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Uses PIVOT to return aggregated sales information for each sales representative.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'VIEW',@level1name=N'vSalesPersonSalesByFiscalYears'
GO
