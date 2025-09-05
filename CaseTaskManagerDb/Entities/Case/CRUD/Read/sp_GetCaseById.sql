CREATE PROCEDURE sp_GetCaseById
    @Id INT
AS
BEGIN
    SELECT *
    FROM Cases
    WHERE Id = @Id AND IsDeleted = 0;
END

