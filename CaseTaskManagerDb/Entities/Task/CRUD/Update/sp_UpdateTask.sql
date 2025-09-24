CREATE PROCEDURE dbo.sp_UpdateTask
    @Id INT,
    @Title NVARCHAR(200),
    @Description NVARCHAR(MAX),
    @StatusId INT,
    @TaskTypeId INT,
    @DueDate DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Tasks
    SET Title = @Title,
        Description = @Description,
        StatusId = @StatusId,
        TaskTypeId = @TaskTypeId,
        DueDate = @DueDate
    WHERE Id = @Id AND IsDeleted = 0;

    SELECT @@ROWCOUNT AS RowsAffected;  -- <-- explicit signal
END
