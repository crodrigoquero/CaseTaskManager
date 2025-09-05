CREATE PROCEDURE sp_AddCaseStatus
    @StatusName NVARCHAR(100)
AS
BEGIN
    -- Check for an existing soft-deleted status with the same name to reuse
    IF EXISTS (SELECT 1 FROM CaseStatuses WHERE StatusName = @StatusName AND IsDeleted = 1)
    BEGIN
        UPDATE CaseStatuses
        SET IsDeleted = 0
        WHERE StatusName = @StatusName;

        SELECT Id AS ReusedCaseStatusId
        FROM CaseStatuses
        WHERE StatusName = @StatusName;
    END
    ELSE
    BEGIN
        INSERT INTO CaseStatuses (StatusName)
        VALUES (@StatusName);

        SELECT SCOPE_IDENTITY() AS NewCaseStatusId;
    END
END
