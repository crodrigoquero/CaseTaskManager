CREATE PROCEDURE dbo.sp_GetCaseById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.Id,
        c.Title,
        c.Description,
        c.CreatedAt,
        c.CurrentStatusId,
        cs.StatusName AS CurrentStatusName,  -- NEW
        c.IsDeleted
    FROM dbo.Cases AS c
    LEFT JOIN dbo.CaseStatuses AS cs
        ON cs.Id = c.CurrentStatusId
    WHERE c.Id = @Id
      AND c.IsDeleted = 0;
END;
