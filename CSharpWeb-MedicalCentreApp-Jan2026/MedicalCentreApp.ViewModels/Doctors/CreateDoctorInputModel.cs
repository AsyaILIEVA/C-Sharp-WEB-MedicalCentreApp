using System.ComponentModel.DataAnnotations;
//using static MedicalCentreApp.Data.Common.EntityValidation.Doctor;
using static MedicalCentreApp.GCommon.ViewModelValidation.DoctorViewModels;

using Microsoft.AspNetCore.Http;

namespace MedicalCentreApp.ViewModels.Doctors
{
    public class CreateDoctorInputModel
    {        
        [MinLength(DoctorFullNameMinLength)]
        [MaxLength(DoctorFullNameMaxLength)]
        public string FullName { get; set; } = null!;

       
        [MinLength(DoctorSpecialtyMinLength)]
        [MaxLength(DoctorSpecialtyMaxLength)]
        public string Specialty { get; set; } = null!;

        
        public IFormFile? Image { get; set; }
    }
}
