using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.Common.EntityValidation;

namespace MedicalCentreApp.ViewModels.Doctors
{
    public class EditDoctorInputModel
    {
        public int Id { get; set; }

        [MinLength(DoctorSpecialtyMinLength)]
        [MaxLength(DoctorFullNameMaxLength)]
        public string FullName { get; set; } = null!;

        [MinLength(DoctorSpecialtyMinLength)]
        [MaxLength(DoctorSpecialtyMaxLength)]
        public string Specialty { get; set; } = null!;

        public IFormFile? Image { get; set; }

        public string? ExistingImageUrl { get; set; }
    }
}
