namespace ClinicalTrialETL.Models;

public class VisitRecord
{
    public string PatientCode { get; set; } = string.Empty;
    public int VisitNumber { get; set; }
    public DateTime VisitDate { get; set; }
    public bool Completed { get; set; }
}