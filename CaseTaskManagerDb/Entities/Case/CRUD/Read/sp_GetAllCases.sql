CREATE PROCEDURE sp_GetAllCases
AS
BEGIN
    SELECT *
    FROM Cases
    WHERE IsDeleted = 0;
END
