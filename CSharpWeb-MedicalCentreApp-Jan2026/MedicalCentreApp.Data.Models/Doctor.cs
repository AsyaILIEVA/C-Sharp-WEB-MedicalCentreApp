using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.GCommon.EntityValidation;

namespace MedicalCentreApp.Data.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DoctorFullNameMaxLength)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(DoctorSpecialtyMaxLength)]
        public string Specialty { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } 
            = new HashSet<Appointment>();
    }
}
