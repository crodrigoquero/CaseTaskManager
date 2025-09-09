# Seed Script: Case Statuses
 
This script inserts default Case Status records into the `CaseStatuses` table.
  
```sql
-- Idempotent insert for CaseStatuses
MERGE INTO CaseStatuses AS target
USING (VALUES
    (1, N'Open'),
    (2, N'In Progress'),
    (3, N'Pending Review'),
    (4, N'Approved'),
    (5, N'Rejected'),
    (6, N'On Hold'),
    (7, N'Escalated'),
    (8, N'Closed'),
    (9, N'Reopened')
) AS source (Id, StatusName)
ON target.Id = source.Id
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, StatusName) VALUES (source.Id, source.StatusName);
```

  