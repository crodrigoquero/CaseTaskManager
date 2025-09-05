CREATE PROCEDURE sp_UpdateCaseWorker 
    @Id INT,
    @FullName NVARCHAR(100),
    @Email NVARCHAR(255)
AS
BEGIN
    UPDATE CaseWorkers
    SET FullName = @FullName,
        Email = @Email
    WHERE Id = @Id AND IsDeleted = 0;
END
