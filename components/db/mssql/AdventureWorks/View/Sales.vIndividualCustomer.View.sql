/****** Object:  View [Sales].[vIndividualCustomer]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[Sales].[vIndividualCustomer]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [Sales].[vIndividualCustomer] 
AS 
SELECT 
    p.[BusinessEntityID]
    ,p.[Title]
    ,p.[FirstName]
    ,p.[MiddleName]
    ,p.[LastName]
    ,p.[Suffix]
    ,pp.[PhoneNumber]
	,pnt.[Name] AS [PhoneNumberType]
    ,ea.[EmailAddress]
    ,p.[EmailPromotion]
    ,at.[Name] AS [AddressType]
    ,a.[AddressLine1]
    ,a.[AddressLine2]
    ,a.[City]
    ,[StateProvinceName] = sp.[Name]
    ,a.[PostalCode]
    ,[CountryRegionName] = cr.[Name]
    ,p.[Demographics]
FROM [Person].[Person] p
    INNER JOIN [Person].[BusinessEntityAddress] bea 
    ON bea.[BusinessEntityID] = p.[BusinessEntityID] 
    INNER JOIN [Person].[Address] a 
    ON a.[AddressID] = bea.[AddressID]
    INNER JOIN [Person].[StateProvince] sp 
    ON sp.[StateProvinceID] = a.[StateProvinceID]
    INNER JOIN [Person].[CountryRegion] cr 
    ON cr.[CountryRegionCode] = sp.[CountryRegionCode]
    INNER JOIN [Person].[AddressType] at 
    ON at.[AddressTypeID] = bea.[AddressTypeID]
	INNER JOIN [Sales].[Customer] c
	ON c.[PersonID] = p.[BusinessEntityID]
	LEFT OUTER JOIN [Person].[EmailAddress] ea
	ON ea.[BusinessEntityID] = p.[BusinessEntityID]
	LEFT OUTER JOIN [Person].[PersonPhone] pp
	ON pp.[BusinessEntityID] = p.[BusinessEntityID]
	LEFT OUTER JOIN [Person].[PhoneNumberType] pnt
	ON pnt.[PhoneNumberTypeID] = pp.[PhoneNumberTypeID]
WHERE c.StoreID IS NULL;
' 
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'VIEW',N'vIndividualCustomer', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Individual customers (names and addresses) that purchase Adventure Works Cycles products online.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'VIEW',@level1name=N'vIndividualCustomer'
GO
