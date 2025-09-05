CREATE PROCEDURE sp_DeleteCase
    @Id INT
AS
BEGIN
    UPDATE Cases
    SET IsDeleted = 1
    WHERE Id = @Id;
END
