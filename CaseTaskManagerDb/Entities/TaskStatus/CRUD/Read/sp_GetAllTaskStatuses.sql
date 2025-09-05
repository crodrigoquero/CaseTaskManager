CREATE PROCEDURE sp_GetAllTaskStatuses
AS
BEGIN
    SELECT *
    FROM TaskStatuses
    WHERE IsDeleted = 0;
END
