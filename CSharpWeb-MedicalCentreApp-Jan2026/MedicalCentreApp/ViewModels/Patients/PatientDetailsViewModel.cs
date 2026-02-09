namespace MedicalCentreApp.ViewModels.Patients
{
    
        public class PatientDetailsViewModel
        {
            public string FullName => $"{FirstName} {LastName}";

            public string FirstName { get; set; } = null!;

        // Middle Name

            public string LastName { get; set; } = null!;

            public string EGN { get; set; } = null!;

            public DateTime DateOfBirth { get; set; }

            public string PhoneNumber { get; set; } = null!;

            public ICollection<AppointmentInfoViewModel> Appointments { get; set; }
                = new List<AppointmentInfoViewModel>();
        }

        public class AppointmentInfoViewModel
        {
            public DateTime Date { get; set; }

            public string DoctorName { get; set; } = null!;

            public string Reason { get; set; } = null!;
        }
    }


