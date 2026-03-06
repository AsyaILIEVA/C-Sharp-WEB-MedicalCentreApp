using MedicalCentreApp.Data.Models.Enums;

namespace MedicalCentreApp.ViewModels.Appointments
{
    public class AppointmentDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public string Reason { get; set; } = null!;

        public AppointmentStatus Status { get; set; }

        public string PatientName { get; set; } = null!;

        public string DoctorName { get; set; } = null!;

        public bool HasMedicalRecord { get; set; }

        public Guid? MedicalRecordId { get; set; }

        public string? Diagnosis { get; set; }

        public List<string> Prescriptions { get; set; } = new();
    }
}
