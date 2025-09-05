CREATE PROCEDURE dbo.sp_GetTasksByTypeId
    @TaskTypeId INT
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
    WHERE TaskTypeId = @TaskTypeId AND IsDeleted = 0
    ORDER BY DueDate;
END;
