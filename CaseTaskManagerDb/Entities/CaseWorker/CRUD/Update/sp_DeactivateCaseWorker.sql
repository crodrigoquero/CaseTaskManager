CREATE PROCEDURE sp_DeactivateCaseWorker
    @CaseWorkerId INT
AS
BEGIN
    UPDATE CaseWorkers
    SET IsActive = 0
    WHERE Id = @CaseWorkerId AND IsDeleted = 0;
END
