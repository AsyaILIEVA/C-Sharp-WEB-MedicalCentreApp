using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MedicalCentreApp.GCommon.EntityValidation.Prescription;

namespace MedicalCentreApp.Data.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PrescriptionMedicationNameMaxLength)]
        public string MedicationName { get; set; } = null!;

        [Required]
        [MaxLength(PrescriptionDosageMaxLength)]
        public string Dosage { get; set; } = null!;

        [Required]
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public DateTime? ExpirationDate { get; set; }

        [ForeignKey(nameof(MedicalRecord))]
        public Guid MedicalRecordId { get; set; }

        public virtual MedicalRecord MedicalRecord { get; set; } = null!;
    }
}
