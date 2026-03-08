using MedicalCentreApp.ViewModels.Prescriptions;

namespace MedicalCentreApp.ViewModels.MedicalRecords
{
    public class MedicalRecordDetailsViewModel
    {
        public Guid Id { get; set; }

        public int AppointmentId { get; set; }

        public string Diagnosis { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public List<PrescriptionListViewModel> Prescriptions { get; set; } = new();
    }
}