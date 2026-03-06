using MedicalCentreApp.ViewModels.Prescriptions;
using System.ComponentModel.DataAnnotations;

namespace MedicalCentreApp.ViewModels.Appointments
{
    public class CreateMedicalRecordViewModel
    {
        public int AppointmentId { get; set; }

        [Required]
        public string Diagnosis { get; set; } = null!;

        public List<PrescriptionViewModel> Prescriptions { get; set; } = new();
    }
}
