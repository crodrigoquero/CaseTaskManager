CREATE PROCEDURE sp_DeleteCaseStatus
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CaseStatuses
    SET IsDeleted = 1
    WHERE Id = @Id AND IsDeleted = 0;
END
