CREATE PROCEDURE sp_GetAssignmentsByCaseId
    @CaseId INT
AS
BEGIN
    SELECT *
    FROM CaseAssignments
    WHERE CaseId = @CaseId;
END

