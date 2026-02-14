using MedicalCentreApp.ViewModels.Appointments;
using System;

namespace MedicalCentreApp.ViewModels.Patients
{
    public class PatientDetailsViewModel
    {
        public int Id { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FirstName { get; set; } = null!;

        public string MiddleName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string EGN { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Email { get; set; }

        public string? Address { get; set; }

        public ICollection<AppointmentInfoViewModel> Appointments { get; set; }
            = new List<AppointmentInfoViewModel>();
    }
}
