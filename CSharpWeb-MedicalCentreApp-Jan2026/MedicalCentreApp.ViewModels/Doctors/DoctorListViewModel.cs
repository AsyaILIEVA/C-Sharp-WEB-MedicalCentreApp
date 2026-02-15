
namespace MedicalCentreApp.ViewModels.Doctors
{
    public class DoctorListViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Specialty { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }

}
