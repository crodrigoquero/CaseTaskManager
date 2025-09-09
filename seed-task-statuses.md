# Seed Task Statuses
  
This script inserts default **Task Statuses** into the database.  
It is **idempotent**: running it multiple times will not duplicate records.

```sql
-- Pending
IF NOT EXISTS (SELECT 1 FROM TaskStatuses WHERE StatusName = N'Pending')
BEGIN
    INSERT INTO TaskStatuses (StatusName) VALUES (N'Pending');
END
GO

-- In Progress
IF NOT EXISTS (SELECT 1 FROM TaskStatuses WHERE StatusName = N'In Progress')
BEGIN
    INSERT INTO TaskStatuses (StatusName) VALUES (N'In Progress');
END
GO

-- Completed
IF NOT EXISTS (SELECT 1 FROM TaskStatuses WHERE StatusName = N'Completed')
BEGIN
    INSERT INTO TaskStatuses (StatusName) VALUES (N'Completed');
END
GO
```
