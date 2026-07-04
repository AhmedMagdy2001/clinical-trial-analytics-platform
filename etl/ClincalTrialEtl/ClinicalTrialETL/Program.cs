using ClinicalTrialETL.Services;

var csvService = new CsvService();
var databaseService = new DatabaseService();

string baseDir = AppContext.BaseDirectory;
string path = Path.Combine(baseDir, "../../../Data/patients.csv");

var patients = csvService.ReadPatients(path);

Console.WriteLine($"Patients Loaded: {patients.Count}");

databaseService.InsertPatients(patients);

Console.WriteLine("Patients imported successfully!");