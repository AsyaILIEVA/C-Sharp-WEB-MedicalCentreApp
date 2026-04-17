using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.GCommon.ViewModelValidation.DoctorViewModels;

namespace MedicalCentreApp.ViewModels.Doctors
{
    public class CreateDoctorInputModel
    {        
        [MinLength(DoctorFullNameMinLength)]
        [MaxLength(DoctorFullNameMaxLength)]
        public string FullName { get; set; } = null!;

        [Required]
        [MinLength(DoctorSpecialtyMinLength)]
        [MaxLength(DoctorSpecialtyMaxLength)]
        public string Specialty { get; set; } = null!;

        public IFormFile? Image { get; set; }

        public int DepartmentId { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; } = null!;
    }
}
