USE CaseTaskManagerDb;
GO

/* ============================
   CASE STATUSES
   ============================ */

INSERT INTO CaseStatuses (StatusName, Description)
VALUES
('Open', 'Case has been created and is awaiting processing'),
('In Progress', 'Work on the case has started'),
('On Hold', 'Case is temporarily paused'),
('Closed', 'Case has been completed');
GO


/* ============================
   TASK STATUSES
   ============================ */

INSERT INTO TaskStatuses (StatusName)
VALUES
('Pending'),
('In Progress'),
('Blocked'),
('Completed'),
('Cancelled');
GO


/* ============================
   TASK TYPES
   ============================ */

INSERT INTO TaskTypes (TypeName, Description)
VALUES
('Investigation', 'Investigate issue or gather evidence'),
('Documentation', 'Create or update documentation'),
('Customer Contact', 'Contact customer for information or updates'),
('Review', 'Internal review of case information'),
('Resolution', 'Final resolution activities');
GO


/* ============================
   CASE WORKERS
   ============================ */

INSERT INTO CaseWorkers (FullName, Email)
VALUES
('Alice Johnson', 'alice.johnson@casemanager.local'),
('Brian Smith', 'brian.smith@casemanager.local'),
('Carla Rodriguez', 'carla.rodriguez@casemanager.local'),
('David Patel', 'david.patel@casemanager.local');
GO


/* ============================
   CASES
   ============================ */

INSERT INTO Cases (Title, Description, CurrentStatusId)
VALUES
(
'Customer billing dispute',
'Customer claims incorrect charge on invoice INV-4421.',
1
),
(
'Missing shipment investigation',
'Customer reports shipment tracking stopped updating.',
2
),
(
'Account access issue',
'Customer cannot log into account after password reset.',
1
);
GO


/* ============================
   CASE ASSIGNMENTS
   ============================ */

INSERT INTO CaseAssignments (CaseId, CaseWorkerId)
VALUES
(1,1),
(2,2),
(3,3);
GO


/* ============================
   TASKS
   ============================ */

INSERT INTO Tasks (CaseId, Title, Description, StatusId, TaskTypeId, DueDate)
VALUES
(
1,
'Review invoice records',
'Check billing system logs and invoice generation.',
2,
1,
DATEADD(day,3,GETDATE())
),
(
1,
'Contact customer for clarification',
'Verify the disputed charge details.',
1,
3,
DATEADD(day,2,GETDATE())
),
(
2,
'Check logistics tracking system',
'Verify shipment updates in carrier API.',
2,
1,
DATEADD(day,4,GETDATE())
),
(
3,
'Reset user credentials',
'Assist customer with secure password reset.',
1,
5,
DATEADD(day,1,GETDATE())
);
GO