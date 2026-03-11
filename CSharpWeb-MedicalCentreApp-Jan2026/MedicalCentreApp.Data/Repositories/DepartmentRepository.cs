using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;

namespace MedicalCentreApp.Data.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MedicalCentreAppDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            Department? department = await this.dbSet.FindAsync(id);

            return department;
        }
    }
}