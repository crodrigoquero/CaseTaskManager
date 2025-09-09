# Seed Script: Case Statuses
 
This script inserts default Case Status records into the `CaseStatuses` table.
  
```sql
INSERT INTO CaseStatuses (Id, StatusName)
VALUES
    (1, N'Open'),
    (2, N'In Progress'),
    (3, N'Pending Review'),
    (4, N'Approved'),
    (5, N'Rejected'),
    (6, N'On Hold'),
    (7, N'Escalated'),
    (8, N'Closed'),
    (9, N'Reopened');
```

  