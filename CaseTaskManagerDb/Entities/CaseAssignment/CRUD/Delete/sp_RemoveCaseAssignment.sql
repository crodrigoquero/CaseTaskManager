CREATE PROCEDURE sp_RemoveCaseAssignment
    @CaseId INT,
    @CaseWorkerId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Only update if the assignment is still active
    UPDATE CaseAssignments
    SET RemovedAt = GETDATE()
    WHERE CaseId = @CaseId 
      AND CaseWorkerId = @CaseWorkerId 
      AND RemovedAt IS NULL;

    -- Return number of affected rows (optional)
    SELECT @@ROWCOUNT AS AffectedRows;
END
