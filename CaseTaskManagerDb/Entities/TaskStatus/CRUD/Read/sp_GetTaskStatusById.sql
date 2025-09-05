CREATE PROCEDURE sp_GetTaskStatusById
    @Id INT
AS
BEGIN
    SELECT *
    FROM TaskStatuses
    WHERE Id = @Id AND IsDeleted = 0;
END
