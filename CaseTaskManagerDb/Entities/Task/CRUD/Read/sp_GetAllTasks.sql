CREATE PROCEDURE dbo.sp_GetAllTasks
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.Id,
        t.CaseId,
        t.Title,
        t.Description,
        t.StatusId,
        ts.[StatusName] AS StatusName,   -- join for human-readable status
        t.TaskTypeId,
        tt.[TypeName] AS TaskTypeName, -- join task types too
        t.DueDate,
        t.CreatedAt
    FROM dbo.Tasks t
    LEFT JOIN dbo.TaskStatuses ts ON ts.Id = t.StatusId
    LEFT JOIN dbo.TaskTypes tt ON tt.Id = t.TaskTypeId
    WHERE t.IsDeleted = 0
    ORDER BY t.DueDate;
END;

