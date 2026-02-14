using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.GCommon.EntityValidation;

namespace MedicalCentreApp.Data.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PatientFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(PatientMiddleNameMaxLength)]
        public string MiddleName { get; set; } = null!;

        [Required]
        [MaxLength(PatientLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(PatientEGNMaxLength)]        
        public string EGN { get; set; } = null!;
       
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(PatientPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;
        
        
        [MaxLength(PatientEmailMaxLength)]
        public string? Email { get; set; } 

        
        [MaxLength(PatientAddressMaxLength)]
        public string? Address { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        = new HashSet<Appointment>();
    }
}
