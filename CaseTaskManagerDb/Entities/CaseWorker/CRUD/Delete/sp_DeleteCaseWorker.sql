CREATE PROCEDURE sp_DeleteCaseWorker
    @Id INT
AS
BEGIN
    UPDATE CaseWorkers
    SET IsDeleted = 1
    WHERE Id = @Id;
END

