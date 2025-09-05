CREATE TABLE [dbo].[CaseAssignments]
(
    Id INT PRIMARY KEY IDENTITY,
    CaseId INT NOT NULL,
    CaseWorkerId INT NOT NULL,
    AssignedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    RemovedAt DATETIME2 NULL,
    FOREIGN KEY (CaseId) REFERENCES Cases(Id),
    FOREIGN KEY (CaseWorkerId) REFERENCES CaseWorkers(Id)
)
