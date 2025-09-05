CREATE PROCEDURE sp_AddTaskType
    @TypeName NVARCHAR(100),
    @Description NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if a soft-deleted type with the same name exists to reactivate
    IF EXISTS (SELECT 1 FROM TaskTypes WHERE TypeName = @TypeName AND IsDeleted = 1)
    BEGIN
        UPDATE TaskTypes
        SET IsDeleted = 0,
            Description = @Description
        WHERE TypeName = @TypeName;

        SELECT Id AS ReusedTaskTypeId
        FROM TaskTypes
        WHERE TypeName = @TypeName;
    END
    ELSE
    BEGIN
        INSERT INTO TaskTypes (TypeName, Description)
        VALUES (@TypeName, @Description);

        SELECT SCOPE_IDENTITY() AS NewTaskTypeId;
    END
END
