using MedicalCentreApp.Data.Models.Enums;
using System;

namespace MedicalCentreApp.ViewModels.Appointments
{
    public class AppointmentInfoViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string DoctorName { get; set; } = null!;

        public string PatientName { get; set; } = null!;

        public string Reason { get; set; } = null!;

        public AppointmentStatus AppointmentStatus { get; set; }

        // Medical Record Info
        public bool HasMedicalRecord { get; set; }

        public string? Diagnosis { get; set; }

        public string? Prescription { get; set; }
    }
}
