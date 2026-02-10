using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalCentreApp.ViewModels.Appointments
{
    public class CreateAppointmentViewModel
    {
            [Required]
            [Display(Name = "Appointment Date")]
            public DateTime Date { get; set; }

            [Required]
            [Display(Name = "Appointment Time")]
            public TimeSpan Time { get; set; }

            [Required]
            [Display(Name = "Reason for Visit")]
            public string Reason { get; set; } = null!;

            [Required]
            public int PatientId { get; set; }

            public string? PatientName { get; set; } 

            [Required]
            public int DoctorId { get; set; }

        
            public string? DoctorName { get; set; }

            public IEnumerable<SelectListItem> Patients { get; set; }
                = Enumerable.Empty<SelectListItem>();

            public IEnumerable<SelectListItem> Doctors { get; set; }
                = Enumerable.Empty<SelectListItem>();
    }        
}

