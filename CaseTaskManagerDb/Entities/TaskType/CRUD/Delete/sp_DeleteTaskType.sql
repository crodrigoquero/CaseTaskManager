CREATE PROCEDURE sp_DeleteTaskType
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE TaskTypes
    SET IsDeleted = 1
    WHERE Id = @Id AND IsDeleted = 0;
END
