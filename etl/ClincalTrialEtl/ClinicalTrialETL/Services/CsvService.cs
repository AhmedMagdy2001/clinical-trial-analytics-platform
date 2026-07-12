using CsvHelper;
using ClinicalTrialETL.Models;
using System.Globalization;

namespace ClinicalTrialETL.Services;

public class CsvService
{
    public List<PatientRecord> ReadPatients(string path)
    {
        using var reader = new StreamReader(path);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csv.GetRecords<PatientRecord>().ToList();
    }
    public List<VisitRecord> ReadVisits(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csv.GetRecords<VisitRecord>().ToList();
    }

    public List<AdverseEventRecord> ReadAdverseEvents(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csv.GetRecords<AdverseEventRecord>().ToList();
    }
}