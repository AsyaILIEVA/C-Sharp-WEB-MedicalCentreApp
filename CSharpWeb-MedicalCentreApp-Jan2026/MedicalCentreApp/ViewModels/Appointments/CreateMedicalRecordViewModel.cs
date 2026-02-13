using System.ComponentModel.DataAnnotations;

namespace MedicalCentreApp.ViewModels.Appointments
{
    public class CreateMedicalRecordViewModel
    {
        public int AppointmentId { get; set; }

        [Required]
        public string Diagnosis { get; set; } = null!;

        public string? Prescription { get; set; }
    }
}
