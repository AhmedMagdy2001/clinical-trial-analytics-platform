using ClinicalTrialETL.Services;

var csvService = new CsvService();
var databaseService = new DatabaseService();


string baseDir = AppContext.BaseDirectory;
string path = Path.Combine(baseDir, "../../../Data/");

//patients
var patients = csvService.ReadPatients(path + "patients.csv");

//visits
var visits = csvService.ReadVisits(path + "visits.csv");
databaseService.InsertVisits(visits);

// Adverse Events
var adverseEvents = csvService.ReadAdverseEvents(path + "adverse_events.csv");
databaseService.InsertAdverseEvents(adverseEvents);

Console.WriteLine($"Patients Loaded: {patients.Count}");
Console.WriteLine($"Visits Loaded: {visits.Count}");
databaseService.InsertPatients(patients);

Console.WriteLine("Patients imported successfully!");