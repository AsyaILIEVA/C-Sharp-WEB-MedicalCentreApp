using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.Common.EntityValidation;

namespace MedicalCentreApp.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PatientFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(PatientLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [Length(PatientEGNMinLength, PatientEGNMaxLength)]
        // [StringLength(PatientEGNMaxLength, MinimumLength = 10)]
        public string EGN { get; set; } = null!;
       
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Length(PatientPhoneNumberMinLength, PatientPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; }
        = new HashSet<Appointment>();
    }
}
