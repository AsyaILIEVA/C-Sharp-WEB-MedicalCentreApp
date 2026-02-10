namespace MedicalCentreApp.ViewModels.Appointments
{
    public class AppointmentListViewModel
    {        
            public int Id { get; set; }

            public DateTime Date { get; set; }

            public string PatientName { get; set; } = null!;

            public string DoctorName { get; set; } = null!;

            public string Status { get; set; } = null!;
        
    }

}
