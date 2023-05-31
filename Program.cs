using System;
using System.Collections.Generic;

class Program
{
    static Hospital hospital = new Hospital();

    static void Main(string[] args)
    {
        while (true)
        {
            PrintMenu();
            Console.WriteLine("Enter your choice:");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                continue;
            }

            Console.Clear();

            switch (choice)
            {
                case 1:
                    AddPatient();
                    break;
                case 2:
                    UpdatePatientInformation();
                    break;
                case 3:
                    RemovePatient();
                    break;
                case 4:
                    ViewAllPatients();
                    break;
                case 5:
                    SortPatients();
                    break;
                case 6:
                    PrintPatientCount();
                    break;
                case 7:
                    SearchPatientByName();
                    break;
                case 8:
                    Console.WriteLine("Exiting the program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("===========================");
        Console.WriteLine("  Hospital Management System");
        Console.WriteLine("===========================");
        Console.WriteLine("1. Add Patient");
        Console.WriteLine("2. Update Patient Information");
        Console.WriteLine("3. Remove Patient");
        Console.WriteLine("4. View All Patients");
        Console.WriteLine("5. Sort Patients by ID");
        Console.WriteLine("6. Patient Count");
        Console.WriteLine("7. Search Patient by Name");
        Console.WriteLine("8. Exit");
        Console.WriteLine("===========================");
    }

    static void AddPatient()
    {
        Console.WriteLine("Enter patient name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter patient age:");
        int age = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter patient address:");
        string address = Console.ReadLine();

        Console.WriteLine("Enter patient diagnosis:");
        string diagnosis = Console.ReadLine();

        Console.WriteLine("Enter patient ID:");
        int id = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter patient contact number:");
        string contactNumber = Console.ReadLine();

        Patient patient = new Patient(name, age, address, diagnosis, id, contactNumber);

        hospital.AddPatient(patient);
        Console.WriteLine("Patient added successfully.");
    }

    static void UpdatePatientInformation()
    {
        Console.WriteLine("Enter patient ID to update:");
        int patientId = int.Parse(Console.ReadLine());

        Patient patientToUpdate = hospital.GetPatientById(patientId);

        if (patientToUpdate != null)
        {
            Console.WriteLine("Enter updated patient name:");
            string updatedName = Console.ReadLine();

            Console.WriteLine("Enter updated patient age:");
            int updatedAge = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter updated patient address:");
            string updatedAddress = Console.ReadLine();

            Console.WriteLine("Enter updated patient diagnosis:");
            string updatedDiagnosis = Console.ReadLine();

            Console.WriteLine("Enter updated patient contact number:");
            string updatedContactNumber = Console.ReadLine();

            patientToUpdate.UpdateInformation(updatedName, updatedAge, updatedAddress, updatedDiagnosis, updatedContactNumber);

            Console.WriteLine("Patient information updated successfully.");
        }
        else
        {
            Console.WriteLine("Patient not found.");
        }
    }

    static void RemovePatient()
    {
        Console.WriteLine("Enter patient ID to remove:");
        int patientId = int.Parse(Console.ReadLine());

        if (hospital.RemovePatient(patientId))
        {
            Console.WriteLine("Patient removed successfully.");
        }
        else
        {
            Console.WriteLine("Patient not found.");
        }
    }

    static void ViewAllPatients()
    {
        List<Patient> patients = hospital.GetAllPatients();

        if (patients.Count == 0)
        {
            Console.WriteLine("No patients found.");
        }
        else
        {
            Console.WriteLine("All Patients:");
            foreach (Patient patient in patients)
            {
                Console.WriteLine(patient);
                Console.WriteLine("---------------------------");
            }
        }
    }

    static void SortPatients()
    {
        hospital.SortPatientsById();

        Console.WriteLine("Patients sorted by ID.");
    }

    static void PrintPatientCount()
    {
        int count = hospital.GetPatientCount();
        Console.WriteLine($"Total Patients: {count}");
    }

    static void SearchPatientByName()
    {
        Console.WriteLine("Enter patient name to search:");
        string name = Console.ReadLine();

        List<Patient> patients = hospital.SearchPatientsByName(name);

        if (patients.Count == 0)
        {
            Console.WriteLine("No patients found.");
        }
        else
        {
            Console.WriteLine($"Patients with name '{name}':");
            foreach (Patient patient in patients)
            {
                Console.WriteLine(patient);
                Console.WriteLine("---------------------------");
            }
        }
    }
}

class Hospital
{
    private List<Patient> patients;

    public Hospital()
    {
        patients = new List<Patient>();
    }

    public void AddPatient(Patient patient)
    {
        patients.Add(patient);
    }

    public bool RemovePatient(int patientId)
    {
        Patient patientToRemove = GetPatientById(patientId);
        if (patientToRemove != null)
        {
            patients.Remove(patientToRemove);
            return true;
        }
        return false;
    }

    public List<Patient> GetAllPatients()
    {
        return patients;
    }

    public void SortPatientsById()
    {
        patients.Sort((x, y) => x.Id.CompareTo(y.Id));
    }

    public int GetPatientCount()
    {
        return patients.Count;
    }

    public List<Patient> SearchPatientsByName(string name)
    {
        List<Patient> searchResults = new List<Patient>();

        foreach (Patient patient in patients)
        {
            if (patient.Name.ToLower().Contains(name.ToLower()))
            {
                searchResults.Add(patient);
            }
        }

        return searchResults;
    }

    public Patient GetPatientById(int patientId)
    {
        foreach (Patient patient in patients)
        {
            if (patient.Id == patientId)
            {
                return patient;
            }
        }
        return null;
    }
}

class Patient
{
    public string Name { get; private set; }
    public int Age { get; private set; }
    public string Address { get; private set; }
    public string Diagnosis { get; private set; }
    public int Id { get; private set; }
    public string ContactNumber { get; private set; }

    public Patient(string name, int age, string address, string diagnosis, int id, string contactNumber)
    {
        Name = name;
        Age = age;
        Address = address;
        Diagnosis = diagnosis;
        Id = id;
        ContactNumber = contactNumber;
    }

    public void UpdateInformation(string name, int age, string address, string diagnosis, string contactNumber)
    {
        Name = name;
        Age = age;
        Address = address;
        Diagnosis = diagnosis;
        ContactNumber = contactNumber;
    }

    public override string ToString()
    {
        return $"Name: {Name}\nAge: {Age}\nAddress: {Address}\nDiagnosis: {Diagnosis}\nID: {Id}\nContact Number: {ContactNumber}";
    }
}