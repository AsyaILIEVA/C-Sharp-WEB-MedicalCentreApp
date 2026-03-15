using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.GCommon.ViewModelValidation.PrescriptionViewModels;

namespace MedicalCentreApp.ViewModels.Prescriptions
{
    public class CreatePrescriptionViewModel
    {
        public Guid MedicalRecordId { get; set; }

        [Required]
        [StringLength(PrescriptionMedicationNameMaxLength)]
        public string MedicationName { get; set; } = null!;

        [Required]
        [StringLength(PrescriptionDosageMaxLength)]
        public string Dosage { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }
    }
}