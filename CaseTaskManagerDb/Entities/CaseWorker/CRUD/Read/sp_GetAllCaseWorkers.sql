CREATE PROCEDURE sp_GetAllCaseWorkers
AS
BEGIN
    SELECT *
    FROM CaseWorkers
    WHERE IsDeleted = 0;
END

