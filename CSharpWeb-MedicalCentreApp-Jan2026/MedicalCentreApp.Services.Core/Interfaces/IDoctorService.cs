using MedicalCentreApp.ViewModels.Doctors;

namespace MedicalCentreApp.Services.Core.Interfaces
{
    public interface IDoctorService
    {
        
        Task<IEnumerable<DoctorListViewModel>> GetAllAsync(string? specialty);

        Task CreateAsync(CreateDoctorInputModel model);

        Task<EditDoctorInputModel?> GetForEditAsync(int id);

        Task<bool> UpdateAsync(EditDoctorInputModel model);

        Task<DoctorDetailsViewModel?> GetForDeleteAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<DoctorDetailsViewModel?> GetDetailsAsync(int id);
    }
}
