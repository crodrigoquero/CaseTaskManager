CREATE PROCEDURE sp_GetCaseStatusById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, StatusName
    FROM CaseStatuses
    WHERE Id = @Id AND IsDeleted = 0;
END
