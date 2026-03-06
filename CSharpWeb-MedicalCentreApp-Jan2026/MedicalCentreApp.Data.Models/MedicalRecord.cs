using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MedicalCentreApp.GCommon.EntityValidation.MedicalRecord;

namespace MedicalCentreApp.Data.Models
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

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Prescription> Prescriptions { get; set; }
            = new HashSet<Prescription>();
    }
}
