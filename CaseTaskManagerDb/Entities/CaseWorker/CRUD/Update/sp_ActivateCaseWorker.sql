CREATE PROCEDURE sp_ActivateCaseWorker
    @CaseWorkerId INT
AS
BEGIN
    UPDATE CaseWorkers
    SET IsActive = 1
    WHERE Id = @CaseWorkerId AND IsDeleted = 0;
END
