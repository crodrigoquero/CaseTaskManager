CREATE PROCEDURE sp_GetTasksByCaseId
    @CaseId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, CaseId, Title, Description, StatusId, TaskTypeId, DueDate, CreatedAt
    FROM Tasks
    WHERE CaseId = @CaseId AND IsDeleted = 0;
END
