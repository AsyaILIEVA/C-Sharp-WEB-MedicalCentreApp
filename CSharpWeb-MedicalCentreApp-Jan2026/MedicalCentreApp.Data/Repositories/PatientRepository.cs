using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Data.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(MedicalCentreAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<(IEnumerable<Patient>, int)> GetPagedAsync(int page, int pageSize)
        {
            var query = dbContext.Patients;

            var totalCount = await query.CountAsync();

            var patients = await query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (patients, totalCount);
        }
    }
}