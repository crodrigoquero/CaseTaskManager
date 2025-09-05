CREATE PROCEDURE sp_GetTaskTypeById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, TypeName, Description
    FROM TaskTypes
    WHERE Id = @Id AND IsDeleted = 0;
END
