using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.GCommon.EntityValidation;

namespace MedicalCentreApp.ViewModels.Patients
{
    public class CreateEditPatientViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MinLength(PatientFirstNameMinLength)]
        [MaxLength(PatientFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Middle Name")]
        [MinLength(PatientMiddleNameMinLength)]
        [MaxLength(PatientMiddleNameMaxLength)]
        public string MiddleName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        [MinLength(PatientLastNameMinLength)]
        [MaxLength(PatientLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [Display(Name = "EGN")]
        [MinLength(PatientEGNMinLength)]
        [MaxLength(PatientEGNMaxLength)]
        public string EGN { get; set; } = null!;

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [MinLength(PatientPhoneNumberMinLength)]
        [MaxLength(PatientPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Email Address")]
        [MinLength(PatientEmailMinLength)]
        [MaxLength(PatientEmailMaxLength)]
        public string? Email { get; set; }

        [Display(Name = "Home Address")]
        [MinLength(PatientAddressMinLength)]
        [MaxLength(PatientAddressMaxLength)]
        public string? Address { get; set; }
    }
}
