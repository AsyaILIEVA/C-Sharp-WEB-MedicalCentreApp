using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.GCommon.EntityValidation.Department;

namespace MedicalCentreApp.ViewModels.Departments
{
    public class CreateDepartmentViewModel
    {
        [Required]
        [MaxLength(DepartmentNameMaxLength)]
        public string Name { get; set; } = null!;
    }
}