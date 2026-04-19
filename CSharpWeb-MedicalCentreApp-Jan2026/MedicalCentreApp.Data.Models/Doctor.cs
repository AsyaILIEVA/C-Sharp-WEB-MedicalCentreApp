using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MedicalCentreApp.Data.Common.EntityValidation.Doctor;

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


        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; } = null!;

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
            = new HashSet<Appointment>();
    }
}
