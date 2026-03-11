using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Data.Repositories
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(MedicalCentreAppDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Prescription?> GetByIdAsync(int id)
        {
            return await this.dbSet.FindAsync(id);
        }
    }
}