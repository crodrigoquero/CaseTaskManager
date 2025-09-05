CREATE PROCEDURE sp_GetCaseWorkerById
    @Id INT
AS
BEGIN
    SELECT *
    FROM CaseWorkers
    WHERE Id = @Id AND IsDeleted = 0;
END
