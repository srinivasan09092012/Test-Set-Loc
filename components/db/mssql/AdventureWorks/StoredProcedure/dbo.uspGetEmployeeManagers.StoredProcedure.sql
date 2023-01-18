/****** Object:  StoredProcedure [dbo].[uspGetEmployeeManagers]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetEmployeeManagers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[uspGetEmployeeManagers] AS' 
END
GO

ALTER PROCEDURE [dbo].[uspGetEmployeeManagers]
    @BusinessEntityID [int]
AS
BEGIN
    SET NOCOUNT ON;

    -- Use recursive query to list out all Employees required for a particular Manager
    WITH [EMP_cte]([BusinessEntityID], [OrganizationNode], [FirstName], [LastName], [JobTitle], [RecursionLevel]) -- CTE name and columns
    AS (
        SELECT e.[BusinessEntityID], e.[OrganizationNode], p.[FirstName], p.[LastName], e.[JobTitle], 0 -- Get the initial Employee
        FROM [HumanResources].[Employee] e 
			INNER JOIN [Person].[Person] as p
			ON p.[BusinessEntityID] = e.[BusinessEntityID]
        WHERE e.[BusinessEntityID] = @BusinessEntityID
        UNION ALL
        SELECT e.[BusinessEntityID], e.[OrganizationNode], p.[FirstName], p.[LastName], e.[JobTitle], [RecursionLevel] + 1 -- Join recursive member to anchor
        FROM [HumanResources].[Employee] e 
            INNER JOIN [EMP_cte]
            ON e.[OrganizationNode] = [EMP_cte].[OrganizationNode].GetAncestor(1)
            INNER JOIN [Person].[Person] p 
            ON p.[BusinessEntityID] = e.[BusinessEntityID]
    )
    -- Join back to Employee to return the manager name 
    SELECT [EMP_cte].[RecursionLevel], [EMP_cte].[BusinessEntityID], [EMP_cte].[FirstName], [EMP_cte].[LastName], 
        [EMP_cte].[OrganizationNode].ToString() AS [OrganizationNode], p.[FirstName] AS 'ManagerFirstName', p.[LastName] AS 'ManagerLastName'  -- Outer select from the CTE
    FROM [EMP_cte] 
        INNER JOIN [HumanResources].[Employee] e 
        ON [EMP_cte].[OrganizationNode].GetAncestor(1) = e.[OrganizationNode]
        INNER JOIN [Person].[Person] p 
        ON p.[BusinessEntityID] = e.[BusinessEntityID]
    ORDER BY [RecursionLevel], [EMP_cte].[OrganizationNode].ToString()
    OPTION (MAXRECURSION 25) 
END;
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'PROCEDURE',N'uspGetEmployeeManagers', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Stored procedure using a recursive query to return the direct and indirect managers of the specified employee.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'uspGetEmployeeManagers'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'PROCEDURE',N'uspGetEmployeeManagers', N'PARAMETER',N'@BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the stored procedure uspGetEmployeeManagers. Enter a valid BusinessEntityID from the HumanResources.Employee table.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'uspGetEmployeeManagers', @level2type=N'PARAMETER',@level2name=N'@BusinessEntityID'
GO
