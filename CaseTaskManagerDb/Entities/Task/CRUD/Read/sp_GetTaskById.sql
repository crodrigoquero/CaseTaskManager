CREATE PROCEDURE sp_GetTaskById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, CaseId, Title, Description, StatusId, TaskTypeId, DueDate, CreatedAt
    FROM Tasks
    WHERE Id = @Id AND IsDeleted = 0;
END
