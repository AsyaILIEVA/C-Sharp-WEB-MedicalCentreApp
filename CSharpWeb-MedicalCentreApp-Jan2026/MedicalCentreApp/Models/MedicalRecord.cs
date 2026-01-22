using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MedicalCentreApp.Common.EntityValidation;

namespace MedicalCentreApp.Models
{
    public class MedicalRecord
    {
        [Key]
        public Guid Id { get; set; } 
                
        [ForeignKey(nameof(Appointment))]
        public int AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; } = null!;

        [Required]
        [MaxLength(MedicalRecordDiagnosisMaxLength)]
        public string Diagnosis { get; set; } = null!;

        [MaxLength(MedicalRecordPrescriptionMaxLength)]
        public string? Prescription { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } 
            = DateTime.UtcNow;
    }
}
