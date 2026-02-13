namespace MedicalCentreApp.ViewModels.Patients
{
    public class PatientMedicalRecordViewModel
    {
            public DateTime AppointmentDate { get; set; }

            public string DoctorName { get; set; } = null!;

            public string Diagnosis { get; set; } = null!;

            public string? Prescription { get; set; }        

    }
}
