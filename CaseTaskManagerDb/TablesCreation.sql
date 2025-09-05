-- CASEWORKERS
CREATE TABLE CaseWorkers (
    Id INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0
);

-- CASES
CREATE TABLE Cases (
    Id INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CurrentStatusId INT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
);

-- CASE STATUSES (reference table — soft delete supported)
CREATE TABLE CaseStatuses (
    Id INT PRIMARY KEY IDENTITY,
    StatusName NVARCHAR(100) NOT NULL UNIQUE,
    IsDeleted BIT NOT NULL DEFAULT 0
);

-- CASE ASSIGNMENTS (acts like a log — no need for soft delete, uses timestamps)
CREATE TABLE CaseAssignments (
    Id INT PRIMARY KEY IDENTITY,
    CaseId INT NOT NULL,
    CaseWorkerId INT NOT NULL,
    AssignedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    RemovedAt DATETIME2 NULL,
    FOREIGN KEY (CaseId) REFERENCES Cases(Id),
    FOREIGN KEY (CaseWorkerId) REFERENCES CaseWorkers(Id)
);

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
    FOREIGN KEY (CaseId) REFERENCES Cases(Id),
    FOREIGN KEY (StatusId) REFERENCES TaskStatuses(Id),
    FOREIGN KEY (TaskTypeId) REFERENCES TaskTypes(Id)
);

-- TASK STATUSES (reference table — soft delete supported)
CREATE TABLE TaskStatuses (
    Id INT PRIMARY KEY IDENTITY,
    StatusName NVARCHAR(100) NOT NULL UNIQUE,
    IsDeleted BIT NOT NULL DEFAULT 0
);

-- TASK TYPES (reference table — soft delete supported)
CREATE TABLE TaskTypes (
    Id INT PRIMARY KEY IDENTITY,
    TypeName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
);
