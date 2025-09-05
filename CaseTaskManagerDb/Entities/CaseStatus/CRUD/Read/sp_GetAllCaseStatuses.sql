CREATE PROCEDURE sp_GetAllCaseStatuses
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, StatusName
    FROM CaseStatuses
    WHERE IsDeleted = 0;
END
