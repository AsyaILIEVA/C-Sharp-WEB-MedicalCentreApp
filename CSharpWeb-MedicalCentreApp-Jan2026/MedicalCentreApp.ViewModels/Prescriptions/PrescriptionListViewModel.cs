namespace MedicalCentreApp.ViewModels.Prescriptions
{
    public class PrescriptionListViewModel
    {
        public int Id { get; set; }

        public string MedicationName { get; set; } = null!;

        public string Dosage { get; set; } = null!;

        public DateTime IssuedOn { get; set; }
    }

}
