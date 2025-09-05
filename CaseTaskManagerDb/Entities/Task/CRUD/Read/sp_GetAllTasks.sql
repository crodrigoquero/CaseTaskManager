CREATE PROCEDURE dbo.sp_GetAllTasks
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        CaseId,
        Title,
        Description,
        StatusId,
        TaskTypeId,
        DueDate,
        CreatedAt
    FROM dbo.Tasks
    WHERE IsDeleted = 0
    ORDER BY DueDate;
END;
