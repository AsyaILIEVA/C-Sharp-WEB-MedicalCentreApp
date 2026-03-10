using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;

namespace MedicalCentreApp.Data.Repositories
{
    public class MedicalRecordRepository : BaseRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(MedicalCentreAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<MedicalRecord?> GetByIdAsync(Guid id)
        {
            return await this.dbSet.FindAsync(id);
        }
    }
}