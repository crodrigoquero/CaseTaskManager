CREATE PROCEDURE sp_UpdateCase
    @Id INT,
    @Title NVARCHAR(200),
    @Description NVARCHAR(MAX)

AS
BEGIN
    UPDATE Cases
    SET Title = @Title,
        Description = @Description
    WHERE Id = @Id AND IsDeleted = 0;
END
