using MedicalCentreApp.ViewModels.Prescriptions;
using System.ComponentModel.DataAnnotations;

namespace MedicalCentreApp.ViewModels.MedicalRecords
{
    public class CreateMedicalRecordViewModel
    {
        public int AppointmentId { get; set; }

        [Required]
        public string Diagnosis { get; set; } = null!;

        //public List<PrescriptionListViewModel> Prescriptions { get; set; } = new();
    }
}
