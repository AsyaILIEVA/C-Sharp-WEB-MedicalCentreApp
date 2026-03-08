namespace MedicalCentreApp.ViewModels.Prescriptions
{
    public class PrescriptionDetailsViewModel
    {
        public int Id { get; set; }

        public string MedicationName { get; set; } = null!;

        public string Dosage { get; set; } = null!;

        public DateTime IssuedOn { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }


}
