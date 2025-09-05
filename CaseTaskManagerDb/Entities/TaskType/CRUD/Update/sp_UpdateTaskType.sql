CREATE PROCEDURE sp_UpdateTaskType
    @Id INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE TaskTypes
    SET TypeName = @Name,
        Description = @Description
    WHERE Id = @Id AND IsDeleted = 0;
END
