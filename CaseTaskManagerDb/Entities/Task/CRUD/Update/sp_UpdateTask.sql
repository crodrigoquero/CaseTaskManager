CREATE PROCEDURE sp_UpdateTask
    @Id INT,
    @Title NVARCHAR(200),
    @Description NVARCHAR(MAX),
    @StatusId INT,
    @TaskTypeId INT,
    @DueDate DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Tasks
    SET Title = @Title,
        Description = @Description,
        StatusId = @StatusId,
        TaskTypeId = @TaskTypeId,
        DueDate = @DueDate
    WHERE Id = @Id AND IsDeleted = 0;
END
