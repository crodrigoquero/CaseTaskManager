CREATE PROCEDURE dbo.sp_DeleteTask
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Tasks
    SET IsDeleted = 1
    WHERE Id = @Id AND IsDeleted = 0;

    SELECT @@ROWCOUNT AS RowsAffected;
END
