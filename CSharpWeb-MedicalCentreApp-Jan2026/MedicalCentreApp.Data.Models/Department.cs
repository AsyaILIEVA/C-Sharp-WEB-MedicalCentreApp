using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.Data.Common.EntityValidation.Department;

namespace MedicalCentreApp.Data.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DepartmentNameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(DepartmentDescriptionMaxLength)]
        public string? Description { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
            = new HashSet<Doctor>();
    }
}
