CREATE PROCEDURE dbo.sp_GetAllCaseWorkers
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        FullName,
        Email,
        IsActive,  
        IsDeleted
    FROM dbo.CaseWorkers
    WHERE IsDeleted = 0;
END
