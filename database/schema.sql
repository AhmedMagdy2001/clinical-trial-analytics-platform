USE ClinicalTrialAnalytics;
GO

-- =========================
-- Studies
-- =========================
CREATE TABLE Studies
(
    StudyId INT IDENTITY(1,1) PRIMARY KEY,
    StudyCode NVARCHAR(20) NOT NULL UNIQUE,
    StudyName NVARCHAR(100) NOT NULL,
    Phase NVARCHAR(10) NOT NULL,
    TherapeuticArea NVARCHAR(100) NOT NULL,
    TargetEnrollment INT NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NULL
);

-- =========================
-- Sites
-- =========================
CREATE TABLE Sites
(
    SiteId INT IDENTITY(1,1) PRIMARY KEY,
    SiteName NVARCHAR(100) NOT NULL,
    Country NVARCHAR(50) NOT NULL,
    City NVARCHAR(50) NOT NULL
);

-- =========================
-- Patients
-- =========================
CREATE TABLE Patients
(
    PatientId INT IDENTITY(1,1) PRIMARY KEY,
    PatientCode NVARCHAR(20) NOT NULL UNIQUE,
    StudyId INT NOT NULL,
    SiteId INT NOT NULL,
    Gender CHAR(1) NOT NULL,
    BirthDate DATE NOT NULL,
    EnrollmentDate DATE NOT NULL,
    Status NVARCHAR(20) NOT NULL,

    CONSTRAINT FK_Patients_Studies
        FOREIGN KEY (StudyId) REFERENCES Studies(StudyId),

    CONSTRAINT FK_Patients_Sites
        FOREIGN KEY (SiteId) REFERENCES Sites(SiteId)
);

-- =========================
-- Visits
-- =========================
CREATE TABLE Visits
(
    VisitId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    VisitNumber INT NOT NULL,
    VisitDate DATE NOT NULL,
    Completed BIT NOT NULL,

    CONSTRAINT FK_Visits_Patients
        FOREIGN KEY (PatientId) REFERENCES Patients(PatientId)
);

-- =========================
-- Adverse Events
-- =========================
CREATE TABLE AdverseEvents
(
    EventId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    Severity NVARCHAR(20) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    EventDate DATE NOT NULL,
    Resolved BIT NOT NULL,

    CONSTRAINT FK_AdverseEvents_Patients
        FOREIGN KEY (PatientId) REFERENCES Patients(PatientId)
);

-- =========================
-- ETL Import Log
-- =========================
CREATE TABLE ETLImportLog
(
    ImportId INT IDENTITY(1,1) PRIMARY KEY,
    FileName NVARCHAR(255),
    ImportDate DATETIME DEFAULT GETDATE(),
    RowsImported INT,
    Status NVARCHAR(20)
);