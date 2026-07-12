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
            if (PatientExists(connection, patient.PatientCode))
                continue;
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
    public void InsertVisits(List<VisitRecord> visits)
    {
        using var connection = new SqlConnection(_connectionString);

        connection.Open();

        foreach (var visit in visits)
        {


            var patientIdCommand = new SqlCommand(
                "SELECT PatientId FROM Patients WHERE PatientCode = @PatientCode",
                connection);

            patientIdCommand.Parameters.AddWithValue("@PatientCode", visit.PatientCode);

            int patientId = (int)patientIdCommand.ExecuteScalar()!;

            if (VisitExists(connection, patientId, visit.VisitNumber))
                continue;

            var command = new SqlCommand(@"
            INSERT INTO Visits
            (
                PatientId,
                VisitNumber,
                VisitDate,
                Completed
            )
            VALUES
            (
                @PatientId,
                @VisitNumber,
                @VisitDate,
                @Completed
            )", connection);

            command.Parameters.AddWithValue("@PatientId", patientId);
            command.Parameters.AddWithValue("@VisitNumber", visit.VisitNumber);
            command.Parameters.AddWithValue("@VisitDate", visit.VisitDate);
            command.Parameters.AddWithValue("@Completed", visit.Completed);

            command.ExecuteNonQuery();
        }
    }

    public void InsertAdverseEvents(List<AdverseEventRecord> events)
    {
        using var connection = new SqlConnection(_connectionString);

        connection.Open();

        foreach (var adverseEvent in events)
        {
            var patientIdCommand = new SqlCommand(
                "SELECT PatientId FROM Patients WHERE PatientCode = @PatientCode",
                connection);

            patientIdCommand.Parameters.AddWithValue("@PatientCode", adverseEvent.PatientCode);

            int patientId = (int)patientIdCommand.ExecuteScalar()!;

            if (AdverseEventExists(connection, patientId, adverseEvent.EventDate))
                continue;

            var command = new SqlCommand(@"
            INSERT INTO AdverseEvents
            (
                PatientId,
                Severity,
                Category,
                EventDate,
                Resolved
            )
            VALUES
            (
                @PatientId,
                @Severity,
                @Category,
                @EventDate,
                @Resolved
            )", connection);

            command.Parameters.AddWithValue("@PatientId", patientId);
            command.Parameters.AddWithValue("@Severity", adverseEvent.Severity);
            command.Parameters.AddWithValue("@Category", adverseEvent.Category);
            command.Parameters.AddWithValue("@EventDate", adverseEvent.EventDate);
            command.Parameters.AddWithValue("@Resolved", adverseEvent.Resolved);

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
    private bool PatientExists(SqlConnection connection, string patientCode)
    {
        var command = new SqlCommand(
            "SELECT COUNT(*) FROM Patients WHERE PatientCode = @PatientCode",
            connection);

        command.Parameters.AddWithValue("@PatientCode", patientCode);

        return (int)command.ExecuteScalar()! > 0;
    }

    private bool VisitExists(SqlConnection connection, int patientId, int visitNumber)
    {
        var command = new SqlCommand(
            @"SELECT COUNT(*)
          FROM Visits
          WHERE PatientId = @PatientId
          AND VisitNumber = @VisitNumber",
            connection);

        command.Parameters.AddWithValue("@PatientId", patientId);
        command.Parameters.AddWithValue("@VisitNumber", visitNumber);

        return (int)command.ExecuteScalar()! > 0;
    }

    private bool AdverseEventExists(SqlConnection connection, int patientId, DateTime eventDate)
    {
        var command = new SqlCommand(
            @"SELECT COUNT(*)
          FROM AdverseEvents
          WHERE PatientId = @PatientId
          AND EventDate = @EventDate",
            connection);

        command.Parameters.AddWithValue("@PatientId", patientId);
        command.Parameters.AddWithValue("@EventDate", eventDate);

        return (int)command.ExecuteScalar()! > 0;
    }
}