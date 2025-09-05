CREATE PROCEDURE sp_AddCaseAssignment
    @CaseId INT,
    @CaseWorkerId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Prevent reassigning a case that is already assigned
    IF EXISTS (
        SELECT 1
        FROM CaseAssignments
        WHERE CaseId = @CaseId AND RemovedAt IS NULL
    )
    BEGIN
        RAISERROR('This case is already assigned to a case worker.', 16, 1);
        RETURN;
    END

    -- Optional: Limit the number of active assignments per case worker
    IF (
        SELECT COUNT(*)
        FROM CaseAssignments
        WHERE CaseWorkerId = @CaseWorkerId AND RemovedAt IS NULL
    ) >= 5
    BEGIN
        RAISERROR('This case worker is already handling the maximum allowed cases.', 16, 1);
        RETURN;
    END

    -- Proceed with the assignment
    INSERT INTO CaseAssignments (CaseId, CaseWorkerId, AssignedAt)
    VALUES (@CaseId, @CaseWorkerId, GETDATE());

    SELECT SCOPE_IDENTITY() AS NewAssignmentId;
END
