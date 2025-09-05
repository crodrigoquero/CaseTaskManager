CREATE PROCEDURE sp_AddTaskStatus
    @StatusName NVARCHAR(100)
AS
BEGIN
    INSERT INTO TaskStatuses (StatusName, IsDeleted)
    VALUES (@StatusName, 0);

    SELECT SCOPE_IDENTITY() AS NewTaskStatusId;
END
