CREATE PROCEDURE sp_AddCase
    @Title NVARCHAR(200),
    @Description NVARCHAR(MAX),
    @CurrentStatusId INT = NULL
AS
BEGIN
    INSERT INTO Cases (Title, Description, CurrentStatusId, CreatedAt, IsDeleted)
    VALUES (@Title, @Description, @CurrentStatusId, GETDATE(), 0);

    SELECT SCOPE_IDENTITY() AS NewCaseId;
END
