using MedicalCentreApp.Data.Models;

namespace MedicalCentreApp.Data.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> All();

        IQueryable<Department> AllAsNoTracking();

        Task<Department?> GetByIdAsync(int id);

        Task AddAsync(Department department);

        void Update(Department department);

        void Delete(Department department);

        Task<int> SaveChangesAsync();
    }
}