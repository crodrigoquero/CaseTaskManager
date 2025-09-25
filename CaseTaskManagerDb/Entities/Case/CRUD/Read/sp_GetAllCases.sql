CREATE PROCEDURE dbo.sp_GetAllCases
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.Id,
        c.Title,
        c.Description,
        c.CreatedAt,
        c.CurrentStatusId,
        cs.StatusName AS CurrentStatusName,
        c.IsDeleted
    FROM dbo.Cases AS c
    LEFT JOIN dbo.CaseStatuses AS cs
        ON cs.Id = c.CurrentStatusId
    WHERE c.IsDeleted = 0
    ORDER BY c.CreatedAt DESC;
END;
