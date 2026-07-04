USE ClinicalTrialAnalytics;
GO

INSERT INTO Studies
(StudyCode, StudyName, Phase, TherapeuticArea, TargetEnrollment, Status, StartDate, EndDate)
VALUES
('CTA-001', 'CardioLife', 'III', 'Cardiology', 200, 'Active', '2025-01-01', NULL),
('CTA-002', 'NeuroCure', 'II', 'Neurology', 150, 'Active', '2025-02-15', NULL),
('CTA-003', 'OncoHope', 'III', 'Oncology', 300, 'Active', '2025-03-01', NULL),
('CTA-004', 'RespiraX', 'IV', 'Pulmonology', 120, 'Completed', '2024-05-01', '2025-05-01'),
('CTA-005', 'GlycoBalance', 'II', 'Endocrinology', 180, 'Active', '2025-04-01', NULL);

INSERT INTO Sites
(SiteName, Country, City)
VALUES
('Cairo University Hospital', 'Egypt', 'Cairo'),
('Ain Shams University Hospital', 'Egypt', 'Cairo'),
('Cleveland Clinic Abu Dhabi', 'UAE', 'Abu Dhabi'),
('King Faisal Specialist Hospital', 'Saudi Arabia', 'Riyadh'),
('Mayo Clinic', 'USA', 'Rochester'),
('Johns Hopkins Hospital', 'USA', 'Baltimore'),
('Royal London Hospital', 'United Kingdom', 'London'),
('Charité Hospital', 'Germany', 'Berlin');

