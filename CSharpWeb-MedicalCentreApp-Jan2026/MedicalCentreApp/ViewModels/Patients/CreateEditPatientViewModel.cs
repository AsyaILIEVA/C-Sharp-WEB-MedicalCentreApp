using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.Common.EntityValidation;

namespace MedicalCentreApp.ViewModels.Patients
{
    public class CreateEditPatientViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(PatientFirstNameMinLength)]
        [MaxLength(PatientFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(PatientLastNameMinLength)]
        [MaxLength(PatientLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [MinLength(PatientEGNMinLength)]
        [MaxLength(PatientEGNMaxLength)]
        public string EGN { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MinLength(PatientPhoneNumberMinLength)]
        [MaxLength(PatientPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;
    }
}
