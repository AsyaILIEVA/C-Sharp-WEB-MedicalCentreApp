using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.GCommon.ViewModelValidation.DoctorViewModels;

namespace MedicalCentreApp.ViewModels.Doctors
{
    public class EditDoctorInputModel
    {
        public int Id { get; set; }

        [MinLength(DoctorSpecialtyMinLength)]
        [MaxLength(DoctorFullNameMaxLength)]
        public string FullName { get; set; } = null!;

        [Required]
        [MinLength(DoctorSpecialtyMinLength)]
        [MaxLength(DoctorSpecialtyMaxLength)]
        public string Specialty { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Please select a department")]
        public int DepartmentId { get; set; }
        
        public IEnumerable<SelectListItem> Departments { get; set; } = new List<SelectListItem>();

        public IFormFile? Image { get; set; }

        public string? ExistingImageUrl { get; set; }
    }
}
