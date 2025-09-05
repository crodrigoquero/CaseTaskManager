CREATE PROCEDURE sp_DeleteTask
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Tasks
    SET IsDeleted = 1
    WHERE Id = @Id AND IsDeleted = 0;
END

