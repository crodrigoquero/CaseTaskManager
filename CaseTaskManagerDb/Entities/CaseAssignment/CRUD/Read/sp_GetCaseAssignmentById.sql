CREATE PROCEDURE sp_GetCaseAssignmentById
    @Id INT
AS
BEGIN
    SELECT *
    FROM CaseAssignments
    WHERE Id = @Id;
END

