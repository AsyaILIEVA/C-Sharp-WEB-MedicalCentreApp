using MedicalCentreApp.Models;
using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.Common.EntityValidation;

namespace MedicalCentreApp.ViewModels.Doctors
{
    public class DoctorDetailsViewModel
    {
        public int Id { get; set; }

        [MinLength(DoctorFullNameMinLength)]
        [MaxLength(DoctorFullNameMaxLength)]
        public string FullName { get; set; } = null!;

       
        [MinLength(DoctorSpecialtyMinLength)]
        [MaxLength(DoctorSpecialtyMaxLength)]
        public string Specialty { get; set; } = null!;
        
        public string? ImageUrl { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
            = new HashSet<Appointment>();
    }
}
