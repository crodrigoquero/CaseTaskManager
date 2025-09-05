CREATE PROCEDURE sp_AddTask
    @CaseId INT,
    @Title NVARCHAR(200),
    @Description NVARCHAR(MAX),
    @StatusId INT,
    @TaskTypeId INT,
    @DueDate DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tasks (
        CaseId, Title, Description, StatusId, TaskTypeId, DueDate
    )
    VALUES (
        @CaseId, @Title, @Description, @StatusId, @TaskTypeId, @DueDate
    );

    SELECT SCOPE_IDENTITY() AS NewTaskId;
END
