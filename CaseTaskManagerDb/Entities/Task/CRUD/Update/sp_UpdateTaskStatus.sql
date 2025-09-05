CREATE PROCEDURE sp_UpdateTaskStatus
    @TaskId INT,
    @StatusId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Tasks
    SET StatusId = @StatusId
    WHERE Id = @TaskId AND IsDeleted = 0;
END
