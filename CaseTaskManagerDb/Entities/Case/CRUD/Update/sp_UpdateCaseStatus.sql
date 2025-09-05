CREATE PROCEDURE sp_UpdateCaseStatus
    @CaseId INT,
    @StatusId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Cases
    SET CurrentStatusId = @StatusId
    WHERE Id = @CaseId AND IsDeleted = 0;
END
