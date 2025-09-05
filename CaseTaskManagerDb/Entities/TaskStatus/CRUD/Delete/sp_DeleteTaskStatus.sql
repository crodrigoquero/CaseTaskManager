CREATE PROCEDURE sp_DeleteTaskStatus
    @Id INT
AS
BEGIN
    UPDATE TaskStatuses
    SET IsDeleted = 1
    WHERE Id = @Id;
END
