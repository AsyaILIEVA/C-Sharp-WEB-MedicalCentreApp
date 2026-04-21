using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Departments;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetAllAsync()
        {
            IEnumerable<DepartmentViewModel> departments = await departmentRepository
                .AllAsNoTracking()
                .Select(d => new DepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .OrderBy(d => d.Name)
                .ToListAsync();

            return departments;
        }

        public async Task CreateAsync(CreateDepartmentViewModel model)
        {
            Department department = new Department
            {
                Name = model.Name
            };

            await departmentRepository.AddAsync(department);

            await departmentRepository.SaveChangesAsync();
        }

        public async Task<DeleteDepartmentViewModel?> GetForDeleteAsync(int id)
        {
            DeleteDepartmentViewModel? department = await departmentRepository
                .AllAsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new DeleteDepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .FirstOrDefaultAsync();

            return department;
        }

        public async Task DeleteAsync(int id)
        {
            Department? department = await departmentRepository.GetByIdAsync(id);

            if (department != null)
            {
                departmentRepository.Delete(department);

                await departmentRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            bool exists = await departmentRepository
                .AllAsNoTracking()
                .AnyAsync(d => d.Id == id);

            return exists;
        }
    }
}