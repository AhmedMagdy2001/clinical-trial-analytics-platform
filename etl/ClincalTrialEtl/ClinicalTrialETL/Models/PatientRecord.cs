namespace ClinicalTrialETL.Models;

public class PatientRecord
{
    public string PatientCode { get; set; } = string.Empty;

    public string StudyCode { get; set; } = string.Empty;

    public string SiteName { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public string Status { get; set; } = string.Empty;
}