namespace MedicalCentreApp.ViewModels.Patients
{
    public class PatientListViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string EGN { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}
