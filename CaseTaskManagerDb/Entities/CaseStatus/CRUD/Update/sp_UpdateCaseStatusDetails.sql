CREATE PROCEDURE sp_UpdateCaseStatusDetails
    @Id INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CaseStatuses
    SET StatusName = @Name,
        Description = @Description
    WHERE Id = @Id AND IsDeleted = 0;
END
