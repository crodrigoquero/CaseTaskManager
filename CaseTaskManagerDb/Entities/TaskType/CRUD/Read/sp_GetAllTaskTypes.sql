CREATE PROCEDURE sp_GetAllTaskTypes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, TypeName, Description
    FROM TaskTypes
    WHERE IsDeleted = 0;
END
