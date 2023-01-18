/****** Object:  StoredProcedure [HumanResources].[uspUpdateEmployeeLogin]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HumanResources].[uspUpdateEmployeeLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [HumanResources].[uspUpdateEmployeeLogin] AS' 
END
GO

ALTER PROCEDURE [HumanResources].[uspUpdateEmployeeLogin]
    @BusinessEntityID [int], 
    @OrganizationNode [hierarchyid],
    @LoginID [nvarchar](256),
    @JobTitle [nvarchar](50),
    @HireDate [datetime],
    @CurrentFlag [dbo].[Flag]
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE [HumanResources].[Employee] 
        SET [OrganizationNode] = @OrganizationNode 
            ,[LoginID] = @LoginID 
            ,[JobTitle] = @JobTitle 
            ,[HireDate] = @HireDate 
            ,[CurrentFlag] = @CurrentFlag 
        WHERE [BusinessEntityID] = @BusinessEntityID;
    END TRY
    BEGIN CATCH
        EXECUTE [dbo].[uspLogError];
    END CATCH;
END;
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'PROCEDURE',N'uspUpdateEmployeeLogin', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updates the Employee table with the values specified in the input parameters for the given BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'PROCEDURE',@level1name=N'uspUpdateEmployeeLogin'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'PROCEDURE',N'uspUpdateEmployeeLogin', N'PARAMETER',N'@BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the stored procedure uspUpdateEmployeeLogin. Enter a valid EmployeeID from the Employee table.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'PROCEDURE',@level1name=N'uspUpdateEmployeeLogin', @level2type=N'PARAMETER',@level2name=N'@BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'PROCEDURE',N'uspUpdateEmployeeLogin', N'PARAMETER',N'@OrganizationNode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter a valid ManagerID for the employee.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'PROCEDURE',@level1name=N'uspUpdateEmployeeLogin', @level2type=N'PARAMETER',@level2name=N'@OrganizationNode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'PROCEDURE',N'uspUpdateEmployeeLogin', N'PARAMETER',N'@LoginID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter a valid login for the employee.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'PROCEDURE',@level1name=N'uspUpdateEmployeeLogin', @level2type=N'PARAMETER',@level2name=N'@LoginID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'PROCEDURE',N'uspUpdateEmployeeLogin', N'PARAMETER',N'@JobTitle'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter a title for the employee.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'PROCEDURE',@level1name=N'uspUpdateEmployeeLogin', @level2type=N'PARAMETER',@level2name=N'@JobTitle'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'PROCEDURE',N'uspUpdateEmployeeLogin', N'PARAMETER',N'@HireDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter a hire date for the employee.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'PROCEDURE',@level1name=N'uspUpdateEmployeeLogin', @level2type=N'PARAMETER',@level2name=N'@HireDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'HumanResources', N'PROCEDURE',N'uspUpdateEmployeeLogin', N'PARAMETER',N'@CurrentFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter the current flag for the employee.' , @level0type=N'SCHEMA',@level0name=N'HumanResources', @level1type=N'PROCEDURE',@level1name=N'uspUpdateEmployeeLogin', @level2type=N'PARAMETER',@level2name=N'@CurrentFlag'
GO
