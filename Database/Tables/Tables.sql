USE CaseTaskManagerDb;
GO

-- CASEWORKERS
CREATE TABLE CaseWorkers (
                             Id INT PRIMARY KEY IDENTITY,
                             FullName NVARCHAR(100) NOT NULL,
                             Email NVARCHAR(255) NOT NULL UNIQUE,
                             IsActive BIT NOT NULL DEFAULT 1,
                             IsDeleted BIT NOT NULL DEFAULT 0
);
GO

-- CASE STATUSES
CREATE TABLE [dbo].[CaseStatuses]
(
    Id INT PRIMARY KEY IDENTITY,
    StatusName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
);
GO

-- TASK STATUSES
CREATE TABLE TaskStatuses (
                              Id INT PRIMARY KEY IDENTITY,
                              StatusName NVARCHAR(100) NOT NULL UNIQUE,
                              IsDeleted BIT NOT NULL DEFAULT 0
);
GO

-- TASK TYPES
CREATE TABLE TaskTypes (
                           Id INT PRIMARY KEY IDENTITY,
                           TypeName NVARCHAR(100) NOT NULL UNIQUE,
                           Description NVARCHAR(255) NULL,
                           IsDeleted BIT NOT NULL DEFAULT 0
);
GO

-- CASES
CREATE TABLE Cases (
                       Id INT PRIMARY KEY IDENTITY,
                       Title NVARCHAR(200) NOT NULL,
                       Description NVARCHAR(MAX),
                       CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
                       CurrentStatusId INT NULL,
                       IsDeleted BIT NOT NULL DEFAULT 0,
                       CONSTRAINT FK_Cases_CaseStatuses
                           FOREIGN KEY (CurrentStatusId) REFERENCES CaseStatuses(Id)
);
GO

-- CASE ASSIGNMENTS
CREATE TABLE CaseAssignments (
                                 Id INT PRIMARY KEY IDENTITY,
                                 CaseId INT NOT NULL,
                                 CaseWorkerId INT NOT NULL,
                                 AssignedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
                                 RemovedAt DATETIME2 NULL,
                                 CONSTRAINT FK_CaseAssignments_Cases
                                     FOREIGN KEY (CaseId) REFERENCES Cases(Id),
                                 CONSTRAINT FK_CaseAssignments_CaseWorkers
                                     FOREIGN KEY (CaseWorkerId) REFERENCES CaseWorkers(Id)
);
GO

-- TASKS
CREATE TABLE Tasks (
                       Id INT PRIMARY KEY IDENTITY,
                       CaseId INT NOT NULL,
                       Title NVARCHAR(200) NOT NULL,
                       Description NVARCHAR(MAX),
                       StatusId INT NOT NULL,
                       TaskTypeId INT NOT NULL,
                       DueDate DATETIME2 NOT NULL,
                       CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
                       IsDeleted BIT NOT NULL DEFAULT 0,
                       CONSTRAINT FK_Tasks_Cases
                           FOREIGN KEY (CaseId) REFERENCES Cases(Id),
                       CONSTRAINT FK_Tasks_TaskStatuses
                           FOREIGN KEY (StatusId) REFERENCES TaskStatuses(Id),
                       CONSTRAINT FK_Tasks_TaskTypes
                           FOREIGN KEY (TaskTypeId) REFERENCES TaskTypes(Id)
);
GO