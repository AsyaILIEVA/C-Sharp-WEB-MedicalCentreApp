using MedicalCentreApp.Data;
using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Departments;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class DepartmentService : IDepartmentService
    {
        private readonly MedicalCentreAppDbContext dbContext;

        public DepartmentService(MedicalCentreAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetAllAsync()
        {
            IEnumerable<DepartmentViewModel> departments = await dbContext.Departments
                .AsNoTracking()
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

            await dbContext.Departments.AddAsync(department);
            await dbContext.SaveChangesAsync();
        }

        public async Task<DepartmentViewModel?> GetForDeleteAsync(int id)
        {
            DepartmentViewModel? department = await dbContext.Departments
                .AsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new DepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .FirstOrDefaultAsync();

            return department;
        }

        public async Task DeleteAsync(int id)
        {
            Department? department = await dbContext.Departments
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department != null)
            {
                dbContext.Departments.Remove(department);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            bool exists = await dbContext.Departments
                .AnyAsync(d => d.Id == id);

            return exists;
        }
    }
}