using ClinicalTrialETL.Models;
using Microsoft.Data.SqlClient;

namespace ClinicalTrialETL.Services;

public class DatabaseService
{
    private readonly string _connectionString =
        "Server=.;Database=ClinicalTrialAnalytics;Trusted_Connection=True;TrustServerCertificate=True;";

    public void InsertPatients(List<PatientRecord> patients)
    {
        using var connection = new SqlConnection(_connectionString);

        connection.Open();

        foreach (var patient in patients)
        {
            // Get StudyId
            int studyId = GetStudyId(connection, patient.StudyCode);

            // Get SiteId
            int siteId = GetSiteId(connection, patient.SiteName);

            var command = new SqlCommand(@"
                INSERT INTO Patients
                (
                    PatientCode,
                    StudyId,
                    SiteId,
                    Gender,
                    BirthDate,
                    EnrollmentDate,
                    Status
                )
                VALUES
                (
                    @PatientCode,
                    @StudyId,
                    @SiteId,
                    @Gender,
                    @BirthDate,
                    @EnrollmentDate,
                    @Status
                )", connection);

            command.Parameters.AddWithValue("@PatientCode", patient.PatientCode);
            command.Parameters.AddWithValue("@StudyId", studyId);
            command.Parameters.AddWithValue("@SiteId", siteId);
            command.Parameters.AddWithValue("@Gender", patient.Gender);
            command.Parameters.AddWithValue("@BirthDate", patient.BirthDate);
            command.Parameters.AddWithValue("@EnrollmentDate", patient.EnrollmentDate);
            command.Parameters.AddWithValue("@Status", patient.Status);

            command.ExecuteNonQuery();
        }
    }

    private int GetStudyId(SqlConnection connection, string studyCode)
    {
        var command = new SqlCommand(
            "SELECT StudyId FROM Studies WHERE StudyCode = @StudyCode",
            connection);

        command.Parameters.AddWithValue("@StudyCode", studyCode);

        return (int)command.ExecuteScalar()!;
    }

    private int GetSiteId(SqlConnection connection, string siteName)
    {
        var command = new SqlCommand(
            "SELECT SiteId FROM Sites WHERE SiteName = @SiteName",
            connection);

        command.Parameters.AddWithValue("@SiteName", siteName);

        return (int)command.ExecuteScalar()!;
    }
}