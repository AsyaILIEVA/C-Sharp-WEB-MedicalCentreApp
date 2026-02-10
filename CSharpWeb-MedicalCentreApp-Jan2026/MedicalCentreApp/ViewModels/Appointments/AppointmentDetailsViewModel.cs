namespace MedicalCentreApp.ViewModels.Appointments
{
    public class AppointmentDetailsViewModel
    {
        public DateTime Date { get; set; }

        public string Reason { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string PatientName { get; set; } = null!;

        public string DoctorName { get; set; } = null!;

        public bool HasMedicalRecord { get; set; }
    }
}
