/****** Object:  UserDefinedFunction [dbo].[ufnGetContactInformation]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ufnGetContactInformation]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[ufnGetContactInformation](@PersonID int)
RETURNS @retContactInformation TABLE 
(
    -- Columns returned by the function
    [PersonID] int NOT NULL, 
    [FirstName] [nvarchar](50) NULL, 
    [LastName] [nvarchar](50) NULL, 
	[JobTitle] [nvarchar](50) NULL,
    [BusinessEntityType] [nvarchar](50) NULL
)
AS 
-- Returns the first name, last name, job title and business entity type for the specified contact.
-- Since a contact can serve multiple roles, more than one row may be returned.
BEGIN
	IF @PersonID IS NOT NULL 
		BEGIN
		IF EXISTS(SELECT * FROM [HumanResources].[Employee] e 
					WHERE e.[BusinessEntityID] = @PersonID) 
			INSERT INTO @retContactInformation
				SELECT @PersonID, p.FirstName, p.LastName, e.[JobTitle], ''Employee''
				FROM [HumanResources].[Employee] AS e
					INNER JOIN [Person].[Person] p
					ON p.[BusinessEntityID] = e.[BusinessEntityID]
				WHERE e.[BusinessEntityID] = @PersonID;

		IF EXISTS(SELECT * FROM [Purchasing].[Vendor] AS v
					INNER JOIN [Person].[BusinessEntityContact] bec 
					ON bec.[BusinessEntityID] = v.[BusinessEntityID]
					WHERE bec.[PersonID] = @PersonID)
			INSERT INTO @retContactInformation
				SELECT @PersonID, p.FirstName, p.LastName, ct.[Name], ''Vendor Contact'' 
				FROM [Purchasing].[Vendor] AS v
					INNER JOIN [Person].[BusinessEntityContact] bec 
					ON bec.[BusinessEntityID] = v.[BusinessEntityID]
					INNER JOIN [Person].ContactType ct
					ON ct.[ContactTypeID] = bec.[ContactTypeID]
					INNER JOIN [Person].[Person] p
					ON p.[BusinessEntityID] = bec.[PersonID]
				WHERE bec.[PersonID] = @PersonID;
		
		IF EXISTS(SELECT * FROM [Sales].[Store] AS s
					INNER JOIN [Person].[BusinessEntityContact] bec 
					ON bec.[BusinessEntityID] = s.[BusinessEntityID]
					WHERE bec.[PersonID] = @PersonID)
			INSERT INTO @retContactInformation
				SELECT @PersonID, p.FirstName, p.LastName, ct.[Name], ''Store Contact'' 
				FROM [Sales].[Store] AS s
					INNER JOIN [Person].[BusinessEntityContact] bec 
					ON bec.[BusinessEntityID] = s.[BusinessEntityID]
					INNER JOIN [Person].ContactType ct
					ON ct.[ContactTypeID] = bec.[ContactTypeID]
					INNER JOIN [Person].[Person] p
					ON p.[BusinessEntityID] = bec.[PersonID]
				WHERE bec.[PersonID] = @PersonID;

		IF EXISTS(SELECT * FROM [Person].[Person] AS p
					INNER JOIN [Sales].[Customer] AS c
					ON c.[PersonID] = p.[BusinessEntityID]
					WHERE p.[BusinessEntityID] = @PersonID AND c.[StoreID] IS NULL) 
			INSERT INTO @retContactInformation
				SELECT @PersonID, p.FirstName, p.LastName, NULL, ''Consumer'' 
				FROM [Person].[Person] AS p
					INNER JOIN [Sales].[Customer] AS c
					ON c.[PersonID] = p.[BusinessEntityID]
					WHERE p.[BusinessEntityID] = @PersonID AND c.[StoreID] IS NULL; 
		END

	RETURN;
END;
' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'FUNCTION',N'ufnGetContactInformation', N'PARAMETER',N'@PersonID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the table value function ufnGetContactInformation. Enter a valid PersonID from the Person.Contact table.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'ufnGetContactInformation', @level2type=N'PARAMETER',@level2name=N'@PersonID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'FUNCTION',N'ufnGetContactInformation', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Table value function returning the first name, last name, job title and contact type for a given contact.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'ufnGetContactInformation'
GO
