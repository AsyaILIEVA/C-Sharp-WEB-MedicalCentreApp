using MedicalCentreApp.ViewModels.Doctors;

namespace MedicalCentreApp.Services.Core.Interfaces
{
    public interface IDoctorService
    {
        Task<PagedList<DoctorListViewModel>> GetDoctorsWithPaginationAsync(string? specialty, int pageNumber, int pageSize);

        Task<IEnumerable<DoctorListViewModel>> GetAllAsync(string? specialty);

        Task CreateAsync(CreateDoctorInputModel model);

        Task<EditDoctorInputModel?> GetForEditAsync(int id);

        Task<bool> UpdateAsync(EditDoctorInputModel model);

        Task<DoctorDetailsViewModel?> GetForDeleteAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<DoctorDetailsViewModel?> GetDetailsAsync(int id);
    }
}
