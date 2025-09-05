CREATE PROCEDURE sp_GetAssignmentsByCaseWorkerId
    @CaseWorkerId INT
AS
BEGIN
    SELECT *
    FROM CaseAssignments
    WHERE CaseWorkerId = @CaseWorkerId;
END
