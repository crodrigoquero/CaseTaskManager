CREATE PROCEDURE sp_AddCaseWorker
    @FullName NVARCHAR(100),
    @Email NVARCHAR(255),
    @IsActive BIT = 1
AS
BEGIN
    INSERT INTO CaseWorkers (FullName, Email, IsActive, IsDeleted)
    VALUES (@FullName, @Email, @IsActive, 0);

    SELECT SCOPE_IDENTITY() AS NewCaseWorkerId;
END

