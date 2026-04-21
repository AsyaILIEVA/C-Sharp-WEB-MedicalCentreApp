using MedicalCentreApp.ViewModels.Departments;

namespace MedicalCentreApp.Services.Core.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentViewModel>> GetAllAsync();

        Task CreateAsync(CreateDepartmentViewModel model);

        Task<DeleteDepartmentViewModel?> GetForDeleteAsync(int id);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}