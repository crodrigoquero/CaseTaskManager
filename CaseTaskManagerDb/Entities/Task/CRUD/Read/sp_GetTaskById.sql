CREATE PROCEDURE dbo.sp_GetTaskById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.Id,
        t.CaseId,
        COALESCE(c.Title, CONCAT('Case #', t.CaseId)) AS CaseTitle, -- <—
        t.Title,
        t.Description,
        t.StatusId,
        ts.StatusName,
        t.TaskTypeId,
        tt.TypeName AS TaskTypeName,
        t.DueDate,
        t.CreatedAt
    FROM dbo.Tasks AS t
    LEFT JOIN dbo.TaskStatuses AS ts ON ts.Id = t.StatusId
    LEFT JOIN dbo.TaskTypes    AS tt ON tt.Id = t.TaskTypeId
    LEFT JOIN dbo.Cases        AS c  ON c.Id = t.CaseId              -- <— no IsDeleted filter
    WHERE t.Id = @Id
      AND t.IsDeleted = 0;
END
GO