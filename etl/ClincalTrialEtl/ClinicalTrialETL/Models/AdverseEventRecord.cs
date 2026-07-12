namespace ClinicalTrialETL.Models;

public class AdverseEventRecord
{
    public string PatientCode { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public bool Resolved { get; set; }
}